using System;
using System.Linq;
using System.Windows.Forms;
using AdvancedScada.IBaseService.Common;

namespace AdvancedScada.Studio.Tools
{
    public partial class ContentHistory : UserControl
    {
        public ContentHistory()
        {
            InitializeComponent();
        }

        private void ContentDocument_Load(object sender, EventArgs e)
        {
            XCollection.eventLoggingMessage += ServiceBase_eventChannelCount;
            XCollection.EventscadaException += ServiceBase_eventChannelCount;
        }
        private void ServiceBase_eventChannelCount(string message)
        {
            txtHistory.Text += string.Format("{0}" + Environment.NewLine, message);
        }
        private void ServiceBase_eventChannelCount(string classname, string erorr)
        {
            txtHistory.Text += string.Format("{0} : {1}" + Environment.NewLine, classname, erorr);
        }

        private void barItemClear_Click(object sender, EventArgs e)
        {
            txtHistory.Text = string.Empty;
        }
    }
}
