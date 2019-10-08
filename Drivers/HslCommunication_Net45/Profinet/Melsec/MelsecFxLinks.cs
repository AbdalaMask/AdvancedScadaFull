using HslCommunication.Serial;
using HslCommunication.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HslCommunication.BasicFramework;

namespace HslCommunication.Profinet.Melsec
{
    /// <summary>
    /// 三菱PLC的计算机链接协议，适用的PLC型号参考备注
    /// </summary>
    /// <remarks>
    /// 支持的通讯的系列如下参考
    /// <list type="table">
    ///     <listheader>
    ///         <term>系列</term>
    ///         <term>是否支持</term>
    ///         <term>备注</term>
    ///     </listheader>
    ///     <item>
    ///         <description>FX3UC系列</description>
    ///         <description>支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX3U系列</description>
    ///         <description>支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX3GC系列</description>
    ///         <description>支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX3G系列</description>
    ///         <description>支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX3S系列</description>
    ///         <description>支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX2NC系列</description>
    ///         <description>支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX2N系列</description>
    ///         <description>部分支持(v1.06+)</description>
    ///         <description>通过监控D8001来确认版本号</description>
    ///     </item>
    ///     <item>
    ///         <description>FX1NC系列</description>
    ///         <description>支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX1N系列</description>
    ///         <description>支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX1S系列</description>
    ///         <description>支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX0N系列</description>
    ///         <description>部分支持(v1.20+)</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX0S系列</description>
    ///         <description>不支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX0系列</description>
    ///         <description>不支持</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX2C系列</description>
    ///         <description>部分支持(v3.30+)</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX2(FX)系列</description>
    ///         <description>部分支持(v3.30+)</description>
    ///         <description></description>
    ///     </item>
    ///     <item>
    ///         <description>FX1系列</description>
    ///         <description>不支持</description>
    ///         <description></description>
    ///     </item>
    /// </list>
    /// 数据地址支持的格式如下：
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
    ///     <term>8</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>输出继电器</term>
    ///     <term>Y</term>
    ///     <term>Y10,Y20</term>
    ///     <term>8</term>
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
    ///     <term>TS</term>
    ///     <term>TS100,TS200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>定时器的当前值</term>
    ///     <term>TN</term>
    ///     <term>TN100,TN200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>×</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>计数器的触点</term>
    ///     <term>CS</term>
    ///     <term>CS100,CS200</term>
    ///     <term>10</term>
    ///     <term>√</term>
    ///     <term>√</term>
    ///     <term></term>
    ///   </item>
    ///   <item>
    ///     <term>计数器的当前</term>
    ///     <term>CN</term>
    ///     <term>CN100,CN200</term>
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
    public class MelsecFxLinks : SerialDeviceBase<RegularByteTransform>
    {
        #region Constructor

        /// <summary>
        /// 实例化默认的构造方法
        /// </summary>
        public MelsecFxLinks( )
        {
            WordLength = 1;
        }

        #endregion

        #region Public Member

        /// <summary>
        /// PLC的站号信息
        /// </summary>
        public byte Station { get => station; set => station = value; }

        /// <summary>
        /// 报文等待时间，单位10ms，设置范围为0-15
        /// </summary>
        public byte WaittingTime
        {
            get => watiingTime;
            set
            {
                if (watiingTime > 0x0F)
                {
                    watiingTime = 0x0F;
                }
                else
                {
                    watiingTime = value;
                }
            }
        }

        /// <summary>
        /// 是否启动和校验
        /// </summary>
        public bool SumCheck { get => sumCheck; set => sumCheck = value; }

        #endregion

        #region Read Write Support

        /// <summary>
        /// 批量读取PLC的数据，以字为单位，支持读取X,Y,M,S,D,T,C，具体的地址范围需要根据PLC型号来确认
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <param name="length">数据长度</param>
        /// <returns>读取结果信息</returns>
        public override OperateResult<byte[]> Read( string address, ushort length )
        {
            // 解析指令
            OperateResult<byte[]> command = MelsecFxLinksOverTcp.BuildReadCommand( this.station, address, length, false, sumCheck, watiingTime );
            if (!command.IsSuccess) return OperateResult.CreateFailedResult<byte[]>( command );

            // 核心交互
            OperateResult<byte[]> read = ReadBase( command.Content );
            if (!read.IsSuccess) return OperateResult.CreateFailedResult<byte[]>( read );

            // 结果验证
            if (read.Content[0] != 0x02) return new OperateResult<byte[]>( read.Content[0], "Read Faild:" + BasicFramework.SoftBasic.ByteToHexString( read.Content, ' ' ) );

            // 提取结果
            byte[] Content = new byte[length * 2];
            for (int i = 0; i < Content.Length / 2; i++)
            {
                ushort tmp = Convert.ToUInt16( Encoding.ASCII.GetString( read.Content, i * 4 + 5, 4 ), 16 );
                BitConverter.GetBytes( tmp ).CopyTo( Content, i * 2 );
            }
            return OperateResult.CreateSuccessResult( Content );
        }

