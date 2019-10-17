namespace HslCommunication.MQTT
{
    /// <summary>
    /// Mqtt协议的验证对象，包含用户名和密码
    /// </summary>
    public class MqttCredential
    {
        #region Constructor

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public MqttCredential()
        {

        }

        /// <summary>
        /// 实例化指定的用户名和密码的对象
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        public MqttCredential(string name, string pwd)
        {
            UserName = name;
            Password = pwd;
        }

        #endregion

        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串数据</returns>
        public override string ToString()
        {
            return UserName;
        }
    }
}
