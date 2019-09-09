using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Client;
using KeyPad;
using HslScada.Controls;

namespace HMIControl
{

    public class HMILable : Control
    {
        public HMILable()
        {


        }
       
        public static DependencyProperty BorderStyleProperty = DependencyProperty.Register("BorderStyle", typeof(BorderStyle), typeof(HMILable),
             new FrameworkPropertyMetadata(BorderStyle.None, FrameworkPropertyMetadataOptions.AffectsRender));

        public static DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(HMILable),
             new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HMILable),
          new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty UnitProperty = DependencyProperty.Register("Unit", typeof(string), typeof(HMILable),
        new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

        public static DependencyProperty StringFormatProperty = DependencyProperty.Register("StringFormat", typeof(string), typeof(HMILable));

        public static readonly DependencyProperty PLCAddressValueProperty = DependencyProperty.Register("PLCAddressValue", typeof(string),
            typeof(HMILable),
        new FrameworkPropertyMetadata("0"));


      

        protected override void OnRender(DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            double bevel = height * 0.1;
            Color color = Colors.Black;
            if (this.Background is SolidColorBrush)
                color = (this.Background as SolidColorBrush).Color;
            Pen pen = new Pen(base.BorderBrush, base.BorderThickness.Left);
            switch (this.BorderStyle)
            {
                case BorderStyle.Fixed3D:

                    break;
                case BorderStyle.FixedSingle:
                    drawingContext.DrawRectangle(this.Background, pen, new Rect(0, 0, width, height));
                    break;
                default:
                    drawingContext.DrawRectangle(this.Background, pen, new Rect(0, 0, width, height));
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
                this.SetBinding(TextProperty, binding);
            }


        }


        [Category("HMI")]
        public BorderStyle BorderStyle
        {
            get
            {
                return (BorderStyle)base.GetValue(BorderStyleProperty);
            }
            set
            {
                base.SetValue(BorderStyleProperty, value);
            }
        }

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
        public string Unit
        {
            get
            {
                return (string)base.GetValue(UnitProperty);
            }
            set
            {
                base.SetValue(UnitProperty, value);
            }
        }

        [Category("HMI")]
        public string StringFormat
        {
            get
            {
                return (string)base.GetValue(StringFormatProperty);
            }
            set
            {
                base.SetValue(StringFormatProperty, value);
            }
        }

        #region "Keypad popup for data entry"

        [Category("HMI")]
        public string PLCAddressKeypad
        {
            get { return (string)GetValue(PLCAddressKeypadProperty); }
            set { SetValue(PLCAddressKeypadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PLCAddressKeypad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PLCAddressKeypadProperty =
            DependencyProperty.Register("PLCAddressKeypad", typeof(string), typeof(HMILable), new PropertyMetadata(string.Empty));


        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            if (PLCAddressKeypad != null && (string.Compare(PLCAddressKeypad, string.Empty) != 0) & IsEnabled)
            {
                Keypad keypadWindow = new Keypad(this, null);
                if (keypadWindow.ShowDialog() == true)
                {
                    Utilities.Write(PLCAddressKeypad, keypadWindow.Result);
                }
                    //this.Text = keypadWindow.Result;
            }
        }

        #endregion
    }



    public enum BorderStyle
    {
        Fixed3D,
        FixedSingle,
        None
    }
   
}