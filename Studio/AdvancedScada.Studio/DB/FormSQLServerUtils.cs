using AdvancedScada.Studio.Properties;
using AdvancedScada.Utils.Databases;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Studio.DB
{
    public partial class FormSQLServerUtils : KryptonForm
    {
        public DataTable dt = new DataTable();
        public string FileName = string.Empty;
        public FormSQLServerUtils()
        {
            InitializeComponent();
        }

        private void FormSQLServerUtils_Load(object sender, EventArgs e)
        {

        }

        private void DBListLookUpEditButton_Click(object sender, EventArgs e)
        {
            if (Settings.Default.DatabaseTypes == "SQLite")
            {
                // Show the dialog and get result.
                openFileDialog1.Filter = "db3 Files (*.db3)|*.db3|All files (*.*)|*.*";
                openFileDialog1.FileName = "config";
                var result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    FileName = openFileDialog1.FileName;
                    dt = UtilsTableSQLite.AddDatabases(openFileDialog1.FileName);
                    DBListLookUpEdit.DataSource = dt;
                    DBListLookUpEdit.DisplayMember = "DatabaseName";
                    DBListLookUpEdit.ValueMember = "DBId";
                }
            }
            else if (Settings.Default.DatabaseTypes == "SQL")
            {
                dt = UtilsTable.AddDatabases(Settings.Default.teServer);
                DBListLookUpEdit.DataSource = dt;
                DBListLookUpEdit.DisplayMember = "DatabaseName";
                DBListLookUpEdit.ValueMember = "DBId";
            }
        }

        private void ColumnLookUpEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DBListLookUpEdit.Text == "System.Data.DataRowView") return;
                var ds = new DataTable();
                if (Settings.Default.DatabaseTypes == "SQLite")
                {
                    //ds = UtilsTableSQLite.AddTableListBox(FileName);
                    List<string> tables = UtilsTableSQLite.GetListTables(FileName);
                    TableNamesListBox.Items.Clear();

                    TableNamesListBox.Items.AddRange(tables.ToArray());
                }
                else if (Settings.Default.DatabaseTypes == "SQL")
                {
                    ds = UtilsTable.AddTableListBox(DBListLookUpEdit.Text, Settings.Default.teServer);
                    TableNamesListBox.Items.Clear();
                    for (var i = 0; i <= ds.Rows.Count - 1; i++)
                        TableNamesListBox.Items.Add(ds.Rows[i].ItemArray[0].ToString());

                }



            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void TableNamesListBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Settings.Default.DatabaseTypes == "SQLite")
                {
                    var dt = new DataTable();
                    dt = UtilsTableSQLite.ColumnExists($"{TableNamesListBox.SelectedItem}", FileName);
                    ColumnLookUpEdit.DataSource = dt;
                }
                else if (Settings.Default.DatabaseTypes == "SQL")
                {

                    var dt = new DataTable();
                    dt = UtilsTable.AddColumnGrid(DBListLookUpEdit.Text, $"{TableNamesListBox.SelectedItem}", Settings.Default.teServer);

                    ColumnLookUpEdit.DataSource = dt;

                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void TableNamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableNamesListBox_MouseClick(sender, null);
        }

        private void ColumnLookUpEdit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
