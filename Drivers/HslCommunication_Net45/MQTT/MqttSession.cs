using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace HslCommunication.MQTT
{
    /// <summary>
    /// Mqtt的会话
    /// </summary>
    public class MqttSession
    {
        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public MqttSession( )
        {
            ByteHead = new byte[1];
            Topics = new List<string>( );
            ActiveTime = DateTime.Now;
            ActiveTimeSpan = TimeSpan.FromSeconds( 1000000 );
        }

        /// <summary>
        /// 当前接收的头数据的信息
        /// </summary>
        public byte[] ByteHead { get; set; }

        /// <summary>
        /// 当前接收的客户端ID信息
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 当前客户端的激活时间
        /// </summary>
        public DateTime ActiveTime { get; set; }

        /// <summary>
        /// 两次活动的最小时间间隔
        /// </summary>
        public TimeSpan ActiveTimeSpan { get; set; }

        /// <summary>
        /// 当前客户端绑定的套接字对象
        /// </summary>
        public Socket MqttSocket { get; set; }

        /// <summary>
        /// 当前客户端订阅的所有的Topic信息
        /// </summary>
        public List<string> Topics { get; set; }

        /// <summary>
        /// 当前的用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 检查当前的连接对象是否在
        /// </summary>
        /// <param name="topic">主题信息</param>
        /// <returns>是否包含的结果信息</returns>
        public bool IsClientSubscribe( string topic )
        {
            bool ret = false;
            lock (objLock)
            {
                ret = Topics.Contains( topic );
            }
            return ret;
        }

        /// <summary>
        /// 当前的会话信息新增一个订阅的信息
        /// </summary>
        /// <param name="topic">主题的信息</param>
        public void AddSubscribe( string topic )
        {
            lock (objLock)
            {
                if(!Topics.Contains( topic ))
                {
                    Topics.Add( topic );
                }
            }
        }


        /// <summary>
        /// 当前的会话信息新增一个订阅的信息
        /// </summary>
        /// <param name="topics">主题的信息</param>
        public void AddSubscribe( string[] topics )
        {
            if (topics == null) return;
            lock (objLock)
            {
                for (int i = 0; i < topics.Length; i++)
                {
                    if (!Topics.Contains( topics[i] ))
                    {
                        Topics.Add( topics[i] );
                    }
                }
            }
        }

        /// <summary>
        /// 移除会话信息的一个订阅的主题
        /// </summary>
        /// <param name="topic">主题</param>
        public void RemoveSubscribe( string topic )
        {
            lock (objLock)
            {
                if (Topics.Contains( topic ))
                {
                    Topics.Remove( topic );
                }
            }
        }

        /// <summary>
        /// 移除会话信息的一个订阅的主题
        /// </summary>
        /// <param name="topics">主题</param>
        public void RemoveSubscribe( string[] topics )
        {
            if (topics == null) return;
            lock (objLock)
            {
                for (int i = 0; i < topics.Length; i++)
                {
                    if (Topics.Contains( topics[i] ))
                    {
                        Topics.Remove( topics[i] );
                    }
                }
            }
        }

        private object objLock = new object( );
    }
}