        /// <summary>
        /// 批量写入PLC的数据，以字为单位，也就是说最少2个字节信息，支持X,Y,M,S,D,T,C，具体的地址范围需要根据PLC型号来确认
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <param name="value">数据值</param>
        /// <returns>是否写入成功</returns>
        public override OperateResult Write( string address, byte[] value )
        {
            // 解析指令
            OperateResult<byte[]> command = MelsecFxLinksOverTcp.BuildWriteByteCommand( this.station, address, value, sumCheck, watiingTime );
            if (!command.IsSuccess) return command;

            // 核心交互
            OperateResult<byte[]> read = ReadBase( command.Content );
            if (!read.IsSuccess) return read;
            
            // 结果验证
            if (read.Content[0] != 0x06) return new OperateResult( read.Content[0], "Write Faild:" + BasicFramework.SoftBasic.ByteToHexString( read.Content, ' ' ) );

            // 提取结果
            return OperateResult.CreateSuccessResult( );
        }

        #endregion

        #region Bool Read Write

        /// <summary>
        /// 批量读取bool类型数据，支持的类型为X,Y,S,T,C，具体的地址范围取决于PLC的类型
        /// </summary>
        /// <param name="address">地址信息，比如X10,Y17，注意X，Y的地址是8进制的</param>
        /// <param name="length">读取的长度</param>
        /// <returns>读取结果信息</returns>
        public override OperateResult<bool[]> ReadBool( string address, ushort length )
        {
            // 解析指令
            OperateResult<byte[]> command = MelsecFxLinksOverTcp.BuildReadCommand( this.station, address, length, true, sumCheck, watiingTime );
            if (!command.IsSuccess) return OperateResult.CreateFailedResult<bool[]>( command );

            // 核心交互
            OperateResult<byte[]> read = ReadBase( command.Content );
            if(!read.IsSuccess) return OperateResult.CreateFailedResult<bool[]>( read );

            // 结果验证
            if (read.Content[0] != 0x02) return new OperateResult<bool[]>( read.Content[0], "Read Faild:" + BasicFramework.SoftBasic.ByteToHexString( read.Content, ' ' ) );

            // 提取结果
            byte[] buffer = new byte[length];
            Array.Copy( read.Content, 5, buffer, 0, length );
            return OperateResult.CreateSuccessResult( buffer.Select( m => m == 0x31 ).ToArray( ) );
        }

        /// <summary>
        /// 批量写入bool类型的数组，支持的类型为X,Y,S,T,C，具体的地址范围取决于PLC的类型
        /// </summary>
        /// <param name="address">PLC的地址信息</param>
        /// <param name="value">数据信息</param>
        /// <returns>是否写入成功</returns>
        public override OperateResult Write( string address, bool[] value )
        {
            // 解析指令
            OperateResult<byte[]> command = MelsecFxLinksOverTcp.BuildWriteBoolCommand( this.station, address, value, sumCheck, watiingTime );
            if (!command.IsSuccess) return command;

            // 核心交互
            OperateResult<byte[]> read = ReadBase( command.Content );
            if (!read.IsSuccess) return read;

            // 结果验证
            if (read.Content[0] != 0x06) return new OperateResult( read.Content[0], "Write Faild:" + BasicFramework.SoftBasic.ByteToHexString( read.Content, ' ' ) );

            // 提取结果
            return OperateResult.CreateSuccessResult( );
        }

        #endregion

        #region Start Stop

        /// <summary>
        /// 启动PLC
        /// </summary>
        /// <returns>是否启动成功</returns>
        public OperateResult StartPLC( )
        {
            // 解析指令
            OperateResult<byte[]> command = MelsecFxLinksOverTcp.BuildStart( this.station, sumCheck, watiingTime );
            if (!command.IsSuccess) return command;

            // 核心交互
            OperateResult<byte[]> read = ReadBase( command.Content );
            if (!read.IsSuccess) return read;

            // 结果验证
            if (read.Content[0] != 0x06) return new OperateResult( read.Content[0], "Start Faild:" + BasicFramework.SoftBasic.ByteToHexString( read.Content, ' ' ) );

            // 提取结果
            return OperateResult.CreateSuccessResult( );
        }

        /// <summary>
        /// 停止PLC
        /// </summary>
        /// <returns>是否停止成功</returns>
        public OperateResult StopPLC( )
        {
            // 解析指令
            OperateResult<byte[]> command = MelsecFxLinksOverTcp.BuildStop( this.station, sumCheck, watiingTime );
            if (!command.IsSuccess) return command;

            // 核心交互
            OperateResult<byte[]> read = ReadBase( command.Content );
            if (!read.IsSuccess) return read;

            // 结果验证
            if (read.Content[0] != 0x06) return new OperateResult( read.Content[0], "Stop Faild:" + BasicFramework.SoftBasic.ByteToHexString( read.Content, ' ' ) );

            // 提取结果
            return OperateResult.CreateSuccessResult( );
        }

        #endregion

        #region Private Member

        private byte station = 0x00;                 // PLC的站号信息
        private byte watiingTime = 0x00;             // 报文的等待时间，设置为0-15
        private bool sumCheck = true;                // 是否启用和校验

        #endregion
    }
}
