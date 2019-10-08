using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HslCommunication.Core.Net;
using HslCommunication.Core;
using HslCommunication;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using HslCommunication.BasicFramework;

namespace HslCommunication.MQTT
{
    /// <summary>
    /// Mqtt协议的客户端实现
    /// </summary>
    public class MqttClient : NetworkXBase
    {
        #region Constructor

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        /// <param name="options">配置信息</param>
        public MqttClient( MqttConnectionOptions options )
        {
            this.connectionOptions               = options;
            buffer                               = new byte[1];
            incrementCount                       = new SoftIncrementCount( ushort.MaxValue, 1 );
            listLock                             = new object( );
            publishMessages                      = new List<MqttPublishMessage>( );
            subcribeTopics                       = new List<string>( );
            connectEvent                         = new ManualResetEvent( false );
            activeTime                           = DateTime.Now;
            subcribeLock                         = new SimpleHybirdLock( );
        }

        #endregion

        #region Connect DisConnect

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <returns>连接是否成功</returns>
        public OperateResult ConnectServer( )
        {
            if (this.connectionOptions == null) return new OperateResult( "Optines is null" );

            // 开启连接
            CoreSocket?.Close( );
            OperateResult<Socket> connect = CreateSocketAndConnect( this.connectionOptions.IpAddress, this.connectionOptions.Port, this.connectionOptions.ConnectTimeout );
            if (!connect.IsSuccess) return connect;

            CoreSocket = connect.Content;

            OperateResult<byte[]> command = BuildConnectMqttCommand( this.connectionOptions );
            if (!command.IsSuccess) return command;

            //string hex = SoftBasic.ByteToHexString( command.Content, ' ' );

            // 发送连接的报文信息
            OperateResult send = Send( CoreSocket, command.Content );
            if (!send.IsSuccess) return send;

            try
            {
                CoreSocket.BeginReceive( buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback( ReceiveAsyncCallback ), CoreSocket );
            }
            catch (Exception ex)
            {
                return new OperateResult( ex.Message );
            }

            if (!connectEvent.WaitOne( this.connectionOptions.ConnectTimeout ))
                return new OperateResult( StringResources.Language.ConnectedFailed );
            if (connectStatus > 0)
            {
                return new OperateResult( connectStatus, (connectStatus == 5 ? "UserName or Password is wrong!" : StringResources.Language.UnknownError) ); 
            }

            // 检查是否有订阅的
            StartSubscribeExsistMessage( );

            // 重置消息计数
            this.incrementCount.ResetCurrentValue( );

            // 重置关闭状态
            this.closed = false;

            // 开启心跳检测
            this.timerCheck?.Dispose( );
            this.activeTime = DateTime.Now;
            if ((int)this.connectionOptions.KeepAliveSendInterval.TotalMilliseconds > 0)
            {
                timerCheck = new Timer( new TimerCallback( TimerCheckServer ), null, 2000, (int)this.connectionOptions.KeepAliveSendInterval.TotalMilliseconds );
            }
            return OperateResult.CreateSuccessResult( );
        }

        /// <summary>
        /// 关闭Mqtt服务器的连接。
        /// </summary>
        public void ConnectClose( )
        {
            OperateResult<byte[]> command = BuildMqttCommand( MqttControlMessage.DISCONNECT, 0x00, null, null );
            if (command.IsSuccess) Send( CoreSocket, command.Content );
            closed = true;
            timerCheck?.Dispose( );
            CoreSocket?.Close( );
        }

        #endregion

        #region Publish Message

        /// <summary>
        /// 发布一个消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>发布结果</returns>
        public OperateResult PublishMessage( MqttApplicationMessage message )
        {
            MqttPublishMessage publishMessage = new MqttPublishMessage( )
            {
                Identifier = message.QualityOfServiceLevel == MqttQualityOfServiceLevel.AtMostOnce ? 0 : (int)incrementCount.GetCurrentValue( ),
                Message = message,
            };

            OperateResult<byte[]> command = BuildPublishMqttCommand( publishMessage );
            if (!command.IsSuccess) return command;

            if(message.QualityOfServiceLevel == MqttQualityOfServiceLevel.AtMostOnce)
            {
                return Send( CoreSocket, command.Content );
            }
            else
            {
                AddPublishMessage( publishMessage );
                return Send( CoreSocket, command.Content );
            }
        }

