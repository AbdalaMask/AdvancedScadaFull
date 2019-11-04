using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdvancedScada.WPF.HMIControls.SevenSegment
{
    /// <summary>
    /// Interaction logic for HMISevenSegmentNew.xaml
    /// </summary>
    public partial class HMISevenSegmentNew : UserControl
    {
        public HMISevenSegmentNew()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
     "Value", typeof(string), typeof(HMISevenSegmentNew), new PropertyMetadata("0"));
        public static readonly DependencyProperty PLCAddressValueProperty = DependencyProperty.Register(
         "PLCAddressValue", typeof(string), typeof(HMISevenSegmentNew), new PropertyMetadata("0"));

        public static readonly DependencyProperty PLCAddressClickProperty = DependencyProperty.Register(
          "PLCAddressClick", typeof(string), typeof(HMISevenSegmentNew), new PropertyMetadata("0"));


        [Category("HMI")]
        public string PLCAddressValue
        {
            get
            {
                return (string)base.GetValue(PLCAddressValueProperty);
            }
            set
            {
                base.SetValue(PLCAddressValueProperty, value);



            }
        }

        [Category("HMI")]
        public string PLCAddressClick
        {
            get
            {
                return (string)base.GetValue(PLCAddressValueProperty);
            }
            set
            {
                base.SetValue(PLCAddressValueProperty, value);



            }
        }
        [Category("HMI")]
        public string Value
        {
            get
            {
                return (string)base.GetValue(ValueProperty);
            }
            set
            {
                base.SetValue(ValueProperty, value);



            }
        }
    }
}
