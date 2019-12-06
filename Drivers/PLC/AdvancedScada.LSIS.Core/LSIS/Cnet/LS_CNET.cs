using AdvancedScada.Common;
using HslCommunication;
using HslCommunication.Profinet.LSIS;
using System;
using System.IO.Ports;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.LSIS.Core.LSIS.Cnet
{
    public class LS_CNET : IDriverAdapter
    {
        private readonly SerialPort serialPort;
        private XGBCnet xGBCnet = null;
        public byte Station { get; set; }
        private readonly object LockObject = new object();
        public LS_CNET(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
        }

        #region IDriverAdapter
        public bool IsConnected { get; set; } = false;

        public bool Connection()
        {


            try
            {
                lock (LockObject)
                {
                    xGBCnet?.Close();
                    xGBCnet = new XGBCnet
                    {
                        Station = Station
                    };

                    try
                    {
                        xGBCnet.SerialPortInni(sp =>
                       {
                           sp.PortName = serialPort.PortName;
                           sp.BaudRate = serialPort.BaudRate;
                           sp.DataBits = serialPort.DataBits;
                           sp.StopBits = serialPort.StopBits;
                           sp.Parity = serialPort.Parity;
                       });
                        xGBCnet.Open();
                        if (xGBCnet.IsOpen())
                        {
                            EventscadaException?.Invoke(GetType().Name, StringResources.Language.ConnectedSuccess);
                            IsConnected = true;
                        }
                        else
                        {
                            EventscadaException?.Invoke(GetType().Name, StringResources.Language.ConnectedFailed);
                        }
                        IsConnected = true;
                        return IsConnected;
                    }
                    catch (Exception ex)
                    {
                        EventscadaException?.Invoke(GetType().Name, ex.Message);
                        return IsConnected;

                    }
                }
            }
            catch (TimeoutException ex)
            {
                IsConnected = false;
                EventscadaException?.Invoke(GetType().Name, ex.Message);
                return IsConnected;
            }
        }
        public bool Disconnection()
        {

            try
            {
                xGBCnet.Close();
                IsConnected = false;
                return IsConnected;
            }
            catch (TimeoutException ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
                return IsConnected;
            }
        }


        public bool Write(string address, dynamic value)
        {

            if (value is bool)
            {
                xGBCnet.WriteCoil(address, value);
            }
            else
            {
                xGBCnet.Write(address, value);
            }
            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                object b = ReadCoil(address, length);
                return (TValue[])b;
            }
            if (typeof(TValue) == typeof(ushort))
            {

                OperateResult<ushort[]> b = xGBCnet.ReadUInt16(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"{b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }

            }
            if (typeof(TValue) == typeof(int))
            {
                OperateResult<int[]> b = xGBCnet.ReadInt32(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"{b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }


            }
            if (typeof(TValue) == typeof(uint))
            {
                OperateResult<uint[]> b = xGBCnet.ReadUInt32(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"{b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }

            }
            if (typeof(TValue) == typeof(long))
            {
                OperateResult<long[]> b = xGBCnet.ReadInt64(address, length);

                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"{b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }
            }
            if (typeof(TValue) == typeof(ulong))
            {
                OperateResult<ulong[]> b = xGBCnet.ReadUInt64(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"{b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }
            }

            if (typeof(TValue) == typeof(short))
            {
                OperateResult<short[]> b = xGBCnet.ReadInt16(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"{b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }
            }
            if (typeof(TValue) == typeof(double))
            {
                OperateResult<double[]> b = xGBCnet.ReadDouble(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"{b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }
            }
            if (typeof(TValue) == typeof(float))
            {
                OperateResult<float[]> b = xGBCnet.ReadFloat(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"{b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }

            }
            if (typeof(TValue) == typeof(string))
            {
                OperateResult<string> b = xGBCnet.ReadString(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"{b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }
            }


            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }
        #endregion
        private object ReadCoil(string address, ushort length)
        {
            OperateResult<byte[]> b = xGBCnet.Read(address, length);
            if (!b.IsSuccess)
            {
                throw new InvalidOperationException($"{b.Message}");
            }
            else
            {
                return HslCommunication.BasicFramework.SoftBasic.ByteToBoolArray(b.Content);
            }
        }


        public TValue Read<TValue>(string address)
        {
            if (typeof(TValue) == typeof(bool))
            {
                OperateResult<bool[]> read = xGBCnet.ReadBool(address, 1);
                if (!read.IsSuccess)
                {
                    throw new InvalidOperationException($"{read.Message}");
                }
                else
                {
                    return (TValue)(object)read.Content[0];
                }

            }
            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));

        }


    }
}
