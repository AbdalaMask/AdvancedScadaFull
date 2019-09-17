using AdvancedScada;
using AdvancedScada;
using AdvancedScada;
using AdvancedScada;
using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.Licenses;
using System.Diagnostics;

namespace AdvancedScada.Controls.AHMI.Licenses
{
    public static class LicenseManager
    {
        public static bool IsInDesignMode
        {
            get
            {
                if (Process.GetCurrentProcess().ProcessName == "devenv"
                    || Process.GetCurrentProcess().ProcessName == "VCSExpress"
                    || Process.GetCurrentProcess().ProcessName == "vbexpress"
                    || Process.GetCurrentProcess().ProcessName == "WDExpress")
                    return true;
                return false;
            }
        }
    }
}