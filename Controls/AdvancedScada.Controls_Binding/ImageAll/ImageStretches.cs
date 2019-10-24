using System.ComponentModel;
using CM = System.ComponentModel;
namespace AdvancedScada.Controls_Binding.ImageAll
{
    /// <summary>
    /// Ways of stretching image
    /// <para>Способы растяжения изображения</para>
    /// </summary>
    [CM.TypeConverter(typeof(EnumConverter))]
    public enum ImageStretches
    {
        /// <summary>
        /// Не задано
        /// </summary>
        [Description("None")]
        None,

        /// <summary>
        /// Заполнить заданный размер
        /// </summary>
        [Description("Fill")]
        Fill,

        /// <summary>
        /// Растянуть пропорционально в рамках заданного размера
        /// </summary>
        [Description("Zoom")]
        Zoom
    }
}
