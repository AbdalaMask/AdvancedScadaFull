namespace AdvancedScada.Studio.Alarms
{
    partial class FrmAddAlarm
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
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny2 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtTagName = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtDataBlock = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtDevice = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtChannel = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonSeparator1 = new ComponentFactory.Krypton.Toolkit.KryptonSeparator();
            this.ColValue = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ColAlarmCalss = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ColAlarmText = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ColName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTagName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDevice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColAlarmCalss)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.FixedRight;
            this.buttonSpecAny1.UniqueName = "548d9b927c914d939ad6730cc75af63a";
            // 
            // buttonSpecAny2
            // 
            this.buttonSpecAny2.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.Next;
            this.buttonSpecAny2.UniqueName = "9b9a47497ce143a09f63854fbad9bd85";
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtTagName);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDataBlock);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDevice);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtChannel);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonSeparator1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.ColValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel11);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.ColAlarmCalss);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel7);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.ColAlarmText);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.ColName);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(532, 332);
            this.kryptonHeaderGroup1.TabIndex = 6;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Alarm ";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.Studio.Properties.Resources.AddTag;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(18, 142);
            this.kryptonLabel8.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(68, 20);
            this.kryptonLabel8.TabIndex = 42;
            this.kryptonLabel8.Values.Text = "AlarmText:";
            // 
            // txtTagName
            // 
            this.txtTagName.DropDownWidth = 188;
            this.txtTagName.IntegralHeight = false;
            this.txtTagName.Location = new System.Drawing.Point(359, 103);
            this.txtTagName.Margin = new System.Windows.Forms.Padding(2);
            this.txtTagName.Name = "txtTagName";
            this.txtTagName.Size = new System.Drawing.Size(141, 21);
            this.txtTagName.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.txtTagName.TabIndex = 41;
            // 
            // txtDataBlock
            // 
            this.txtDataBlock.DropDownWidth = 188;
            this.txtDataBlock.IntegralHeight = false;
            this.txtDataBlock.Location = new System.Drawing.Point(100, 104);
            this.txtDataBlock.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataBlock.Name = "txtDataBlock";
            this.txtDataBlock.Size = new System.Drawing.Size(141, 21);
            this.txtDataBlock.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.txtDataBlock.TabIndex = 40;
            this.txtDataBlock.SelectedIndexChanged += new System.EventHandler(this.txtDataBlock_SelectedIndexChanged);
            // 
            // txtDevice
            // 
            this.txtDevice.DropDownWidth = 188;
            this.txtDevice.IntegralHeight = false;
            this.txtDevice.Location = new System.Drawing.Point(359, 78);
            this.txtDevice.Margin = new System.Windows.Forms.Padding(2);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(141, 21);
            this.txtDevice.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.txtDevice.TabIndex = 39;
            this.txtDevice.SelectedIndexChanged += new System.EventHandler(this.txtDevice_SelectedIndexChanged);
            // 
            // txtChannel
            // 
            this.txtChannel.DropDownWidth = 188;
            this.txtChannel.IntegralHeight = false;
            this.txtChannel.Location = new System.Drawing.Point(100, 79);
            this.txtChannel.Margin = new System.Windows.Forms.Padding(2);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(141, 21);
            this.txtChannel.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.txtChannel.TabIndex = 38;
            this.txtChannel.SelectedIndexChanged += new System.EventHandler(this.txtChannel_SelectedIndexChanged);
            // 
            // kryptonSeparator1
            // 
            this.kryptonSeparator1.Location = new System.Drawing.Point(-1, 56);
            this.kryptonSeparator1.Name = "kryptonSeparator1";
            this.kryptonSeparator1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.kryptonSeparator1.Size = new System.Drawing.Size(525, 18);
            this.kryptonSeparator1.TabIndex = 37;
            // 
            // ColValue
            // 
            this.ColValue.DropDownWidth = 188;
            this.ColValue.IntegralHeight = false;
            this.ColValue.Items.AddRange(new object[] {
            "false",
            "true"});
            this.ColValue.Location = new System.Drawing.Point(356, 30);
            this.ColValue.Margin = new System.Windows.Forms.Padding(2);
            this.ColValue.Name = "ColValue";
            this.ColValue.Size = new System.Drawing.Size(155, 21);
            this.ColValue.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ColValue.TabIndex = 36;
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Location = new System.Drawing.Point(282, 30);
            this.kryptonLabel11.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(44, 20);
            this.kryptonLabel11.TabIndex = 35;
            this.kryptonLabel11.Values.Text = "Value:";
            // 
            // ColAlarmCalss
            // 
            this.ColAlarmCalss.DropDownWidth = 188;
            this.ColAlarmCalss.IntegralHeight = false;
            this.ColAlarmCalss.Items.AddRange(new object[] {
            "Discrete",
            "Analog"});
            this.ColAlarmCalss.Location = new System.Drawing.Point(128, 30);
            this.ColAlarmCalss.Margin = new System.Windows.Forms.Padding(2);
            this.ColAlarmCalss.Name = "ColAlarmCalss";
            this.ColAlarmCalss.Size = new System.Drawing.Size(141, 21);
            this.ColAlarmCalss.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ColAlarmCalss.TabIndex = 34;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(10, 30);
            this.kryptonLabel7.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(76, 20);
            this.kryptonLabel7.TabIndex = 33;
            this.kryptonLabel7.Values.Text = "AlarmCalss :";
            // 
            // ColAlarmText
            // 
            this.ColAlarmText.Location = new System.Drawing.Point(100, 142);
            this.ColAlarmText.Margin = new System.Windows.Forms.Padding(2);
            this.ColAlarmText.Multiline = true;
            this.ColAlarmText.Name = "ColAlarmText";
            this.ColAlarmText.Size = new System.Drawing.Size(422, 143);
            this.ColAlarmText.TabIndex = 32;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(18, 103);
            this.kryptonLabel6.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(68, 20);
            this.kryptonLabel6.TabIndex = 13;
            this.kryptonLabel6.Values.Text = "DataBlock:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(263, 104);
            this.kryptonLabel5.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(66, 20);
            this.kryptonLabel5.TabIndex = 10;
            this.kryptonLabel5.Values.Text = "TagName:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(263, 80);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(49, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Device:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(18, 79);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(58, 20);
            this.kryptonLabel4.TabIndex = 5;
            this.kryptonLabel4.Values.Text = "Channel:";
            // 
            // ColName
            // 
            this.ColName.Location = new System.Drawing.Point(128, 6);
            this.ColName.Margin = new System.Windows.Forms.Padding(2);
            this.ColName.Name = "ColName";
            this.ColName.Size = new System.Drawing.Size(141, 20);
            this.ColName.TabIndex = 2;
            this.ColName.Text = " ";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(10, 6);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(46, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Name:";
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(178, 332);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(177, 34);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(355, 332);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(177, 34);
            this.btnOK.TabIndex = 9;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmAddAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 366);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Name = "FrmAddAlarm";
            this.Text = "AddAlarm ";
            this.Load += new System.EventHandler(this.FrmAddAlarm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTagName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDevice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColAlarmCalss)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox txtTagName;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox txtDataBlock;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox txtDevice;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox txtChannel;
        private ComponentFactory.Krypton.Toolkit.KryptonSeparator kryptonSeparator1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ColValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ColAlarmCalss;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox ColAlarmText;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox ColName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
    }
}