using AdvancedScada.Common;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AdvancedScada.Studio.Editors
{
    public partial class XSelectedDrivers : KryptonForm
    {
        private string DriverTypes;
        public EventSelectedDriversChanged eventSelectedDriversChanged = null;
        public XSelectedDrivers()
        {
            InitializeComponent();
        }
        public void LoadPlug()
        {
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            foreach (FileInfo fi in di.GetFiles("AdvancedScada.*.Core.dll"))
            {
                Assembly lib = Assembly.LoadFrom(fi.FullName);
                foreach (Type t in lib.GetExportedTypes())
                {
                    if (t.GetInterface(typeof(IODriver).FullName) != null)
                    {
                        IODriver plug = (IODriver)Activator.CreateInstance(t);
                        cboxSelectedDrivers.Items.Add(plug.Name);
                    }
                }
            }
        }
        public void LoadPlug(string ImageUrl)
        {
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            foreach (FileInfo fi in di.GetFiles($"AdvancedScada.{ImageUrl}.Core.dll"))
            {
                Assembly lib = Assembly.LoadFrom(fi.FullName);
                foreach (Type t in lib.GetExportedTypes())
                {
                    if (t.GetInterface(typeof(IODriver).FullName) != null)
                    {
                        IODriver plug = (IODriver)Activator.CreateInstance(t);
                        picSelectedDrivers.Image = plug.ImageUrl;
                    }
                }
            }
        }
        private void XSelectedDrivers_Load(object sender, EventArgs e)
        {
            cboxSelectedDrivers.Items.Clear();
            LoadPlug();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\FormConfiguration", "ChannelTypes", DriverTypes);
            eventSelectedDriversChanged?.Invoke(true);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            eventSelectedDriversChanged?.Invoke(false);

            Close();
        }

        private void cboxSelectedDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            DriverTypes = cboxSelectedDrivers.Text;

            LoadPlug(DriverTypes);

        }
    }
}
