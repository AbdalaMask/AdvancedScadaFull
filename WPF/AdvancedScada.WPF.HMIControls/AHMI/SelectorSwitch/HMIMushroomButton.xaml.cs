using AdvancedScada.WPF.HMIControls.Comm;
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

namespace AdvancedScada.WPF.HMIControls.AHMI.SelectorSwitch
{
    /// <summary>
    /// Interaction logic for HMIMushroomButton.xaml
    /// </summary>
    public partial class HMIMushroomButton : UserControl
    {
        public HMIMushroomButton()
        {
            InitializeComponent();
        }
        #region DependencyProperty


        public static readonly DependencyProperty HMIMushroomButtonTextProperty = DependencyProperty.Register(
           "HMIMushroomButtonText", typeof(string), typeof(HMIMushroomButton), new PropertyMetadata("Name"));
        // Using a DependencyProperty as the backing store for OutputTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutputTypesProperty =
            DependencyProperty.Register("OutputTypes", typeof(OutputType), typeof(HMIMushroomButton), new PropertyMetadata(OutputType.MomentarySet));

        public static readonly DependencyProperty PLCAddressValueProperty = DependencyProperty.Register(
          "PLCAddressValue", typeof(string), typeof(HMIMushroomButton), new PropertyMetadata("0"));

        #endregion

        #region Public Properties
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
        public OutputType OutputTypes
        {
            get { return (OutputType)GetValue(OutputTypesProperty); }
            set { SetValue(OutputTypesProperty, value); }
        }

        /// <summary>
        /// string
        /// </summary>
        [Category("HMI")]
        public string HMIMushroomButtonText
        {
            set { SetValue(HMIMushroomButtonTextProperty, value); }
            get { return (string)GetValue(HMIMushroomButtonTextProperty); }
        }
        #endregion
        private void HMIMushroomButtonAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/EstopButton.png"));

        }

        private void HMIMushroomButtonAll_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/EstopButtonDown.png"));
        }
    }
}
