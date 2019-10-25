namespace AdvancedScada.Common
{
    public interface IPropertiesControls
    {
        string PLCAddressValue { get; set; }
        string PLCAddressClick { get; set; }
        string PLCAddressVisible { get; set; }
        string PLCAddressEnabled { get; set; }
        void DisplayError(string ErrorMessage);
    }
}
