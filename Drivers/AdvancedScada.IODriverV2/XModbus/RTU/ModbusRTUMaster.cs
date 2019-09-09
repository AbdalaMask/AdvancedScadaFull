
using AdvancedScada.DriverBase.DataTypes;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IODriverV2.Comm;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.IODriverV2.XModbus.RTU
{
    public class ModbusRTUMaster : ModbusRTUMessage, IDriverAdapterV2
    {
        private const int DELAY = 100; // delay 100 ms


        private EthernetAdapter EthernetAdaper;
        private SerialPortAdapter SerialAdaper;

        public bool _IsConnected = false;
        private short slaveId;

        public ModbusRTUMaster(short slaveId)
        {
            this.slaveId = slaveId;
        }

        public bool IsConnected
        {
            get { return _IsConnected; }
            set { _IsConnected = value; }
        }

        public bool IsAvailable
        {
            get
            {
                throw new NotImplementedException();
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

                EventscadaException?.Invoke(this.GetType().Name,
                    $"Could Not Connect to Server : {ex.Message}Time{stopwatch.ElapsedTicks}");

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

                EventscadaException?.Invoke(this.GetType().Name, $"Could Not Connect to Server : {ex.Message}");

            }
        }

        public byte[] ReadCoilStatus(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var frame = ReadCoilStatusMessage(slaveAddress, startAddress, nuMBErOfPoints);
            SerialAdaper.Write(frame, 0, frame.Length);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.Read();
            if (buffReceiver.Length == 5) ModbusExcetion(buffReceiver);
            var data = new byte[buffReceiver.Length - 5];
            Array.Copy(buffReceiver, 3, data, 0, data.Length);
            return Bit.ToByteArray(Bit.ToArray(data));
        }

        public byte[] ReadHoldingRegisters(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var frame = ReadHoldingRegistersMessage(slaveAddress, startAddress, nuMBErOfPoints);
            SerialAdaper.Write(frame, 0, frame.Length);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.Read();
            if (buffReceiver.Length == 5) ModbusExcetion(buffReceiver);
            var data = new byte[buffReceiver.Length - 5];
            Array.Copy(buffReceiver, 3, data, 0, data.Length);
            return data;
        }

        public byte[] ReadInputRegisters(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var frame = ReadInputRegistersMessage(slaveAddress, startAddress, nuMBErOfPoints);
            SerialAdaper.Write(frame, 0, frame.Length);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.Read();
            if (buffReceiver.Length == 5) ModbusExcetion(buffReceiver);
            var data = new byte[buffReceiver.Length - 5];
            Array.Copy(buffReceiver, 3, data, 0, data.Length);
            return data;
        }

        public byte[] ReadInputStatus(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var frame = ReadInputStatusMessage(slaveAddress, startAddress, nuMBErOfPoints);
            SerialAdaper.Write(frame, 0, frame.Length);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.Read();
            if (buffReceiver.Length == 5) ModbusExcetion(buffReceiver);
            var data = new byte[buffReceiver.Length - 5];
            Array.Copy(buffReceiver, 3, data, 0, data.Length);
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
            var frame = WriteMultipleCoilsMessage(slaveAddress, startAddress, values);
            SerialAdaper.Write(frame, 0, frame.Length);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.Read();
            if (buffReceiver.Length == 5) ModbusExcetion(buffReceiver);
            return buffReceiver;
        }

        public byte[] WriteMultipleRegisters(byte slaveAddress, string startAddress, byte[] values)
        {
            var frame = WriteMultipleRegistersMessage(slaveAddress, startAddress, values);
            SerialAdaper.Write(frame, 0, frame.Length);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.Read();
            if (buffReceiver.Length == 5) ModbusExcetion(buffReceiver);
            return buffReceiver;
        }

        public byte[] WriteSingleCoil(byte slaveAddress, string startAddress, bool value)
        {
            var frame = WriteSingleCoilMessage(slaveAddress, startAddress, value);
            SerialAdaper.Write(frame, 0, frame.Length);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.Read();
            if (buffReceiver.Length == 5) ModbusExcetion(buffReceiver);
            return buffReceiver;
        }

        public byte[] WriteSingleRegister(byte slaveAddress, string startAddress, byte[] values)
        {
            var frame = WriteSingleRegisterMessage(slaveAddress, startAddress, values);
            SerialAdaper.Write(frame, 0, frame.Length);
            Thread.Sleep(DELAY);
            var buffReceiver = SerialAdaper.Read();
            if (buffReceiver.Length == 5) ModbusExcetion(buffReceiver);
            return buffReceiver;
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