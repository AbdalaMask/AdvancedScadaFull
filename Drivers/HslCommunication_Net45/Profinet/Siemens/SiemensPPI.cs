using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HslCommunication.Serial;
using HslCommunication.Core.Address;
#if Net45
using System.Threading.Tasks;
#endif

namespace HslCommunication.Profinet.Siemens
{
    /// <summary>
    /// 西门子的PPI协议，适用于s7-200plc，注意，本类库有个致命的风险需要注意，由于本类库的每次通讯分成2次操作，故而不支持多线程同时读写，当发生线程竞争的时候，会导致数据异常，
    /// 想要解决的话，需要您在每次数据交互时添加同步锁。
    /// </summary>
    /// <remarks>
    /// 适用于西门子200的通信，非常感谢 合肥-加劲 的测试，让本类库圆满完成。
    /// 
    /// 注意：M地址范围有限 0-31地址
    /// </remarks>
    public class SiemensPPI : SerialDeviceBase<HslCommunication.Core.ReverseBytesTransform>
    {
        #region Constructor

        /// <summary>
        /// 实例化一个西门子的PPI协议对象
        /// </summary>
        public SiemensPPI( )
        {
            WordLength = 2;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 西门子PLC的站号信息
        /// </summary>
        public byte Station { get => station;
            set {
                station = value;
                executeConfirm[1] = value;

                int count = 0;
                for (int i = 1; i < 4; i++)
                {
                    count += executeConfirm[i];
                }
                executeConfirm[4] = (byte)count;
            }
        }

        #endregion

        #region Read Write Override

        /// <summary>
        /// 从西门子的PLC中读取数据信息，地址为"M100","AI100","I0","Q0","V100","S100"等，详细请参照API文档
        /// </summary>
        /// <param name="address">西门子的地址数据信息</param>
        /// <param name="length">数据长度</param>
        /// <returns>带返回结果的结果对象</returns>
        public override OperateResult<byte[]> Read( string address, ushort length )
        {
            // 解析指令
            OperateResult<byte[]> command = SiemensPPIOverTcp.BuildReadCommand( station, address, length, false );
            if (!command.IsSuccess) return command;

            // 第一次数据交互
            OperateResult<byte[]> read1 = ReadBase( command.Content );
            if (!read1.IsSuccess) return read1;

            // 验证
            if (read1.Content[0] != 0xE5) return new OperateResult<byte[]>( "PLC Receive Check Failed:" + BasicFramework.SoftBasic.ByteToHexString( read1.Content, ' ' ) );

            // 第二次数据交互
            OperateResult<byte[]> read2 = ReadBase( executeConfirm );
            if (!read2.IsSuccess) return read2;

            // 错误码判断
            if (read2.Content.Length < 21) return new OperateResult<byte[]>( read2.ErrorCode, "Failed: " + BasicFramework.SoftBasic.ByteToHexString( read2.Content, ' ' ) );
            if (read2.Content[17] != 0x00 || read2.Content[18] != 0x00) return new OperateResult<byte[]>( read2.Content[19], SiemensPPIOverTcp.GetMsgFromStatus( read2.Content[18], read2.Content[19] ) );
            if (read2.Content[21] != 0xFF) return new OperateResult<byte[]>( read2.Content[21], SiemensPPIOverTcp.GetMsgFromStatus( read2.Content[21] ) );

            // 数据提取
            byte[] buffer = new byte[length];
            if (read2.Content[21] == 0xFF && read2.Content[22] == 0x04)
            {
                Array.Copy( read2.Content, 25, buffer, 0, length );
            }
            return OperateResult.CreateSuccessResult( buffer );
        }

        /// <summary>
        /// 从西门子的PLC中读取bool数据信息，地址为"M100.0","AI100.1","I0.3","Q0.6","V100.4","S100"等，详细请参照API文档
        /// </summary>
        /// <param name="address">西门子的地址数据信息</param>
        /// <param name="length">数据长度</param>
        /// <returns>带返回结果的结果对象</returns>
        public override OperateResult<bool[]> ReadBool( string address, ushort length )
        {
            // 解析指令
            OperateResult<byte[]> command = SiemensPPIOverTcp.BuildReadCommand( station, address, length, true );
            if (!command.IsSuccess) return OperateResult.CreateFailedResult<bool[]>( command );

            // 第一次数据交互
            OperateResult<byte[]> read1 = ReadBase( command.Content );
            if (!read1.IsSuccess) return OperateResult.CreateFailedResult<bool[]>( read1 );

            // 验证
            if (read1.Content[0] != 0xE5) return new OperateResult<bool[]>( "PLC Receive Check Failed:" + BasicFramework.SoftBasic.ByteToHexString( read1.Content, ' ' ) );

            // 第二次数据交互
            OperateResult<byte[]> read2 = ReadBase( executeConfirm );
            if (!read2.IsSuccess) return OperateResult.CreateFailedResult<bool[]>( read2 );

            // 错误码判断
            if (read2.Content.Length < 21) return new OperateResult<bool[]>( read2.ErrorCode, "Failed: " + BasicFramework.SoftBasic.ByteToHexString( read2.Content, ' ' ) );
            if (read2.Content[17] != 0x00 || read2.Content[18] != 0x00) return new OperateResult<bool[]>( read2.Content[19], SiemensPPIOverTcp.GetMsgFromStatus( read2.Content[18], read2.Content[19] ) );
            if (read2.Content[21] != 0xFF) return new OperateResult<bool[]>( read2.Content[21], SiemensPPIOverTcp.GetMsgFromStatus( read2.Content[21] ) );

            // 数据提取
            byte[] buffer = new byte[read2.Content.Length - 27];
            if (read2.Content[21] == 0xFF && read2.Content[22] == 0x03)
            {
                Array.Copy( read2.Content, 25, buffer, 0, buffer.Length );
            }

            return OperateResult.CreateSuccessResult( BasicFramework.SoftBasic.ByteToBoolArray( buffer, length ) );
        }

        /// <summary>
        /// 将字节数据写入到西门子PLC中，地址为"M100.0","AI100.1","I0.3","Q0.6","V100.4","S100"等，详细请参照API文档
        /// </summary>
        /// <param name="address">西门子的地址数据信息</param>
        /// <param name="value">数据长度</param>
        /// <returns>带返回结果的结果对象</returns>
        public override OperateResult Write( string address, byte[] value )
        {
            // 解析指令
            OperateResult<byte[]> command = SiemensPPIOverTcp.BuildWriteCommand( station, address, value );
            if (!command.IsSuccess) return command;

            // 第一次数据交互
            OperateResult<byte[]> read1 = ReadBase( command.Content );
            if (!read1.IsSuccess) return read1;

            // 验证
            if (read1.Content[0] != 0xE5) return new OperateResult<byte[]>( "PLC Receive Check Failed:" + read1.Content[0] );

            // 第二次数据交互
            OperateResult<byte[]> read2 = ReadBase( executeConfirm );
            if (!read2.IsSuccess) return read2;

            // 错误码判断
            if (read2.Content.Length < 21) return new OperateResult( read2.ErrorCode, "Failed: " + BasicFramework.SoftBasic.ByteToHexString( read2.Content, ' ' ) );
            if (read2.Content[17] != 0x00 || read2.Content[18] != 0x00) return new OperateResult( read2.Content[19], SiemensPPIOverTcp.GetMsgFromStatus( read2.Content[18], read2.Content[19] ) );
            if (read2.Content[21] != 0xFF) return new OperateResult( read2.Content[21], SiemensPPIOverTcp.GetMsgFromStatus( read2.Content[21] ) );
            // 数据提取
            return OperateResult.CreateSuccessResult( );
        }

        /// <summary>
        /// 将bool数据写入到西门子PLC中，地址为"M100.0","AI100.1","I0.3","Q0.6","V100.4","S100"等，详细请参照API文档
        /// </summary>
        /// <param name="address">西门子的地址数据信息</param>
        /// <param name="value">数据长度</param>
        /// <returns>带返回结果的结果对象</returns>
        public override OperateResult Write(string address, bool[] value )
        {
            // 解析指令
            OperateResult<byte[]> command = SiemensPPIOverTcp.BuildWriteCommand( station, address, value );
            if (!command.IsSuccess) return command;

            // 第一次数据交互
            OperateResult<byte[]> read1 = ReadBase( command.Content );
            if (!read1.IsSuccess) return read1;

            // 验证
            if (read1.Content[0] != 0xE5) return new OperateResult<byte[]>( "PLC Receive Check Failed:" + read1.Content[0] );
            
            // 第二次数据交互
            OperateResult<byte[]> read2 = ReadBase( executeConfirm );
            if (!read2.IsSuccess) return read2;

            // 错误码判断
            if (read2.Content.Length < 21) return new OperateResult( read2.ErrorCode, "Failed: " + BasicFramework.SoftBasic.ByteToHexString( read2.Content, ' ' ) );
            if (read2.Content[17] != 0x00 || read2.Content[18] != 0x00) return new OperateResult( read2.Content[19], SiemensPPIOverTcp.GetMsgFromStatus( read2.Content[18], read2.Content[19] ) );
            if (read2.Content[21] != 0xFF) return new OperateResult( read2.Content[21], SiemensPPIOverTcp.GetMsgFromStatus( read2.Content[21] ) );
            // 数据提取
            return OperateResult.CreateSuccessResult( );
        }

        #endregion

        #region Byte Read Write

        /// <summary>
        /// 从西门子的PLC中读取byte数据信息，地址为"M100.0","AI100.1","I0.3","Q0.6","V100.4","S100"等，详细请参照API文档
        /// </summary>
        /// <param name="address">西门子的地址数据信息</param>
        /// <returns>带返回结果的结果对象</returns>
        public OperateResult<byte> ReadByte( string address )
        {
            OperateResult<byte[]> read = Read( address, 1 );
            if (!read.IsSuccess) return OperateResult.CreateFailedResult<byte>( read );

            return OperateResult.CreateSuccessResult( read.Content[0] );
        }

        /// <summary>
        /// 将byte数据写入到西门子PLC中，地址为"M100.0","AI100.1","I0.3","Q0.6","V100.4","S100"等，详细请参照API文档
        /// </summary>
        /// <param name="address">西门子的地址数据信息</param>
        /// <param name="value">数据长度</param>
        /// <returns>带返回结果的结果对象</returns>
        public OperateResult WriteByte(string address, byte value )
        {
            return Write( address, new byte[] { value } );
        }

        #endregion

        #region Start Stop

        /// <summary>
        /// 启动西门子PLC为RUN模式
        /// </summary>
        /// <returns>是否启动成功</returns>
        public OperateResult Start( )
        {
            byte[] cmd = new byte[] { 0x68, 0x21, 0x21, 0x68, station, 0x00, 0x6C, 0x32, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x14, 0x00, 0x00, 0x28, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFD, 0x00, 0x00, 0x09, 0x50, 0x5F, 0x50, 0x52, 0x4F, 0x47, 0x52, 0x41, 0x4D, 0xAA, 0x16 };
            // 第一次数据交互
            OperateResult<byte[]> read1 = ReadBase( cmd );
            if (!read1.IsSuccess) return read1;

            // 验证
            if (read1.Content[0] != 0xE5) return new OperateResult<byte[]>( "PLC Receive Check Failed:" + read1.Content[0] );

            // 第二次数据交互
            OperateResult<byte[]> read2 = ReadBase( executeConfirm );
            if (!read2.IsSuccess) return read2;

            // 数据提取
            return OperateResult.CreateSuccessResult( );
        }

        /// <summary>
        /// 停止西门子PLC，切换为Stop模式
        /// </summary>
        /// <returns>是否停止成功</returns>
        public OperateResult Stop( )
        {
            byte[] cmd = new byte[] { 0x68, 0x1D, 0x1D, 0x68, station, 0x00, 0x6C, 0x32, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x29, 0x00, 0x00, 0x00, 0x00, 0x00, 0x09, 0x50, 0x5F, 0x50, 0x52, 0x4F, 0x47, 0x52, 0x41, 0x4D, 0xAA, 0x16 };
            // 第一次数据交互
            OperateResult<byte[]> read1 = ReadBase( cmd );
            if (!read1.IsSuccess) return read1;

            // 验证
            if (read1.Content[0] != 0xE5) return new OperateResult<byte[]>( "PLC Receive Check Failed:" + read1.Content[0] );

            // 第二次数据交互
            OperateResult<byte[]> read2 = ReadBase( executeConfirm );
            if (!read2.IsSuccess) return read2;

            // 数据提取
            return OperateResult.CreateSuccessResult( );
        }

        #endregion

        #region Private Member

        private byte station = 0x02;            // PLC的站号信息
        private byte[] executeConfirm = new byte[] { 0x10, 0x02, 0x00, 0x5C, 0x5E, 0x16 };

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串信息</returns>
        public override string ToString( )
        {
            return $"SiemensPPI[{PortName}:{BaudRate}]";
        }

        #endregion
    }
}
