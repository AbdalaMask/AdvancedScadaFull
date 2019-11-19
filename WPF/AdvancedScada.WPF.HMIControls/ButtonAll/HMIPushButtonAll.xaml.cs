using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace AdvancedScada.WPF.HMIControls.ButtonAll
{
    /// <summary>
    /// Interaction logic for HMIPushButtonAll.xaml
    /// </summary>
    public partial class HMIPushButtonAll : UserControl
    {
        public HMIPushButtonAll()
        {
            InitializeComponent();
        }

        [Category("HMI")]
        public Color PushButtonColor
        {
            get { return (Color)GetValue(PushButtonColorProperty); }
            set { SetValue(PushButtonColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PushButtonColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PushButtonColorProperty =
            DependencyProperty.Register("PushButtonColor", typeof(Color), typeof(HMIPushButtonAll), new PropertyMetadata(Colors.Red));

        [Category("HMI")]
        public string PushButtonText
        {
            get { return (string )GetValue(PushButtonTextProperty); }
            set { SetValue(PushButtonTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PushButtonText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PushButtonTextProperty =
            DependencyProperty.Register("PushButtonText", typeof(string ), typeof(HMIPushButtonAll), new PropertyMetadata("Start"));
 

        private void HMIPushButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
          ep.StrokeThickness = 8;
        }

       

        private void HMIPushButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ep.StrokeThickness = 0.5;
        }
    }
}
