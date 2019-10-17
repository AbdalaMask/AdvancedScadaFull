using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HslCommunication.BasicFramework;
using HslCommunication.Core.Address;
using HslCommunication.Serial;

namespace HslCommunication.ModBus
{

    /// <summary>
    /// Modbus协议相关的一些信息
    /// </summary>
    public class ModbusInfo
    {
        #region Function Declaration
        
        /// <summary>
        /// 读取线圈
        /// </summary>
        public const byte ReadCoil = 0x01;

        /// <summary>
        /// 读取离散量
        /// </summary>
        public const byte ReadDiscrete = 0x02;

        /// <summary>
        /// 读取寄存器
        /// </summary>
        public const byte ReadRegister = 0x03;

        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        public const byte ReadInputRegister = 0x04;

        /// <summary>
        /// 写单个线圈
        /// </summary>
        public const byte WriteOneCoil = 0x05;

        /// <summary>
        /// 写单个寄存器
        /// </summary>
        public const byte WriteOneRegister = 0x06;

        /// <summary>
        /// 写多个线圈
        /// </summary>
        public const byte WriteCoil = 0x0F;

        /// <summary>
        /// 写多个寄存器
        /// </summary>
        public const byte WriteRegister = 0x10;

        #endregion

        #region ErrCode Declaration
        
        /// <summary>
        /// 不支持该功能码
        /// </summary>
        public const byte FunctionCodeNotSupport = 0x01;
        /// <summary>
        /// 该地址越界
        /// </summary>
        public const byte FunctionCodeOverBound = 0x02;
        /// <summary>
        /// 读取长度超过最大值
        /// </summary>
        public const byte FunctionCodeQuantityOver = 0x03;
        /// <summary>
        /// 读写异常
        /// </summary>
        public const byte FunctionCodeReadWriteException = 0x04;

        #endregion
        
        #region Static Helper Method

        /// <summary>
        /// 构建Modbus读取数据的核心报文，需要指定地址，长度，站号，是否起始地址0，默认的功能码应该根据bool或是字来区分
        /// </summary>
        /// <param name="address">Modbus的富文本地址</param>
        /// <param name="length">读取的数据长度</param>
        /// <param name="station">默认的站号信息</param>
        /// <param name="isStartWithZero">起始地址是否从0开始</param>
        /// <param name="defaultFunction">默认的功能码</param>
        /// <returns>包含最终命令的结果对象</returns>
        public static OperateResult<byte[]> BuildReadModbusCommand( string address, ushort length, byte station, bool isStartWithZero, byte defaultFunction )
        {
            try
            {
                ModbusAddress mAddress = new ModbusAddress( address, station, defaultFunction );
                if (!isStartWithZero)
                {
                    if (mAddress.Address < 1) throw new Exception( StringResources.Language.ModbusAddressMustMoreThanOne );
                    mAddress.Address = (ushort)(mAddress.Address - 1);
                }

                return BuildReadModbusCommand( mAddress, length );
            }
            catch(Exception ex)
            {
                return new OperateResult<byte[]>( ex.Message );
            }
        }

        /// <summary>
        /// 构建Modbus读取数据的核心报文，需要指定地址，长度，站号，是否起始地址0，默认的功能码应该根据bool或是字来区分
        /// </summary>
        /// <param name="mAddress">Modbus的富文本地址</param>
        /// <param name="length">读取的数据长度</param>
        /// <returns>包含最终命令的结果对象</returns>
        public static OperateResult<byte[]> BuildReadModbusCommand( ModbusAddress mAddress, ushort length )
        {
            try
            {
                byte[] content = new byte[6];
                content[0] = (byte)mAddress.Station;
                content[1] = (byte)mAddress.Function;
                content[2] = BitConverter.GetBytes( mAddress.Address )[1];
                content[3] = BitConverter.GetBytes( mAddress.Address )[0];
                content[4] = BitConverter.GetBytes( length )[1];
                content[5] = BitConverter.GetBytes( length )[0];
                return OperateResult.CreateSuccessResult( content );
            }
            catch (Exception ex)
            {
                return new OperateResult<byte[]>( ex.Message );
            }
        }

