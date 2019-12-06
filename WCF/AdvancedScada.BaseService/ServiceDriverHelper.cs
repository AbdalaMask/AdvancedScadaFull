using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using AdvancedScada.Management.BLManager;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.BaseService
{
    public class ServiceDriverHelper : BaseBinding
    {
        public static ConnectionState objConnectionState = ConnectionState.DISCONNECT;
        private IODriver driverHelper = null;
        private ushort _SerialNo;
        public Dictionary<string, IODriver> RequestsDriver { get; set; }

        public ServiceDriverHelper()
        {
            RequestsDriver = new Dictionary<string, IODriver>(1024);
        }

        public ServiceHost InitializeReadServiceHttp()

        {
            ServiceHost serviceHost = null;
            Type serviceType = null;
            try
            {
                serviceType = typeof(ReadService);

                Uri[] baseAddresses = new Uri[2]
                {
                    new Uri(string.Format(URI_DRIVER, Environment.MachineName, PORT, "Driver")),
                    new Uri(string.Format(URI_DRIVERWeb,Environment.MachineName, "Driver"))
                };


                serviceHost = new ServiceHost(serviceType, baseAddresses);
                ServiceThrottlingBehavior throttle = serviceHost.Description.Behaviors.Find<ServiceThrottlingBehavior>();
                if (throttle == null)
                {
                    throttle = new ServiceThrottlingBehavior
                    {
                        MaxConcurrentCalls = int.MaxValue,
                        MaxConcurrentSessions = int.MaxValue,
                        MaxConcurrentInstances = int.MaxValue
                    };
                    serviceHost.Description.Behaviors.Add(throttle);
                }

                NetTcpBinding binding = GetNetTcpBinding();
                serviceHost.AddServiceEndpoint(typeof(IReadService), binding, string.Empty);


                ////Enable metadata exchange
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior
                {
                    HttpGetUrl = new Uri(string.Format(URI_DRIVERWeb, Environment.MachineName, "Driver")),
                    HttpGetEnabled = true
                };
                serviceHost.Description.Behaviors.Add(smb);
            }
            catch (CommunicationException ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return serviceHost;
        }
        public ServiceHost InitializeReadService()
        {
            ServiceHost serviceHost = null;

            try
            {


                string address = string.Format(URI_DRIVER, Environment.MachineName, PORT, "Driver");

                NetTcpBinding netTcpBinding = GetNetTcpBinding();
                serviceHost = new ServiceHost(typeof(ReadService));

                serviceHost.AddServiceEndpoint(typeof(IReadService), netTcpBinding, address);

                return serviceHost;
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);

            }
            return serviceHost;
        }
        public WebServiceHost InitializeReadServiceWeb()
        {
            WebServiceHost objWebServiceHost = null;

            try
            {
                Uri uriWeb = new Uri(string.Format(URI_DRIVERWeb2, Environment.MachineName, PORTWeb, "Driver"));
                Uri uriWS = new Uri(string.Format(URI_DRIVERWeb2, Environment.MachineName, 8088, "Driver"));
                objWebServiceHost = new WebServiceHost(typeof(ReadServiceWeb));
                WebHttpBinding objWebHttpBinding = GetWebHttpBinding();
                WSHttpBinding objWSHttpBinding = GetWSHttpBinding();
                objWebServiceHost.AddServiceEndpoint(typeof(IReadServiceWeb), objWebHttpBinding, uriWeb);
                objWebServiceHost.AddServiceEndpoint(typeof(IReadServiceWeb), objWSHttpBinding, uriWS);
                objWebServiceHost.Open();

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);

            }
            return objWebServiceHost;
        }
        private IODriver GetDriver(string ChannelTypes)
        {
            IODriver DriverHelper = null;
            GetIODriver objFunctions = GetIODriver.GetFunctions();
            DriverHelper =
                       objFunctions.GetAssembly($@"\AdvancedScada.{ChannelTypes}.Core.dll",
                           $"AdvancedScada.{ChannelTypes}.Core.IODriverHelper");
            return DriverHelper;
        }
        public bool InitializePLC()
        {
            ChannelService objChannelManager = ChannelService.GetChannelManager();
            try
            {
                RequestsDriver.Clear();
                string xmlFile = objChannelManager.ReadKey(objChannelManager.XML_NAME_DEFAULT);
                if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile))
                {
                    return false;
                }

                objChannelManager.Channels.Clear();
                TagCollection.Tags.Clear();
                List<Channel> channels = objChannelManager.GetChannels(xmlFile);
                GetIODriver objFunctions = GetIODriver.GetFunctions();
                ////Sort.
                channels.Sort(delegate (Channel x, Channel y)
                {
                    return x.ChannelTypes.CompareTo(y.ChannelTypes);
                });
                foreach (Channel item in channels)
                {

                    driverHelper = GetDriver(item.ChannelTypes);
                    if (driverHelper != null)
                    {
                        driverHelper.InitializeService(item);
                    }
                }
                foreach (Channel item in channels)
                {
                    _SerialNo = (ushort)(_SerialNo++ % 255 + 1);
                    driverHelper = GetDriver(item.ChannelTypes);

                    if (RequestsDriver.ContainsKey(item.ChannelTypes))
                    {
                    }
                    else
                    {
                        RequestsDriver.Add(item.ChannelTypes, driverHelper);
                        if (driverHelper != null)
                        {
                            driverHelper?.Connect();
                        }
                    }

                }

                return true;


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
            return true;

        }

        public bool GetStopService()
        {
            try
            {



                //driverHelper.Disconnect();

                return true;
            }
            catch (System.Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
            return true;
        }


    }
}
