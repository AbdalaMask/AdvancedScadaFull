using System.Windows;
using System.Windows.Controls;

namespace HslScada.Controls
{
    public class CircleButton : Control
    {
        static CircleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleButton), new FrameworkPropertyMetadata(typeof(CircleButton)));
        }
    }
}
