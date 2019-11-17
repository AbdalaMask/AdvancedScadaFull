using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
