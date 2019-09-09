using AdvancedScada.BaseService;
using AdvancedScada.IBaseService.Common;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Linq;
using System.ServiceModel;
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

                new DriverService().GetStartService();
                host = new DriverService().InitializeReadService();
                host.Opened += host_Opened;
                host.Open();

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
    }
}
