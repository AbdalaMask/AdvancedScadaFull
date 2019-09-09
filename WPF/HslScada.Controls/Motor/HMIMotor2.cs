using AdvancedScada.DriverBase.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace HslScada.Controls
{
    public class HMIMotor2 : Control
    {
        #region DependencyProperty
        protected event PropertyChangedCallback PropertyChanged = (sender, e) => { };

        public static DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(HMIMotor2),
         new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HMIMotor2),
          new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));
        public static DependencyProperty MotorColorsProperty = DependencyProperty.Register(
            "MotorColors", typeof(MotorColor), typeof(HMIMotor2),
         new FrameworkPropertyMetadata(MotorColor.Gray, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
           "Value", typeof(bool), typeof(HMIMotor2), new PropertyMetadata(false, OnCurrentReadingChanged));

        // Using a DependencyProperty as the backing store for OutputTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutputTypesProperty =
            DependencyProperty.Register("OutputTypes", typeof(OutputType), typeof(HMIMotor2), new PropertyMetadata(OutputType.MomentarySet));



        public bool SuppressErrorDisplay
        {
            get { return (bool)GetValue(SuppressErrorDisplayProperty); }
            set { SetValue(SuppressErrorDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SuppressErrorDisplay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SuppressErrorDisplayProperty =
            DependencyProperty.Register("SuppressErrorDisplay", typeof(bool), typeof(HMIMotor2), new PropertyMetadata(false));



        private static void OnCurrentReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HMIMotor2 Motor2 = (HMIMotor2)d;
            if (Motor2.Value) Motor2.MotorColors = MotorColor.Green;
            else Motor2.MotorColors = MotorColor.Gray;
            Motor2.PropertyChanged(d, e);
        }

        public static readonly DependencyProperty PLCAddressValueProperty = DependencyProperty.Register(
           "PLCAddressValue", typeof(string), typeof(HMIMotor2), new PropertyMetadata("0"));

        public static readonly DependencyProperty PLCAddressClickProperty = DependencyProperty.Register(
          "PLCAddressClick", typeof(string), typeof(HMIMotor2), new PropertyMetadata("0"));

        [Category("Text")]
        public TextAlignment TextAlignment
        {
            get
            {
                return (TextAlignment)base.GetValue(TextAlignmentProperty);
            }
            set
            {
                base.SetValue(TextAlignmentProperty, value);
            }
        }

        [Category("HMI")]
        public string Text
        {
            get
            {
                return (string)base.GetValue(TextProperty);
            }
            set
            {
                base.SetValue(TextProperty, value);
            }
        }
        [Category("HMI")]
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

        [Category("HMI")]
        public OutputType OutputTypes
        {
            get { return (OutputType)GetValue(OutputTypesProperty); }
            set { SetValue(OutputTypesProperty, value); }
        }
        public enum MotorColor
        {
            Gray,
            Green,
            Red,
            Yellow
        }
        #endregion
        #region override
        ImageSource imageSource;
        protected override void OnRender(DrawingContext drawingContext)
        {
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            double bevel = height * 0.1;
            switch (MotorColors)
            {
                case MotorColor.Gray:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Controls;component/Images/MotorGray.png"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case MotorColor.Green:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Controls;component/Images/MotorGreen.png"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case MotorColor.Red:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Controls;component/Images/MotorRed.png"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case MotorColor.Yellow:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Controls;component/Images/MotorYellow.png"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                default:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Controls;component/Images/MotorGray.png"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
            }
            string txt = this.Text;
            if (!string.IsNullOrEmpty(txt))
            {
                FormattedText formattedText = new FormattedText(txt, System.Globalization.CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                    new Typeface(base.FontFamily, base.FontStyle, base.FontWeight, base.FontStretch), base.FontSize, base.Foreground);
                Point pt = new Point((this.TextAlignment == TextAlignment.Center ? (width - formattedText.Width) * 0.5 :
                   this.TextAlignment == TextAlignment.Left ? bevel : width - bevel - formattedText.Width), (height - formattedText.Height) * 0.5);
                drawingContext.DrawText(formattedText, pt);
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
        #endregion

        #region Events
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (!string.IsNullOrWhiteSpace(PLCAddressClick) & IsEnabled && PLCAddressClick != null)
            {
                try
                {
                    switch (OutputTypes)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(PLCAddressClick, "1");
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(PLCAddressClick, "0");
                            break;
                        case OutputType.SetTrue:
                            Utilities.Write(PLCAddressClick, "1");
                            break;
                        case OutputType.SetFalse:
                            Utilities.Write(PLCAddressClick, "0");
                            break;
                        case OutputType.Toggle:

                            var CurrentValue = Value;
                            if (CurrentValue)
                                Utilities.Write(PLCAddressClick, "0");
                            else
                                Utilities.Write(PLCAddressClick, "1");
                            break;
                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError("WRITE FAILED!" + ex.Message);
                }

                
            }
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (!string.IsNullOrWhiteSpace(PLCAddressClick) & IsEnabled)
            {
                try
                {

                    switch (OutputTypes)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(PLCAddressClick, "0");
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(PLCAddressClick, "1");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError("WRITE FAILED!" + ex.Message);
                }

                 
            }
        }
        #endregion
        #region Error Display
        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private string OriginalText;
        private DispatcherTimer ErrorDisplayTime;
       


        private void DisplayError(string ErrorMessage)
        {
            if (!SuppressErrorDisplay)
            {
                if (ErrorDisplayTime == null)
                {
                    ErrorDisplayTime = new DispatcherTimer();
                    ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                    ErrorDisplayTime.Interval = TimeSpan.FromSeconds(1);
                }

                //* Save the text to return to
                if (!ErrorDisplayTime.IsEnabled)
                {
                    OriginalText = this.Text;
                }

                ErrorDisplayTime.IsEnabled = true;

                this.Text = ErrorMessage;
            }
        }


        //**************************************************************************************
        //* Return the text back to its original after displaying the error for a few seconds.
        //**************************************************************************************
        private void ErrorDisplay_Tick(object sender, System.EventArgs e)
        {
            Text = OriginalText;

            if (ErrorDisplayTime != null)
            {
                ErrorDisplayTime.IsEnabled = false;
                ErrorDisplayTime.Stop();
                ErrorDisplayTime = null;
            }
        }
        #endregion
    }
}
