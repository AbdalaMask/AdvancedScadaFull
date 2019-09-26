using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdvancedScada.WPF.HMIControls.PilotLight
{
    /// <summary>
    /// Interaction logic for HMIMushroomButton.xaml
    /// </summary>
    public partial class HMIMushroomButton : UserControl
    {
        public HMIMushroomButton()
        {
            InitializeComponent();
        }

        #region DependencyProperty

        /// <summary>
        /// Color
        /// </summary>

        public static readonly DependencyProperty PilotLightSmallTextProperty = DependencyProperty.Register(
           "PilotLightSmallText", typeof(string), typeof(HMIMushroomButton), new PropertyMetadata("Name"));

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
