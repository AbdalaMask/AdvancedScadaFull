using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Client;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.Linear
{

    public class HMILinearMeterV : MfgControl.AdvancedHMI.Controls.LinearMeterVertical, IPropertiesControls
    {

        #region propartas

        private string _TagName;

        [Category("Link TagName")]
        [Browsable(true)]
        public string TagName
        {
            get { return _TagName; }

            set
            {
                _TagName = value;
                try
                {
                    if (string.IsNullOrEmpty(_TagName) || string.IsNullOrWhiteSpace(_TagName) ||
                        Licenses.LicenseManager.IsInDesignMode) return;
                    var bd = new Binding("Value", TagCollectionClient.Tags[_TagName], "Value", true);
                    if (DataBindings.Count > 0) DataBindings.Clear();
                    DataBindings.Add(bd);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string PLCAddressValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PLCAddressClick { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PLCAddressVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PLCAddressEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void DisplayError(string ErrorMessage)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


}