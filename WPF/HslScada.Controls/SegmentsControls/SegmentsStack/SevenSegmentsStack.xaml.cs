using HslScada.Controls;
using KeyPad;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SegmentsControls
{
    /// <summary>
    /// Interaction logic for SevenSegmentsStack.xaml
    /// </summary>
    public partial class SevenSegmentsStack : SegmentsStackBase
    {

        /// <summary>
        /// Stores chars from the splitted value string
        /// </summary>
        private ObservableCollection<CharItem> ValueChars;

        public SevenSegmentsStack()
        {
            InitializeComponent();
        }



        public override void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ValueChars = GetCharsArray();
            SegmentsArray.ItemsSource = ValueChars;
        }

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
            DependencyProperty.Register("PLCAddressKeypad", typeof(string), typeof(SevenSegmentsStack), new PropertyMetadata(string.Empty));


        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            if (PLCAddressKeypad != null && (string.Compare(PLCAddressKeypad, string.Empty) != 0) & IsEnabled)
            {
                Keypad keypadWindow = new Keypad(this, null);
                if (keypadWindow.ShowDialog() == true)
                {
                    Utilities.Write(PLCAddressKeypad, keypadWindow.Result);
                }

            }
        }

        #endregion
    }
}
