namespace AdvancedScada.LSIS.Core.Editors
{
    partial class XTagForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XTagForm));
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtTagName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtAddress = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtTagId = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
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
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.txtDesc = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.cboxDataType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(172, 253);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(177, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTagName
            // 
            this.txtTagName.Location = new System.Drawing.Point(348, 71);
            this.txtTagName.Margin = new System.Windows.Forms.Padding(2);
            this.txtTagName.Name = "txtTagName";
            this.txtTagName.Size = new System.Drawing.Size(155, 20);
            this.txtTagName.TabIndex = 31;
            this.txtTagName.Text = " ";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(247, 71);
            this.kryptonLabel8.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(69, 20);
            this.kryptonLabel8.TabIndex = 30;
            this.kryptonLabel8.Values.Text = "Tag Name:";
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(2, 95);
            this.kryptonLabel9.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(57, 20);
            this.kryptonLabel9.TabIndex = 29;
            this.kryptonLabel9.Values.Text = "Address:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(84, 95);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(141, 20);
            this.txtAddress.TabIndex = 28;
            // 
            // txtTagId
            // 
            this.txtTagId.Location = new System.Drawing.Point(84, 71);
            this.txtTagId.Margin = new System.Windows.Forms.Padding(2);
            this.txtTagId.Name = "txtTagId";
            this.txtTagId.Size = new System.Drawing.Size(141, 20);
            this.txtTagId.TabIndex = 27;
            this.txtTagId.Text = " ";
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(2, 71);
            this.kryptonLabel10.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(44, 20);
            this.kryptonLabel10.TabIndex = 26;
            this.kryptonLabel10.Values.Text = "TagId:";
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(349, 253);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(177, 24);
            this.btnOK.TabIndex = 7;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cboxDataType
            // 
            this.cboxDataType.DropDownWidth = 188;
            this.cboxDataType.IntegralHeight = false;
            this.cboxDataType.Location = new System.Drawing.Point(348, 94);
            this.cboxDataType.Margin = new System.Windows.Forms.Padding(2);
            this.cboxDataType.Name = "cboxDataType";
            this.cboxDataType.Size = new System.Drawing.Size(155, 21);
            this.cboxDataType.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxDataType.TabIndex = 16;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(251, 94);
            this.kryptonLabel7.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(65, 20);
            this.kryptonLabel7.TabIndex = 15;
            this.kryptonLabel7.Values.Text = "DataType:";
            // 
            // txtDataBlock
            // 
            this.txtDataBlock.Location = new System.Drawing.Point(348, 47);
            this.txtDataBlock.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataBlock.Name = "txtDataBlock";
            this.txtDataBlock.Size = new System.Drawing.Size(155, 20);
            this.txtDataBlock.TabIndex = 14;
            this.txtDataBlock.Text = " ";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(2, 47);
            this.kryptonLabel6.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(78, 20);
            this.kryptonLabel6.TabIndex = 13;
            this.kryptonLabel6.Values.Text = "DataBlockId:";
            // 
            // txtDataBlockId
            // 
            this.txtDataBlockId.Location = new System.Drawing.Point(84, 47);
            this.txtDataBlockId.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataBlockId.Name = "txtDataBlockId";
            this.txtDataBlockId.Size = new System.Drawing.Size(141, 20);
            this.txtDataBlockId.TabIndex = 11;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(244, 47);
            this.kryptonLabel5.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(100, 20);
            this.kryptonLabel5.TabIndex = 10;
            this.kryptonLabel5.Values.Text = "DataBlockName:";
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.Location = new System.Drawing.Point(348, 23);
            this.txtDeviceName.Margin = new System.Windows.Forms.Padding(2);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(155, 20);
            this.txtDeviceName.TabIndex = 8;
            this.txtDeviceName.Text = " ";
            // 
            // txtDeviceId
            // 
            this.txtDeviceId.Location = new System.Drawing.Point(84, 23);
            this.txtDeviceId.Margin = new System.Windows.Forms.Padding(2);
            this.txtDeviceId.Name = "txtDeviceId";
            this.txtDeviceId.Size = new System.Drawing.Size(141, 20);
            this.txtDeviceId.TabIndex = 7;
            this.txtDeviceId.Text = " ";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(244, 23);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(85, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Device Name:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(2, 23);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel4.TabIndex = 5;
            this.kryptonLabel4.Values.Text = "Device Id:";
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(348, 2);
            this.txtChannelName.Margin = new System.Windows.Forms.Padding(2);
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.Size = new System.Drawing.Size(155, 20);
            this.txtChannelName.TabIndex = 3;
            this.txtChannelName.Text = " ";
            // 
            // txtChannelId
            // 
            this.txtChannelId.Location = new System.Drawing.Point(84, -2);
            this.txtChannelId.Margin = new System.Windows.Forms.Padding(2);
            this.txtChannelId.Name = "txtChannelId";
            this.txtChannelId.Size = new System.Drawing.Size(141, 20);
            this.txtChannelId.TabIndex = 2;
            this.txtChannelId.Text = " ";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(244, 2);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(94, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Channel Name:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(2, 2);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(72, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Channel Id:";
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
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDesc);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtTagName);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel9);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtAddress);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtTagId);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel10);
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
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(526, 253);
            this.kryptonHeaderGroup1.TabIndex = 5;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Tag";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.LSIS.Core.Properties.Resources.AddTag;
            // 
            // txtDesc
            // 
            this.txtDesc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtDesc.Location = new System.Drawing.Point(0, 119);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(2);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(524, 89);
            this.txtDesc.TabIndex = 32;
            // 
            // XTagForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(526, 277);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XTagForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XTagForm";
            this.Load += new System.EventHandler(this.XTagForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboxDataType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtTagName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtAddress;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtTagId;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
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
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDesc;
    }
}