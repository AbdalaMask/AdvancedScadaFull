using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using System;
using System.Collections.Generic;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Modbus.Core.Editors
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

        #region Modbus
        public void AddressCreateTagModbus(DataBlock db, bool IsNew, int TagsCount = 1)
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
            {

                for (var i = 0; i < txtAddressLength.Value; i++)
                {
                    var tg = new Tag()
                    {
                        ChannelId = int.Parse(txtChannelId.Text),
                        DeviceId = int.Parse(txtDeviceId.Text),
                        DataBlockId = int.Parse(txtDataBlockId.Text),
                        TagId = i + 1,
                        TagName = $"TAG{i + TagsCount:d5}",
                        Address = $"{txtStartAddress.Value + i}",
                        DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType.SelectedItem.ToString()),
                        Description = $"{txtDesc.Text} {i + 1}"
                    };
                    db.Tags.Add(tg);
                }
            }


        }
        #endregion




        private void XDataBlockForm_Load(object sender, EventArgs e)
        {

            try
            {
                txtDeviceName.Text = dv.DeviceName;
                txtChannelName.Text = ch.ChannelName;
                txtChannelId.Text = ch.ChannelId.ToString();
                txtDeviceId.Text = dv.DeviceId.ToString();
                CboxTypeOfRead.SelectedIndex = 0;
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
                    CboxTypeOfRead.Text = db.TypeOfRead;
                    txtStartAddress.Value = db.StartAddress;
                    txtAddressLength.Value = db.Length;
                    txtDomain.Text = db.MemoryType;
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



        private void cboxDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                //switch ((DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType.SelectedValue.ToString()))
                //{
                //    case DataTypes.Bit:
                //        switch (ch.ChannelTypes)

                //        {
                //            case "LSIS":
                //                txtAddressLength.Maximum = 1;
                //                chkX10.Enabled = true;
                //                txtAddressLength.Minimum = 1;
                //                break;
                //            case "Modbus":
                //                txtAddressLength.Maximum = 2000;
                //                txtAddressLength.Minimum = 1;
                //                break;
                //            default:
                //                break;
                //        }

                //        break;
                //    case DataTypes.Byte:
                //        break;
                //    case DataTypes.Short:
                //        switch (ch.ChannelTypes)

                //        {
                //            case "LSIS":
                //                if (ch.ConnectionType == "SerialPort")
                //                {
                //                    txtAddressLength.Maximum = 60;
                //                    txtAddressLength.Minimum = 1;
                //                    chkX10.Enabled = false;
                //                }

                //                else
                //                {
                //                    txtAddressLength.Maximum = 120;
                //                    txtAddressLength.Minimum = 1;
                //                    chkX10.Enabled = false;
                //                }
                //                break;
                //            case "Modbus":
                //                txtAddressLength.Maximum = 120;
                //                txtAddressLength.Minimum = 1;
                //                break;
                //            default:
                //                break;
                //        }

                //        break;
                //    case DataTypes.UShort:
                //        break;
                //    case DataTypes.Int:
                //        break;
                //    case DataTypes.UInt:
                //        break;
                //    case DataTypes.Long:                        
                //    case DataTypes.ULong:
                //    case DataTypes.Float:
                //    case DataTypes.Double:
                //        txtAddressLength.Maximum = 60;
                //        txtAddressLength.Minimum = 1;
                //        chkX10.Enabled = false;
                //        break;
                //    case DataTypes.String:
                //        break;
                //    default:
                //        break;
                //}

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void chkCreateTag_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtDomain.Enabled = chkCreateTag.Checked;
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                if (chkCreateTag.Checked && (string.IsNullOrEmpty(txtDomain.Text)
                                            || string.IsNullOrWhiteSpace(txtDomain.Text)))
                {
                    DxErrorProvider1.SetError(txtDomain, "The Prefix is empty");
                }

                if (string.IsNullOrEmpty(txtDataBlock.Text)
                    || string.IsNullOrWhiteSpace(txtDataBlock.Text))
                {
                    DxErrorProvider1.SetError(txtDataBlock, "The datablock is empty");
                }
                else
                {

                    if (db == null)
                    {
                        var dbNew = new DataBlock()
                        {
                            ChannelId = ch.ChannelId,
                            DeviceId = dv.DeviceId,
                            DataBlockId = dv.DataBlocks.Count + 1,
                            DataBlockName = txtDataBlock.Text,
                            TypeOfRead = CboxTypeOfRead.Text,
                            StartAddress = (ushort)txtStartAddress.Value,
                            MemoryType = txtDomain.Text,
                            Description = txtDesc.Text,
                            Length = (ushort)txtAddressLength.Value,
                            DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", cboxDataType.SelectedItem)),
                            IsArray = true,
                            Tags = new List<Tag>()
                        };


                        AddressCreateTagModbus(dbNew, true, TagsCount);

                        eventDataBlockChanged?.Invoke(dbNew, true);
                        Close();
                    }
                    else
                    {
                        db.ChannelId = db.ChannelId;
                        db.DeviceId = db.DeviceId;
                        db.DataBlockId = int.Parse(txtDataBlockId.Text);
                        db.DataBlockName = txtDataBlock.Text;
                        db.TypeOfRead = CboxTypeOfRead.Text;
                        db.StartAddress = (ushort)txtStartAddress.Value;
                        db.MemoryType = txtDomain.Text;
                        db.Length = (ushort)txtAddressLength.Value;
                        db.Description = txtDesc.Text;
                        db.DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", cboxDataType.SelectedItem));
                        db.IsArray = true;


                        AddressCreateTagModbus(db, false);

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


    }
}

