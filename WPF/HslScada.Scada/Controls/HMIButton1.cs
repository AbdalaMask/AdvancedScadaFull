using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HslScada.Scada.Controls
{
   public  class HMIButton1 : Control
    {

        #region DependencyProperty

       
        public static DependencyProperty MotorColorsProperty = DependencyProperty.Register(
          "ButtonColor", typeof(ButtonColor), typeof(HMIButton1),
       new FrameworkPropertyMetadata(ButtonColor.Gray, FrameworkPropertyMetadataOptions.AffectsRender));

       
        public static DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(HMIButton1),
          new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HMIButton1),
          new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));
        [Category("HMI")]
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
        public ButtonColor ButtonColors
        {
            get
            {
                return (ButtonColor)base.GetValue(MotorColorsProperty);
            }
            set
            {
                base.SetValue(MotorColorsProperty, value);

            }
        }
        public enum ButtonColor
        {
            Gray,
            Green,Red, Orange, Yellow, BlueViolet, WhiteSmoke, Brown, DarkGreen

        }
        #endregion
        #region OnRender


        ImageSource imageSource;
        protected override void OnRender(DrawingContext drawingContext)
        {
            
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            double bevel = height * 0.1;
            switch (ButtonColors)
            {
                case ButtonColor.Gray:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Scada;component/Resources/18-6.bmp"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case ButtonColor.Green:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Scada;component/Resources/17-4.bmp"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case ButtonColor.Red:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Scada;component/Resources/17-1.bmp"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case ButtonColor.Orange:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Scada;component/Resources/17-2.bmp"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case ButtonColor.Yellow:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Scada;component/Resources/17-3.bmp"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case ButtonColor.BlueViolet:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Scada;component/Resources/17-5.bmp"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case ButtonColor.WhiteSmoke:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Scada;component/Resources/17-6.bmp"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case ButtonColor.Brown:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Scada;component/Resources/18-3.bmp"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                case ButtonColor.DarkGreen:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Scada;component/Resources/18-4.bmp"));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));
                    break;
                default:
                    imageSource = new BitmapImage(new Uri("pack://application:,,,/HslScada.Scada;component/Resources/17-1.bmp"));
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
        #endregion
    }
}
