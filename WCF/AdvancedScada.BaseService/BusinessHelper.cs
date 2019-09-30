using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using AdvancedScada.IBaseService.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedScada.BaseService
{
    public class BusinessHelper : BaseBinding
    {
        private static string FILE_LOG = @"C:\IndustrialNetworks.txt";
        public static UOCIndustrialNetworks objUOCIndustrialNetworks = null;

        public static Dictionary<int, Tag> Pumps = new Dictionary<int, Tag>();

        public static ConnectionState objConnectionState = ConnectionState.DISCONNECT;

        /// <summary>
        /// Hàm mở dịch vụ PumpService
        /// </summary>
        public static void OpenPumpServiceHost()
        {
            try
            {
                FILE_LOG = string.Format(@"C:\{0:MM-yyyy}_IndustrialNetworks.txt", DateTime.Now);
                Pumps.Clear();
                //Pumps.Add(1, new Pump() { PumpId = 1, PumpName = string.Format("Bơm {0}", 1) });

                using (StreamWriter writer = new StreamWriter(FILE_LOG, false))
                {
                    writer.WriteLine("===================================================");
                    writer.WriteLine("Designed By Industrial Networks");
                    writer.WriteLine("Phone: 0909.886.483");
                    writer.WriteLine("Skype: katllu");
                    writer.WriteLine("E-mail: hoangluu.automation@gmail.com");
                    writer.WriteLine("Youtube: https://www.youtube.com/industrialnetworks");
                    writer.WriteLine("===================================================");
                    writer.WriteLine("");
                }
                Uri uriWeb = new Uri("http://localhost:8086/PumpService");
                Uri uriWS = new Uri("http://localhost:8088/PumpService");
                WebServiceHost objWebServiceHost = new WebServiceHost(typeof(ReadService));
                WebHttpBinding objWebHttpBinding = BusinessHelper.GetWebHttpBinding();
                WSHttpBinding objWSHttpBinding = BusinessHelper.GetWSHttpBinding();
                objWebServiceHost.AddServiceEndpoint(typeof(IReadService), objWebHttpBinding, uriWeb);
                objWebServiceHost.AddServiceEndpoint(typeof(IReadService), objWSHttpBinding, uriWS);
                objWebServiceHost.Open();
                foreach (ServiceEndpoint item in objWebServiceHost.Description.Endpoints)
                {
                    //Console.WriteLine("Service is host with endpoint: " + item.Address);
                    BusinessHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> Service is host with endpoint: '{1}'", DateTime.Now, item.Address));
                }

                // Kết nối PLC.
                InitializePLC();
            }
            catch (Exception ex)
            {
                BusinessHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(OpenPumpServiceHost): '{1}'", DateTime.Now, ex.Message));
            }
        }

        private static void InitializePLC()
        {
            try
            {
                //Kết nối PLC.
                objUOCIndustrialNetworks = new UOCIndustrialNetworks(ipAddress: "14.225.5.51", port: 2000);
                //objUOCIndustrialNetworks = new UOCIndustrialNetworks(ipAddress: "192.168.1.102", port: 2000);
                ThreadPool.QueueUserWorkItem((th) =>
                {
                    objUOCIndustrialNetworks.Connect();
                });
                objUOCIndustrialNetworks.EventUOSListenning += new EventUOSListenning(AddMessage);
                objUOCIndustrialNetworks.EventUOSAccepting += new EventUOSAccepting(SetConnectionState);
            }
            catch (Exception ex)
            {
                BusinessHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(InitializePLC): '{1}'", DateTime.Now, ex.Message));
            }
        }
        private static void SetConnectionState(ConnectionState connState, string msg)
        {

            try
            {
                objConnectionState = connState;
                switch (connState)
                {
                    case ConnectionState.CONNECT:
                        BusinessHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> Connection State: '{1}'", DateTime.Now, "Connect"));
                        break;
                    case ConnectionState.DISCONNECT:
                        BusinessHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> Connection State: '{1}'", DateTime.Now, "Disconnect"));
                        break;
                }

            }
            catch (Exception ex)
            {
                BusinessHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(OpenPumpServiceHost): '{1}'", DateTime.Now, ex.Message));
            }

        }

        private static void AddMessage(string msg)
        {
            try
            {
                BusinessHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> BusinessHelper(AddMessage): '{1}'", DateTime.Now, msg));
            }
            catch (Exception ex)
            {
                BusinessHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(OpenPumpServiceHost): '{1}'", DateTime.Now, ex.Message));
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

        /// <summary>
        /// Khởi tạo dịch vụ S7ConnectorService.
        /// </summary>
        public static void OpenS7ConnectorServiceHost()
        {
            ServiceHost objServiceHost = null;
            string description = string.Empty;
            try
            {
                NetTcpBinding objNetTcpBinding = BusinessHelper.GetNetTcpBinding();
                objServiceHost = new ServiceHost(typeof(ReadService));
                objServiceHost.AddServiceEndpoint(typeof(IReadService), objNetTcpBinding, "net.tcp://localhost:8099/ConnectorService");
                objServiceHost.Open();
                foreach (ServiceEndpoint item in objServiceHost.Description.Endpoints)
                {
                    //Console.WriteLine("Service is host with endpoint: " + item.Address);
                    BusinessHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> Service is host with endpoint: '{1}'", DateTime.Now, item.Address));
                }
            }
            catch (Exception ex)
            {
                BusinessHelper.AddLog(string.Format("At {0:dd/MM/yyyy hh:mm:ss tt} --> ERROR(InitializeS7ConnectorService): '{1}'", DateTime.Now, ex.Message));
            }
        }
    }
}
