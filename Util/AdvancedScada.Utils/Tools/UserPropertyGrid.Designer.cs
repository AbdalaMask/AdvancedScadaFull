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
            this.HeaderGroupDataBlock = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.PvGridDataBlock = new System.Windows.Forms.PropertyGrid();
            this.HeaderGroupDevice = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.PvGridDevice = new System.Windows.Forms.PropertyGrid();
            this.HeaderGroupChannel = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.PvGridChannel = new System.Windows.Forms.PropertyGrid();
            this.buttonSpecHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonSpecHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonSpecHeaderGroup3 = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupDataBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupDataBlock.Panel)).BeginInit();
            this.HeaderGroupDataBlock.Panel.SuspendLayout();
            this.HeaderGroupDataBlock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupDevice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupDevice.Panel)).BeginInit();
            this.HeaderGroupDevice.Panel.SuspendLayout();
            this.HeaderGroupDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupChannel.Panel)).BeginInit();
            this.HeaderGroupChannel.Panel.SuspendLayout();
            this.HeaderGroupChannel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.HeaderGroupDataBlock);
            this.kryptonPanel2.Controls.Add(this.HeaderGroupDevice);
            this.kryptonPanel2.Controls.Add(this.HeaderGroupChannel);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(211, 517);
            this.kryptonPanel2.TabIndex = 3;
            // 
            // HeaderGroupDataBlock
            // 
            this.HeaderGroupDataBlock.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroup3});
            this.HeaderGroupDataBlock.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderGroupDataBlock.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.HeaderGroupDataBlock.HeaderVisibleSecondary = false;
            this.HeaderGroupDataBlock.Location = new System.Drawing.Point(0, 359);
            this.HeaderGroupDataBlock.Margin = new System.Windows.Forms.Padding(2);
            this.HeaderGroupDataBlock.Name = "HeaderGroupDataBlock";
            // 
            // HeaderGroupDataBlock.Panel
            // 
            this.HeaderGroupDataBlock.Panel.Controls.Add(this.PvGridDataBlock);
            this.HeaderGroupDataBlock.Size = new System.Drawing.Size(211, 156);
            this.HeaderGroupDataBlock.TabIndex = 4;
            this.HeaderGroupDataBlock.ValuesPrimary.Heading = "DataBlock";
            this.HeaderGroupDataBlock.ValuesPrimary.Image = global::AdvancedScada.Utils.Properties.Resources.AddGoup;
            // 
            // PvGridDataBlock
            // 
            this.PvGridDataBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PvGridDataBlock.HelpVisible = false;
            this.PvGridDataBlock.Location = new System.Drawing.Point(0, 0);
            this.PvGridDataBlock.Name = "PvGridDataBlock";
            this.PvGridDataBlock.Size = new System.Drawing.Size(209, 132);
            this.PvGridDataBlock.TabIndex = 2;
            this.PvGridDataBlock.ToolbarVisible = false;
            // 
            // HeaderGroupDevice
            // 
            this.HeaderGroupDevice.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroup2});
            this.HeaderGroupDevice.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderGroupDevice.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.HeaderGroupDevice.HeaderVisibleSecondary = false;
            this.HeaderGroupDevice.Location = new System.Drawing.Point(0, 214);
            this.HeaderGroupDevice.Margin = new System.Windows.Forms.Padding(2);
            this.HeaderGroupDevice.Name = "HeaderGroupDevice";
            // 
            // HeaderGroupDevice.Panel
            // 
            this.HeaderGroupDevice.Panel.Controls.Add(this.PvGridDevice);
            this.HeaderGroupDevice.Size = new System.Drawing.Size(211, 145);
            this.HeaderGroupDevice.TabIndex = 3;
            this.HeaderGroupDevice.ValuesPrimary.Heading = "Device";
            this.HeaderGroupDevice.ValuesPrimary.Image = global::AdvancedScada.Utils.Properties.Resources.AddDevice;
            // 
            // PvGridDevice
            // 
            this.PvGridDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PvGridDevice.HelpVisible = false;
            this.PvGridDevice.Location = new System.Drawing.Point(0, 0);
            this.PvGridDevice.Name = "PvGridDevice";
            this.PvGridDevice.Size = new System.Drawing.Size(209, 121);
            this.PvGridDevice.TabIndex = 2;
            this.PvGridDevice.ToolbarVisible = false;
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
            this.HeaderGroupChannel.Size = new System.Drawing.Size(211, 214);
            this.HeaderGroupChannel.TabIndex = 1;
            this.HeaderGroupChannel.ValuesPrimary.Heading = "Channel";
            this.HeaderGroupChannel.ValuesPrimary.Image = global::AdvancedScada.Utils.Properties.Resources.AddChannel;
            // 
            // PvGridChannel
            // 
            this.PvGridChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PvGridChannel.HelpVisible = false;
            this.PvGridChannel.Location = new System.Drawing.Point(0, 0);
            this.PvGridChannel.Name = "PvGridChannel";
            this.PvGridChannel.Size = new System.Drawing.Size(209, 190);
            this.PvGridChannel.TabIndex = 2;
            this.PvGridChannel.ToolbarVisible = false;
            // 
            // buttonSpecHeaderGroup1
            // 
            this.buttonSpecHeaderGroup1.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowUp;
            this.buttonSpecHeaderGroup1.UniqueName = "5f529c53e6bf47fbaec9d9e1caebad1b";
            // 
            // buttonSpecHeaderGroup2
            // 
            this.buttonSpecHeaderGroup2.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowUp;
            this.buttonSpecHeaderGroup2.UniqueName = "cdba6a708f8448a9997d95d0a104777c";
            // 
            // buttonSpecHeaderGroup3
            // 
            this.buttonSpecHeaderGroup3.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowUp;
            this.buttonSpecHeaderGroup3.UniqueName = "8004093246734f43aa3b1a55f2011ce0";
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
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupDataBlock.Panel)).EndInit();
            this.HeaderGroupDataBlock.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupDataBlock)).EndInit();
            this.HeaderGroupDataBlock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupDevice.Panel)).EndInit();
            this.HeaderGroupDevice.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupDevice)).EndInit();
            this.HeaderGroupDevice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupChannel.Panel)).EndInit();
            this.HeaderGroupChannel.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupChannel)).EndInit();
            this.HeaderGroupChannel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup HeaderGroupDataBlock;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup HeaderGroupChannel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup HeaderGroupDevice;
        public System.Windows.Forms.PropertyGrid PvGridDataBlock;
        public System.Windows.Forms.PropertyGrid PvGridChannel;
        public System.Windows.Forms.PropertyGrid PvGridDevice;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroup3;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroup2;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroup1;
    }
}
