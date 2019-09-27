using System.Collections.ObjectModel;
using System.Windows;

namespace AdvancedScada.WPF.HMIControls.SevenSegment
{
    /// <summary>
    /// Interaction logic for HMISixteenSegmentsStack.xaml
    /// </summary>
    public partial class HMISixteenSegmentsStack : SegmentsStackBase
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
