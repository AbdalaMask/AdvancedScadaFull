using AdvancedScada.BaseService;
using AdvancedScada.DriverBase;
using AdvancedScada.IBaseService.Common;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.ServiceModel;
using System.Windows.Forms;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.Studio.Service
{
    public partial class FormServerUtils : KryptonForm
    {
        public bool close = true;
        public ServiceHost host;
        public FormServerUtils()
        {

            InitializeComponent();
        }
        public ServiceHost InitializeTags(bool Start = false)
        {
            ServiceHost host = null;

            try
            {

                new DriverService().InitializePLC();
                host = new DriverService().InitializeReadService();
                host.Opened += host_Opened;
                host.Open();

                eventAddMessage += new EventListenning(AddMessage);
                eventConnectionState += new EventConnectionState(SetConnectionState);

                foreach (var se in host.Description.Endpoints)
                {
                    string[] row = { Convert.ToString(DateTime.Now), string.Format("{0}", se.Binding.Name), string.Format("{0}", se.Address) };


                    DGServerUtils.Rows.Add(row);
                }


                if (host.State == CommunicationState.Opened) txtStatus.Text = "The Server is running";



            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }


            this.Text = "ServerUtils : AdvancedScada";
            return host;
        }
        private ConnectionState _ConnState = ConnectionState.DISCONNECT;

        private void SetConnectionState(ConnectionState connState, string msg)
        {

            try
            {

                if (!this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        if (connState != _ConnState)
                        {
                            switch (connState)
                            {
                                case ConnectionState.CONNECT:
                                    lblConnectState.Image = Properties.Resources.Connect16px;
                                    lblConnectState.Text = "Connected";
                                    break;
                                case ConnectionState.DISCONNECT:
                                    lblConnectState.Image = Properties.Resources.Disconnect16px;
                                    lblConnectState.Text = "Disonnect";
                                    break;
                            }
                            this.AddLog(msg);
                            _ConnState = connState;
                        }
                    });
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
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

        private void AddLog(string msg)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        ///*txtLog.Text +=*/ string.Format("At {0: dd/MM/yyyy HH:mm:ss}--> {1}" + Environment.NewLine, DateTime.Now, msg);
                    });
                }

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        private void FormServerUtils_Load(object sender, EventArgs e)
        {
            try
            {



                XCollection.EventChannelCount += ServiceBase_eventChannelCount;

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);

            }

            try
            {

                host = InitializeTags(true);

            }
            catch (Exception ex)
            {

                string[] row = { Convert.ToString(DateTime.Now), ex.Message };


                DGServerUtils.Rows.Add(row);
            }
        }
        private void ServiceBase_eventChannelCount(int ChannelCount, bool IsNew)
        {
            if (IsNew)
            {
                var ChannelCount2 = int.Parse(txtChannelCount.Text);

                txtChannelCount.Text = $"{ChannelCount2 + ChannelCount}";
            }
            else
            {
                var ChannelCount2 = int.Parse(txtChannelCount.Text);

                txtChannelCount.Text = $"{ChannelCount2 - ChannelCount}";
            }
        }

        public void host_Opened(object sender, EventArgs e)
        {

            txtStatus.Text = "The Server is running";
        }

        private void DGServerUtils_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
