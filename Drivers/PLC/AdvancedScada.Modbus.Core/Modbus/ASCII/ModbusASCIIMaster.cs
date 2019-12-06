using AdvancedScada.Modbus.Common;
using HslCommunication.ModBus;
using System;
using System.IO.Ports;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Modbus.Core.Modbus.ASCII
{
    public class ModbusASCIIMaster : IModbusAdapter
    {
        private readonly SerialPort serialPort;


        public byte Station { get; set; }
        public ModbusASCIIMaster(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
        }

        private ModbusAscii busAsciiClient = null;

        #region IDriverAdapter


        public bool IsConnected { get; set; }
        public bool Connection()
        {

            busAsciiClient?.Close();
            busAsciiClient = new ModbusAscii(Station)
            {
                AddressStartWithZero = true,

                IsStringReverse = false
            };
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
                return IsConnected;

            }
            catch (Exception ex)
            {


                EventscadaException?.Invoke(GetType().Name, ex.Message);
                return IsConnected;
            }
        }

        public bool Disconnection()
        {
            try
            {
                busAsciiClient.Close();
                return IsConnected;
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
                return IsConnected;
            }
        }

        public bool Write(string address, dynamic value)
        {

            busAsciiClient.Write(address, value);


            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                bool[] b = busAsciiClient.ReadCoil(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                ushort[] b = busAsciiClient.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                int[] b = busAsciiClient.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                uint[] b = busAsciiClient.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                long[] b = busAsciiClient.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                ulong[] b = busAsciiClient.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                short[] b = busAsciiClient.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                double[] b = busAsciiClient.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                float[] b = busAsciiClient.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                string b = busAsciiClient.ReadString(address, length).Content;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }

        #endregion


        public bool[] ReadDiscrete(string address, ushort length)
        {
            return busAsciiClient.ReadDiscrete(address, length).Content;
        }
        public TValue Read<TValue>(string address)
        {
            throw new NotImplementedException();
        }
        public bool ReadSingle(string address, ushort length)
        {
            throw new NotImplementedException();
        }
    }
}