using AdvancedScada.DriverBase;
using HslCommunication;
using HslCommunication.Profinet.Siemens;
using System;
using System.Data;
using System.Diagnostics;
using System.IO.Ports;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriver.Siemens
{
    public  class SiemensComPPI : IDriverAdapter
    {
        private SiemensPPI siemensPPI = null;
        private const int DELAY = 100; // delay 100 ms
         private SerialPort serialPort;
       
        public bool IsConnected { get => _IsConnected; set => _IsConnected = value; }
        private byte station;
        public byte Station { get => station; set => station = value; }
        public SiemensComPPI(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
            siemensPPI = new SiemensPPI();
        }
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

                    return IsConnected;


                }
                catch
                {
                    return false;
                }
            }
        }

        private bool _IsConnected;

        public bool Connection()
        {
            var stopwatch = Stopwatch.StartNew();
            siemensPPI?.Close();
            siemensPPI = new SiemensPPI();
            try
            {

                siemensPPI.SerialPortInni(sp =>
                {
                    sp.PortName = serialPort.PortName;
                    sp.BaudRate = serialPort.BaudRate;
                    sp.DataBits = serialPort.DataBits;
                    sp.StopBits = serialPort.StopBits;
                    sp.Parity = serialPort.Parity;
                });
                siemensPPI.Open();
                siemensPPI.Station = station;
                IsConnected = true;
              
                stopwatch.Stop();
                return IsConnected;
            }
            catch (TimeoutException ex)
            {
                stopwatch.Stop();

               EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                return IsConnected;
            }
        }

        public bool Disconnection()
        {
            try
            {
                siemensPPI.Close();
                return IsConnected;
            }
            catch (TimeoutException ex)
            {

               EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return IsConnected;
        }

 
       
        
        public bool Write(string address, dynamic value)
        {
            if (value is bool)
            {
                siemensPPI.Write(address, value);
            }
            else
            {
                siemensPPI.Write(address, value);
            }

            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                var b = siemensPPI.ReadBool(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = siemensPPI.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = siemensPPI.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = siemensPPI.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = siemensPPI.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = siemensPPI.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = siemensPPI.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = siemensPPI.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = siemensPPI.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = siemensPPI.ReadString(address, length).Content;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }

        public OperateResult<bool[]> ReadDiscrete(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadSingle(string address, ushort length)
        {
            throw new NotImplementedException();
        }
    }
}
