using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.DriverBase.Devices;
using HslCommunication;
using HslCommunication.Profinet.LSIS;
using System;
using System.Data;
using System.IO.Ports;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriver.LSIS.Cnet
{
    public class LS_CNET : IDriverAdapter
    {
        private SerialPort serialPort;
        private XGBCnet xGBCnet = null;
        public event EventHandler<PlcComEventArgs> DataReceived;
        public event EventHandler<PlcComEventArgs> ComError;
        public event EventHandler ConnectionEstablished;
        private object LockObject = new object();
        public LS_CNET(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
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
        #region IReadWritePLC
        public bool IsConnected { get; set; } = false;
        public byte Station { get; set; }
        public void Connection()
        {


            try
            {
                lock (LockObject)
                {
                    xGBCnet?.Close();
                    xGBCnet = new XGBCnet();
                    xGBCnet.Station = Station;

                    try
                    {
                        xGBCnet.SerialPortInni(sp =>
                        {
                            sp.PortName = serialPort.PortName;
                            sp.BaudRate = serialPort.BaudRate;
                            sp.DataBits = serialPort.DataBits;
                            sp.StopBits = serialPort.StopBits;
                            sp.Parity = serialPort.Parity;
                        });
                        xGBCnet.Open();
                        IsConnected = true;
                    }
                    catch (Exception ex)
                    {
                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                        throw new PLCDriverException("Failed To Open ." + ex.Message);

                    }

                    OnConnectionEstablished(System.EventArgs.Empty);
                }
            }
            catch (TimeoutException ex)
            {
                IsConnected = false;
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);

            }
        }
        public void Disconnection()
        {

            try
            {
                xGBCnet.Close();

                IsConnected = false;
            }
            catch (TimeoutException ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        #endregion
        protected virtual void OnDataReceived(PlcComEventArgs e)
        {
            if (DataReceived != null)
                DataReceived(this, e);
        }

        protected virtual void OnComError(PlcComEventArgs e)
        {
            if (ComError != null)
                ComError(this, e);
        }

        protected virtual void OnConnectionEstablished(System.EventArgs e)
        {
            if (ConnectionEstablished != null)
                ConnectionEstablished(this, e);
        }

        public byte[] BuildReadByte(byte station, string address, ushort length)
        {
            var frame = DemoUtils.BulkReadRenderResult(xGBCnet, address, length);
            return frame;
        }

        public byte[] BuildWriteByte(byte station, string address, byte[] value)
        {
            try
            {
                DemoUtils.WriteResultRender(xGBCnet.Write(address, value), address);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return new byte[0];
        }

        public bool Write(string address, dynamic value)
        {

            if (value is bool)
            {
                xGBCnet.WriteCoil(address, value);
            }
            else
            {
                xGBCnet.Write(address, value);
            }
            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                var b = ReadCoil(address, length);
                return (TValue[])b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = xGBCnet.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = xGBCnet.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = xGBCnet.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = xGBCnet.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = xGBCnet.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = xGBCnet.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = xGBCnet.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = xGBCnet.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = xGBCnet.ReadString(address, length).Content;
                return (TValue[])(object)b;
            }
            else
            {
                OnComError(new PlcComEventArgs(-20, "No Response from PLC", (ushort)1, 1));
            }
            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }

        private object ReadCoil(string address, ushort length)
        {
            var bitArys = DemoUtils.BulkReadRenderResult(xGBCnet, address, length);
            return HslCommunication.BasicFramework.SoftBasic.ByteToBoolArray(bitArys);
        }

        public bool[] ReadDiscrete(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public ConnectionState GetConnectionState()
        {
            return ConnectionState.Broken;
        }

        public TValue[] Read<TValue>(DataBlock db)
        {
            throw new NotImplementedException();
        }
    }
}
