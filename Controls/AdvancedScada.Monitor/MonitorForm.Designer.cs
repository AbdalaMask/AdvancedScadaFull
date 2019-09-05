namespace AdvancedScada.Monitor
{
    partial class MonitorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorForm));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lblSelectedTagName = new System.Windows.Forms.ToolStripLabel();
            this.kryptonSplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.treeViewSI = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.kryptonHeader2 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.DGMonitorForm = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.colTagId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeStamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonDockableWorkspace1 = new ComponentFactory.Krypton.Docking.KryptonDockableWorkspace();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.popupMenuLeft = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.mSetON = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.mSetOFF = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuHeading2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.mWriteTagValue = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonDockingManager1 = new ComponentFactory.Krypton.Docking.KryptonDockingManager();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGMonitorForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.lblSelectedTagName});
            this.toolStrip2.Location = new System.Drawing.Point(0, 587);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(975, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(74, 22);
            this.toolStripLabel1.Text = "SelectedTag:";
            // 
            // lblSelectedTagName
            // 
            this.lblSelectedTagName.Name = "lblSelectedTagName";
            this.lblSelectedTagName.Size = new System.Drawing.Size(86, 22);
            this.lblSelectedTagName.Text = "toolStripLabel2";
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.treeViewSI);
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kryptonHeader2);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.DGMonitorForm);
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.kryptonDockableWorkspace1);
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.kryptonHeader1);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(975, 587);
            this.kryptonSplitContainer1.SplitterDistance = 176;
            this.kryptonSplitContainer1.TabIndex = 2;
            // 
            // treeViewSI
            // 
            this.treeViewSI.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridHeaderRowList;
            this.treeViewSI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSI.Location = new System.Drawing.Point(0, 23);
            this.treeViewSI.Name = "treeViewSI";
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
            this.treeViewSI.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode9});
            this.treeViewSI.Size = new System.Drawing.Size(176, 564);
            this.treeViewSI.StateImageList = this.imageList1;
            this.treeViewSI.TabIndex = 3;
            this.treeViewSI.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSI_AfterSelect);
            // 
            // kryptonHeader2
            // 
            this.kryptonHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader2.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeader2.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader2.Name = "kryptonHeader2";
            this.kryptonHeader2.Size = new System.Drawing.Size(176, 23);
            this.kryptonHeader2.TabIndex = 2;
            this.kryptonHeader2.Values.Description = "";
            this.kryptonHeader2.Values.Heading = "ChannelList";
            this.kryptonHeader2.Values.Image = global::AdvancedScada.Monitor.Properties.Resources.AddTag;
            // 
            // DGMonitorForm
            // 
            this.DGMonitorForm.AllowUserToAddRows = false;
            this.DGMonitorForm.AllowUserToDeleteRows = false;
            this.DGMonitorForm.AllowUserToResizeRows = false;
            this.DGMonitorForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGMonitorForm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTagId,
            this.colTagName,
            this.colAddress,
            this.colDataType,
            this.colValue,
            this.colTimeStamp,
            this.colDescription});
            this.DGMonitorForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGMonitorForm.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Sheet;
            this.DGMonitorForm.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.DGMonitorForm.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.DGMonitorForm.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.DGMonitorForm.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.DGMonitorForm.HideOuterBorders = true;
            this.DGMonitorForm.Location = new System.Drawing.Point(0, 31);
            this.DGMonitorForm.MultiSelect = false;
            this.DGMonitorForm.Name = "DGMonitorForm";
            this.DGMonitorForm.RowHeadersVisible = false;
            this.DGMonitorForm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGMonitorForm.Size = new System.Drawing.Size(794, 556);
            this.DGMonitorForm.TabIndex = 4;
            this.DGMonitorForm.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGMonitorForm_CellContentDoubleClick);
            this.DGMonitorForm.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGMonitorForm_CellMouseDown);
            this.DGMonitorForm.SelectionChanged += new System.EventHandler(this.DGMonitorForm_SelectionChanged);
            // 
            // colTagId
            // 
            this.colTagId.HeaderText = "TagId";
            this.colTagId.Name = "colTagId";
            this.colTagId.ReadOnly = true;
            this.colTagId.Width = 89;
            // 
            // colTagName
            // 
            this.colTagName.HeaderText = "TagName";
            this.colTagName.Name = "colTagName";
            this.colTagName.ReadOnly = true;
            this.colTagName.Width = 90;
            // 
            // colAddress
            // 
            this.colAddress.HeaderText = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            this.colAddress.Width = 89;
            // 
            // colDataType
            // 
            this.colDataType.HeaderText = "DataType";
            this.colDataType.Name = "colDataType";
            this.colDataType.ReadOnly = true;
            this.colDataType.Width = 89;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.Width = 89;
            // 
            // colTimeStamp
            // 
            this.colTimeStamp.HeaderText = "TimeStamp";
            this.colTimeStamp.Name = "colTimeStamp";
            this.colTimeStamp.ReadOnly = true;
            this.colTimeStamp.Width = 90;
            // 
            // colDescription
            // 
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Width = 89;
            // 
            // kryptonDockableWorkspace1
            // 
            this.kryptonDockableWorkspace1.AutoHiddenHost = false;
            this.kryptonDockableWorkspace1.CompactFlags = ((ComponentFactory.Krypton.Workspace.CompactFlags)(((ComponentFactory.Krypton.Workspace.CompactFlags.RemoveEmptyCells | ComponentFactory.Krypton.Workspace.CompactFlags.RemoveEmptySequences) 
            | ComponentFactory.Krypton.Workspace.CompactFlags.PromoteLeafs)));
            this.kryptonDockableWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDockableWorkspace1.Location = new System.Drawing.Point(0, 31);
            this.kryptonDockableWorkspace1.Name = "kryptonDockableWorkspace1";
            // 
            // 
            // 
            this.kryptonDockableWorkspace1.Root.UniqueName = "5b53109fabe9406dadf51391b1e3d4f6";
            this.kryptonDockableWorkspace1.Root.WorkspaceControl = this.kryptonDockableWorkspace1;
            this.kryptonDockableWorkspace1.ShowMaximizeButton = false;
            this.kryptonDockableWorkspace1.Size = new System.Drawing.Size(794, 556);
            this.kryptonDockableWorkspace1.TabIndex = 5;
            this.kryptonDockableWorkspace1.TabStop = true;
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(794, 31);
            this.kryptonHeader1.TabIndex = 1;
            this.kryptonHeader1.Values.Heading = "TagList";
            // 
            // popupMenuLeft
            // 
            this.popupMenuLeft.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading1,
            this.kryptonContextMenuItems1});
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            this.kryptonContextMenuHeading1.Text = "Bit";
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.mSetON,
            this.mSetOFF,
            this.kryptonContextMenuHeading2,
            this.mWriteTagValue});
            // 
            // mSetON
            // 
            this.mSetON.Image = global::AdvancedScada.Monitor.Properties.Resources.IconSetSigns3_16x16;
            this.mSetON.Text = "Set ON";
            this.mSetON.Click += new System.EventHandler(this.mSetON_Click);
            // 
            // mSetOFF
            // 
            this.mSetOFF.Image = global::AdvancedScada.Monitor.Properties.Resources.IconSetRedToBlack4_16x16;
            this.mSetOFF.Text = "Set OFF";
            this.mSetOFF.Click += new System.EventHandler(this.mSetOFF_Click);
            // 
            // kryptonContextMenuHeading2
            // 
            this.kryptonContextMenuHeading2.ExtraText = "";
            this.kryptonContextMenuHeading2.Text = "WriteTagValue";
            // 
            // mWriteTagValue
            // 
            this.mWriteTagValue.Image = global::AdvancedScada.Monitor.Properties.Resources.Save;
            this.mWriteTagValue.Text = "WriteTagValue";
            this.mWriteTagValue.Click += new System.EventHandler(this.mWriteTagValue_Click);
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2007Blue;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Phone_16x16.png");
            this.imageList1.Images.SetKeyName(1, "BottomCenterHorizontalOutside_16x16.png");
            this.imageList1.Images.SetKeyName(2, "Database_16x16.png");
            // 
            // MonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 612);
            this.Controls.Add(this.kryptonSplitContainer1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "MonitorForm";
            this.Text = "Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PLC_MonitorForm_FormClosing);
            this.Load += new System.EventHandler(this.PLC_MonitorForm_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            this.kryptonSplitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            this.kryptonSplitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGMonitorForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip2;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView treeViewSI;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader2;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView DGMonitorForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeStamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu popupMenuLeft;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem mSetON;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem mSetOFF;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem mWriteTagValue;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public System.Windows.Forms.ToolStripLabel lblSelectedTagName;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        
        private ComponentFactory.Krypton.Docking.KryptonDockingManager kryptonDockingManager1;
        private ComponentFactory.Krypton.Docking.KryptonDockableWorkspace kryptonDockableWorkspace1;
        private System.Windows.Forms.ImageList imageList1;
    }
}