using AdvancedScada.DriverBase.DataTypes;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IODriverV2.Comm;
using System;
using System.Data;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriverV2.XModbus.TCP
{
    public class ModbusTCPMaster : ModbusTCPMessage, IDriverAdapterV2
    {
        private const int DELAY = 10;
        private EthernetAdapter EthernetAdaper;
        private SerialPortAdapter SerialAdaper;
        public bool _IsConnected = false;
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

        public ModbusTCPMaster()
        {
        }

        public ModbusTCPMaster(string ip, int port)
            : this()
        {
            EthernetAdaper = new EthernetAdapter(ip, port);
        }

        public ModbusTCPMaster(string ip, short port, int connectTimeout)
            : this()
        {
            EthernetAdaper = new EthernetAdapter(ip, port, connectTimeout);
        }

        public void Connection()
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                IsConnected = EthernetAdaper.Connect();

                stopwatch.Stop();
            }
            catch (SocketException ex)
            {
                stopwatch.Stop();

                EventscadaException?.Invoke(this.GetType().Name,
                   $"Could Not Connect to Server : {ex.SocketErrorCode}Time{stopwatch.ElapsedTicks}");


            }
        }

        public void Disconnection()
        {
            try
            {
                EthernetAdaper.Close();

            }
            catch (SocketException)
            {
            }
            finally
            {

            }
        }

        public byte[] ReadCoilStatus(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var stopwatch = Stopwatch.StartNew();

            var frame = ReadCoilStatusMessage(slaveAddress, startAddress, nuMBErOfPoints);


            stopwatch.Stop();

            EthernetAdaper.Write(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = EthernetAdaper.Read();
            if (FUNCTION_01 != buffReceiver[7])
            {
                var errorbytes = new byte[3];
                Array.Copy(buffReceiver, 6, errorbytes, 0, errorbytes.Length);
                ModbusExcetion(errorbytes);
            }

            int SizeByte = buffReceiver[8]; // Số lượng byte dữ liệu thu được.
            var data = new byte[SizeByte];
            Array.Copy(buffReceiver, 9, data, 0,
                data.Length); // Dữ liệu cần lấy bắt đầu từ byte có chỉ số 9 trong buffReceive.            
            return Bit.ToByteArray(Bit.ToArray(data));
        }

        public byte[] ReadInputStatus(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var frame = ReadInputStatusMessage(slaveAddress, startAddress, nuMBErOfPoints);

            EthernetAdaper.Write(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = EthernetAdaper.Read();
            if (FUNCTION_02 != buffReceiver[7])
            {
                var errorbytes = new byte[3];
                Array.Copy(buffReceiver, 6, errorbytes, 0, errorbytes.Length);
                ModbusExcetion(errorbytes);
            }

            int SizeByte = buffReceiver[8]; // Số lượng byte dữ liệu thu được.
            var data = new byte[SizeByte];
            Array.Copy(buffReceiver, 9, data, 0,
                data.Length); // Dữ liệu cần lấy bắt đầu từ byte có chỉ số 9 trong buffReceive.            
            return Bit.ToByteArray(Bit.ToArray(data));
        }

        public byte[] ReadHoldingRegisters(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var frame = ReadHoldingRegistersMessage(slaveAddress, startAddress, nuMBErOfPoints);

            EthernetAdaper.Write(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = EthernetAdaper.Read();
            if (FUNCTION_03 != buffReceiver[7])
            {
                var errorbytes = new byte[3];
                Array.Copy(buffReceiver, 6, errorbytes, 0, errorbytes.Length);
                ModbusExcetion(errorbytes);
            }

            int SizeByte = buffReceiver[8]; // Số lượng byte dữ liệu thu được.
            var data = new byte[SizeByte];
            Array.Copy(buffReceiver, 9, data, 0,
                data.Length); // Dữ liệu cần lấy bắt đầu từ byte có chỉ số 9 trong buffReceive.            
            return data;
        }

        public byte[] ReadInputRegisters(byte slaveAddress, string startAddress, ushort nuMBErOfPoints)
        {
            var frame = ReadInputRegistersMessage(slaveAddress, startAddress, nuMBErOfPoints);

            EthernetAdaper.Write(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = EthernetAdaper.Read();
            if (FUNCTION_04 != buffReceiver[7])
            {
                var errorbytes = new byte[3];
                Array.Copy(buffReceiver, 6, errorbytes, 0, errorbytes.Length);
                ModbusExcetion(errorbytes);
            }

            int SizeByte = buffReceiver[8]; // Số lượng byte dữ liệu thu được.
            var data = new byte[SizeByte];
            Array.Copy(buffReceiver, 9, data, 0,
                data.Length); // Dữ liệu cần lấy bắt đầu từ byte có chỉ số 9 trong buffReceive.            
            return data;
        }

        public byte[] WriteSingleCoil(byte slaveAddress, string startAddress, bool value)
        {
            var frame = WriteSingleCoilMessage(slaveAddress, startAddress, value);

            EthernetAdaper.Write(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = EthernetAdaper.Read();
            if (FUNCTION_05 != buffReceiver[7])
            {
                var errorbytes = new byte[3];
                Array.Copy(buffReceiver, 6, errorbytes, 0, errorbytes.Length);
                ModbusExcetion(errorbytes);
            }

            return buffReceiver;
        }

        public byte[] WriteMultipleCoils(byte slaveAddress, string startAddress, bool[] values)
        {
            var frame = WriteMultipleCoilsMessage(slaveAddress, startAddress, values);

            EthernetAdaper.Write(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = EthernetAdaper.Read();
            if (FUNCTION_15 != buffReceiver[7])
            {
                var errorbytes = new byte[3];
                Array.Copy(buffReceiver, 6, errorbytes, 0, errorbytes.Length);
                ModbusExcetion(errorbytes);
            }

            return buffReceiver;
        }

        public byte[] WriteSingleRegister(byte slaveAddress, string startAddress, byte[] values)
        {
            var frame = WriteSingleRegisterMessage(slaveAddress, startAddress, values);

            EthernetAdaper.Write(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = EthernetAdaper.Read();
            if (FUNCTION_06 != buffReceiver[7])
            {
                var errorbytes = new byte[3];
                Array.Copy(buffReceiver, 6, errorbytes, 0, errorbytes.Length);
                ModbusExcetion(errorbytes);
            }

            return buffReceiver;
        }

        public byte[] WriteMultipleRegisters(byte slaveAddress, string startAddress, byte[] values)
        {
            var frame = WriteMultipleRegistersMessage(slaveAddress, startAddress, values);

            EthernetAdaper.Write(frame);
            Thread.Sleep(DELAY);
            var buffReceiver = EthernetAdaper.Read();
            if (FUNCTION_16 != buffReceiver[7])
            {
                var errorbytes = new byte[3];
                Array.Copy(buffReceiver, 6, errorbytes, 0, errorbytes.Length);
                ModbusExcetion(errorbytes);
            }

            return buffReceiver;
        }

        public void AllSerialPortAdapter(SerialPortAdapter iModbusSerialPortAdapter)
        {
            SerialAdaper = iModbusSerialPortAdapter;
        }

        public void AllEthernetAdapter(EthernetAdapter iModbusEthernetAdapter)
        {
            EthernetAdaper = iModbusEthernetAdapter;
        }

        public ConnectionState GetConnectionState()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public TValue[] Read<TValue>(DataBlock db)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadDiscrete(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public bool Write(string address, dynamic value)
        {
            throw new NotImplementedException();
        }
    }
}