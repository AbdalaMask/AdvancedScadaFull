using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.DriverBase
{
   public  interface IPropertiesControls
    {
        string PLCAddressValue { get; set; }
        string PLCAddressClick { get; set; }
        string PLCAddressVisible { get; set; }
        string PLCAddressEnabled { get; set; }
        void DisplayError(string ErrorMessage);
    }
}
