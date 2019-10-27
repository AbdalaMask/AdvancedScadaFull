using System;
using Microsoft.Owin;
using Owin;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using AdvancedScada.BaseService.Client;
using AdvancedScada.DriverBase;
using AdvancedScada.IBaseService;
using AdvancedScada.Common;
using static AdvancedScada.Common.XCollection;
[assembly: OwinStartup(typeof(AdvancedScada.WEB.Scada.Startup))]

namespace AdvancedScada.WEB.Scada
{
    public class Startup
    {
        public IReadService client = null;
        public void Configuration(IAppBuilder app)
        {
            try
            {

                ReadServiceCallbackClient.LoadTagCollection();
                XCollection.CURRENT_MACHINE = new Machine
                {
                    MachineName = Environment.MachineName,
                    Description = "Free"
                };
                IPAddress[] hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress iPAddress in hostAddresses)
                {
                    if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        XCollection.CURRENT_MACHINE.IPAddress = $"{iPAddress}";
                        break;
                    }
                }
                client = ClientDriverHelper.GetInstance().GetReadService();
                client.Connect(XCollection.CURRENT_MACHINE);

            }
            catch (CommunicationException ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
    }
}
