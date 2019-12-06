using AdvancedScada.Modbus.Common;
using HslCommunication.ModBus;
using System;
using System.IO.Ports;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Modbus.Core.Modbus.RTU
{
    public class ModbusRTUMaster : IModbusAdapter
    {
        private readonly SerialPort serialPort;

        public bool IsConnected { get; set; }
        public byte Station { get; set; }
        public ModbusRTUMaster(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
        }

        private ModbusRtu busRtuClient = null;

        public bool Connection()
        {

            busRtuClient?.Close();
            busRtuClient = new ModbusRtu(Station)
            {
                AddressStartWithZero = true,

                IsStringReverse = false
            };
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
                busRtuClient.Close();
                return IsConnected;
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
                return IsConnected;
            }
        }







        public bool[] ReadDiscrete(string address, ushort length)
        {
            return busRtuClient.ReadDiscrete(address, length).Content;
        }
        public bool Write(string address, dynamic value)
        {
            if (value is bool)
            {
                busRtuClient.Write(address, value);
            }
            else
            {
                busRtuClient.Write(address, value);
            }

            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                bool[] b = busRtuClient.ReadCoil(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                ushort[] b = busRtuClient.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                int[] b = busRtuClient.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                uint[] b = busRtuClient.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                long[] b = busRtuClient.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                ulong[] b = busRtuClient.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                short[] b = busRtuClient.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                double[] b = busRtuClient.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                float[] b = busRtuClient.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                string b = busRtuClient.ReadString(address, length).Content;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
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