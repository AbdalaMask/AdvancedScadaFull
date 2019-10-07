using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Siemens.Core.Siemens;
using HslCommunication.Profinet.Siemens;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.Siemens.Core
{
    public class IODriverHelper : AdvancedScada.DriverBase.IODriver
    {
        public static readonly ManualResetEvent SendDone = new ManualResetEvent(true);
        public static List<Channel> Channels = new List<Channel>();

        //==================================Siemens===================================================
        private static Dictionary<string, SiemensNet> _PLCS7 = new Dictionary<string, SiemensNet>();
        private static Dictionary<string, SiemensComPPI> _PLCPPI = new Dictionary<string, SiemensComPPI>();
        private static bool IsConnected;
        private static int COUNTER;


        #region IServiceDriver
        public string Name => "Siemens";
        public void InitializeService(Channel ch)
        {

            try
            {

                //=================================================================

                if (Channels == null) return;
                Channels.Add(ch);


                IDriverAdapter DriverAdapter = null;
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

                                DriverAdapter = new SiemensComPPI(dv.SlaveId, sp);
                                _PLCPPI.Add(ch.ChannelName, (SiemensComPPI)DriverAdapter);
                                break;
                            case "Ethernet":
                                var die = (DIEthernet)ch;
                                var cpu = (SiemensPLCS)Enum.Parse(typeof(SiemensPLCS), die.CPU);
                                DriverAdapter = new SiemensNet(cpu, die.IPAddress, (short)die.Rack, (short)die.Slot);
                                _PLCS7.Add(ch.ChannelName, (SiemensNet)DriverAdapter);

                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                    }
                    foreach (var db in dv.DataBlocks)
                    {

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

        private static Thread[] threads;
        public void Connect()
        {

            try
            {
                IsConnected = true;


                Console.WriteLine(string.Format("STARTED: {0}", ++COUNTER));
                threads = new Thread[Channels.Count];

                if (threads == null) throw new NullReferenceException("No Data");
                for (int i = 0; i < Channels.Count; i++)
                {
                    threads[i] = new Thread((chParam) =>
                    {
                        IDriverAdapter DriverAdapter = null;
                        Channel ch = (Channel)chParam;

                        switch (ch.ConnectionType)
                        {
                            case "SerialPort":
                                DriverAdapter = _PLCPPI[ch.ChannelName];
                                break;

                            case "Ethernet":
                                DriverAdapter = _PLCS7[ch.ChannelName];
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

                                        SendPackageSiemens(DriverAdapter, dv, db);
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

                    })
                    {
                        IsBackground = true
                    };
                    threads[i].Start(Channels[i]);
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

                TagCollection.Tags.Clear();
                Channels = null;
                for (int i = 0; i < threads.Length; i++)
                {

                    threads[i].Abort();
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
        private void SendPackageSiemens(IDriverAdapter ISiemens, Device dv, DataBlock db)
        {
            try
            {

                switch (db.DataType)
                {
                    case DataTypes.Bit:

                        lock (ISiemens)
                        {

                            bool[] bitRs = ISiemens.Read<bool>($"{db.MemoryType}{db.StartAddress}", db.Length);

                            int length = bitRs.Length;
                            if (bitRs.Length > db.Tags.Count) length = db.Tags.Count;
                            for (int j = 0; j < length; j++)
                            {
                                db.Tags[j].Value = bitRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Short:

                        lock (ISiemens)
                        {
                            short[] IntRs = ISiemens.Read<Int16>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (IntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < IntRs.Length; j++)
                            {
                                
                                    db.Tags[j].Value = IntRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Int:

                        lock (ISiemens)
                        {
                            int[] DIntRs = ISiemens.Read<Int32>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (DIntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < DIntRs.Length; j++)
                            {
                                db.Tags[j].Value = DIntRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.UInt:

                        lock (ISiemens)
                        {
                            var wdRs = ISiemens.Read<uint>($"{db.MemoryType}{db.StartAddress}", db.Length);
                            if (wdRs == null) return;
                            if (wdRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < wdRs.Length; j++)
                            {
                                
                                    db.Tags[j].Value = wdRs[j];
                                
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Long:

                        lock (ISiemens)
                        {
                            long[] dwRs = ISiemens.Read<long>($"{db.MemoryType}{db.StartAddress}", (ushort)db.Length);

                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Float:

                        lock (ISiemens)
                        {
                            float[] rl1Rs = ISiemens.Read<float>($"{db.MemoryType}{db.StartAddress}", (ushort)db.Length);

                            for (int j = 0; j < rl1Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl1Rs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Double:

                        lock (ISiemens)
                        {
                            double[] rl2Rs = ISiemens.Read<double>($"{db.MemoryType}{db.StartAddress}", (ushort)db.Length);

                            for (int j = 0; j < rl2Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl2Rs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
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
                            IDriverAdapter DriverAdapter = null;
                            switch (ch.ConnectionType)
                            {
                                case "SerialPort":
                                    DriverAdapter = _PLCPPI[ch.ChannelName];
                                    break;

                                case "Ethernet":
                                    DriverAdapter = _PLCS7[ch.ChannelName];
                                    break;
                            }
                            if (DriverAdapter == null) return;
                            lock (DriverAdapter)
                                switch (TagCollection.Tags[tagName].DataType)
                                {
                                    case DataTypes.Bit:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), value == "1" ? true : false);
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
