using System;
using System.ServiceModel;
using System.ServiceModel.Description;
namespace AdvancedScada.IBaseService
{

    public class BaseBinding
    {
        protected ushort PORT = 8086;
        protected ushort PORTWeb = 8090;
        protected string URI_DRIVER = "net.tcp://{0}:{1}/DriverService/{2}";
        protected string URI_DRIVERWeb = "http://{0}/DriverService/{1}";
        protected string URI_DRIVERWeb2 = "http://{0}:{1}/DriverService/{2}";
        protected const string DRIVER = "Driver";



        protected static NetTcpBinding GetNetTcpBinding()
        {
            NetTcpBinding objNetTcpBinding = new NetTcpBinding(SecurityMode.Transport);
            objNetTcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            objNetTcpBinding.ReceiveTimeout = TimeSpan.FromDays(7);
            objNetTcpBinding.SendTimeout = TimeSpan.FromDays(7);
            objNetTcpBinding.MaxReceivedMessageSize = int.MaxValue;
            objNetTcpBinding.MaxBufferPoolSize = int.MaxValue;
            objNetTcpBinding.MaxBufferSize = int.MaxValue;
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
                WebHttpBinding objWebHttpBinding = new WebHttpBinding
                {
                    ReceiveTimeout = TimeSpan.FromHours(2),
                    SendTimeout = TimeSpan.FromHours(2),
                    MaxReceivedMessageSize = 2000000
                };
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
                WSHttpBinding objWSHttpBinding = new WSHttpBinding
                {
                    ReceiveTimeout = TimeSpan.FromHours(2),
                    SendTimeout = TimeSpan.FromHours(2),
                    MaxReceivedMessageSize = 2000000
                };
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
                serviceThrottlingBehavior = new ServiceThrottlingBehavior
                {
                    MaxConcurrentCalls = int.MaxValue,
                    MaxConcurrentSessions = int.MaxValue,
                    MaxConcurrentInstances = int.MaxValue
                };

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