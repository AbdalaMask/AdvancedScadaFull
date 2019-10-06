using AdvancedScada.DriverBase.Client;
using AdvancedScada.WPF.HMIControls.Comm;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AdvancedScada.WPF.HMIControls.HslControl.TankAll
{
    /// <summary>
    /// Interaction logic for HMIBottle.xaml
    /// </summary>
    public partial class HMIBottle : UserControl
    {
        #region Constructor
        protected event PropertyChangedCallback PropertyChanged = (sender, e) => { };


        #endregion
        public HMIBottle()
        {

            InitializeComponent();

        }
        public static readonly DependencyProperty PLCAddressValueProperty = DependencyProperty.Register(
        "PLCAddressValue", typeof(string), typeof(HMIBottle), new PropertyMetadata("0"));
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
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        "Value", typeof(string), typeof(HMIBottle), new PropertyMetadata("50", new PropertyChangedCallback(MoveSpeedDependencyPropertyChanged)));

        private static void MoveSpeedDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        [Category("HMI")]
        public string Value
        {
            get
            {
                return (string)base.GetValue(ValueProperty);
            }
            set
            {
                base.SetValue(ValueProperty, value);



            }
        }


        private void UserControl_Initialized(object sender, EventArgs e)
        {

        }



        private void DisplayError(string message)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                //* When address is changed, re-subscribe to new address
                if (string.IsNullOrEmpty(PLCAddressValue) || string.IsNullOrWhiteSpace(PLCAddressValue) ||
                           LicenseHMI.IsInDesignMode) return;
                Binding binding = new Binding("Value");
                binding.Source = TagCollectionClient.Tags[PLCAddressValue];
                this.SetBinding(ValueProperty, binding);


            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
            }
        }
    }
}
