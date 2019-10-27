using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AdvancedScada.WPF.HMIControls.KeyPad.Converter
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class Keypad : Window, INotifyPropertyChanged
    {
        #region Public Properties

        private string _result;
        public string Result
        {
            get { return _result; }
            private set { _result = value; this.OnPropertyChanged("Result"); }
        }

        #endregion

        public Keypad(Control owner, Window wndOwner)
        {
            InitializeComponent();
            this.Owner = wndOwner;
            this.DataContext = this;
            Result = string.Empty;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.CommandParameter.ToString())
            {
                case "ESC":
                    this.DialogResult = false;
                    break;

                case "RETURN":
                    this.DialogResult = true;
                    break;

                case "BACK":
                    if (Result.Length > 0)
                        Result = Result.Remove(Result.Length - 1);
                    break;

                default:
                    Result += button.Content.ToString();
                    break;
            }
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion



    }
}
