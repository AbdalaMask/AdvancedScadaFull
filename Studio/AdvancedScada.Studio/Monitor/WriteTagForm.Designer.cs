namespace AdvancedScada.Studio.Monitor
{
    partial class WriteTagForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriteTagForm));
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSend = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtValue = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtAddress = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(4, 4);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(57, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Address:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(4, 32);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(44, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Value:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(235, 130);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(346, 130);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(110, 30);
            this.btnSend.TabIndex = 5;
            this.btnSend.Values.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(69, 32);
            this.txtValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(355, 20);
            this.txtValue.TabIndex = 3;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(69, 4);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(355, 20);
            this.txtAddress.TabIndex = 6;
            this.txtAddress.Text = " ";
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtAddress);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtValue);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(456, 130);
            this.kryptonHeaderGroup1.TabIndex = 7;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "WriteTag";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.Studio.Properties.Resources.AddTag;
            // 
            // WriteTagForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(456, 161);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockActive;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(472, 199);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(472, 199);
            this.Name = "WriteTagForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WriteTag";
            this.Load += new System.EventHandler(this.WriteTagForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSend;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtValue;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtAddress;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
    }
}