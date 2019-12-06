using AdvancedScada.Modbus.Common;
using AdvancedScada.Utils;
using HslCommunication;
using HslCommunication.ModBus;
using System;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Modbus.Core.Modbus.TCP
{
    public class ModbusTCPMaster : IModbusAdapter
    {
        public bool IsConnected { get; set; } = false;
        public byte Station { get; set; }
        private ModbusTcpNet busTcpClient = null;
        private readonly int Port = 502;
        private readonly string IP = "127.0.0.1";
        public ModbusTCPMaster()
        {
        }

        public ModbusTCPMaster(short slaveId, string ip, int port)
            : this()
        {
            Station = (byte)slaveId;
            IP = ip;
            Port = port;

        }


        public bool Connection()
        {
            if (!System.Net.IPAddress.TryParse(IP, out System.Net.IPAddress address))
            {
                EventscadaException?.Invoke(GetType().Name, DemoUtils.IpAddressInputWrong);
                return false;
            }

            if (!int.TryParse($"{Port}", out int port))
            {
                EventscadaException?.Invoke(GetType().Name, DemoUtils.PortInputWrong);
                return false;
            }



            try
            {

                busTcpClient?.ConnectClose();
                busTcpClient = new ModbusTcpNet(IP, Port, Station)
                {
                    AddressStartWithZero = true,


                    IsStringReverse = false
                };

                try
                {
                    OperateResult connect = busTcpClient.ConnectServer();
                    if (connect.IsSuccess)
                    {
                        EventscadaException?.Invoke(GetType().Name, StringResources.Language.ConnectedSuccess);
                        IsConnected = true;
                    }
                    else
                    {
                        EventscadaException?.Invoke(GetType().Name, StringResources.Language.ConnectedFailed);
                    }
                    return IsConnected;
                }
                catch (Exception ex)
                {
                    EventscadaException?.Invoke(GetType().Name, ex.Message);
                    return IsConnected;
                }




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
                busTcpClient.ConnectClose();
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
            return busTcpClient.ReadDiscrete(address, length).Content;
        }


        public bool Write(string address, dynamic value)
        {

            busTcpClient.Write(address, value);
            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {


            if (typeof(TValue) == typeof(bool))
            {
                bool[] b = busTcpClient.ReadCoil(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                ushort[] b = busTcpClient.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                int[] b = busTcpClient.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                uint[] b = busTcpClient.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                long[] b = busTcpClient.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                ulong[] b = busTcpClient.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                short[] b = busTcpClient.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                double[] b = busTcpClient.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                float[] b = busTcpClient.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                string b = busTcpClient.ReadString(address, length).Content;
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