        #endregion

        #region Subscribe Message

        /// <summary>
        /// 订阅一个主题信息
        /// </summary>
        /// <param name="topic">主题信息</param>
        /// <returns>订阅结果</returns>
        public OperateResult SubscribeMessage(string topic )
        {
            return SubscribeMessage( new string[] { topic } );
        }

        /// <summary>
        /// 订阅多个主题信息
        /// </summary>
        /// <param name="topics">主题信息</param>
        /// <returns>订阅结果</returns>
        public OperateResult SubscribeMessage( string[] topics )
        {
            if (topics == null) return OperateResult.CreateSuccessResult( );
            if (topics.Length == 0) return OperateResult.CreateSuccessResult( );

            MqttSubcribeMessage subcribeMessage = new MqttSubcribeMessage( )
            {
                Identifier = (int)incrementCount.GetCurrentValue( ),
                Topics = topics,
            };

            OperateResult<byte[]> command = BuildSubscribeMqttCommand( subcribeMessage );
            AddSubTopics( topics );
            return Send( CoreSocket, command.Content );
        }

        private void AddSubTopics( string[] topics )
        {
            subcribeLock.Enter( );

            for (int i = 0; i < topics.Length; i++)
            {
                if (!subcribeTopics.Contains( topics[i] ))
                {
                    subcribeTopics.Add( topics[i] );
                }
            }

            subcribeLock.Leave( );
        }


        /// <summary>
        /// 取消订阅多个主题信息
        /// </summary>
        /// <param name="topics">主题信息</param>
        /// <returns>取消订阅结果</returns>
        public OperateResult UnSubscribeMessage( string[] topics )
        {
            MqttSubcribeMessage subcribeMessage = new MqttSubcribeMessage( )
            {
                Identifier = (int)incrementCount.GetCurrentValue( ),
                Topics = topics,
            };

            OperateResult<byte[]> command = BuildUnSubscribeMqttCommand( subcribeMessage );
            RemoveSubTopics( topics );
            return Send( CoreSocket, command.Content );
        }

        /// <summary>
        /// 取消订阅置顶的主题信息
        /// </summary>
        /// <param name="topic">主题信息</param>
        /// <returns>取消订阅结果</returns>
        public OperateResult UnSubscribeMessage( string topic )
        {
            return UnSubscribeMessage( new string[] { topic } );
        }

        private void RemoveSubTopics( string[] topics )
        {
            subcribeLock.Enter( );

            for (int i = 0; i < topics.Length; i++)
            {
                if (subcribeTopics.Contains( topics[i] ))
                {
                    subcribeTopics.Remove( topics[i] );
                }
            }

            subcribeLock.Leave( );
        }

        private void StartSubscribeExsistMessage(  )
        {
            subcribeLock.Enter( );
            string[] topics = subcribeTopics.ToArray( );
            subcribeLock.Leave( );

            SubscribeMessage( topics );
        }

        #endregion

        #region Private Method

