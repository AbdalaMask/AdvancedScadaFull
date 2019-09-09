using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IODriverV2.Comm;
using S7.Net;
using System;
using System.Data;
using System.Diagnostics;
using System.Net.Sockets;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriverV2.XSiemens

{
    /// <summary>
    /// Creates an instance of S7Net.Core driver
    /// </summary>
    public partial class PlcSiemens : IDriverAdapterV2
    {
        private EthernetAdapter EthernetAdaper;
        private SerialPortAdapter SerialAdaper;

        Plc plc = null;

        #region MyRegion
        /// <summary>
        /// IP address of the PLC
        /// </summary>
        public string IP { get; private set; }

        /// <summary>
        /// CPU type of the PLC
        /// </summary>
        public CpuType CPU { get; private set; }

        /// <summary>
        /// Rack of the PLC
        /// </summary>
        public Int16 Rack { get; private set; }

        /// <summary>
        /// Slot of the CPU of the PLC
        /// </summary>
        public Int16 Slot { get; private set; }

        /// <summary>
        /// Max PDU size this cpu supports
        /// </summary>
        public Int16 MaxPDUSize { get; set; }

        #endregion

        /// <summary>
        /// Returns true if a connection to the PLC can be established
        /// </summary>
        public bool IsAvailable
        {
            //TODO: Fix This
            get
            {
                try
                {
                    Connection();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsConnected
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }



        public PlcSiemens(CpuType cpu, string ip, Int16 rack, Int16 slot)
        {
            if (!Enum.IsDefined(typeof(CpuType), cpu))
                throw new ArgumentException($"The value of argument '{nameof(cpu)}' ({cpu}) is invalid for Enum type '{typeof(CpuType).Name}'.", nameof(cpu));

            if (string.IsNullOrEmpty(ip))
                throw new ArgumentException("IP address must valid.", nameof(ip));

            CPU = cpu;
            IP = ip;
            Rack = rack;
            Slot = slot;
            MaxPDUSize = 240;
        }

        // construction
        /// <summary>
        /// 
        /// </summary>
        public PlcSiemens()
        {
            IP = "localhost";
            CPU = CpuType.S7400;
            Rack = 0;
            Slot = 3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpu"></param>
        /// <param name="ip"></param>
        /// <param name="rack"></param>
        /// <param name="slot"></param>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        public PlcSiemens(CpuType cpu, string ip, Int16 rack, Int16 slot, string name, object tag)
        {
            IP = ip;
            CPU = cpu;
            Rack = rack;
            Slot = slot;

        }
        #region Connection (Open, Close)


        public void Connection()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                plc = new Plc(CPU, IP, Rack, Slot);
                plc.Open();
                stopwatch.Stop();
            }
            catch (SocketException ex)
            {
                plc.Close();
                stopwatch.Stop();

                EventscadaException?.Invoke(this.GetType().Name, string.Format("Could Not Connect to Server : {0} Time: {1}", ex.SocketErrorCode,
                    stopwatch.ElapsedTicks));


            }
        }

        public void Disconnection()
        {
            try
            {
                plc.Close();


            }
            catch (SocketException ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

        }

        #endregion
        public object ReadStrings(string variable)
        {
            var adr = new PLCAddressStrings(variable);
            return plc.Read(adr.DataType, adr.DbNumber, adr.StartByte, adr.VarType, 1, (byte)adr.BitNumber);
        }
        public object ReadStruct(DataBlock structType, int db, int startByteAdr = 0)
        {
            int numBytes = Struct.GetStructSize(structType);
            // now read the package

            var resultBytes = plc.ReadBytes(DataType.DataBlock, db, startByteAdr, numBytes);
            // and decode it
            return Struct.FromBytes(structType, resultBytes, this);
        }

        public void AllSerialPortAdapter(SerialPortAdapter iS7SerialPortAdapter)
        {
            SerialAdaper = iS7SerialPortAdapter;
        }

        public void AllEthernetAdapter(EthernetAdapter iS7EthernetAdapter)
        {
            EthernetAdaper = iS7EthernetAdapter;
        }

        public ConnectionState GetConnectionState()
        {
            throw new NotImplementedException();
        }

        public byte[] BuildReadByte(byte station, string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public byte[] BuildWriteByte(byte station, string address, byte[] value)
        {
            throw new NotImplementedException();
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public TValue[] Read<TValue>(DataBlock db)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadDiscrete(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public bool Write(string address, dynamic value)
        {
            throw new NotImplementedException();
        }
    }
}
