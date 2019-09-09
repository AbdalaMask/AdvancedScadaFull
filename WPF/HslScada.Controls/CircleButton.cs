using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HslScada.Controls
{
   public class CircleButton: Control
    {
        static CircleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleButton), new FrameworkPropertyMetadata(typeof(CircleButton)));
        }
    }
}
