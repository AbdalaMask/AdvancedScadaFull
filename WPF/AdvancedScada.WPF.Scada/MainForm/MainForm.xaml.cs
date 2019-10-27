using AdvancedScada.BaseService.Client;
using AdvancedScada.Common;
using AdvancedScada.IBaseService;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.WPF.Scada.MainForm
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        public IReadService client = null;
        public MainForm()
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Application.Current.Shutdown(0);
            }
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                client?.Disconnect(XCollection.CURRENT_MACHINE);
                // client?.Stop();

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
    }
}
