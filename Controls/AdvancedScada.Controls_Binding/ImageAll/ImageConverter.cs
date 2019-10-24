using System;
using System.ComponentModel;
using System.Globalization;
namespace AdvancedScada.Controls_Binding.ImageAll
{
    /// <summary>
    /// Converter of images for PropertyGrid
    /// <para>Преобразователь изображений для PropertyGrid</para>
    /// </summary>
    public class ImageConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string imageName = (value ?? "").ToString();
                return string.IsNullOrEmpty(imageName) ? "" : imageName;
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
