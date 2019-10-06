using AdvancedScada.DriverBase;
using AdvancedScada.Utils;
using HslCommunication;
using HslCommunication.Profinet.LSIS;
using System;
using System.Net.Sockets;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.LSIS.Core.LSIS.FENET
{
    public class LS_FENET : IDriverAdapter
    {
        private XGBFastEnet fastEnet = null;
        private readonly int Port = 2004;
        private readonly string IP = "127.0.0.1";

        private object slotNo;
        private string CpuType = "XGB";

        #region construction

        public LS_FENET()
        {
            fastEnet = new XGBFastEnet();
        }
        public LS_FENET(string ip, int port)
            : this()
        {

            IP = ip;
            Port = port;
            fastEnet = new XGBFastEnet();
        }

        public LS_FENET(string CpuType, string ip, int port, object slotNo)
            : this(ip, port)
        {

            IP = ip;
            Port = port;
            this.slotNo = slotNo;
            this.CpuType = CpuType;
        }

        #endregion
        #region IDriverAdapter
        public bool IsConnected { get; set; } = false;

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

            if (!byte.TryParse($"{slotNo}", out byte slot))
            {
                EventscadaException?.Invoke(this.GetType().Name, DemoUtils.SlotInputWrong);
                return false;
            }

            fastEnet = new XGBFastEnet();
            try
            {

                fastEnet.IpAddress = IP;
                fastEnet.Port = Port;
                fastEnet.SlotNo = 3;
                fastEnet.XCpuType = CpuType;
                try
                {
                    OperateResult connect = fastEnet.ConnectServer();
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


                IsConnected = false;
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                return IsConnected;

            }
        }

        public bool Disconnection()
        {
            try
            {
                fastEnet.ConnectClose();

                IsConnected = false;
                return IsConnected;
            }
            catch (SocketException ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                return IsConnected;
            }

        }

        public bool Write(string address, dynamic value)
        {
            if (value is bool)
            {
                fastEnet.WriteCoil(address, value);
            }
            else
            {
                fastEnet.Write(address, value);
            }



            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                var b = ReadCoil(address, length);
                return (TValue[])b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
               
                var result = fastEnet.ReadUInt16(address, length).Content;
                
                return (TValue[])(object)result;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = fastEnet.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = fastEnet.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = fastEnet.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = fastEnet.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = fastEnet.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = fastEnet.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = fastEnet.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = fastEnet.ReadString(address, length).Content;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }
        #endregion
        private object ReadCoil(string address, ushort length)
        {
            var bitArys = fastEnet.Read(address, length);
            return HslCommunication.BasicFramework.SoftBasic.ByteToBoolArray(bitArys.Content);
        }

        public bool[] ReadSingle(string address, ushort length)
        {
            return fastEnet.ReadBool(address, length).Content;
        }


    }
}