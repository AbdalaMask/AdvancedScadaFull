using AdvancedScada.BaseService.Client;
using AdvancedScada.DriverBase;
using AdvancedScada.IBaseService;
using AdvancedScada.IBaseService.Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.WPF.Scada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IReadService client = null;
        public MainWindow()
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
