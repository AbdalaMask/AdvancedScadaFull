using AdvancedScada.Common.Client;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace AdvancedScada.WPF.HMIControls.SevenSegment
{
    /// <summary>
    /// Interaction logic for HMISevenSegmentsStack.xaml
    /// </summary>
    public partial class HMISevenSegmentsStack : SegmentsStackBase
    { /// <summary>
      /// Stores chars from the splitted value string
      /// </summary>
        private ObservableCollection<CharItem> ValueChars;

        public HMISevenSegmentsStack()
        {
            InitializeComponent();

        }
        public override void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ValueChars = GetCharsArray();
            SegmentsArray.ItemsSource = ValueChars;
        }
        public static readonly DependencyProperty PLCAddressValueProperty = DependencyProperty.Register(
          "PLCAddressValue", typeof(string), typeof(HMISevenSegmentsStack), new PropertyMetadata("0"));
        [Category("HMI")]
        public string PLCAddressValue
        {
            get
            {
                return (string)base.GetValue(PLCAddressValueProperty);
            }
            set
            {
                base.SetValue(PLCAddressValueProperty, value);


            }
        }
        #region "Keypad popup for data entry"

        [Category("HMI")]
        public string PLCAddressKeypad
        {
            get { return (string)GetValue(PLCAddressKeypadProperty); }
            set { SetValue(PLCAddressKeypadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PLCAddressKeypad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PLCAddressKeypadProperty =
            DependencyProperty.Register("PLCAddressKeypad", typeof(string), typeof(HMISevenSegmentsStack), new PropertyMetadata(string.Empty));


        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            if (PLCAddressKeypad != null && (string.Compare(PLCAddressKeypad, string.Empty) != 0) & IsEnabled)
            {
                KeyPad.Converter.Keypad keypadWindow = new KeyPad.Converter.Keypad(this, null);
                if (keypadWindow.ShowDialog() == true)
                {
                    Utilities.Write(PLCAddressKeypad, keypadWindow.Result);
                }

            }
        }

        #endregion

        private void SegmentsStackBase_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //* When address is changed, re-subscribe to new address
                if (string.IsNullOrEmpty(PLCAddressValue) || string.IsNullOrWhiteSpace(PLCAddressValue) ||
                           Comm.LicenseHMI.IsInDesignMode) return;
                Binding binding = new Binding("Value");
                binding.Source = TagCollectionClient.Tags[PLCAddressValue];
                this.SetBinding(ValueProperty, binding);


            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
            }
        }

        private void DisplayError(string message)
        {

        }
    }
}
