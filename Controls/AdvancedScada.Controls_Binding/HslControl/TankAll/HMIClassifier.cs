using AdvancedScada.Common;
using HslControls;

namespace AdvancedScada.Controls_Binding.HslControl.TankAll
{
    public class HMIClassifier : HslClassifier, IPropertiesControls
    {
        public string PLCAddressValue { get ; set; }
        public string PLCAddressClick { get; set; }
        public string PLCAddressVisible { get; set; }
        public string PLCAddressEnabled { get; set; }

        public void DisplayError(string ErrorMessage)
        {
            Utilities.DisplayError(this, ErrorMessage);
        }
    }
}
