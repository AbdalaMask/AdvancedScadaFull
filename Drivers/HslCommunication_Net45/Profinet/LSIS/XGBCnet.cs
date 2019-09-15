using HslCommunication.Core;
using HslCommunication.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HslCommunication.BasicFramework;

namespace HslCommunication.Profinet.LSIS
{
    /// <summary>
    /// XGB Cnet I/F module supports Serial Port.
    /// </summary>
    public class XGBCnet : SerialDeviceBase<RegularByteTransform>
    {
        #region Constructor

        /// <summary>
        /// Instantiate a Default object
        /// </summary>
        public XGBCnet()
        {
            WordLength = 2;
            ByteTransform = new RegularByteTransform();
        }

        #endregion

        #region Public Member

        /// <summary>
        /// PLC Station No.
        /// </summary>
        public byte Station { get; set; } = 0x05;

        #endregion

        #region Read Write Byte
        /// <summary>
        /// ReadBool
        /// </summary>
        /// <param name="address">address, for example: MX100, PX100</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<byte[]> command = XGBCnetOverTcp.BuildReadOneCommand(Station, address, length);

            if (!command.IsSuccess) return OperateResult.CreateFailedResult<bool[]>(command);
            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return OperateResult.CreateFailedResult<bool[]>(read);

            return OperateResult.CreateSuccessResult(SoftBasic.ByteToBoolArray(XGBCnetOverTcp.ExtractActualData(read.Content, true).Content, length));
        }
        /// <summary>
        /// ReadCoil
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public OperateResult<bool> ReadCoil(string address)
        {
            return ReadBool(address);
        }

        /// <summary>
        /// ReadCoil
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public OperateResult<bool[]> ReadCoil(string address, ushort length)
        {
            return ReadBool(address, length);
        }
        /// <summary>
        /// Read single byte value from plc
        /// </summary>
        /// <param name="address">Start address</param>
        /// <returns>result</returns>
        public OperateResult<byte> ReadByte(string address)
        {
            var read = Read(address, 2);
            if (!read.IsSuccess) return OperateResult.CreateFailedResult<byte>(read);

            return OperateResult.CreateSuccessResult(read.Content[0]);
        }

        /// <summary>
        /// Write single byte value to plc
        /// </summary>
        /// <param name="address">Start address</param>
        /// <param name="value">value</param>
        /// <returns>Whether to write the successful</returns>
        public OperateResult Write(string address, byte value)
        {
            return Write(address, new byte[] { value });
        }

        /// <summary>
        /// WriteCoil
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public OperateResult WriteCoil(string address, bool value)
        {

            return Write(address, new byte[] { (byte)(value ? 0x01 : 0x00) });
        }

        #endregion

        #region Read Write Support

        /// <summary>
        /// Read Bytes From PLC, you should specify the length
        /// </summary>
        /// <param name="address">the address of the data</param>
        /// <param name="length">the length of the data, in byte unit</param>
        /// <returns>result contains whether success.</returns>
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> command = null;
             var DataTypeResult = XGBFastEnet.GetDataTypeToAddress(address);
            if (!DataTypeResult.IsSuccess) return OperateResult.CreateFailedResult<byte[]>(DataTypeResult);

            switch (DataTypeResult.Content)
            {
                case "Bit":
                    command = XGBCnetOverTcp.BuildReadOneCommand(Station, address, length);
                    break;
                case "Continuous":
                    command = XGBCnetOverTcp.BuildReadByteCommand(Station, address, length);
                    break;
                default: break;
            }

            if (!command.IsSuccess) return command;

            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return read;

            return XGBCnetOverTcp.ExtractActualData(read.Content, true);
        }

        /// <summary>
        /// Write Data into plc, , you should specify the address
        /// </summary>
        /// <param name="address">the address of the data</param>
        /// <param name="value">source data</param>
        /// <returns>result contains whether success.</returns>
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> command = null;
 
            var DataTypeResult = XGBFastEnet.GetDataTypeToAddress(address);
            if (!DataTypeResult.IsSuccess) return OperateResult.CreateFailedResult<byte[]>(DataTypeResult);

            switch (DataTypeResult.Content)
            {
                case "Bit":
                    command = XGBCnetOverTcp.BuildWriteOneCommand(Station, address, value);
                    break;
                case "Continuous":
                    command = XGBCnetOverTcp.BuildWriteByteCommand(Station, address, value);
                    break;
                default: break;
            }

            if (!command.IsSuccess) return command;

            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return read;

            return XGBCnetOverTcp.ExtractActualData(read.Content, false);
        }

        #endregion

        #region Object Override

        /// <summary>
        /// Returns a string representing the current object
        /// </summary>
        /// <returns>×ض·û´®ذإد¢</returns>
        public override string ToString()
        {
            return $"XGBCnet[{PortName}:{BaudRate}]";
        }

        #endregion
    }
}
