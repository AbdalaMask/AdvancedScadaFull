using AdvancedScada.Delta.Common;
using HslCommunication.ModBus;
using System;
using System.IO.Ports;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.IODriver.Delta.RTU
{
    public class DeltaRTUMaster : IDeltaAdapter
    {
        private readonly SerialPort serialPort;

        public bool IsConnected { get; set; }
        public byte Station { get; set; }
        public DeltaRTUMaster(short slaveId, SerialPort serialPort)
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
            int Address = DMT.DevToAddrW("DVP", address, Station);
            return busRtuClient.ReadDiscrete($"{Address}", length).Content;
        }

        public bool Write(string address, dynamic value)
        {
            int Address = DMT.DevToAddrW("DVP", address, Station);
            if (value is bool)
            {
                busRtuClient.Write($"{Address}", value);
            }
            else
            {
                busRtuClient.Write($"{Address}", value);
            }

            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            int Address = DMT.DevToAddrW("DVP", address, Station);
            if (typeof(TValue) == typeof(bool))
            {
                bool[] b = busRtuClient.ReadCoil($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                ushort[] b = busRtuClient.ReadUInt16($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                int[] b = busRtuClient.ReadInt32($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                uint[] b = busRtuClient.ReadUInt32($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                long[] b = busRtuClient.ReadInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                ulong[] b = busRtuClient.ReadUInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                short[] b = busRtuClient.ReadInt16($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                double[] b = busRtuClient.ReadDouble($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                float[] b = busRtuClient.ReadFloat($"{Address}", length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                string b = busRtuClient.ReadString($"{Address}", length).Content;
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