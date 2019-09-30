using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService.Common;
using AdvancedScada.IODriver.Delta.ASCII;
using AdvancedScada.IODriver.Delta.RTU;
using AdvancedScada.IODriver.Delta.TCP;
using AdvancedScada.IODriver.LSIS.Cnet;
using AdvancedScada.IODriver.LSIS.FENET;
using AdvancedScada.IODriver.Modbus.ASCII;
using AdvancedScada.IODriver.Modbus.RTU;
using AdvancedScada.IODriver.Modbus.TCP;
using HslCommunication.Profinet.LSIS;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net.Sockets;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriver
{
    public partial class DriverHelper
    {
        public static readonly ManualResetEvent SendDone = new ManualResetEvent(true);
        public static List<Channel> Channels;
        //==================================All===================================================
        private static GenericDictionary DriverDictionary = null;

        
        private static bool IsConnected;
        private static int COUNTER;

        public static ConnectionState objConnectionState = ConnectionState.DISCONNECT;
    
      
        private static Queue<RequestWrite> RequestWriteToClient = new Queue<RequestWrite>();
        #region IServiceDriver


        public void InitializeService(List<Channel> chns)
        {
            DriverDictionary = new GenericDictionary();

            try
            {
                
                //=================================================================


                Channels = chns;
                if (Channels == null) return;
                foreach (Channel ch in Channels)
                {
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
                                    switch (ch.ChannelTypes)
                                    {
                                        case "Delta":
                                            switch (dis.Mode)
                                            {
                                                case "RTU":
                                                    DriverAdapter = new DeltaRTUMaster(dv.SlaveId, sp);
                                                    DriverDictionary.Add(ch.ChannelName, (DeltaRTUMaster)DriverAdapter);
                                                    break;
                                                case "ASCII":
                                                    DriverAdapter = new DeltaASCIIMaster(dv.SlaveId, sp);
                                                    DriverDictionary.Add(ch.ChannelName, (DeltaASCIIMaster)DriverAdapter);
                                                    break;
                                            }
                                            break;
                                        case "Modbus":
                                            switch (dis.Mode)
                                            {
                                                case "RTU":
                                                    DriverAdapter = new ModbusRTUMaster(dv.SlaveId, sp);
                                                    DriverDictionary.Add(ch.ChannelName, (ModbusRTUMaster)DriverAdapter);
                                                    break;
                                                case "ASCII":
                                                    DriverAdapter = new ModbusASCIIMaster(dv.SlaveId, sp);
                                                    DriverDictionary.Add(ch.ChannelName, (ModbusASCIIMaster)DriverAdapter);
                                                    break;
                                            }
                                            break;
                                        case "LSIS":
                                            DriverAdapter = new LS_CNET(dv.SlaveId, sp);
                                            DriverDictionary.Add(ch.ChannelName, (LS_CNET)DriverAdapter);
                                            break;

                                        default:
                                            break;
                                    }
                                    break;
                                case "Ethernet":
                                    var die = (DIEthernet)ch;
                                    switch (ch.ChannelTypes)
                                    {
                                        case "Delta":
                                            DriverAdapter = new DeltaTCPMaster(dv.SlaveId, die.IPAddress, die.Port);
                                            DriverDictionary.Add(ch.ChannelName, (DeltaTCPMaster)DriverAdapter);
                                            break;
                                        case "Modbus":
                                            DriverAdapter = new ModbusTCPMaster(dv.SlaveId, die.IPAddress, die.Port);
                                            DriverDictionary.Add(ch.ChannelName, (ModbusTCPMaster)DriverAdapter);
                                            break;
                                        case "LSIS":
                                            
                                            DriverAdapter = new LS_FENET(die.CPU, die.IPAddress, die.Port, die.Slot);
                                            DriverDictionary.Add(ch.ChannelName, (LS_FENET)DriverAdapter);
                                            break;

                                        default:
                                            break;
                                    }
                                    break;

                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);

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

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                throw new PLCDriverException(ex.Message);
            }
        }


        private static Thread[] threads;
        public void Connect()
        {
            try
            {
                Console.WriteLine("STARTED: {0}", ++COUNTER);
                threads = new Thread[Channels.Count];
                int SendSuccess = 0;
                if (threads == null) throw new NullReferenceException("No Data");
                for (var i = 0; i < Channels.Count; i++)
                {
                    threads[i] = new Thread((chParam) =>
                    {
                        IDriverAdapter DriverAdapter = null;
                        var ch = (Channel)chParam;
                        switch (ch.ChannelTypes)
                        {
                            case "Delta":
                                switch (ch.Mode)
                                {
                                    case "RTU":
                                        DriverAdapter = DriverDictionary.GetValue<DeltaRTUMaster>(ch.ChannelName);
                                        break;
                                    case "ASCII":
                                        DriverAdapter = DriverDictionary.GetValue<DeltaASCIIMaster>(ch.ChannelName);
                                        break;
                                    case "TCP":
                                        DriverAdapter = DriverDictionary.GetValue<DeltaTCPMaster>(ch.ChannelName);
                                        break;
                                }
                                break;
                            case "Modbus":
                                switch (ch.Mode)
                                {
                                    case "RTU":
                                        DriverAdapter = DriverDictionary.GetValue<ModbusRTUMaster>(ch.ChannelName);
                                        break;
                                    case "ASCII":
                                        DriverAdapter = DriverDictionary.GetValue<ModbusASCIIMaster>(ch.ChannelName);
                                        break;
                                    case "TCP":

                                        DriverAdapter = DriverDictionary.GetValue<ModbusTCPMaster>(ch.ChannelName);
                                        break;
                                }
                                break;
                            case "LSIS":
                                switch (ch.ConnectionType)
                                {
                                    case "SerialPort":
                                        DriverAdapter = DriverDictionary.GetValue<LS_CNET>(ch.ChannelName);
                                        break;

                                    case "Ethernet":
                                        DriverAdapter = DriverDictionary.GetValue<LS_FENET>(ch.ChannelName);
                                        break;
                                }
                                break;


                            default:
                                break;
                        }

                        //======Connection to PLC==================================
                        IsConnected= DriverAdapter.Connection();

                        while (IsConnected)
                        {

                            try
                            {

                                if (RequestWriteToClient.Count > 0)
                                {
                                    if (DriverAdapter.IsConnected)
                                    {
                                        //Thread.Sleep(200);
                                        foreach (RequestWrite item1 in RequestWriteToClient)
                                        {
                                            SendSuccess = write(item1);
                                            break;
                                        }
                                        if (SendSuccess > 0)
                                            RequestWriteToClient.Dequeue();
                                    }
                                }
                                else
                                {
                                    try
                                    {



                                        foreach (Device dv in ch.Devices)
                                        {

                                            foreach (DataBlock db in dv.DataBlocks)
                                            {
                                                if (!IsConnected) break;
                                                switch (ch.ChannelTypes)
                                                {
                                                    case "Delta":
                                                        SendPackageDelta(DriverAdapter, db);
                                                        break;
                                                    case "Modbus":
                                                        SendPackageModbus(DriverAdapter, db);
                                                        break;
                                                    case "LSIS":
                                                        SendPackageLSIS(DriverAdapter, db);
                                                        break;

                                                    default:
                                                        break;
                                                }

                                            }

                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Disconnect();
                                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                                    }

                                }
                            }
                            catch (SocketException)
                            {
                                DriverAdapter = null;
                                objConnectionState = ConnectionState.DISCONNECT;
                                eventUOSAccepting?.Invoke(objConnectionState, string.Format("Server disconnect with S7-1200."));
                            }
                            catch (Exception ex)
                            {
                                eventUOSAccepting?.Invoke(objConnectionState, ex.Message);
                            }

                            if (IsConnected && objConnectionState == ConnectionState.DISCONNECT)
                            {
                                objConnectionState = ConnectionState.CONNECT;
                                eventUOSAccepting?.Invoke(objConnectionState, string.Format("S7-1200 connected to Server."));
                            }
                            else if (!IsConnected && objConnectionState == ConnectionState.CONNECT)
                            {
                                objConnectionState = ConnectionState.DISCONNECT;
                                eventUOSAccepting?.Invoke(objConnectionState, string.Format("Server disconnect with S7-1200."));
                            }

                        }
                    });
                    threads[i].IsBackground = true;
                    threads[i].Start(Channels[i]);
                }
                
            }
            catch (Exception ex)
            {
                if (myException != null) myException(string.Format("ERROR: {0}", ex.Message));
                Console.WriteLine(string.Format("ERROR: {0}", ex.Message));
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
           
        }



        public void Disconnect()
        {

            try
            {
               
                eventUOSAccepting = null;
                eventUOSAccepting = null;

                objConnectionState = ConnectionState.DISCONNECT;
            }
            catch (Exception ex)
            {
                if (myException != null) myException(string.Format("ERROR: {0}", ex.Message));
                Console.WriteLine(string.Format("ERROR: {0}", ex.Message));
            }
        }

        #endregion

        #region SendPackage All
        private void SendPackageDelta(IDriverAdapter DriverAdapter, DataBlock db)
        {
            try
            {
                SendDone.WaitOne(-1);
                switch (db.DataType)
                {
                    case DataTypes.Bit:
                        lock (DriverAdapter)
                        {

                            bool[] bitRs = DriverAdapter.Read<bool>($"{db.MemoryType}{db.StartAddress}", db.Length);
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
                            Byte[] IntRs = DriverAdapter.Read<Byte>($"{db.MemoryType}{db.StartAddress}", db.Length);
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
        private void SendPackageModbus(IDriverAdapter DriverAdapter, DataBlock db)
        {
            try
            {
                SendDone.WaitOne(-1);
                switch (db.DataType)
                {
                    case DataTypes.Bit:
                        lock (DriverAdapter)
                        {

                            bool[] bitRs = DriverAdapter.Read<bool>($"{db.StartAddress}", db.Length);
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
                        break;
                    case DataTypes.Short:
                        lock (DriverAdapter)
                        {
                            short[] IntRs = DriverAdapter.Read<Int16>($"{db.StartAddress}", db.Length);
                            if (IntRs == null) return;
                            if (IntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < IntRs.Length; j++)
                            {

                                db.Tags[j].Value = IntRs[j];


                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.UShort:
                        break;
                    case DataTypes.Int:
                        lock (DriverAdapter)
                        {
                            int[] DIntRs = DriverAdapter.Read<Int32>(string.Format("{0}", db.StartAddress), db.Length);
                            if (DIntRs == null) return;
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
                            var wdRs = DriverAdapter.Read<uint>($"{db.StartAddress}", db.Length);

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
                        lock (DriverAdapter)
                        {
                            long[] dwRs = DriverAdapter.Read<long>(string.Format("{0}", db.StartAddress), db.Length);
                            if (dwRs == null) return;
                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.ULong:
                        break;
                    case DataTypes.Float:
                        lock (DriverAdapter)
                        {
                            float[] rl1Rs = DriverAdapter.Read<float>(string.Format("{0}", db.StartAddress), db.Length);
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
                            double[] rl2Rs = DriverAdapter.Read<double>(string.Format("{0}", db.StartAddress), db.Length);
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
        private void SendPackageLSIS(IDriverAdapter ILSIS, DataBlock db)
        {
            try
            {
                SendDone.WaitOne(-1);
                switch (db.DataType)
                {
                    case DataTypes.Bit:
                        lock (ILSIS)
                        {
                            bool[] bitArys = null;
                            if (db.IsArray)
                            {
                                bitArys = ILSIS.Read<bool>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", (ushort)(2 * db.Length));

                            }
                            else
                            {
                                bitArys = ILSIS.Read<bool>($"{db.MemoryType}{db.StartAddress}", (ushort)(db.Length));

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
                        lock (ILSIS)
                        {
                            byte[] bitArys = ILSIS.Read<byte>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", (ushort)(2 * db.Length));
                            if (bitArys == null || bitArys.Length == 0) return;
                            if (bitArys.Length > db.Tags.Count)
                                return;
                            for (var j = 0; j <= db.Tags.Count - 1; j++)
                            {
                                db.Tags[j].Value = bitArys[j];

                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }

                        break;
                    case DataTypes.Short:
                        lock (ILSIS)
                        {
                            short[] IntRs = ILSIS.Read<short>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", db.Length);
                            if (IntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < IntRs.Length; j++)
                            {
                                db.Tags[j].Value = IntRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.UShort:
                        lock (ILSIS)
                        {
                            ushort[] bitArys = ILSIS.Read<ushort>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", db.Length);
                            if (bitArys == null || bitArys.Length == 0) return;
                            if (bitArys.Length > db.Tags.Count)
                                return;
                            for (var j = 0; j <= db.Tags.Count - 1; j++)
                            {
                                db.Tags[j].Value = bitArys[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }

                        break;
                    case DataTypes.Int:
                        lock (ILSIS)
                        {
                            int[] DIntRs = ILSIS.Read<Int32>($"{db.MemoryType.Substring(0, 1)}{4 * db.StartAddress}", db.Length);
                            if (DIntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < DIntRs.Length; j++)
                            {
                                db.Tags[j].Value = DIntRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.UInt:
                        lock (ILSIS)
                        {
                            var wdRs = ILSIS.Read<uint>($"{db.MemoryType.Substring(0, 1)}{4 * db.StartAddress}", db.Length);
                            if (wdRs == null) return;
                            for (int j = 0; j < db.Tags.Count; j++)
                            {
                                db.Tags[j].Value = wdRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Long:
                        lock (ILSIS)
                        {
                            long[] dwRs = ILSIS.Read<long>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", db.Length);

                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.ULong:
                        lock (ILSIS)
                        {
                            ulong[] dwRs = ILSIS.Read<ulong>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", db.Length);

                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Float:
                        lock (ILSIS)
                        {
                            float[] rl1Rs = ILSIS.Read<float>($"{db.MemoryType.Substring(0, 1)}{4 * db.StartAddress}", db.Length);

                            for (int j = 0; j < rl1Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl1Rs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Double:
                        lock (ILSIS)
                        {
                            double[] rl2Rs = ILSIS.Read<double>($"{db.MemoryType.Substring(0, 1)}{4 * db.StartAddress}", db.Length);

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
            catch (SocketException ex)
            {
                Disconnect();
                if (ex.Message == "Hex Character Count Not Even") return;
                IsConnected = false;
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            catch (Exception ex)
            {
                Disconnect();
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
        #region Write All
        public int write(RequestWrite data)
        {
            var tagName = data.tagName;
            var value = data.value;
            try
            {
                //SendDone.Reset();
                string[] ary = tagName.Split('.');
                string tagDevice = string.Format("{0}.{1}", ary[0], ary[1]);
                foreach (Channel ch in Channels)
                {
                    foreach (Device dv in ch.Devices)
                    {

                        if (string.Format("{0}.{1}", ch.ChannelName, dv.DeviceName).Equals(tagDevice))
                        {
                            IDriverAdapter DriverAdapter = null;

                            switch (ch.ChannelTypes)
                            {
                                case "Delta":
                                    switch (ch.Mode)
                                    {
                                        case "RTU":
                                            DriverAdapter = DriverDictionary.GetValue<DeltaRTUMaster>(ch.ChannelName);
                                            break;
                                        case "ASCII":
                                            DriverAdapter = DriverDictionary.GetValue<DeltaASCIIMaster>(ch.ChannelName);
                                            break;
                                        case "TCP":
                                            DriverAdapter = DriverDictionary.GetValue<ModbusTCPMaster>(ch.ChannelName);
                                            break;
                                    }
                                    break;
                                case "Modbus":
                                    switch (ch.Mode)
                                    {
                                        case "RTU":
                                            DriverAdapter = DriverDictionary.GetValue<ModbusRTUMaster>(ch.ChannelName);
                                            break;
                                        case "ASCII":
                                            DriverAdapter = DriverDictionary.GetValue<ModbusASCIIMaster>(ch.ChannelName);
                                            break;
                                        case "TCP":
                                            DriverAdapter = DriverDictionary.GetValue<ModbusTCPMaster>(ch.ChannelName);
                                            break;
                                    }
                                    break;
                                case "LSIS":
                                    switch (ch.ConnectionType)
                                    {
                                        case "SerialPort":
                                            DriverAdapter = DriverDictionary.GetValue<LS_CNET>(ch.ChannelName);
                                            break;

                                        case "Ethernet":
                                            DriverAdapter = DriverDictionary.GetValue<LS_FENET>(ch.ChannelName);
                                            break;
                                    }
                                    break;

                                default:
                                    break;
                            }

                            if (DriverAdapter == null) return 0;
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
            //finally
            //{
            //    SendDone.Set();
            //}
            return 1;
        }
        public void WriteTag(string tagName, dynamic value)
        {
            RequestWrite request = new RequestWrite()
            {
                tagName = tagName,
                value = value

            };
            RequestWriteToClient.Enqueue(request);
          
        }

        #endregion
    }
}
