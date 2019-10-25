using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using System;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Siemens.Core.Editors
{
    public partial class XTagForm : AdvancedScada.Management.Editors.XTagForm
    {
        private int TagsCount = 1;
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
                foreach (var item in dv.DataBlocks)
                {

                    TagsCount += item.Tags.Count;
                    if (db != null)
                    {
                        if (db.DataBlockName.Equals(item.DataBlockName)) break;
                    }

                }
                if (tg == null)
                {
                    Tag newTg = new Tag();
                    newTg.ChannelId = int.Parse(txtChannelId.Text);
                    newTg.DeviceId = int.Parse(txtDeviceId.Text);
                    newTg.DataBlockId = int.Parse(txtDataBlockId.Text);
                    newTg.TagId = db.Tags.Count + 1;
                    newTg.TagName = txtTagName.Text;
                    newTg.Address = string.Format("{0}{1}", txtAddress.Text, txtStartAddress.Text);
                    newTg.Description = txtDesc.Text;
                    newTg.DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType.SelectedItem.ToString());

                    if (eventTagChanged != null) eventTagChanged(newTg, true);
                    txtTagId.Text = GetIDTag();
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
                cboxDataType.Items.AddRange(System.Enum.GetNames(typeof(DataTypes)));

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

        private void CboxDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tg == null)
            {
                switch (cboxDataType.SelectedIndex)
                {
                    case 0:
                        txtAddress.Text = string.Format("DB{0}.DBX", db.StartAddress);
                        break;
                    case 1:
                        txtAddress.Text = string.Empty;
                        break;
                    case 2:
                        txtAddress.Text = string.Format("DB{0}.DBW", db.StartAddress);
                        break;
                    case 3:
                        txtAddress.Text = string.Empty;
                        break;
                    case 4:
                        txtAddress.Text = string.Format("DB{0}.DBW", db.StartAddress);
                        break;
                    case 5:
                        txtAddress.Text = string.Format("DB{0}.DBD", db.StartAddress);
                        break;
                    case 6:
                        txtAddress.Text = string.Format("DB{0}.DBD", db.StartAddress);
                        break;
                    case 7:
                        txtAddress.Text = string.Format("DB{0}.DBB", db.StartAddress);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
