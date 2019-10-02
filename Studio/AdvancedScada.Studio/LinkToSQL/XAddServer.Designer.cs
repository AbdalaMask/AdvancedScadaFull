using AdvancedScada.Utils.Net;

namespace AdvancedScada.Studio.LinkToSQL
{
    partial class XAddServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XAddServer));
            this.kryptonHeaderGroup4 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.txtPasswerd = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel16 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtUserName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtDesc = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtServerName = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtServerId = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4.Panel)).BeginInit();
            this.kryptonHeaderGroup4.Panel.SuspendLayout();
            this.kryptonHeaderGroup4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonHeaderGroup4
            // 
            this.kryptonHeaderGroup4.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup4.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup4.Location = new System.Drawing.Point(0, 282);
            this.kryptonHeaderGroup4.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonHeaderGroup4.Name = "kryptonHeaderGroup4";
            // 
            // kryptonHeaderGroup4.Panel
            // 
            this.kryptonHeaderGroup4.Panel.Controls.Add(this.btnCancel);
            this.kryptonHeaderGroup4.Panel.Controls.Add(this.btnOK);
            this.kryptonHeaderGroup4.Size = new System.Drawing.Size(609, 57);
            this.kryptonHeaderGroup4.TabIndex = 6;
            this.kryptonHeaderGroup4.ValuesPrimary.Image = global::AdvancedScada.Studio.Properties.Resources.AddChannel;
            this.kryptonHeaderGroup4.ValuesSecondary.Heading = "";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(367, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 33);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(487, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 33);
            this.btnOK.TabIndex = 3;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.txtPasswerd);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.kryptonLabel16);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.txtUserName);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.txtDesc);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.txtServerName);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.txtServerId);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(609, 280);
            this.kryptonHeaderGroup2.TabIndex = 7;
            // 
            // txtPasswerd
            // 
            this.txtPasswerd.Location = new System.Drawing.Point(406, 33);
            this.txtPasswerd.Margin = new System.Windows.Forms.Padding(4);
            this.txtPasswerd.Name = "txtPasswerd";
            this.txtPasswerd.Size = new System.Drawing.Size(191, 20);
            this.txtPasswerd.TabIndex = 37;
            // 
            // kryptonLabel16
            // 
            this.kryptonLabel16.Location = new System.Drawing.Point(312, 33);
            this.kryptonLabel16.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel16.Name = "kryptonLabel16";
            this.kryptonLabel16.Size = new System.Drawing.Size(64, 20);
            this.kryptonLabel16.TabIndex = 36;
            this.kryptonLabel16.Values.Text = "Passwerd:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(101, 37);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(191, 20);
            this.txtUserName.TabIndex = 35;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(7, 37);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(74, 20);
            this.kryptonLabel3.TabIndex = 34;
            this.kryptonLabel3.Values.Text = "User Name:";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(101, 69);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(496, 145);
            this.txtDesc.TabIndex = 33;
            // 
            // txtServerName
            // 
            this.txtServerName.DropDownWidth = 163;
            this.txtServerName.IntegralHeight = false;
            this.txtServerName.Location = new System.Drawing.Point(406, 4);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(191, 21);
            this.txtServerName.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.txtServerName.TabIndex = 32;
            // 
            // txtServerId
            // 
            this.txtServerId.Location = new System.Drawing.Point(101, 4);
            this.txtServerId.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerId.Name = "txtServerId";
            this.txtServerId.Size = new System.Drawing.Size(191, 20);
            this.txtServerId.TabIndex = 31;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(7, 68);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel4.TabIndex = 30;
            this.kryptonLabel4.Values.Text = "Description:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(301, 4);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(80, 20);
            this.kryptonLabel2.TabIndex = 29;
            this.kryptonLabel2.Values.Text = "ServerName:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(7, 4);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(61, 20);
            this.kryptonLabel1.TabIndex = 28;
            this.kryptonLabel1.Values.Text = "Server Id:";
            // 
            // XAddServer
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(609, 342);
            this.Controls.Add(this.kryptonHeaderGroup2);
            this.Controls.Add(this.kryptonHeaderGroup4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XAddServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQLServer :Add New";
            this.Load += new System.EventHandler(this.XtraSQLAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4.Panel)).EndInit();
            this.kryptonHeaderGroup4.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4)).EndInit();
            this.kryptonHeaderGroup4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPasswerd;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel16;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtUserName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDesc;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox txtServerName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtServerId;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
    }
}