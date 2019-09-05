using AdvancedScada.Management;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Win32;
using System;
using System.Linq;
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

        private void XSelectedDrivers_Load(object sender, EventArgs e)
        {

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
                case "LSIS":
                    picSelectedDrivers.Image = Properties.Resources._0000157_xbc_dn32ua_500;
                    break;
                case "Delta":
                    picSelectedDrivers.Image = Properties.Resources.DVP10MC11T_300x300;
                    break;
                case "Modbus":
                    picSelectedDrivers.Image = Properties.Resources.Modbus;
                    break;
                //case 3:
                //    picSelectedDrivers.Image = Properties.Resources.Panasonic;
                //    break;

                default:
                    break;
            }


            //// قراءة مصفوفة  البايت الخاصة بالمشروع الثاني
            //txtPath.Text = Application.StartupPath + $@"\AdvancedScada.{DriverTypes.Insert(0, "X")}.Core.dll";
        }
    }
}
