using System;
using System.Threading;

namespace HslCommunication.MQTT
{
    /// <summary>
    /// Mqtt发送的消息封装对象，是对 <see cref="MqttApplicationMessage"/> 对象的封装，添加了序号，还有是否重发的信息
    /// </summary>
    public class MqttPublishMessage : IDisposable
    {
        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public MqttPublishMessage()
        {
            IsSendFirstTime = true;
            ResetEvent = new AutoResetEvent(false);
        }

        /// <summary>
        /// 是否第一次发送数据信息
        /// </summary>
        public bool IsSendFirstTime { get; set; }

        /// <summary>
        /// 当前的消息的标识符，当质量等级为0的时候，不需要重发以及考虑标识情况
        /// </summary>
        public int Identifier { get; set; }

        /// <summary>
        /// 当前发布消息携带的mqtt的应用消息
        /// </summary>
        public MqttApplicationMessage Message { get; set; }

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
        protected virtual void Dispose(bool disposing)
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
                ResetEvent.Close();
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~MqttSubcribeMessage()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。

        /// <summary>
        /// 释放当前的资源
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
