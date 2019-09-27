using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdvancedScada.WPF.HMIControls.ButtonAll
{
   public  class HMIButton2 : System.Windows.Controls.UserControl
    {
        private System.Globalization.CultureInfo EnglishCultureInfo = new System.Globalization.CultureInfo("en-us", false);

        public System.Windows.Media.LinearGradientBrush LinearProcessBrush;

        public System.Windows.Media.LinearGradientBrush LinearProcessBrush2;

        public HMIButton2() {
        // ---------------------------
        Viewbox _viewbox0 = new Viewbox();
        this.Content = _viewbox0;
        // ---------------------------
        Canvas _canvas1 = new Canvas();
        _viewbox0.Child = _canvas1;
        _canvas1.Height = 40D;
        _canvas1.Width = 100D;
        // ---------------------------
        UIElementCollection _uIElementCollection2 = _canvas1.Children;
        // ---------------------------
        Rectangle _rectangle3 = new Rectangle();
        _uIElementCollection2.Add(_rectangle3);
        Canvas.SetLeft(_rectangle3, 0D);
        Canvas.SetTop(_rectangle3, 0D);
        _rectangle3.Height = 40D;
        _rectangle3.Width = 100D;
        _rectangle3.StrokeThickness = 0.5D;
        _rectangle3.Stroke = Brushes.Gray;
        _rectangle3.RadiusX = 5D;
        _rectangle3.RadiusY = 5D;
        // ---------------------------
        LinearProcessBrush = new LinearGradientBrush();
        PointConverter _pointConverter = new PointConverter();
        LinearProcessBrush.StartPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, " 0.5,1")));
        LinearProcessBrush.EndPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, "1,0")));
        LinearProcessBrush.SpreadMethod = GradientSpreadMethod.Pad;
        // ---------------------------
        GradientStopCollection _gradientStopCollection4 = LinearProcessBrush.GradientStops;
        // ---------------------------
        GradientStop _gradientStop5 = new GradientStop();
        _gradientStop5.Color = Colors.Gray;
        _gradientStop5.Offset = 0D;
        _gradientStopCollection4.Add(_gradientStop5);
        // ---------------------------
        GradientStop _gradientStop6 = new GradientStop();
        _gradientStop6.Color = Colors.White;
        _gradientStop6.Offset = 0.5D;
        _gradientStopCollection4.Add(_gradientStop6);
        _rectangle3.Fill = LinearProcessBrush;
        // ---------------------------
        Rectangle _rectangle7 = new Rectangle();
        _uIElementCollection2.Add(_rectangle7);
        Canvas.SetLeft(_rectangle7, 2D);
        Canvas.SetTop(_rectangle7, 2D);
        _rectangle7.Height = 36D;
        _rectangle7.Width = 96D;
        _rectangle7.StrokeThickness = 0.5D;
        _rectangle7.Stroke = Brushes.DarkGray;
        _rectangle7.RadiusX = 3.5D;
        _rectangle7.RadiusY = 3.5D;
        // ---------------------------
        LinearProcessBrush2 = new LinearGradientBrush();
        LinearProcessBrush2.StartPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, " 0,0")));
        LinearProcessBrush2.EndPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, "0,1")));
        LinearProcessBrush2.SpreadMethod = GradientSpreadMethod.Pad;
        // ---------------------------
        GradientStopCollection _gradientStopCollection8 = LinearProcessBrush2.GradientStops;
        // ---------------------------
        GradientStop _gradientStop9 = new GradientStop();
        _gradientStop9.Color = Colors.Gray;
        _gradientStop9.Offset = 0D;
        _gradientStopCollection8.Add(_gradientStop9);
        // ---------------------------
        GradientStop _gradientStop10 = new GradientStop();
        _gradientStop10.Color = Colors.White;
        _gradientStop10.Offset = 0.5D;
        _gradientStopCollection8.Add(_gradientStop10);
        _rectangle7.Fill = LinearProcessBrush2;
        // ---------------------------
        Rectangle _rectangle11 = new Rectangle();
        _uIElementCollection2.Add(_rectangle11);
        Canvas.SetLeft(_rectangle11, 5D);
        Canvas.SetTop(_rectangle11, 5D);
        _rectangle11.Height = 30D;
        _rectangle11.Width = 90D;
        _rectangle11.StrokeThickness = 0.5D;
        _rectangle11.Stroke = Brushes.Black;
        _rectangle11.RadiusX = 3D;
        _rectangle11.RadiusY = 3D;
        // ---------------------------
        SolidColorBrush _solidColorBrush12 = new SolidColorBrush();
        _solidColorBrush12.Color = Colors.DarkGreen;
        _rectangle11.Fill = _solidColorBrush12;
        // ---------------------------
        Label _label13 = new Label();
        _uIElementCollection2.Add(_label13);
        _label13.Height = 30D;
        _label13.Width = 90D;
        _label13.Content = "Start";
        Canvas.SetLeft(_label13, 5D);
        Canvas.SetTop(_label13, 5D);
        _label13.FontSize = 16D;
        _label13.Foreground = Brushes.White;
        _label13.HorizontalContentAlignment = HorizontalAlignment.Center;
        _label13.VerticalContentAlignment = VerticalAlignment.Center;
    }
}
    public partial class HMIButton3 : System.Windows.Controls.UserControl {
    
    private System.Globalization.CultureInfo EnglishCultureInfo = new System.Globalization.CultureInfo("en-us", false);

    public System.Windows.Media.LinearGradientBrush LinearProcessBrush;

    public System.Windows.Media.LinearGradientBrush LinearProcessBrush2;

    public HMIButton3() {
        // ---------------------------
        Viewbox _viewbox0 = new Viewbox();
        this.Content = _viewbox0;
        // ---------------------------
        Canvas _canvas1 = new Canvas();
        _viewbox0.Child = _canvas1;
        _canvas1.Height = 40D;
        _canvas1.Width = 100D;
        // ---------------------------
        UIElementCollection _uIElementCollection2 = _canvas1.Children;
        // ---------------------------
        Rectangle _rectangle3 = new Rectangle();
        _uIElementCollection2.Add(_rectangle3);
        Canvas.SetLeft(_rectangle3, 0D);
    Canvas.SetTop(_rectangle3, 0D);
    _rectangle3.Height = 40D;
    _rectangle3.Width = 100D;
    _rectangle3.StrokeThickness = 0.5D;
    _rectangle3.Stroke = Brushes.Gray;
    // ---------------------------
    LinearProcessBrush = new LinearGradientBrush();
    PointConverter _pointConverter = new PointConverter();
    LinearProcessBrush.StartPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, " 0.5,1")));
        LinearProcessBrush.EndPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, "1,0")));
        LinearProcessBrush.SpreadMethod = GradientSpreadMethod.Pad;
        // ---------------------------
        GradientStopCollection _gradientStopCollection4 = LinearProcessBrush.GradientStops;
    // ---------------------------
    GradientStop _gradientStop5 = new GradientStop();
    _gradientStop5.Color = Colors.Gray;
        _gradientStop5.Offset = 0D;
        _gradientStopCollection4.Add(_gradientStop5);
        // ---------------------------
        GradientStop _gradientStop6 = new GradientStop();
    _gradientStop6.Color = Colors.White;
        _gradientStop6.Offset = 0.5D;
        _gradientStopCollection4.Add(_gradientStop6);
        _rectangle3.Fill = LinearProcessBrush;
        // ---------------------------
        Rectangle _rectangle7 = new Rectangle();
    _uIElementCollection2.Add(_rectangle7);
        Canvas.SetLeft(_rectangle7, 2D);
        Canvas.SetTop(_rectangle7, 2D);
        _rectangle7.Height = 36D;
        _rectangle7.Width = 96D;
        _rectangle7.StrokeThickness = 0.5D;
        _rectangle7.Stroke = Brushes.DarkGray;
        // ---------------------------
        LinearProcessBrush2 = new LinearGradientBrush();
    LinearProcessBrush2.StartPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, " 0,0")));
        LinearProcessBrush2.EndPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, "0,1")));
        LinearProcessBrush2.SpreadMethod = GradientSpreadMethod.Pad;
        // ---------------------------
        GradientStopCollection _gradientStopCollection8 = LinearProcessBrush2.GradientStops;
    // ---------------------------
    GradientStop _gradientStop9 = new GradientStop();
    _gradientStop9.Color = Colors.Gray;
        _gradientStop9.Offset = 0D;
        _gradientStopCollection8.Add(_gradientStop9);
        // ---------------------------
        GradientStop _gradientStop10 = new GradientStop();
    _gradientStop10.Color = Colors.White;
        _gradientStop10.Offset = 0.5D;
        _gradientStopCollection8.Add(_gradientStop10);
        _rectangle7.Fill = LinearProcessBrush2;
        // ---------------------------
        Rectangle _rectangle11 = new Rectangle();
    _uIElementCollection2.Add(_rectangle11);
        Canvas.SetLeft(_rectangle11, 5D);
        Canvas.SetTop(_rectangle11, 5D);
        _rectangle11.Height = 30D;
        _rectangle11.Width = 90D;
        _rectangle11.StrokeThickness = 0.5D;
        _rectangle11.Stroke = Brushes.Black;
        // ---------------------------
        SolidColorBrush _solidColorBrush12 = new SolidColorBrush();
    ColorConverter _colorConverter = new ColorConverter();
    _solidColorBrush12.Color = ((Color)(_colorConverter.ConvertFrom(null, EnglishCultureInfo, "#FF640E00")));
        _rectangle11.Fill = _solidColorBrush12;
        // ---------------------------
        Label _label13 = new Label();
    _uIElementCollection2.Add(_label13);
        _label13.Height = 30D;
        _label13.Width = 90D;
        _label13.Content = "Start";
        Canvas.SetLeft(_label13, 5D);
        Canvas.SetTop(_label13, 5D);
        _label13.FontSize = 16D;
        _label13.Foreground = Brushes.White;
        _label13.HorizontalContentAlignment = HorizontalAlignment.Center;
        _label13.VerticalContentAlignment = VerticalAlignment.Center;
    }
}
    public partial class HMIButton4 : System.Windows.Controls.UserControl {
    
    private System.Globalization.CultureInfo EnglishCultureInfo = new System.Globalization.CultureInfo("en-us", false);

    public System.Windows.Media.LinearGradientBrush LinearProcessBrush;

    public System.Windows.Media.LinearGradientBrush LinearProcessBrush2;

    public HMIButton4() {
        // ---------------------------
        Viewbox _viewbox0 = new Viewbox();
        this.Content = _viewbox0;
        // ---------------------------
        Canvas _canvas1 = new Canvas();
        _viewbox0.Child = _canvas1;
        _canvas1.Height = 100D;
        _canvas1.Width = 100D;
        // ---------------------------
        UIElementCollection _uIElementCollection2 = _canvas1.Children;
        // ---------------------------
        Ellipse _ellipse3 = new Ellipse();
        _uIElementCollection2.Add(_ellipse3);
        Canvas.SetLeft(_ellipse3, 0D);
    Canvas.SetTop(_ellipse3, 0D);
    _ellipse3.Width = 100D;
    _ellipse3.Height = 100D;
    _ellipse3.StrokeThickness = 0.5D;
    _ellipse3.Stroke = Brushes.Gray;
    // ---------------------------
    LinearProcessBrush = new LinearGradientBrush();
    PointConverter _pointConverter = new PointConverter();
    LinearProcessBrush.StartPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, "0.5,1")));
        LinearProcessBrush.EndPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, "1,0")));
        LinearProcessBrush.SpreadMethod = GradientSpreadMethod.Pad;
        // ---------------------------
        GradientStopCollection _gradientStopCollection4 = LinearProcessBrush.GradientStops;
    // ---------------------------
    GradientStop _gradientStop5 = new GradientStop();
    ColorConverter _colorConverter = new ColorConverter();
    _gradientStop5.Color = ((Color)(_colorConverter.ConvertFrom(null, EnglishCultureInfo, "#FF071923")));
        _gradientStop5.Offset = 0D;
        _gradientStopCollection4.Add(_gradientStop5);
        // ---------------------------
        GradientStop _gradientStop6 = new GradientStop();
    _gradientStop6.Color = Colors.White;
        _gradientStop6.Offset = 1D;
        _gradientStopCollection4.Add(_gradientStop6);
        _ellipse3.Fill = LinearProcessBrush;
        // ---------------------------
        Ellipse _ellipse7 = new Ellipse();
    _uIElementCollection2.Add(_ellipse7);
        Canvas.SetLeft(_ellipse7, 7.25D);
        Canvas.SetTop(_ellipse7, 7.25D);
        _ellipse7.Width = 85D;
        _ellipse7.Height = 85D;
        _ellipse7.StrokeThickness = 0.5D;
        _ellipse7.Stroke = Brushes.Gray;
        // ---------------------------
        LinearProcessBrush2 = new LinearGradientBrush();
    LinearProcessBrush2.StartPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, "1,0.5")));
        LinearProcessBrush2.EndPoint = ((Point)(_pointConverter.ConvertFrom(null, EnglishCultureInfo, "0,1")));
        LinearProcessBrush2.SpreadMethod = GradientSpreadMethod.Pad;
        // ---------------------------
        GradientStopCollection _gradientStopCollection8 = LinearProcessBrush2.GradientStops;
    // ---------------------------
    GradientStop _gradientStop9 = new GradientStop();
    _gradientStop9.Color = ((Color)(_colorConverter.ConvertFrom(null, EnglishCultureInfo, "#FF071923")));
        _gradientStop9.Offset = 0D;
        _gradientStopCollection8.Add(_gradientStop9);
        // ---------------------------
        GradientStop _gradientStop10 = new GradientStop();
    _gradientStop10.Color = Colors.White;
        _gradientStop10.Offset = 1.2D;
        _gradientStopCollection8.Add(_gradientStop10);
        _ellipse7.Fill = LinearProcessBrush2;
        // ---------------------------
        Ellipse _ellipse11 = new Ellipse();
    _uIElementCollection2.Add(_ellipse11);
        Canvas.SetLeft(_ellipse11, 12.25D);
        Canvas.SetTop(_ellipse11, 12.25D);
        _ellipse11.Width = 75D;
        _ellipse11.Height = 75D;
        _ellipse11.StrokeThickness = 0.5D;
        _ellipse11.Stroke = Brushes.Black;
        // ---------------------------
        SolidColorBrush _solidColorBrush12 = new SolidColorBrush();
    _solidColorBrush12.Color = ((Color)(_colorConverter.ConvertFrom(null, EnglishCultureInfo, "#FF910E0E")));
        _ellipse11.Fill = _solidColorBrush12;
        // ---------------------------
        Label _label13 = new Label();
    _uIElementCollection2.Add(_label13);
        _label13.Height = 71D;
        _label13.Width = 60D;
        _label13.Content = "Start";
        Canvas.SetLeft(_label13, 19D);
        Canvas.SetTop(_label13, 14D);
        _label13.Foreground = Brushes.White;
        _label13.HorizontalContentAlignment = HorizontalAlignment.Center;
        _label13.VerticalContentAlignment = VerticalAlignment.Center;
    }
}


}
