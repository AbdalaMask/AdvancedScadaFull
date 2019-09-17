using System.Windows;
using System.Windows.Controls;

namespace HslScada.Controls
{
    public class HMIArrow : Button
    {
        static HMIArrow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HMIArrow), new FrameworkPropertyMetadata(typeof(HMIArrow)));
        }
    }
}
