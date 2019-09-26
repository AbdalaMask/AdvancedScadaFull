using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
