using System;
using System.ServiceModel;
using System.ServiceModel.Description;
namespace AdvancedScada.IBaseService
{
    public delegate void ScadaLogger(int _Id, string _logType, string _time, string _message);
    public delegate void ScadaException(string classname, string erorr);

    public delegate void PvGridChannelGet(object Value, bool Visible);
    public delegate void PvGridDeviceGet(object Value, bool Visible);
    public delegate void PvGridDataBlockGet(object Value, bool Visible);

    public class BaseBinding
    {
        protected ushort PORT = 8086;

        protected string URI_DRIVER = "net.tcp://{0}:{1}/DriverService/{2}";

      

      
        protected const string DRIVER = "Driver";
 

      
        protected NetTcpBinding GetNetTcpBinding()
        {
            return new NetTcpBinding
            {
                ReceiveTimeout = TimeSpan.FromMinutes(2.0),
                SendTimeout = TimeSpan.FromMinutes(2.0),
                OpenTimeout = TimeSpan.FromDays(15.0),
                CloseTimeout = TimeSpan.FromDays(15.0),
                MaxReceivedMessageSize = int.MaxValue
            };
        }

        protected BasicHttpBinding GetBasicHttpBinding()
        {
            return new BasicHttpBinding
            {
                ReceiveTimeout = TimeSpan.FromMinutes(2.0),
                SendTimeout = TimeSpan.FromMinutes(2.0),
                OpenTimeout = TimeSpan.FromDays(15.0),
                CloseTimeout = TimeSpan.FromDays(15.0),
                MaxReceivedMessageSize = int.MaxValue
            };
        }

        protected WebHttpBinding GetWebHttpBinding()
        {
            return new WebHttpBinding
            {
                ReceiveTimeout = TimeSpan.FromMinutes(2.0),
                SendTimeout = TimeSpan.FromMinutes(2.0),
                OpenTimeout = TimeSpan.FromDays(15.0),
                CloseTimeout = TimeSpan.FromDays(15.0),
                MaxReceivedMessageSize = int.MaxValue
            };
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
            }
            return serviceThrottlingBehavior;
        }
    }
}