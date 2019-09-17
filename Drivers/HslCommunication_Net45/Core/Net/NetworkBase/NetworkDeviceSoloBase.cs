using HslCommunication.Core.IMessage;
using System;
using System.Net.Sockets;
using System.Threading;

namespace HslCommunication.Core.Net
{
    /// <summary>
    /// 基于单次无协议的网络交互的基类，通常是串口协议扩展成网口协议的基类
    /// </summary>
    /// <typeparam name="TTransform">指定了数据转换的规则</typeparam>
    public class NetworkDeviceSoloBase<TTransform> : NetworkDeviceBase<HslMessage, TTransform> where TTransform : IByteTransform, new()
    {
        #region Constrcutor

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public NetworkDeviceSoloBase()
        {
            ReceiveTimeOut = 5000;
        }

        #endregion

        #region Receive Bytes

        /// <summary>
        /// 从串口接收一串数据信息，可以指定是否一定要接收到数据
        /// </summary>
        /// <param name="socket">串口对象</param>
        /// <param name="awaitData">是否必须要等待数据返回</param>
        /// <returns>结果数据对象</returns>
        protected OperateResult<byte[]> ReceiveSolo(Socket socket, bool awaitData)
        {
            if (!Authorization.nzugaydgwadawdibbas()) return new OperateResult<byte[]>(StringResources.Language.AuthorizationFailed);
            byte[] buffer = new byte[1024];
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            DateTime start = DateTime.Now;                                  // 开始时间，用于确认是否超时的信息

            HslTimeOut hslTimeOut = new HslTimeOut()
            {
                DelayTime = ReceiveTimeOut,
                WorkSocket = socket,
            };
            if (ReceiveTimeOut > 0) ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolCheckTimeOut), hslTimeOut);

            try
            {
                Thread.Sleep(sleepTime);
                int receiveCount = socket.Receive(buffer);
                hslTimeOut.IsSuccessful = true;
                ms.Write(buffer, 0, receiveCount);
            }
            catch (Exception ex)
            {
                ms.Dispose();
                return new OperateResult<byte[]>(ex.Message);
            }
            byte[] result = ms.ToArray();
            ms.Dispose();
            return OperateResult.CreateSuccessResult(result);
        }

        #endregion

        #region Override NetworkDeviceBase

        /// <summary>
        /// 重写读取服务器的数据信息
        /// </summary>
        /// <param name="socket">网络套接字</param>
        /// <param name="send">发送的数据内容</param>
        /// <returns>读取数据的结果</returns>
        public override OperateResult<byte[]> ReadFromCoreServer(Socket socket, byte[] send)
        {

            LogNet?.WriteDebug(ToString(), StringResources.Language.Send + " : " + BasicFramework.SoftBasic.ByteToHexString(send, ' '));

            // send
            OperateResult sendResult = Send(socket, send);
            if (!sendResult.IsSuccess)
            {
                socket?.Close();
                return OperateResult.CreateFailedResult<byte[]>(sendResult);
            }

            if (receiveTimeOut < 0) return OperateResult.CreateSuccessResult(new byte[0]);

            // receive msg
            OperateResult<byte[]> resultReceive = ReceiveSolo(socket, false);
            if (!resultReceive.IsSuccess)
            {
                socket?.Close();
                return new OperateResult<byte[]>(StringResources.Language.ReceiveDataTimeout + receiveTimeOut);
            }

            LogNet?.WriteDebug(ToString(), StringResources.Language.Receive + " : " + BasicFramework.SoftBasic.ByteToHexString(resultReceive.Content, ' '));

            // Success
            return OperateResult.CreateSuccessResult(resultReceive.Content);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 连续串口缓冲数据检测的间隔时间，默认20ms
        /// </summary>
        public int SleepTime
        {
            get { return sleepTime; }
            set { if (value > 0) sleepTime = value; }
        }


        #endregion

        #region Private Member

        private int sleepTime = 20;                               // 睡眠的时间

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串信息
        /// </summary>
        /// <returns>字符串信息</returns>
        public override string ToString()
        {
            return $"NetworkDeviceSoloBase<{typeof(TTransform)}>[{IpAddress}:{Port}]";
        }

        #endregion
    }
}
