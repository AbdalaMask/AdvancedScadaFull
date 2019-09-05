using AdvancedScada.Controls.DialogEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedScada;
 
 
using AdvancedScada.Controls.AHMI.Licenses;
using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.AHMI.Display;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;

namespace AdvancedScada.Controls.AHMI.Display
{
    public class HMITextBoxInput : System.Windows.Forms.TextBox
    {   //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressValueToWrite = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressValueToWrite
        {
            get { return m_PLCAddressValueToWrite; }
            set { m_PLCAddressValueToWrite = value; }


        }

        public void ValueToWrite()
        {
            if (string.IsNullOrEmpty(m_PLCAddressValueToWrite) || string.IsNullOrWhiteSpace(m_PLCAddressValueToWrite) ||
                          Licenses.LicenseManager.IsInDesignMode) return;
            Utilities.Write(m_PLCAddressValueToWrite, this.Text);

        }

    }


}

