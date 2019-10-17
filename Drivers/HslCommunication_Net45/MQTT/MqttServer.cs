using HslCommunication.BasicFramework;
using HslCommunication.Core.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using HslCommunication.Core;

namespace HslCommunication.MQTT
{
    /// <summary>
    /// 一个Mqtt的服务器类对象
    /// </summary>
    public class MqttServer : NetworkServerBase
    {
        #region Constructor

        /// <summary>
        /// 实例化一个mc协议的服务器
        /// </summary>
        public MqttServer( )
        {
            retainKeys = new Dictionary<string, byte[]>( );
            keysLock = new SimpleHybirdLock( );
        }

        #endregion

        #region NetServer Override

        /// <summary>
        /// 当线程登录的时候触发的方法
        /// </summary>
        /// <param name="socket">套接字的内容</param>
        /// <param name="endPoint">对方的远程节点</param>
        protected override void ThreadPoolLogin( Socket socket, IPEndPoint endPoint )
        {
            // 先接收客户端的信息，对于不符合要求的客户端进行过滤
            OperateResult<byte[]> receive = Receive( socket, 1 );
            if (!receive.IsSuccess) return;

            if (receive.Content[0] >> 4 != MqttControlMessage.CONNECT)
            {
                LogNet?.WriteInfo( "Client Send Faied, And Close!" );
                socket?.Close( );
                return;
            }
            OperateResult<int> receLength = MqttClient.ReceiveMqttNextLength( socket, Receive );
            if (!receLength.IsSuccess)
            {
                LogNet?.WriteDebug( $"Receive Length Failed:{receLength.Message}" );
                return;
            }

            OperateResult<byte[]> data = Receive( socket, receLength.Content );
            if (!data.IsSuccess)
            {
                LogNet?.WriteDebug( $"Receive Data Failed:{data.Message}" );
                return;
            }

            // 提取登录信息
            if (data.Content.Length < 10)
            {
                socket?.Close( );
                LogNet?.WriteDebug( $"Receive Data Too Short:{data.Content}" );
                return;
            }

            if (Encoding.ASCII.GetString( data.Content, 2, 4 ) != "MQTT")
            {
                socket?.Close( );
                LogNet?.WriteDebug( $"Not Mqtt Client Connection" );
                return;
            }

            try
            {
                int index = 10;
                string clientId = MqttClient.ExtraMsgFromBytes( data.Content, ref index );
                string willTopic = ((data.Content[7] & 0x04) == 0x04) ? MqttClient.ExtraMsgFromBytes( data.Content, ref index ) : string.Empty;
                string willMessage = ((data.Content[7] & 0x04) == 0x04) ? MqttClient.ExtraMsgFromBytes( data.Content, ref index ) : string.Empty;
                string userName = ((data.Content[7] & 0x80) == 0x80) ? MqttClient.ExtraMsgFromBytes( data.Content, ref index ) : string.Empty;
                string password = ((data.Content[7] & 0x40) == 0x40) ? MqttClient.ExtraMsgFromBytes( data.Content, ref index ) : string.Empty;
                int keepAlive = data.Content[8] * 256 + data.Content[9];

                int returnCode = ClientVerification != null ? ClientVerification( clientId, userName, password ) : 0;
                if (returnCode != 0)
                {
                    socket?.Send( MqttClient.BuildMqttCommand( MqttControlMessage.CONNACK, 0x00, null, MqttClient.BuildIntBytes( returnCode ) ).Content );

                    LogNet?.WriteDebug( $"Client verificated failed:{returnCode}" );
                    System.Threading.Thread.Sleep( 20 );
                    socket?.Close( );
                    return;
                }

                socket?.Send( MqttClient.BuildMqttCommand( MqttControlMessage.CONNACK, 0x00, null, new byte[] { 0x00, 0x00 } ).Content );
                MqttSession mqttSession = new MqttSession( );
                mqttSession.MqttSocket = socket;
                mqttSession.ClientId = clientId;
                mqttSession.UserName = userName;
                if(keepAlive > 0) mqttSession.ActiveTimeSpan = TimeSpan.FromSeconds( keepAlive );

                socket.BeginReceive( mqttSession.ByteHead, 0, 1, SocketFlags.None, new AsyncCallback( SocketReceiveCallback ), mqttSession );
                AddMqttSession( mqttSession );
            }
            catch (Exception ex)
            {
                LogNet?.WriteDebug( $"Client Online Exception : " + ex.Message );
            }
        }

