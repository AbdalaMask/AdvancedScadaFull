﻿using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Siemens.Core.Common;
using AdvancedScada.Utils;
using HslCommunication;
using HslCommunication.Profinet.Siemens;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Siemens.Core.Siemens
{
    public class SiemensNet : IPLCS7Adapter
    {
        private readonly SiemensS7Net siemensTcpNet = null;
        private readonly SiemensPLCS siemensPLCSelected = SiemensPLCS.S1200;
        private const int DELAY = 10;

        public bool _IsConnected = false;
        public byte station;
        private readonly SiemensPLCS cpu;
        private readonly string iPAddress;
        private readonly short rack;
        private readonly short slot;

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
            if (!System.Net.IPAddress.TryParse(iPAddress, out System.Net.IPAddress address))
            {
                EventscadaException?.Invoke(GetType().Name, DemoUtils.IpAddressInputWrong);
                return false;
            }



            Stopwatch stopwatch = Stopwatch.StartNew();

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
                }



                stopwatch.Stop();
                return IsConnected;
            }
            catch (SocketException ex)
            {
                stopwatch.Stop();
                EventscadaException?.Invoke(GetType().Name, ex.Message);

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
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
            return IsConnected;
        }


        private object ReadCoil(string address, ushort length)
        {

            OperateResult<byte[]> b = siemensTcpNet.Read(address, length);
            if (!b.IsSuccess)
            {
                throw new InvalidOperationException($"TValue[] Read {b.Message}");
            }
            else
            {
                return HslCommunication.BasicFramework.SoftBasic.ByteToBoolArray(b.Content);
            }

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

                object b = ReadCoil(address, length);
                return (TValue[])b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                ushort[] b = siemensTcpNet.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                int[] b = siemensTcpNet.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                uint[] b = siemensTcpNet.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                long[] b = siemensTcpNet.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                ulong[] b = siemensTcpNet.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                short[] b = siemensTcpNet.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                double[] b = siemensTcpNet.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                float[] b = siemensTcpNet.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                string b = siemensTcpNet.ReadString(address, length).Content;
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

        public void ReadStruct(DataBlock structType, ushort length)
        {
            //int numBytes = GetStructSize(structType);
            //// now read the package
            //OperateResult<byte[]> read = siemensTcpNet.Read($"DB{structType.StartAddress}.0", (ushort)numBytes);
            //if (read.IsSuccess)
            //    // and decode it
            //   FromBytes(structType, read.Content);
            System.Collections.Generic.List<Tag> infos = structType.Tags;
            lock (structType)
            {


                foreach (Tag info in infos)
                {
                    switch (info.DataType)
                    {
                        case DriverBase.DataTypes.BitOnByte:
                            break;
                        case DriverBase.DataTypes.BitOnWord:
                            break;
                        case DriverBase.DataTypes.Bit:

                            info.Value = siemensTcpNet.ReadBool(info.Address).Content;
                            info.TimeSpan = DateTime.Now;

                            break;
                        case DriverBase.DataTypes.Byte:
                            info.Value = siemensTcpNet.ReadByte(info.Address).Content;
                            info.TimeSpan = DateTime.Now;

                            break;
                        case DriverBase.DataTypes.Short:
                            info.Value = siemensTcpNet.ReadInt16(info.Address).Content;
                            info.TimeSpan = DateTime.Now;

                            break;
                        case DriverBase.DataTypes.UShort:
                            info.Value = siemensTcpNet.ReadUInt16(info.Address).Content;
                            info.TimeSpan = DateTime.Now;

                            break;
                        case DriverBase.DataTypes.Int:
                            info.Value = siemensTcpNet.ReadInt32(info.Address).Content;
                            info.TimeSpan = DateTime.Now;

                            break;
                        case DriverBase.DataTypes.UInt:
                            info.Value = siemensTcpNet.ReadUInt32(info.Address).Content;
                            info.TimeSpan = DateTime.Now;

                            break;
                        case DriverBase.DataTypes.Long:
                            info.Value = siemensTcpNet.ReadInt64(info.Address).Content;
                            info.TimeSpan = DateTime.Now;

                            break;
                        case DriverBase.DataTypes.ULong:

                            info.Value = siemensTcpNet.ReadUInt64(info.Address).Content;
                            info.TimeSpan = DateTime.Now;

                            break;
                        case DriverBase.DataTypes.Float:
                            info.Value = siemensTcpNet.ReadFloat(info.Address).Content;

                            info.TimeSpan = DateTime.Now;

                            break;
                        case DriverBase.DataTypes.Double:
                            info.Value = siemensTcpNet.ReadDouble(info.Address).Content;

                            info.TimeSpan = DateTime.Now;

                            break;
                        case DriverBase.DataTypes.String:
                            info.Value = siemensTcpNet.ReadString(info.Address).Content;
                            info.TimeSpan = DateTime.Now;
                            break;
                        default:
                            break;
                    }

                }
            }
            //throw new NotImplementedException();
        }
        public static int GetStructSize(DataBlock structType)
        {
            double numBytes = 0.0;

            System.Collections.Generic.List<Tag> infos = structType.Tags;
            foreach (Tag info in infos)
            {
                switch (info.DataType)
                {
                    case DriverBase.DataTypes.BitOnByte:
                        break;
                    case DriverBase.DataTypes.BitOnWord:
                        break;
                    case DriverBase.DataTypes.Bit:
                        numBytes += 0.125;
                        break;
                    case DriverBase.DataTypes.Byte:
                        numBytes = Math.Ceiling(numBytes);
                        numBytes++;
                        break;
                    case DriverBase.DataTypes.Short:
                    case DriverBase.DataTypes.UShort:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        numBytes += 2;
                        break;
                    case DriverBase.DataTypes.Int:
                    case DriverBase.DataTypes.UInt:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.Long:
                    case DriverBase.DataTypes.ULong:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.Float:
                    case DriverBase.DataTypes.Double:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.String:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        numBytes += 2;
                        break;
                    default:
                        break;
                }

            }
            return (int)numBytes;
        }

        /// <summary>
        /// Creates a struct of a specified type by an array of bytes.
        /// </summary>
        /// <param name="structType">The struct type</param>
        /// <param name="bytes">The array of bytes</param>
        /// <returns>The object depending on the struct type or null if fails(array-length != struct-length</returns>
        public object FromBytes(DataBlock structType, byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            if (bytes.Length != GetStructSize(structType))
            {
                return null;
            }

            // and decode it
            int bytePos = 0;
            int bitPos = 0;
            double numBytes = 0.0;

            System.Collections.Generic.List<Tag> infos = structType.Tags;

            foreach (Tag info in infos)
            {
                switch (info.DataType)
                {
                    case DriverBase.DataTypes.BitOnByte:
                        break;
                    case DriverBase.DataTypes.BitOnWord:
                        break;
                    case DriverBase.DataTypes.Bit:
                        // get the value
                        bytePos = (int)Math.Floor(numBytes);
                        bitPos = (int)((numBytes - bytePos) / 0.125);
                        if ((bytes[bytePos] & (int)Math.Pow(2, bitPos)) != 0)
                        {
                            info.Value = true;
                            info.TimeSpan = DateTime.Now;
                        }


                        else
                        {
                            info.Value = false;
                            info.TimeSpan = DateTime.Now;
                        }

                        numBytes += 0.125;

                        break;
                    case DriverBase.DataTypes.Byte:
                        numBytes = Math.Ceiling(numBytes);
                        info.Value((bytes[(int)numBytes]));
                        info.TimeSpan = DateTime.Now;
                        numBytes++;
                        break;
                    case DriverBase.DataTypes.Short:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        info.Value = siemensTcpNet.ByteTransform.TransInt16(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 2;
                        break;
                    case DriverBase.DataTypes.UShort:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        info.Value = siemensTcpNet.ByteTransform.TransUInt16(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 2;
                        break;
                    case DriverBase.DataTypes.Int:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        info.Value = siemensTcpNet.ByteTransform.TransInt32(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.UInt:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        info.Value = siemensTcpNet.ByteTransform.TransUInt32(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.Long:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        info.Value = siemensTcpNet.ByteTransform.TransInt64(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.ULong:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        {
                            numBytes++;
                        }

                        info.Value = siemensTcpNet.ByteTransform.TransUInt64(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.Float:
                        info.Value = siemensTcpNet.ByteTransform.TransSingle(bytes, (int)numBytes);

                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.Double:
                        info.Value = siemensTcpNet.ByteTransform.TransDouble(bytes, (int)numBytes);

                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.String:
                        info.Value = siemensTcpNet.ReadString(info.Address).Content;
                        info.TimeSpan = DateTime.Now;
                        break;
                    default:
                        break;
                }

            }
            return infos;
        }
    }
}
