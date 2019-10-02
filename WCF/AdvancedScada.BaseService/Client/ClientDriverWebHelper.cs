using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AdvancedScada.BaseService.Client
{
    public class ClientDriverWebHelper : BaseBinding
    {
        public static string HOST = "127.0.0.1";

        public static int PORT = 8088;

        public static int PORT_TCP = 8099;

        public static string USER_NAME = "";

        public static string PASSWORD = "";

        public static Dictionary<int, Tag> Pumps = null;

        /// <summary>
        /// Hàm mở dịch vụ BusinessHelper
        /// </summary>
        public static IReadServiceWeb GetReadServiceWeb()
        {
            try
            {
                //string.Format("http://{0}:{1}/", HOST_NAME, PORT);
                EndpointAddress objEndpointAddress = new EndpointAddress(string.Format("http://{0}:{1}/", HOST, PORT) + "ReadServiceWeb");
                WSHttpBinding objWSHttpBinding = GetWSHttpBinding();
                ChannelFactory<IReadServiceWeb> cf = new ChannelFactory<IReadServiceWeb>(objWSHttpBinding, objEndpointAddress);
                cf.Credentials.Windows.ClientCredential.UserName = USER_NAME; // string.Format("administrator");
                cf.Credentials.Windows.ClientCredential.Password = PASSWORD;
                return cf.CreateChannel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IReadService GetConnectorService()
        {
            try
            {
                InstanceContext objInstanceContext = new InstanceContext(new ReadServiceCallbackClient());
                NetTcpBinding objNetTcpBinding = ClientDriverWebHelper.GetNetTcpBinding();
                EndpointAddress objEndpointAddress = new EndpointAddress(string.Format("net.tcp://{0}:{1}/", HOST, PORT_TCP) + "ConnectorService");
                DuplexChannelFactory<IReadService> factory = new DuplexChannelFactory<IReadService>(objInstanceContext, objNetTcpBinding, objEndpointAddress);

                factory.Credentials.Windows.ClientCredential.UserName = USER_NAME; // string.Format("administrator");
                factory.Credentials.Windows.ClientCredential.Password = PASSWORD;
                IReadService objIS7ConnectorService = factory.CreateChannel();
                return objIS7ConnectorService;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
