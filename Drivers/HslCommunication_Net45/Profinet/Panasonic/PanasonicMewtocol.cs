using HslCommunication.Core;
using HslCommunication.Serial;

namespace HslCommunication.Profinet.Panasonic
{
    /// <summary>
    /// 松下PLC的数据交互协议，采用Mewtocol协议通讯
    /// </summary>
    /// <remarks>
    /// 触点地址的输入的格式说明如下：
    /// <list type="table">
    ///   <listheader>
    ///     <term>地址名称</term>
    ///     <term>地址代号</term>
    ///     <term>示例</term>
    ///     <term>地址进制</term>
    ///     <term>字操作</term>
    ///     <term>位操作</term>
    ///     <term>备注</term>
    ///   </listheader>
    ///   <item>
    ///     <term>外部输入继电器</term>
    ///     <term>X</term>
    ///     <term>X0,X100</term>
    ///     <term>10</term>
    ///     <term>×</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>外部输出继电器</term>
    ///     <term>Y</term>
    ///     <term>Y0,Y100</term>
    ///     <term>10</term>
    ///     <term>×</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>内部继电器</term>
    ///     <term>R</term>
    ///     <term>R0,R100</term>
    ///     <term>10</term>
    ///     <term>×</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>定时器</term>
    ///     <term>T</term>
    ///     <term>T0,T100</term>
    ///     <term>10</term>
    ///     <term>×</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>计数器</term>
    ///     <term>C</term>
    ///     <term>C0,C100</term>
    ///     <term>10</term>
    ///     <term>×</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>链接继电器</term>
    ///     <term>L</term>
    ///     <term>L0,L100</term>
    ///     <term>10</term>
    ///     <term>×</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    /// </list>
    /// 数据地址的输入的格式说明如下：
    /// <list type="table">
    ///   <listheader>
    ///     <term>地址名称</term>
    ///     <term>地址代号</term>
    ///     <term>示例</term>
    ///     <term>地址进制</term>
    ///     <term>字操作</term>
    ///     <term>位操作</term>
    ///     <term>备注</term>
    ///   </listheader>
    ///   <item>
    ///     <term>数据寄存器 DT</term>
    ///     <term>D</term>
    ///     <term>D0,D100</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>链接寄存器 LT</term>
    ///     <term>L</term>
    ///     <term>L0,L100</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>文件寄存器 FL</term>
    ///     <term>F</term>
    ///     <term>F0,F100</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>目标值 SV</term>
    ///     <term>S</term>
    ///     <term>S0,S100</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>经过值 EV</term>
    ///     <term>K</term>
    ///     <term>K0,K100</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>索引寄存器 IX</term>
    ///     <term>IX</term>
    ///     <term>IX0,IX100</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>索引寄存器 IY</term>
    ///     <term>IY</term>
    ///     <term>IY0,IY100</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    /// </list>
    /// </remarks>
    public class PanasonicMewtocol : SerialDeviceBase<RegularByteTransform>
    {
        #region Constructor

        /// <summary>
        /// 实例化一个默认的松下PLC通信对象，默认站号为1
        /// </summary>
        /// <param name="station">站号信息，默认为0xEE</param>
        public PanasonicMewtocol(byte station = 238)
        {
            this.Station = station;
            this.ByteTransform.DataFormat = DataFormat.DCBA;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 设备的目标站号
        /// </summary>
        public byte Station { get; set; }

        #endregion

        #region Read Write Override

        /// <summary>
        /// 从松下PLC中读取数据
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="length">长度</param>
        /// <returns>返回数据信息</returns>
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            // 创建指令
            OperateResult<byte[]> command = PanasonicMewtocolOverTcp.BuildReadCommand(Station, address, length);
            if (!command.IsSuccess) return command;

            // 数据交互
            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return read;

            // 提取数据
            return PanasonicMewtocolOverTcp.ExtraActualData(read.Content);
        }

        /// <summary>
        /// 将数据写入到松下PLC中
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="value">真实数据</param>
        /// <returns>是否写入成功</returns>
        public override OperateResult Write(string address, byte[] value)
        {
            // 创建指令
            OperateResult<byte[]> command = PanasonicMewtocolOverTcp.BuildWriteCommand(Station, address, value);
            if (!command.IsSuccess) return command;

            // 数据交互
            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return read;

            // 提取结果
            return PanasonicMewtocolOverTcp.ExtraActualData(read.Content);
        }

        #endregion

        #region Read Write Bool

        /// <summary>
        /// 批量读取松下PLC的位数据
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="length">数据长度</param>
        /// <returns>读取结果对象</returns>
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            // 读取数据
            OperateResult<byte[]> read = Read(address, length);
            if (!read.IsSuccess) return OperateResult.CreateFailedResult<bool[]>(read);

            // 提取bool
            byte[] buffer = BasicFramework.SoftBasic.BytesReverseByWord(read.Content);
            return OperateResult.CreateSuccessResult(BasicFramework.SoftBasic.ByteToBoolArray(read.Content, length));
        }

        /// <summary>
        /// 读取单个的地址信息的bool值
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <returns>读取结果对象</returns>
        public override OperateResult<bool> ReadBool(string address)
        {
            // 创建指令
            OperateResult<byte[]> command = PanasonicMewtocolOverTcp.BuildReadOneCoil(Station, address);
            if (!command.IsSuccess) return OperateResult.CreateFailedResult<bool>(command);

            // 数据交互
            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return OperateResult.CreateFailedResult<bool>(read);

            // 提取数据
            return PanasonicMewtocolOverTcp.ExtraActualBool(read.Content);
        }

        /// <summary>
        /// 写入bool数据信息，存在一定的风险，谨慎操作
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="values">数据值信息</param>
        /// <returns>返回是否成功的结果对象</returns>
        public override OperateResult Write(string address, bool[] values)
        {
            // 计算字节数据
            byte[] buffer = BasicFramework.SoftBasic.BoolArrayToByte(values);

            // 创建指令
            OperateResult<byte[]> command = PanasonicMewtocolOverTcp.BuildWriteCommand(Station, address, BasicFramework.SoftBasic.BytesReverseByWord(buffer), (short)values.Length);
            if (!command.IsSuccess) return command;

            // 数据交互
            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return read;

            // 提取结果
            return PanasonicMewtocolOverTcp.ExtraActualData(read.Content);
        }

        /// <summary>
        /// 写入bool数据信息
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="value">数据值信息</param>
        /// <returns>返回是否成功的结果对象</returns>
        public override OperateResult Write(string address, bool value)
        {
            // 创建指令
            OperateResult<byte[]> command = PanasonicMewtocolOverTcp.BuildWriteOneCoil(Station, address, value);
            if (!command.IsSuccess) return command;

            // 数据交互
            OperateResult<byte[]> read = ReadBase(command.Content);
            if (!read.IsSuccess) return read;

            // 提取结果
            return PanasonicMewtocolOverTcp.ExtraActualData(read.Content);
        }

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串信息</returns>
        public override string ToString()
        {
            return $"Panasonic Mewtocol[{PortName}:{BaudRate}]";
        }

        #endregion
    }
}
