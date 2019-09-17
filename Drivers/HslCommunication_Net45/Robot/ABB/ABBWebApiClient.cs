#if !NET35
#endif
using HslCommunication.Core.Net;
using System.Text.RegularExpressions;

namespace HslCommunication.Robot.ABB
{
    /// <summary>
    /// ABB机器人的web api机制的客户端
    /// </summary>
    /// <remarks>
    /// 参考的界面信息是：http://developercenter.robotstudio.com/webservice/api_reference
    /// </remarks>
    public class ABBWebApiClient : NetworkWebApiBase, IRobotNet
    {
        #region Constrcutor

        /// <summary>
        /// 使用指定的ip地址来初始化对象
        /// </summary>
        /// <param name="ipAddress">Ip地址信息</param>
        public ABBWebApiClient(string ipAddress) : base(ipAddress)
        {
        }

        /// <summary>
        /// 使用指定的ip地址来初始化对象
        /// </summary>
        /// <param name="ipAddress">Ip地址信息</param>
        /// <param name="port">端口号信息</param>
        public ABBWebApiClient(string ipAddress, int port) : base(ipAddress, port)
        {
        }

        /// <summary>
        /// 使用指定的ip地址及端口号来初始化对象
        /// </summary>
        /// <param name="ipAddress">Ip地址信息</param>
        /// <param name="port">端口号信息</param>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        public ABBWebApiClient(string ipAddress, int port, string name, string password) : base(ipAddress, port, name, password)
        {
        }

        #endregion

        #region Read Write Support

        /// <summary>
        /// 采集自定义的数据信息
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns>带有额外信息的结果类对象</returns>
        protected override OperateResult<string> ReadByAddress(string address)
        {
            if (address.ToUpper() == "ErrorState".ToUpper())
            {
                return GetErrorState();
            }
            else if (address.ToUpper() == "PhysicalJoints".ToUpper())
            {
                return GetPhysicalJoints();
            }
            else if (address.ToUpper() == "SpeedRatio".ToUpper())
            {
                return GetSpeedRatio();
            }
            else if (address.ToUpper() == "OperationMode".ToUpper())
            {
                return GetOperationMode();
            }
            else if (address.ToUpper() == "CtrlState".ToUpper())
            {
                return GetCtrlState();
            }
            else
            {
                return base.ReadByAddress(address);
            }
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 获取当前的控制状态，Content属性就是机器人的控制信息
        /// </summary>
        /// <returns>带有状态信息的结果类对象</returns>
        public OperateResult<string> GetCtrlState()
        {
            OperateResult<string> read = ReadString("url=/rw/panel/ctrlstate");
            if (!read.IsSuccess) return read;

            Match match = Regex.Match(read.Content, "<span class=\"ctrlstate\">[^<]+");
            if (!match.Success) return new OperateResult<string>(read.Content);

            return OperateResult.CreateSuccessResult(match.Value.Substring(24));
        }

        /// <summary>
        /// 获取当前的错误状态，Content属性就是机器人的状态信息
        /// </summary>
        /// <returns>带有状态信息的结果类对象</returns>
        public OperateResult<string> GetErrorState()
        {
            OperateResult<string> read = ReadString("url=/rw/motionsystem/errorstate");
            if (!read.IsSuccess) return read;

            Match match = Regex.Match(read.Content, "<span class=\"err-state\">[^<]+");
            if (!match.Success) return new OperateResult<string>(read.Content);

            return OperateResult.CreateSuccessResult(match.Value.Substring(24));
        }

        /// <summary>
        /// 获取当前机器人的物理关节点信息，返回json格式的关节信息
        /// </summary>
        /// <returns>带有关节信息的结果类对象</returns>
        public OperateResult<string> GetPhysicalJoints()
        {
            OperateResult<string> read = ReadString("url=/rw/motionsystem/mechunits/ROB_1/jointtarget");
            if (!read.IsSuccess) return read;

            MatchCollection mc = Regex.Matches(read.Content, "<span class=\"rax[^<]*");
            if (mc.Count != 6) return new OperateResult<string>(read.Content);

            double[] joints = new double[6];
            for (int i = 0; i < mc.Count; i++)
            {
                if (mc[i].Length > 17)
                {
                    joints[i] = double.Parse(mc[i].Value.Substring(20));
                }
            }
            return OperateResult.CreateSuccessResult(Newtonsoft.Json.Linq.JArray.FromObject(joints).ToString(Newtonsoft.Json.Formatting.None));
        }

        /// <summary>
        /// 获取当前机器人的速度配比信息
        /// </summary>
        /// <returns>带有速度信息的结果类对象</returns>
        public OperateResult<string> GetSpeedRatio()
        {
            OperateResult<string> read = ReadString("url=/rw/panel/speedratio");
            if (!read.IsSuccess) return read;

            Match match = Regex.Match(read.Content, "<span class=\"speedratio\">[^<]*");
            if (!match.Success) return new OperateResult<string>(read.Content);

            return OperateResult.CreateSuccessResult(match.Value.Substring(25));
        }

        /// <summary>
        /// 获取当前机器人的工作模式
        /// </summary>
        /// <returns>带有工作模式信息的结果类对象</returns>
        public OperateResult<string> GetOperationMode()
        {
            OperateResult<string> read = ReadString("url=/rw/panel/opmode");
            if (!read.IsSuccess) return read;

            Match match = Regex.Match(read.Content, "<span class=\"opmode\">[^<]*");
            if (!match.Success) return new OperateResult<string>(read.Content);

            return OperateResult.CreateSuccessResult(match.Value.Substring(21));
        }

        #endregion

        #region Object Override



        #endregion
    }
}