        private void ReceiveAsyncCallback(IAsyncResult ar )
        {
            if (ar.AsyncState is Socket socket)
            {
                try
                {
                    int receiveCount = socket.EndReceive( ar );
                    if (receiveCount != 1)
                    {
                        Thread.Sleep( 100 );
                        LogNet?.WriteDebug( $"Receive {receiveCount}" );
                        socket?.Close( );
                        //socket.BeginReceive( buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback( ReceiveAsyncCallback ), socket );
                        return;
                    }

                    OperateResult<int> receLength = ReceiveMqttNextLength( socket, Receive );
                    if (!receLength.IsSuccess)
                    {
                        ThreadPool.QueueUserWorkItem( new WaitCallback( BeginReconnectServer ), null );
                        LogNet?.WriteDebug( $"Receive Length Failed:{receLength.Message}" );
                        return;
                    }

                    OperateResult<byte[]> data = Receive( socket, receLength.Content );
                    if (!data.IsSuccess)
                    {
                        ThreadPool.QueueUserWorkItem( new WaitCallback( BeginReconnectServer ), null );
                        LogNet?.WriteDebug( $"Receive Data Failed:{data.Message}" );
                        return;
                    }

                    byte mqttCode = buffer[0];

                    if (mqttCode >> 4 == MqttControlMessage.CONNACK)
                    {
                        // 连接确认
                        connectStatus = data.Content[0] * 256 + data.Content[1];
                        connectEvent.Set( );
                        LogNet?.WriteDebug( $"Code[{mqttCode.ToString( "X2" )}] Connect Ack: {SoftBasic.ByteToHexString( data.Content, ' ' )}" );
                        if (connectStatus > 0)
                        {
                            socket?.Close( );
                            return;
                        }

                    }

                    socket.BeginReceive( buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback( ReceiveAsyncCallback ), socket );
                    if (mqttCode >> 4 == MqttControlMessage.CONNACK)
                    {

                    }
                    else if (mqttCode >> 4 == MqttControlMessage.PUBACK)
                    {
                        // Qos1的发布确认
                        LogNet?.WriteDebug( $"Code[{mqttCode.ToString( "X2" )}] Publish Ack: {SoftBasic.ByteToHexString( data.Content, ' ' )}" );
                    }
                    else if (mqttCode >> 4 == MqttControlMessage.PUBREC)
                    {
                        // Qos2的发布收到
                        Send( socket, BuildMqttCommand( MqttControlMessage.PUBREL, 0x02, data.Content, new byte[0] ).Content );
                        LogNet?.WriteDebug( $"Code[{mqttCode.ToString( "X2" )}] Publish Rec: {SoftBasic.ByteToHexString( data.Content, ' ' )}" );
                    }
                    else if (mqttCode >> 4 == MqttControlMessage.PUBCOMP)
                    {
                        // Qos2的发布收到
                        LogNet?.WriteDebug( $"Code[{mqttCode.ToString( "X2" )}] Publish Complete: {SoftBasic.ByteToHexString( data.Content, ' ' )}" );
                    }
                    else if (mqttCode >> 4 == MqttControlMessage.PINGRESP)
                    {
                        // 心跳响应
                        activeTime = DateTime.Now;
                        LogNet?.WriteDebug( $"Heart Code Check!" );
                    }
                    else if (mqttCode >> 4 == MqttControlMessage.PUBLISH)
                    {
                        // 订阅反馈
                        activeTime = DateTime.Now;
                        if (data.Content.Length < 2) return;

                        int topicLength = data.Content[0] * 256 + data.Content[1];
                        if (data.Content.Length < 2 + topicLength)
                        {
                            LogNet?.WriteDebug( $"Code[{mqttCode.ToString( "X2" )}] Subscribe Error: {SoftBasic.ByteToHexString( data.Content, ' ' )}" );
                            return;
                        }

                        string topic = topicLength > 0 ? Encoding.UTF8.GetString( data.Content, 2, topicLength ) : string.Empty;
                        byte[] payload = new byte[data.Content.Length - topicLength - 2];
                        Array.Copy( data.Content, topicLength + 2, payload, 0, payload.Length );

                        LogNet?.WriteDebug( $"Code[{mqttCode.ToString( "X2" )}] Subscribe[{topic}] {SoftBasic.ByteToHexString( payload, ' ' )}" );
                        OnMqttMessageReceived?.Invoke( topic, payload );
                    }
                    else if (mqttCode >> 4 == MqttControlMessage.SUBACK)
                    {
                        LogNet?.WriteDebug( $"Code[{mqttCode.ToString( "X2" )}] Subscribe Ack: {SoftBasic.ByteToHexString( data.Content, ' ' )}" );
                    }
                    else if (mqttCode >> 4 == MqttControlMessage.UNSUBACK)
                    {
                        LogNet?.WriteDebug( $"Code[{mqttCode.ToString( "X2" )}] UnSubscribe Ack: {SoftBasic.ByteToHexString( data.Content, ' ' )}" );
                    }
                    else
                    {
                        LogNet?.WriteDebug( $"Code[{mqttCode.ToString( "X2" )}] {SoftBasic.ByteToHexString( data.Content, ' ' )}" );
                    }
                }
                catch (ObjectDisposedException)
                {
                    ThreadPool.QueueUserWorkItem( new WaitCallback( BeginReconnectServer ), null );
                }
                catch (Exception ex)
                {
                    LogNet?.WriteException( "ReceiveAsyncCallback", ex );
                    ThreadPool.QueueUserWorkItem( new WaitCallback( BeginReconnectServer ), null );
                }
            }
        }

