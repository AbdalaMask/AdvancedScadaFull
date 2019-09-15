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
                case DriverBase.Comm.DataTypes.Bit:
                    break;
                case DriverBase.Comm.DataTypes.Byte:
                    break;
                case DriverBase.Comm.DataTypes.Short:
                    NumValue.Text = "0";
                    break;
                case DriverBase.Comm.DataTypes.UShort:
                    break;
                case DriverBase.Comm.DataTypes.Int:
                    break;
                case DriverBase.Comm.DataTypes.UInt:
                    break;
                case DriverBase.Comm.DataTypes.Long:
                    break;
                case DriverBase.Comm.DataTypes.ULong:
                    break;
                case DriverBase.Comm.DataTypes.Float:
                    break;
                case DriverBase.Comm.DataTypes.Double:
                    break;
                case DriverBase.Comm.DataTypes.String:
                    NumValue.Text = "Test";
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
