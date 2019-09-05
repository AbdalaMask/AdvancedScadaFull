using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AdvancedScada.DriverBase;
using AdvancedScada.IBaseService;

namespace AdvancedScada.Monitor
{
    public partial class WriteTagForm : KryptonForm
    {
        public IReadService client;
        public WriteTagForm()
        {
            InitializeComponent();
        }
        public WriteTagForm(string Address, IReadService client = null)
        {

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            txtAddress.Text = Address;
            this.client = client;

        }
        public WriteTagForm(List<string> _SelectedTag, IReadService client = null)
        {

            // This call is required by the designer.
            InitializeComponent();

            if (_SelectedTag == null) return;
            // txtAddress.Items.AddRange(_SelectedTag);
            txtAddress.SelectedIndex = 0;
            this.client = client;

        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            //   client = DriverHelper.GetInstance().GetReadService();
            if (client != null)
                client.WriteTag(txtAddress.Text, NumValue.Text);

            Thread.Sleep(50);

            Close();
        }

        private void WriteTagForm_Load(object sender, EventArgs e)
        {
            switch (TagCollection.Tags[txtAddress.Text].DataType)
            {
                case "String":
                    NumValue.Text = "Test";
                    break;
                case "Int":
                case "DInt":
                case "Word":
                case "DWord":
                case "Real1":
                case "Real2":
                    NumValue.Text = "0";
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
