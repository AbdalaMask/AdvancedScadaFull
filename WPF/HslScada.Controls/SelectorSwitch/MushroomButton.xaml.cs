using System.Windows;
using System.Windows.Controls;

namespace HslScada.Controls.SelectorSwitch
{
    /// <summary>
    /// Interaction logic for MushroomButton.xaml
    /// </summary>
    public partial class MushroomButton : UserControl
    {
        public MushroomButton()
        {
            InitializeComponent();
        }

        #region DependencyProperty

        /// <summary>
        /// Color
        /// </summary>

        public static readonly DependencyProperty PilotLightSmallTextProperty = DependencyProperty.Register(
           "PilotLightSmallText", typeof(string), typeof(MushroomButton), new PropertyMetadata("Name"));

        #endregion

        #region Public Properties


        /// <summary>
        /// string
        /// </summary>
        public string PilotLightSmallText
        {
            set { SetValue(PilotLightSmallTextProperty, value); }
            get { return (string)GetValue(PilotLightSmallTextProperty); }
        }
        #endregion
    }
}
