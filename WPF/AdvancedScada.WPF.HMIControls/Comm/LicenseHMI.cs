using System;
using System.ComponentModel;

namespace AdvancedScada.WPF.HMIControls.Comm
{
    public static class LicenseHMI
    {
        public static bool IsInDesignMode
        {
            get
            {
                Boolean isInWpfDesignerMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
                Boolean isInFormsDesignerMode = (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");

                if (isInWpfDesignerMode || isInFormsDesignerMode)
                {
                    // is in any designer mode
                    return true;
                }
                else
                {
                    // not in designer mode

                    return false;
                }
            }
        }
    }
}
