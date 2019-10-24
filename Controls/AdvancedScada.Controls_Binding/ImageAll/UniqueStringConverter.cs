using System.ComponentModel;
using System.Globalization;

namespace AdvancedScada.Controls_Binding.ImageAll
{
    /// <summary>
    /// Converter of strings for PropertyGrid which checks value uniqueness
    /// <para>Преобразователь строк для PropertyGrid, который проверяет уникальность значения</para>
    /// </summary>
    public class UniqueStringConverter : StringConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (context.Instance is IUniqueItem)
            {
                string key = (value ?? "").ToString().Trim();

                if (((IUniqueItem)context.Instance).KeyIsUnique(key))
                    return key;
                
                    
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
