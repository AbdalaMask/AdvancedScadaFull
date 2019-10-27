using AdvancedScada.Common.Client;
using AdvancedScada.Controls_Binding.DialogEditor;
using AdvancedScada.Controls_Net45;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.Segment
{

    public class HMISegment7LED : DasNetLED.DAS_Net_Segment7LED
    {

        private string OriginalText;

        #region PLC Properties

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressText = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressText
        {
            get { return m_PLCAddressText; }
            set
            {
                if (m_PLCAddressText != value)
                {
                    m_PLCAddressText = value;
                    try
                    {
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressText) || string.IsNullOrWhiteSpace(m_PLCAddressText) ||
                            Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("Text", TagCollectionClient.Tags[m_PLCAddressValue], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressVisible
        {
            get { return m_PLCAddressVisible; }
            set
            {
                if (m_PLCAddressVisible != value)
                {
                    m_PLCAddressVisible = value;
                    try
                    {
                        // If Not String.IsNullOrEmpty(m_PLCAddressVisible) Then
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressVisible) ||
                            string.IsNullOrWhiteSpace(m_PLCAddressVisible) || Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("Visible", TagCollectionClient.Tags[m_PLCAddressVisible], "Value", true);
                        DataBindings.Add(bd);
                        //End If
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressValue = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressValue
        {
            get { return m_PLCAddressValue; }
            set
            {
                if (m_PLCAddressValue != value)
                {
                    m_PLCAddressValue = value;
                    try
                    {
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressValue) || string.IsNullOrWhiteSpace(m_PLCAddressValue) ||
                            Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("Value", TagCollectionClient.Tags[m_PLCAddressValue], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Write Data To
        //*****************************************
        private string m_PLCAddressKeypad = string.Empty;

        [Category("PLC Properties")]
        public string PLCAddressKeypad
        {
            get { return m_PLCAddressKeypad; }
            set
            {
                if (m_PLCAddressKeypad != value) m_PLCAddressKeypad = value;
            }
        }


        #endregion


        #region "Error Display"

        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }

        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private Timer ErrorDisplayTime;
        private readonly object ErrorLock = new object();

        private void DisplayError(string ErrorMessage)
        {
            if (!SuppressErrorDisplay)
            {
                //* Create the error display timer
                if (ErrorDisplayTime == null)
                {
                    ErrorDisplayTime = new Timer();
                    ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                    ErrorDisplayTime.Interval = 5000;
                }

                //* Save the text to return to
                lock (ErrorLock)
                {
                    if (!ErrorDisplayTime.Enabled)
                    {
                        ErrorDisplayTime.Enabled = true;
                        OriginalText = Text;
                        Text = ErrorMessage;
                        Utilities.DisplayError(this, ErrorMessage);
                    }
                }
            }
        }


        //**************************************************************************************
        //* Return the text back to its original after displaying the error for a few seconds.
        //**************************************************************************************
        private void ErrorDisplay_Tick(object sender, EventArgs e)
        {
            //UpdateText()
            lock (ErrorLock)
            {
                Text = OriginalText;
                //If ErrorDisplayTime IsNot Nothing Then
                ErrorDisplayTime.Enabled = false;
                // ErrorIsDisplayed = False
            }

            //RemoveHandler ErrorDisplayTime.Tick, AddressOf ErrorDisplay_Tick
            //ErrorDisplayTime.Dispose()
            //ErrorDisplayTime = Nothing
            //End If
        }

        #endregion

        #region "Keypad popup for data entry"

        public string KeypadText { get; set; }

        private Font m_KeypadFont = new Font("Arial", 10);

        public Font KeypadFont
        {
            get { return m_KeypadFont; }
            set { m_KeypadFont = value; }
        }

        private Color m_KeypadForeColor = Color.WhiteSmoke;

        public Color KeypadFontColor
        {
            get { return m_KeypadForeColor; }
            set { m_KeypadForeColor = value; }
        }

        private int m_KeypadWidth = 400;

        public int KeypadWidth
        {
            get { return m_KeypadWidth; }
            set { m_KeypadWidth = value; }
        }

        //* 29-JAN-13

        public double KeypadMinValue { get; set; }

        public double KeypadMaxValue { get; set; }

        private double m_KeypadScaleFactor = 1;

        [DefaultValue(1)]
        public double KeypadScaleFactor
        {
            get { return m_KeypadScaleFactor; }
            set { m_KeypadScaleFactor = value; }
        }

        public bool KeypadAlphaNumeric { get; set; }

        public bool KeypadShowCurrentValue { get; set; }

      

        private IKeyboard KeypadPopUp;

        private void KeypadPopUp_ButtonClick(object sender, KeypadEventArgs e)
        {
            if (e.Key == "Quit")
            {
                KeypadPopUp.Visible = false;
            }
            else if (e.Key == "Enter")
            {
                if (KeypadPopUp.Value != null && string.Compare(KeypadPopUp.Value, string.Empty) != 0)
                {
                    //* 29-JAN-13 - Validate value if a Min/Max was specified
                    try
                    {
                        if (KeypadMaxValue != KeypadMinValue)
                            if ((Convert.ToDouble(KeypadPopUp.Value) < KeypadMinValue) |
                                (Convert.ToDouble(KeypadPopUp.Value) > KeypadMaxValue))
                            {
                                MessageBox.Show("Value must be >" + KeypadMinValue + " and <" + KeypadMaxValue);
                                return;
                            }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to validate value. " + ex.Message);
                        return;
                    }

                    try
                    {
                        //* 29-JAN-13 - reduced code and checked for divide by 0
                        if ((KeypadScaleFactor == 1) | (KeypadScaleFactor == 0))
                            Utilities.Write(m_PLCAddressKeypad, KeypadPopUp.Value);
                        else
                            Utilities.Write(m_PLCAddressKeypad,
                                (Convert.ToDouble(KeypadPopUp.Value) / m_KeypadScaleFactor).ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to write value - " + ex.Message);
                    }
                }

                KeypadPopUp.Visible = false;
            }
        }

        //***********************************************************
        //* If label is clicked, pop up a keypad for data entry.
        //* Limit this to the left-click of the mouse only.
        //***********************************************************
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (e.GetType() == typeof(MouseEventArgs))
            {
                var mea = (MouseEventArgs)e;
                if (mea.Button.ToString() == "Left")
                    if (m_PLCAddressKeypad != null && (string.Compare(m_PLCAddressKeypad, string.Empty) != 0) & Enabled)
                        ActivateKeypad();
            }
        }

        public void ActivateKeypad()
        {
            if (KeypadPopUp == null)
            {
                if (KeypadAlphaNumeric)
                    KeypadPopUp = new AlphaKeyboard_v3();
                else
                    KeypadPopUp = new Keypad_v3(m_KeypadWidth);
                KeypadPopUp.ButtonClick += KeypadPopUp_ButtonClick;
                KeypadPopUp.StartPosition = FormStartPosition.CenterScreen;
                KeypadPopUp.TopMost = true;
            }

            //***************************
            //*Set the font and forecolor
            //****************************
            if (m_KeypadFont != null)
                KeypadPopUp.Font = m_KeypadFont;
            KeypadPopUp.ForeColor = m_KeypadForeColor;

            KeypadPopUp.Text = KeypadText;
            if (KeypadShowCurrentValue)
                try
                {
                    try
                    {
                        var ScaledValue = Convert.ToDouble(Value);
                        KeypadPopUp.Value = ScaledValue.ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Failed to Scale current value of " + Value);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to read current value of " + m_PLCAddressKeypad);
                }
            else
                KeypadPopUp.Value = string.Empty;

            KeypadPopUp.Visible = true;
        }

        #endregion

    }

}
