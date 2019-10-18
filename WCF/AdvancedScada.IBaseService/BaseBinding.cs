using AdvancedScada.DriverBase;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
namespace AdvancedScada.IBaseService
{
    public delegate void ScadaLogger(int _Id, string _logType, string _time, string _message);
    public delegate void ScadaException(string classname, string erorr);
    public delegate void EventConnectionChanged(ConnectionState status);
    public delegate void PvGridChannelGet(object Value, bool Visible);
    public delegate void PvGridDeviceGet(object Value, bool Visible);
    public delegate void PvGridDataBlockGet(object Value, bool Visible);
    public delegate void EventListenning(string msg);
    public delegate void EventConnectionState(ConnectionState connState, string msg);
    public class BaseBinding
    {
        protected ushort PORT = 8086;

        protected string URI_DRIVER = "net.tcp://{0}:{1}/DriverService/{2}";
        protected string URI_DRIVERWeb = "http://{0}/DriverService/{1}";
        protected const string DRIVER = "Driver";



        protected static NetTcpBinding GetNetTcpBinding()
        {
            NetTcpBinding objNetTcpBinding = new NetTcpBinding(SecurityMode.Transport);
            objNetTcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            objNetTcpBinding.ReceiveTimeout = TimeSpan.FromDays(7);
            objNetTcpBinding.SendTimeout = TimeSpan.FromDays(7);
            objNetTcpBinding.MaxReceivedMessageSize = Int32.MaxValue;
            objNetTcpBinding.MaxBufferPoolSize = Int32.MaxValue;
            objNetTcpBinding.MaxBufferSize = Int32.MaxValue;
            return objNetTcpBinding;
        }

        /// <summary>
        /// Hàm trả về đối tượng WebHttpBinding
        /// </summary>
        /// <returns>WebHttpBinding</returns>
        public static WebHttpBinding GetWebHttpBinding()
        {
            try
            {
                WebHttpBinding objWebHttpBinding = new WebHttpBinding();
                objWebHttpBinding.ReceiveTimeout = TimeSpan.FromHours(2);
                objWebHttpBinding.SendTimeout = TimeSpan.FromHours(2);
                objWebHttpBinding.MaxReceivedMessageSize = 2000000;
                //objWebHttpBinding.Security.Mode = WebHttpSecurityMode.None;
                return objWebHttpBinding;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Hàm trả về đối tượng WSHttpBinding
        /// </summary>
        /// <returns>WSHttpBinding</returns>
        public static WSHttpBinding GetWSHttpBinding()
        {
            try
            {
                WSHttpBinding objWSHttpBinding = new WSHttpBinding();
                objWSHttpBinding.ReceiveTimeout = TimeSpan.FromHours(2);
                objWSHttpBinding.SendTimeout = TimeSpan.FromHours(2);
                objWSHttpBinding.MaxReceivedMessageSize = 2000000;
                //objWSHttpBinding.Security.Mode = SecurityMode.Message;
                //objWSHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                return objWSHttpBinding;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public ServiceThrottlingBehavior GetServiceThrottlingBehaviorByHost(ServiceHost host)
        {
            ServiceThrottlingBehavior serviceThrottlingBehavior = host.Description.Behaviors.Find<ServiceThrottlingBehavior>();
            if (serviceThrottlingBehavior == null)
            {
                serviceThrottlingBehavior = new ServiceThrottlingBehavior();
                serviceThrottlingBehavior.MaxConcurrentCalls = int.MaxValue;
                serviceThrottlingBehavior.MaxConcurrentSessions = int.MaxValue;
                serviceThrottlingBehavior.MaxConcurrentInstances = int.MaxValue;
               
                host.Description.Behaviors.Add(serviceThrottlingBehavior);

                //ServiceMetadataBehavior metad = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
        
                //if (metad == null)
                //    metad = new ServiceMetadataBehavior();
                //metad.HttpGetEnabled = true;
                //host.Description.Behaviors.Add(metad);
               
            }
            return serviceThrottlingBehavior;
        }
    }
}