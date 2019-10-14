using AdvancedScada.DriverBase.Devices;
using System;
using System.Collections.Generic;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.Delta.Core.Editors
{
    public partial class XDeviceForm : AdvancedScada.Management.Editors.XDeviceForm
    {


        public XDeviceForm()
        {
            InitializeComponent();
        }
        public XDeviceForm(Channel chParam, Device dvPara = null)
        {
            InitializeComponent();
            ch = chParam;
            dv = dvPara;
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDeviceName.Text)
                               || string.IsNullOrWhiteSpace(txtDeviceName.Text))
                    DxErrorProvider1.SetError(txtDeviceName, "The device name is empty");
                else
                {
                    DxErrorProvider1.Clear();
                    if (dv == null)
                    {
                        Device dvNew = new Device();
                        dvNew.DeviceId = ch.Devices.Count + 1;
                        dvNew.SlaveId = (short)txtSlaveId.Value;
                        dvNew.DeviceName = txtDeviceName.Text;
                        dvNew.Description = txtDesp.Text;
                        dvNew.DataBlocks = new List<DataBlock>();
                        EventscadaLogger?.Invoke(1, "DeviceManager", $"{DateTime.Now}", "Add Device");

                        if (eventDeviceChanged != null) eventDeviceChanged(dvNew, true);

                    }
                    else
                    {
                        dv.SlaveId = (short)txtSlaveId.Value;
                        dv.DeviceName = txtDeviceName.Text;
                        dv.Description = txtDesp.Text;
                        EventscadaLogger?.Invoke(1, "DeviceManager", $"{DateTime.Now}", "Editor Device");

                        if (eventDeviceChanged != null) eventDeviceChanged(dv, false);
                    }
                    Close();
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void XUserDeviceForm_Load(object sender, EventArgs e)
        {
            try
            {
                txtChannelName.Text = ch.ChannelName;
                txtChannelID.Text = ch.ChannelId.ToString();

                if (dv != null)
                {
                    this.Text = "Edit Device  " + ch.ChannelTypes;
                    txtSlaveId.Value = dv.SlaveId;
                    txtDeviceName.Text = dv.DeviceName;
                    txtDeviceId.Text = $"{dv.DeviceId}";
                    txtDesp.Text = dv.Description;
                }
                else
                {
                    this.Text = "Add Device  " + ch.ChannelTypes;
                    txtDeviceId.Text = Convert.ToString(ch.Devices.Count + 1);
                    txtDeviceName.Text = "PLC" + Convert.ToString(ch.Devices.Count + 1);
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
