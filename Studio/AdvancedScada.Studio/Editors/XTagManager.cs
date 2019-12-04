using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Management.BLManager;
using AdvancedScada.Studio.IE;
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;
using HslScada.Studio.Tools;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Studio.Editors
{
    public partial class XTagManager : KryptonForm
    {
        public bool IsDataChanged;
        private ChannelService objChannelManager;
        private DataBlockService objDataBlockManager;
        private DeviceService objDeviceManager;
        private TagService objTagManager;
        private string _lblValueInfo = string.Empty;


        private Device result;
        private Channel fCh;
        private Channel fChNew;
        private Device dvNewCopy;
        private DataBlock dbNewCopy;
        private Tag tgNewCopy;
        public string SelecteCopy_Paste;
        public XTagManager()
        {
            InitializeComponent();
        }
        #region ber


        private void InitializeData(string xmlPath)
        {
            objChannelManager.Channels.Clear();
            objChannelManager.XmlPath = xmlPath;
            List<Channel> chList = objChannelManager.GetChannels(xmlPath);
            treeViewSI.Nodes.Clear();
            foreach (Channel ch in chList)
            {
                List<TreeNode> dvList = new List<TreeNode>();
                ////Sort.
                ch.Devices.Sort(delegate (Device x, Device y)
                {
                    return x.DeviceName.CompareTo(y.DeviceName);
                });

                foreach (Device dv in ch.Devices)
                {
                    List<TreeNode> tgList = new List<TreeNode>();
                    foreach (DataBlock db in dv.DataBlocks)
                    {
                        TreeNode dbNode = new TreeNode(db.DataBlockName);
                        dbNode.StateImageIndex = 2;
                        tgList.Add(dbNode);
                    }

                    TreeNode dvNode = new TreeNode(dv.DeviceName, tgList.ToArray());
                    dvNode.StateImageIndex = 1;
                    dvList.Add(dvNode);
                }
                TreeNode chNode = new TreeNode(ch.ChannelName, dvList.ToArray());
                chNode.StateImageIndex = 0;
                treeViewSI.Nodes.Add(chNode);
            }
        }
        public void GettreeListChannel()
        {
            treeViewSI.Nodes.Clear();
            foreach (Channel ch in objChannelManager.Channels)
            {
                List<TreeNode> dvList = new List<TreeNode>();
                ////Sort.
                ch.Devices.Sort(delegate (Device x, Device y)
                {
                    return x.DeviceName.CompareTo(y.DeviceName);
                });

                foreach (Device dv in ch.Devices)
                {
                    List<TreeNode> tgList = new List<TreeNode>();
                    foreach (DataBlock db in dv.DataBlocks)
                    {
                        tgList.Add(new TreeNode(db.DataBlockName));
                    }
                    //TreeNode dvNode = new TreeNode(dv.DeviceName, tgList.ToArray());
                    TreeNode dvNode = new TreeNode(dv.DeviceName, tgList.ToArray());
                    dvList.Add(dvNode);
                }
                TreeNode chNode = new TreeNode(ch.ChannelName, dvList.ToArray());
                treeViewSI.Nodes.Add(chNode);

            }
        }
        private void XTagManager_Load(object sender, EventArgs e)
        {
            KryptonDockingWorkspace w = kryptonDockingManager1.ManageWorkspace("Workspace", kryptonDockableWorkspace1);
            kryptonDockingManager1.ManageControl("Control", kryptonSplitContainer1.Panel2, w);
            kryptonDockingManager1.ManageFloating("Floating", this);


            kryptonDockingManager1.AddAutoHiddenGroup("Control", DockingEdge.Right, new KryptonPage[] { NewUserPropertyGrid() });

            objChannelManager = ChannelService.GetChannelManager();
            objDeviceManager = DeviceService.GetDeviceManager();
            objDataBlockManager = DataBlockService.GetDataBlockManager();
            objTagManager = TagService.GetTagManager();
            var xmlFile = objChannelManager.ReadKey(objChannelManager.XML_NAME_DEFAULT);
            if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile)) return;
            InitializeData(xmlFile);
        }

        private void barButtonNew_Click(object sender, EventArgs e)
        {
            try
            {
                ItemAddChannel.Enabled = true;
                ItemAddDevice.Enabled = true;
                ItemAddDataBlock.Enabled = true;
                var saveFileDialog = new SaveFileDialog { Filter = "Xml Files (*.xml)|*.xml|All files (*.*)|*.*", FileName = objChannelManager.XML_NAME_DEFAULT };
                var dr = saveFileDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var xmlPath = saveFileDialog.FileName;
                    objChannelManager.CreatFile(xmlPath);
                    treeViewSI.Nodes.Clear();

                    objChannelManager.Channels.Clear();
                    IsDataChanged = true;
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void barButtonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                ItemAddChannel.Enabled = true;
                ItemAddDevice.Enabled = true;
                ItemAddDataBlock.Enabled = true;
                var openFileDialog = new OpenFileDialog { Filter = "Xml Files (*.xml)|*.xml|All files (*.*)|*.*", FileName = "config" };
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    //   InitializeData(openFileDialog.FileName);
                    objChannelManager.Channels.Clear();
                    objChannelManager.XmlPath = openFileDialog.FileName;
                    InitializeData(openFileDialog.FileName);
                    IsDataChanged = true;
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void barButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                objChannelManager.Save(objChannelManager.XmlPath);
                MessageBox.Show(this, "Data saved successfully!", "INFORMATION", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                IsDataChanged = false;
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        #endregion
        private KryptonPage NewUserPropertyGrid()
        {
            return NewPages("UserProperty ", 0, new UserPropertyGrid());
        }
        private KryptonPage NewPages(string name, int image, Control content)
        {
            // Create new page with title and image
            KryptonPage p = new KryptonPage();
            p.Text = name;
            p.TextTitle = name;
            p.TextDescription = name;



            // Add the control for display inside the page
            content.Dock = DockStyle.Fill;
            p.Controls.Add(content);


            return p;
        }
        #region treeList
        private void treeViewSI_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                string selectedNode = treeViewSI.SelectedNode.Text;
                DataBlock dbCurrent = null; //Node: DataBlock
                Device dvCurrent = null; //Node: Device
                Channel chCurrent = null; //Node: Channel
                this.Selection();
                switch (Level)
                {
                    case 0:
                        chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Text);
                        DGMonitorForm.Rows.Clear();
                        switch (chCurrent.ConnectionType)
                        {
                            case "SerialPort":

                                var dis = (DISerialPort)chCurrent;
                                EventPvGridChannelGet?.Invoke(dis, true);

                                break;
                            case "Ethernet":

                                var die = (DIEthernet)chCurrent;
                                EventPvGridChannelGet?.Invoke(die, true);

                                break;
                        }
                        break;
                    case 1:
                        chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Text);
                        dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, selectedNode);
                        DGMonitorForm.Rows.Clear();
                        EventPvGridChannelGet?.Invoke(dvCurrent, true);
                        break;
                    case 2:
                        chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                        dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                        dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, treeViewSI.SelectedNode.Text);
                        DGMonitorForm.Rows.Clear();
                        if (dbCurrent != null)
                        {
                            EventPvGridChannelGet?.Invoke(dbCurrent, true);
                            if (dbCurrent.Tags != null)
                            {
                                lblTagCount.Text = $"Total: {dbCurrent.Tags.Count} tags";
                            }

                        }
                        foreach (Tag tg in dbCurrent.Tags)
                        {
                            string[] row = { string.Format("{0}", tg.TagId), tg.TagName, string.Format("{0}", tg.Address), string.Format("{0}", tg.DataType), tg.Description };


                            DGMonitorForm.Rows.Add(row);
                        }
                        break;
                    default:
                        DGMonitorForm.Rows.Clear();
                        break;
                }
                //if (chCurrent == null)
                //{
                //    EventPvGridChannelGet?.Invoke(chCurrent, false);

                //}
                //else
                //{
                //    switch (chCurrent.ConnectionType)
                //    {
                //        case "SerialPort":

                //            var dis = (DISerialPort)chCurrent;
                //            EventPvGridChannelGet?.Invoke(dis, true);

                //            break;
                //        case "Ethernet":

                //            var die = (DIEthernet)chCurrent;
                //            EventPvGridChannelGet?.Invoke(die, true);

                //            break;
                //    }


                //}
                //if (dvCurrent != null)
                //{
                //    EventPvGridChannelGet?.Invoke(dvCurrent, true);

                //}
                //else
                //{
                //    EventPvGridChannelGet?.Invoke(dvCurrent, false);
                //}
                //if (dbCurrent != null)
                //{
                //    EventPvGridChannelGet?.Invoke(dbCurrent, true);
                //    if (dbCurrent.Tags != null)
                //    {
                //        lblTagCount.Text = $"Total: {dbCurrent.Tags.Count} tags";
                //    }

                //}
                //else
                //{
                //    EventPvGridChannelGet?.Invoke(dbCurrent, false);


                //}

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }


        private void treeViewSI_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    Selection();

                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void treeViewSI_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    Selection();

                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void Selection()
        {
            if (treeViewSI.SelectedNode == null) return;
            int Level = treeViewSI.SelectedNode.Level;
            switch (Level)
            {
                case 0:
                    ItemAddChannel.Enabled = true;
                    ItemAddDevice.Enabled = true;
                    ItemAddDataBlock.Enabled = false;
                    ItemAddTag.Enabled = false;
                    break;
                case 1:
                    ItemAddChannel.Enabled = false;
                    ItemAddDevice.Enabled = true;
                    ItemAddDataBlock.Enabled = true;
                    ItemAddTag.Enabled = false;
                    break;
                case 2:
                    ItemAddChannel.Enabled = false;
                    ItemAddDevice.Enabled = false;
                    ItemAddDataBlock.Enabled = true;
                    ItemAddTag.Enabled = true;
                    break;
                default:

                    break;

            }
        }
        #endregion
        #region Add
        private void ItemAddDevice_Click(object sender, EventArgs e)
        {
            try
            {
                Channel chCurrent = null;

                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                switch (Level)
                {
                    case 0:
                        chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Text);
                        break;
                    case 1:
                        chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Text);
                        break;
                }
                var dvFrm = new GetDeviceForm().XDeviceFormLoad(chCurrent);
                dvFrm.eventDeviceChanged += new EventDeviceChanged((dv, isNew) =>
                {
                    try
                    {
                        if (isNew) objDeviceManager.Add(chCurrent, dv);
                        else objDeviceManager.Update(chCurrent, dv);
                        TreeNode dvNode = new TreeNode(dv.DeviceName);
                        dvNode.StateImageIndex = 1;
                        switch (Level)
                        {
                            case 0:
                                treeViewSI.SelectedNode.Nodes.Add(dvNode);
                                IsDataChanged = true;
                                break;
                            case 1:
                                treeViewSI.SelectedNode.Parent.Nodes.Add(dvNode);
                                IsDataChanged = true;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {

                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                    }
                });
                dvFrm.ShowDialog();
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void ItemAddDataBlock_Click(object sender, EventArgs e)
        {
            try
            {
                Channel chCurrent = null;
                Device dvCurrent = null;

                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                switch (Level)
                {
                    case 1:
                        chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Text);
                        dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Text);
                        break;
                    case 2:
                        chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                        dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                        break;
                }
                var dbFrm = new GetDataBlockForm().XDataBlockFormLoad(chCurrent, dvCurrent);
                dbFrm.eventDataBlockChanged += new EventDataBlockChanged((db, isNew) =>
                {
                    try
                    {
                        if (isNew) objDataBlockManager.Add(dvCurrent, db);
                        else objDataBlockManager.Update(dvCurrent, db);
                        TreeNode dbNode = new TreeNode(db.DataBlockName);
                        dbNode.StateImageIndex = 2;
                        switch (Level)
                        {
                            case 1:
                                treeViewSI.SelectedNode.Nodes.Add(dbNode);
                                IsDataChanged = true;
                                break;
                            case 2:
                                treeViewSI.SelectedNode.Parent.Nodes.Add(dbNode);
                                IsDataChanged = true;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {

                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                    }
                });
                dbFrm.ShowDialog();
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void ItemAddTag_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewSI.SelectedNode == null) return;
                Channel chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                DataBlock dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, treeViewSI.SelectedNode.Text);
                var tgFrm = new GetTagForm().XTagFormLoad(chCurrent, dvCurrent, dbCurrent);
                tgFrm.eventTagChanged += new EventTagChanged((tg, isNew) =>
                {
                    try
                    {
                        if (isNew)
                        {
                            objTagManager.Add(dbCurrent, tg);
                            string[] row = { string.Format("{0}", tg.TagId), tg.TagName, string.Format("{0}", tg.Address), string.Format("{0}", tg.DataType), tg.Description };

                            DGMonitorForm.Rows.Add(row);
                        }

                        else objTagManager.Update(dbCurrent, tg);

                    }
                    catch (Exception ex)
                    {

                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                    }
                });
                tgFrm.ShowDialog();
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void ItemAddChannel_Click(object sender, EventArgs e)
        {
            try
            {
                var DriverFrm = new XSelectedDrivers();

                if (DriverFrm.ShowDialog() == DialogResult.OK)
                {
                    var chFrm = new GetChannelForm().XChannelFormLoad(DriverFrm.cboxSelectedDrivers.Text, objChannelManager, null);
                    chFrm.eventChannelChanged += new EventChannelChanged((ch, isNew) =>
                    {
                        try
                        {
                            if (isNew) objChannelManager.Add(ch);
                            else objChannelManager.Update(ch);
                            TreeNode chNode = new TreeNode(ch.ChannelName);
                            chNode.StateImageIndex = 0;
                            treeViewSI.Nodes.Add(chNode);
                            IsDataChanged = true;
                        }
                        catch (Exception ex)
                        {

                            EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                        }
                    });
                    chFrm.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void treeViewSI_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
                int Level = treeViewSI.SelectedNode.Level;
                string selectedNode = treeViewSI.SelectedNode.Text;
                Channel chCurrent = null;
                Device dvCurrent = null;
                DataBlock dbCurrent = null;
                switch (Level)
                {
                    case 0:
                        chCurrent = objChannelManager.GetByChannelName(selectedNode);
                        var chFrm = new GetChannelForm().XChannelFormLoad(chCurrent.ChannelTypes, objChannelManager, chCurrent);
                        chFrm.eventChannelChanged += new EventChannelChanged((ch, isNew) =>
                        {
                            if (isNew) objChannelManager.Add(ch);
                            else objChannelManager.Update(ch);
                            treeViewSI.SelectedNode.Text = ch.ChannelName;
                        });
                        chFrm.StartPosition = FormStartPosition.CenterScreen;
                        chFrm.ShowDialog();
                        break;
                    case 1:
                        chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Text);
                        dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, selectedNode);
                        var dvFrm = new GetDeviceForm().XDeviceFormLoad(chCurrent, dvCurrent);
                        dvFrm.eventDeviceChanged += new EventDeviceChanged((dv, isNew) =>
                        {
                            if (isNew) objDeviceManager.Add(chCurrent, dv);
                            else objDeviceManager.Update(chCurrent, dv);
                            treeViewSI.SelectedNode.Text = dv.DeviceName;
                        });
                        dvFrm.StartPosition = FormStartPosition.CenterScreen;
                        dvFrm.ShowDialog();
                        break;
                    case 2:
                        chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                        dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                        dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, selectedNode);
                        var dbFrm = new GetDataBlockForm().XDataBlockFormLoad(chCurrent, dvCurrent, dbCurrent);
                        dbFrm.eventDataBlockChanged += new EventDataBlockChanged((db, isNew) =>
                        {
                            if (isNew) objDataBlockManager.Add(dvCurrent, db);
                            else objDataBlockManager.Update(dvCurrent, db);
                            treeViewSI.SelectedNode.Text = db.DataBlockName;
                            DGMonitorForm.Rows.Clear();
                            foreach (Tag tg in db.Tags)
                            {
                                string[] row = { string.Format("{0}", tg.TagId), tg.TagName, string.Format("{0}", tg.Address), string.Format("{0}", tg.DataType), tg.Description };

                                DGMonitorForm.Rows.Add(row);
                            }
                        });
                        dbFrm.StartPosition = FormStartPosition.CenterScreen;
                        dbFrm.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void ItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                string selectedNode = treeViewSI.SelectedNode.Text;
                DialogResult result;
                switch (Level)
                {
                    case 0:
                        result = MessageBox.Show(this, string.Format("Are you sure delete channel: {0}?", selectedNode), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            Channel fCh = objChannelManager.GetByChannelName(selectedNode);
                            objChannelManager.Delete(fCh);
                            treeViewSI.SelectedNode.Remove();
                            IsDataChanged = true;
                        }
                        break;
                    case 1:
                        result = MessageBox.Show(this, string.Format("Are you sure delete device: {0} of the channel: {1}?", selectedNode, treeViewSI.SelectedNode.Parent.Text), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            Channel fCh = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Text);
                            objDeviceManager.Delete(fCh, selectedNode);
                            treeViewSI.SelectedNode.Remove();
                            IsDataChanged = true;
                        }
                        break;
                    case 2:
                        result = MessageBox.Show(this, string.Format("Are you sure delete datablock: {0} of the device: {1}, channel: {2}?", selectedNode, treeViewSI.SelectedNode.Parent.Text, treeViewSI.SelectedNode.Parent.Parent.Text), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            Channel chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                            Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                            DataBlock dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, treeViewSI.SelectedNode.Text);
                            objDataBlockManager.Delete(dvCurrent, dbCurrent);
                            treeViewSI.SelectedNode.Remove();
                            IsDataChanged = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void ItemImport_Click(object sender, EventArgs e)
        {
            try
            {
                int Level = treeViewSI.SelectedNode.Level;
                string selectedNode = treeViewSI.SelectedNode.Text;

                switch (Level)
                {
                    case 0:

                        break;
                    case 1:

                        break;
                    case 2:
                        Channel chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                        Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                        DataBlock dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, treeViewSI.SelectedNode.Text);


                        var frm = new FormImport(chCurrent, dvCurrent, dbCurrent);
                        foreach (Form form in Application.OpenForms)
                            if (form.GetType() == typeof(FormImport))
                            {
                                form.Activate();
                                return;
                            }
                        frm.eventDataBlockChanged += db =>
                        {
                            try
                            {
                                IsDataChanged = true;
                                foreach (Tag tg in db.Tags)
                                {
                                    string[] row = { string.Format("{0}", tg.TagId), tg.TagName, string.Format("{0}", tg.Address), string.Format("{0}", tg.DataType), tg.Description };

                                    DGMonitorForm.Rows.Add(row);
                                }
                            }
                            catch (Exception ex)
                            {

                                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                            }
                        };

                        frm.ShowDialog();


                        break;
                    default:

                        break;
                }

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void ItemExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                string selectedNode = treeViewSI.SelectedNode.Text;
                // Creating a Excel object.
                using (var p = new ExcelPackage())
                {
                    // List Channels.
                    var chCurrent = objChannelManager.GetByChannelName(selectedNode);
                    p.Workbook.Properties.Author = chCurrent.ChannelName;
                    p.Workbook.Properties.Title = chCurrent.ConnectionType;
                    p.Workbook.Properties.Company = chCurrent.ConnectionType;
                    var cellRowIndex = 1;
                    var cellColumnIndex = 1;
                    // List Devices.
                    foreach (var dv in chCurrent.Devices)
                    {
                        if (dv.DataBlocks.Count == 0) continue;
                        // List DataBlock.
                        var Worksheets = 1;
                        foreach (var db in dv.DataBlocks)
                        {
                            cellRowIndex = 1;
                            cellColumnIndex = 1;
                            // The rest of our code will go here...
                            p.Workbook.Worksheets.Add(db.DataBlockName);
                            // 1 is the position of the worksheet
                            var ws = p.Workbook.Worksheets[Worksheets];
                            ws.Name = db.DataBlockName + chCurrent.ChannelId;
                            for (var j = 0; j < DGMonitorForm.Columns.Count; j++)
                            {
                                // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check.
                                if (cellRowIndex == 1)
                                {
                                    var cell_actionName = ws.Cells[cellRowIndex, cellColumnIndex];
                                    cell_actionName.Value = DGMonitorForm.Columns[j].HeaderText;
                                }

                                cellColumnIndex++;
                            }

                            cellRowIndex++;
                            // List Tags.
                            foreach (var tg in db.Tags)
                            {
                                cellColumnIndex = 1;
                                var TagId = ws.Cells[cellRowIndex, cellColumnIndex];
                                TagId.Value = tg.TagId;
                                cellColumnIndex++;
                                var TagName = ws.Cells[cellRowIndex, cellColumnIndex];
                                TagName.Value = tg.TagName;
                                cellColumnIndex++;
                                var Address = ws.Cells[cellRowIndex, cellColumnIndex];
                                Address.Value = tg.Address;
                                cellColumnIndex++;
                                var DataType = ws.Cells[cellRowIndex, cellColumnIndex];
                                DataType.Value = tg.DataType;
                                cellColumnIndex++;
                                var Desp = ws.Cells[cellRowIndex, cellColumnIndex];
                                Desp.Value = tg.Description;
                                cellRowIndex++;
                            }

                            Worksheets++;
                        }
                    }

                    //Getting the location and file name of the excel to save from user.
                    var saveDialog = new SaveFileDialog { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*", FilterIndex = 1, FileName = "DataBlockName" };
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var bin = p.GetAsByteArray();
                        File.WriteAllBytes(saveDialog.FileName, bin);
                        MessageBox.Show("Export Successful");
                    }
                }

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void ItemCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                string selectedNode = treeViewSI.SelectedNode.Text;
                fChNew = null;
                dvNewCopy = null;
                dvNewCopy = null;
                switch (Level)
                {
                    case 0:

                        fChNew = new Channel();
                        fCh = objChannelManager.GetByChannelName(selectedNode);
                        fChNew = objChannelManager.Copy(fCh);
                        SelecteCopy_Paste = "Channel";
                        break;
                    case 1:
                        dvNewCopy = new Device();
                        fCh = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Text);
                        result = objDeviceManager.GetByDeviceName(fCh, selectedNode);
                        dvNewCopy = objDeviceManager.Copy(result, fCh);
                        SelecteCopy_Paste = "Device";
                        break;
                    case 2:
                        dbNewCopy = new DataBlock();
                        fChNew = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                        dvNewCopy = objDeviceManager.GetByDeviceName(fChNew, treeViewSI.SelectedNode.Parent.Text);
                        var dbCurrent = objDataBlockManager.GetByDataBlockName(dvNewCopy, treeViewSI.SelectedNode.Text);
                        dbNewCopy = objDataBlockManager.Copy(dbCurrent, dvNewCopy);
                        SelecteCopy_Paste = "DataBlock";

                        break;
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void ItemPaste_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                string selectedNode = treeViewSI.SelectedNode.Text;
                switch (SelecteCopy_Paste)
                {
                    case "Channel":
                        switch (fCh.ConnectionType)
                        {
                            case "SerialPort":
                                objChannelManager.Add((DISerialPort)fChNew);
                                break;
                            case "Ethernet":
                                objChannelManager.Add((DIEthernet)fChNew);

                                break;
                        }


                        break;
                    case "Device":
                        fCh = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Text);
                        objDeviceManager.Add(fCh, dvNewCopy);

                        break;
                    case "DataBlock":
                        fCh = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                        dvNewCopy = objDeviceManager.GetByDeviceName(fCh, treeViewSI.SelectedNode.Parent.Text);
                        objDataBlockManager.Add(dvNewCopy, dbNewCopy);


                        break;
                }
                GettreeListChannel(); IsDataChanged = true;
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        #endregion
        #region GridControl
        private void DGMonitorForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int Level = treeViewSI.SelectedNode.Level;

                if (Level == 2)
                {
                    Channel chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                    Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                    DataBlock dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, treeViewSI.SelectedNode.Text);

                    var channelName = chCurrent.ChannelName;
                    var DeviceName = dvCurrent.DeviceName;

                    var DataBlockName = dbCurrent.DataBlockName;



                    if (DGMonitorForm.SelectedRows.Count == 1)
                    {
                        string tgName = (string)DGMonitorForm.SelectedRows[0].Cells[1].Value;

                        var tagName = $"{channelName}.{DeviceName}.{DataBlockName}.{tgName}";

                        var tgCurrent = objTagManager.GetByTagName(dbCurrent, tgName);

                        var tgFrm = new GetTagForm().XTagFormLoad(chCurrent, dvCurrent, dbCurrent, tgCurrent);
                        tgFrm.eventTagChanged += (tg, isNew) =>
                        {
                            objTagManager.Update(dbCurrent, tgCurrent);


                            IsDataChanged = true;
                        };
                        tgFrm.StartPosition = FormStartPosition.CenterScreen;
                        tgFrm.ShowDialog();

                    }



                }


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void DGMonitorForm_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {

                    popupMenuRight.Show(MousePosition);
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        #endregion
        #region popupMenuRight
        private void ItemEditor_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                string selectedNode = treeViewSI.SelectedNode.Text;

                if (Level == 2)
                {
                    Channel chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                    Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                    DataBlock dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, treeViewSI.SelectedNode.Text);

                    var channelName = chCurrent.ChannelName;
                    var DeviceName = dvCurrent.DeviceName;

                    var DataBlockName = dbCurrent.DataBlockName;



                    if (DGMonitorForm.SelectedRows.Count == 1)
                    {
                        string tgName = (string)DGMonitorForm.SelectedRows[0].Cells[1].Value;



                        var tgCurrent = objTagManager.GetByTagName(dbCurrent, tgName);

                        var tgFrm = new GetTagForm().XTagFormLoad(chCurrent, dvCurrent, dbCurrent, tgCurrent);
                        tgFrm.eventTagChanged += (tg, isNew) =>
                        {
                            objTagManager.Update(dbCurrent, tgCurrent);


                            IsDataChanged = true;
                        };
                        tgFrm.StartPosition = FormStartPosition.CenterScreen;
                        tgFrm.ShowDialog();

                    }



                }


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void ItemCopyToTag_Click(object sender, EventArgs e)
        {
            if (_lblValueInfo == string.Empty) return;
            Clipboard.SetText(_lblValueInfo);
        }

        private void mDeleteTag_Click(object sender, EventArgs e)
        {
            try
            {
                int Level = treeViewSI.SelectedNode.Level;

                if (Level == 2)
                {
                    Channel chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                    Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                    DataBlock dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, treeViewSI.SelectedNode.Text);

                    var channelName = chCurrent.ChannelName;
                    var DeviceName = dvCurrent.DeviceName;

                    var DataBlockName = dbCurrent.DataBlockName;



                    if (DGMonitorForm.SelectedRows.Count == 1)
                    {
                        string tgName = (string)DGMonitorForm.SelectedRows[0].Cells[1].Value;

                        objTagManager.Delete(dbCurrent, tgName);

                    }


                }


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void RItemCopy_Click(object sender, EventArgs e)
        {
            try
            {

                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                string selectedNode = treeViewSI.SelectedNode.Text;
                if (Level == 2)
                {
                    Channel chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                    Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                    DataBlock dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, treeViewSI.SelectedNode.Text);

                    var channelName = chCurrent.ChannelName;
                    var DeviceName = dvCurrent.DeviceName;

                    var DataBlockName = dbCurrent.DataBlockName;

                    if (DGMonitorForm.SelectedRows.Count == 1)
                    {
                        string tgName = (string)DGMonitorForm.SelectedRows[0].Cells[1].Value;

                        _lblValueInfo = tgName;
                        var tgCurrent = objTagManager.GetByTagName(dbCurrent, tgName);
                        tgNewCopy = new Tag();


                        tgNewCopy.TagName = tgCurrent.TagName + "New";
                        tgNewCopy.TagId = dbCurrent.Tags.Count + 1;
                        tgNewCopy.Address = tgCurrent.Address;
                        tgNewCopy.ChannelId = tgCurrent.ChannelId;
                        tgNewCopy.DeviceId = tgCurrent.DeviceId;
                        tgNewCopy.DataBlockId = tgCurrent.DataBlockId;
                        tgNewCopy.DataType = tgCurrent.DataType;
                        tgNewCopy.Description = tgCurrent.Description;
                    }
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void RItemPaste_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                string selectedNode = treeViewSI.SelectedNode.Text;
                Selection();
                switch (Level)
                {
                    case 0:

                        break;
                    case 1:
                        break;
                    case 2:
                        Channel chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                        Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                        DataBlock dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, treeViewSI.SelectedNode.Text);


                        dbCurrent.Tags.Add(tgNewCopy);



                        break;
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        #endregion

        private void treeViewSI_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnButtonExportTags_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "Text Files (*.Text)|*.Text|All files (*.*)|*.*";

                DialogResult dr = saveFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in TagCollection.Tags)
                    {
                        var tgName = '"';

                        sb.AppendLine($"public const string {(item.Key.Replace('.', '_'))} = {tgName} {item.Key.ToString()} {tgName}; ");
                    }

                    File.WriteAllText(saveFileDialog1.FileName, sb.ToString());

                    IsDataChanged = false;
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
    }
}