        private void SocketReceiveCallback( IAsyncResult ar )
        {
            if (ar.AsyncState is MqttSession mqttSession)
            {
                try
                {
                    int length = mqttSession.MqttSocket.EndReceive( ar );
                    if (length == 0)
                    {
                        RemoveAndCloseSession( mqttSession ); return;
                    }
                }
                catch (ObjectDisposedException)
                {
                    RemoveAndCloseSession( mqttSession ); return;
                }
                catch(Exception ex)
                {
                    LogNet?.WriteFatal( "Mqtt Client Exception: " + ex.Message );
                    RemoveAndCloseSession( mqttSession ); return;
                }

                byte code = mqttSession.ByteHead[0];

                // 接收数据并进行分析
                OperateResult<int> receLength = MqttClient.ReceiveMqttNextLength( mqttSession.MqttSocket, Receive );
                if (!receLength.IsSuccess)
                {
                    LogNet?.WriteDebug( $"Receive Length Failed:{receLength.Message}" );
                    return;
                }

                OperateResult<byte[]> data = Receive( mqttSession.MqttSocket, receLength.Content );
                if (!data.IsSuccess)
                {
                    LogNet?.WriteDebug( $"Receive Data Failed:{data.Message}" );
                    return;
                }

                try
                {
                    if (code >> 4 != MqttControlMessage.DISCONNECT)
                        mqttSession.MqttSocket.BeginReceive( mqttSession.ByteHead, 0, 1, SocketFlags.None, new AsyncCallback( SocketReceiveCallback ), mqttSession );
                    else
                    {
                        RemoveAndCloseSession( mqttSession ); return;
                    }
                }
                catch
                {
                    RemoveAndCloseSession( mqttSession ); return;
                }

                // 处理数据
                if (code >> 4 == MqttControlMessage.PUBLISH)
                {
                    // 发布消息
                    mqttSession.ActiveTime = DateTime.Now;
                    DealWithPublish( mqttSession, code, data.Content );
                }
                else if(code >> 4 == MqttControlMessage.PUBREL)
                {
                    // 回发消息完成
                    Send( mqttSession.MqttSocket, MqttClient.BuildMqttCommand( MqttControlMessage.PUBCOMP, 0x00, null, data.Content ).Content );
                }
                else if (code >> 4 == MqttControlMessage.SUBSCRIBE)
                {
                    // 订阅消息
                    mqttSession.ActiveTime = DateTime.Now;
                    DealWithSubscribe( mqttSession, code, data.Content );
                }
                else if (code >> 4 == MqttControlMessage.UNSUBSCRIBE)
                {
                    // 取消订阅消息
                    mqttSession.ActiveTime = DateTime.Now;
                    DealWithUnSubscribe( mqttSession, code, data.Content );
                }
                else if(code >> 4 == MqttControlMessage.PINGREQ)
                {
                    // 心跳请求
                    mqttSession.ActiveTime = DateTime.Now;
                    Send( mqttSession.MqttSocket, MqttClient.BuildMqttCommand( MqttControlMessage.PINGRESP, 0x00, null, null ).Content );
                }
            }
        }

        /// <summary>
        /// 启动初始化的信息，将启动一个定时器，来检查客户端的信息
        /// </summary>
        protected override void StartInitialization( )
        {

        }

        /// <summary>
        /// 关闭引擎的时候触发的内容。
        /// </summary>
        protected override void CloseAction( )
        {
            base.CloseAction( );

            lock (sessionsLock)
            {
                for (int i = 0; i < mqttSessions.Count; i++)
                {
                    mqttSessions[i].MqttSocket?.Close( );
                }
                mqttSessions.Clear( );
            }
        }


