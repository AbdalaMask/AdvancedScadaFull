﻿using AdvancedScada.Common;
using AdvancedScada.Utils;
using HslCommunication;
using HslCommunication.Profinet.LSIS;
using System;
using System.Net.Sockets;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.LSIS.Core.LSIS.FENET
{
    public class LS_FENET : IDriverAdapter
    {
        private XGBFastEnet fastEnet = null;
        private readonly int Port = 2004;
        private readonly string IP = "127.0.0.1";

        private readonly byte slotNo;
        private readonly string CpuType = "XGB";

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

        public LS_FENET(string CpuType, string ip, int port, byte slotNo)
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
                EventscadaException?.Invoke(GetType().Name, DemoUtils.IpAddressInputWrong);
                return false;
            }

            if (!int.TryParse($"{Port}", out int port))
            {
                EventscadaException?.Invoke(GetType().Name, DemoUtils.PortInputWrong);
                return false;
            }

            if (!byte.TryParse($"{slotNo}", out byte slot))
            {
                EventscadaException?.Invoke(GetType().Name, DemoUtils.SlotInputWrong);
                return false;
            }

            fastEnet = new XGBFastEnet();
            try
            {

                fastEnet.IpAddress = IP;
                fastEnet.Port = Port;
                fastEnet.SlotNo = slotNo;
                fastEnet.SetCpuType = CpuType;
                try
                {
                    OperateResult connect = fastEnet.ConnectServer();
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
            catch (SocketException ex)
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
                fastEnet.ConnectClose();

                IsConnected = false;
                return IsConnected;
            }
            catch (SocketException ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
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
                object b = ReadCoil(address, length);
                return (TValue[])b;
            }
            else if (typeof(TValue) == typeof(ushort))
            {

                OperateResult<ushort[]> b = fastEnet.ReadUInt16(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue[] Read {b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }

            }
            else if (typeof(TValue) == typeof(int))
            {
                OperateResult<int[]> b = fastEnet.ReadInt32(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue[] Read {b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }


            }
            else if (typeof(TValue) == typeof(uint))
            {
                OperateResult<uint[]> b = fastEnet.ReadUInt32(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue[] Read {b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }

            }
            else if (typeof(TValue) == typeof(long))
            {
                OperateResult<long[]> b = fastEnet.ReadInt64(address, length);

                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue[] Read {b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }
            }
            else if (typeof(TValue) == typeof(ulong))
            {
                OperateResult<ulong[]> b = fastEnet.ReadUInt64(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue[] Read {b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }
            }

            else if (typeof(TValue) == typeof(short))
            {
                OperateResult<short[]> b = fastEnet.ReadInt16(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue[] Read {b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }
            }
            else if (typeof(TValue) == typeof(double))
            {
                OperateResult<double[]> b = fastEnet.ReadDouble(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue[] Read {b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }
            }
            else if (typeof(TValue) == typeof(float))
            {
                OperateResult<float[]> b = fastEnet.ReadFloat(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue[] Read {b.Message}");
                }
                else
                {
                    return (TValue[])(object)b.Content;
                }

            }
            else if (typeof(TValue) == typeof(string))
            {
                OperateResult<string> b = fastEnet.ReadString(address, length);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue[] Read {b.Message}");
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

            OperateResult<byte[]> b = fastEnet.Read(address, length);
            if (!b.IsSuccess)
            {
                throw new InvalidOperationException($"TValue[] Read {b.Message}");
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

                OperateResult<bool[]> read = fastEnet.ReadBool(address, 1);
                if (!read.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue Read {address} {read.Message}");
                }
                else
                {
                    return (TValue)(object)read.Content[0];
                }

            }
            else if (typeof(TValue) == typeof(ushort))
            {

                OperateResult<ushort[]> b = fastEnet.ReadUInt16(address, 1);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue Read {b.Message}");
                }
                else
                {
                    return (TValue)(object)b.Content[0];
                }

            }
            else if (typeof(TValue) == typeof(int))
            {
                OperateResult<int[]> b = fastEnet.ReadInt32(address, 1);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue Read {b.Message}");
                }
                else
                {
                    return (TValue)(object)b.Content[0];
                }


            }
            else if (typeof(TValue) == typeof(uint))
            {
                OperateResult<uint[]> b = fastEnet.ReadUInt32(address, 1);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue Read {b.Message}");
                }
                else
                {
                    return (TValue)(object)b.Content[0];
                }

            }
            else if (typeof(TValue) == typeof(long))
            {
                OperateResult<long[]> b = fastEnet.ReadInt64(address, 1);

                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue Read {b.Message}");
                }
                else
                {
                    return (TValue)(object)b.Content[0];
                }
            }
            else if (typeof(TValue) == typeof(ulong))
            {
                OperateResult<ulong[]> b = fastEnet.ReadUInt64(address, 1);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue Read {b.Message}");
                }
                else
                {
                    return (TValue)(object)b.Content[0];
                }
            }

            else if (typeof(TValue) == typeof(short))
            {
                OperateResult<short[]> b = fastEnet.ReadInt16(address, 1);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue Read {b.Message}");
                }
                else
                {
                    return (TValue)(object)b.Content[0];
                }
            }
            else if (typeof(TValue) == typeof(double))
            {
                OperateResult<double[]> b = fastEnet.ReadDouble(address, 1);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue Read {b.Message}");
                }
                else
                {
                    return (TValue)(object)b.Content[0];
                }
            }
            else if (typeof(TValue) == typeof(float))
            {
                OperateResult<float[]> b = fastEnet.ReadFloat(address, 1);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue Read {b.Message}");
                }
                else
                {
                    return (TValue)(object)b.Content[0];
                }

            }
            else if (typeof(TValue) == typeof(string))
            {
                OperateResult<string> b = fastEnet.ReadString(address, 1);
                if (!b.IsSuccess)
                {
                    throw new InvalidOperationException($"TValue[] Read {b.Message}");
                }
                else
                {
                    return (TValue)(object)b.Content[0];
                }
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));

        }


    }
}