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
    /// Interaction logic for HMIPushButton.xaml
    /// </summary>
    public partial class HMIPushButton : UserControl
    {
        public HMIPushButton()
        {
            InitializeComponent();
        }
        [Category("HMI")]
        public LightColors LightColor
        {
            get { return (LightColors)GetValue(LightColorProperty); }
            set { SetValue(LightColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LightColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightColorProperty =
            DependencyProperty.Register("LightColor", typeof(LightColors), typeof(HMIPushButton), new FrameworkPropertyMetadata(LightColors.Blue, FrameworkPropertyMetadataOptions.AffectsRender));

        #region DependencyProperty


        public static readonly DependencyProperty PilotLightSmallTextProperty = DependencyProperty.Register(
           "PilotLightSmallText", typeof(string), typeof(HMIPushButton), new PropertyMetadata("Name"));
        // Using a DependencyProperty as the backing store for OutputTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutputTypesProperty =
            DependencyProperty.Register("OutputTypes", typeof(OutputType), typeof(HMIPushButton), new PropertyMetadata(OutputType.MomentarySet));

        public static readonly DependencyProperty PLCAddressValueProperty = DependencyProperty.Register(
          "PLCAddressValue", typeof(string), typeof(HMIPushButton), new PropertyMetadata("0"));

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
        public string PilotLightSmallText
        {
            set { SetValue(PilotLightSmallTextProperty, value); }
            get { return (string)GetValue(PilotLightSmallTextProperty); }
        }
        #endregion
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            switch (LightColor)
            {
                case LightColors.Red:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/RedButton.png"));

                    break;
                case LightColors.Green:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/GreenButton.png"));

                    break;
                case LightColors.Blue:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/BlueButton.png"));

                    break;
                //case LightColors.Yellow:
                //    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/WpfApp1;component/Resources/YellowPilotOff.png"));

                //    break;
                case LightColors.Black:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/BlackButton.png"));

                    break;
                default:
                    break;
            }
        }
        private void HMIPushButtonAll_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (LightColor)
            {
                case LightColors.Red:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/RedButtonPressed.png"));

                    break;
                case LightColors.Green:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/GreenButtonPressed.png"));

                    break;
                case LightColors.Blue:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/BlueButtonPressed.png"));

                    break;
                //case LightColors.Yellow:
                //    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/WpfApp1;component/Resources/YellowPilotOn.png"));

                //    break;
                case LightColors.Black:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/BlackButtonPressed.png"));

                    break;
                default:
                    break;
            }
        }

        private void HMIPushButtonAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (LightColor)
            {
                case LightColors.Red:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/RedButton.png"));

                    break;
                case LightColors.Green:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/GreenButton.png"));

                    break;
                case LightColors.Blue:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/BlueButton.png"));

                    break;
                //case LightColors.Yellow:
                //    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/WpfApp1;component/Resources/YellowPilotOff.png"));

                //    break;
                case LightColors.Black:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/BlackButton.png"));

                    break;
                default:
                    break;
            }
        }

        public enum LightColors
        {
            Red,
            Green,
            Blue,
            Yellow,
            Black
        }
    }
}
