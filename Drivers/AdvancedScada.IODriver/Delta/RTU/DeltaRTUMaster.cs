using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Data;
using System.IO.Ports;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriver.Delta.RTU
{
    public class DeltaRTUMaster : IDriverAdapter
    {
        private SerialPort serialPort;

        public bool IsConnected { get; set; }
        public byte Station { get; set; }
        public DeltaRTUMaster(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
        }

        private ModbusRtu busRtuClient = null;
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
        public void Connection()
        {

            busRtuClient?.Close();
            busRtuClient = new ModbusRtu(Station);
            busRtuClient.AddressStartWithZero = true;

            busRtuClient.IsStringReverse = false;
            try
            {

                busRtuClient.SerialPortInni(sp =>
                {
                    sp.PortName = serialPort.PortName;
                    sp.BaudRate = serialPort.BaudRate;
                    sp.DataBits = serialPort.DataBits;
                    sp.StopBits = serialPort.StopBits;
                    sp.Parity = serialPort.Parity;
                });
                busRtuClient.Open();
                IsConnected = true;


            }
            catch (TimeoutException ex)
            {


                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        public void Disconnection()
        {
            try
            {
                busRtuClient.Close();

            }
            catch (TimeoutException ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }



        public byte[] BuildReadByte(byte station, string address, ushort length)
        {
            var Address = DMT.DevToAddrW("DVP", address, station);
            var frame = DemoUtils.BulkReadRenderResult(busRtuClient, $"{Address}", length);


            return frame;
        }

        public byte[] BuildWriteByte(byte station, string address, byte[] value)
        {
            try
            {
                var Address = DMT.DevToAddrW("DVP", address, station);
                DemoUtils.WriteResultRender(busRtuClient.Write($"{Address}", value), address);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return new byte[0];
        }



        public OperateResult<bool[]> ReadDiscrete(string address, ushort length)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            return busRtuClient.ReadDiscrete($"{Address}", length);
        }

        public bool Write(string address, dynamic value)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            if (value is bool)
            {
                busRtuClient.WriteCoil($"{Address}", value);
            }
            else
            {
                busRtuClient.Write($"{Address}", value);
            }

            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            if (typeof(TValue) == typeof(bool))
            {
                var b = busRtuClient.ReadCoil($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = busRtuClient.ReadUInt16($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = busRtuClient.ReadInt32($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = busRtuClient.ReadUInt32($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = busRtuClient.ReadInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = busRtuClient.ReadUInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = busRtuClient.ReadInt16($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = busRtuClient.ReadDouble($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = busRtuClient.ReadFloat($"{Address}", length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = busRtuClient.ReadString($"{Address}", length).Content;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
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