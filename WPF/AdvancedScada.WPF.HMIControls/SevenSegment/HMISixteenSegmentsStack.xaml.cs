using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AdvancedScada.WPF.HMIControls.SevenSegment
{
    /// <summary>
    /// Interaction logic for HMISixteenSegmentsStack.xaml
    /// </summary>
    public partial class HMISixteenSegmentsStack :  SegmentsStackBase
    { /// <summary>
      /// Stores chars from the splitted value string
      /// </summary>
        private ObservableCollection<CharItem> ValueChars;

        public HMISixteenSegmentsStack()
        {
            InitializeComponent();
            VertSegDivider = defVertDividerSixteen;
            HorizSegDivider = defHorizDividerSixteen;
        }
        public override void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ValueChars = GetCharsArray();
            SegmentsArray.ItemsSource = ValueChars;
        }
    }
}
