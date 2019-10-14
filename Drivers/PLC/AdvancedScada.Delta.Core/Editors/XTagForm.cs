using AdvancedScada.DriverBase.Comm;
using AdvancedScada.DriverBase.Devices;
using System;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.Delta.Core.Editors
{
    public partial class XTagForm : AdvancedScada.Management.Editors.XTagForm
    {

        public XTagForm()
        {
            InitializeComponent();
        }
        public XTagForm(Channel chParam, Device dvParam, DataBlock dbParam, Tag tgParam = null)
        {
            InitializeComponent();
            this.dv = dvParam;
            this.db = dbParam;
            this.ch = chParam;
            this.tg = tgParam;
        }
        public string GetIDTag()
        {
            return $"{db.Tags.Count + 1}";
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (tg == null)
                {
                    Tag newTg = new Tag();
                    newTg.ChannelId = int.Parse(txtChannelId.Text);
                    newTg.DeviceId = int.Parse(txtDeviceId.Text);
                    newTg.DataBlockId = int.Parse(txtDataBlockId.Text);
                    newTg.TagId = db.Tags.Count + 1;
                    newTg.TagName = txtTagName.Text;
                    newTg.Address = txtAddress.Text;
                    newTg.Description = txtDesc.Text;
                    newTg.DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType.SelectedItem.ToString());

                    if (eventTagChanged != null) eventTagChanged(newTg, true);
                }
                else
                {
                    tg.ChannelId = int.Parse(txtChannelId.Text);
                    tg.DeviceId = int.Parse(txtDeviceId.Text);
                    tg.DataBlockId = int.Parse(txtDataBlockId.Text);
                    tg.TagName = txtTagName.Text;
                    tg.Address = txtAddress.Text;
                    tg.Description = txtDesc.Text;
                    tg.DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType.SelectedItem.ToString());

                    if (eventTagChanged != null) eventTagChanged(tg, false);

                }
                Close();
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

        }

        private void XTagForm_Load(object sender, EventArgs e)
        {
            try
            {

                cboxDataType.SelectedItem = $"{this.db.DataType}";
                this.txtChannelName.Text = this.ch.ChannelName;
                this.txtDeviceName.Text = this.dv.DeviceName;
                this.txtDataBlock.Text = this.db.DataBlockName;
                txtChannelId.Text = ch.ChannelId.ToString();
                txtDeviceId.Text = Convert.ToString(ch.Devices.Count);
                txtDataBlockId.Text = Convert.ToString(db.DataBlockId);

                if (tg == null)
                {

                    this.Text = "Add Tag";
                    txtTagId.Text = GetIDTag();
                }
                else
                {



                    this.Text = "Edit Tag";
                    txtTagId.Text = tg.TagId.ToString();
                    txtAddress.Text = tg.Address;
                    txtAddress.Enabled = true;
                    cboxDataType.SelectedItem = $"{tg.DataType}";
                    txtTagName.Text = tg.TagName;
                    txtDesc.Text = tg.Description;
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
