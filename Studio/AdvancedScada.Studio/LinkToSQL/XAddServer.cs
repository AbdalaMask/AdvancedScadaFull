using AdvancedScada.Management.SQLManager;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Win32;
using System;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Studio.LinkToSQL
{
    public delegate void EventSQLServerChanged(Server SQ);
    public partial class XAddServer : KryptonForm
    {
        public EventSQLServerChanged eventSQLServerChanged = null;
        private readonly ServerManager objServerManager;
        private readonly Server SQ;
        public XAddServer()
        {
            InitializeComponent();
        }
        public XAddServer(ServerManager SManager, Server SQLCurrent = null)
        {
            InitializeComponent();
            SQ = SQLCurrent;
            objServerManager = SManager;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (SQ == null)
                {
                    var clsSql = new Server();
                    clsSql.ServerId = Convert.ToInt32(txtServerId.Text);
                    clsSql.ServerName = txtServerName.Text;
                    clsSql.UserName = txtUserName.Text;
                    clsSql.Passwerd = Convert.ToInt32(txtPasswerd.Text);
                    clsSql.Description = txtDesc.Text;
                    objServerManager.Add(clsSql);
                    eventSQLServerChanged?.Invoke(clsSql);
                    System.Windows.Forms.DialogResult dialogResult = DialogResult;


                }
                else
                {
                    SQ.ServerId = Convert.ToInt32(txtServerId.Text);
                    SQ.ServerName = txtServerName.Text;
                    SQ.UserName = txtUserName.Text;
                    SQ.Passwerd = Convert.ToInt32(txtPasswerd.Text);
                    SQ.Description = txtDesc.Text;
                    objServerManager.Update(SQ);
                    eventSQLServerChanged?.Invoke(SQ);

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

        private void XtraSQLAdd_Load(object sender, EventArgs e)
        {
            if (SQ != null)
            {
                Text = "Edit SQL";
                txtServerId.Text = $"{ SQ.ServerId}";
                txtServerName.Text = SQ.ServerName;
                txtUserName.Text = SQ.UserName;
                txtPasswerd.Text = $"{ SQ.Passwerd}";
                txtDesc.Text = SQ.Description;



            }
            else
            {
                Text = "Add SQL";
                txtServerId.Text += 1;
            }
            var rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server");
            var instances = (string[])rk.GetValue("InstalledInstances");
            if (instances != null)
                if (instances.Length > 0)
                    foreach (var element in instances)
                        if (element == "MSSQLSERVER")
                            txtServerName.Items.Add(Environment.MachineName);
                        else
                            txtServerName.Items.Add(Environment.MachineName + "\\" + element);
        }
    }
}
