using AdvancedScada.Controls_Binding.DialogEditor;
using AdvancedScada.DriverBase;
using System.ComponentModel;
using System.Drawing.Design;

namespace AdvancedScada.Controls_Binding.Display
{
    public class HMITextBoxInput : System.Windows.Forms.TextBox, IPropertiesControls
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

        public string PLCAddressValue { get; set; }
        public string PLCAddressClick { get; set; }
        public string PLCAddressVisible { get; set; }
        public string PLCAddressEnabled { get; set; }

        public void DisplayError(string ErrorMessage)
        {
            throw new System.NotImplementedException();
        }

        public void ValueToWrite()
        {
            if (string.IsNullOrEmpty(m_PLCAddressValueToWrite) || string.IsNullOrWhiteSpace(m_PLCAddressValueToWrite) ||
                          Controls_Binding.Licenses.LicenseManager.IsInDesignMode) return;
            Utilities.Write(m_PLCAddressValueToWrite, this.Text);

        }

    }


}

