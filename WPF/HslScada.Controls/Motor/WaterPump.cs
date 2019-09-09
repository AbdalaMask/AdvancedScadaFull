using AdvancedScada.DriverBase.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HslScada.Controls
{
   public class WaterPump : Control
    {
        protected event PropertyChangedCallback PropertyChanged = (sender, e) => { };
        public static DependencyProperty MotorColorsProperty = DependencyProperty.Register(
          "MotorColors", typeof(MotorColor), typeof(WaterPump),
       new FrameworkPropertyMetadata(MotorColor.Gray, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
          "Value", typeof(bool), typeof(WaterPump), new PropertyMetadata(false, OnCurrentReadingChanged));

        private static void OnCurrentReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WaterPump Motor2 = (WaterPump)d;
            if (Motor2.Value) Motor2.MotorColors = MotorColor.Green;
            else Motor2.MotorColors = MotorColor.Gray;
            Motor2.PropertyChanged(d, e);
        }

        public static readonly DependencyProperty PLCAddressValueProperty = DependencyProperty.Register(
           "PLCAddressValue", typeof(string), typeof(WaterPump), new PropertyMetadata("0"));


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
        public bool Value
        {
            get
            {
                return (bool)base.GetValue(ValueProperty);
            }
            set
            {
                base.SetValue(ValueProperty, value);



            }
        }
        [Category("HMI"),Browsable(false)]
        public MotorColor MotorColors
        {
            get
            {
                return (MotorColor)base.GetValue(MotorColorsProperty);
            }
            set
            {
                base.SetValue(MotorColorsProperty, value);

            }
        }
        public enum MotorColor
        {
            Gray,
            Green
           
        }

        ImageSource imageSource;
        protected override void OnRender(DrawingContext drawingContext)
        {
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            switch (MotorColors)
            {
                case MotorColor.Gray:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Controls;component/Images/RSCorPumpOff.png"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case MotorColor.Green:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Controls;component/Images/RSCorPumpOn.png"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                default:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Controls;component/Images/RSCorPumpOff.png"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
            }


        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Boolean isInWpfDesignerMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            Boolean isInFormsDesignerMode = (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");

            if (isInWpfDesignerMode || isInFormsDesignerMode)
            {
                // is in any designer mode
            }
            else
            {
                // not in designer mode
                //* When address is changed, re-subscribe to new address
                if (string.IsNullOrEmpty(PLCAddressValue) || string.IsNullOrWhiteSpace(PLCAddressValue) ||
                    HslScada.Controls.Licenses.LicenseManager.IsInDesignMode) return;
                Binding binding = new Binding("Value");
                binding.Source = TagCollectionClient.Tags[PLCAddressValue];
                this.SetBinding(ValueProperty, binding);
            }


        }
    }
}