        private void DealWithPublish( MqttSession session, byte code, byte[] data )
        {
            bool dup = (code & 0x08) == 0x08;
            int qos = (code & 0x04) == 0x04 ? 2 : 0 + (code & 0x02) == 0x02 ? 1 : 0;
            bool retain = (code & 0x01) == 0x01;
            int msgId = 0;

            int index = 0;
            string topic = MqttClient.ExtraMsgFromBytes( data, ref index );
            if (qos > 0)
            {
                msgId = MqttClient.ExtraIntFromBytes( data, ref index );
            }
            byte[] payload = SoftBasic.BytesArrayRemoveBegin( data, index );

            if(qos == 1)
            {
                // 最少一次
                Send( session.MqttSocket, MqttClient.BuildMqttCommand( MqttControlMessage.PUBACK, 0x00, null, MqttClient.BuildIntBytes( msgId ) ).Content );
            }
            else if(qos == 2)
            {
                // 刚好一次
                Send( session.MqttSocket, MqttClient.BuildMqttCommand( MqttControlMessage.PUBREC, 0x00, null, MqttClient.BuildIntBytes( msgId ) ).Content );
            }

            MqttClientApplicationMessage mqttClientApplicationMessage = new MqttClientApplicationMessage( )
            {
                ClientId = session.ClientId,
                QualityOfServiceLevel = qos == 0 ? MqttQualityOfServiceLevel.AtMostOnce : qos == 1 ? MqttQualityOfServiceLevel.AtLeastOnce : MqttQualityOfServiceLevel.ExactlyOnce,
                Retain = retain,
                Topic = topic,
                UserName = session.UserName,
                Payload = payload,
            };

            OnClientApplicationMessageReceive?.Invoke( mqttClientApplicationMessage );
            if (retain)
            {
                keysLock.Enter( );

                if (retainKeys.ContainsKey( topic ))
                {
                    retainKeys[topic] = payload;
                }
                else
                {
                    retainKeys.Add( topic, payload );
                }

                keysLock.Leave( );
            }
            PublishTopicPayload( topic, payload );
        }

        /// <summary>
        /// 处理订阅的消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="code"></param>
        /// <param name="data"></param>
        private void DealWithSubscribe( MqttSession session, byte code, byte[] data )
        {
            int msgId = 0;
            int index = 0;

            msgId = MqttClient.ExtraIntFromBytes( data, ref index );
            List<string> topics = new List<string>( );
            while(index < data.Length)
            {
                topics.Add( MqttClient.ExtraMsgFromBytes( data, ref index ) );
                index += 1;
            }

            // 返回订阅成功
            Send( session.MqttSocket, MqttClient.BuildMqttCommand( MqttControlMessage.SUBACK, 0x00, null, MqttClient.BuildIntBytes( msgId ) ).Content );

            keysLock.Enter( );

            for (int i = 0; i < topics.Count; i++)
                if (retainKeys.ContainsKey( topics[i] ))
                    Send( session.MqttSocket, MqttClient.BuildPublishMqttCommand( topics[i], retainKeys[topics[i]] ).Content );

            keysLock.Leave( );
            // 添加订阅信息
            session.AddSubscribe( topics.ToArray( ) );
        }

        /// <summary>
        /// 处理取消订阅的消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="code"></param>
        /// <param name="data"></param>
        private void DealWithUnSubscribe( MqttSession session, byte code, byte[] data )
        {
            int msgId = 0;
            int index = 0;

            msgId = MqttClient.ExtraIntFromBytes( data, ref index );
            List<string> topics = new List<string>( );
            while (index < data.Length)
            {
                topics.Add( MqttClient.ExtraMsgFromBytes( data, ref index ) );
                index += 1;
            }

            // 返回订阅成功
            Send( session.MqttSocket, MqttClient.BuildMqttCommand( MqttControlMessage.UNSUBACK, 0x00, null, MqttClient.BuildIntBytes( msgId ) ).Content );

            // 添加订阅信息
            session.RemoveSubscribe( topics.ToArray( ) );
        }

        #endregion

        #region Publish Message

