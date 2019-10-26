using AdvancedScada.Common;
using HslCommunication;
using HslCommunication.Profinet.Siemens;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Siemens.Core.Siemens
{
    public class SiemensNet : IDriverAdapter
    {
        private SiemensS7Net siemensTcpNet = null;
        private SiemensPLCS siemensPLCSelected = SiemensPLCS.S1200;
        private const int DELAY = 10;

        public bool _IsConnected = false;
        public byte station;
        private SiemensPLCS cpu;
        private string iPAddress;
        private short rack;
        private short slot;

        public bool IsConnected { get => _IsConnected; set => _IsConnected = value; }
        public byte Station { get => station; set => station = value; }

        public SiemensNet()
        {
        }

        public SiemensNet(SiemensPLCS cpu, string iPAddress, short rack, short slot)
            : this()
        {

            this.cpu = cpu;
            this.iPAddress = iPAddress;
            this.rack = rack;
            this.slot = slot;
            siemensPLCSelected = cpu;
            siemensTcpNet = new SiemensS7Net(cpu);

        }




        public bool Connection()
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {

                siemensTcpNet.IpAddress = iPAddress;
                if (siemensPLCSelected != SiemensPLCS.S200Smart)
                {
                    siemensTcpNet.Rack = (byte)rack;
                    siemensTcpNet.Slot = (byte)slot;
                }


                OperateResult connect = siemensTcpNet.ConnectServer();

                try
                {

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



                stopwatch.Stop();
                return IsConnected;
            }
            catch (SocketException ex)
            {
                stopwatch.Stop();
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);

            }
            return IsConnected;
        }

        public bool Disconnection()
        {
            try
            {
                siemensTcpNet.ConnectClose();
                return IsConnected;

            }
            catch (SocketException ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return IsConnected;
        }




        public bool Write(string address, dynamic value)
        {

            siemensTcpNet.Write(address, value);


            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                var b = siemensTcpNet.Read(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = siemensTcpNet.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = siemensTcpNet.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = siemensTcpNet.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = siemensTcpNet.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = siemensTcpNet.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = siemensTcpNet.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = siemensTcpNet.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = siemensTcpNet.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = siemensTcpNet.ReadString(address, length).Content;
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
