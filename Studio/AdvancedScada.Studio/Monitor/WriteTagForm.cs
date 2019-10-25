using AdvancedScada.Common;
using AdvancedScada.DriverBase;
using AdvancedScada.IBaseService;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AdvancedScada.Studio.Monitor
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

            this.client = client;

        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            //   client = DriverHelper.GetInstance().GetReadService();
            if (client != null)
                client.WriteTag(txtAddress.Text, txtValue.Text);

            Thread.Sleep(50);

            Close();
        }

        private void WriteTagForm_Load(object sender, EventArgs e)
        {
            switch (TagCollection.Tags[txtAddress.Text].DataType)
            {
                case DriverBase.DataTypes.Bit:
                    break;
                case DriverBase.DataTypes.Byte:
                    break;
                case DriverBase.DataTypes.Short:
                case DriverBase.DataTypes.UShort:
                case DriverBase.DataTypes.Int:
                case DriverBase.DataTypes.UInt:
                case DriverBase.DataTypes.Long:
                case DriverBase.DataTypes.ULong:
                    txtValue.Text = "0";
                    break;
                case DriverBase.DataTypes.Float:
                case DriverBase.DataTypes.Double:
                    txtValue.Text = "0.0";
                    break;
                case DriverBase.DataTypes.String:
                    txtValue.Text = "Test";
                    break;
                default:
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
