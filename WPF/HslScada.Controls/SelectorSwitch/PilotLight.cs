using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace HslScada.Controls
{
   public  class PilotLight: ToggleButton
    {
        static PilotLight()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PilotLight), new FrameworkPropertyMetadata(typeof(PilotLight)));
        }
    }
}
