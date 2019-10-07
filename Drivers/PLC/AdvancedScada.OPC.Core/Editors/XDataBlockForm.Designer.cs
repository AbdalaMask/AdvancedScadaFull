namespace AdvancedScada.OPC.Core.Editors
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node3", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XDataBlockForm));
            this.DxErrorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
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
            this.gpDataBlock = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.channelsTree = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.connectButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtOPCServer = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtOPCServerPath = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpDataBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpDataBlock.Panel)).BeginInit();
            this.gpDataBlock.Panel.SuspendLayout();
            this.gpDataBlock.SuspendLayout();
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
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(434, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnOK.Location = new System.Drawing.Point(309, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(125, 28);
            this.btnOK.TabIndex = 5;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gpDataBlock
            // 
            this.gpDataBlock.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpDataBlock.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.gpDataBlock.HeaderVisibleSecondary = false;
            this.gpDataBlock.Location = new System.Drawing.Point(0, 0);
            this.gpDataBlock.Margin = new System.Windows.Forms.Padding(2);
            this.gpDataBlock.Name = "gpDataBlock";
            // 
            // gpDataBlock.Panel
            // 
            this.gpDataBlock.Panel.Controls.Add(this.kryptonHeaderGroup2);
            this.gpDataBlock.Panel.Controls.Add(this.connectButton);
            this.gpDataBlock.Panel.Controls.Add(this.txtOPCServer);
            this.gpDataBlock.Panel.Controls.Add(this.kryptonLabel8);
            this.gpDataBlock.Panel.Controls.Add(this.txtOPCServerPath);
            this.gpDataBlock.Panel.Controls.Add(this.kryptonLabel7);
            this.gpDataBlock.Panel.Controls.Add(this.txtDataBlock);
            this.gpDataBlock.Panel.Controls.Add(this.kryptonLabel6);
            this.gpDataBlock.Panel.Controls.Add(this.txtDataBlockId);
            this.gpDataBlock.Panel.Controls.Add(this.kryptonLabel5);
            this.gpDataBlock.Panel.Controls.Add(this.txtDeviceName);
            this.gpDataBlock.Panel.Controls.Add(this.txtDeviceId);
            this.gpDataBlock.Panel.Controls.Add(this.kryptonLabel3);
            this.gpDataBlock.Panel.Controls.Add(this.kryptonLabel4);
            this.gpDataBlock.Panel.Controls.Add(this.txtChannelName);
            this.gpDataBlock.Panel.Controls.Add(this.txtChannelId);
            this.gpDataBlock.Panel.Controls.Add(this.kryptonLabel2);
            this.gpDataBlock.Panel.Controls.Add(this.kryptonLabel1);
            this.gpDataBlock.Size = new System.Drawing.Size(553, 429);
            this.gpDataBlock.TabIndex = 3;
            this.gpDataBlock.ValuesPrimary.Image = global::AdvancedScada.OPC.Core.Properties.Resources.AddGoup;
            this.gpDataBlock.ValuesSecondary.Heading = "Heading";
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup2.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(4, 172);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.channelsTree);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(548, 230);
            this.kryptonHeaderGroup2.TabIndex = 20;
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "ChannelName";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = global::AdvancedScada.OPC.Core.Properties.Resources.AddChannel;
            // 
            // channelsTree
            // 
            this.channelsTree.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridHeaderRowList;
            this.channelsTree.CheckBoxes = true;
            this.channelsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelsTree.Location = new System.Drawing.Point(0, 0);
            this.channelsTree.Margin = new System.Windows.Forms.Padding(5);
            this.channelsTree.Name = "channelsTree";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.Name = "Node2";
            treeNode2.Text = "Node2";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Node0";
            treeNode4.Name = "Node4";
            treeNode4.Text = "Node4";
            treeNode5.Name = "Node5";
            treeNode5.Text = "Node5";
            treeNode6.Name = "Node3";
            treeNode6.Text = "Node3";
            treeNode7.Name = "Node7";
            treeNode7.Text = "Node7";
            treeNode8.Name = "Node8";
            treeNode8.Text = "Node8";
            treeNode9.Name = "Node6";
            treeNode9.Text = "Node6";
            this.channelsTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode9});
            this.channelsTree.Size = new System.Drawing.Size(546, 206);
            this.channelsTree.TabIndex = 2;
            this.channelsTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ChannelsTree_AfterCheck);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(180, 141);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(180, 25);
            this.connectButton.TabIndex = 19;
            this.connectButton.Values.Text = "Connect";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // txtOPCServer
            // 
            this.txtOPCServer.Location = new System.Drawing.Point(109, 116);
            this.txtOPCServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtOPCServer.Name = "txtOPCServer";
            this.txtOPCServer.Size = new System.Drawing.Size(363, 20);
            this.txtOPCServer.TabIndex = 18;
            this.txtOPCServer.Text = " ";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(10, 115);
            this.kryptonLabel8.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(81, 20);
            this.kryptonLabel8.TabIndex = 17;
            this.kryptonLabel8.Values.Text = "OPCServer   :";
            // 
            // txtOPCServerPath
            // 
            this.txtOPCServerPath.Location = new System.Drawing.Point(109, 90);
            this.txtOPCServerPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtOPCServerPath.Name = "txtOPCServerPath";
            this.txtOPCServerPath.Size = new System.Drawing.Size(363, 20);
            this.txtOPCServerPath.TabIndex = 16;
            this.txtOPCServerPath.Text = " opcda://localhost";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(10, 90);
            this.kryptonLabel7.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(95, 20);
            this.kryptonLabel7.TabIndex = 15;
            this.kryptonLabel7.Values.Text = "OPCServerPath:";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnOK);
            this.kryptonPanel1.Controls.Add(this.btnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 429);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(553, 28);
            this.kryptonPanel1.TabIndex = 6;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Localhost.png");
            this.imageList1.Images.SetKeyName(1, "ServerOn.png");
            this.imageList1.Images.SetKeyName(2, "DAGroup.png");
            this.imageList1.Images.SetKeyName(3, "TreeView_16x16.png");
            this.imageList1.Images.SetKeyName(4, "Database_16x16.png");
            this.imageList1.Images.SetKeyName(5, "ServerOff.png");
            // 
            // XDataBlockForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(553, 457);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.gpDataBlock);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XDataBlockForm";
            this.Text = "XDataBlockForm - (Administrator)";
            this.Load += new System.EventHandler(this.XUserDataBlockForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpDataBlock.Panel)).EndInit();
            this.gpDataBlock.Panel.ResumeLayout(false);
            this.gpDataBlock.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpDataBlock)).EndInit();
            this.gpDataBlock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
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
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup gpDataBlock;
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
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton connectButton;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtOPCServer;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtOPCServerPath;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView channelsTree;
        private System.Windows.Forms.ImageList imageList1;
    }
}