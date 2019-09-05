using System.Windows.Forms;
using AdvancedScada;
using AdvancedScada.Controls.Utility;
using AdvancedScada;
using AdvancedScada.Controls;

namespace AdvancedScada.Controls.Utility
{
    public static class Utility
    {
        public static void ShowTagInvalidMessage(IWin32Window control, string tagName)
        {
            MessageBox.Show(control, $"The {tagName} have Tag = null", "ERROR", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static void ShowTagNameInvalidMessage(IWin32Window control, string controlName)
        {
            MessageBox.Show(control, $"The {controlName} have TagName = null", "ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}