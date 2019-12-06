namespace HslScada.Studio.Tools
{
    partial class UserPropertyGrid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.HeaderGroupChannel = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.PvGridChannel = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupChannel.Panel)).BeginInit();
            this.HeaderGroupChannel.Panel.SuspendLayout();
            this.HeaderGroupChannel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.HeaderGroupChannel);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(211, 517);
            this.kryptonPanel2.TabIndex = 3;
            // 
            // HeaderGroupChannel
            // 
            this.HeaderGroupChannel.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroup1});
            this.HeaderGroupChannel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderGroupChannel.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.HeaderGroupChannel.HeaderVisibleSecondary = false;
            this.HeaderGroupChannel.Location = new System.Drawing.Point(0, 0);
            this.HeaderGroupChannel.Margin = new System.Windows.Forms.Padding(2);
            this.HeaderGroupChannel.Name = "HeaderGroupChannel";
            // 
            // HeaderGroupChannel.Panel
            // 
            this.HeaderGroupChannel.Panel.Controls.Add(this.PvGridChannel);
            this.HeaderGroupChannel.Size = new System.Drawing.Size(211, 313);
            this.HeaderGroupChannel.TabIndex = 1;
            this.HeaderGroupChannel.ValuesPrimary.Heading = "Channel";
            this.HeaderGroupChannel.ValuesPrimary.Image = global::AdvancedScada.Utils.Properties.Resources.AddChannel;
            // 
            // buttonSpecHeaderGroup1
            // 
            this.buttonSpecHeaderGroup1.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowUp;
            this.buttonSpecHeaderGroup1.UniqueName = "5f529c53e6bf47fbaec9d9e1caebad1b";
            // 
            // PvGridChannel
            // 
            this.PvGridChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PvGridChannel.HelpVisible = false;
            this.PvGridChannel.Location = new System.Drawing.Point(0, 0);
            this.PvGridChannel.Name = "PvGridChannel";
            this.PvGridChannel.Size = new System.Drawing.Size(209, 289);
            this.PvGridChannel.TabIndex = 2;
            this.PvGridChannel.ToolbarVisible = false;
            // 
            // UserPropertyGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel2);
            this.Name = "UserPropertyGrid";
            this.Size = new System.Drawing.Size(211, 517);
            this.Load += new System.EventHandler(this.UserPropertyGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupChannel.Panel)).EndInit();
            this.HeaderGroupChannel.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupChannel)).EndInit();
            this.HeaderGroupChannel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup HeaderGroupChannel;
        public System.Windows.Forms.PropertyGrid PvGridChannel;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroup1;
    }
}
