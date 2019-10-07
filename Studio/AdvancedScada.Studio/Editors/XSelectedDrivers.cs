using AdvancedScada.Management;
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
                    if (t.GetInterface(typeof(AdvancedScada.DriverBase.IODriver).FullName) != null)
                    {
                        AdvancedScada.DriverBase.IODriver plug = (AdvancedScada.DriverBase.IODriver)Activator.CreateInstance(t);
                        cboxSelectedDrivers.Items.Add(plug.Name);
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

            switch (cboxSelectedDrivers.Text)
            {
                case "Siemens":
                    picSelectedDrivers.Image = Properties.Resources.PLC_SIEMENS;
                    break;
                case "LSIS":
                    picSelectedDrivers.Image = Properties.Resources.P00135;
                    break;
                case "Delta":
                    picSelectedDrivers.Image = Properties.Resources.DVP10MC11T_300x300;
                    break;
                case "Modbus":
                    picSelectedDrivers.Image = Properties.Resources.Modbus;
                    break;
               case "OPC":
                    picSelectedDrivers.Image = Properties.Resources.OPC;
                    break;
                default:
                    picSelectedDrivers.Image = Properties.Resources.img_wemx_designer_5;
                    break;
            }

        }
    }
}
