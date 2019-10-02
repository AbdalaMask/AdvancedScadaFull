using AdvancedScada.IBaseService.Common;
using System;
using System.Windows.Forms;

namespace AdvancedScada.Studio.Tools
{
    public partial class ContentHistory : UserControl
    {
        public ContentHistory()
        {
            InitializeComponent();
        }
        private delegate void SetLabelTextInvoker(System.Windows.Forms.Control label, string Text);
        public static void SetLabelText(System.Windows.Forms.Control Label, string Text)
        {
            if (Label.InvokeRequired == true)
            {
                Label.Invoke(new SetLabelTextInvoker(SetLabelText), Label, Text);
            }
            else
            {
                Label.Text += Text;
            }
        }
        private void ContentDocument_Load(object sender, EventArgs e)
        {
            XCollection.eventLoggingMessage += ServiceBase_eventChannelCount;
            XCollection.EventscadaException += ServiceBase_eventChannelCount;
        }
        private void ServiceBase_eventChannelCount(string message)
        {
            SetLabelText( txtHistory,string.Format("{0}" + Environment.NewLine, message));
        }
        private void ServiceBase_eventChannelCount(string classname, string erorr)
        {
            SetLabelText(txtHistory, string.Format("{0} : {1}" + Environment.NewLine, classname, erorr));
        }

        private void barItemClear_Click(object sender, EventArgs e)
        {
            txtHistory.Text = string.Empty;
        }
    }
}
