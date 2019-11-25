using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Management.AlarmManager;
using AdvancedScada.Management.BLManager;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.ComponentModel;
using System.Linq;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Studio.Alarms
{
    public delegate void EventAlarmChanged(ClassAlarm Alarm, bool isNew);
    public partial class FrmAddAlarm : KryptonForm
    {
        private BindingList<Tag> bS7Tags;
        public EventAlarmChanged eventAlarmChanged = null;

        private readonly ClassAlarm Alarm;
        private readonly ChannelService objChannelManager;
        private readonly DataBlockService objDataBlockManager;
        private readonly DeviceService objDeviceManager;
        private TagService objTagManager;
        public FrmAddAlarm()
        {
            objChannelManager = ChannelService.GetChannelManager();
            objDeviceManager = DeviceService.GetDeviceManager();
            objDataBlockManager = DataBlockService.GetDataBlockManager();
            objTagManager = TagService.GetTagManager();
            InitializeComponent();
        }
        public FrmAddAlarm(ClassAlarm AlarmCurrent = null)
        {
            objChannelManager = ChannelService.GetChannelManager();
            objDeviceManager = DeviceService.GetDeviceManager();
            objDataBlockManager = DataBlockService.GetDataBlockManager();
            objTagManager = TagService.GetTagManager();
            InitializeComponent();

            Alarm = AlarmCurrent;

        }
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Alarm == null)
                {
                    var newAlarm = new ClassAlarm();

                    newAlarm.TriggerTeg = txtTagName.Text;
                    newAlarm.Channel = txtChannel.Text;
                    newAlarm.Name = ColName.Text;
                    newAlarm.DataBlock = txtDataBlock.Text;
                    newAlarm.Device = txtDevice.Text;
                    newAlarm.AlarmCalss = ColAlarmCalss.Text;
                    newAlarm.AlarmText = ColAlarmText.Text;
                    newAlarm.Value = ColValue.Text;

                    eventAlarmChanged?.Invoke(newAlarm, true);
                }
                else
                {
                    Alarm.TriggerTeg = txtTagName.Text;
                    Alarm.Channel = txtChannel.Text;
                    Alarm.Name = ColName.Text;
                    Alarm.DataBlock = txtDataBlock.Text;
                    Alarm.Device = txtDevice.Text;
                    Alarm.AlarmCalss = ColAlarmCalss.Text;
                    Alarm.AlarmText = ColAlarmText.Text;
                    Alarm.Value = ColValue.Text;

                    eventAlarmChanged?.Invoke(Alarm, false);

                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void FrmAddAlarm_Load(object sender, System.EventArgs e)
        {
            txtChannel.DataSource = objChannelManager.Channels.ToList();
            txtChannel.DisplayMember = "ChannelName";
            txtChannel.ValueMember = "ChannelId";

            if (Alarm == null)
            {
                Text = "Add Alarm";
            }
            else
            {
                Text = "Edit Alarm";
                txtTagName.Text = Alarm.TriggerTeg;
                txtChannel.Text = Alarm.Channel;
                ColName.Text = Alarm.Name;
                txtDataBlock.Text = Alarm.DataBlock;
                txtDevice.Text = Alarm.Device;
                ColAlarmText.Text = Alarm.AlarmText;
                ColAlarmCalss.Text = Alarm.AlarmCalss;
                ColValue.Text = Alarm.Value;

            }
        }

        private void txtChannel_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                var ch = objChannelManager.GetByChannelName(txtChannel.Text);
                txtDevice.DataSource = ch.Devices.ToList();
                txtDevice.DisplayMember = "DeviceName";
                txtDevice.ValueMember = "DeviceId";
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void txtDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var chCurrent = objChannelManager.GetByChannelName(txtChannel.Text);
                var dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, txtDevice.Text);
                txtDataBlock.DataSource = dvCurrent.DataBlocks.ToList();
                txtDataBlock.DisplayMember = "DataBlockName";
                txtDataBlock.ValueMember = "DataBlockId";
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void txtDataBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var chCurrent = objChannelManager.GetByChannelName(txtChannel.Text);
                var dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, txtDevice.Text);
                var dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, txtDataBlock.Text);
                bS7Tags = new BindingList<Tag>(dbCurrent.Tags);
                txtTagName.DataSource = dbCurrent.Tags.ToList();
                txtTagName.DisplayMember = "TagName";
                txtTagName.ValueMember = "TagId";
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
