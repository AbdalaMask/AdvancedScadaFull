using AdvancedScada.DriverBase.Devices;
using S7.Net;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.Siemens.Core.Profinet

{
    /// <summary>
    /// Creates an instance of S7Net.Core driver
    /// </summary>
    public  class Plc 
    {
      

        S7.Net.Plc plc = null;
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
        public bool IsConnected { get; set; }

        public Plc(CpuType cpu, string ip, Int16 rack, Int16 slot)
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

        /// <summary>
        /// 
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Tag { get; set; }


        // construction
        /// <summary>
        /// 
        /// </summary>
        public Plc()
        {
            IP = "localhost";
            CPU = CpuType.S7400;
            Rack = 0;
            Slot = 3;
        }

        #region Read
        public object ReadStrings(string variable)
        {
            var adr = new PLCAddressStrings(variable);
            return plc.Read(adr.DataType, adr.DbNumber, adr.StartByte, adr.VarType, 1, (byte)adr.BitNumber);
        }
        public object ReadStruct(DataBlock structType, int db, int startByteAdr = 0)
        {
            int numBytes = Common. Struct.GetStructSize(structType);
            // now read the package

            var resultBytes = plc.ReadBytes(DataType.DataBlock, db, startByteAdr, numBytes);
            // and decode it
            return Common.Struct.FromBytes(structType, resultBytes, this);
        }

        #endregion

        public Plc(CpuType cpu, string ip, Int16 rack, Int16 slot, string name, object tag)
        {
            IP = ip;
            CPU = cpu;
            Rack = rack;
            Slot = slot;
            DeviceName = name;
            Tag = tag;
        }

        #region Write
        public void WriteString(string variable, object value)
        {
            var adr = new PLCAddressStrings(variable);
            plc.Write(adr.DataType, adr.DbNumber, adr.StartByte, value, adr.BitNumber);
        }
        public void Write(string variable, object value)
        {
            plc.Write(variable, value);
        }
        #endregion

        #region Connection (Open, Close)

        public void Connection()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                plc = new S7.Net.Plc(CPU, IP, Rack, Slot);
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
            finally
            {
                 

            }
        }

        #endregion

       







    }
}
