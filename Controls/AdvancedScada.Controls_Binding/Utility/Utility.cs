using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.Utility
{
    public static class Utility
    {
        public static void ShowTagInvalidMessage(IWin32Window control, string tagName)
        {
            MessageBox.Show(control, string.Format("The {0} have Tag = null", tagName), "ERROR", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static void ShowTagNameInvalidMessage(IWin32Window control, string controlName)
        {
            MessageBox.Show(control, string.Format("The {0} have TagName = null", controlName), "ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}