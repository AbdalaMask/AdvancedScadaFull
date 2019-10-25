using AdvancedScada.Management.SQLManager;
using ComponentFactory.Krypton.Toolkit;
using System;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Studio.LinkToSQL
{
    public delegate void EventDataBaseChanged(DataBase dbs);
    public partial class XAddDataBase : KryptonForm
    {
        private readonly DataBase dbs;
        public EventDataBaseChanged eventDataBaseChanged = null;
        private readonly Server SQl;
        public XAddDataBase(Server chParam, DataBase dvPara = null)
        {
            InitializeComponent();
            SQl = chParam;
            dbs = dvPara;
        }
        public XAddDataBase()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDataBaseName.Text)
                    || string.IsNullOrWhiteSpace(txtDataBaseName.Text))
                {
                    DxErrorProvider1.SetError(txtDataBaseName, "The DataBase name is empty");
                }
                else
                {
                    DxErrorProvider1.Clear();
                    if (dbs == null)
                    {
                        var dbsNew = new DataBase();
                        dbsNew.DataBaseId = SQl.DataBase.Count + 1;
                        dbsNew.DataBaseName = txtDataBaseName.Text;
                        dbsNew.Description = txtDesc.Text;
                        //dvNew.DataBlocks = new List<DataBlock>();
                        DataBaseManager.Add(SQl, dbsNew);
                        if (eventDataBaseChanged != null) eventDataBaseChanged(dbsNew);
                        Close();
                    }
                    else
                    {
                        dbs.DataBaseId = short.Parse(txtDataBaseId.Text);
                        dbs.DataBaseName = txtDataBaseName.Text;
                        dbs.Description = txtDesc.Text;

                        DataBaseManager.Update(SQl, dbs);
                        if (eventDataBaseChanged != null) eventDataBaseChanged(dbs);

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

        private void XtraSQLAddDataBase_Load(object sender, EventArgs e)
        {
            try
            {
                txtServerName.Text = SQl.ServerName;
                txtServerId.Text = SQl.ServerId.ToString();

                if (dbs != null)
                {
                    Text = "Edit DataBase";
                    txtDataBaseId.Text = $"{ dbs.DataBaseId}";
                    txtDataBaseName.Text = dbs.DataBaseName;

                    txtDesc.Text = dbs.Description;
                }
                else
                {
                    Text = "Add DataBase";
                    txtDataBaseId.Text = Convert.ToString(SQl.DataBase.Count + 1);
                    txtDataBaseName.Items.AddRange(AdvancedScada.Utils.DriverLinkToSQL.LinkToSQL.AddDatabaseNames(SQl.ServerName).ToArray());
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
    }
}
