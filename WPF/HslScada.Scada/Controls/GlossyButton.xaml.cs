using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace HslScada.Scada
{
    /// <summary>
    /// Interaction logic for GlossyButton.xaml
    /// </summary>
    public partial class GlossyButton : UserControl
    {
        public GlossyButton()
        {
            InitializeComponent();
        }
        #region DependencyProperty

        /// <summary>
        /// Color
        /// </summary>
        public static readonly DependencyProperty GlossyBorderBrushProperty = DependencyProperty.Register(
            "GlossyBorderBrushColor", typeof(GlossyBorderBrush), typeof(GlossyButton), new PropertyMetadata(GlossyBorderBrush.blueBorder));

        public static readonly DependencyProperty GlossyBackgroundProperty = DependencyProperty.Register(
           "GlossyBackgroundColor", typeof(GlossyBackground), typeof(GlossyButton), new PropertyMetadata(GlossyBackground.blueBackground));

        #endregion

        #region Public Properties

        /// <summary>
        /// Color
        /// </summary>
        [Category("HMI")]
        public GlossyBorderBrush GlossyBorderBrushColor
        {
            set { SetValue(GlossyBorderBrushProperty, value); }
            get { return (GlossyBorderBrush)GetValue(GlossyBorderBrushProperty); }
        }
        /// <summary>
        /// string
        /// </summary>
        [Category("HMI")]
        public GlossyBackground GlossyBackgroundColor
        {
            set { SetValue(GlossyBackgroundProperty, value); }
            get { return (GlossyBackground)GetValue(GlossyBackgroundProperty); }
        }
        #endregion
        public enum GlossyBorderBrush
        {
            redBorder, blueBorder, greenBorder, yellowBorder, blackBorder
        }
        public enum GlossyBackground
        {
            redBackground, blueBackground, greenBackground, yellowBackground, blackBackground
        }
    }
}
