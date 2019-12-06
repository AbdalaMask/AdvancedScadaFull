using AdvancedScada.Delta.Common;
using HslCommunication.ModBus;
using System;
using System.IO.Ports;
using static AdvancedScada.Common.XCollection;



namespace AdvancedScada.IODriver.Delta.ASCII
{
    public class DeltaASCIIMaster : IDeltaAdapter
    {
        private readonly SerialPort serialPort;

        public bool IsConnected { get; set; }
        public byte Station { get; set; }
        public DeltaASCIIMaster(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
        }

        private ModbusAscii busAsciiClient = null;

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
                return true;


            }
            catch (Exception ex)
            {


                EventscadaException?.Invoke(GetType().Name, ex.Message);
                return false;
            }
        }

        public bool Disconnection()
        {
            try
            {
                busAsciiClient.Close();
                return true;
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
                return false;
            }
        }






        public bool[] ReadDiscrete(string address, ushort length)
        {
            int Address = DMT.DevToAddrW("DVP", address, Station);
            return busAsciiClient.ReadDiscrete($"{Address}", length).Content;
        }

        public bool Write(string address, dynamic value)
        {
            int Address = DMT.DevToAddrW("DVP", address, Station);
            if (value is bool)
            {
                busAsciiClient.Write($"{Address}", value);
            }
            else
            {
                busAsciiClient.Write($"{Address}", value);
            }

            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            int Address = DMT.DevToAddrW("DVP", address, Station);
            if (typeof(TValue) == typeof(bool))
            {
                bool[] b = busAsciiClient.ReadCoil($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                ushort[] b = busAsciiClient.ReadUInt16($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                int[] b = busAsciiClient.ReadInt32($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                uint[] b = busAsciiClient.ReadUInt32($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                long[] b = busAsciiClient.ReadInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                ulong[] b = busAsciiClient.ReadUInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                short[] b = busAsciiClient.ReadInt16($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                double[] b = busAsciiClient.ReadDouble($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                float[] b = busAsciiClient.ReadFloat($"{Address}", length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                string b = busAsciiClient.ReadString($"{Address}", length).Content;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }

        public bool ReadSingle(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public TValue Read<TValue>(string address)
        {
            throw new NotImplementedException();
        }
    }
}