        /// <summary>
        /// 构建Modbus写入bool数据的核心报文，需要指定地址，长度，站号，是否起始地址0，默认的功能码
        /// </summary>
        /// <param name="address">Modbus的富文本地址</param>
        /// <param name="values">bool数组的信息</param>
        /// <param name="station">默认的站号信息</param>
        /// <param name="isStartWithZero">起始地址是否从0开始</param>
        /// <param name="defaultFunction">默认的功能码</param>
        /// <returns>包含最终命令的结果对象</returns>
        public static OperateResult<byte[]> BuildWriteBoolModbusCommand( string address, bool[] values, byte station, bool isStartWithZero, byte defaultFunction )
        {
            try
            {
                ModbusAddress mAddress = new ModbusAddress( address, station, defaultFunction );
                if (!isStartWithZero)
                {
                    if (mAddress.Address < 1) throw new Exception( StringResources.Language.ModbusAddressMustMoreThanOne );
                    mAddress.Address = (ushort)(mAddress.Address - 1);
                }

                return BuildWriteBoolModbusCommand( mAddress, values );
            }
            catch (Exception ex)
            {
                return new OperateResult<byte[]>( ex.Message );
            }
        }

        /// <summary>
        /// 构建Modbus写入bool数据的核心报文，需要指定地址，长度，站号，是否起始地址0，默认的功能码
        /// </summary>
        /// <param name="mAddress">Modbus的富文本地址</param>
        /// <param name="values">bool数组的信息</param>
        /// <returns>包含最终命令的结果对象</returns>
        public static OperateResult<byte[]> BuildWriteBoolModbusCommand( ModbusAddress mAddress, bool[] values )
        {
            try
            {
                byte[] data = SoftBasic.BoolArrayToByte( values );
                byte[] content = new byte[7 + data.Length];
                content[0] = (byte)mAddress.Station;
                content[1] = (byte)mAddress.Function;
                content[2] = BitConverter.GetBytes( mAddress.Address )[1];
                content[3] = BitConverter.GetBytes( mAddress.Address )[0];
                content[4] = (byte)(values.Length / 256);
                content[5] = (byte)(values.Length % 256);
                content[6] = (byte)(data.Length);
                data.CopyTo( content, 7 );
                return OperateResult.CreateSuccessResult( content );
            }
            catch (Exception ex)
            {
                return new OperateResult<byte[]>( ex.Message );
            }
        }

        /// <summary>
        /// 构建Modbus写入字数据的核心报文，需要指定地址，长度，站号，是否起始地址0，默认的功能码
        /// </summary>
        /// <param name="address">Modbus的富文本地址</param>
        /// <param name="values">bool数组的信息</param>
        /// <param name="station">默认的站号信息</param>
        /// <param name="isStartWithZero">起始地址是否从0开始</param>
        /// <param name="defaultFunction">默认的功能码</param>
        /// <returns>包含最终命令的结果对象</returns>
        public static OperateResult<byte[]> BuildWriteWordModbusCommand( string address, byte[] values, byte station, bool isStartWithZero, byte defaultFunction )
        {
            try
            {
                ModbusAddress mAddress = new ModbusAddress( address, station, defaultFunction );
                if (!isStartWithZero)
                {
                    if (mAddress.Address < 1) throw new Exception( StringResources.Language.ModbusAddressMustMoreThanOne );
                    mAddress.Address = (ushort)(mAddress.Address - 1);
                }

                return BuildWriteWordModbusCommand( mAddress, values );
            }
            catch (Exception ex)
            {
                return new OperateResult<byte[]>( ex.Message );
            }
        }