        private void BeginReconnectServer( object obj )
        {
            if (closed) return;

            if (Interlocked.CompareExchange( ref isReConnectMqtt, 1, 0 ) == 0)
            {
                connectStatus = -1;
                LogNet?.WriteDebug( "Server is lost, try re-connect to server!" );
                for (int i = 0; i < 10; i++)
                {
                    if (closed) return;
                    LogNet?.WriteDebug( $"Waite for {9 - i} seconds to connect server!" );
                    Thread.Sleep( 1000 );
                }

                if (closed) return;
                OperateResult connect = ConnectServer( );
                if (!connect.IsSuccess)
                {
                    Interlocked.Exchange( ref isReConnectMqtt, 0 );
                    ThreadPool.QueueUserWorkItem( new WaitCallback( BeginReconnectServer ), null );
                }
                else
                {
                    LogNet?.WriteDebug( $"re-connect server success!" );
                    Interlocked.Exchange( ref isReConnectMqtt, 0 );
                }
            }
        }

        private void TimerCheckServer( object obj )
        {
            if (CoreSocket != null)
            {
                if((DateTime.Now - activeTime).TotalSeconds > this.connectionOptions.KeepAliveSendInterval.TotalSeconds * 3)
                {
                    // 3个心跳周期没有接收到数据
                    ThreadPool.QueueUserWorkItem( new WaitCallback( BeginReconnectServer ), null );
                }
                else
                {
                    if (!Send( CoreSocket, BuildMqttCommand( MqttControlMessage.PINGREQ, 0x00, new byte[0], new byte[0] ).Content ).IsSuccess)
                        ThreadPool.QueueUserWorkItem( new WaitCallback( BeginReconnectServer ), null );
                }
            }
        }

        #endregion

        #region Publish Message

        private void AddPublishMessage( MqttPublishMessage publishMessage )
        {
            lock (listLock)
            {
                //publishMessages.Add( publishMessage );
            }
        }

        #endregion

        #region Event Handle

        /// <summary>
        /// 接收到Mqtt协议的委托
        /// </summary>
        /// <param name="topic">主题信息</param>
        /// <param name="payload">负载数据</param>
        public delegate void MqttMessageReceive( string topic, byte[] payload );

        /// <summary>
        /// 当接收到Mqtt订阅的信息的时候触发
        /// </summary>
        public event MqttMessageReceive OnMqttMessageReceived;

        #endregion

        #region Private Member

        private DateTime activeTime;                                          // 激活时间
        private int isReConnectMqtt = 0;                                      // 是否重连服务器中
        private int connectStatus = -1;
        private ManualResetEvent connectEvent;                                // 连接的方法的处理
        private List<MqttPublishMessage> publishMessages;                     // 缓存的等待处理的Qos大于0的消息
        private object listLock;                                              // 缓存的处理的消息的队列
        private List<string> subcribeTopics;                                  // 缓存的等待处理的订阅的消息内容
        private SimpleHybirdLock subcribeLock;                                // 订阅的主题的队列锁
        private SoftIncrementCount incrementCount;                            // 自增的数据id对象
        private bool closed = false;                                          // 客户端是否关闭
        private MqttConnectionOptions connectionOptions;                      // 连接服务器时的配置信息
        private byte[] buffer;                                                // 缓存的消息
        private Timer timerCheck;

        #endregion

        #region Static Helper Method

