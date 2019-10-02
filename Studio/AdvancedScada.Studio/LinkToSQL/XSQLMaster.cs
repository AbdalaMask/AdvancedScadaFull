using AdvancedScada.Management.SQLManager;
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;
using HslScada.Studio.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.Studio.LinkToSQL
{
    public partial class XSQLMaster : KryptonForm
    {
        private BindingList<Column> bColumns;
        public bool IsDataChanged;

        private string _lblValueInfo = string.Empty;
        private ServerManager objServerManager;

        public string SelecteCopy_Paste;
        public XSQLMaster()
        {
            InitializeComponent();
        }
        #region ber


        private void InitializeData(string xmlPath)
        {
            objServerManager.SQLServers.Clear();
            objServerManager.XmlPath = xmlPath;
            var chList = objServerManager.GetServers(xmlPath);
            treeViewSI.Nodes.Clear();
            foreach (var ch in chList)
            {
                List<TreeNode> dvList = new List<TreeNode>();
                ////Sort.
                ch.DataBase.Sort(delegate (DataBase x, DataBase y)
                {
                    return x.DataBaseName.CompareTo(y.DataBaseName);
                });

                foreach (var dv in ch.DataBase)
                {
                    List<TreeNode> tgList = new List<TreeNode>();
                    foreach (var db in dv.Tables)
                    {
                        TreeNode dbNode = new TreeNode(db.TableName);
                        dbNode.StateImageIndex = 2;
                        tgList.Add(dbNode);
                    }

                    TreeNode dvNode = new TreeNode(dv.DataBaseName, tgList.ToArray());
                    dvNode.StateImageIndex = 1;
                    dvList.Add(dvNode);
                }
                TreeNode chNode = new TreeNode(ch.ServerName, dvList.ToArray());
                chNode.StateImageIndex = 0;
                treeViewSI.Nodes.Add(chNode);
            }
        }

        private void XTagManager_Load(object sender, EventArgs e)
        {
            KryptonDockingWorkspace w = kryptonDockingManager1.ManageWorkspace("Workspace", kryptonDockableWorkspace1);
            kryptonDockingManager1.ManageControl("Control", kryptonSplitContainer1.Panel2, w);
            kryptonDockingManager1.ManageFloating("Floating", this);


            kryptonDockingManager1.AddAutoHiddenGroup("Control", DockingEdge.Right, new KryptonPage[] { NewUserPropertyGrid() });

            objServerManager = ServerManager.GetServerManager();
            var xmlFile = objServerManager.ReadKey(ServerManager.XML_NAME_DEFAULT);
            if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile)) return;
            InitializeData(xmlFile);
        }

        private void barButtonNew_Click(object sender, EventArgs e)
        {
            try
            {
                ItemSQLServer.Enabled = true;
                ItemDataBase.Enabled = true;
                ItemTable.Enabled = true;
                var saveFileDialog = new SaveFileDialog { Filter = "Xml Files (*.xml)|*.xml|All files (*.*)|*.*", FileName = "XML_NAME_DEFAULT" };
                var dr = saveFileDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var xmlPath = saveFileDialog.FileName;
                    objServerManager.CreatFile(xmlPath);
                    treeViewSI.Nodes.Clear();

                    objServerManager.SQLServers.Clear();
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
                ItemSQLServer.Enabled = true;
                ItemDataBase.Enabled = true;
                ItemTable.Enabled = true;
                var openFileDialog = new OpenFileDialog { Filter = "Xml Files (*.xml)|*.xml|All files (*.*)|*.*", FileName = "config" };
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    //   InitializeData(openFileDialog.FileName);
                    objServerManager.SQLServers.Clear();
                    objServerManager.XmlPath = openFileDialog.FileName;
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
                objServerManager.Save(objServerManager.XmlPath);
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
                Table dbCurrent = null; //Node: Table
                DataBase dvCurrent = null; //Node: DataBase
                Server chCurrent = null; //N
                this.Selection();
                switch (Level)
                {
                    case 0:
                        chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Text);
                        DGMonitorForm.Rows.Clear();
                        break;
                    case 1:
                        chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Text);
                        dvCurrent = DataBaseManager.GetByDataBaseName(chCurrent, selectedNode);
                        DGMonitorForm.Rows.Clear();

                        break;
                    case 2:
                        chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Parent.Text);
                        dvCurrent = DataBaseManager.GetByDataBaseName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                        dbCurrent = TableManager.GetByTableName(dvCurrent, treeViewSI.SelectedNode.Text);
                        DGMonitorForm.Rows.Clear();

                        foreach (var item in dbCurrent.Columns)
                        {
                            string[] row = { string.Format("{0}", item.ColumnId), item.ColumnName, item.TagName, item.DataBlock, item.Device, item.Channel, item.Cycle, item.Description };

                            DGMonitorForm.Rows.Add(row);
                        }
                        break;
                    default:
                        DGMonitorForm.Rows.Clear();
                        break;
                }
                if (chCurrent == null)
                {
                    EventPvGridChannelGet?.Invoke(chCurrent, false);

                }
                else
                {
                    EventPvGridChannelGet?.Invoke(chCurrent, true);
                }
                if (dvCurrent != null)
                {
                    EventPvGridDeviceGet?.Invoke(dvCurrent, true);

                }
                else
                {
                    EventPvGridDeviceGet?.Invoke(dvCurrent, false);
                }
                if (dbCurrent != null)
                {
                    EventPvGridDataBlockGet?.Invoke(dbCurrent, true);
                    if (dbCurrent.Columns != null)
                    {
                        lblTagCount.Text = $"Total: {dbCurrent.Columns.Count} tags";
                    }

                }
                else
                {
                    EventPvGridDataBlockGet?.Invoke(dbCurrent, false);


                }

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
                    ItemSQLServer.Enabled = true;
                    ItemDataBase.Enabled = true;
                    ItemTable.Enabled = false;
                    ItemAddTag.Enabled = false;
                    break;
                case 1:
                    ItemSQLServer.Enabled = false;
                    ItemDataBase.Enabled = true;
                    ItemTable.Enabled = true;
                    ItemAddTag.Enabled = false;
                    break;
                case 2:
                    ItemSQLServer.Enabled = false;
                    ItemDataBase.Enabled = false;
                    ItemTable.Enabled = true;
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
                Server chCurrent = null;
                XAddDataBase dvFrm = null;
                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                switch (Level)
                {
                    case 0:
                        chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Text);
                        break;
                    case 1:
                        chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Text);
                        break;
                }
                dvFrm = new XAddDataBase(chCurrent);
                dvFrm.eventDataBaseChanged += dv =>
                {
                    try
                    {

                        TreeNode dvNode = new TreeNode(dv.DataBaseName);
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
                };
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
                Server chCurrent = null;
                DataBase dvCurrent = null;
                XAddTable dbFrm = null;
                if (treeViewSI.SelectedNode == null) return;
                int Level = treeViewSI.SelectedNode.Level;
                switch (Level)
                {
                    case 1:
                        chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Text);
                        dvCurrent = DataBaseManager.GetByDataBaseName(chCurrent, treeViewSI.SelectedNode.Text);
                        break;
                    case 2:
                        chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Parent.Text);
                        dvCurrent = DataBaseManager.GetByDataBaseName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                        break;
                }
                dbFrm = new XAddTable(chCurrent, dvCurrent);
                dbFrm.eventTableChanged += db =>
                {
                    try
                    {

                        TreeNode dbNode = new TreeNode(db.TableName);
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
                };
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

                Server chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Parent.Text);
                DataBase dvCurrent = DataBaseManager.GetByDataBaseName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                Table dbCurrent = TableManager.GetByTableName(dvCurrent, treeViewSI.SelectedNode.Text);
                var tgFrm = new XAddColumn(chCurrent, dvCurrent, dbCurrent);
                tgFrm.eventColumnChanged += tg =>
                {

                    try
                    {
                        DGMonitorForm.Rows.Clear();
                        foreach (var item in dbCurrent.Columns)
                        {
                            string[] row = { string.Format("{0}", item.ColumnId), item.ColumnName, item.TagName, item.DataBlock, item.Device, item.Channel, item.Cycle, item.Description };

                            DGMonitorForm.Rows.Add(row);
                        }


                    }
                    catch (Exception ex)
                    {

                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                    }
                };
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

                var chFrm = new XAddServer(objServerManager);
                chFrm.eventSQLServerChanged += ch =>
                {
                    try
                    {

                        TreeNode chNode = new TreeNode(ch.ServerName);
                        chNode.StateImageIndex = 0;
                        treeViewSI.Nodes.Add(chNode);
                        IsDataChanged = true;
                    }
                    catch (Exception ex)
                    {

                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                    }

                };
                chFrm.ShowDialog();
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
                Server chCurrent = null;
                DataBase dvCurrent = null;
                Table dbCurrent = null;
                switch (Level)
                {
                    case 0:
                        chCurrent = objServerManager.GetBySQLServerName(selectedNode);
                        var chFrm = new XAddServer(objServerManager, chCurrent);
                        chFrm.eventSQLServerChanged += ch =>
                        {

                            treeViewSI.SelectedNode.Text = ch.ServerName;
                        };
                        chFrm.StartPosition = FormStartPosition.CenterScreen;
                        chFrm.ShowDialog();
                        break;
                    case 1:
                        chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Text);
                        dvCurrent = DataBaseManager.GetByDataBaseName(chCurrent, selectedNode);
                        var dvFrm = new XAddDataBase(chCurrent, dvCurrent);
                        dvFrm.eventDataBaseChanged += dv =>
                        {

                            treeViewSI.SelectedNode.Text = dv.DataBaseName;
                        };
                        dvFrm.StartPosition = FormStartPosition.CenterScreen;
                        dvFrm.ShowDialog();
                        break;
                    case 2:
                        chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Parent.Text);
                        dvCurrent = DataBaseManager.GetByDataBaseName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                        dbCurrent = TableManager.GetByTableName(dvCurrent, selectedNode);
                        XAddTable dbFrm = new XAddTable(chCurrent, dvCurrent, dbCurrent);
                        dbFrm.eventTableChanged += db =>
                        {

                            treeViewSI.SelectedNode.Text = db.TableName;
                            DGMonitorForm.Rows.Clear();

                            foreach (var item in dbCurrent.Columns)
                            {
                                string[] row = { string.Format("{0}", item.ColumnId), item.ColumnName, item.TagName, item.DataBlock, item.Device, item.Channel, item.Cycle, item.Description };

                                DGMonitorForm.Rows.Add(row);
                            }
                        };
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
                            var fCh = objServerManager.GetBySQLServerName(selectedNode);
                            objServerManager.Delete(fCh);
                            treeViewSI.SelectedNode.Remove();
                            IsDataChanged = true;
                        }
                        break;
                    case 1:
                        result = MessageBox.Show(this, string.Format("Are you sure delete device: {0} of the channel: {1}?", selectedNode, treeViewSI.SelectedNode.Parent.Text), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            var fCh = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Text);
                            DataBaseManager.Delete(fCh, selectedNode);
                            treeViewSI.SelectedNode.Remove();
                            IsDataChanged = true;
                        }
                        break;
                    case 2:
                        result = MessageBox.Show(this, string.Format("Are you sure delete datablock: {0} of the device: {1}, channel: {2}?", selectedNode, treeViewSI.SelectedNode.Parent.Text, treeViewSI.SelectedNode.Parent.Parent.Text), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            var chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Parent.Text);
                            var dvCurrent = DataBaseManager.GetByDataBaseName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                            var dbCurrent = TableManager.GetByTableName(dvCurrent, treeViewSI.SelectedNode.Text);
                            TableManager.Delete(dvCurrent, dbCurrent);
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




        #endregion
        #region GridControl
        private void DGMonitorForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int Level = treeViewSI.SelectedNode.Level;

                //if (Level == 2)
                //{
                //    Channel chCurrent = objChannelManager.GetByChannelName(treeViewSI.SelectedNode.Parent.Parent.Text);
                //    Device dvCurrent = objDeviceManager.GetByDeviceName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                //    DataBlock dbCurrent = objDataBlockManager.GetByDataBlockName(dvCurrent, treeViewSI.SelectedNode.Text);

                //    var channelName = chCurrent.ChannelName;
                //    var DeviceName = dvCurrent.DeviceName;

                //    var DataBlockName = dbCurrent.DataBlockName;



                //    if (DGMonitorForm.SelectedRows.Count == 1)
                //    {
                //        string tgName = (string)DGMonitorForm.SelectedRows[0].Cells[1].Value;

                //        var tagName = $"{channelName}.{DeviceName}.{DataBlockName}.{tgName}";

                //        var tgCurrent = objTagManager.GetByTagName(dbCurrent, tgName);

                //        //var tgFrm = new XTagForm(chCurrent, dvCurrent, dbCurrent, tgCurrent);
                //        //tgFrm.eventTagChanged += (tg, isNew) =>
                //        //{
                //        //    objTagManager.Update(dbCurrent, tgCurrent);


                //        //    IsDataChanged = true;
                //        //};
                //        //tgFrm.StartPosition = FormStartPosition.CenterScreen;
                //        //tgFrm.ShowDialog();

                //    }



                //}


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
                    var chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Parent.Text);
                    var dvCurrent = DataBaseManager.GetByDataBaseName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                    var dbCurrent = TableManager.GetByTableName(dvCurrent, treeViewSI.SelectedNode.Text);

                    var channelName = chCurrent.ServerName;
                    var DeviceName = dvCurrent.DataBaseName;

                    var DataBlockName = dbCurrent.TableName;



                    if (DGMonitorForm.SelectedRows.Count == 1)
                    {
                        string tgName = (string)DGMonitorForm.SelectedRows[0].Cells[1].Value;
                        var tgCurrent = ColumnManager.GetByTagName(dbCurrent, tgName);

                        var tgFrm = new XAddColumn(chCurrent, dvCurrent, dbCurrent, tgCurrent);
                        tgFrm.eventColumnChanged += tg =>
                        {

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



        private void mDeleteTag_Click(object sender, EventArgs e)
        {
            try
            {
                int Level = treeViewSI.SelectedNode.Level;

                if (Level == 2)
                {
                    var chCurrent = objServerManager.GetBySQLServerName(treeViewSI.SelectedNode.Parent.Parent.Text);
                    var dvCurrent = DataBaseManager.GetByDataBaseName(chCurrent, treeViewSI.SelectedNode.Parent.Text);
                    var dbCurrent = TableManager.GetByTableName(dvCurrent, treeViewSI.SelectedNode.Text);

                    var channelName = chCurrent.ServerName;
                    var DeviceName = dvCurrent.DataBaseName;

                    var DataBlockName = dbCurrent.TableName;



                    if (DGMonitorForm.SelectedRows.Count == 1)
                    {
                        string tgName = (string)DGMonitorForm.SelectedRows[0].Cells[1].Value;

                        ColumnManager.Delete(dbCurrent, tgName);

                    }


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
    }
}
