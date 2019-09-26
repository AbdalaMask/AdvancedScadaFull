namespace AdvancedScada.Studio.Editors
{
    partial class XDeviceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XDeviceForm));
            this.DxErrorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.txtDesp = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtSlaveId = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtDeviceName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtDeviceId = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtChannelName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtChannelID = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DxErrorProvider1
            // 
            this.DxErrorProvider1.ContainerControl = this;
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonHeaderGroup2);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtSlaveId);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDeviceName);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDeviceId);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtChannelName);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtChannelID);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(481, 215);
            this.kryptonHeaderGroup1.TabIndex = 4;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Device";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.Studio.Properties.Resources.AddDevice;
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "";
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup2.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(0, 98);
            this.kryptonHeaderGroup2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.txtDesp);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(479, 94);
            this.kryptonHeaderGroup2.TabIndex = 12;
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "Description:";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = global::AdvancedScada.Studio.Properties.Resources.AddDevice;
            this.kryptonHeaderGroup2.ValuesSecondary.Heading = "";
            // 
            // txtDesp
            // 
            this.txtDesp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDesp.Location = new System.Drawing.Point(0, 0);
            this.txtDesp.Multiline = true;
            this.txtDesp.Name = "txtDesp";
            this.txtDesp.Size = new System.Drawing.Size(477, 70);
            this.txtDesp.TabIndex = 11;
            // 
            // txtSlaveId
            // 
            this.txtSlaveId.Location = new System.Drawing.Point(83, 52);
            this.txtSlaveId.Name = "txtSlaveId";
            this.txtSlaveId.Size = new System.Drawing.Size(382, 22);
            this.txtSlaveId.StateCommon.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.txtSlaveId.TabIndex = 9;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(8, 52);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(52, 20);
            this.kryptonLabel5.TabIndex = 8;
            this.kryptonLabel5.Values.Text = "SlaveId:";
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.Location = new System.Drawing.Point(324, 20);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(141, 20);
            this.txtDeviceName.TabIndex = 7;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(230, 20);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(85, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Device Name:";
            // 
            // txtDeviceId
            // 
            this.txtDeviceId.Location = new System.Drawing.Point(83, 20);
            this.txtDeviceId.Name = "txtDeviceId";
            this.txtDeviceId.Size = new System.Drawing.Size(129, 20);
            this.txtDeviceId.TabIndex = 5;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(7, 20);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel4.TabIndex = 4;
            this.kryptonLabel4.Values.Text = "Device Id:";
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(324, 3);
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.Size = new System.Drawing.Size(141, 20);
            this.txtChannelName.TabIndex = 3;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(230, 3);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(94, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Channel Name:";
            // 
            // txtChannelID
            // 
            this.txtChannelID.Location = new System.Drawing.Point(83, 3);
            this.txtChannelID.Name = "txtChannelID";
            this.txtChannelID.Size = new System.Drawing.Size(129, 20);
            this.txtChannelID.TabIndex = 1;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(7, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(72, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Channel Id:";
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(407, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 32);
            this.btnOK.TabIndex = 5;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(333, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 32);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnCancel);
            this.kryptonPanel1.Controls.Add(this.btnOK);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 215);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(481, 32);
            this.kryptonPanel1.TabIndex = 5;
            // 
            // XDeviceForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(481, 247);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(497, 285);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(497, 285);
            this.Name = "XDeviceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeviceForm";
            this.Load += new System.EventHandler(this.XUserDeviceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider DxErrorProvider1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDesp;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txtSlaveId;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDeviceName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDeviceId;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtChannelName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtChannelID;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
    }
}