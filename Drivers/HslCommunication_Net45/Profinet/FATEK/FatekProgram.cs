using HslCommunication.Core;
using HslCommunication.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HslCommunication.BasicFramework;


namespace HslCommunication.Profinet.FATEK
{
    /// <summary>
    /// 台湾永宏公司的编程口协议
    /// </summary>
    /// <remarks>
    /// 其所支持的地址形式如下：
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
    ///     <term>内部继电器</term>
    ///     <term>M</term>
    ///     <term>M100,M200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>输入继电器</term>
    ///     <term>X</term>
    ///     <term>X10,X20</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>输出继电器</term>
    ///     <term>Y</term>
    ///     <term>Y10,Y20</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>步进继电器</term>
    ///     <term>S</term>
    ///     <term>S100,S200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>定时器的触点</term>
    ///     <term>T</term>
    ///     <term>T100,T200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>定时器的当前值</term>
    ///     <term>RT</term>
    ///     <term>RT100,RT200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>计数器的触点</term>
    ///     <term>C</term>
    ///     <term>C100,C200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>计数器的当前</term>
    ///     <term>RC</term>
    ///     <term>RC100,RC200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>数据寄存器</term>
    ///     <term>D</term>
    ///     <term>D1000,D2000</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>文件寄存器</term>
    ///     <term>R</term>
    ///     <term>R100,R200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    /// </list>
    /// </remarks>
    public class FatekProgram : SerialDeviceBase<RegularByteTransform>
    {
        #region Constructor

        /// <summary>
        /// 实例化默认的构造方法
        /// </summary>
        public FatekProgram( )
        {
            WordLength = 1;
        }

        #endregion

        #region Public Member

        /// <summary>
        /// PLC的站号信息
        /// </summary>
        public byte Station { get => station; set => station = value; }

        #endregion

        #region Read Write Support

        /// <summary>
        /// 批量读取PLC的数据，以字为单位，支持读取X,Y,M,S,D,T,C,R,RT,RC具体的地址范围需要根据PLC型号来确认
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <param name="length">数据长度</param>
        /// <returns>读取结果信息</returns>
        public override OperateResult<byte[]> Read( string address, ushort length )
        {
            // 解析指令
            OperateResult<byte[]> command = FatekProgramOverTcp.BuildReadCommand( this.station, address, length, false );
            if (!command.IsSuccess) return OperateResult.CreateFailedResult<byte[]>( command );

            // 核心交互
            OperateResult<byte[]> read = ReadBase( command.Content );
            if (!read.IsSuccess) return OperateResult.CreateFailedResult<byte[]>( read );

            // 结果验证
            if (read.Content[0] != 0x02) return new OperateResult<byte[]>( read.Content[0], "Read Faild:" + BasicFramework.SoftBasic.ByteToHexString( read.Content, ' ' ) );
            if (read.Content[5] != 0x30) return new OperateResult<byte[]>( read.Content[5], FatekProgramOverTcp.GetErrorDescriptionFromCode( (char)read.Content[5] ) );

            // 提取结果
            byte[] Content = new byte[length * 2];
            for (int i = 0; i < Content.Length / 2; i++)
            {
                ushort tmp = Convert.ToUInt16( Encoding.ASCII.GetString( read.Content, i * 4 + 6, 4 ), 16 );
                BitConverter.GetBytes( tmp ).CopyTo( Content, i * 2 );
            }
            return OperateResult.CreateSuccessResult( Content );
        }

        /// <summary>
        /// 批量写入PLC的数据，以字为单位，也就是说最少2个字节信息，支持X,Y,M,S,D,T,C,R,RT,RC具体的地址范围需要根据PLC型号来确认
        /// </summary>
        /// <param name="address">地址信息，举例，D100，R200，RC100，RT200</param>
        /// <param name="value">数据值</param>
        /// <returns>是否写入成功</returns>
        public override OperateResult Write( string address, byte[] value )
        {
            // 解析指令
            OperateResult<byte[]> command = FatekProgramOverTcp.BuildWriteByteCommand( this.station, address, value );
            if (!command.IsSuccess) return command;

            // 核心交互
            OperateResult<byte[]> read = ReadBase( command.Content );
            if (!read.IsSuccess) return read;

            // 结果验证
            if (read.Content[0] != 0x02) return new OperateResult( read.Content[0], "Write Faild:" + BasicFramework.SoftBasic.ByteToHexString( read.Content, ' ' ) );
            if (read.Content[5] != 0x30) return new OperateResult<byte[]>( read.Content[5], FatekProgramOverTcp.GetErrorDescriptionFromCode( (char)read.Content[5] ) );

            // 提取结果
            return OperateResult.CreateSuccessResult( );
        }

        #endregion

        #region Bool Read Write

        /// <summary>
        /// 批量读取bool类型数据，支持的类型为X,Y,M,S,T,C，具体的地址范围取决于PLC的类型
        /// </summary>
        /// <param name="address">地址信息，比如X10，Y17，M100</param>
        /// <param name="length">读取的长度</param>
        /// <returns>读取结果信息</returns>
        public override OperateResult<bool[]> ReadBool( string address, ushort length )
        {
            // 解析指令
            OperateResult<byte[]> command = FatekProgramOverTcp.BuildReadCommand( this.station, address, length, true );
            if (!command.IsSuccess) return OperateResult.CreateFailedResult<bool[]>( command );

            // 核心交互
            OperateResult<byte[]> read = ReadBase( command.Content );
            if (!read.IsSuccess) return OperateResult.CreateFailedResult<bool[]>( read );

            // 结果验证
            if (read.Content[0] != 0x02) return new OperateResult<bool[]>( read.Content[0], "Read Faild:" + BasicFramework.SoftBasic.ByteToHexString( read.Content, ' ' ) );
            if (read.Content[5] != 0x30) return new OperateResult<bool[]>( read.Content[5], FatekProgramOverTcp.GetErrorDescriptionFromCode( (char)read.Content[5] ) );

            // 提取结果
            byte[] buffer = new byte[length];
            Array.Copy( read.Content, 6, buffer, 0, length );
            return OperateResult.CreateSuccessResult( buffer.Select( m => m == 0x31 ).ToArray( ) );
        }

        /// <summary>
        /// 批量写入bool类型的数组，支持的类型为X,Y,M,S,T,C，具体的地址范围取决于PLC的类型
        /// </summary>
        /// <param name="address">PLC的地址信息</param>
        /// <param name="value">数据信息</param>
        /// <returns>是否写入成功</returns>
        public override OperateResult Write( string address, bool[] value )
        {
            // 解析指令
            OperateResult<byte[]> command = FatekProgramOverTcp.BuildWriteBoolCommand( this.station, address, value );
            if (!command.IsSuccess) return command;

            // 核心交互
            OperateResult<byte[]> read = ReadBase( command.Content );
            if (!read.IsSuccess) return read;

            // 结果验证
            if (read.Content[0] != 0x02) return new OperateResult( read.Content[0], "Write Faild:" + BasicFramework.SoftBasic.ByteToHexString( read.Content, ' ' ) );
            if (read.Content[5] != 0x30) return new OperateResult<bool[]>( read.Content[5], FatekProgramOverTcp.GetErrorDescriptionFromCode( (char)read.Content[5] ) );

            // 提取结果
            return OperateResult.CreateSuccessResult( );
        }

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString( )
        {
            return $"FatekProgram[{PortName}:{BaudRate}]";
        }

        #endregion

        #region Private Member

        private byte station = 0x01;                 // PLC的站号信息

        #endregion
    }
}