        /// <summary>
        /// 根据数据的总长度，计算出剩余的数据长度信息
        /// </summary>
        /// <param name="length">数据的总长度</param>
        /// <returns>计算结果</returns>
        public static OperateResult<byte[]> CalculateLengthToMqttLength( int length )
        {
            if (length > 268_435_455) return new OperateResult<byte[]>( StringResources.Language.MQTTDataTooLong );

            if (length < 128) return OperateResult.CreateSuccessResult( new byte[1] { (byte)length } );

            if (length < 128 * 128)
            {
                byte[] buffer = new byte[2];
                buffer[0] = (byte)(length / 128 + 0x80);
                buffer[1] = (byte)(length % 128);
                return OperateResult.CreateSuccessResult( buffer );
            }

            if (length < 128 * 128 * 128)
            {
                byte[] buffer = new byte[3];
                buffer[0] = (byte)(length / 128 / 128 + 0x80);
                buffer[1] = (byte)(length / 128 + 0x80);
                buffer[2] = (byte)(length % 128);
                return OperateResult.CreateSuccessResult( buffer );
            }
            else
            {
                byte[] buffer = new byte[4];
                buffer[0] = (byte)(length / 128 / 128 / 128 + 0x80);
                buffer[1] = (byte)(length / 128 / 128 + 0x80);
                buffer[2] = (byte)(length / 128 + 0x80);
                buffer[3] = (byte)(length % 128);
                return OperateResult.CreateSuccessResult( buffer );
            }
        }

        /// <summary>
        /// 从网络套接字中接收剩余的数据长度
        /// </summary>
        /// <param name="socket">网络套接字</param>
        /// <param name="receiveBytes">接收数据的基础方法</param>
        /// <returns>结果数据</returns>
        public static  OperateResult<int> ReceiveMqttNextLength( Socket socket, Func<Socket,int, OperateResult<byte[]>> receiveBytes )
        {
            List<byte> buffer = new List<byte>( );
            while (true)
            {
                OperateResult<byte[]> rece = receiveBytes( socket, 1 );
                if (!rece.IsSuccess) return OperateResult.CreateFailedResult<int>( rece );

                buffer.Add( rece.Content[0] );
                if (rece.Content[0] < 0x80) break;
            }

            if (buffer.Count > 4) return new OperateResult<int>( "Receive Length is too long!" );
            if (buffer.Count == 1) return OperateResult.CreateSuccessResult( (int)buffer[0] );
            if (buffer.Count == 2) return OperateResult.CreateSuccessResult( (buffer[0] - 128) * 128 + buffer[1] );
            if (buffer.Count == 3) return OperateResult.CreateSuccessResult( (buffer[0] - 128) * 128 * 128 + (buffer[1] - 128) * 128 + buffer[2] );
            return OperateResult.CreateSuccessResult( (buffer[0] - 128) * 128 * 128 * 128 + (buffer[1] - 128) * 128 * 128 + (buffer[2] - 128) * 128 + buffer[3] );
        }

        /// <summary>
        /// 将一个数据打包成一个mqtt协议的内容
        /// </summary>
        /// <param name="control">控制码</param>
        /// <param name="flags">标记</param>
        /// <param name="variableHeader">可变头的字节内容</param>
        /// <param name="payLoad">负载数据</param>
        /// <returns>带有是否成功的结果对象</returns>
        public static OperateResult<byte[]> BuildMqttCommand( byte control, byte flags, byte[] variableHeader, byte[] payLoad )
        {
            if (variableHeader == null) variableHeader = new byte[0];
            if (payLoad == null) payLoad = new byte[0];

            control = (byte)(control << 4);
            byte head = (byte)(control | flags);

            // 先计算长度
            OperateResult<byte[]> bufferLength = CalculateLengthToMqttLength( variableHeader.Length + payLoad.Length );
            if (!bufferLength.IsSuccess) return bufferLength;

            MemoryStream ms = new MemoryStream( );
            ms.WriteByte( head );
            ms.Write( bufferLength.Content, 0, bufferLength.Content.Length );
            if (variableHeader.Length > 0) ms.Write( variableHeader, 0, variableHeader.Length );
            if (payLoad.Length > 0) ms.Write( payLoad, 0, payLoad.Length );
            byte[] result = ms.ToArray( );
            ms.Dispose( );
            return OperateResult.CreateSuccessResult( result );
        }

