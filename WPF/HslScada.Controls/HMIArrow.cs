using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HslScada.Controls
{
   public  class HMIArrow : Button
    {
        static HMIArrow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HMIArrow), new FrameworkPropertyMetadata(typeof(HMIArrow)));
        }
    }
}
