using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using ComponentFactory.Krypton.Navigator;
using System;
using System.Collections.Generic;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Siemens.Core.Editors
{
    public partial class XDataBlockForm : AdvancedScada.Management.Editors.XDataBlockForm
    {


        int TagsCount = 1;

        public XDataBlockForm()
        {
            InitializeComponent();
        }
        public XDataBlockForm(Channel chParam, Device dvParam, DataBlock dbParam = null)
        {
            InitializeComponent();
            ch = chParam;
            dv = dvParam;
            db = dbParam;
        }





        private void XDataBlockForm_Load(object sender, EventArgs e)
        {

            try
            {
                txtDeviceName.Text = dv.DeviceName;
                txtChannelName.Text = ch.ChannelName;
                txtChannelId.Text = ch.ChannelId.ToString();
                txtDeviceId.Text = dv.DeviceId.ToString();
                cboxDataType2.DataSource = System.Enum.GetNames(typeof(DataTypes));
                cboxDataType.DataSource = System.Enum.GetNames(typeof(DataTypes));
                if (db == null)
                {

                    Text = "Add DataBlock   " + ch.ChannelTypes;
                    txtDataBlockId.Text = Convert.ToString(dv.DataBlocks.Count + 1);
                    txtDataBlock.Text = "DataBlock" + Convert.ToString(dv.DataBlocks.Count + 1);
                }
                else
                {

                    Text = "Edit DataBlock    " + ch.ChannelTypes;
                    txtChannelId.Text = db.ChannelId.ToString();
                    txtDeviceId.Text = db.DeviceId.ToString();
                    txtDataBlock.Text = db.DataBlockName;
                    txtDBNumber.Text = db.MemoryType;
                    txtDesc.Text = db.Description;
                    txtDataBlockId.Text = $"{db.DataBlockId}";
                    cboxDataType.SelectedItem = string.Format("{0}", db.DataType);

                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        public void AddressCreateTagSiemensIsArray(DataBlock db, bool IsNew, int TagsCount = 1)
        {
            if (IsNew == false) db.Tags.Clear();
            foreach (var item in dv.DataBlocks)
            {

                TagsCount += item.Tags.Count;
                if (db != null)
                {
                    if (db.DataBlockName.Equals(item.DataBlockName)) break;
                }

            }
            if (chkCreateTag.Checked)
                for (var i = 0; i < txtAddressLength.Value; i++)
                {
                    var tg = new Tag()
                    {
                        TagId = i + 1,
                        ChannelId = int.Parse(txtChannelId.Text),
                        DeviceId = int.Parse(txtDeviceId.Text),
                        DataBlockId = int.Parse(txtDataBlockId.Text),
                        TagName =
                        $"TAG{i + TagsCount:d5}",
                        Address = $"{txtDomain.Text.Trim()}{txtStartAddress.Value + i*2}",
                        DataType =
                        (DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType2.SelectedItem.ToString()),
                        Description = $"{txtDesc.Text} {i + 1}"
                    };
                    db.Tags.Add(tg);
                }
        }

        private void cboxDataType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        string Tab = string.Empty;
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {


                //if ((string.IsNullOrEmpty(txtDBNumber.Text)
                //                            || string.IsNullOrWhiteSpace(txtDBNumber.Text)))
                //{
                //    DxErrorProvider1.SetError(txtDBNumber, "The Prefix is empty");
                //}

                if (string.IsNullOrEmpty(txtDataBlock.Text)
                    || string.IsNullOrWhiteSpace(txtDataBlock.Text))
                {
                    DxErrorProvider1.SetError(txtDataBlock, "The datablock is empty");
                }
                else
                {

                    if (db == null)
                    {

                        if(Tab== "IsArray")
                        {
                            var dbNew = new DataBlock()
                            {
                                ChannelId = ch.ChannelId,
                                DeviceId = dv.DeviceId,
                                DataBlockId = dv.DataBlocks.Count + 1,
                                DataBlockName = txtDataBlock.Text,
                                TypeOfRead = string.Empty,
                                StartAddress = ushort.Parse(txtStartAddress.Text),
                                MemoryType = txtDomain.Text,
                                Description = txtDesc.Text,
                                Length = (ushort)txtAddressLength.Value,
                                DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", cboxDataType2.SelectedItem)),
                                IsArray = true,
                                Tags = new List<Tag>()
                            };
                            AddressCreateTagSiemensIsArray(dbNew, true, TagsCount);

                            eventDataBlockChanged?.Invoke(dbNew, true);
                        }
                        else
                        {
                         var dbNew = new DataBlock()
                        {
                            ChannelId = ch.ChannelId,
                            DeviceId = dv.DeviceId,
                            DataBlockId = dv.DataBlocks.Count + 1,
                            DataBlockName = txtDataBlock.Text,
                            TypeOfRead = string.Empty,
                            StartAddress = 0,
                            MemoryType = txtDBNumber.Text,
                            Description = txtDesc.Text,
                            Length = 0,
                            DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", cboxDataType.SelectedItem)),
                            IsArray = false,
                            Tags = new List<Tag>()
                        };
                          

                            eventDataBlockChanged?.Invoke(dbNew, true);
                        }
                      

                    
                        Close();
                    }
                    else
                    {
                        db.ChannelId = db.ChannelId;
                        db.DeviceId = db.DeviceId;
                        db.DataBlockId = int.Parse(txtDataBlockId.Text);
                        db.DataBlockName = txtDataBlock.Text;
                        db.TypeOfRead = string.Empty;
                        db.StartAddress = ushort.Parse(txtDBNumber.Text);
                        db.MemoryType = txtDBNumber.Text;
                        db.Length = (ushort)txtAddressLength.Value;
                        db.Description = txtDesc.Text;
                        db.DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", cboxDataType.SelectedItem));
                        db.IsArray = false;



                        eventDataBlockChanged?.Invoke(db, false);
                        Close();
                    }
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

        private void kryptonNavigator1_TabClicked(object sender, KryptonPageEventArgs e)
        {
            if (e.Item.Text == "DB")
            {
                Tab = "DB";
            }
            else if (e.Item.Text == "IsArray")
            {
                Tab = "IsArray";
            }
        }

        private void kryptonNavigator1_Selected(object sender, KryptonPageEventArgs e)
        {

        }
    }
}

