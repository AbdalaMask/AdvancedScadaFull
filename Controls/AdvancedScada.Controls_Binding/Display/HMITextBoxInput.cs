using AdvancedScada.Controls_Binding.DialogEditor;
using System.ComponentModel;
using System.Drawing.Design;

namespace AdvancedScada.Controls_Binding.Display
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
                          Controls_Binding.Licenses.LicenseManager.IsInDesignMode) return;
            Utilities.Write(m_PLCAddressValueToWrite, this.Text);

        }

    }


}

