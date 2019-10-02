using AdvancedScada.IBaseService;
using System;
using System.ServiceModel;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.BaseService.Client
{
    public class ClientDriverHelper : BaseBinding
    {
        private static ClientDriverHelper objDriverHelper = null;

        public static string IP_ADDRESS = "127.0.0.1";

        public static string USER_NAME = string.Empty;

        public static string PASSWORD = string.Empty;



        public static ClientDriverHelper GetInstance()
        {
            if (objDriverHelper != null)
            {
                return objDriverHelper;
            }
            return new ClientDriverHelper();
        }

        public IReadService GetReadService()
        {
            try
            {
                InstanceContext callbackInstance = new InstanceContext(new ReadServiceCallbackClient());
                NetTcpBinding netTcpBinding = GetNetTcpBinding();
                EndpointAddress remoteAddress = new EndpointAddress(string.Format(URI_DRIVER, IP_ADDRESS, PORT, "Driver"));
                DuplexChannelFactory<IReadService> duplexChannelFactory = new DuplexChannelFactory<IReadService>(callbackInstance, netTcpBinding, remoteAddress);
                duplexChannelFactory.Credentials.Windows.ClientCredential.UserName = USER_NAME;
                duplexChannelFactory.Credentials.Windows.ClientCredential.Password = PASSWORD;
                return duplexChannelFactory.CreateChannel();
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return null;
        }
        public IReadService GetReadService(InstanceContext callbackInstance)
        {
            try
            {

                NetTcpBinding netTcpBinding = GetNetTcpBinding();
                EndpointAddress remoteAddress = new EndpointAddress(string.Format(URI_DRIVER, IP_ADDRESS, PORT, "Driver"));
                DuplexChannelFactory<IReadService> duplexChannelFactory = new DuplexChannelFactory<IReadService>(callbackInstance, netTcpBinding, remoteAddress);
                duplexChannelFactory.Credentials.Windows.ClientCredential.UserName = USER_NAME;
                duplexChannelFactory.Credentials.Windows.ClientCredential.Password = PASSWORD;
                return duplexChannelFactory.CreateChannel();
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return null;
        }

    }
}