        /// <summary>
        /// 将字符串打包成utf8编码，并且带有2个字节的表示长度的信息
        /// </summary>
        /// <param name="message">文本消息</param>
        /// <returns>打包之后的信息</returns>
        public static byte[] BuildSegCommandByString(string message )
        {
            byte[] buffer = string.IsNullOrEmpty( message ) ? new byte[0] : Encoding.UTF8.GetBytes( message );
            byte[] result = new byte[buffer.Length + 2];
            buffer.CopyTo( result, 2 );
            result[0] = (byte)(buffer.Length / 256);
            result[1] = (byte)(buffer.Length % 256);
            return result;
        }

        /// <summary>
        /// 从MQTT的缓存信息里，提取文本信息
        /// </summary>
        /// <param name="buffer">Mqtt的报文</param>
        /// <param name="index">索引</param>
        /// <returns>值</returns>
        public static string ExtraMsgFromBytes( byte[] buffer, ref int index )
        {
            int indexTmp = index;
            int length = buffer[index] * 256 + buffer[index + 1];
            index = index + 2 + length;
            return Encoding.UTF8.GetString( buffer, indexTmp + 2, length );
        }

        /// <summary>
        /// 从MQTT的缓存信息里，提取长度信息
        /// </summary>
        /// <param name="buffer">Mqtt的报文</param>
        /// <param name="index">索引</param>
        /// <returns>值</returns>
        public static int ExtraIntFromBytes( byte[] buffer, ref int index )
        {
            int length = buffer[index] * 256 + buffer[index + 1];
            index += 2;
            return length;
        }

        /// <summary>
        /// 从MQTT的缓存信息里，提取长度信息
        /// </summary>
        /// <param name="data">数据信息</param>
        /// <returns>值</returns>
        public static byte[] BuildIntBytes( int data )
        {
            return new byte[] { BitConverter.GetBytes( data )[1], BitConverter.GetBytes( data )[0] };
        }

        /// <summary>
        /// 创建MQTT连接服务器的报文信息
        /// </summary>
        /// <param name="connectionOptions">连接配置</param>
        /// <returns>返回是否成功的信息</returns>
        public static OperateResult<byte[]> BuildConnectMqttCommand( MqttConnectionOptions connectionOptions )
        {
            List<byte> variableHeader = new List<byte>( );
            variableHeader.AddRange( new byte[] { 0x00, 0x04, 0x4d, 0x51, 0x54, 0x54 } );                         // MQTT
            variableHeader.Add( 0x04 );                                                                           // 协议版本，3.1.1
            byte connectFlags = 0x00;
            if (connectionOptions.Credentials != null)                                                            // 是否需要验证用户名和密码
            {
                connectFlags = (byte)(connectFlags | 0x80);
                connectFlags = (byte)(connectFlags | 0x40);
            }
            if (connectionOptions.CleanSession)
            {
                connectFlags = (byte)(connectFlags | 0x02);
            }
            variableHeader.Add( connectFlags );
            if (connectionOptions.KeepAlivePeriod.TotalSeconds < 1) connectionOptions.KeepAlivePeriod = TimeSpan.FromSeconds( 1 );
            byte[] keepAlivePeriod = BitConverter.GetBytes( (int)connectionOptions.KeepAlivePeriod.TotalSeconds );
            variableHeader.Add( keepAlivePeriod[1] );
            variableHeader.Add( keepAlivePeriod[0] );


            List<byte> payLoad = new List<byte>( );
            payLoad.AddRange( BuildSegCommandByString( connectionOptions.ClientId ) );                         // 添加客户端的id信息

            if (connectionOptions.Credentials != null)                                                         // 根据需要选择是否添加用户名和密码
            {
                payLoad.AddRange( BuildSegCommandByString( connectionOptions.Credentials.UserName ) );
                payLoad.AddRange( BuildSegCommandByString( connectionOptions.Credentials.Password ) );
            }

            return BuildMqttCommand( MqttControlMessage.CONNECT, 0x00, variableHeader.ToArray( ), payLoad.ToArray( ) );
        }

