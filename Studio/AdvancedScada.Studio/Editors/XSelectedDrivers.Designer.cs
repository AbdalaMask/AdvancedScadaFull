namespace AdvancedScada.Studio.Editors
{
    partial class XSelectedDrivers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSelectedDrivers));
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.cboxSelectedDrivers = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.picSelectedDrivers = new System.Windows.Forms.PictureBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxSelectedDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectedDrivers)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cboxSelectedDrivers);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.picSelectedDrivers);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(473, 284);
            this.kryptonHeaderGroup1.TabIndex = 0;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "SelectedDrivers";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.Studio.Properties.Resources.BottomCenterHorizontalOutside_32x321;
            // 
            // cboxSelectedDrivers
            // 
            this.cboxSelectedDrivers.DropDownWidth = 376;
            this.cboxSelectedDrivers.IntegralHeight = false;
            this.cboxSelectedDrivers.Location = new System.Drawing.Point(95, 3);
            this.cboxSelectedDrivers.Name = "cboxSelectedDrivers";
            this.cboxSelectedDrivers.Size = new System.Drawing.Size(376, 21);
            this.cboxSelectedDrivers.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxSelectedDrivers.TabIndex = 3;
            this.cboxSelectedDrivers.Text = "LSIS";
            this.cboxSelectedDrivers.SelectedIndexChanged += new System.EventHandler(this.cboxSelectedDrivers_SelectedIndexChanged);
            // 
            // picSelectedDrivers
            // 
            this.picSelectedDrivers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picSelectedDrivers.Image = global::AdvancedScada.Studio.Properties.Resources.img_wemx_designer_5;
            this.picSelectedDrivers.Location = new System.Drawing.Point(0, 29);
            this.picSelectedDrivers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picSelectedDrivers.Name = "picSelectedDrivers";
            this.picSelectedDrivers.Size = new System.Drawing.Size(471, 217);
            this.picSelectedDrivers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSelectedDrivers.TabIndex = 2;
            this.picSelectedDrivers.TabStop = false;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 0);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(101, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "SelectedDrivers :";
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOK.Location = new System.Drawing.Point(0, 284);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(236, 34);
            this.btnOK.TabIndex = 4;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(237, 284);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(236, 34);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // XSelectedDrivers
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(473, 318);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XSelectedDrivers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectedDrivers";
            this.Load += new System.EventHandler(this.XSelectedDrivers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboxSelectedDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectedDrivers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.PictureBox picSelectedDrivers;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        public ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxSelectedDrivers;
    }
}