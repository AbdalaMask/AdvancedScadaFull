using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Management.BLManager;
using AdvancedScada.Management.SQLManager;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.ComponentModel;
using System.Linq;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Studio.LinkToSQL
{
    public partial class XAddColumn : KryptonForm
    {
        public delegate void EventColumnChanged(Column tg);
        private BindingList<Tag> bS7Tags;

        private Channel ch;
        private readonly Column Co;
        private readonly DataBase DBS;

        public EventColumnChanged eventColumnChanged = null;
        private readonly ChannelService objChannelManager;
        private readonly DataBlockService objDataBlockManager;
        private readonly DeviceService objDeviceManager;
        private readonly TagService objTagManager;
        private readonly Server SQL;
        private readonly Table Tb;
        public XAddColumn()
        {
            objChannelManager = ChannelService.GetChannelManager();
            objDeviceManager = DeviceService.GetDeviceManager();
            objDataBlockManager = DataBlockService.GetDataBlockManager();
            objTagManager = TagService.GetTagManager();
            InitializeComponent();
        }

        public XAddColumn(Server SQLParam, DataBase dbsParam, Table tbParam, Column coParam = null)

        {
            InitializeComponent();
            DBS = dbsParam;
            Tb = tbParam;
            SQL = SQLParam;
            Co = coParam;
            objChannelManager = ChannelService.GetChannelManager();
            objDeviceManager = DeviceService.GetDeviceManager();
            objDataBlockManager = DataBlockService.GetDataBlockManager();
            objTagManager = TagService.GetTagManager();


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Co == null)
                {
                    Column newColumn = new Column
                    {
                        ColumnId = Tb.Columns.Count + 1
                    };
                    ;
                    newColumn.TagName = txtTagName.Text;
                    newColumn.Channel = txtChannel.Text;
                    newColumn.ColumnName = txtColumnName.Text;
                    newColumn.DataBlock = txtDataBlock.Text;
                    newColumn.Device = txtDevice.Text;
                    newColumn.Cycle = txtCycle.Text;
                    newColumn.Description = txtDesc.Text;
                    ColumnManager.Add(Tb, newColumn);
                    eventColumnChanged?.Invoke(newColumn);
                }
                else
                {
                    Co.ColumnId = Tb.Columns.Count + 1; ;
                    Co.TagName = txtTagName.Text;
                    Co.Channel = txtChannel.Text;
                    Co.ColumnName = txtColumnName.Text;
                    Co.DataBlock = txtDataBlock.Text;
                    Co.Device = txtDevice.Text;
                    Co.Cycle = txtCycle.Text;
                    Co.Description = txtDesc.Text;
                    ColumnManager.Update(Tb, Co);
                    eventColumnChanged?.Invoke(Co);

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

        private void AddColumn_Load(object sender, EventArgs e)
        {
            try
            {
                txtSQLDataBase.Text = DBS.DataBaseName;
                txtSQLTable.Text = Tb.TableName;

                txtChannel.DataSource = objChannelManager.Channels.ToList();
                txtChannel.DisplayMember = "ChannelName";
                txtChannel.ValueMember = "ChannelId";
                Utils.DriverLinkToSQL.LinkToSQL linkToSql = new AdvancedScada.Utils.DriverLinkToSQL.LinkToSQL();

                txtColumnName.DataSource = linkToSql.AddColumn(txtSQLTable.Text, SQL.ServerName, DBS.DataBaseName);
                txtColumnName.DisplayMember = "ColumnName";
                txtColumnName.ValueMember = "ColumnName";
                if (Co == null)
                {
                    Text = "Add Column";
                }
                else
                {
                    Text = "Edit Column";
                    txtTagName.Text = Co.TagName;

                    txtChannel.Text = Co.Channel;
                    txtColumnName.Text = Co.ColumnName;
                    txtDataBlock.Text = Co.DataBlock;
                    txtDevice.Text = Co.Device;
                    txtDesc.Text = Co.Description;

                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }





        private void TxtChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ch = objChannelManager.GetByChannelName(txtChannel.Text);
                txtDevice.DataSource = ch.Devices.ToList();
                txtDevice.DisplayMember = "DeviceName";
                txtDevice.ValueMember = "DeviceId";
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        private void TxtDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Channel chCurrent = objChannelManager.GetByChannelName(txtChannel.Text);
                Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, txtDevice.Text);
                txtDataBlock.DataSource = dvCurrent.DataBlocks.ToList();
                txtDataBlock.DisplayMember = "DataBlockName";
                txtDataBlock.ValueMember = "DataBlockId";
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        private void TxtDataBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Channel chCurrent = objChannelManager.GetByChannelName(txtChannel.Text);
                Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, txtDevice.Text);
                DataBlock dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, txtDataBlock.Text);
                bS7Tags = new BindingList<Tag>(dbCurrent.Tags);
                txtTagName.DataSource = dbCurrent.Tags.ToList();
                txtTagName.DisplayMember = "TagName";
                txtTagName.ValueMember = "TagId";
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }
    }
}
