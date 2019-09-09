using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HslScada.Controls.SelectorSwitch
{
    public class ColorLightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;
            int degree = 40;
            if (parameter != null)
            {
                degree = System.Convert.ToInt32(parameter);
            }
            return Color.FromRgb((byte)(color.R + (255 - color.R) * degree / 100), (byte)(color.G + (255 - color.G) * degree / 100), (byte)(color.B + (255 - color.B) * degree / 100));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

}
