using AdvancedScada.DriverBase;
using HslControls;

namespace AdvancedScada.Controls_Binding.HslControl.SelectorSwitch
{
    public class HMISwitch : HslSwitch, IPropertiesControls
    {
        public string PLCAddressValue { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string PLCAddressClick { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string PLCAddressVisible { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string PLCAddressEnabled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void DisplayError(string ErrorMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}
