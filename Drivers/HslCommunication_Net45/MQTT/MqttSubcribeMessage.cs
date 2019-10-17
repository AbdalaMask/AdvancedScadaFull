using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HslCommunication.MQTT
{
    /// <summary>
    /// 订阅的消息类
    /// </summary>
    public class MqttSubcribeMessage : IDisposable
    {
        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public MqttSubcribeMessage( )
        {
            QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce;
            ResetEvent = new AutoResetEvent( false );
        }

        /// <summary>
        /// 这个字段表示应用消息分发的服务质量等级保证。分为，最多一次，最少一次，正好一次
        /// </summary>
        /// <remarks>
        /// 在实际的开发中的情况下，最多一次是最省性能的，正好一次是最消耗性能的，如果应有场景为推送实时的数据，那么，最多一次的性能是最高的
        /// </remarks>
        public MqttQualityOfServiceLevel QualityOfServiceLevel { get; set; }

        /// <summary>
        /// 当前的消息的标识符，当质量等级为0的时候，不需要重发以及考虑标识情况
        /// </summary>
        public int Identifier { get; set; }

        /// <summary>
        /// 当前发布消息携带的mqtt的应用消息
        /// </summary>
        public string[] Topics { get; set; }

        /// <summary>
        /// 线程间的通知器
        /// </summary>
        public AutoResetEvent ResetEvent { get; set; }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose( bool disposing )
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
                ResetEvent.Close( );
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~MqttSubcribeMessage()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose( )
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose( true );
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
