using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using CM = System.ComponentModel;
namespace AdvancedScada.Controls_Binding.ImageAll
{
    [Serializable]
    public class StaticPicture : PictureBox
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public StaticPicture()
        {
           
            ImageName = "";
            ImageStretch = ImageStretches.None;
            
           
        }


        /// <summary>
        /// Получить или установить наименование изображения
        /// </summary>
        #region Attributes
        [DisplayName("ImageName")]
        [Description("The image from the collection of scheme images.")]
        [CM.TypeConverter(typeof(ImageConverter)), CM.Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        [CM.DefaultValue("")]
        #endregion
        public string ImageName { get; set; }

        /// <summary>
        /// Получить или установить растяжение изображения
        /// </summary>
        #region Attributes
        [DisplayName("Image stretch")]
        [Description("Stretch the image.")]
        [CM.DefaultValue(ImageStretches.None)]
        #endregion
        public ImageStretches ImageStretch { get; set; }


       
       
    }
}
