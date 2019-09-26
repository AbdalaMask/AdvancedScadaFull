using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.WPF.HMIControls.Comm
{
    public enum OutputType
    {
        MomentarySet,
        MomentaryReset,
        SetTrue,
        SetFalse,
        Toggle,
        WriteValue
    }
}
