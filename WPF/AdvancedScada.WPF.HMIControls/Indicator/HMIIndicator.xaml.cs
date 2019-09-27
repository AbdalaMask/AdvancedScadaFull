using AdvancedScada.DriverBase.Client;
using AdvancedScada.WPF.HMIControls.Comm;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AdvancedScada.WPF.HMIControls.Indicator
{
    /// <summary>
    /// Interaction logic for HMIIndicator.xaml
    /// </summary>
    public partial class HMIIndicator : UserControl
    {
        public HMIIndicator()
        {
            InitializeComponent();
        }
        #region Constructor
        protected event PropertyChangedCallback PropertyChanged = (sender, e) => { };


        #endregion

        #region DependencyProperty

        /// <summary>
        /// 指示灯控件的颜色依赖属性
        /// </summary>
        public static readonly DependencyProperty IndicatorColorProperty = DependencyProperty.Register(
            "IndicatorColor", typeof(Color), typeof(HMIIndicator), new PropertyMetadata(Colors.Gray));
        public static readonly DependencyProperty PLCAddressValueProperty = DependencyProperty.Register(
            "PLCAddressValue", typeof(string), typeof(HMIIndicator), new FrameworkPropertyMetadata("0"));


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
           "Value", typeof(bool), typeof(HMIIndicator), new PropertyMetadata(false, OnCurrentReadingChanged));


        private static void OnCurrentReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HMIIndicator Indicator2 = (HMIIndicator)d;
            if (Indicator2.Value) Indicator2.IndicatorColor = Colors.Green;
            else Indicator2.IndicatorColor = Colors.Gray;
            Indicator2.PropertyChanged(d, e);
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// 获取或设置指示灯控件的颜色
        /// </summary>
        public Color IndicatorColor
        {
            set { SetValue(IndicatorColorProperty, value); }
            get { return (Color)GetValue(IndicatorColorProperty); }
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
        [Category("HMI")]
        public bool Value
        {
            get
            {
                return (bool)base.GetValue(ValueProperty);
            }
            set
            {
                base.SetValue(ValueProperty, value);



            }
        }
        #endregion

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

        private void DisplayError(string message)
        {

        }
    }
}
