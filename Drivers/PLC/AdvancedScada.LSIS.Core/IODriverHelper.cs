using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.LSIS.Core.LSIS.Cnet;
using AdvancedScada.LSIS.Core.LSIS.FENET;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.LSIS.Core
{
    public class IODriverHelper : AdvancedScada.DriverBase.IODriver
    {
        public static readonly ManualResetEvent SendDone = new ManualResetEvent(true);
        public static List<Channel> Channels = new List<Channel>();

        //==================================LS===================================================
        private static Dictionary<string, LS_CNET> cnet = new Dictionary<string, LS_CNET>();
        private static Dictionary<string, LS_FENET> FENET = new Dictionary<string, LS_FENET>();

        private static bool IsConnected;
        private static int COUNTER;


        #region IServiceDriver
        public string Name => "LSIS";
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

                                DriverAdapter = new LS_CNET(dv.SlaveId, sp);
                                cnet.Add(ch.ChannelName, (LS_CNET)DriverAdapter);
                                break;
                            case "Ethernet":
                                var die = (DIEthernet)ch;

                                DriverAdapter = new LS_FENET(die.CPU, die.IPAddress, die.Port,(byte) die.Slot);
                                FENET.Add(ch.ChannelName, (LS_FENET)DriverAdapter);

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
                                DriverAdapter = cnet[ch.ChannelName];
                                break;

                            case "Ethernet":
                                DriverAdapter = FENET[ch.ChannelName];
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

                                        SendPackageLSIS(DriverAdapter, db);
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

        private void SendPackageLSIS(IDriverAdapter DriverAdapter, DataBlock db)
        {
            try
            {
                int baseAddress = db.StartAddress;
                switch (db.DataType)
                {
                    case DataTypes.BitOnByte:
                        baseAddress = ((db.StartAddress >= 2) ? (db.StartAddress / 2) : 0) * 2;
                        break;
                    case DataTypes.BitOnWord:
                        baseAddress = db.StartAddress * 2;
                        break;
                    case DataTypes.Bit:
                        baseAddress = ((db.StartAddress >= 16) ? (db.StartAddress / 16) : 0) * 2;
                        break;
                    case DataTypes.Byte:
                        baseAddress = db.StartAddress;
                        break;
                    case DataTypes.Short:
                    case DataTypes.UShort:
                        baseAddress = db.StartAddress * 2;
                        break;
                    case DataTypes.Int:
                    case DataTypes.UInt:
                        baseAddress = db.StartAddress * 4;
                        break;
                    case DataTypes.Long:
                    case DataTypes.ULong:
                        baseAddress = db.StartAddress * 8;
                        break;
                    case DataTypes.Float:
                        baseAddress = db.StartAddress * 4;
                        break;
                    case DataTypes.Double:
                        baseAddress = db.StartAddress * 8;
                        break;
                }
                SendDone.WaitOne(-1);
                switch (db.DataType)
                {
                    case DataTypes.BitOnByte:
                    case DataTypes.BitOnWord:
                        lock (DriverAdapter)
                        {
                            var bitArys2 = DriverAdapter.Read<bool>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", (ushort)(2 * db.Length));
                            if (bitArys2 == null || bitArys2.Length == 0) return;
                            if (bitArys2.Length > db.Tags.Count) return;
                            for (var j = 0; j <= db.Tags.Count - 1; j++)
                            {
                                db.Tags[j].Value = bitArys2[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Bit:
                        lock (DriverAdapter)
                        {
                            bool[] bitArys = null;
                            if (db.IsArray)
                            {
                                bitArys = DriverAdapter.Read<bool>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", (ushort)(2 * db.Length));

                            }
                            else
                            {
                                bitArys = DriverAdapter.ReadSingle($"{db.MemoryType}{db.StartAddress}", (ushort)(db.Length));
                            }
                            if (bitArys == null || bitArys.Length == 0) return;
                            if (bitArys.Length > db.Tags.Count) return;
                            for (var j = 0; j <= db.Tags.Count - 1; j++)
                            {
                                db.Tags[j].Value = bitArys[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Byte:
                        lock (DriverAdapter)
                        {
                            byte[] bitArys = DriverAdapter.Read<byte>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", (ushort)(2 * db.Length));
                            if (bitArys == null || bitArys.Length == 0) return;
                            if (bitArys.Length > db.Tags.Count) return;
                            for (var j = 0; j <= db.Tags.Count - 1; j++)
                            {
                                db.Tags[j].Value = bitArys[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Short:
                        lock (DriverAdapter)
                        {
                            short[] IntRs = DriverAdapter.Read<short>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", db.Length);
                            if (IntRs == null || IntRs.Length == 0) return;
                            if (IntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < IntRs.Length; j++)
                            {
                                db.Tags[j].Value = IntRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.UShort:
                        lock (DriverAdapter)
                        {
                            ushort[] ushortArys = DriverAdapter.Read<ushort>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", db.Length);
                            if (ushortArys == null || ushortArys.Length == 0) return;
                            if (ushortArys.Length > db.Tags.Count) return;
                            for (var j = 0; j <= db.Tags.Count - 1; j++)
                            {
                                db.Tags[j].Value = ushortArys[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Int:
                        lock (DriverAdapter)
                        {
                            int[] DIntRs = DriverAdapter.Read<int>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", db.Length);
                            if (DIntRs == null || DIntRs.Length == 0) return;
                            if (DIntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < DIntRs.Length; j++)
                            {
                                db.Tags[j].Value = DIntRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.UInt:
                        lock (DriverAdapter)
                        {
                            var wdRs = DriverAdapter.Read<uint>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", db.Length);
                            if (wdRs == null || wdRs.Length == 0) return;
                            if (wdRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < db.Tags.Count; j++)
                            {
                                db.Tags[j].Value = wdRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Long:
                        lock (DriverAdapter)
                        {
                            long[] dwRs = DriverAdapter.Read<long>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", db.Length);
                            if (dwRs == null || dwRs.Length == 0) return;
                            if (dwRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.ULong:
                        lock (DriverAdapter)
                        {
                            ulong[] dwRs = DriverAdapter.Read<ulong>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", db.Length);
                            if (dwRs == null || dwRs.Length == 0) return;
                            if (dwRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Float:
                        lock (DriverAdapter)
                        {
                            float[] rl1Rs = DriverAdapter.Read<float>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", db.Length);
                            if (rl1Rs == null || rl1Rs.Length == 0) return;
                            if (rl1Rs.Length > db.Tags.Count) return;
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
                            double[] rl2Rs = DriverAdapter.Read<double>($"{db.MemoryType.Substring(0, 1)}{baseAddress}", db.Length);
                            if (rl2Rs == null || rl2Rs.Length == 0) return;
                            if (rl2Rs.Length > db.Tags.Count) return;
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
                            IDriverAdapter DriverAdapter = null;
                            switch (ch.ConnectionType)
                            {
                                case "SerialPort":
                                    DriverAdapter = cnet[ch.ChannelName];
                                    break;

                                case "Ethernet":
                                    DriverAdapter = FENET[ch.ChannelName];
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
