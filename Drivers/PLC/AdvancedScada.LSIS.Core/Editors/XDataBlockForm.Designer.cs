namespace AdvancedScada.LSIS.Core.Editors
{
    partial class XDataBlockForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XDataBlockForm));
            this.DxErrorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.txtDesc = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.chkCreateTag = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.txtAddressLength = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtDomain = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtPrefix = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.chkX10 = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.txtStartAddress = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cboxDataType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtDataBlock = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtDataBlockId = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtDeviceName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtDeviceId = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtChannelName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtChannelId = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.GLSIS = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.chkIsArray = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkIsHex = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkX16 = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDomain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxDataType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GLSIS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GLSIS.Panel)).BeginInit();
            this.GLSIS.Panel.SuspendLayout();
            this.GLSIS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DxErrorProvider1
            // 
            this.DxErrorProvider1.ContainerControl = this;
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup2.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(0, 185);
            this.kryptonHeaderGroup2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.txtDesc);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(626, 82);
            this.kryptonHeaderGroup2.TabIndex = 25;
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "Description:";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = global::AdvancedScada.LSIS.Core.Properties.Resources.AddGoup;
            this.kryptonHeaderGroup2.ValuesSecondary.Heading = "Heading";
            // 
            // txtDesc
            // 
            this.txtDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDesc.Location = new System.Drawing.Point(0, 0);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(2);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(624, 58);
            this.txtDesc.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(509, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkCreateTag
            // 
            this.chkCreateTag.Location = new System.Drawing.Point(473, 129);
            this.chkCreateTag.Margin = new System.Windows.Forms.Padding(2);
            this.chkCreateTag.Name = "chkCreateTag";
            this.chkCreateTag.Size = new System.Drawing.Size(79, 20);
            this.chkCreateTag.TabIndex = 24;
            this.chkCreateTag.Values.Text = "CreateTag";
            this.chkCreateTag.CheckedChanged += new System.EventHandler(this.chkCreateTag_CheckedChanged);
            // 
            // txtAddressLength
            // 
            this.txtAddressLength.Location = new System.Drawing.Point(89, 127);
            this.txtAddressLength.Margin = new System.Windows.Forms.Padding(2);
            this.txtAddressLength.Name = "txtAddressLength";
            this.txtAddressLength.Size = new System.Drawing.Size(141, 22);
            this.txtAddressLength.TabIndex = 23;
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(4, 129);
            this.kryptonLabel10.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(51, 20);
            this.kryptonLabel10.TabIndex = 22;
            this.kryptonLabel10.Values.Text = "Length:";
            // 
            // txtDomain
            // 
            this.txtDomain.DropDownWidth = 72;
            this.txtDomain.IntegralHeight = false;
            this.txtDomain.Items.AddRange(new object[] {
            "PX",
            "PB",
            "PW",
            "MX",
            "MB",
            "MW",
            "DW",
            "TW",
            "TD",
            "DD",
            "DL"});
            this.txtDomain.Location = new System.Drawing.Point(332, 128);
            this.txtDomain.Margin = new System.Windows.Forms.Padding(2);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(135, 21);
            this.txtDomain.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.txtDomain.TabIndex = 21;
            this.txtDomain.Text = "MX";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(238, 129);
            this.txtPrefix.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(85, 20);
            this.txtPrefix.TabIndex = 20;
            this.txtPrefix.Values.Text = "MemoryType:";
            // 
            // chkX10
            // 
            this.chkX10.Location = new System.Drawing.Point(2, 2);
            this.chkX10.Margin = new System.Windows.Forms.Padding(2);
            this.chkX10.Name = "chkX10";
            this.chkX10.Size = new System.Drawing.Size(44, 20);
            this.chkX10.TabIndex = 19;
            this.chkX10.Values.Text = "X10";
            // 
            // txtStartAddress
            // 
            this.txtStartAddress.Location = new System.Drawing.Point(332, 98);
            this.txtStartAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtStartAddress.Name = "txtStartAddress";
            this.txtStartAddress.Size = new System.Drawing.Size(135, 22);
            this.txtStartAddress.TabIndex = 18;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(238, 99);
            this.kryptonLabel8.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(82, 20);
            this.kryptonLabel8.TabIndex = 17;
            this.kryptonLabel8.Values.Text = "StartAddress:";
            // 
            // cboxDataType
            // 
            this.cboxDataType.DropDownWidth = 188;
            this.cboxDataType.IntegralHeight = false;
            this.cboxDataType.Location = new System.Drawing.Point(89, 98);
            this.cboxDataType.Margin = new System.Windows.Forms.Padding(2);
            this.cboxDataType.Name = "cboxDataType";
            this.cboxDataType.Size = new System.Drawing.Size(141, 21);
            this.cboxDataType.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxDataType.TabIndex = 16;
            this.cboxDataType.SelectedIndexChanged += new System.EventHandler(this.cboxDataType_SelectedIndexChanged);
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(4, 99);
            this.kryptonLabel7.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(65, 20);
            this.kryptonLabel7.TabIndex = 15;
            this.kryptonLabel7.Values.Text = "DataType:";
            // 
            // txtDataBlock
            // 
            this.txtDataBlock.Location = new System.Drawing.Point(332, 58);
            this.txtDataBlock.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataBlock.Name = "txtDataBlock";
            this.txtDataBlock.Size = new System.Drawing.Size(135, 20);
            this.txtDataBlock.TabIndex = 14;
            this.txtDataBlock.Text = " ";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(4, 55);
            this.kryptonLabel6.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(78, 20);
            this.kryptonLabel6.TabIndex = 13;
            this.kryptonLabel6.Values.Text = "DataBlockId:";
            // 
            // txtDataBlockId
            // 
            this.txtDataBlockId.Location = new System.Drawing.Point(89, 57);
            this.txtDataBlockId.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataBlockId.Name = "txtDataBlockId";
            this.txtDataBlockId.Size = new System.Drawing.Size(141, 20);
            this.txtDataBlockId.TabIndex = 11;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(238, 57);
            this.kryptonLabel5.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(100, 20);
            this.kryptonLabel5.TabIndex = 10;
            this.kryptonLabel5.Values.Text = "DataBlockName:";
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.Location = new System.Drawing.Point(332, 30);
            this.txtDeviceName.Margin = new System.Windows.Forms.Padding(2);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(135, 20);
            this.txtDeviceName.TabIndex = 8;
            this.txtDeviceName.Text = " ";
            // 
            // txtDeviceId
            // 
            this.txtDeviceId.Location = new System.Drawing.Point(89, 29);
            this.txtDeviceId.Margin = new System.Windows.Forms.Padding(2);
            this.txtDeviceId.Name = "txtDeviceId";
            this.txtDeviceId.Size = new System.Drawing.Size(141, 20);
            this.txtDeviceId.TabIndex = 7;
            this.txtDeviceId.Text = " ";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(238, 32);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(85, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Device Name:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(4, 30);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel4.TabIndex = 5;
            this.kryptonLabel4.Values.Text = "Device Id:";
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(332, 2);
            this.txtChannelName.Margin = new System.Windows.Forms.Padding(2);
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.Size = new System.Drawing.Size(135, 20);
            this.txtChannelName.TabIndex = 3;
            this.txtChannelName.Text = " ";
            // 
            // txtChannelId
            // 
            this.txtChannelId.Location = new System.Drawing.Point(89, 1);
            this.txtChannelId.Margin = new System.Windows.Forms.Padding(2);
            this.txtChannelId.Name = "txtChannelId";
            this.txtChannelId.Size = new System.Drawing.Size(141, 20);
            this.txtChannelId.TabIndex = 2;
            this.txtChannelId.Text = " ";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(238, 6);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(94, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Channel Name:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(4, 2);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(72, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Channel Id:";
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(384, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(125, 28);
            this.btnOK.TabIndex = 5;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.GLSIS);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonHeaderGroup2);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.chkCreateTag);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtAddressLength);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel10);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDomain);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtPrefix);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtStartAddress);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cboxDataType);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel7);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDataBlock);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDataBlockId);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDeviceName);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDeviceId);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtChannelName);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtChannelId);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(628, 291);
            this.kryptonHeaderGroup1.TabIndex = 3;
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.LSIS.Core.Properties.Resources.AddGoup;
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Heading";
            // 
            // GLSIS
            // 
            this.GLSIS.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.GLSIS.HeaderVisibleSecondary = false;
            this.GLSIS.Location = new System.Drawing.Point(473, 2);
            this.GLSIS.Margin = new System.Windows.Forms.Padding(2);
            this.GLSIS.Name = "GLSIS";
            // 
            // GLSIS.Panel
            // 
            this.GLSIS.Panel.Controls.Add(this.chkIsArray);
            this.GLSIS.Panel.Controls.Add(this.chkIsHex);
            this.GLSIS.Panel.Controls.Add(this.chkX16);
            this.GLSIS.Panel.Controls.Add(this.chkX10);
            this.GLSIS.Size = new System.Drawing.Size(149, 75);
            this.GLSIS.TabIndex = 30;
            this.GLSIS.ValuesPrimary.Heading = "Tools PLC LSIS";
            this.GLSIS.ValuesPrimary.Image = global::AdvancedScada.LSIS.Core.Properties.Resources.AddGoup;
            this.GLSIS.ValuesSecondary.Heading = "Heading";
            // 
            // chkIsArray
            // 
            this.chkIsArray.Location = new System.Drawing.Point(92, 26);
            this.chkIsArray.Margin = new System.Windows.Forms.Padding(2);
            this.chkIsArray.Name = "chkIsArray";
            this.chkIsArray.Size = new System.Drawing.Size(61, 20);
            this.chkIsArray.TabIndex = 30;
            this.chkIsArray.Values.Text = "IsArray";
            // 
            // chkIsHex
            // 
            this.chkIsHex.Location = new System.Drawing.Point(92, 2);
            this.chkIsHex.Margin = new System.Windows.Forms.Padding(2);
            this.chkIsHex.Name = "chkIsHex";
            this.chkIsHex.Size = new System.Drawing.Size(53, 20);
            this.chkIsHex.TabIndex = 28;
            this.chkIsHex.Values.Text = "IsHex";
            // 
            // chkX16
            // 
            this.chkX16.Location = new System.Drawing.Point(50, 2);
            this.chkX16.Margin = new System.Windows.Forms.Padding(2);
            this.chkX16.Name = "chkX16";
            this.chkX16.Size = new System.Drawing.Size(44, 20);
            this.chkX16.TabIndex = 29;
            this.chkX16.Values.Text = "X16";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnOK);
            this.kryptonPanel1.Controls.Add(this.btnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 291);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(628, 28);
            this.kryptonPanel1.TabIndex = 6;
            // 
            // XDataBlockForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(628, 319);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XDataBlockForm";
            this.Load += new System.EventHandler(this.XDataBlockForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDomain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxDataType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GLSIS.Panel)).EndInit();
            this.GLSIS.Panel.ResumeLayout(false);
            this.GLSIS.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GLSIS)).EndInit();
            this.GLSIS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider DxErrorProvider1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDesc;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkCreateTag;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txtAddressLength;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox txtDomain;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel txtPrefix;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkX10;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txtStartAddress;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxDataType;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDataBlock;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDataBlockId;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDeviceName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDeviceId;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtChannelName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtChannelId;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkIsHex;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkX16;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup GLSIS;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkIsArray;
    }
}