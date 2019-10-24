using System;
using System.ComponentModel;
using System.Drawing.Design;
using CM = System.ComponentModel;
namespace AdvancedScada.Controls_Binding.ImageAll
{
    /// <summary>
    /// Scheme component that represents dynamic picture
    /// <para>Компонент схемы, представляющий динамический рисунок</para>
    /// </summary>
    [Serializable]
    public class DynamicPicture : StaticPicture
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DynamicPicture()
            : base()
        {
          
            ImageOnHoverName = "";
            
        }


      
        /// <summary>
        /// Получить или установить наименование изображения, отображаемого при наведении указателя мыши
        /// </summary>
        #region Attributes
        [DisplayName("Image on hover")]
        [Description("The image shown when user rests the pointer on the component.")]
        [CM.TypeConverter(typeof(ImageConverter)), CM.Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        [CM.DefaultValue("")]
        #endregion
        public string ImageOnHoverName { get; set; }



       

    }
}
