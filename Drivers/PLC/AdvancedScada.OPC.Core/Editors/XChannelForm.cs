using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Management.BLManager;
using Opc;
using OpcCom;
using System;
using System.Collections.Generic;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.OPC.Core.Editors
{
    public partial class XChannelForm : AdvancedScada.Management.Editors.XChannelForm
    {




        public XChannelForm()
        {
            InitializeComponent();
            GetOpcServers();
        }
        public XChannelForm(string DriverTypes, ChannelService chm = null, Channel chCurrent = null)
        {
            InitializeComponent();
            objChannelManager = chm;
            ch = chCurrent;
            GetOpcServers();

        }

        private void GetOpcServers()
        {
            try
            {
                var se = new ServerEnumerator();

                var servers = se.GetAvailableServers(Specification.COM_DA_20);
                serversComboBox.Items.Clear();
                foreach (var item in servers) serversComboBox.Items.Add(item.Name);
            }
            catch (Exception)
            {
            }
        }

        private void XChannelForm_Load(object sender, EventArgs e)
        {

            try
            {


                if (ch != null)
                {
                    Text = "Edit Channel";
                    txtChannelName.Text = ch.ChannelName;
                    txtDesc.Text = ch.ChannelName;
                }
                else
                {
                    Text = "Add Channel";
                }
            }
            catch (Exception)
            {
            }




        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {

                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txtChannelName.Text)
                               || string.IsNullOrWhiteSpace(txtChannelName.Text))
                {
                    errorProvider1.SetError(txtChannelName, "The channel name is empty");
                    return;
                }
                DIEthernet die = null;

                die = new DIEthernet
                {
                    ChannelName = txtChannelName.Text,
                    ChannelTypes = "OPC",
                    CPU = serversComboBox.Text,
                    Rack = 0,
                    Slot = 0,
                    IPAddress = "127.0.0.1",
                    Port = 502,
                    ConnectionType = "Ethernet",
                    Mode = serverTextBox.Text
                };

                if (ch == null)
                {

                    die.ChannelId = objChannelManager.Channels.Count + 1;
                    die.Devices = new List<Device>();
                    if (eventChannelChanged != null) eventChannelChanged(die, true);
                    //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    EventscadaLogger?.Invoke(1, "ChannelManager", $"{DateTime.Now}", "Add Channel");

                }
                else
                {

                    die.ChannelId = ch.ChannelId;
                    die.Devices = ch.Devices;
                    if (eventChannelChanged != null) eventChannelChanged(die, false);
                    //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    EventscadaLogger?.Invoke(1, "ChannelManager", $"{DateTime.Now}", "Editor Channel");

                }

                btnNext.Text = "Finish";
                btnBlack.Enabled = true;


            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            try
            {
                TabControlOPC.SelectedIndex = 0;
                btnBlack.Enabled = false;
                btnNext.Text = "Next >";
            }
            catch (Exception)
            {
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            GetOpcServers();
        }
    }
}
