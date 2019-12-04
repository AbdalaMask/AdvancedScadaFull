using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Siemens.Core.Common;
using HslCommunication;
using HslCommunication.Profinet.Siemens;
using System;
using System.Diagnostics;
using System.IO.Ports;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Siemens.Core.Siemens
{
    public class SiemensComPPI : IPLCS7Adapter
    {
        private SiemensPPI siemensPPI = null;
        private const int DELAY = 100; // delay 100 ms
        private SerialPort serialPort;

        public bool IsConnected { get; set; }
        private byte station;
        public byte Station { get => station; set => station = value; }
        public SiemensComPPI(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
            siemensPPI = new SiemensPPI();
        }

        public bool Connection()
        {
            var stopwatch = Stopwatch.StartNew();
            siemensPPI?.Close();
            siemensPPI = new SiemensPPI();
            try
            {

                siemensPPI.SerialPortInni(sp =>
                {
                    sp.PortName = serialPort.PortName;
                    sp.BaudRate = serialPort.BaudRate;
                    sp.DataBits = serialPort.DataBits;
                    sp.StopBits = serialPort.StopBits;
                    sp.Parity = serialPort.Parity;
                });
                siemensPPI.Open();
                siemensPPI.Station = station;
                if (siemensPPI.IsOpen())
                {
                    EventscadaException?.Invoke(this.GetType().Name, StringResources.Language.ConnectedSuccess);
                    IsConnected = true;
                }
                else
                {
                    EventscadaException?.Invoke(this.GetType().Name, StringResources.Language.ConnectedFailed);
                }
                IsConnected = true;


                stopwatch.Stop();
                return IsConnected;
            }
            catch (TimeoutException ex)
            {
                stopwatch.Stop();

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                return IsConnected;
            }
        }

        public bool Disconnection()
        {
            try
            {
                siemensPPI.Close();
                return IsConnected;
            }
            catch (TimeoutException ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return IsConnected;
        }




        public bool Write(string address, dynamic value)
        {

            siemensPPI.Write(address, value);


            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                var b = siemensPPI.ReadBool(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = siemensPPI.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = siemensPPI.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = siemensPPI.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = siemensPPI.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = siemensPPI.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = siemensPPI.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = siemensPPI.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = siemensPPI.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = siemensPPI.ReadString(address, length).Content;
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
            //OperateResult<byte[]> read = siemensPPI.Read($"DB{structType.StartAddress}.0", (ushort)numBytes);
            //if (read.IsSuccess)
            //    // and decode it
            //    return FromBytes(structType, read.Content);
            //throw new NotImplementedException();
        }
        public static int GetStructSize(DataBlock structType)
        {
            double numBytes = 0.0;

            var infos = structType.Tags;
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
                            numBytes++;
                        numBytes += 2;
                        break;
                    case DriverBase.DataTypes.Int:
                    case DriverBase.DataTypes.UInt:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.Long:
                    case DriverBase.DataTypes.ULong:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.Float:
                    case DriverBase.DataTypes.Double:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.String:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
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
                return null;

            if (bytes.Length != GetStructSize(structType))
                return null;

            // and decode it
            int bytePos = 0;
            int bitPos = 0;
            double numBytes = 0.0;

            var infos = structType.Tags;

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
                        bitPos = (int)((numBytes - (double)bytePos) / 0.125);
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
                            numBytes++;
                        info.Value = siemensPPI.ByteTransform.TransInt16(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 2;
                        break;
                    case DriverBase.DataTypes.UShort:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        info.Value = siemensPPI.ByteTransform.TransUInt16(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 2;
                        break;
                    case DriverBase.DataTypes.Int:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        info.Value = siemensPPI.ByteTransform.TransInt32(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.UInt:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        info.Value = siemensPPI.ByteTransform.TransUInt32(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.Long:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        info.Value = siemensPPI.ByteTransform.TransInt64(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.ULong:
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        info.Value = siemensPPI.ByteTransform.TransUInt64(bytes, (int)numBytes);
                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.Float:
                        info.Value = siemensPPI.ByteTransform.TransSingle(bytes, (int)numBytes);

                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.Double:
                        info.Value = siemensPPI.ByteTransform.TransDouble(bytes, (int)numBytes);

                        info.TimeSpan = DateTime.Now;
                        numBytes += 4;
                        break;
                    case DriverBase.DataTypes.String:
                        info.Value = siemensPPI.ReadString(info.Address, 64).Content;
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
