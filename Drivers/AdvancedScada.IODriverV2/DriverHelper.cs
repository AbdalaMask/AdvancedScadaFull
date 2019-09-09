using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IODriverV2.Comm;
using AdvancedScada.IODriverV2.XDelta.ASCII;
using AdvancedScada.IODriverV2.XDelta.RTU;
using AdvancedScada.IODriverV2.XDelta.TCP;
using AdvancedScada.IODriverV2.XLSIS.Cnet;
using AdvancedScada.IODriverV2.XLSIS.FENET;
using AdvancedScada.IODriverV2.XModbus.ASCII;
using AdvancedScada.IODriverV2.XModbus.RTU;
using AdvancedScada.IODriverV2.XModbus.TCP;
using AdvancedScada.IODriverV2.XSiemens;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Net.Sockets;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriverV2
{
    public class DriverHelper
    {
        public static readonly ManualResetEvent SendDone = new ManualResetEvent(true);
        public static List<Channel> Channels;
        //==================================Delta===================================================
        private static Dictionary<string, DVPTCPMaster> Deltambe = null;
        private static Dictionary<string, DVPRTUMaster> Deltartu = null;
        private static Dictionary<string, DVPASCIIMaster> Deltaascii = null;
        //==================================Modbus===================================================
        private static Dictionary<string, ModbusTCPMaster> mbe = null;
        private static Dictionary<string, ModbusRTUMaster> rtu = null;
        private static Dictionary<string, ModbusASCIIMaster> ascii = null;
        //==================================LS===================================================
        private static Dictionary<string, LS_CNET> cnet = null;
        private static Dictionary<string, LS_FENET> FENET = null;
       
        //==================================Siemens===================================================
        private static Dictionary<string, PlcSiemens> _PLCS7 = null;
       

        private static int COUNTER;
        private static bool IsConnected;

        #region IServiceDriver


        public void InitializeService(List<Channel> chns)
        {

            try
            {
                //===============================================================
                Deltambe = new Dictionary<string, DVPTCPMaster>();
                Deltartu = new Dictionary<string, DVPRTUMaster>();
                Deltaascii = new Dictionary<string, DVPASCIIMaster>();
                //===============================================================
                mbe = new Dictionary<string, ModbusTCPMaster>();
                rtu = new Dictionary<string, ModbusRTUMaster>();
                ascii = new Dictionary<string, ModbusASCIIMaster>();
                //==================================================================
                cnet = new Dictionary<string, LS_CNET>();
                FENET = new Dictionary<string, LS_FENET>();
                //=================================================================
               
                _PLCS7 = new Dictionary<string, PlcSiemens>();
                
                Channels = chns;
                if (Channels == null) return;
                foreach (Channel ch in Channels)
                {
                    IDriverAdapterV2 DriverAdapter = null;

                    foreach (var dv in ch.Devices)
                    {
                        try
                        {
                            switch (ch.ConnectionType)
                            {
                                case "SerialPort":
                                    var dis = (DISerialPort)ch;
                                    var sp = new SerialPort(dis.PortName, dis.BaudRate, dis.Parity, dis.DataBits, dis.StopBits);
                                    sp.Handshake = dis.Handshake;
                                    var spAdaper = new SerialPortAdapter(sp);
                                    switch (ch.ChannelTypes)
                                    {
                                        case "Delta":
                                            switch (dis.Mode)
                                            {
                                                case "RTU":
                                                    dv.PLC = new DVPRTUMaster(dv.SlaveId);
                                                    DriverAdapter =  new DVPRTUMaster(dv.SlaveId);
                                                    DriverAdapter.AllSerialPortAdapter(spAdaper);
                                                    Deltartu.Add(ch.ChannelName, (DVPRTUMaster)DriverAdapter);
                                                    break;
                                                case "ASCII":
                                                    dv.PLC = new DVPASCIIMaster(dv.SlaveId);
                                                    DriverAdapter = new DVPASCIIMaster(dv.SlaveId);
                                                    DriverAdapter.AllSerialPortAdapter(spAdaper);
                                                    Deltaascii.Add(ch.ChannelName, (DVPASCIIMaster)DriverAdapter);
                                                    break;
                                            }
                                            break;
                                        case "Modbus":
                                            switch (dis.Mode)
                                            {
                                                case "RTU":
                                                    dv.PLC = new ModbusRTUMaster(dv.SlaveId);
                                                    DriverAdapter = new ModbusRTUMaster(dv.SlaveId);
                                                    DriverAdapter.AllSerialPortAdapter(spAdaper);
                                                    rtu.Add(ch.ChannelName, (ModbusRTUMaster)DriverAdapter);
                                                    break;
                                                case "ASCII":
                                                    dv.PLC = new ModbusASCIIMaster(dv.SlaveId);
                                                    DriverAdapter = new ModbusASCIIMaster(dv.SlaveId);
                                                    DriverAdapter.AllSerialPortAdapter(spAdaper);
                                                    ascii.Add(ch.ChannelName, (ModbusASCIIMaster)DriverAdapter);
                                                    break;
                                            }
                                            break;
                                        case "LSIS":
                                            dv.PLC = new LS_CNET(dv.SlaveId);
                                            DriverAdapter = new LS_CNET(dv.SlaveId);
                                            DriverAdapter.AllSerialPortAdapter(spAdaper);
                                            cnet.Add(ch.ChannelName, (LS_CNET)DriverAdapter);
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
                                            dv.PLC = new DVPTCPMaster( die.IPAddress, die.Port);
                                            DriverAdapter = new DVPTCPMaster( die.IPAddress, die.Port);
                                            Deltambe.Add(ch.ChannelName, (DVPTCPMaster)DriverAdapter);
                                            break;
                                        case "Modbus":
                                            dv.PLC = new ModbusTCPMaster( die.IPAddress, die.Port);
                                            DriverAdapter = new ModbusTCPMaster( die.IPAddress, die.Port);
                                            mbe.Add(ch.ChannelName, (ModbusTCPMaster)DriverAdapter);
                                            break;
                                        case "LSIS":
                                            dv.PLC = new LS_FENET(die.IPAddress, die.Port, die.Slot);
                                            DriverAdapter = new LS_FENET(die.IPAddress, die.Port, die.Slot);
                                            FENET.Add(ch.ChannelName, (LS_FENET)DriverAdapter);
                                            break;
                                        case "Panasonic":
                                            break;
                                        case "Siemens":
                                            var cpu = (CpuType)Enum.Parse(typeof(CpuType), die.CPU);
                                            DriverAdapter = new PlcSiemens(cpu, die.IPAddress, (short)die.Rack, (short)die.Slot);
                                            _PLCS7.Add(ch.ChannelName, (PlcSiemens)DriverAdapter);
                                            break;
                                        
                                        default:
                                            break;
                                    }
                                    break;

                            }
                            DeviceCollection.Devices.Add($"{ch.ChannelName}.{dv.DeviceName}", dv);
                            dv.DeviceState = ((dv.PLC.GetConnectionState() != ConnectionState.Open) ? DeviceState.Disconnected : DeviceState.Connected);


                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            dv.DeviceState = DeviceState.Disconnected;
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

        public static Device CurrentPLC = null;
        private static Thread[] threads;
        public void Connect()
        {

            try
            {
                foreach (Channel channel in Channels)
                {
                    using (List<Device>.Enumerator enumerator2 = channel.Devices.GetEnumerator())
                    {
                        if (enumerator2.MoveNext())
                        {
                            CurrentPLC = enumerator2.Current;
                        }
                    }
                }
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
                        switch (ch.ChannelTypes)
                        {
                            case "Delta":
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
                                break;
                            case "Modbus":
                                switch (ch.Mode)
                                {
                                    case "RTU":
                                        DriverAdapter = rtu[ch.ChannelName];
                                        break;
                                    case "ASCII":
                                        DriverAdapter = ascii[ch.ChannelName];
                                        break;
                                    case "TCP":
                                        DriverAdapter = mbe[ch.ChannelName];
                                        break;
                                }
                                break;
                            case "LSIS":
                                switch (ch.ConnectionType)
                                {
                                    case "SerialPort":
                                        DriverAdapter = cnet[ch.ChannelName];
                                        break;

                                    case "Ethernet":
                                        DriverAdapter = FENET[ch.ChannelName];
                                        break;
                                }
                                break;
 
                            case "Siemens":
                                switch (ch.ConnectionType)
                                {
                                    
                                    case "Ethernet":
                                        DriverAdapter = _PLCS7[ch.ChannelName];
                                        break;
                                }
                                break;

                            default:
                                break;
                        }



                        while (IsConnected)
                        {
                            foreach (Device dv in ch.Devices)
                            {
                                //if (DriverAdapter.IsAvailable)
                                //{
                                //    dv.Status = "Connection";

                                foreach (DataBlock db in dv.DataBlocks)
                                {
                                    if (!IsConnected) break;
                                    switch (ch.ChannelTypes)
                                    {
                                        case "Delta":
                                            SendPackageDelta(DriverAdapter, dv, db);
                                            break;
                                        case "Modbus":
                                            SendPackageModbus(DriverAdapter, dv, db);
                                            break;
                                        case "LSIS":
                                            SendPackageLSIS(DriverAdapter, ch, dv, db);
                                            break;
                                       
                                           
                                        case "Siemens":
                                            SendPackageSiemens(DriverAdapter, dv, db);
                                            break;
                                       
                                         
                                        default:
                                            break;
                                    }

                                }
                                //}
                                //else
                                //{

                                //    dv.Status = "Disconnection";
                                //}

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

            IsConnected = false;

        }

        #endregion

        #region SendPackage All

      
        private void SendPackageDelta(IDriverAdapter DriverAdapter, Device dv, DataBlock db)
        {
            try
            {

                if (DriverAdapter.IsAvailable)
                {
                    dv.Status = "Connection";
                    switch (db.DataType)
                    {
                        case "Bit":

                            lock (DriverAdapter)
                            {

                                bool[] bitRs = DriverAdapter.Read<bool>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (bitRs == null) return;
                                int length = bitRs.Length;
                                if (bitRs.Length > db.Tags.Count) length = db.Tags.Count;
                                for (int j = 0; j < length; j++)
                                {
                                    db.Tags[j].Value = bitRs[j];
                                    db.Tags[j].Checked = bitRs[j];
                                    db.Tags[j].Enabled = bitRs[j];
                                    db.Tags[j].Visible = bitRs[j];
                                    db.Tags[j].ValueSelect1 = bitRs[j];
                                    db.Tags[j].ValueSelect2 = bitRs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Int":

                            lock (DriverAdapter)
                            {
                                short[] IntRs = DriverAdapter.Read<Int16>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (IntRs == null) return;
                                if (IntRs.Length > db.Tags.Count) return;
                                for (int j = 0; j < IntRs.Length; j++)
                                {
                                    if (db.Tags[j].IsScaled)
                                    {
                                        db.Tags[j].Value = Util.Interpolation(IntRs[j], db.Tags[j].AImin, db.Tags[j].AImax, db.Tags[j].RLmin, db.Tags[j].RLmax);
                                    }
                                    else
                                    {
                                        db.Tags[j].Value = IntRs[j];
                                    }

                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "DInt":

                            lock (DriverAdapter)
                            {
                                int[] DIntRs = DriverAdapter.Read<Int32>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (DIntRs == null) return;
                                if (DIntRs.Length > db.Tags.Count) return;
                                for (int j = 0; j < DIntRs.Length; j++)
                                {
                                    db.Tags[j].Value = DIntRs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Word":

                            lock (DriverAdapter)
                            {
                                var wdRs = DriverAdapter.Read<UInt16>($"{db.MemoryType}{db.StartAddress}", db.Length);

                                if (wdRs == null) return;
                                if (wdRs.Length > db.Tags.Count) return;
                                for (int j = 0; j < wdRs.Length; j++)
                                {
                                    if (db.Tags[j].IsScaled)
                                    {
                                        db.Tags[j].Value = Util.Interpolation(wdRs[j], db.Tags[j].AImin, db.Tags[j].AImax, db.Tags[j].RLmin, db.Tags[j].RLmax);
                                    }
                                    else
                                    {
                                        db.Tags[j].Value = wdRs[j];
                                    }
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "DWord":

                            lock (DriverAdapter)
                            {
                                uint[] dwRs = DriverAdapter.Read<UInt32>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (dwRs == null) return;
                                for (int j = 0; j < dwRs.Length; j++)
                                {
                                    db.Tags[j].Value = dwRs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Real1":

                            lock (DriverAdapter)
                            {
                                float[] rl1Rs = DriverAdapter.Read<float>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (rl1Rs == null) return;
                                for (int j = 0; j < rl1Rs.Length; j++)
                                {
                                    db.Tags[j].Value = rl1Rs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Real2":

                            lock (DriverAdapter)
                            {
                                double[] rl2Rs = DriverAdapter.Read<double>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (rl2Rs == null) return;
                                for (int j = 0; j < rl2Rs.Length; j++)
                                {
                                    db.Tags[j].Value = rl2Rs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                    }
                }
                else
                {

                    dv.Status = "Disconnection";
                }

            }
            catch (SocketException ex)
            {
                Disconnect();
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            catch (Exception ex)
            {
                Disconnect();
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
   
        private void SendPackageModbus(IDriverAdapter DriverAdapter, Device dv, DataBlock db)
        {
            try
            {

                if (DriverAdapter.IsAvailable)
                {
                    dv.Status = "Connection";
                    switch (db.DataType)
                    {
                        case "Bit":

                            lock (DriverAdapter)
                            {

                                bool[] bitRs = DriverAdapter.Read<bool>($"{db.StartAddress}", db.Length);
                                if (bitRs == null) return;
                                int length = bitRs.Length;
                                if (bitRs.Length > db.Tags.Count) length = db.Tags.Count;
                                for (int j = 0; j < length; j++)
                                {
                                    db.Tags[j].Value = bitRs[j];
                                    db.Tags[j].Checked = bitRs[j];
                                    db.Tags[j].Enabled = bitRs[j];
                                    db.Tags[j].Visible = bitRs[j];
                                    db.Tags[j].ValueSelect1 = bitRs[j];
                                    db.Tags[j].ValueSelect2 = bitRs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Int":

                            lock (DriverAdapter)
                            {
                                short[] IntRs = DriverAdapter.Read<Int16>($"{db.StartAddress}", db.Length);
                                if (IntRs == null) return;
                                if (IntRs.Length > db.Tags.Count) return;
                                for (int j = 0; j < IntRs.Length; j++)
                                {
                                    if (db.Tags[j].IsScaled)
                                    {
                                        db.Tags[j].Value = Util.Interpolation(IntRs[j], db.Tags[j].AImin, db.Tags[j].AImax, db.Tags[j].RLmin, db.Tags[j].RLmax);
                                    }
                                    else
                                    {
                                        db.Tags[j].Value = IntRs[j];
                                    }

                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "DInt":

                            lock (DriverAdapter)
                            {
                                int[] DIntRs = DriverAdapter.Read<Int32>(string.Format("{0}", db.StartAddress), db.Length);
                                if (DIntRs == null) return;
                                if (DIntRs.Length > db.Tags.Count) return;
                                for (int j = 0; j < DIntRs.Length; j++)
                                {
                                    db.Tags[j].Value = DIntRs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Word":

                            lock (DriverAdapter)
                            {
                                var wdRs = DriverAdapter.Read<UInt16>($"{db.StartAddress}", db.Length);

                                if (wdRs == null) return;
                                if (wdRs.Length > db.Tags.Count) return;
                                for (int j = 0; j < wdRs.Length; j++)
                                {
                                    if (db.Tags[j].IsScaled)
                                    {
                                        db.Tags[j].Value = Util.Interpolation(wdRs[j], db.Tags[j].AImin, db.Tags[j].AImax, db.Tags[j].RLmin, db.Tags[j].RLmax);
                                    }
                                    else
                                    {
                                        db.Tags[j].Value = wdRs[j];
                                    }
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "DWord":

                            lock (DriverAdapter)
                            {
                                uint[] dwRs = DriverAdapter.Read<UInt32>(string.Format("{0}", db.StartAddress), db.Length);
                                if (dwRs == null) return;
                                for (int j = 0; j < dwRs.Length; j++)
                                {
                                    db.Tags[j].Value = dwRs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Real1":

                            lock (DriverAdapter)
                            {
                                float[] rl1Rs = DriverAdapter.Read<float>(string.Format("{0}", db.StartAddress), db.Length);
                                if (rl1Rs == null) return;
                                for (int j = 0; j < rl1Rs.Length; j++)
                                {
                                    db.Tags[j].Value = rl1Rs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Real2":

                            lock (DriverAdapter)
                            {
                                double[] rl2Rs = DriverAdapter.Read<double>(string.Format("{0}", db.StartAddress), db.Length);
                                if (rl2Rs == null) return;
                                for (int j = 0; j < rl2Rs.Length; j++)
                                {
                                    db.Tags[j].Value = rl2Rs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                    }
                }
                else
                {

                    dv.Status = "Disconnection";
                }

            }
            catch (SocketException ex)
            {
                Disconnect();
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            catch (Exception ex)
            {
                Disconnect();
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        private void SendPackageLSIS(IDriverAdapter ILSIS, Channel ch, Device dv, DataBlock db)
        {
            try
            {
                switch (db.DataType)
                {
                    case "Bit":

                        byte[] bitArys = null;
                        lock (ILSIS)
                            bitArys = ILSIS.BuildReadByte((byte)dv.SlaveId, $"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", (ushort)(2 * db.Length));
                        if (bitArys == null || bitArys.Length == 0) return;

                        var bitRs = DriverBase.DataTypes.Bit.ToArray(bitArys);
                        if (bitRs.Length > db.Tags.Count)
                            return;
                        for (var j = 0; j <= db.Tags.Count - 1; j++)
                        {
                            db.Tags[j].Value = bitRs[j];
                            db.Tags[j].Visible = bitRs[j];
                            db.Tags[j].Enabled = bitRs[j];
                            db.Tags[j].Timestamp = DateTime.Now;
                        }


                        break;
                    case "Int":

                        lock (ILSIS)
                        {
                            short[] IntRs = ILSIS.Read<Int16>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", (ushort)(2 * db.Length));
                            if (IntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < IntRs.Length; j++)
                            {
                                if (db.Tags[j].IsScaled)
                                {
                                    db.Tags[j].Value = Util.Interpolation(IntRs[j], db.Tags[j].AImin, db.Tags[j].AImax, db.Tags[j].RLmin, db.Tags[j].RLmax);
                                }
                                else
                                {
                                    db.Tags[j].Value = IntRs[j];
                                }

                                db.Tags[j].Timestamp = DateTime.Now;
                            }
                        }
                        break;
                    case "DInt":

                        lock (ILSIS)
                        {
                            int[] DIntRs = ILSIS.Read<Int32>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", (ushort)(2 * db.Length));
                            if (DIntRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < DIntRs.Length; j++)
                            {
                                db.Tags[j].Value = DIntRs[j];
                                db.Tags[j].Timestamp = DateTime.Now;
                            }
                        }
                        break;
                    case "Word":

                        lock (ILSIS)
                        {
                            var wdRs = ILSIS.Read<Int16>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", (ushort)(2 * db.Length));
                            if (wdRs == null) return;
                            //if (wdRs.Length > db.Tags.Count) return;
                            for (int j = 0; j < db.Tags.Count; j++)
                            {
                                if (db.Tags[j].IsScaled)
                                {
                                    db.Tags[j].Value = Util.Interpolation(wdRs[j], db.Tags[j].AImin, db.Tags[j].AImax, db.Tags[j].RLmin, db.Tags[j].RLmax);
                                }
                                else
                                {
                                    db.Tags[j].Value = wdRs[j];
                                }
                                db.Tags[j].Timestamp = DateTime.Now;
                            }
                        }
                        break;
                    case "DWord":

                        lock (ILSIS)
                        {
                            uint[] dwRs = ILSIS.Read<UInt32>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", (ushort)(2 * db.Length));

                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].Timestamp = DateTime.Now;
                            }
                        }
                        break;
                    case "Real1":

                        lock (ILSIS)
                        {
                            float[] rl1Rs = ILSIS.Read<float>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", (ushort)(2 * db.Length));

                            for (int j = 0; j < rl1Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl1Rs[j];
                                db.Tags[j].Timestamp = DateTime.Now;
                            }
                        }
                        break;
                    case "Real2":

                        lock (ILSIS)
                        {
                            double[] rl2Rs = ILSIS.Read<double>($"{db.MemoryType.Substring(0, 1)}{2 * db.StartAddress}", (ushort)(2 * db.Length));

                            for (int j = 0; j < rl2Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl2Rs[j];
                                db.Tags[j].Timestamp = DateTime.Now;
                            }
                        }
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
       
        private void SendPackageSiemens(IDriverAdapter ISiemens, Device dv, DataBlock db)
        {
            try
            {
                SendDone.WaitOne(-1);
                if (db.IsArray)
                {
                    ISiemens.Read<string>(db);

                }
                else
                {
                    switch (db.DataType)
                    {
                        case "Bit":

                            lock (ISiemens)
                            {

                                bool[] bitRs = ISiemens.Read<bool>($"{db.MemoryType}{db.StartAddress}", db.Length);

                                int length = bitRs.Length;
                                if (bitRs.Length > db.Tags.Count) length = db.Tags.Count;
                                for (int j = 0; j < length; j++)
                                {
                                    db.Tags[j].Value = bitRs[j];
                                    db.Tags[j].Checked = bitRs[j];
                                    db.Tags[j].Enabled = bitRs[j];
                                    db.Tags[j].Visible = bitRs[j];
                                    db.Tags[j].ValueSelect1 = bitRs[j];
                                    db.Tags[j].ValueSelect2 = bitRs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Int":

                            lock (ISiemens)
                            {
                                short[] IntRs = ISiemens.Read<Int16>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (IntRs.Length > db.Tags.Count) return;
                                for (int j = 0; j < IntRs.Length; j++)
                                {
                                    if (db.Tags[j].IsScaled)
                                    {
                                        db.Tags[j].Value = Util.Interpolation(IntRs[j], db.Tags[j].AImin, db.Tags[j].AImax, db.Tags[j].RLmin, db.Tags[j].RLmax);
                                    }
                                    else
                                    {
                                        db.Tags[j].Value = IntRs[j];
                                    }

                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "DInt":

                            lock (ISiemens)
                            {
                                int[] DIntRs = ISiemens.Read<Int32>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (DIntRs.Length > db.Tags.Count) return;
                                for (int j = 0; j < DIntRs.Length; j++)
                                {
                                    db.Tags[j].Value = DIntRs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Word":

                            lock (ISiemens)
                            {
                                var wdRs = ISiemens.Read<UInt16>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (wdRs == null) return;
                                if (wdRs.Length > db.Tags.Count) return;
                                for (int j = 0; j < wdRs.Length; j++)
                                {
                                    if (db.Tags[j].IsScaled)
                                    {
                                        db.Tags[j].Value = Util.Interpolation(wdRs[j], db.Tags[j].AImin, db.Tags[j].AImax, db.Tags[j].RLmin, db.Tags[j].RLmax);
                                    }
                                    else
                                    {
                                        db.Tags[j].Value = wdRs[j];
                                    }
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "DWord":

                            lock (ISiemens)
                            {
                                uint[] dwRs = ISiemens.Read<UInt32>($"{db.MemoryType}{db.StartAddress}", db.Length);

                                for (int j = 0; j < dwRs.Length; j++)
                                {
                                    db.Tags[j].Value = dwRs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Real1":

                            lock (ISiemens)
                            {
                                float[] rl1Rs = ISiemens.Read<float>($"{db.MemoryType}{db.StartAddress}", db.Length);

                                for (int j = 0; j < rl1Rs.Length; j++)
                                {
                                    db.Tags[j].Value = rl1Rs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                        case "Real2":

                            lock (ISiemens)
                            {
                                double[] rl2Rs = ISiemens.Read<double>($"{db.MemoryType}{db.StartAddress}", db.Length);

                                for (int j = 0; j < rl2Rs.Length; j++)
                                {
                                    db.Tags[j].Value = rl2Rs[j];
                                    db.Tags[j].Timestamp = DateTime.Now;
                                }
                            }
                            break;
                    }
                }
            }
            catch (SocketException ex)
            {
                Disconnect();
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
  #endregion
        #region SendPackage All

     
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

                            switch (ch.ChannelTypes)
                            {
                                case "Delta":
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
                                    break;
                                case "Modbus":
                                    switch (ch.Mode)
                                    {
                                        case "RTU":
                                            DriverAdapter = rtu[ch.ChannelName];
                                            break;
                                        case "ASCII":
                                            DriverAdapter = ascii[ch.ChannelName];
                                            break;
                                        case "TCP":
                                            DriverAdapter = mbe[ch.ChannelName];
                                            break;
                                    }
                                    break;
                                case "LSIS":
                                    switch (ch.ConnectionType)
                                    {
                                        case "SerialPort":
                                            DriverAdapter = cnet[ch.ChannelName];
                                            break;

                                        case "Ethernet":
                                            DriverAdapter = FENET[ch.ChannelName];
                                            break;
                                    }
                                    break;
                              
                                case "Siemens":
                                    switch (ch.ConnectionType)
                                    {
                                       
                                        case "Ethernet":
                                            DriverAdapter = _PLCS7[ch.ChannelName];
                                            break;
                                    }
                                    break;

                                default:
                                    break;
                            }

                            if (DriverAdapter == null) return;
                            lock (DriverAdapter)
                                switch (TagCollection.Tags[tagName].DataType)
                                {
                                    case "Bit":
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), value == "1" ? true : false);
                                        break;
                                    case "Int":
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), short.Parse(value));
                                        break;
                                    case "Word":
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), ushort.Parse(value));
                                        break;
                                    case "DInt":
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), short.Parse(value));
                                        break;
                                    case "DWord":
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), ushort.Parse(value));
                                        break;
                                    case "Real1":
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), float.Parse(value));
                                        break;
                                    case "Real2":
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), double.Parse(value));
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
