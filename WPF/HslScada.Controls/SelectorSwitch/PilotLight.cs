using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace HslScada.Controls
{
    public class PilotLight : ToggleButton
    {
        static PilotLight()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PilotLight), new FrameworkPropertyMetadata(typeof(PilotLight)));
        }
    }
}
