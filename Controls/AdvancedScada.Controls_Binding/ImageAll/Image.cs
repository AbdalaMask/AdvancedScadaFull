using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml;
namespace AdvancedScada.Controls_Binding.ImageAll
{
    /// <summary>
    /// Image of a scheme
    /// <para>Изображение схемы</para>
    /// </summary>
    [TypeConverter(typeof(ImageConverter))]
    [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
    public class Image
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Image()
        {
            Name = "";
            Data = null;
        }


        /// <summary>
        /// Получить или установить наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получить или установить данные
        /// </summary>
        public byte[] Data { get; set; }


       
        /// <summary>
        /// Копировать объект
        /// </summary>
        public Image Copy()
        {
            return new Image()
            {
                Name = Name,
                Data = Data
            };
        }
    }
}