        /// <summary>
        /// 向订阅了指定的主题的客户端发送消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="payload">消息内容</param>
        public void PublishTopicPayload(string topic, byte[] payload )
        {
            lock (sessionsLock)
            {
                for (int i = 0; i < mqttSessions.Count; i++)
                {
                    // 向订阅消息的客户端进行发送数据
                    if (mqttSessions[i].IsClientSubscribe( topic ))
                    {
                        OperateResult send = Send( mqttSessions[i].MqttSocket, MqttClient.BuildPublishMqttCommand( topic, payload ).Content );
                        if (!send.IsSuccess)
                            LogNet?.WriteError( $"Send Topic Failed,Client Id:{mqttSessions[i].ClientId}" );
                    }
                }
            }
        }

        /// <summary>
        /// 向所有的客户端强制发送消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="payload">消息内容</param>
        public void PublishAllClientTopicPayload( string topic, byte[] payload )
        {
            lock (sessionsLock)
            {
                for (int i = 0; i < mqttSessions.Count; i++)
                {
                    OperateResult send = Send( mqttSessions[i].MqttSocket, MqttClient.BuildPublishMqttCommand( topic, payload ).Content );
                    if (!send.IsSuccess)
                        LogNet?.WriteError( $"Send Topic Failed,Client Id:{mqttSessions[i].ClientId}" );
                }
            }
        }


        /// <summary>
        /// 向指定的客户端强制发送消息
        /// </summary>
        /// <param name="clientId">指定的客户端ID信息</param>
        /// <param name="topic">主题</param>
        /// <param name="payload">消息内容</param>
        public void PublishTopicPayload( string clientId, string topic, byte[] payload )
        {
            lock (sessionsLock)
            {
                for (int i = 0; i < mqttSessions.Count; i++)
                {
                    if (mqttSessions[i].ClientId == clientId)
                    {
                        OperateResult send = Send( mqttSessions[i].MqttSocket, MqttClient.BuildPublishMqttCommand( topic, payload ).Content );
                        if (!send.IsSuccess)
                            LogNet?.WriteError( $"Send Topic Failed,Client Id:{mqttSessions[i].ClientId}" );
                    }
                }
            }
        }

        #endregion

        #region Session Method

        private void AddMqttSession( MqttSession session )
        {
            lock (sessionsLock)
            {
                mqttSessions.Add( session );
            }
            LogNet?.WriteDebug( $"Client[{session.ClientId}] Name:{session.UserName} Online" );
        }

        private void RemoveAndCloseSession( MqttSession session )
        {
            lock (sessionsLock)
            {
                mqttSessions.Remove( session );
            }
            session.MqttSocket?.Close( );
            LogNet?.WriteDebug( $"Client[{session.ClientId}] Name:{session.UserName} Offline" );
        }

        #endregion

        #region Event Handler

        /// <summary>
        /// Mqtt的消息收到委托
        /// </summary>
        /// <param name="message">Mqtt的消息</param>
        public delegate void OnClientApplicationMessageReceiveDelegate( MqttClientApplicationMessage message );

        /// <summary>
        /// Mqtt的消息收到时触发
        ///</summary>
        public event OnClientApplicationMessageReceiveDelegate OnClientApplicationMessageReceive;

        /// <summary>
        /// 验证的委托
        /// </summary>
        /// <param name="clientId">客户端的id</param>
        /// <param name="userName">用户名</param>
        /// <param name="passwrod">密码</param>
        /// <returns>0则是通过，否则，就是连接失败</returns>
        public delegate int ClientVerificationDelegate( string clientId, string userName, string passwrod );

        /// <summary>
        /// 客户端验证的事件
        /// </summary>
        public event ClientVerificationDelegate ClientVerification;                                                   // 验证的委托信息

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取当前的在线的客户端数量
        /// </summary>
        public int OnlineCount
        {
            get => mqttSessions.Count;
        }

        #endregion

        #region Private Member

        private Dictionary<string, byte[]> retainKeys;
        private SimpleHybirdLock keysLock;                                                                            // 驻留的消息的词典锁

        private List<MqttSession> mqttSessions = new List<MqttSession>( );                                            // MQTT的客户端信息
        private object sessionsLock = new object( );

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串信息</returns>
        public override string ToString( )
        {
            return $"MqttServer[{Port}]";
        }

        #endregion

        #region Static Helper



        #endregion

    }
}
