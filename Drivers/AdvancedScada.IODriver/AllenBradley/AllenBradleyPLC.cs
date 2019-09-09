using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using HslCommunication;
using HslCommunication.Profinet.AllenBradley;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriver.AllenBradley
{
    public class AllenBradleyPLC : IDriverAdapter
    {
        private AllenBradleyNet allenBradleyNet = null;
        public bool _IsConnected = false;
        public byte station;

        private string iPAddress;
        private int Port = 44818;
        private int slot = 0;
        public AllenBradleyPLC(string IPAddress, int port = 44818, int Slot = 0)
        {
            iPAddress = IPAddress;
            Port = port;
            slot = Slot;
        }
        public bool IsConnected { get; set; } = false;
        public byte Station { get; set; }
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

        public byte[] BuildReadByte(byte station, string address, ushort length)
        {
            var frame = DemoUtils.BulkReadRenderResult(allenBradleyNet, address, length);


            return frame;
        }

        public byte[] BuildWriteByte(byte station, string address, byte[] value)
        {
            try
            {
                DemoUtils.WriteResultRender(allenBradleyNet.Write(address, value), address);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return new byte[0];
        }

        public void Connection()
        {
            allenBradleyNet = new AllenBradleyNet();
            allenBradleyNet.IpAddress = iPAddress;
            allenBradleyNet.Port = Port;
            allenBradleyNet.Slot = (byte)slot;
            OperateResult connect = allenBradleyNet.ConnectServer();

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

        }

        public void Disconnection()
        {
            try
            {
                allenBradleyNet.ConnectClose();

            }
            catch (SocketException ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        public ConnectionState GetConnectionState()
        {
            return ConnectionState.Broken;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                var b = allenBradleyNet.Read(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = allenBradleyNet.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = allenBradleyNet.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = allenBradleyNet.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = allenBradleyNet.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = allenBradleyNet.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = allenBradleyNet.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = allenBradleyNet.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = allenBradleyNet.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = allenBradleyNet.ReadString(address, length).Content;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }

        public bool[] ReadDiscrete(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public bool Write(string address, dynamic value)
        {

            allenBradleyNet.Write(address, value);


            return true;
        }

        public TValue[] Read<TValue>(DataBlock db)
        {
            List<string> CurrentAddress = new List<string>();

            foreach (var item in db.Tags)
            {
                CurrentAddress.Add(item.Address);
            }


            OperateResult<byte[]> b = allenBradleyNet.Read(CurrentAddress.ToArray());
            if (b.IsSuccess)
            {


                foreach (var item in db.Tags)
                {
                    switch (item.DataType)
                    {
                        case "Bit":
                            break;
                        case "Int":
                            item.Value = allenBradleyNet.ByteTransform.TransInt32(b.Content, 0);
                            break;
                        case "DInt":
                            break;
                        case "Word":
                            break;
                        case "DWord":
                            break;
                        case "Real1":
                            item.Value = allenBradleyNet.ByteTransform.TransSingle(b.Content, 4);
                            break;
                        case "Real2":
                            break;
                        default:
                            break;
                    }
                }


            }

            return (TValue[])(object)b;
        }
    }
}
