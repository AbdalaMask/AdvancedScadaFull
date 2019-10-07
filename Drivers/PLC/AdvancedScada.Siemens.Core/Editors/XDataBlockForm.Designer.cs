namespace AdvancedScada.Siemens.Core.Editors
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
            this.txtPrefix = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
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
            this.kryptonNavigator1 = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.PGDB = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.txtDBNumber = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.PGIsArray = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.chkCreateTag = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.txtAddressLength = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtStartAddress = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cboxDataType2 = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.txtDomain = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxDataType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PGDB)).BeginInit();
            this.PGDB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PGIsArray)).BeginInit();
            this.PGIsArray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxDataType2)).BeginInit();
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
            this.kryptonHeaderGroup2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup2.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(0, 258);
            this.kryptonHeaderGroup2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.txtDesc);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(562, 82);
            this.kryptonHeaderGroup2.TabIndex = 25;
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "Description:";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = global::AdvancedScada.Siemens.Core.Properties.Resources.AddGoup;
            this.kryptonHeaderGroup2.ValuesSecondary.Heading = "Heading";
            // 
            // txtDesc
            // 
            this.txtDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDesc.Location = new System.Drawing.Point(0, 0);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(2);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(560, 58);
            this.txtDesc.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(443, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(264, 45);
            this.txtPrefix.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(74, 20);
            this.txtPrefix.TabIndex = 20;
            this.txtPrefix.Values.Text = "DBNumber:";
            // 
            // cboxDataType
            // 
            this.cboxDataType.DropDownWidth = 188;
            this.cboxDataType.IntegralHeight = false;
            this.cboxDataType.Location = new System.Drawing.Point(103, 44);
            this.cboxDataType.Margin = new System.Windows.Forms.Padding(2);
            this.cboxDataType.Name = "cboxDataType";
            this.cboxDataType.Size = new System.Drawing.Size(141, 21);
            this.cboxDataType.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxDataType.TabIndex = 16;
            this.cboxDataType.SelectedIndexChanged += new System.EventHandler(this.cboxDataType_SelectedIndexChanged);
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(9, 45);
            this.kryptonLabel7.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel7.TabIndex = 15;
            this.kryptonLabel7.Values.Text = "Memory Type:";
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
            this.btnOK.Location = new System.Drawing.Point(318, 0);
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
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonNavigator1);
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
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(562, 258);
            this.kryptonHeaderGroup1.TabIndex = 3;
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.Siemens.Core.Properties.Resources.AddGoup;
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Heading";
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonNavigator1.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonNavigator1.Location = new System.Drawing.Point(0, 87);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.HeaderBarCheckButtonGroup;
            this.kryptonNavigator1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.PGDB,
            this.PGIsArray});
            this.kryptonNavigator1.SelectedIndex = 1;
            this.kryptonNavigator1.Size = new System.Drawing.Size(560, 147);
            this.kryptonNavigator1.TabIndex = 27;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // PGDB
            // 
            this.PGDB.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.PGDB.Controls.Add(this.kryptonLabel7);
            this.PGDB.Controls.Add(this.txtDBNumber);
            this.PGDB.Controls.Add(this.cboxDataType);
            this.PGDB.Controls.Add(this.txtPrefix);
            this.PGDB.Flags = 65534;
            this.PGDB.LastVisibleSet = true;
            this.PGDB.MinimumSize = new System.Drawing.Size(50, 50);
            this.PGDB.Name = "PGDB";
            this.PGDB.Size = new System.Drawing.Size(558, 114);
            this.PGDB.Text = "DB";
            this.PGDB.ToolTipTitle = "Page ToolTip";
            this.PGDB.UniqueName = "120a1177abab424ca9495c5835a06e57";
            // 
            // txtDBNumber
            // 
            this.txtDBNumber.Location = new System.Drawing.Point(342, 45);
            this.txtDBNumber.Margin = new System.Windows.Forms.Padding(2);
            this.txtDBNumber.Name = "txtDBNumber";
            this.txtDBNumber.Size = new System.Drawing.Size(135, 20);
            this.txtDBNumber.TabIndex = 26;
            this.txtDBNumber.Text = " ";
            // 
            // PGIsArray
            // 
            this.PGIsArray.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.PGIsArray.Controls.Add(this.txtDomain);
            this.PGIsArray.Controls.Add(this.chkCreateTag);
            this.PGIsArray.Controls.Add(this.txtAddressLength);
            this.PGIsArray.Controls.Add(this.kryptonLabel10);
            this.PGIsArray.Controls.Add(this.kryptonLabel8);
            this.PGIsArray.Controls.Add(this.txtStartAddress);
            this.PGIsArray.Controls.Add(this.kryptonLabel9);
            this.PGIsArray.Controls.Add(this.cboxDataType2);
            this.PGIsArray.Controls.Add(this.kryptonLabel11);
            this.PGIsArray.Flags = 65534;
            this.PGIsArray.LastVisibleSet = true;
            this.PGIsArray.MinimumSize = new System.Drawing.Size(50, 50);
            this.PGIsArray.Name = "PGIsArray";
            this.PGIsArray.Size = new System.Drawing.Size(558, 114);
            this.PGIsArray.Text = "IsArray";
            this.PGIsArray.ToolTipTitle = "Page ToolTip";
            this.PGIsArray.UniqueName = "f63be40881154f33978e176609306190";
            // 
            // chkCreateTag
            // 
            this.chkCreateTag.Location = new System.Drawing.Point(469, 75);
            this.chkCreateTag.Margin = new System.Windows.Forms.Padding(2);
            this.chkCreateTag.Name = "chkCreateTag";
            this.chkCreateTag.Size = new System.Drawing.Size(79, 20);
            this.chkCreateTag.TabIndex = 33;
            this.chkCreateTag.Values.Text = "CreateTag";
            // 
            // txtAddressLength
            // 
            this.txtAddressLength.Location = new System.Drawing.Point(85, 73);
            this.txtAddressLength.Margin = new System.Windows.Forms.Padding(2);
            this.txtAddressLength.Name = "txtAddressLength";
            this.txtAddressLength.Size = new System.Drawing.Size(141, 22);
            this.txtAddressLength.TabIndex = 32;
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(8, 75);
            this.kryptonLabel10.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(51, 20);
            this.kryptonLabel10.TabIndex = 31;
            this.kryptonLabel10.Values.Text = "Length:";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(234, 75);
            this.kryptonLabel8.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(85, 20);
            this.kryptonLabel8.TabIndex = 29;
            this.kryptonLabel8.Values.Text = "MemoryType:";
            // 
            // txtStartAddress
            // 
            this.txtStartAddress.Location = new System.Drawing.Point(328, 44);
            this.txtStartAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtStartAddress.Name = "txtStartAddress";
            this.txtStartAddress.Size = new System.Drawing.Size(135, 22);
            this.txtStartAddress.TabIndex = 28;
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(234, 45);
            this.kryptonLabel9.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(82, 20);
            this.kryptonLabel9.TabIndex = 27;
            this.kryptonLabel9.Values.Text = "StartAddress:";
            // 
            // cboxDataType2
            // 
            this.cboxDataType2.DropDownWidth = 188;
            this.cboxDataType2.IntegralHeight = false;
            this.cboxDataType2.Location = new System.Drawing.Point(85, 44);
            this.cboxDataType2.Margin = new System.Windows.Forms.Padding(2);
            this.cboxDataType2.Name = "cboxDataType2";
            this.cboxDataType2.Size = new System.Drawing.Size(141, 21);
            this.cboxDataType2.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxDataType2.TabIndex = 26;
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Location = new System.Drawing.Point(5, 45);
            this.kryptonLabel11.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(65, 20);
            this.kryptonLabel11.TabIndex = 25;
            this.kryptonLabel11.Values.Text = "DataType:";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnOK);
            this.kryptonPanel1.Controls.Add(this.btnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 340);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(562, 28);
            this.kryptonPanel1.TabIndex = 6;
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(328, 75);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(135, 20);
            this.txtDomain.TabIndex = 34;
            this.txtDomain.Text = " ";
            // 
            // XDataBlockForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(562, 368);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonHeaderGroup2);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XDataBlockForm";
            this.Text = "XDataBlockForm - (Administrator)";
            this.Load += new System.EventHandler(this.XDataBlockForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboxDataType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PGDB)).EndInit();
            this.PGDB.ResumeLayout(false);
            this.PGDB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PGIsArray)).EndInit();
            this.PGIsArray.ResumeLayout(false);
            this.PGIsArray.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxDataType2)).EndInit();
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
        private ComponentFactory.Krypton.Toolkit.KryptonLabel txtPrefix;
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
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDBNumber;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage PGDB;
        private ComponentFactory.Krypton.Navigator.KryptonPage PGIsArray;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkCreateTag;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txtAddressLength;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txtStartAddress;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxDataType2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDomain;
    }
}