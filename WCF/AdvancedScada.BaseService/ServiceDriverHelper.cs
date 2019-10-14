using AdvancedScada.BaseService.Web;
using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using AdvancedScada.IBaseService.Common;
using AdvancedScada.Management.BLManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.BaseService
{
    public class ServiceDriverHelper : BaseBinding
    {
        public static ConnectionState objConnectionState = ConnectionState.DISCONNECT;
        private static string FILE_LOG = @"C:\AdvancedScada.txt";
        IODriver driverHelper = null;
        private ushort _SerialNo;
        public Dictionary<string, IODriver> RequestsDriver { get; set; }

        public ServiceDriverHelper()
        {
            RequestsDriver = new Dictionary<string, IODriver>(1024);
        }

        public void OpenWebServiceHost()
        {
            try
            {
                Uri uriWeb = new Uri("http://localhost:8086/Driver");
                Uri uriWS = new Uri("http://localhost:8088/Driver");
                WebServiceHost objWebServiceHost = new WebServiceHost(typeof(ReadServiceWeb));
                WebHttpBinding objWebHttpBinding = GetWebHttpBinding();
                WSHttpBinding objWSHttpBinding = GetWSHttpBinding();
                objWebServiceHost.AddServiceEndpoint(typeof(IReadServiceWeb), objWebHttpBinding, uriWeb);
                objWebServiceHost.AddServiceEndpoint(typeof(IReadServiceWeb), objWSHttpBinding, uriWS);
                objWebServiceHost.Open();
                foreach (ServiceEndpoint item in objWebServiceHost.Description.Endpoints)
                {

                    //DriverService.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> Service is host with endpoint: '{1}'", DateTime.Now, item.Address));
                }

                // Kết nối PLC.
                InitializePLC();
            }
            catch (Exception ex)
            {
                //AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(OpenWebServiceHost): '{1}'", DateTime.Now, ex.Message));
            }

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
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);

            }
            return serviceHost;
        }
        private IODriver GetDriver(string ChannelTypes)
        {
            IODriver DriverHelper = null;
            var objFunctions = GetIODriver.GetFunctions();
            DriverHelper =
                       objFunctions.GetAssembly($@"\AdvancedScada.{ChannelTypes}.Core.dll",
                           $"AdvancedScada.{ChannelTypes}.Core.IODriverHelper");
            return DriverHelper;
        }
        public bool InitializePLC()
        {
            var objChannelManager = ChannelService.GetChannelManager();
            try
            {
                RequestsDriver.Clear();
                var xmlFile = objChannelManager.ReadKey(objChannelManager.XML_NAME_DEFAULT);
                if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile)) return false;
                objChannelManager.Channels.Clear();
                TagCollection.Tags.Clear();
                var channels = objChannelManager.GetChannels(xmlFile);
                var objFunctions = GetIODriver.GetFunctions();
                ////Sort.
                channels.Sort(delegate (Channel x, Channel y)
                {
                    return x.ChannelTypes.CompareTo(y.ChannelTypes);
                });
                foreach (var item in channels)
                {

                    driverHelper = GetDriver(item.ChannelTypes);
                    if (driverHelper != null)
                        driverHelper.InitializeService(item);
                }
                foreach (var item in channels)
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
                            driverHelper?.Connect();

                    }

                }
                eventAddMessage += new EventListenning(AddMessage);
                eventConnectionState += new EventConnectionState(SetConnectionState);

                //driverHelper.InitializeService(channels);

                //ThreadPool.QueueUserWorkItem((th) =>
                //{
                //    driverHelper.Connect();
                //});
                //eventAddMessage += new EventListenning(AddMessage);
                //eventConnectionState += new EventConnectionState(SetConnectionState);
                return true;


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
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

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            return true;
        }

        private static void SetConnectionState(ConnectionState connState, string msg)
        {

            try
            {
                objConnectionState = connState;
                switch (connState)
                {
                    case ConnectionState.CONNECT:
                        ServiceDriverHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> Connection State: '{1}'", DateTime.Now, "Connect"));
                        break;
                    case ConnectionState.DISCONNECT:
                        ServiceDriverHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> Connection State: '{1}'", DateTime.Now, "Disconnect"));
                        break;
                }

            }
            catch (Exception ex)
            {
                ServiceDriverHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(OpenWebServiceHost): '{1}'", DateTime.Now, ex.Message));
            }

        }

        private static void AddMessage(string msg)
        {
            try
            {
                ServiceDriverHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> DriverService(AddMessage): '{1}'", DateTime.Now, msg));
            }
            catch (Exception ex)
            {
                ServiceDriverHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(OpenWebServiceHost): '{1}'", DateTime.Now, ex.Message));
            }
        }


        public static void AddLog(string msg)
        {
            try
            {
                // Write single line to new file.
                FILE_LOG = string.Format(@"C:\{0:MM-yyyy}_AdvancedScada.txt", DateTime.Now);
                using (StreamWriter writer = new StreamWriter(FILE_LOG, true))
                {
                    writer.WriteLine(msg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR(AddLog): " + ex.Message);
            }
        }
    }
}