        /// <summary>
        /// 构建Modbus写入字数据的核心报文，需要指定地址，长度，站号，是否起始地址0，默认的功能码
        /// </summary>
        /// <param name="mAddress">Modbus的富文本地址</param>
        /// <param name="values">bool数组的信息</param>
        /// <returns>包含最终命令的结果对象</returns>
        public static OperateResult<byte[]> BuildWriteWordModbusCommand( ModbusAddress mAddress, byte[] values )
        {
            try
            {
                byte[] content = new byte[7 + values.Length];
                content[0] = (byte)mAddress.Station;
                content[1] = (byte)mAddress.Function;
                content[2] = BitConverter.GetBytes( mAddress.Address )[1];
                content[3] = BitConverter.GetBytes( mAddress.Address )[0];
                content[4] = (byte)(values.Length / 2 / 256);
                content[5] = (byte)(values.Length / 2 % 256);
                content[6] = (byte)(values.Length);
                values.CopyTo( content, 7 );
                return OperateResult.CreateSuccessResult( content );
            }
            catch (Exception ex)
            {
                return new OperateResult<byte[]>( ex.Message );
            }
        }

        /// <summary>
        /// 从返回的modbus的书内容中，提取出真实的数据，适用于写入和读取操作
        /// </summary>
        /// <param name="response">返回的核心modbus报文信息</param>
        /// <returns>结果数据内容</returns>
        public static OperateResult<byte[]> ExtractActualData( byte[] response )
        {
            try
            {
                if (response[1] >= 0x80)
                    return new OperateResult<byte[]>( ModbusInfo.GetDescriptionByErrorCode( response[2] ) );
                else if (response.Length > 3)
                    return OperateResult.CreateSuccessResult( SoftBasic.BytesArrayRemoveBegin( response, 3 ) );
                else
                    return OperateResult.CreateSuccessResult( new byte[0] );
            }
            catch(Exception ex)
            {
                return new OperateResult<byte[]>( ex.Message );
            }
        }

        /// <summary>
        /// 将modbus指令打包成Modbus-Tcp指令
        /// </summary>
        /// <param name="value">Modbus指令</param>
        /// <param name="id">消息的序号</param>
        /// <returns>Modbus-Tcp指令</returns>
        public static byte[] PackCommandToTcp( byte[] value, ushort id )
        {
            byte[] buffer = new byte[value.Length + 6];
            buffer[0] = BitConverter.GetBytes( id )[1];
            buffer[1] = BitConverter.GetBytes( id )[0];

            buffer[4] = BitConverter.GetBytes( value.Length )[1];
            buffer[5] = BitConverter.GetBytes( value.Length )[0];

            value.CopyTo( buffer, 6 );
            return buffer;
        }

        /// <summary>
        /// 将modbus-tcp的数据重新还原成modbus数据
        /// </summary>
        /// <param name="value">modbus-tcp的报文</param>
        /// <returns>modbus数据报文</returns>
        public static byte[] ExplodeTcpCommandToCore( byte[] value )
        {
            return SoftBasic.BytesArrayRemoveBegin( value, 6 );
        }

        /// <summary>
        /// 将modbus-rtu的数据重新还原成modbus数据
        /// </summary>
        /// <param name="value">modbus-rtu的报文</param>
        /// <returns>modbus数据报文</returns>
        public static byte[] ExplodeRtuCommandToCore( byte[] value )
        {
            return SoftBasic.BytesArrayRemoveLast( value, 2 );
        }

        /// <summary>
        /// 将modbus指令打包成Modbus-Rtu指令
        /// </summary>
        /// <param name="value">Modbus指令</param>
        /// <returns>Modbus-Rtu指令</returns>
        public static byte[] PackCommandToRtu( byte[] value )
        {
            return SoftCRC16.CRC16( value );
        }

        /// <summary>
        /// 将一个modbus-rtu的数据报文，转换成modbus-ascii的数据报文
        /// </summary>
        /// <param name="value">modbus-rtu的完整报文，携带相关的校验码</param>
        /// <returns>可以用于直接发送的modbus-ascii的报文</returns>
        public static byte[] TransRtuToAsciiPackCommand( byte[] value )
        {
            // remove crc check
            byte[] modbus = SoftBasic.BytesArrayRemoveLast( value, 2 );

            // add LRC check
            byte[] modbus_lrc = SoftLRC.LRC( modbus );

            // Translate to ascii information
            byte[] modbus_ascii = SoftBasic.BytesToAsciiBytes( modbus_lrc );

            // add head and end informarion
            return SoftBasic.SpliceTwoByteArray( SoftBasic.SpliceTwoByteArray( new byte[] { 0x3A }, modbus_ascii ), new byte[] { 0x0D, 0x0A } );
        }

