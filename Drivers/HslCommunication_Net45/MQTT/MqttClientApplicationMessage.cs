using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HslCommunication.MQTT
{
    /// <summary>
    /// 来自客户端的一次消息的内容
    /// </summary>
    public class MqttClientApplicationMessage : MqttApplicationMessage
    {
        /// <summary>
        /// 客户端的Id信息
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 当前的客户端的用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 当前的连接会话信息
        /// </summary>
        protected MqttSession MqttSession { get; set; }
    }
}
