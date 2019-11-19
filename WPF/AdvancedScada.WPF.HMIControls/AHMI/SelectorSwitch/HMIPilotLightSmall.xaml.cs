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
    /// Interaction logic for HMIPilotLightSmall.xaml
    /// </summary>
    public partial class HMIPilotLightSmall : UserControl
    {
        public HMIPilotLightSmall()
        {
            InitializeComponent();
        }
        [Category("HMI")]
        public LightColors LightColorOff
        {
            get { return (LightColors)GetValue(LightColorOffProperty); }
            set { SetValue(LightColorOffProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LightColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightColorOffProperty =
            DependencyProperty.Register("LightColorOff", typeof(LightColors), typeof(HMIPilotLightSmall), new FrameworkPropertyMetadata(LightColors.Blue, FrameworkPropertyMetadataOptions.AffectsRender));
        [Category("HMI")]
        public LightColors LightColor
        {
            get { return (LightColors)GetValue(LightColorProperty); }
            set { SetValue(LightColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LightColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightColorProperty =
            DependencyProperty.Register("LightColor", typeof(LightColors), typeof(HMIPilotLightSmall), new PropertyMetadata(LightColors.Blue));
        #region DependencyProperty

       
        public static readonly DependencyProperty PilotLightSmallTextProperty = DependencyProperty.Register(
           "PilotLightSmallText", typeof(string), typeof(HMIPilotLightSmall), new PropertyMetadata("Name"));
        // Using a DependencyProperty as the backing store for OutputTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutputTypesProperty =
            DependencyProperty.Register("OutputTypes", typeof(OutputType), typeof(HMIPilotLightSmall), new PropertyMetadata(OutputType.MomentarySet));

        public static readonly DependencyProperty PLCAddressValueProperty = DependencyProperty.Register(
          "PLCAddressValue", typeof(string), typeof(HMIPilotLightSmall), new PropertyMetadata("0"));

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
            switch (LightColorOff)
            {
                case LightColors.Red:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/RedPilotOff.png"));

                    break;
                case LightColors.Green:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/GreenPilotOff.png"));

                    break;
                case LightColors.Blue:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/BluePilotOff.png"));

                    break;
                case LightColors.Yellow:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/YellowPilotOff.png"));

                    break;
                case LightColors.White:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/WhitePilotOff.png"));

                    break;
                default:
                    break;
            }
        }
        private void HMIPilotLightAll_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (LightColor)
            {
                case LightColors.Red:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/RedPilotOn.png"));

                    break;
                case LightColors.Green:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/GreenPilotOn.png"));

                    break;
                case LightColors.Blue:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/BluePilotOn.png"));

                    break;
                case LightColors.Yellow:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/YellowPilotOn.png"));

                    break;
                case LightColors.White:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/WhitePilotOn.png"));

                    break;
                default:
                    break;
            }
        }

        private void HMIPilotLightAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (LightColorOff)
            {
                case LightColors.Red:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/RedPilotOff.png"));

                    break;
                case LightColors.Green:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/GreenPilotOff.png"));

                    break;
                case LightColors.Blue:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/BluePilotOff.png"));

                    break;
                case LightColors.Yellow:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/YellowPilotOff.png"));

                    break;
                case LightColors.White:
                    ImgButton.Source = new BitmapImage(new Uri($"pack://application:,,,/{MyResource.ResourceName};component/Images/WhitePilotOff.png"));

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
            White
        }
    }
}
