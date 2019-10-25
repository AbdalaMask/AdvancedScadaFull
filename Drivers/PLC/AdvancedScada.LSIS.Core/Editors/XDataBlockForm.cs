using AdvancedScada.DriverBase.Comm;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Utils.LSIS;
using System;
using System.Collections.Generic;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.LSIS.Core.Editors
{
    public partial class XDataBlockForm : AdvancedScada.Management.Editors.XDataBlockForm
    {
        private int IDX;
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


        #region LSIS
        public void AddressCreateTagWord(DataBlock db, bool IsNew)
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
            var y = txtStartAddress.Value;
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
                        Address = $"{txtDomain.Text}{y}",
                        DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType.SelectedItem.ToString()),
                        Description = $"{txtDesc.Text} {i + 1}"
                    };
                    db.Tags.Add(tg);
                    y += 1;
                }
            }


        }
        public void AddressCreateTagBitHex(DataBlock db, int Address, int Save_BufAddr, bool IsNew)
        {
            var hex1 = 0;
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
                var y = Address;
                hex1 = (int)HexadecimalToDecimal.HexToDec(Address.ToString());
                for (var i = Address; i < Save_BufAddr + y; i++)
                {
                    var hexaNumber = string.Empty;
                    var tg = new Tag() { TagId = IDX += 1 };

                    if (hex1 == 0)
                        hexaNumber = "0";
                    else
                        hexaNumber = DecimalToHex.GetDecimalToHex(long.Parse(hex1.ToString()));
                    tg.ChannelId = int.Parse(txtChannelId.Text);
                    tg.DeviceId = int.Parse(txtDeviceId.Text);
                    tg.DataBlockId = int.Parse(txtDataBlockId.Text);
                    tg.TagName = $"TAG{TagsCount:d5}";
                    tg.Address = $"{txtDomain.Text}{hexaNumber}";
                    tg.Description = $"{txtDesc.Text} {i.ToString("X")}";
                    tg.DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType.SelectedItem.ToString());
                    db.Tags.Add(tg);
                    hex1 += 1;
                    TagsCount += 1;
                }
            }
        }
        public void AddressCreateTagBit(DataBlock db, int Address, int Save_BufAddr, bool IsNew)
        {
            var hex1 = Address;
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
                var y = Address;

                for (var i = Address; i < Save_BufAddr + y; i++)
                {
                    var hexaNumber = string.Empty;
                    var tg = new Tag() { TagId = IDX += 1 };


                    tg.ChannelId = int.Parse(txtChannelId.Text);
                    tg.DeviceId = int.Parse(txtDeviceId.Text);
                    tg.DataBlockId = int.Parse(txtDataBlockId.Text);
                    tg.TagName = $"TAG{TagsCount:d5}";
                    tg.Address = $"{txtDomain.Text}{hex1}";
                    tg.Description = $"{txtDesc.Text} {i.ToString("X")}";
                    tg.DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType.SelectedItem.ToString());
                    db.Tags.Add(tg);
                    hex1 += 1;
                    TagsCount += 1;
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

                    txtStartAddress.Value = db.StartAddress;
                    txtAddressLength.Value = db.Length;
                    txtDomain.Text = db.MemoryType;
                    txtDesc.Text = db.Description;
                    txtDataBlockId.Text = $"{db.DataBlockId}";
                    cboxDataType.SelectedItem = string.Format("{0}", db.DataType);
                    chkIsArray.Checked = db.IsArray;
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


                switch ((DataTypes)System.Enum.Parse(typeof(DataTypes), cboxDataType.SelectedValue.ToString()))
                {
                    case DataTypes.Bit:
                        chkX16.Enabled = true;
                        chkIsHex.Enabled = true;
                        txtAddressLength.Maximum = 1;
                                chkX10.Enabled = true;
                                txtAddressLength.Minimum = 1;
                           

                        break;
                    case DataTypes.Byte:
                        break;
                    case DataTypes.Short:
                        
                                if (ch.ConnectionType == "SerialPort")
                                {
                                    txtAddressLength.Maximum = 60;
                                    txtAddressLength.Minimum = 1;
                                   
                                }

                                else
                                {
                                    txtAddressLength.Maximum = 120;
                                    txtAddressLength.Minimum = 1;
                                    
                                }
                        chkX10.Enabled = false;
                        chkX16.Enabled = false;
                        chkIsHex.Enabled = false;
                        break;
                          
                         
                    case DataTypes.UShort:
                        break;
                    case DataTypes.Int:
                        break;
                    case DataTypes.UInt:
                        break;
                    case DataTypes.Long:
                    case DataTypes.ULong:
                    case DataTypes.Float:
                    case DataTypes.Double:
                        txtAddressLength.Maximum = 60;
                        txtAddressLength.Minimum = 1;
                        chkX10.Enabled = false;
                        chkX16.Enabled = false;
                        chkIsHex.Enabled = false;
                        break;
                    case DataTypes.String:
                        break;
                    default:
                        break;
                }

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
                var Address = 0;
                var Save_BufAddr = 0;


                switch (cboxDataType.Text)
                {
                    case "Bit" when chkX16.Checked:
                        Save_BufAddr = (int)txtAddressLength.Value * 16;
                        break;
                    default:
                        Save_BufAddr = (int)txtAddressLength.Value;
                        break;
                }
                Address = chkX10.Checked ? 10 * (int)txtStartAddress.Value : (int)txtStartAddress.Value;

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
                            TypeOfRead = "",
                            StartAddress = (ushort)txtStartAddress.Value,
                            MemoryType = txtDomain.Text,
                            Description = txtDesc.Text,
                            Length = (ushort)txtAddressLength.Value,
                            DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", cboxDataType.SelectedItem)),
                            IsArray = chkIsArray.Checked,
                            Tags = new List<Tag>()
                        };


                        switch (cboxDataType.Text)
                        {
                            case "Bit":
                            case "Byte":
                                if (chkCreateTag.Checked && chkIsHex.Checked) AddressCreateTagBitHex(dbNew, Address, Save_BufAddr, true);
                                else AddressCreateTagBit(dbNew, Address, Save_BufAddr, true);
                                break;
                            default:
                                if (chkCreateTag.Checked) AddressCreateTagWord(dbNew, true);
                                break;
                        }


                        eventDataBlockChanged?.Invoke(dbNew, true);
                        Close();
                    }
                    else
                    {
                        db.ChannelId = db.ChannelId;
                        db.DeviceId = db.DeviceId;
                        db.DataBlockId = int.Parse(txtDataBlockId.Text);
                        db.DataBlockName = txtDataBlock.Text;
                        db.TypeOfRead = "";
                        db.StartAddress = (ushort)txtStartAddress.Value;
                        db.MemoryType = txtDomain.Text;
                        db.Length = (ushort)txtAddressLength.Value;
                        db.Description = txtDesc.Text;
                        db.DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", cboxDataType.SelectedItem));
                        db.IsArray = chkIsArray.Checked;


                        switch (cboxDataType.Text)
                        {
                            case "Bit":
                            case "Byte":
                                if (chkCreateTag.Checked && chkIsHex.Checked) AddressCreateTagBitHex(db, Address, Save_BufAddr, false);
                                else AddressCreateTagBit(db, Address, Save_BufAddr, false);
                                break;
                            default:
                                if (chkCreateTag.Checked) AddressCreateTagWord(db, false);
                                break;
                        }

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

