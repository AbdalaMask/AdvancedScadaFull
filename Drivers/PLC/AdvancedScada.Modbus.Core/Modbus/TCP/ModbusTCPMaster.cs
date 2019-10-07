using AdvancedScada.DriverBase;
using AdvancedScada.Utils;
using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Net.Sockets;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.Modbus.Core.Modbus.TCP
{
    public class ModbusTCPMaster : IDriverAdapter
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
                EventscadaException?.Invoke(this.GetType().Name, DemoUtils.IpAddressInputWrong);
                return false;
            }

            if (!int.TryParse($"{Port}", out int port))
            {
                EventscadaException?.Invoke(this.GetType().Name, DemoUtils.PortInputWrong);
                return false;
            }

            

            try
            {

                busTcpClient?.ConnectClose();
                busTcpClient = new ModbusTcpNet(IP, Port, Station);
                busTcpClient.AddressStartWithZero = true;


                busTcpClient.IsStringReverse = false;

                try
                {
                    OperateResult connect = busTcpClient.ConnectServer();
                    if (connect.IsSuccess)
                    {
                        EventscadaException?.Invoke(this.GetType().Name, StringResources.Language.ConnectedSuccess);
                        IsConnected = true;
                    }
                    else
                    {
                        EventscadaException?.Invoke(this.GetType().Name, StringResources.Language.ConnectedFailed);
                    }
                    return IsConnected;
                }
                catch (Exception ex)
                {
                    EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                    return IsConnected;
                }




            }
            catch (SocketException ex)
            {


                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
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
            catch (SocketException ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
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
                var b = busTcpClient.ReadCoil(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = busTcpClient.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = busTcpClient.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = busTcpClient.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = busTcpClient.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = busTcpClient.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = busTcpClient.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = busTcpClient.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = busTcpClient.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = busTcpClient.ReadString(address, length).Content;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }

        public bool[] ReadSingle(string address, ushort length)
        {
            throw new NotImplementedException();
        }
    }
}