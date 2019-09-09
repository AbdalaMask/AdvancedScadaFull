using AdvancedScada.DriverBase.Devices;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AdvancedScada.Utils.LSIS;
using static AdvancedScada.IBaseService.Common.XCollection;
using AdvancedScada.Management;

namespace AdvancedScada.Studio.Editors
{
    public partial class XDataBlockForm : KryptonForm
    {
        private int IDX;
        int TagsCount = 1;
        private readonly Channel ch;
        private readonly DataBlock db;
        private readonly Device dv;
        public EventDataBlockChanged eventDataBlockChanged = null;


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
                        DataType = $"{cboxDataType.SelectedItem}",
                        Description = $"{txtDesc.Text} {i + 1}"
                    };
                    db.Tags.Add(tg);
                }
            }


        }
        #endregion

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
                        DataType = $"{cboxDataType.SelectedItem}",
                        Description = $"{txtDesc.Text} {i + 1}"
                    };
                    db.Tags.Add(tg);
                    y += 1;
                }
            }


        }
        public void AddressCreateTagBit(DataBlock db, int Address, int Save_BufAddr, bool IsNew)
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
                    tg.DataType = $"{cboxDataType.SelectedItem}";
                    db.Tags.Add(tg);
                    hex1 += 1;
                    TagsCount += 1;
                }
            }
        }
        #endregion

        #region DVP
        public void AddressCreateTagDVP(DataBlock db, bool IsNew, int TagsCount = 1)
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
                        Address = $"{txtDomain.Text}{txtStartAddress.Value + i}",
                        DataType =
                        $"{cboxDataType.SelectedItem}",
                        Description = $"{txtDesc.Text} {i + 1}"
                    };
                    db.Tags.Add(tg);
                }
        }
        #endregion
        private void XDataBlockForm_Load(object sender, EventArgs e)
        {
            string[] DVP = new string[5] { "M", "Y", "X", "D", "T" };
            try
            {
                txtDeviceName.Text = dv.DeviceName;
                txtChannelName.Text = ch.ChannelName;
                txtChannelId.Text = ch.ChannelId.ToString();
                txtDeviceId.Text = dv.DeviceId.ToString();
                CboxTypeOfRead.SelectedIndex = 0;
                if (db == null)
                {
                    switch (ch.ChannelTypes)

                    {
                        case "Delta":
                            chkX10.Visible = false;
                            txtDomain.Items.Clear();
                            txtDomain.Items.AddRange(DVP);
                            txtPrefix.Text = "txtPrefix :";
                            chkIsArray.Visible = false;
                            break;
                        case "LSIS":
                            lblTypeOfRead.Visible = false;
                            CboxTypeOfRead.Visible = false;
                            chkIsArray.Visible = false;
                            break;
                        case "Modbus":
                            chkX10.Visible = false;
                            txtDomain.Items.Clear();
                            txtPrefix.Text = "txtPrefix :";
                            chkIsArray.Visible = false;
                            break;
                        default:
                            break;
                    }
                    Text = "Add DataBlock   " + ch.ChannelTypes;
                    txtDataBlockId.Text = Convert.ToString(dv.DataBlocks.Count + 1);
                    txtDataBlock.Text = "DataBlock" + Convert.ToString(dv.DataBlocks.Count + 1);
                }
                else
                {
                    switch (ch.ChannelTypes)

                    {
                        case "Delta":
                            chkX10.Visible = false;
                            txtDomain.Items.Clear();
                            txtDomain.Items.AddRange(DVP);
                            txtPrefix.Text = "txtPrefix :";
                            chkIsArray.Visible = false;
                            break;
                        case "LSIS":
                            lblTypeOfRead.Visible = false;
                            CboxTypeOfRead.Visible = false; chkIsArray.Visible = false;
                            break;
                        case "Modbus":
                            chkX10.Visible = false;
                            txtDomain.Items.Clear(); chkIsArray.Visible = false;
                            break;
                        default:
                            break;
                    }
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
                    cboxDataType.Text = $"{db.DataType}";
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

            var DriverTypes2 = ch.ChannelTypes;




        }

        private void DbFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close();
        }

        private void cboxDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                var ohj = cboxDataType.Text;

                switch (ohj)
                {
                    case "Bit":
                    case "Byte":
                        switch (ch.ChannelTypes)

                        {
                            case "LSIS":
                                txtAddressLength.Maximum = 1;
                                chkX10.Enabled = true;
                                txtAddressLength.Minimum = 1;
                                break;
                            case "Modbus":
                                txtAddressLength.Maximum = 2000;
                                txtAddressLength.Minimum = 1;
                                break;
                            default:
                                break;
                        }


                        break;
                    case "Word":
                    case "Int":
                        switch (ch.ChannelTypes)

                        {
                            case "LSIS":
                                if (ch.ConnectionType == "SerialPort")
                                {
                                    txtAddressLength.Maximum = 60;
                                    txtAddressLength.Minimum = 1;
                                    chkX10.Enabled = false;
                                }

                                else
                                {
                                    txtAddressLength.Maximum = 120;
                                    txtAddressLength.Minimum = 1;
                                    chkX10.Enabled = false;
                                }
                                break;
                            case "Modbus":
                                txtAddressLength.Maximum = 120;
                                txtAddressLength.Minimum = 1;
                                break;
                            default:
                                break;
                        }

                        break;
                    case "DWord":
                    case "DInt":
                    case "Real":

                        txtAddressLength.Maximum = 60;
                        txtAddressLength.Minimum = 1;
                        chkX10.Enabled = false;

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
                switch (ch.ChannelTypes)

                {
                    case "LSIS":

                        if (cboxDataType.Text == "Bit" || cboxDataType.Text == "Byte")
                            Save_BufAddr = (int)txtAddressLength.Value * 16;
                        else
                            Save_BufAddr = (int)txtAddressLength.Value;
                        if (chkX10.Checked)
                            Address = 10 * (int)txtStartAddress.Value;
                        else
                            Address = (int)txtStartAddress.Value;
                        break;
                    case "Modbus":

                        break;
                    default:
                        break;
                }




                if (chkCreateTag.Checked && (string.IsNullOrEmpty(txtDomain.Text)
                                            || string.IsNullOrWhiteSpace(txtDomain.Text)))
                    DxErrorProvider1.SetError(txtDomain, "The Prefix is empty");
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
                            DataType = cboxDataType.Text,
                            IsArray = chkIsArray.Checked,
                            Tags = new List<Tag>()
                        };

                        switch (ch.ChannelTypes)

                        {
                            case "Delta":
                                AddressCreateTagDVP(dbNew, true, TagsCount);
                                break;
                            case "LSIS":
                                if (cboxDataType.Text == "Bit" || cboxDataType.Text == "Byte")
                                {
                                    if (chkCreateTag.Checked) AddressCreateTagBit(dbNew, Address, Save_BufAddr, true);

                                }
                                else
                                {
                                    if (chkCreateTag.Checked) AddressCreateTagWord(dbNew, true);

                                }


                                break;
                            case "Modbus":
                                AddressCreateTagModbus(dbNew, true, TagsCount);
                                break;
                            default:
                                break;
                        }

                        if (eventDataBlockChanged != null) eventDataBlockChanged(dbNew, true);
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
                        db.DataType = $"{cboxDataType.Text}";
                        db.IsArray = chkIsArray.Checked;

                        switch (ch.ChannelTypes)

                        {
                            case "Delta":
                                AddressCreateTagDVP(db, false, TagsCount);
                                break;
                            case "LSIS":
                                if (cboxDataType.Text == "Bit" || cboxDataType.Text == "Byte")
                                {
                                    if (chkCreateTag.Checked) AddressCreateTagBit(db, Address, Save_BufAddr, false);

                                }
                                else
                                {
                                    if (chkCreateTag.Checked) AddressCreateTagWord(db, false);

                                }


                                break;
                            case "Modbus":
                                AddressCreateTagModbus(db, false);
                                break;
                            default:
                                break;
                        }

                        if (eventDataBlockChanged != null) eventDataBlockChanged(db, false);
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

        private void ChkIsArray_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsArray.Checked)
            {
                CboxTypeOfRead.Visible = true;
                chkCreateTag.Visible = true;
                txtDomain.Visible = true;
                txtStartAddress.Visible = true;
                txtAddressLength.Visible = true;
            }
            else
            {
                CboxTypeOfRead.Visible = false;
                chkCreateTag.Visible = false;
                txtDomain.Visible = false;
                txtStartAddress.Visible = false;
                txtAddressLength.Visible = false;
            }

        }
    }
}

