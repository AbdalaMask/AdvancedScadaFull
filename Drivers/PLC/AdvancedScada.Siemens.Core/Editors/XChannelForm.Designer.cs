using AdvancedScada.Utils.Net;

namespace AdvancedScada.Siemens.Core.Editors
{
    partial class XChannelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XChannelForm));
            this.TabControlSiemens = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.tabPageChannel = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.txtDesc = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.cboxModel = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cboxConnType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtChannelName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tabPageSerialPort = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.cboxHandshake = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cboxParity = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cboxStopBits = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cboxDataBits = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cboxBaudRate = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cboxPort = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel13 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel12 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tabPageEthernet = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.txtRack = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel14 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtIPAddress = new AdvancedScada.Utils.Net.IPAddressText();
            this.txtSlot = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.txtPort = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeaderGroup4 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnBlack = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnNext = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TabControlSiemens)).BeginInit();
            this.TabControlSiemens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageChannel)).BeginInit();
            this.tabPageChannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxConnType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageSerialPort)).BeginInit();
            this.tabPageSerialPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxHandshake)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxParity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxStopBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxDataBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBaudRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageEthernet)).BeginInit();
            this.tabPageEthernet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4.Panel)).BeginInit();
            this.kryptonHeaderGroup4.Panel.SuspendLayout();
            this.kryptonHeaderGroup4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControlSiemens
            // 
            this.TabControlSiemens.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabControlSiemens.Location = new System.Drawing.Point(0, 0);
            this.TabControlSiemens.Margin = new System.Windows.Forms.Padding(4);
            this.TabControlSiemens.Name = "TabControlSiemens";
            this.TabControlSiemens.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.Group;
            this.TabControlSiemens.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.tabPageChannel,
            this.tabPageSerialPort,
            this.tabPageEthernet});
            this.TabControlSiemens.SelectedIndex = 0;
            this.TabControlSiemens.Size = new System.Drawing.Size(500, 273);
            this.TabControlSiemens.TabIndex = 2;
            // 
            // tabPageChannel
            // 
            this.tabPageChannel.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tabPageChannel.Controls.Add(this.txtDesc);
            this.tabPageChannel.Controls.Add(this.cboxModel);
            this.tabPageChannel.Controls.Add(this.cboxConnType);
            this.tabPageChannel.Controls.Add(this.txtChannelName);
            this.tabPageChannel.Controls.Add(this.kryptonLabel4);
            this.tabPageChannel.Controls.Add(this.kryptonLabel3);
            this.tabPageChannel.Controls.Add(this.kryptonLabel2);
            this.tabPageChannel.Controls.Add(this.kryptonLabel1);
            this.tabPageChannel.Flags = 65534;
            this.tabPageChannel.LastVisibleSet = true;
            this.tabPageChannel.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageChannel.MinimumSize = new System.Drawing.Size(67, 62);
            this.tabPageChannel.Name = "tabPageChannel";
            this.tabPageChannel.Size = new System.Drawing.Size(498, 271);
            this.tabPageChannel.Text = "PageChannel";
            this.tabPageChannel.ToolTipTitle = "Page ToolTip";
            this.tabPageChannel.UniqueName = "5867A4647CE64D017D90BC6F521C74C2";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(121, 119);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(370, 145);
            this.txtDesc.TabIndex = 23;
            // 
            // cboxModel
            // 
            this.cboxModel.DropDownWidth = 163;
            this.cboxModel.IntegralHeight = false;
            this.cboxModel.Location = new System.Drawing.Point(121, 81);
            this.cboxModel.Margin = new System.Windows.Forms.Padding(4);
            this.cboxModel.Name = "cboxModel";
            this.cboxModel.Size = new System.Drawing.Size(247, 21);
            this.cboxModel.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxModel.TabIndex = 22;
            // 
            // cboxConnType
            // 
            this.cboxConnType.DropDownWidth = 163;
            this.cboxConnType.IntegralHeight = false;
            this.cboxConnType.Items.AddRange(new object[] {
            "None",
            "SerialPort",
            "Ethernet"});
            this.cboxConnType.Location = new System.Drawing.Point(121, 49);
            this.cboxConnType.Margin = new System.Windows.Forms.Padding(4);
            this.cboxConnType.Name = "cboxConnType";
            this.cboxConnType.Size = new System.Drawing.Size(247, 21);
            this.cboxConnType.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxConnType.TabIndex = 21;
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(121, 21);
            this.txtChannelName.Margin = new System.Windows.Forms.Padding(4);
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.Size = new System.Drawing.Size(247, 20);
            this.txtChannelName.TabIndex = 20;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(16, 122);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel4.TabIndex = 19;
            this.kryptonLabel4.Values.Text = "Description:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(18, 80);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(36, 20);
            this.kryptonLabel3.TabIndex = 18;
            this.kryptonLabel3.Values.Text = "CPU:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(16, 48);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(105, 20);
            this.kryptonLabel2.TabIndex = 17;
            this.kryptonLabel2.Values.Text = "Connection Type:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(16, 16);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(94, 20);
            this.kryptonLabel1.TabIndex = 16;
            this.kryptonLabel1.Values.Text = "Channel Name:";
            // 
            // tabPageSerialPort
            // 
            this.tabPageSerialPort.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tabPageSerialPort.Controls.Add(this.kryptonHeaderGroup1);
            this.tabPageSerialPort.Flags = 65534;
            this.tabPageSerialPort.LastVisibleSet = true;
            this.tabPageSerialPort.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageSerialPort.MinimumSize = new System.Drawing.Size(67, 62);
            this.tabPageSerialPort.Name = "tabPageSerialPort";
            this.tabPageSerialPort.Size = new System.Drawing.Size(498, 271);
            this.tabPageSerialPort.Text = "PageSerialPort";
            this.tabPageSerialPort.ToolTipTitle = "Page ToolTip";
            this.tabPageSerialPort.UniqueName = "E49C0033D0E049C8FC8960990408E927";
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cboxHandshake);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cboxParity);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cboxStopBits);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cboxDataBits);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cboxBaudRate);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cboxPort);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel13);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel12);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel11);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel10);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel9);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(498, 271);
            this.kryptonHeaderGroup1.TabIndex = 1;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "SerialPort";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.Siemens.Core.Properties.Resources.AddChannel;
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "";
            // 
            // cboxHandshake
            // 
            this.cboxHandshake.DropDownWidth = 163;
            this.cboxHandshake.IntegralHeight = false;
            this.cboxHandshake.Location = new System.Drawing.Point(128, 170);
            this.cboxHandshake.Margin = new System.Windows.Forms.Padding(4);
            this.cboxHandshake.Name = "cboxHandshake";
            this.cboxHandshake.Size = new System.Drawing.Size(108, 21);
            this.cboxHandshake.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxHandshake.TabIndex = 11;
            // 
            // cboxParity
            // 
            this.cboxParity.DropDownWidth = 163;
            this.cboxParity.IntegralHeight = false;
            this.cboxParity.Location = new System.Drawing.Point(128, 137);
            this.cboxParity.Margin = new System.Windows.Forms.Padding(4);
            this.cboxParity.Name = "cboxParity";
            this.cboxParity.Size = new System.Drawing.Size(108, 21);
            this.cboxParity.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxParity.TabIndex = 10;
            // 
            // cboxStopBits
            // 
            this.cboxStopBits.DropDownWidth = 163;
            this.cboxStopBits.IntegralHeight = false;
            this.cboxStopBits.Location = new System.Drawing.Point(128, 103);
            this.cboxStopBits.Margin = new System.Windows.Forms.Padding(4);
            this.cboxStopBits.Name = "cboxStopBits";
            this.cboxStopBits.Size = new System.Drawing.Size(108, 21);
            this.cboxStopBits.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxStopBits.TabIndex = 9;
            this.cboxStopBits.Text = " ";
            // 
            // cboxDataBits
            // 
            this.cboxDataBits.DropDownWidth = 163;
            this.cboxDataBits.IntegralHeight = false;
            this.cboxDataBits.Location = new System.Drawing.Point(128, 70);
            this.cboxDataBits.Margin = new System.Windows.Forms.Padding(4);
            this.cboxDataBits.Name = "cboxDataBits";
            this.cboxDataBits.Size = new System.Drawing.Size(108, 21);
            this.cboxDataBits.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxDataBits.TabIndex = 8;
            this.cboxDataBits.Text = " ";
            // 
            // cboxBaudRate
            // 
            this.cboxBaudRate.DropDownWidth = 163;
            this.cboxBaudRate.IntegralHeight = false;
            this.cboxBaudRate.Location = new System.Drawing.Point(128, 37);
            this.cboxBaudRate.Margin = new System.Windows.Forms.Padding(4);
            this.cboxBaudRate.Name = "cboxBaudRate";
            this.cboxBaudRate.Size = new System.Drawing.Size(108, 21);
            this.cboxBaudRate.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxBaudRate.TabIndex = 7;
            this.cboxBaudRate.Text = " ";
            // 
            // cboxPort
            // 
            this.cboxPort.DropDownWidth = 163;
            this.cboxPort.IntegralHeight = false;
            this.cboxPort.Location = new System.Drawing.Point(128, 4);
            this.cboxPort.Margin = new System.Windows.Forms.Padding(4);
            this.cboxPort.Name = "cboxPort";
            this.cboxPort.Size = new System.Drawing.Size(108, 21);
            this.cboxPort.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxPort.TabIndex = 6;
            this.cboxPort.Text = " ";
            // 
            // kryptonLabel13
            // 
            this.kryptonLabel13.Location = new System.Drawing.Point(3, 167);
            this.kryptonLabel13.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel13.Name = "kryptonLabel13";
            this.kryptonLabel13.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel13.TabIndex = 5;
            this.kryptonLabel13.Values.Text = "Handshake";
            // 
            // kryptonLabel12
            // 
            this.kryptonLabel12.Location = new System.Drawing.Point(3, 135);
            this.kryptonLabel12.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel12.Name = "kryptonLabel12";
            this.kryptonLabel12.Size = new System.Drawing.Size(41, 20);
            this.kryptonLabel12.TabIndex = 4;
            this.kryptonLabel12.Values.Text = "Parity";
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Location = new System.Drawing.Point(3, 106);
            this.kryptonLabel11.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(59, 20);
            this.kryptonLabel11.TabIndex = 3;
            this.kryptonLabel11.Values.Text = "Stop Bits";
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(3, 73);
            this.kryptonLabel10.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(59, 20);
            this.kryptonLabel10.TabIndex = 2;
            this.kryptonLabel10.Values.Text = "Data Bits";
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(3, 38);
            this.kryptonLabel9.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel9.TabIndex = 1;
            this.kryptonLabel9.Values.Text = "Baud rate";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(3, 4);
            this.kryptonLabel8.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(33, 20);
            this.kryptonLabel8.TabIndex = 0;
            this.kryptonLabel8.Values.Text = "Port";
            // 
            // tabPageEthernet
            // 
            this.tabPageEthernet.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tabPageEthernet.Controls.Add(this.txtRack);
            this.tabPageEthernet.Controls.Add(this.kryptonLabel14);
            this.tabPageEthernet.Controls.Add(this.txtIPAddress);
            this.tabPageEthernet.Controls.Add(this.txtSlot);
            this.tabPageEthernet.Controls.Add(this.txtPort);
            this.tabPageEthernet.Controls.Add(this.kryptonLabel7);
            this.tabPageEthernet.Controls.Add(this.kryptonLabel6);
            this.tabPageEthernet.Controls.Add(this.kryptonLabel5);
            this.tabPageEthernet.Flags = 65534;
            this.tabPageEthernet.LastVisibleSet = true;
            this.tabPageEthernet.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageEthernet.MinimumSize = new System.Drawing.Size(67, 62);
            this.tabPageEthernet.Name = "tabPageEthernet";
            this.tabPageEthernet.Size = new System.Drawing.Size(498, 271);
            this.tabPageEthernet.Text = "PageEthernet";
            this.tabPageEthernet.ToolTipTitle = "Page ToolTip";
            this.tabPageEthernet.UniqueName = "BCC78D39E7E54B47709379D54EC0D2B1";
            // 
            // txtRack
            // 
            this.txtRack.Location = new System.Drawing.Point(121, 145);
            this.txtRack.Margin = new System.Windows.Forms.Padding(4);
            this.txtRack.Name = "txtRack";
            this.txtRack.Size = new System.Drawing.Size(88, 22);
            this.txtRack.TabIndex = 18;
            // 
            // kryptonLabel14
            // 
            this.kryptonLabel14.Location = new System.Drawing.Point(20, 147);
            this.kryptonLabel14.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel14.Name = "kryptonLabel14";
            this.kryptonLabel14.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel14.TabIndex = 17;
            this.kryptonLabel14.Values.Text = "Rack:";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(124, 21);
            this.txtIPAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(189, 20);
            this.txtIPAddress.TabIndex = 16;
            this.txtIPAddress.Text = "127.0.0.1";
            // 
            // txtSlot
            // 
            this.txtSlot.Location = new System.Drawing.Point(121, 106);
            this.txtSlot.Margin = new System.Windows.Forms.Padding(4);
            this.txtSlot.Name = "txtSlot";
            this.txtSlot.Size = new System.Drawing.Size(88, 22);
            this.txtSlot.TabIndex = 15;
            // 
            // txtPort
            // 
            this.txtPort.Enabled = false;
            this.txtPort.Location = new System.Drawing.Point(121, 63);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtPort.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.txtPort.Name = "txtPort";
            this.txtPort.ReadOnly = true;
            this.txtPort.Size = new System.Drawing.Size(88, 22);
            this.txtPort.TabIndex = 14;
            this.txtPort.Value = new decimal(new int[] {
            102,
            0,
            0,
            0});
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(20, 108);
            this.kryptonLabel7.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(34, 20);
            this.kryptonLabel7.TabIndex = 13;
            this.kryptonLabel7.Values.Text = "Slot:";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(20, 61);
            this.kryptonLabel6.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(36, 20);
            this.kryptonLabel6.TabIndex = 12;
            this.kryptonLabel6.Values.Text = "Port:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(20, 21);
            this.kryptonLabel5.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel5.TabIndex = 11;
            this.kryptonLabel5.Values.Text = "IP Address:";
            // 
            // kryptonHeaderGroup4
            // 
            this.kryptonHeaderGroup4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup4.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup4.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup4.Location = new System.Drawing.Point(0, 273);
            this.kryptonHeaderGroup4.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonHeaderGroup4.Name = "kryptonHeaderGroup4";
            // 
            // kryptonHeaderGroup4.Panel
            // 
            this.kryptonHeaderGroup4.Panel.Controls.Add(this.btnCancel);
            this.kryptonHeaderGroup4.Panel.Controls.Add(this.btnBlack);
            this.kryptonHeaderGroup4.Panel.Controls.Add(this.btnNext);
            this.kryptonHeaderGroup4.Size = new System.Drawing.Size(500, 54);
            this.kryptonHeaderGroup4.TabIndex = 6;
            this.kryptonHeaderGroup4.ValuesPrimary.Image = global::AdvancedScada.Siemens.Core.Properties.Resources.AddChannel;
            this.kryptonHeaderGroup4.ValuesSecondary.Heading = "";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(138, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBlack
            // 
            this.btnBlack.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBlack.Location = new System.Drawing.Point(258, 0);
            this.btnBlack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(120, 30);
            this.btnBlack.TabIndex = 4;
            this.btnBlack.Values.Text = "< Back";
            this.btnBlack.Click += new System.EventHandler(this.btnBlack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.Location = new System.Drawing.Point(378, 0);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(120, 30);
            this.btnNext.TabIndex = 3;
            this.btnNext.Values.Text = "Next >";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // XChannelForm
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(500, 327);
            this.Controls.Add(this.kryptonHeaderGroup4);
            this.Controls.Add(this.TabControlSiemens);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(516, 365);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(516, 365);
            this.Name = "XChannelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XChannelForm - (Administrator)";
            this.Load += new System.EventHandler(this.XChannelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TabControlSiemens)).EndInit();
            this.TabControlSiemens.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabPageChannel)).EndInit();
            this.tabPageChannel.ResumeLayout(false);
            this.tabPageChannel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxConnType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageSerialPort)).EndInit();
            this.tabPageSerialPort.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboxHandshake)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxParity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxStopBits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxDataBits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBaudRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageEthernet)).EndInit();
            this.tabPageEthernet.ResumeLayout(false);
            this.tabPageEthernet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4.Panel)).EndInit();
            this.kryptonHeaderGroup4.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4)).EndInit();
            this.kryptonHeaderGroup4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonNavigator TabControlSiemens;
        private ComponentFactory.Krypton.Navigator.KryptonPage tabPageChannel;
        private ComponentFactory.Krypton.Navigator.KryptonPage tabPageEthernet;
        private ComponentFactory.Krypton.Navigator.KryptonPage tabPageSerialPort;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDesc;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxModel;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxConnType;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtChannelName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private IPAddressText txtIPAddress;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txtSlot;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txtPort;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxHandshake;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxParity;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxStopBits;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxDataBits;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxBaudRate;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxPort;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel13;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel12;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnBlack;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnNext;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txtRack;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel14;
    }
}