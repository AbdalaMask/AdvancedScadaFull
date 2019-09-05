using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Data;
using System.Net.Sockets;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriver.Modbus.TCP
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
                }
                catch (Exception ex)
                {
                    EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                }




            }
            catch (SocketException ex)
            {


                EventscadaException?.Invoke(this.GetType().Name, ex.Message);

            }
        }

        public void Disconnection()
        {
            try
            {
                busTcpClient.ConnectClose();

            }
            catch (SocketException ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

        }
        public byte[] BuildReadByte(byte station, string address, ushort length)
        {


            var frame = DemoUtils.BulkReadRenderResult(busTcpClient, address, length);


            return frame;


        }

        public byte[] BuildWriteByte(byte station, string address, byte[] value)
        {
            try
            {
                DemoUtils.WriteResultRender(busTcpClient.Write(address, value), address);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return new byte[0];
        }




        public OperateResult<bool[]> ReadDiscrete(string address, ushort length)
        {
            return busTcpClient.ReadDiscrete(address, length);
        }


        public bool Write(string address, dynamic value)
        {
            if (value is bool)
            {
                busTcpClient.WriteCoil(address, value);
            }
            else
            {
                busTcpClient.Write(address, value);
            }

            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (!IsAvailable) return null;

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

        public virtual ConnectionState GetConnectionState()
        {
            return ConnectionState.Broken;
        }

        public TValue[] Read<TValue>(string[] address)
        {
            throw new NotImplementedException();
        }

        public TValue[] Read<TValue>(DataBlock db)
        {
            throw new NotImplementedException();
        }
    }
}