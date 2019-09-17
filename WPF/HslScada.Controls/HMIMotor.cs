using System.Windows;
using System.Windows.Controls;

namespace HslScada.Controls
{
    public class HMIMotor : Control
    {
        static HMIMotor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HMIMotor), new FrameworkPropertyMetadata(typeof(HMIMotor)));
        }
    }
}
