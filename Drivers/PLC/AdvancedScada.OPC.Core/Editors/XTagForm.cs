using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using System;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.OPC.Core.Editors
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
            dv = dvParam;
            db = dbParam;
            ch = chParam;
            tg = tgParam;
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
                    Tag newTg = new Tag
                    {
                        ChannelId = int.Parse(txtChannelId.Text),
                        DeviceId = int.Parse(txtDeviceId.Text),
                        DataBlockId = int.Parse(txtDataBlockId.Text),
                        TagId = db.Tags.Count + 1,
                        TagName = txtTagName.Text,
                        Address = txtAddress.Text,
                        Description = txtDesc.Text,
                        DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType.SelectedItem.ToString())
                    };

                    if (eventTagChanged != null)
                    {
                        eventTagChanged(newTg, true);
                    }
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

                    if (eventTagChanged != null)
                    {
                        eventTagChanged(tg, false);
                    }
                }
                Close();
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

        }

        private void XTagForm_Load(object sender, EventArgs e)
        {
            try
            {

                cboxDataType.SelectedItem = $"{db.DataType}";
                txtChannelName.Text = ch.ChannelName;
                txtDeviceName.Text = dv.DeviceName;
                txtDataBlock.Text = db.DataBlockName;
                txtChannelId.Text = ch.ChannelId.ToString();
                txtDeviceId.Text = Convert.ToString(ch.Devices.Count);
                txtDataBlockId.Text = Convert.ToString(db.DataBlockId);

                if (tg == null)
                {

                    Text = "Add Tag";
                    txtTagId.Text = GetIDTag();
                }
                else
                {



                    Text = "Edit Tag";
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
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
