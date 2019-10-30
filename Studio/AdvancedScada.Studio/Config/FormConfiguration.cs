using AdvancedScada.Studio.Properties;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Win32;
using System;
using System.Data;
using System.Data.Sql;
using System.Windows.Forms;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Studio.Config
{
    public partial class FormConfiguration : KryptonForm
    {
        private string DatabaseTypes = string.Empty;
        public FormConfiguration()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\FormConfiguration", "IPAddress", txtIPAddress.Text);
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\FormConfiguration", "Port", txtPort.Text);
 

                Settings.Default.teServer = txtServerName.Text;
                Settings.Default.Port = txtPort.Text;
                Settings.Default.IPAddress = txtIPAddress.Text;
                Settings.Default.DatabaseTypes = DatabaseTypes;

           

                Settings.Default.Save();

                Close();

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        public void FillComboServers(KryptonComboBox Combo)
        {
            var SqlEnumerator = SqlDataSourceEnumerator.Instance;
            var dTable = SqlEnumerator.GetDataSources();
            foreach (DataRow Dr in dTable.Rows) Combo.Items.Add(Dr[0]);
        }
        public void GetSQLServer()
        {
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormConfiguration_Load(object sender, EventArgs e)
        {
            try
            {



                cboxDatabaseTypes.Text = Settings.Default.DatabaseTypes;

          
                GetSQLServer();

                txtIPAddress.Text = Settings.Default.IPAddress;
                txtPort.Text = Settings.Default.Port;
                txtServerName.Text = Settings.Default.teServer;



            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void cboxDatabaseTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseTypes = cboxDatabaseTypes.Text;
        }
        public void WriteKey(string keyName, string keyValue)
        {
            try
            {
                RegistryKey regKey;
                regKey = Registry.CurrentUser.CreateSubKey(@"Software\HMI");
                regKey.SetValue(keyName, keyValue);
                regKey.Close();
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        public void WriteKeySymbolFactory(string keyName, string keyValue)
        {
            try
            {
                RegistryKey regKey;
                regKey = Registry.LocalMachine.CreateSubKey(@"Software\\Software Toolbox\\Symbol Factory .NET\\2.0.0.221");
                regKey.SetValue(keyName, keyValue);
                regKey.Close();
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
      
    }
}
