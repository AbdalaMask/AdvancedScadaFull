using AdvancedScada.DriverBase.DataTypes;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IODriverV2.Comm;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.IODriverV2.XDelta.ASCII
{
    public class DVPASCIIMaster : DVPASCIIMessage, IDriverAdapterV2
    {
        private const int DELAY = 100; // delay 100 ms
        private EthernetAdapter EthernetAdaper;
        private SerialPortAdapter SerialAdaper;
        private short slaveId;

        public DVPASCIIMaster(short slaveId)
        {
            this.slaveId = slaveId;
        }

        public bool IsConnected { get; set; } = false;

        public bool IsAvailable
        {
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
            var stopwatch = Stopwatch.StartNew();

            try
            {
                IsConnected = SerialAdaper.Connect();


                stopwatch.Stop();
            }
            catch (TimeoutException ex)
            {
                stopwatch.Stop();

                EventscadaException?.Invoke(this.GetType().Name, string.Format("Could Not Connect to Server : {0}Time{1}", this.GetType().Name, ex.Message,
                    stopwatch.ElapsedTicks));

                IsConnected = false;
            }
        }

        public void Disconnection()
        {
            try
            {
                SerialAdaper.Close();

            }
            catch (TimeoutException ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, string.Format("Could Not Connect to Server : {0}", this.GetType().Name, ex.Message));

            }
        }
        /// <summary>
        /// Command Code：17, Report Slave ID
        /// </summary>
        /// <param name="slaveAddress">Địa chỉ slave</param>
        /// <returns>byte[]</returns>
        public byte[] ReportSlaveID(byte slaveAddress)
        {
            try
            {
                string frame = this.ReportSlaveIDMessage(slaveAddress);
                this.SerialAdaper.WriteLine(frame);
                Thread.Sleep(DELAY);
                string buffReceiver = this.SerialAdaper.ReadLine();
                string tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
                byte[] data = Comm.Conversion.HexToBytes(tempStrg);
                if (buffReceiver.Length == 10) this.ModbusExcetion(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte[] ReadCoilStatus(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var Address = DMT.DevToAddrW("DVP", startAddress, slaveAddress);
            var frame = ReadCoilStatusMessage(slaveAddress, Convert.ToUInt32(Address), nuMBErOfPoints);
            SerialAdaper.WriteLine(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.ReadLine();
            var tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
            var messageReceived = Comm.Conversion.HexToBytes(tempStrg);
            if (buffReceiver.Length == 10) ModbusExcetion(messageReceived);
            var data = new byte[messageReceived[2]];
            Array.Copy(messageReceived, 3, data, 0, data.Length);
            return Bit.ToByteArray(Bit.ToArray(data));
        }

        public byte[] ReadHoldingRegisters(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var Address = DMT.DevToAddrW("DVP", startAddress, slaveAddress);
            var frame = ReadHoldingRegistersMessage(slaveAddress, Convert.ToUInt32(Address), nuMBErOfPoints);
            SerialAdaper.WriteLine(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.ReadLine();
            if (string.IsNullOrEmpty(buffReceiver)) return new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            var tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
            var messageReceived = Comm.Conversion.HexToBytes(tempStrg);
            if (buffReceiver.Length == 10) ModbusExcetion(messageReceived);
            var data = new byte[messageReceived[2]];
            Array.Copy(messageReceived, 3, data, 0, data.Length);
            return data;
        }

        public byte[] ReadInputRegisters(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var Address = DMT.DevToAddrW("DVP", startAddress, slaveAddress);
            var frame = ReadInputRegistersMessage(slaveAddress, Convert.ToUInt32(Address), nuMBErOfPoints);
            SerialAdaper.WriteLine(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.ReadLine();
            var tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
            var messageReceived = Comm.Conversion.HexToBytes(tempStrg);
            if (buffReceiver.Length == 10) ModbusExcetion(messageReceived);
            var data = new byte[messageReceived[2]];
            Array.Copy(messageReceived, 3, data, 0, data.Length);
            return data;
        }

        public byte[] ReadInputStatus(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var Address = DMT.DevToAddrW("DVP", startAddress, slaveAddress);
            var frame = ReadInputStatusMessage(slaveAddress, Convert.ToUInt32(Address), nuMBErOfPoints);
            SerialAdaper.WriteLine(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.ReadLine();
            var tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
            var messageReceived = Comm.Conversion.HexToBytes(tempStrg);
            if (buffReceiver.Length == 10) ModbusExcetion(messageReceived);
            var data = new byte[messageReceived[2]];
            Array.Copy(messageReceived, 3, data, 0, data.Length);
            return Bit.ToByteArray(Bit.ToArray(data));
        }

        public void AllSerialPortAdapter(SerialPortAdapter iModbusSerialPortAdapter)
        {
            SerialAdaper = iModbusSerialPortAdapter;
        }

        public void AllEthernetAdapter(EthernetAdapter iModbusEthernetAdapter)
        {
            EthernetAdaper = iModbusEthernetAdapter;
        }

        public byte[] WriteMultipleCoils(byte slaveAddress, string startAddress, bool[] values)
        {
            var Address = DMT.DevToAddrW("DVP", startAddress, slaveAddress);
            var data1 = Bit.ToByteArray(values);
            var frame = WriteMultipleCoilsMessage(slaveAddress, Convert.ToUInt32(Address), data1);
            SerialAdaper.WriteLine(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.ReadLine();
            var tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
            var data = Comm.Conversion.HexToBytes(tempStrg);
            if (buffReceiver.Length == 10) ModbusExcetion(data);
            return data;
        }

        public byte[] WriteMultipleRegisters(byte slaveAddress, string startAddress, byte[] values)
        {
            var Address = DMT.DevToAddrW("DVP", startAddress, slaveAddress);
            var frame = WriteMultipleRegistersMessage(slaveAddress, Convert.ToUInt32(Address), values);
            SerialAdaper.WriteLine(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.ReadLine();
            var tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
            var data = Comm.Conversion.HexToBytes(tempStrg);
            if (buffReceiver.Length == 10) ModbusExcetion(data);
            return data;
        }

        public byte[] WriteSingleCoil(byte slaveAddress, string startAddress, bool value)
        {
            var Address = DMT.DevToAddrW("DVP", startAddress, slaveAddress);
            var frame = WriteSingleCoilMessage(slaveAddress, Convert.ToUInt32(Address), value);
            SerialAdaper.WriteLine(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.ReadLine();
            var tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
            var data = Comm.Conversion.HexToBytes(tempStrg);
            if (buffReceiver.Length == 10) ModbusExcetion(data);
            return data;
        }

        public byte[] WriteSingleRegister(byte slaveAddress, string startAddress, byte[] values)
        {
            var Address = DMT.DevToAddrW("DVP", startAddress, slaveAddress);
            var frame = WriteSingleRegisterMessage(slaveAddress, Convert.ToUInt32(Address), values);
            SerialAdaper.WriteLine(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.ReadLine();
            var tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
            var data = Comm.Conversion.HexToBytes(tempStrg);
            if (buffReceiver.Length == 10) ModbusExcetion(data);
            return data;
        }

        public ConnectionState GetConnectionState()
        {
            return ConnectionState.Broken;
        }

        public byte[] BuildReadByte(byte station, string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public byte[] BuildWriteByte(byte station, string address, byte[] value)
        {
            throw new NotImplementedException();
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                var b = Bit.ToArray(ReadCoilStatus((byte)slaveId, address, length));
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = Word.ToArray(ReadHoldingRegisters((byte)slaveId, address, length));

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = Int.ToArray(ReadHoldingRegisters((byte)slaveId, address, length));

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = DInt.ToArray(ReadHoldingRegisters((byte)slaveId, address, length));
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = DWord.ToArray(ReadHoldingRegisters((byte)slaveId, address, length));
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = DInt.ToArray(ReadHoldingRegisters((byte)slaveId, address, length));
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = Word.ToArray(ReadHoldingRegisters((byte)slaveId, address, length));
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = Real.ToArrayInverse(ReadHoldingRegisters((byte)slaveId, address, length));
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = Real.ToArray(ReadHoldingRegisters((byte)slaveId, address, length));
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = string.Empty;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }

        public TValue[] Read<TValue>(DataBlock db)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadDiscrete(string address, ushort length)
        {
            var b = Bit.ToArray(ReadInputStatus((byte)slaveId, address, length));
            return b;
        }

        public bool Write(string address, dynamic value)
        {
            if (value is bool)
            {
                WriteSingleCoil((byte)slaveId, address, value);
            }
            else
            {
                WriteSingleRegister((byte)slaveId, address, value);
            }

            return true;
        }
    }
}