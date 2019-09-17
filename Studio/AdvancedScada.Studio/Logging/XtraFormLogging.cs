using ComponentFactory.Krypton.Toolkit;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AdvancedScada.Studio.Logging
{
    public partial class XtraFormLogging : KryptonForm
    {

        public XtraFormLogging()
        {
            InitializeComponent();
        }

        private void XtraFormLogging_Load(object sender, EventArgs e)
        {

            var bindingList = new BindingList<Logger>(Logger.Loggers);
            var source = new BindingSource(bindingList, null);
            DGFormLogging.DataSource = source;
        }
    }
}
