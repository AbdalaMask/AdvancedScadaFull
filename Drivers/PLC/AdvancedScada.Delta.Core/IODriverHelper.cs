using AdvancedScada.Common;
using AdvancedScada.Delta.Common;
using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IODriver.Delta.ASCII;
using AdvancedScada.IODriver.Delta.RTU;
using AdvancedScada.IODriver.Delta.TCP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Delta.Core
{
    public class IODriverHelper : AdvancedScada.Common.IODriver
    {
        public static readonly ManualResetEvent SendDone = new ManualResetEvent(true);
        public static List<Channel> Channels = new List<Channel>();
        //==================================Delta===================================================
        private static Dictionary<string, DeltaTCPMaster> Deltambe = new Dictionary<string, DeltaTCPMaster>();
        private static Dictionary<string, DeltaRTUMaster> Deltartu = new Dictionary<string, DeltaRTUMaster>();
        private static Dictionary<string, DeltaASCIIMaster> Deltaascii = new Dictionary<string, DeltaASCIIMaster>();


        private static bool IsConnected;
        private static int COUNTER;
        #region IServiceDriver

        public string Name => "Delta";
        public Image ImageUrl => Properties.Resources.DVP10MC11T_300x300;
        public void InitializeService(Channel ch)
        {

            try
            {

                //=================================================================

                if (Channels == null) return;
                Channels.Add(ch);


                IDeltaAdapter DriverAdapter = null;
                foreach (var dv in ch.Devices)
                {
                    try
                    {
                        switch (ch.ConnectionType)
                        {
                            case "SerialPort":
                                var dis = (DISerialPort)ch;
                                var sp = new SerialPort(dis.PortName, dis.BaudRate, dis.Parity, dis.DataBits, dis.StopBits)
                                {
                                    Handshake = dis.Handshake
                                };

                                switch (dis.Mode)
                                {
                                    case "RTU":
                                        DriverAdapter = new DeltaRTUMaster(dv.SlaveId, sp);
                                        Deltartu.Add(ch.ChannelName, (DeltaRTUMaster)DriverAdapter);
                                        break;
                                    case "ASCII":
                                        DriverAdapter = new DeltaASCIIMaster(dv.SlaveId, sp);
                                        Deltaascii.Add(ch.ChannelName, (DeltaASCIIMaster)DriverAdapter);
                                        break;
                                }
                                break;
                            case "Ethernet":
                                var die = (DIEthernet)ch;


                                DriverAdapter = new DeltaTCPMaster(dv.SlaveId, die.IPAddress, die.Port);
                                Deltambe.Add(ch.ChannelName, (DeltaTCPMaster)DriverAdapter);


                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                    }
                    foreach (var db in dv.DataBlocks)
                    {
                        DataBlockCollection.DataBlocks.Add($"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}", db);
                        foreach (var tg in db.Tags)
                        {
                            TagCollection.Tags.Add(
                                $"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}.{tg.TagName}", tg);

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);

            }
        }

        private static Task[] taskArray;
        public void Connect()
        {

            try
            {
                IsConnected = true;


                Console.WriteLine(string.Format("STARTED: {0}", ++COUNTER));
                taskArray = new Task[Channels.Count];
                if (taskArray == null) throw new NullReferenceException("No Data");
                for (int i = 0; i < Channels.Count; i++)
                {
                    taskArray[i] = new Task((chParam) =>
                    {
                        IDeltaAdapter DriverAdapter = null;
                        Channel ch = (Channel)chParam;

                        switch (ch.Mode)
                        {
                            case "RTU":
                                DriverAdapter = Deltartu[ch.ChannelName];
                                break;
                            case "ASCII":
                                DriverAdapter = Deltaascii[ch.ChannelName];
                                break;
                            case "TCP":
                                DriverAdapter = Deltambe[ch.ChannelName];
                                break;
                        }


                        //======Connection to PLC==================================
                        DriverAdapter.Connection();

                        while (IsConnected)
                        {
                            try
                            {
                                foreach (Device dv in ch.Devices)
                                {

                                    foreach (DataBlock db in dv.DataBlocks)
                                    {
                                        if (!IsConnected) break;

                                        SendPackageDelta(DriverAdapter, db);


                                    }

                                }
                            }
                            catch (Exception)
                            {
                                Disconnect();
                                objConnectionState = ConnectionState.DISCONNECT;
                                eventConnectionState?.Invoke(objConnectionState, string.Format("Server disconnect with PLC."));
                            }
                            if (IsConnected && objConnectionState == ConnectionState.DISCONNECT)
                            {
                                objConnectionState = ConnectionState.CONNECT;
                                eventConnectionState?.Invoke(objConnectionState, string.Format("PLC connected to Server."));
                            }
                            else if (!IsConnected && objConnectionState == ConnectionState.CONNECT)
                            {
                                objConnectionState = ConnectionState.DISCONNECT;
                                eventConnectionState?.Invoke(objConnectionState, string.Format("Server disconnect with PLC."));
                            }
                        }

                    }, Channels[i]);
                    taskArray[i].Start();
                }
                foreach (var task in taskArray)
                {
                    var data = task.AsyncState as Channel;
                    if (data != null)
                        EventscadaException?.Invoke(this.GetType().Name, $"Task #{data.ChannelId} created at {data.ChannelName}, ran on thread #{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}.");


                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        public void Disconnect()
        {

            try
            {
                IsConnected = false;
                Deltambe = null;
                TagCollection.Tags.Clear();
                Channels = null;
                for (int i = 0; i < taskArray.Length; i++)
                {

                    taskArray[i].Wait(100);
                }

                objConnectionState = ConnectionState.DISCONNECT;
                eventConnectionState?.Invoke(objConnectionState, string.Format("Server disconnect with PLC."));
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);

            }
        }

        #endregion
        #region SendPackage All
        private void SendPackageDelta(IDeltaAdapter DriverAdapter, DataBlock db)
        {
            try
            {

                switch (db.DataType)
                {
                    case DataTypes.Bit:
                        lock (DriverAdapter)
                        {
                            bool[] bitRs = null;
                            if (db.TypeOfRead == "ReadCoilStatus")
                            {
                                bitRs = DriverAdapter.Read<bool>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            }
                            else if (db.TypeOfRead == "ReadInputStatus")
                            {
                                bitRs = DriverAdapter.ReadDiscrete($"{db.MemoryType}{db.StartAddress}", db.Length);
                            }


                            if (bitRs == null) return;
                            int length = bitRs.Length;
                            if (bitRs.Length > db.Tags.Count) length = db.Tags.Count;
                            for (int j = 0; j < length; j++)
                            {
                                db.Tags[j].Value = bitRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Byte:
                        lock (DriverAdapter)
                        {
                            byte[] IntRs = DriverAdapter.Read<byte>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (IntRs == null) return;
                            if (IntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < IntRs.Length; j++)
                            {

                                db.Tags[j].Value = IntRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Short:
                        lock (DriverAdapter)
                        {
                            short[] DIntRs = DriverAdapter.Read<short>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (DIntRs == null) return;
                            if (DIntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < DIntRs.Length; j++)
                            {
                                db.Tags[j].Value = DIntRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.UShort:
                        lock (DriverAdapter)
                        {
                            var wdRs = DriverAdapter.Read<ushort>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (wdRs == null) return;
                            if (wdRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < wdRs.Length; j++)
                            {

                                db.Tags[j].Value = wdRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Int:
                        lock (DriverAdapter)
                        {
                            int[] dwRs = DriverAdapter.Read<int>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (dwRs == null) return;
                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.UInt:
                        lock (DriverAdapter)
                        {
                            uint[] dwRs = DriverAdapter.Read<uint>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (dwRs == null) return;
                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Long:
                        lock (DriverAdapter)
                        {
                            long[] rl1Rs = DriverAdapter.Read<long>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (rl1Rs == null) return;
                            for (int j = 0; j < rl1Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl1Rs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.ULong:
                        lock (DriverAdapter)
                        {
                            ulong[] rl1Rs = DriverAdapter.Read<ulong>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (rl1Rs == null) return;
                            for (int j = 0; j < rl1Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl1Rs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Float:
                        lock (DriverAdapter)
                        {
                            float[] rl1Rs = DriverAdapter.Read<float>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (rl1Rs == null) return;
                            for (int j = 0; j < rl1Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl1Rs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Double:
                        lock (DriverAdapter)
                        {
                            double[] rl2Rs = DriverAdapter.Read<double>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (rl2Rs == null) return;
                            for (int j = 0; j < rl2Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl2Rs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.String:
                        break;
                    default:
                        break;
                }


            }

            catch (Exception ex)
            {
                Disconnect();
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        #endregion
        #region Write All


        public void WriteTag(string tagName, dynamic value)
        {
            try
            {
                SendDone.Reset();
                string[] ary = tagName.Split('.');
                string tagDevice = string.Format("{0}.{1}", ary[0], ary[1]);
                foreach (Channel ch in Channels)
                {
                    foreach (Device dv in ch.Devices)
                    {

                        if (string.Format("{0}.{1}", ch.ChannelName, dv.DeviceName).Equals(tagDevice))
                        {
                            IDeltaAdapter DriverAdapter = null;


                            switch (ch.Mode)
                            {
                                case "RTU":
                                    DriverAdapter = Deltartu[ch.ChannelName];
                                    break;
                                case "ASCII":
                                    DriverAdapter = Deltaascii[ch.ChannelName];
                                    break;
                                case "TCP":
                                    DriverAdapter = Deltambe[ch.ChannelName];
                                    break;
                            }

                            if (DriverAdapter == null) return;
                            lock (DriverAdapter)
                                switch (TagCollection.Tags[tagName].DataType)
                                {
                                    case DataTypes.Bit:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), value);
                                        break;
                                    case DataTypes.Byte:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), byte.Parse(value));

                                        break;
                                    case DataTypes.Short:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), short.Parse(value));

                                        break;
                                    case DataTypes.UShort:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), ushort.Parse(value));

                                        break;
                                    case DataTypes.Int:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), int.Parse(value));

                                        break;
                                    case DataTypes.UInt:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), uint.Parse(value));

                                        break;
                                    case DataTypes.Long:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), long.Parse(value));

                                        break;
                                    case DataTypes.ULong:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), ulong.Parse(value));

                                        break;
                                    case DataTypes.Float:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), float.Parse(value));

                                        break;
                                    case DataTypes.Double:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), double.Parse(value));

                                        break;
                                    case DataTypes.String:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), $"{value}");

                                        break;
                                    default:
                                        break;
                                }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            finally
            {
                SendDone.Set();
            }
        }

        #endregion
    }
}
