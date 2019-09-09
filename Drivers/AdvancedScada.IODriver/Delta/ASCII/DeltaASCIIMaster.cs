using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Data;
using System.IO.Ports;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriver.Delta.ASCII
{
    public class DeltaASCIIMaster : IDriverAdapter
    {
        private SerialPort serialPort;

        public bool IsConnected { get; set; }
        public byte Station { get; set; }
        public DeltaASCIIMaster(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
        }

        private ModbusAscii busAsciiClient = null;
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

            busAsciiClient?.Close();
            busAsciiClient = new ModbusAscii(Station);
            busAsciiClient.AddressStartWithZero = true;

            busAsciiClient.IsStringReverse = false;
            try
            {

                busAsciiClient.SerialPortInni(sp =>
                {
                    sp.PortName = serialPort.PortName;
                    sp.BaudRate = serialPort.BaudRate;
                    sp.DataBits = serialPort.DataBits;
                    sp.StopBits = serialPort.StopBits;
                    sp.Parity = serialPort.Parity;
                });
                busAsciiClient.Open();
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
                busAsciiClient.Close();

            }
            catch (TimeoutException ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }



        public byte[] BuildReadByte(byte station, string address, ushort length)
        {
            var Address = DMT.DevToAddrW("DVP", address, station);
            var frame = DemoUtils.BulkReadRenderResult(busAsciiClient, $"{Address}", length);


            return frame;
        }

        public byte[] BuildWriteByte(byte station, string address, byte[] value)
        {
            try
            {
                var Address = DMT.DevToAddrW("DVP", address, station);
                DemoUtils.WriteResultRender(busAsciiClient.Write($"{Address}", value), address);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return new byte[0];
        }



        public bool[] ReadDiscrete(string address, ushort length)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            return busAsciiClient.ReadDiscrete($"{Address}", length).Content;
        }

        public bool Write(string address, dynamic value)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            if (value is bool)
            {
                busAsciiClient.WriteCoil($"{Address}", value);
            }
            else
            {
                busAsciiClient.Write($"{Address}", value);
            }

            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            if (typeof(TValue) == typeof(bool))
            {
                var b = busAsciiClient.ReadCoil($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = busAsciiClient.ReadUInt16($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = busAsciiClient.ReadInt32($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = busAsciiClient.ReadUInt32($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = busAsciiClient.ReadInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = busAsciiClient.ReadUInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = busAsciiClient.ReadInt16($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = busAsciiClient.ReadDouble($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = busAsciiClient.ReadFloat($"{Address}", length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = busAsciiClient.ReadString($"{Address}", length).Content;
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