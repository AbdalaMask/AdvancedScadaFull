using AdvancedScada.BaseService.Client;
using AdvancedScada.DriverBase;
using AdvancedScada.IBaseService;
using AdvancedScada.IBaseService.Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Windows.Forms;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.HMI
{
    public partial class Form1 : Form
    {
        public IReadService client = null;
        public Form1()
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
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                client?.Disconnect(XCollection.CURRENT_MACHINE);


            }
            catch (CommunicationException ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
    }
}
