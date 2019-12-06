using AdvancedScada.Common;
using AdvancedScada.Common.Client;
using AdvancedScada.Controls_Binding.DialogEditor;
using AdvancedScada.Controls_Net45;
using HslControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.HslControl.Segment
{
    public class HMILedDisplay : HslLedDisplay, IPropertiesControls
    {
        public event EventHandler ValueChanged;
        public HMILedDisplay()
        {
            BackColor = System.Drawing.Color.Transparent;
            DisplayBackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            DisplayNumber = 8;
            DisplayText = "123.45";
            ForeColor = System.Drawing.Color.Black;
            LedNumberSize = 2;
            Margin = new System.Windows.Forms.Padding(3, 11, 3, 11);
        }

        #region PLC Related Properties

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressText = string.Empty;


        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;



        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressValue = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressText
        {
            get => m_PLCAddressText;
            set
            {
                if (m_PLCAddressText != value)
                {
                    m_PLCAddressText = value;

                    try
                    {
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressText) || string.IsNullOrWhiteSpace(m_PLCAddressText) ||
                                 Licenses.LicenseManager.IsInDesignMode)
                        {
                            return;
                        }

                        Binding bd = new Binding("Text", TagCollectionClient.Tags[m_PLCAddressText], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressVisible
        {
            get => m_PLCAddressVisible;
            set
            {
                if (m_PLCAddressVisible != value)
                {
                    m_PLCAddressVisible = value;

                    try
                    {
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressVisible) || string.IsNullOrWhiteSpace(m_PLCAddressVisible) ||
                                 Licenses.LicenseManager.IsInDesignMode)
                        {
                            return;
                        }

                        Binding bd = new Binding("Visible", TagCollectionClient.Tags[m_PLCAddressVisible], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressValue
        {
            get => m_PLCAddressValue;
            set
            {
                if (m_PLCAddressValue != value)
                {
                    m_PLCAddressValue = value;

                    try
                    {
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressValue) || string.IsNullOrWhiteSpace(m_PLCAddressValue) ||
                                 Licenses.LicenseManager.IsInDesignMode)
                        {
                            return;
                        }

                        Binding bd = new Binding("Value", TagCollectionClient.Tags[m_PLCAddressValue], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }
        private string m_Value;

        public string Value
        {
            get => m_Value;
            set
            {
                if (value != m_Value)
                {
                    if (value != null)
                    {
                        m_Value = value;
                        DisplayText = m_Value;
                        OnvalueChanged(EventArgs.Empty);
                    }
                    else
                    {
                        //* version 3.99f
                        if (!string.IsNullOrEmpty(m_Value))
                        {
                            OnvalueChanged(EventArgs.Empty);
                        }

                        m_Value = string.Empty;
                        base.Text = string.Empty;
                    }

                    //* Be sure error handler doesn't revert back to an incorrect text
                    OriginalText = base.Text;
                }
            }
        }
        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }
        protected virtual void OnvalueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }
        #endregion

        #region Error Display
        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private System.Windows.Forms.Timer ErrorDisplayTime;
        public void DisplayError(string ErrorMessage)
        {
            if (!SuppressErrorDisplay)
            {
                if (ErrorDisplayTime == null)
                {
                    ErrorDisplayTime = new System.Windows.Forms.Timer();
                    ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                    ErrorDisplayTime.Interval = 5000;
                }

                //* Save the text to return to
                if (!ErrorDisplayTime.Enabled)
                {
                    OriginalText = Text;
                }

                ErrorDisplayTime.Enabled = true;
                Utilities.DisplayError(this, ErrorMessage);
                Text = ErrorMessage;
            }
        }

        private string OriginalText;
        //**************************************************************************************
        //* Return the text back to its original after displaying the error for a few seconds.
        //**************************************************************************************
        private void ErrorDisplay_Tick(object sender, System.EventArgs e)
        {
            Text = OriginalText;

            if (ErrorDisplayTime != null)
            {
                ErrorDisplayTime.Enabled = false;
                ErrorDisplayTime.Dispose();
                ErrorDisplayTime = null;
            }
        }
        #endregion
        #region "Keypad popup for data entry"

        private Keypad_v3 KeypadPopUp;

        //*****************************************
        //* Property - Address in PLC to Write Data To
        //*****************************************
        private string m_PLCAddressKeypad = string.Empty;

        [Category("PLC Properties")]
        public string PLCAddressKeypad
        {
            get => m_PLCAddressKeypad;
            set
            {
                if (m_PLCAddressKeypad != value)
                {
                    m_PLCAddressKeypad = value;
                }
            }
        }

        public string KeypadText { get; set; }

        private Color m_KeypadFontColor = Color.WhiteSmoke;

        public Color KeypadFontColor
        {
            get => m_KeypadFontColor;
            set => m_KeypadFontColor = value;
        }

        private int m_KeypadWidth = 300;

        public int KeypadWidth
        {
            get => m_KeypadWidth;
            set => m_KeypadWidth = value;
        }

        private double m_KeypadScaleFactor = 1;

        public double KeypadScaleFactor
        {
            get => m_KeypadScaleFactor;
            set => m_KeypadScaleFactor = value;
        }

        public double KeypadMinValue { get; set; }

        public double KeypadMaxValue { get; set; }
        public string PLCAddressClick { get; set; }
        public string PLCAddressEnabled { get; set; }

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
                    try
                    {
                        if (KeypadMaxValue != KeypadMinValue)
                        {
                            if ((Convert.ToDouble(KeypadPopUp.Value) < KeypadMinValue) |
                                (Convert.ToDouble(KeypadPopUp.Value) > KeypadMaxValue))
                            {
                                DisplayError("Value must be >" + KeypadMinValue + " and <" + KeypadMaxValue);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayError("Failed to validate value. " + ex.Message);
                        return;
                    }

                    try
                    {
                        if ((KeypadScaleFactor == 1) | (KeypadScaleFactor == 0))
                        {
                            Utilities.Write(m_PLCAddressKeypad, KeypadPopUp.Value);
                        }
                        else
                        {
                            double v = Convert.ToDouble(KeypadPopUp.Value);
                            double z = v / m_KeypadScaleFactor;
                            Utilities.Write(m_PLCAddressKeypad,
                                (Convert.ToDouble(KeypadPopUp.Value) / m_KeypadScaleFactor).ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayError("Failed to write value. " + ex.Message);
                    }
                }

                KeypadPopUp.Visible = false;
            }
        }

        //***********************************************************
        //* If labeled is clicked, pop up a keypad for data entry
        //***********************************************************
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (m_PLCAddressKeypad != null && (string.Compare(m_PLCAddressKeypad, string.Empty) != 0) & Enabled)
            {
                if (KeypadPopUp == null)
                {
                    KeypadPopUp = new Keypad_v3(m_KeypadWidth);
                    KeypadPopUp.ButtonClick += KeypadPopUp_ButtonClick;
                }

                KeypadPopUp.Text = KeypadText;
                KeypadPopUp.ForeColor = m_KeypadFontColor;
                KeypadPopUp.Value = string.Empty;
                KeypadPopUp.StartPosition = FormStartPosition.CenterScreen;
                KeypadPopUp.TopMost = true;
                KeypadPopUp.Show();
            }
        }

        #endregion
    }
}