        /// <summary>
        /// 将一个modbus-ascii的数据报文，转换成的modbus核心数据报文
        /// </summary>
        /// <param name="value">modbus-ascii的完整报文，携带相关的校验码</param>
        /// <returns>可以用于直接发送的modbus的报文</returns>
        public static OperateResult<byte[]> TransAsciiPackCommandToRtu( byte[] value )
        {
            try
            {
                // response check
                if (value[0] != 0x3A || value[value.Length - 2] != 0x0D || value[value.Length - 1] != 0x0A)
                    return new OperateResult<byte[]>( ) { Message = StringResources.Language.ModbusAsciiFormatCheckFailed + SoftBasic.ByteToHexString( value ) };

                // remove head and end
                byte[] modbus_ascii = SoftBasic.BytesArrayRemoveDouble( value, 1, 2 );

                // get modbus core
                byte[] modbus_core = SoftBasic.AsciiBytesToBytes( modbus_ascii );

                if (!Serial.SoftLRC.CheckLRC( modbus_core ))
                    return new OperateResult<byte[]>( ) { Message = StringResources.Language.ModbusLRCCheckFailed + SoftBasic.ByteToHexString( modbus_core ) };

                // remove the last info
                return OperateResult.CreateSuccessResult( SoftBasic.BytesArrayRemoveLast( modbus_core, 1 ) );
            }
            catch(Exception ex)
            {
                return new OperateResult<byte[]>( ) { Message = ex.Message + SoftBasic.ByteToHexString( value ) };
            }
        }

        /// <summary>
        /// 分析Modbus协议的地址信息，该地址适应于tcp及rtu模式
        /// </summary>
        /// <param name="address">带格式的地址，比如"100"，"x=4;100"，"s=1;100","s=1;x=4;100"</param>
        /// <param name="defaultStation"></param>
        /// <param name="isStartWithZero">起始地址是否从0开始</param>
        /// <param name="defaultFunction">默认的功能码信息</param>
        /// <returns>转换后的地址信息</returns>
        public static OperateResult<ModbusAddress> AnalysisAddress( string address, byte defaultStation, bool isStartWithZero, byte defaultFunction )
        {
            try
            {
                ModbusAddress mAddress = new ModbusAddress( address, defaultStation, defaultFunction );
                if (!isStartWithZero)
                {
                    if (mAddress.Address < 1) throw new Exception( StringResources.Language.ModbusAddressMustMoreThanOne );
                    mAddress.Address = (ushort)(mAddress.Address - 1);
                }
                return OperateResult.CreateSuccessResult( mAddress );
            }
            catch (Exception ex)
            {
                return new OperateResult<ModbusAddress>( ) { Message = ex.Message };
            }
        }

        /// <summary>
        /// 通过错误码来获取到对应的文本消息
        /// </summary>
        /// <param name="code">错误码</param>
        /// <returns>错误的文本描述</returns>
        public static string GetDescriptionByErrorCode( byte code )
        {
            switch (code)
            {
                case ModbusInfo.FunctionCodeNotSupport:               return StringResources.Language.ModbusTcpFunctionCodeNotSupport;
                case ModbusInfo.FunctionCodeOverBound:                return StringResources.Language.ModbusTcpFunctionCodeOverBound;
                case ModbusInfo.FunctionCodeQuantityOver:             return StringResources.Language.ModbusTcpFunctionCodeQuantityOver;
                case ModbusInfo.FunctionCodeReadWriteException:       return StringResources.Language.ModbusTcpFunctionCodeReadWriteException;
                default:                                              return StringResources.Language.UnknownError;
            }
        }

        #endregion
    }
}
