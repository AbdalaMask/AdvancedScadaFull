using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HslCommunication.MQTT
{
    /// <summary>
    /// Mqtt服务器的质量等级
    /// </summary>
    public enum MqttQualityOfServiceLevel
    {
        /// <summary>
        /// 最多一次
        /// </summary>
        AtMostOnce = 0,

        /// <summary>
        /// 最少一次
        /// </summary>
        AtLeastOnce = 1,

        /// <summary>
        /// 只有一次
        /// </summary>
        ExactlyOnce = 2
    }
}
