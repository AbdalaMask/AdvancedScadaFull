using AdvancedScada.Management.SQLManager;
using ComponentFactory.Krypton.Toolkit;
using System;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.Studio.LinkToSQL
{
    public delegate void EventTableChanged(Table db);
    public partial class XAddTable : KryptonForm
    {
        private readonly Server ch;
        private readonly Table db;
        private readonly DataBase dv;
        public EventTableChanged eventTableChanged = null;
        public XAddTable()
        {
            InitializeComponent();
        }
        public XAddTable(Server chParam, DataBase dvParam, Table dbParam = null)
        {
            InitializeComponent();

            ch = chParam;
            dv = dvParam;
            db = dbParam;

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDataBaseName.Text)
                    || string.IsNullOrWhiteSpace(txtDataBaseName.Text))
                {
                    DxErrorProvider1.SetError(txtDataBaseName, "The Table name is empty");
                }
                else
                {
                    DxErrorProvider1.Clear();
                    if (db == null)
                    {
                        var TableNew = new Table();
                        TableNew.TableId = dv.Tables.Count + 1;

                        TableNew.TableName = txtTableName.Text;
                        TableNew.Description = txtDescription.Text;
                        TableManager.Add(dv, TableNew);
                        if (eventTableChanged != null) eventTableChanged(TableNew);
                        Close();
                    }
                    else
                    {
                        db.TableId = short.Parse(txtTableId.Text);
                        db.TableName = txtTableName.Text;
                        db.Description = txtDescription.Text;

                        TableManager.Update(dv, db);
                        if (eventTableChanged != null) eventTableChanged(db);

                        Close();
                    }
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddTable_Load(object sender, EventArgs e)
        {
            var linkToSql = new AdvancedScada.Utils.DriverLinkToSQL.LinkToSQL();
            try
            {
                if (db == null)
                {
                    Text = "Add Table";
                    txtServerId.Text = Convert.ToString(ch.ServerId);
                    txtServerName.Text = ch.ServerName;
                    txtDataBaseId.Text = Convert.ToString(dv.DataBaseId);
                    txtDataBaseName.Text = dv.DataBaseName;
                    txtTableId.Text = Convert.ToString(dv.Tables.Count + 1);

                    txtTableName.Items.AddRange(linkToSql.AddTable(ch.ServerName, dv.DataBaseName).ToArray());
                }
                else
                {
                    Text = "Edit Table";
                    txtServerId.Text = Convert.ToString(ch.ServerId);
                    txtServerName.Text = ch.ServerName;
                    txtDataBaseId.Text = Convert.ToString(dv.DataBaseId);
                    txtDataBaseName.Text = dv.DataBaseName;
                    txtTableId.Text = Convert.ToString(db.TableId);
                    txtTableName.Items.AddRange(linkToSql.AddTable(ch.ServerName, dv.DataBaseName).ToArray());
                    txtTableName.Text = db.TableName;
                    txtDescription.Text = db.Description;

                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

        }

    }
}