        /// <summary>
        /// 创建Mqtt发送消息的命令
        /// </summary>
        /// <param name="message">封装后的消息内容</param>
        /// <returns>结果内容</returns>
        public static OperateResult<byte[]> BuildPublishMqttCommand( MqttPublishMessage message )
        {
            byte flag = 0x00;
            if (!message.IsSendFirstTime) flag = (byte)(flag | 0x08);
            if (message.Message.Retain) flag = (byte)(flag | 0x01);
            if (message.Message.QualityOfServiceLevel == MqttQualityOfServiceLevel.AtLeastOnce)
            {
                flag = (byte)(flag | 0x02);
            }
            else if (message.Message.QualityOfServiceLevel == MqttQualityOfServiceLevel.ExactlyOnce)
            {
                flag = (byte)(flag | 0x04);
            }


            List<byte> variableHeader = new List<byte>( );
            variableHeader.AddRange( BuildSegCommandByString( message.Message.Topic ) );
            if(message.Message.QualityOfServiceLevel != MqttQualityOfServiceLevel.AtMostOnce)
            {
                variableHeader.Add( BitConverter.GetBytes( message.Identifier )[1] );
                variableHeader.Add( BitConverter.GetBytes( message.Identifier )[0] );
            }

            return BuildMqttCommand( MqttControlMessage.PUBLISH, flag, variableHeader.ToArray( ), message.Message.Payload );
        }

        /// <summary>
        /// 创建Mqtt发送消息的命令
        /// </summary>
        /// <param name="topic">主题消息内容</param>
        /// <param name="payload">数据负载</param>
        /// <returns>结果内容</returns>
        public static OperateResult<byte[]> BuildPublishMqttCommand( string topic, byte[] payload )
        {
            List<byte> variableHeader = new List<byte>( );
            variableHeader.AddRange( BuildSegCommandByString( topic ) );

            return BuildMqttCommand( MqttControlMessage.PUBLISH, 0x00, variableHeader.ToArray( ), payload );
        }

        /// <summary>
        /// 创建Mqtt订阅消息的命令
        /// </summary>
        /// <param name="message">订阅的主题</param>
        /// <returns>结果内容</returns>
        public static OperateResult<byte[]> BuildSubscribeMqttCommand( MqttSubcribeMessage message )
        {
            List<byte> variableHeader = new List<byte>( );
            List<byte> payLoad = new List<byte>( );

            variableHeader.Add( BitConverter.GetBytes( message.Identifier )[1] );
            variableHeader.Add( BitConverter.GetBytes( message.Identifier )[0] );

            for (int i = 0; i < message.Topics.Length; i++)
            {
                payLoad.AddRange( BuildSegCommandByString( message.Topics[i] ) );

                if (message.QualityOfServiceLevel == MqttQualityOfServiceLevel.AtMostOnce)
                    payLoad.AddRange( new byte[] { 0x00 } );
                else if (message.QualityOfServiceLevel == MqttQualityOfServiceLevel.AtLeastOnce)
                    payLoad.AddRange( new byte[] { 0x01 } );
                else
                    payLoad.AddRange( new byte[] { 0x02 } );
            }

            return BuildMqttCommand( MqttControlMessage.SUBSCRIBE, 0x02, variableHeader.ToArray( ), payLoad.ToArray( ) );
        }

        /// <summary>
        /// 创建Mqtt取消订阅消息的命令
        /// </summary>
        /// <param name="message">订阅的主题</param>
        /// <returns>结果内容</returns>
        public static OperateResult<byte[]> BuildUnSubscribeMqttCommand( MqttSubcribeMessage message )
        {
            List<byte> variableHeader = new List<byte>( );
            List<byte> payLoad = new List<byte>( );

            variableHeader.Add( BitConverter.GetBytes( message.Identifier )[1] );
            variableHeader.Add( BitConverter.GetBytes( message.Identifier )[0] );

            for (int i = 0; i < message.Topics.Length; i++)
                payLoad.AddRange( BuildSegCommandByString( message.Topics[i] ) );

            return BuildMqttCommand( MqttControlMessage.UNSUBSCRIBE, 0x02, variableHeader.ToArray( ), payLoad.ToArray( ) );
        }

        #endregion
    }
}
