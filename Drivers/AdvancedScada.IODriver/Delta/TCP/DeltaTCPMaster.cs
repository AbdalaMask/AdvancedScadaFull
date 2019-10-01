using AdvancedScada.DriverBase;
using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Net.Sockets;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriver.Delta.TCP
{
    public class DeltaTCPMaster : IDriverAdapter
    {
        public bool IsConnected { get; set; } = false;
        public byte Station { get; set; }
        private ModbusTcpNet busTcpClient = null;
        private readonly int Port = 502;
        private readonly string IP = "127.0.0.1";
        public DeltaTCPMaster()
        {
        }

        public DeltaTCPMaster(short slaveId, string ip, int port)
            : this()
        {
            Station = (byte)slaveId;
            IP = ip;
            Port = port;

        }


        public bool Connection()
        {


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

                        IsConnected = true;
                    }
                    else
                    {
                        IsConnected = false;
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
            var Address = DMT.DevToAddrW("DVP", address, Station);
            return busTcpClient.ReadDiscrete($"{Address}", length).Content;
        }

        public bool Write(string address, dynamic value)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            if (value is bool)
            {
                busTcpClient.Write($"{Address}", value);
            }
            else
            {
                busTcpClient.Write($"{Address}", value);
            }

            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            if (typeof(TValue) == typeof(bool))
            {
                var b = busTcpClient.ReadCoil($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = busTcpClient.ReadUInt16($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = busTcpClient.ReadInt32($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = busTcpClient.ReadUInt32($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = busTcpClient.ReadInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = busTcpClient.ReadUInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = busTcpClient.ReadInt16($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = busTcpClient.ReadDouble($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = busTcpClient.ReadFloat($"{Address}", length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = busTcpClient.ReadString($"{Address}", length).Content;
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