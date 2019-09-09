using AdvancedScada.DriverBase.Client;
using HslScada.Controls;
using KeyPad;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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

        private void SegmentsStackBase_Loaded(object sender, RoutedEventArgs e)
        {
            Boolean isInWpfDesignerMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            Boolean isInFormsDesignerMode = (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");

            if (isInWpfDesignerMode || isInFormsDesignerMode)
            {
                // is in any designer mode
            }
            else
            {
                // not in designer mode
                //* When address is changed, re-subscribe to new address
                if (string.IsNullOrEmpty(PLCAddressValue) || string.IsNullOrWhiteSpace(PLCAddressValue) ||
                    HslScada.Controls.Licenses.LicenseManager.IsInDesignMode) return;
                Binding binding = new Binding("Value");
                binding.Source = TagCollectionClient.Tags[PLCAddressValue];
                this.SetBinding(ValueProperty, binding);
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
