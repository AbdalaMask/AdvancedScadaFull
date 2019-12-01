
namespace AdvancedScada.Studio.Editors
{
    partial class XTagManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XTagManager));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.barButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.barButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.barButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lblTagCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.lblSelectedTagName = new System.Windows.Forms.ToolStripLabel();
            this.kryptonSplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.treeViewSI = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.popupMenuLeft = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems4 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.ItemAddChannel = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.ItemAddDevice = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.ItemAddDataBlock = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuHeading2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.ItemImport = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.ItemExport = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuHeading4 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems3 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.ItemCopy = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.ItemPaste = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.ItemDelete = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.kryptonHeader2 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.DGMonitorForm = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.colTagId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonDockableWorkspace1 = new ComponentFactory.Krypton.Docking.KryptonDockableWorkspace();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonContextMenuItems2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuHeading3 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.popupMenuRight = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading5 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems5 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.ItemAddTag = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.ItemEditor = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.mDeleteTag = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuHeading6 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems6 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.ItemCopyToTag = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.RItemCopy = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.RItemPaste = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonDockingManager1 = new ComponentFactory.Krypton.Docking.KryptonDockingManager();
            this.btnButtonExportTags = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
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
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.barButtonNew,
            this.toolStripSeparator2,
            this.barButtonOpen,
            this.toolStripSeparator3,
            this.barButtonSave,
            this.toolStripSeparator4,
            this.btnButtonExportTags});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(963, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // barButtonNew
            // 
            this.barButtonNew.Image = global::AdvancedScada.Studio.Properties.Resources.New;
            this.barButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.barButtonNew.Name = "barButtonNew";
            this.barButtonNew.Size = new System.Drawing.Size(51, 22);
            this.barButtonNew.Text = "New";
            this.barButtonNew.Click += new System.EventHandler(this.barButtonNew_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // barButtonOpen
            // 
            this.barButtonOpen.Image = global::AdvancedScada.Studio.Properties.Resources.Open;
            this.barButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.barButtonOpen.Name = "barButtonOpen";
            this.barButtonOpen.Size = new System.Drawing.Size(56, 22);
            this.barButtonOpen.Text = "Open";
            this.barButtonOpen.Click += new System.EventHandler(this.barButtonOpen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // barButtonSave
            // 
            this.barButtonSave.Image = global::AdvancedScada.Studio.Properties.Resources.Save;
            this.barButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.barButtonSave.Name = "barButtonSave";
            this.barButtonSave.Size = new System.Drawing.Size(51, 22);
            this.barButtonSave.Text = "Save";
            this.barButtonSave.Click += new System.EventHandler(this.barButtonSave_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.lblTagCount,
            this.toolStripSeparator5,
            this.toolStripLabel2,
            this.lblSelectedTagName});
            this.toolStrip2.Location = new System.Drawing.Point(0, 614);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(963, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(63, 22);
            this.toolStripLabel1.Text = "TagCount:";
            // 
            // lblTagCount
            // 
            this.lblTagCount.Name = "lblTagCount";
            this.lblTagCount.Size = new System.Drawing.Size(13, 22);
            this.lblTagCount.Text = "0";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(109, 22);
            this.toolStripLabel2.Text = "SelectedTagName :";
            // 
            // lblSelectedTagName
            // 
            this.lblSelectedTagName.Name = "lblSelectedTagName";
            this.lblSelectedTagName.Size = new System.Drawing.Size(13, 22);
            this.lblSelectedTagName.Text = "0";
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 25);
            this.kryptonSplitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(963, 589);
            this.kryptonSplitContainer1.SplitterDistance = 168;
            this.kryptonSplitContainer1.TabIndex = 6;
            // 
            // treeViewSI
            // 
            this.treeViewSI.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridHeaderRowList;
            this.treeViewSI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSI.KryptonContextMenu = this.popupMenuLeft;
            this.treeViewSI.Location = new System.Drawing.Point(0, 23);
            this.treeViewSI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.treeViewSI.Size = new System.Drawing.Size(168, 566);
            this.treeViewSI.StateImageList = this.imageList1;
            this.treeViewSI.TabIndex = 1;
            this.treeViewSI.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSI_AfterSelect);
            this.treeViewSI.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewSI_NodeMouseClick);
            this.treeViewSI.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewSI_NodeMouseDoubleClick);
            this.treeViewSI.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewSI_MouseClick);
            this.treeViewSI.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewSI_MouseDown);
            // 
            // popupMenuLeft
            // 
            this.popupMenuLeft.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading1,
            this.kryptonContextMenuItems4,
            this.kryptonContextMenuHeading2,
            this.kryptonContextMenuItems1,
            this.kryptonContextMenuHeading4,
            this.kryptonContextMenuItems3});
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            this.kryptonContextMenuHeading1.Image = global::AdvancedScada.Studio.Properties.Resources.AddChannel;
            this.kryptonContextMenuHeading1.Text = "Channel";
            // 
            // kryptonContextMenuItems4
            // 
            this.kryptonContextMenuItems4.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.ItemAddChannel,
            this.ItemAddDevice,
            this.ItemAddDataBlock});
            // 
            // ItemAddChannel
            // 
            this.ItemAddChannel.Image = global::AdvancedScada.Studio.Properties.Resources.AddChannel;
            this.ItemAddChannel.Text = "AddChannel";
            this.ItemAddChannel.Click += new System.EventHandler(this.ItemAddChannel_Click);
            // 
            // ItemAddDevice
            // 
            this.ItemAddDevice.Image = global::AdvancedScada.Studio.Properties.Resources.AddDevice;
            this.ItemAddDevice.Text = "AddDevice";
            this.ItemAddDevice.Click += new System.EventHandler(this.ItemAddDevice_Click);
            // 
            // ItemAddDataBlock
            // 
            this.ItemAddDataBlock.Image = global::AdvancedScada.Studio.Properties.Resources.AddGoup;
            this.ItemAddDataBlock.Text = "AddDataBlock";
            this.ItemAddDataBlock.Click += new System.EventHandler(this.ItemAddDataBlock_Click);
            // 
            // kryptonContextMenuHeading2
            // 
            this.kryptonContextMenuHeading2.ExtraText = "";
            this.kryptonContextMenuHeading2.Image = global::AdvancedScada.Studio.Properties.Resources.Tag;
            this.kryptonContextMenuHeading2.Text = "Excel";
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.ItemImport,
            this.ItemExport});
            // 
            // ItemImport
            // 
            this.ItemImport.Image = global::AdvancedScada.Studio.Properties.Resources.ExportToXLS_16x16;
            this.ItemImport.Text = "Import";
            this.ItemImport.Click += new System.EventHandler(this.ItemImport_Click);
            // 
            // ItemExport
            // 
            this.ItemExport.Image = global::AdvancedScada.Studio.Properties.Resources.SaveAs;
            this.ItemExport.Text = "Export";
            this.ItemExport.Click += new System.EventHandler(this.ItemExport_Click);
            // 
            // kryptonContextMenuHeading4
            // 
            this.kryptonContextMenuHeading4.ExtraText = "";
            // 
            // kryptonContextMenuItems3
            // 
            this.kryptonContextMenuItems3.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.ItemCopy,
            this.ItemPaste,
            this.ItemDelete});
            // 
            // ItemCopy
            // 
            this.ItemCopy.Image = global::AdvancedScada.Studio.Properties.Resources.Copy_16x16;
            this.ItemCopy.Text = "Copy";
            this.ItemCopy.Click += new System.EventHandler(this.ItemCopy_Click);
            // 
            // ItemPaste
            // 
            this.ItemPaste.Image = global::AdvancedScada.Studio.Properties.Resources.Paste_16x16;
            this.ItemPaste.Text = "Paste";
            this.ItemPaste.Click += new System.EventHandler(this.ItemPaste_Click);
            // 
            // ItemDelete
            // 
            this.ItemDelete.Image = global::AdvancedScada.Studio.Properties.Resources.Exit;
            this.ItemDelete.Text = "Delete";
            this.ItemDelete.Click += new System.EventHandler(this.ItemDelete_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Phone_16x16.png");
            this.imageList1.Images.SetKeyName(1, "BottomCenterHorizontalOutside_16x16.png");
            this.imageList1.Images.SetKeyName(2, "Database_16x16.png");
            // 
            // kryptonHeader2
            // 
            this.kryptonHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader2.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeader2.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonHeader2.Name = "kryptonHeader2";
            this.kryptonHeader2.Size = new System.Drawing.Size(168, 23);
            this.kryptonHeader2.TabIndex = 0;
            this.kryptonHeader2.Values.Description = "";
            this.kryptonHeader2.Values.Heading = "ChannelList";
            this.kryptonHeader2.Values.Image = global::AdvancedScada.Studio.Properties.Resources.AddTag;
            // 
            // DGMonitorForm
            // 
            this.DGMonitorForm.AllowUserToAddRows = false;
            this.DGMonitorForm.AllowUserToDeleteRows = false;
            this.DGMonitorForm.AllowUserToResizeRows = false;
            this.DGMonitorForm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGMonitorForm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTagId,
            this.colTagName,
            this.colAddress,
            this.colDataType,
            this.colDescription});
            this.DGMonitorForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGMonitorForm.HideOuterBorders = true;
            this.DGMonitorForm.Location = new System.Drawing.Point(0, 23);
            this.DGMonitorForm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGMonitorForm.MultiSelect = false;
            this.DGMonitorForm.Name = "DGMonitorForm";
            this.DGMonitorForm.RowHeadersVisible = false;
            this.DGMonitorForm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGMonitorForm.Size = new System.Drawing.Size(790, 566);
            this.DGMonitorForm.TabIndex = 3;
            this.DGMonitorForm.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DGMonitorForm_MouseDoubleClick);
            this.DGMonitorForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGMonitorForm_MouseDown);
            // 
            // colTagId
            // 
            this.colTagId.HeaderText = "TagId";
            this.colTagId.Name = "colTagId";
            this.colTagId.ReadOnly = true;
            // 
            // colTagName
            // 
            this.colTagName.HeaderText = "TagName";
            this.colTagName.Name = "colTagName";
            this.colTagName.ReadOnly = true;
            // 
            // colAddress
            // 
            this.colAddress.HeaderText = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            // 
            // colDataType
            // 
            this.colDataType.HeaderText = "DataType";
            this.colDataType.Name = "colDataType";
            this.colDataType.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // kryptonDockableWorkspace1
            // 
            this.kryptonDockableWorkspace1.AutoHiddenHost = false;
            this.kryptonDockableWorkspace1.CompactFlags = ((ComponentFactory.Krypton.Workspace.CompactFlags)(((ComponentFactory.Krypton.Workspace.CompactFlags.RemoveEmptyCells | ComponentFactory.Krypton.Workspace.CompactFlags.RemoveEmptySequences) 
            | ComponentFactory.Krypton.Workspace.CompactFlags.PromoteLeafs)));
            this.kryptonDockableWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDockableWorkspace1.Location = new System.Drawing.Point(0, 23);
            this.kryptonDockableWorkspace1.Name = "kryptonDockableWorkspace1";
            // 
            // 
            // 
            this.kryptonDockableWorkspace1.Root.UniqueName = "5b53109fabe9406dadf51391b1e3d4f6";
            this.kryptonDockableWorkspace1.Root.WorkspaceControl = this.kryptonDockableWorkspace1;
            this.kryptonDockableWorkspace1.ShowMaximizeButton = false;
            this.kryptonDockableWorkspace1.Size = new System.Drawing.Size(790, 566);
            this.kryptonDockableWorkspace1.TabIndex = 0;
            this.kryptonDockableWorkspace1.TabStop = true;
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(790, 23);
            this.kryptonHeader1.TabIndex = 1;
            this.kryptonHeader1.Values.Description = "";
            this.kryptonHeader1.Values.Heading = "TagList";
            this.kryptonHeader1.Values.Image = global::AdvancedScada.Studio.Properties.Resources.AddTag;
            // 
            // kryptonContextMenuHeading3
            // 
            this.kryptonContextMenuHeading3.ExtraText = "";
            // 
            // popupMenuRight
            // 
            this.popupMenuRight.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading5,
            this.kryptonContextMenuItems5,
            this.kryptonContextMenuHeading6,
            this.kryptonContextMenuItems6});
            // 
            // kryptonContextMenuHeading5
            // 
            this.kryptonContextMenuHeading5.ExtraText = "";
            this.kryptonContextMenuHeading5.Image = global::AdvancedScada.Studio.Properties.Resources.AddTag;
            this.kryptonContextMenuHeading5.Text = "Tag";
            // 
            // kryptonContextMenuItems5
            // 
            this.kryptonContextMenuItems5.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.ItemAddTag,
            this.ItemEditor,
            this.mDeleteTag});
            // 
            // ItemAddTag
            // 
            this.ItemAddTag.Image = global::AdvancedScada.Studio.Properties.Resources.AddTag;
            this.ItemAddTag.Text = "Add Tag";
            this.ItemAddTag.Click += new System.EventHandler(this.ItemAddTag_Click);
            // 
            // ItemEditor
            // 
            this.ItemEditor.Image = global::AdvancedScada.Studio.Properties.Resources.Edit_16x16;
            this.ItemEditor.Text = "Editor Tag";
            this.ItemEditor.Click += new System.EventHandler(this.ItemEditor_Click);
            // 
            // mDeleteTag
            // 
            this.mDeleteTag.Image = global::AdvancedScada.Studio.Properties.Resources.Exit;
            this.mDeleteTag.Text = "Delete Tag";
            this.mDeleteTag.Click += new System.EventHandler(this.mDeleteTag_Click);
            // 
            // kryptonContextMenuHeading6
            // 
            this.kryptonContextMenuHeading6.ExtraText = "";
            this.kryptonContextMenuHeading6.Text = "To PLC";
            // 
            // kryptonContextMenuItems6
            // 
            this.kryptonContextMenuItems6.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.ItemCopyToTag,
            this.RItemCopy,
            this.RItemPaste});
            // 
            // ItemCopyToTag
            // 
            this.ItemCopyToTag.Image = global::AdvancedScada.Studio.Properties.Resources.Tag;
            this.ItemCopyToTag.Text = "CopyToTag";
            this.ItemCopyToTag.Click += new System.EventHandler(this.ItemCopyToTag_Click);
            // 
            // RItemCopy
            // 
            this.RItemCopy.Image = global::AdvancedScada.Studio.Properties.Resources.Copy_16x16;
            this.RItemCopy.Text = "Copy";
            this.RItemCopy.Click += new System.EventHandler(this.RItemCopy_Click);
            // 
            // RItemPaste
            // 
            this.RItemPaste.Image = global::AdvancedScada.Studio.Properties.Resources.Paste_16x16;
            this.RItemPaste.Text = "Paste";
            this.RItemPaste.Click += new System.EventHandler(this.RItemPaste_Click);
            // 
            // btnButtonExportTags
            // 
            this.btnButtonExportTags.Image = global::AdvancedScada.Studio.Properties.Resources.AddDataBlock;
            this.btnButtonExportTags.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnButtonExportTags.Name = "btnButtonExportTags";
            this.btnButtonExportTags.Size = new System.Drawing.Size(88, 22);
            this.btnButtonExportTags.Text = "Export Tags";
            this.btnButtonExportTags.Click += new System.EventHandler(this.btnButtonExportTags_Click);
            // 
            // XTagManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 639);
            this.ControlBox = false;
            this.Controls.Add(this.kryptonSplitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockActive;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "XTagManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TagManager";
            this.Load += new System.EventHandler(this.XTagManager_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton barButtonNew;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton barButtonOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton barButtonSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView treeViewSI;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu popupMenuLeft;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems4;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemAddDevice;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemAddDataBlock;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemAddChannel;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView DGMonitorForm;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemImport;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemExport;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading4;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems3;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemCopy;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemPaste;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading3;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu popupMenuRight;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading5;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems5;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemAddTag;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemEditor;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem mDeleteTag;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading6;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems6;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ItemCopyToTag;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem RItemCopy;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem RItemPaste;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel lblTagCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel lblSelectedTagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private ComponentFactory.Krypton.Docking.KryptonDockableWorkspace kryptonDockableWorkspace1;
        private ComponentFactory.Krypton.Docking.KryptonDockingManager kryptonDockingManager1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnButtonExportTags;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}