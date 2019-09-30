using AdvancedScada.DriverBase;
using AdvancedScada.IBaseService;
using AdvancedScada.IBaseService.Common;
using AdvancedScada.IODriver;
using AdvancedScada.Management.BLManager;
using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.BaseService
{
    public class DriverService : BaseBinding
    {
        public static ConnectionState objConnectionState = ConnectionState.DISCONNECT;
        private static string FILE_LOG = @"C:\AdvancedScada.txt";
        DriverHelper driverHelper = new DriverHelper();

        public  void OpenWebServiceHost()
        {
            try
            {
                Uri uriWeb = new Uri("http://localhost:8086/PumpService");
                Uri uriWS = new Uri("http://localhost:8088/PumpService");
                WebServiceHost objWebServiceHost = new WebServiceHost(typeof(ReadService));
                WebHttpBinding objWebHttpBinding = DriverService.GetWebHttpBinding();
                WSHttpBinding objWSHttpBinding = DriverService.GetWSHttpBinding();
                objWebServiceHost.AddServiceEndpoint(typeof(IReadService), objWebHttpBinding, uriWeb);
                objWebServiceHost.AddServiceEndpoint(typeof(IReadService), objWSHttpBinding, uriWS);
                objWebServiceHost.Open();
                foreach (ServiceEndpoint item in objWebServiceHost.Description.Endpoints)
                {
                    //Console.WriteLine("Service is host with endpoint: " + item.Address);
                    DriverService.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> Service is host with endpoint: '{1}'", DateTime.Now, item.Address));
                }

                // Kết nối PLC.
                InitializePLC();
            }
            catch (Exception ex)
            {
                AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(OpenPumpServiceHost): '{1}'", DateTime.Now, ex.Message));
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
        public bool InitializePLC()
        {
            var objChannelManager = ChannelService.GetChannelManager();
            try
            {

                var xmlFile = objChannelManager.ReadKey(objChannelManager.XML_NAME_DEFAULT);
                if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile)) return false;
                objChannelManager.Channels.Clear();
                TagCollection.Tags.Clear();
                var channels = objChannelManager.GetChannels(xmlFile);

                driverHelper.InitializeService(channels);

                ThreadPool.QueueUserWorkItem((th) =>
                {
                    driverHelper.Connect();
                });
                eventAddMessage += new EventUOSListenning(AddMessage);
                eventUOSAccepting += new EventUOSAccepting(SetConnectionState);
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



                driverHelper.Disconnect();

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
                        DriverService.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> Connection State: '{1}'", DateTime.Now, "Connect"));
                        break;
                    case ConnectionState.DISCONNECT:
                        DriverService.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> Connection State: '{1}'", DateTime.Now, "Disconnect"));
                        break;
                }

            }
            catch (Exception ex)
            {
                DriverService.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(OpenPumpServiceHost): '{1}'", DateTime.Now, ex.Message));
            }

        }

        private static void AddMessage(string msg)
        {
            try
            {
                DriverService.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> BusinessHelper(AddMessage): '{1}'", DateTime.Now, msg));
            }
            catch (Exception ex)
            {
                DriverService.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(OpenPumpServiceHost): '{1}'", DateTime.Now, ex.Message));
            }
        }


        public static void AddLog(string msg)
        {
            try
            {
                // Write single line to new file.
                FILE_LOG = string.Format(@"C:\{0:MM-yyyy}_IndustrialNetworks.txt", DateTime.Now);
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
