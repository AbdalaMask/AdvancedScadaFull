using HslCommunication.BasicFramework;
using HslCommunication.Core;
using HslCommunication.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HslCommunication.Profinet.Keyence
{
    /// <summary>
    /// 基恩士KV上位链路串口通信的对象,适用于Nano系列串口数据,以及L20V通信模块
    /// </summary>
    /// <remarks>
    /// 地址的输入的格式说明如下：
    /// <list type="table">
    ///   <listheader>
    ///     <term>地址名称</term>
    ///     <term>地址代号</term>
    ///     <term>示例</term>
    ///     <term>地址进制</term>
    ///     <term>字操作</term>
    ///     <term>位操作</term>
    ///     <term>KV-7500/7300</term>
    ///     <term>KV-5500/5000/3000</term>
    ///     <term>KV Nano</term>
    ///   </listheader>
    ///   <item>
    ///     <term>输入继电器</term>
    ///     <term>X</term>
    ///     <term>X100,X1A0</term>
    ///     <term>16</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term>R00000~R99915</term>
    ///     <term>R00000~R99915</term>
    ///     <term>R00000～R59915</term>
    ///   </item>
    ///   <item>
    ///     <term>输出继电器</term>
    ///     <term>Y</term>
    ///     <term>Y100,Y1A0</term>
    ///     <term>16</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term>R00000~R99915</term>
    ///     <term>R00000~R99915</term>
    ///     <term>R00000～R59915</term>
    ///   </item>
    ///   <item>
    ///     <term>内部辅助继电器</term>
    ///     <term>MR</term>
    ///     <term>MR100,M200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term>MR00000~MR99915</term>
    ///     <term>MR00000~MR99915</term>
    ///     <term>MR00000～MR59915</term>
    ///   </item>
    ///   <item>
    ///     <term>数据存储器</term>
    ///     <term>DM</term>
    ///     <term>DM100,DM200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term>DM00000~DM65534</term>
    ///     <term>DM00000~DM65534</term>
    ///     <term>DM00000～DM32767</term>
    ///   </item>
    ///   <item>
    ///     <term>定时器（当前值）</term>
    ///     <term>TN</term>
    ///     <term>TN100,TN200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term>T0000~T3999</term>
    ///     <term>T0000~T3999</term>
    ///     <term>T000～T511</term>
    ///   </item>
    ///   <item>
    ///     <term>定时器（接点）</term>
    ///     <term>TS</term>
    ///     <term>TS100,TS200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term>T0000~T3999</term>
    ///     <term>T0000~T3999</term>
    ///     <term>T000～T511</term>
    ///   </item>
    ///   <item>
    ///     <term>计数器（当前值）</term>
    ///     <term>CN</term>
    ///     <term>CN100,CN200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term>C0000~C3999</term>
    ///     <term>C0000~C3999</term>
    ///     <term>C000～C255</term>
    ///   </item>
    ///   <item>
    ///     <term>计数器（接点）</term>
    ///     <term>CS</term>
    ///     <term>CS100,CS200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term>C0000~C3999</term>
    ///     <term>C0000~C3999</term>
    ///     <term>C000～C255</term>
    ///   </item>
    /// </list>
    /// </remarks>
    public class KeyenceNanoSerial : SerialDeviceBase<KeyenceNanoByteTransform>
    { 
        #region Constructor

        /// <summary>
        /// 实例化基恩士的串口协议的通讯对象
        /// </summary>
        public KeyenceNanoSerial()
        {
            WordLength = 1;
        }

        /// <summary>
        /// 初始化后建立通讯连接
        /// </summary>
        /// <returns>是否初始化成功</returns>
        protected override OperateResult InitializationOnOpen()
        {
            // 建立通讯连接{CR/r}
            var result = ReadBase(_buildConnectCmd);
            if (!result.IsSuccess) return result;

            return OperateResult.CreateSuccessResult();
        }
        #endregion

        #region Read Support


        /// <summary>
        /// 读取设备的short类型的数据
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <returns>带成功标志的结果数据对象</returns>
        public new OperateResult<short> ReadInt16(string address)
        {
            var result= ReadInt16(address, 1);
            if (!result.IsSuccess) return OperateResult.CreateFailedResult<short>(result); 
            return OperateResult.CreateSuccessResult(result.Content[0]); 
        }

        /// <summary>
        /// 读取设备的short类型的数组
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="length">数组长度</param>
        /// <returns>带成功标志的结果数据对象</returns>
        public new OperateResult<short[]> ReadInt16(string address, ushort length)
        { 
            address += ".S";
            return base.ReadInt16(address, length);
        }

        /// <summary>
        /// 读取设备的ushort数据类型的数据
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <returns>带成功标志的结果数据对象</returns>
        public new OperateResult<ushort> ReadUInt16(string address)
        {
            var result = ReadUInt16(address, 1);
            if (!result.IsSuccess) return OperateResult.CreateFailedResult<ushort>(result);
            return OperateResult.CreateSuccessResult(result.Content[0]);
        }

        /// <summary>
        /// 读取设备的ushort类型的数组
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="length">数组长度</param>
        /// <returns>带成功标志的结果数据对象</returns>
        public new OperateResult<ushort[]> ReadUInt16(string address, ushort length)
        { 
            address += ".U";
            return base.ReadUInt16(address, length);
        }

        /// <summary>
        /// 读取设备的int类型的数据
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <returns>带成功标志的结果数据对象</returns>
        public new OperateResult<int> ReadInt32(string address)
        {
            var result = ReadInt32(address, 1);
            if (!result.IsSuccess) return OperateResult.CreateFailedResult<int>(result);
            return OperateResult.CreateSuccessResult(result.Content[0]);
        }

        /// <summary>
        /// 读取设备的int类型的数组
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="length">数组长度</param>
        /// <returns>带成功标志的结果数据对象</returns>
        public new OperateResult<int[]> ReadInt32(string address, ushort length)
        { 
            address += ".L";
            return base.ReadInt32(address, length);
        }

        /// <summary>
        /// 读取设备的uint类型的数据
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <returns>带成功标志的结果数据对象</returns>
        public new OperateResult<uint> ReadUInt32(string address)
        {
            var result = ReadUInt32(address, 1);
            if (!result.IsSuccess) return OperateResult.CreateFailedResult<uint>(result);
            return OperateResult.CreateSuccessResult(result.Content[0]);
        }

        /// <summary>
        /// 读取设备的uint类型的数组
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="length">数组长度</param>
        /// <returns>带成功标志的结果数据对象</returns>
        public new OperateResult<uint[]> ReadUInt32(string address, ushort length)
        { 
            address += ".D";
            return base.ReadUInt32(address, length);
        }

        /// <summary>
        /// 从PLC中读取想要的数据，返回读取结果
        /// </summary>
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            // 获取指令
            OperateResult<byte[]> command = KeyenceNanoSerialOverTcp.BuildReadCommand( address, length);
            if (!command.IsSuccess) return OperateResult.CreateFailedResult<byte[]>(command);

            // 核心交互
            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return OperateResult.CreateFailedResult<byte[]>(read);

            // 反馈检查
            OperateResult ackResult = KeyenceNanoSerialOverTcp.CheckPlcReadResponse(read.Content);
            if (!ackResult.IsSuccess) return OperateResult.CreateFailedResult<byte[]>(ackResult);

            // 数据提炼
            return KeyenceNanoSerialOverTcp.ExtractActualData(read.Content);
        }

        /// <summary>
        /// 成批读取Bool值
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <param name="length">数组长度</param>
        /// <returns>带成功标志的结果数据对象</returns>
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            var strBuffer = Encoding.Default.GetString(Read(address, length).Content).Split(' ');
         
            var result = new bool[strBuffer.Length];
            for (int i = 0; i < length; i++)
            {
                result[i] = strBuffer[i] == "1" ? true : false;
            }
            return OperateResult.CreateSuccessResult(result);
        }

        #endregion

        #region Write Support

        /// <summary>
        /// 写入转换后的数据值
        /// </summary>
        /// <param name="address">软元件地址</param>
        /// <param name="value">转换后的Byte[]数据</param>
        /// <returns>是否成功写入的结果</returns>
        public override OperateResult Write(string address, byte[] value)
        {
            // 获取写入
            OperateResult<byte[]> command = KeyenceNanoSerialOverTcp.BuildWriteCommand(address, value);
            if (!command.IsSuccess) return command;

            // 核心交互
            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return read;

            // 结果验证
            OperateResult checkResult = KeyenceNanoSerialOverTcp.CheckPlcWriteResponse(read.Content);
            if (!checkResult.IsSuccess) return checkResult;

            return OperateResult.CreateSuccessResult();
        }

        /// <summary>
        ///  写入位数据的通断，支持的类型参考文档说明
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <param name="value">是否为通</param>
        /// <returns>是否写入成功的结果对象</returns>
        public override OperateResult Write(string address, bool value)
        {
            //value=true时:指令尾部命令为" 1 1";value=false:指令尾部命令为" 1 0";
            var byteTemp = value ? new byte[] {0x20, 0x31,0x20,0x31 } : new byte[] { 0x20, 0x31, 0x20, 0x30 };
            // 先获取指令
            OperateResult<byte[]> command = KeyenceNanoSerialOverTcp.BuildWriteCommand(address, byteTemp);
            if (!command.IsSuccess) return command;

            // 和串口进行核心的数据交互
            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return read;

            // 检查结果是否正确
            OperateResult checkResult = KeyenceNanoSerialOverTcp.CheckPlcWriteResponse(read.Content);
            if (!checkResult.IsSuccess) return checkResult;

            return OperateResult.CreateSuccessResult();
        }

        #endregion

        #region Private Member

        private byte[] _buildConnectCmd = new byte[3] { 0x43, 0x52, 0x0d };     // 建立通讯连接{CR/r}
        private byte[] _writeOkReturn = new byte[] { 0x4f, 0x4b, 0x0d, 0x0a };  // 写入数据成功返回指令

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串信息</returns>
        public override string ToString( )
        {
            return $"KeyenceNanoSerial[{PortName}:{BaudRate}]";
        }

        #endregion

    }
}
