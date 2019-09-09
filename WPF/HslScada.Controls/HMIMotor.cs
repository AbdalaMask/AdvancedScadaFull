using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
