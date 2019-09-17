using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.Display;
using AdvancedScada.Controls.Drivers;
using AdvancedScada.Controls.Drivers;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls_Net45;
using AdvancedScada.DriverBase.Comm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AdvancedScada.Controls.AHMI.Display
{
    public class HMIAnalogValueDisplay : System.Windows.Forms.Label
    {
        public event EventHandler ValueChanged;
        public event EventHandler ValueLimitUpperChanged;
        public event EventHandler ValueLimitLowerChanged;



        #region Constructor
        public HMIAnalogValueDisplay() : base()
        {

            Value = "0000";

            if (base.ForeColor == System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ControlText) || ForeColor == Color.FromArgb(0, 0, 0))
            {
                ForeColor = System.Drawing.Color.WhiteSmoke;
            }
        }

        //****************************************************************
        //* UserControl overrides dispose to clean up the component list.
        //****************************************************************
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (SubScriptions != null)
                    {
                        SubScriptions.Dispose();
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        #endregion

        #region Basic Properties
        [System.ComponentModel.Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        private Color m_ForeColorInLimits = Color.White;
        public Color ForeColorInLimits
        {
            get
            {
                return m_ForeColorInLimits;
            }
            set
            {
                m_ForeColorInLimits = value;
            }
        }


        private Color m_ForeColorOverLimit = Color.Red;
        public Color ForeColorOverLimit
        {
            get
            {
                return m_ForeColorOverLimit;
            }
            set
            {
                m_ForeColorOverLimit = value;
            }
        }


        private Color m_ForeColorUnderLimit = Color.Yellow;
        public Color ForeColorUnderLimit
        {
            get
            {
                return m_ForeColorUnderLimit;
            }
            set
            {
                m_ForeColorUnderLimit = value;
            }
        }


        //******************************************************************************************
        //* Use the base control's text property and make it visible as a property on the designer
        //******************************************************************************************
        private string m_Value;
        private double ValueAsDouble;
        public string Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                if (value != m_Value)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        m_Value = value;
                        //* V3.99y
                        double.TryParse(value, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowParentheses | System.Globalization.NumberStyles.AllowTrailingWhite, System.Globalization.NumberFormatInfo.CurrentInfo, out ValueAsDouble);
                        UpdateText();
                        OnValueChanged(EventArgs.Empty);
                    }
                    else
                    {
                        //* version 3.99f
                        if (!string.IsNullOrEmpty(m_Value))
                        {
                            OnValueChanged(EventArgs.Empty);
                        }
                        m_Value = string.Empty;
                        base.Text = string.Empty;
                    }
                    //* Be sure error handler doesn't revert back to an incorrect text
                    OriginalText = base.Text;
                    UpdateVisible();
                }
            }
        }

        //**********************************
        //* Prefix and suffixes to text
        //**********************************
        private string m_Prefix;
        public string ValuePrefix
        {
            get
            {
                return m_Prefix;
            }
            set
            {
                if (m_Prefix != value)
                {
                    m_Prefix = value;
                    UpdateText();
                }
            }
        }

        private string m_Suffix;
        public string ValueSuffix
        {
            get
            {
                return m_Suffix;
            }
            set
            {
                if (m_Suffix != value)
                {
                    m_Suffix = value;
                    UpdateText();
                }
            }
        }

        private double m_ValueLimitUpper = 999999;
        public double ValueLimitUpper
        {
            get
            {
                return m_ValueLimitUpper;
            }
            set
            {
                if (m_ValueLimitUpper != value)
                {
                    m_ValueLimitUpper = value;
                    UpdateVisible();
                    UpdateText();
                    OnValueLimitUpperChanged(System.EventArgs.Empty);
                }
            }
        }

        private double m_ValueLimitLower = -999999;
        public double ValueLimitLower
        {
            get
            {
                return m_ValueLimitLower;
            }
            set
            {
                if (m_ValueLimitLower != value)
                {
                    m_ValueLimitLower = value;
                    UpdateVisible();
                    UpdateText();
                    OnValueLimitLowerChanged(System.EventArgs.Empty);
                }
            }
        }

        private bool m_ShowValue = true;
        public bool ShowValue
        {
            get
            {
                return m_ShowValue;
            }
            set
            {
                if (m_ShowValue != value)
                {
                    m_ShowValue = value;
                    UpdateText();
                }
            }
        }

        private string m_NumericFormat;
        public string NumericFormat
        {
            get
            {
                return m_NumericFormat;
            }
            set
            {
                if (m_NumericFormat != value)
                {
                    m_NumericFormat = value;
                    UpdateText();
                }
            }
        }

        public enum VisibleControlOptions
        {
            Always,
            BelowLimit,
            WithinLimits,
            AboveLimits
        }

        private VisibleControlOptions m_VisibleControl = VisibleControlOptions.Always;
        public VisibleControlOptions VisibleControl
        {
            get
            {
                return m_VisibleControl;
            }
            set
            {
                if (m_VisibleControl != value)
                {
                    m_VisibleControl = value;
                    if (m_VisibleControl == VisibleControlOptions.Always)
                    {
                        this.Visible = true;
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        private void UpdateText()
        {
            string ResultText = string.Empty;

            double v = 0;

            if (m_ShowValue)
            {
                ResultText = m_Value;
                if (!string.IsNullOrEmpty(m_NumericFormat))
                {
                    if (double.TryParse(Value, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowParentheses | System.Globalization.NumberStyles.AllowTrailingWhite, System.Globalization.NumberFormatInfo.CurrentInfo, out v))
                    {
                        try
                        {
                            ResultText = v.ToString(m_NumericFormat);
                        }
                        catch (Exception ex)
                        {
                            ResultText = "Check Numeric Format";
                        }
                    }
                }
                else
                {
                    ResultText = Value;
                }
            }

            //* Apply the Prefix and Suffix
            if (!string.IsNullOrEmpty(m_Prefix))
            {
                ResultText = m_Prefix + ResultText;
            }
            if (!string.IsNullOrEmpty(m_Suffix))
            {
                ResultText += m_Suffix;
            }

            base.Text = ResultText;

            //* V3.99y
            if (double.TryParse(Value, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowParentheses | System.Globalization.NumberStyles.AllowTrailingWhite, System.Globalization.NumberFormatInfo.CurrentInfo, out v))
            {
                if (v > m_ValueLimitUpper)
                {
                    base.ForeColor = m_ForeColorOverLimit;
                }
                else if (v < m_ValueLimitLower)
                {
                    base.ForeColor = m_ForeColorUnderLimit;
                }
                else
                {
                    base.ForeColor = m_ForeColorInLimits;
                }
            }

        }


        private void UpdateVisible()
        {
            if (m_VisibleControl != VisibleControlOptions.Always)
            {
                if (m_VisibleControl == VisibleControlOptions.AboveLimits)
                {
                    this.Visible = (ValueAsDouble > ValueLimitUpper);
                }
                else if (m_VisibleControl == VisibleControlOptions.BelowLimit)
                {
                    this.Visible = (ValueAsDouble < ValueLimitLower);
                }
                else if (m_VisibleControl == VisibleControlOptions.WithinLimits)
                {
                    this.Visible = (ValueAsDouble <= ValueLimitUpper && ValueAsDouble >= ValueLimitLower);
                }
            }
        }
        #endregion

        #region PLC Related Properties


        public PLCAddressItem PLCAddressVisible { get; set; }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private PLCAddressItem m_PLCAddressValue;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        public PLCAddressItem PLCAddressValue
        {
            get
            {
                try
                {
                    return m_PLCAddressValue;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            set
            {
                if ((value == null || m_PLCAddressValue == null) || ((value.GetType()) == (m_PLCAddressValue.GetType())))
                {
                    try
                    {
                        if (m_PLCAddressValue != value)
                        {
                            m_PLCAddressValue = value;
                        }
                        try
                        {
                            //* When address is changed, re-subscribe to new address
                            SubscribeToComDriver();
                        }
                        catch (Exception ex)
                        {
                            //MsgBox("5 - " & ex.Message)
                        }
                    }
                    catch (Exception ex)
                    {
                        //MsgBox("6 - " & ex.Message)
                        //Console.WriteLine("AnalogValueDisplay PLCAddressValue setter exception")
                    }
                }
                //MsgBox("7 - Exit Setter")
            }
        }


        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private PLCAddressItem m_PLCAddressValueLimitUpper;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        public PLCAddressItem PLCAddressValueLimitUpper
        {
            get
            {
                return m_PLCAddressValueLimitUpper;
            }
            set
            {
                if (m_PLCAddressValueLimitUpper != value)
                {
                    m_PLCAddressValueLimitUpper = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private PLCAddressItem m_PLCAddressValueLimitLower;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        public PLCAddressItem PLCAddressValueLimitLower
        {
            get
            {
                return m_PLCAddressValueLimitLower;
            }
            set
            {
                if (m_PLCAddressValueLimitLower != value)
                {
                    m_PLCAddressValueLimitLower = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        private bool m_SuppressErrorDisplay;
        [System.ComponentModel.DefaultValue(false)]
        public bool SuppressErrorDisplay
        {
            get
            {
                return m_SuppressErrorDisplay;
            }
            set
            {
                m_SuppressErrorDisplay = value;
            }
        }


        #endregion

        #region Events
        //********************************************************************
        //* When an instance is added to the form, set the comm component
        //* property. If a comm component does not exist, add one to the form
        //********************************************************************
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
            {

            }
            else
            {
                SubscribeToComDriver();
            }
        }



        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        protected virtual void OnValueLimitUpperChanged(EventArgs e)
        {
            if (ValueLimitUpperChanged != null)
                ValueLimitUpperChanged(this, e);
        }

        protected virtual void OnValueLimitLowerChanged(EventArgs e)
        {
            if (ValueLimitLowerChanged != null)
                ValueLimitLowerChanged(this, e);
        }

        #endregion

        #region Subscribing and PLC data receiving
        private SubscriptionHandler SubScriptions;
        //**************************************************
        //* Subscribe to addresses in the Comm(PLC) Driver
        //**************************************************
        private void SubscribeToComDriver()
        {
            if (!DesignMode && IsHandleCreated)
            {
                //* Create a subscription handler object
                if (SubScriptions == null)
                {
                    SubScriptions = new SubscriptionHandler { Parent = this };
                    SubScriptions.DisplayError += DisplaySubscribeError;
                }

                SubScriptions.SubscribeAutoProperties();
            }
        }

        //***************************************
        //* Call backs for returned data
        //***************************************
        private string OriginalText;

        private void DisplaySubscribeError(object sender, PlcComEventArgs e)
        {
            DisplayError(e.ErrorMessage);
        }
        #endregion

        #region Error Display
        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private System.Windows.Forms.Timer ErrorDisplayTime;
        private void DisplayError(string ErrorMessage)
        {
            if (!m_SuppressErrorDisplay)
            {
                if (ErrorDisplayTime == null)
                {
                    ErrorDisplayTime = new System.Windows.Forms.Timer();
                    ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                    ErrorDisplayTime.Interval = 6000;
                }

                //* Save the text to return to
                if (!ErrorDisplayTime.Enabled)
                {
                    OriginalText = this.Text;
                }

                ErrorDisplayTime.Enabled = true;

                Text = ErrorMessage;
            }
        }


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

        #region Keypad popup for data entry
        private Keypad_v3 KeypadPopUp;


        //*****************************************
        //* Property - Address in PLC to Write Data To
        //*****************************************
        private string m_PLCAddressKeypad = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressKeypad
        {
            get
            {
                return m_PLCAddressKeypad;
            }
            set
            {
                if (m_PLCAddressKeypad != value)
                {
                    m_PLCAddressKeypad = value;
                }
            }
        }

        private string m_KeypadText;
        public string KeypadText
        {
            get
            {
                return m_KeypadText;
            }
            set
            {
                m_KeypadText = value;
            }
        }

        private Color m_KeypadFontColor = Color.WhiteSmoke;
        public Color KeypadFontColor
        {
            get
            {
                return m_KeypadFontColor;
            }
            set
            {
                m_KeypadFontColor = value;
            }
        }


        private int m_KeypadWidth = 300;
        public int KeypadWidth
        {
            get
            {
                return m_KeypadWidth;
            }
            set
            {
                m_KeypadWidth = value;
            }
        }

        private double m_KeypadScaleFactor = 1;
        public double KeypadScaleFactor
        {
            get
            {
                return m_KeypadScaleFactor;
            }
            set
            {
                m_KeypadScaleFactor = value;
            }
        }

        private double m_KeypadMinValue;
        public double KeypadMinValue
        {
            get
            {
                return m_KeypadMinValue;
            }
            set
            {
                m_KeypadMinValue = value;
            }
        }

        private double m_KeypadMaxValue;
        public double KeypadMaxValue
        {
            get
            {
                return m_KeypadMaxValue;
            }
            set
            {
                m_KeypadMaxValue = value;
            }
        }

        private string m_KeypadPasscode;
        public string KeypadPasscode
        {
            get
            {
                return m_KeypadPasscode;
            }
            set
            {
                m_KeypadPasscode = value;
            }
        }


        private void KeypadPopUp_ButtonClick(object sender, KeypadEventArgs e)
        {
            if (e.Key == "Quit")
            {
                KeypadPopUp.Visible = false;
            }
            else if (e.Key == "Enter")
            {
                if (string.IsNullOrEmpty(m_KeypadPasscode) || PasscodeValidated)
                {
                    if (Utilities.client == null)
                    {
                        DisplayError("ComComponent Property not set");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(KeypadPopUp.Value))
                        {
                            try
                            {
                                if (m_KeypadMaxValue != m_KeypadMinValue)
                                {
                                    if (string.CompareOrdinal(KeypadPopUp.Value, m_KeypadMinValue.ToString()) < 0 || string.CompareOrdinal(KeypadPopUp.Value, m_KeypadMaxValue.ToString()) > 0)
                                    {
                                        MessageBox.Show("Value must be >" + m_KeypadMinValue + " and <" + m_KeypadMaxValue);
                                        return;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Failed to validate value. " + ex.Message);
                                return;
                            }

                            try
                            {
                                if (KeypadScaleFactor == 1 || KeypadScaleFactor == 0)
                                {
                                    Utilities.Write(m_PLCAddressKeypad, KeypadPopUp.Value);
                                }
                                else
                                {
                                    Utilities.Write(m_PLCAddressKeypad, double.Parse(KeypadPopUp.Value) / m_KeypadScaleFactor);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Failed to write value. " + ex.Message);
                            }
                        }
                        KeypadPopUp.Visible = false;
                    }
                }
                else
                {
                    //* A passcode was entered, so validate
                    if (!string.IsNullOrEmpty(KeypadPopUp.Value) && KeypadPopUp.Value == m_KeypadPasscode)
                    {
                        PasscodeValidated = true;
                        PromptNewValue();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Passcode!");
                        KeypadPopUp.Visible = false;
                    }
                }
            }
        }

        //***********************************************************
        //* If labeled is clicked, pop up a keypad for data entry
        //***********************************************************
        protected override void OnClick(System.EventArgs e)
        {
            base.OnClick(e);

            if (m_PLCAddressKeypad != null && (string.Compare(m_PLCAddressKeypad, string.Empty) != 0) && Enabled)
            {
                if (KeypadPopUp == null)
                {
                    KeypadPopUp = new Keypad_v3();
                    KeypadPopUp.MinValue = Convert.ToDecimal(m_KeypadMinValue);
                    KeypadPopUp.MaxValue = Convert.ToDecimal(m_KeypadMaxValue);
                    KeypadPopUp.ButtonClick += KeypadPopUp_ButtonClick;
                }

                if (string.IsNullOrEmpty(m_KeypadPasscode))
                {
                    PromptNewValue();
                }
                else
                {
                    PromptPasscode();
                }
            }
        }

        private void PromptNewValue()
        {
            KeypadPopUp.Text = m_KeypadText;
            KeypadPopUp.ForeColor = m_KeypadFontColor;
            KeypadPopUp.MinValue = Convert.ToDecimal(m_KeypadMinValue);
            KeypadPopUp.MaxValue = Convert.ToDecimal(m_KeypadMaxValue);
            KeypadPopUp.Value = string.Empty;
            KeypadPopUp.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            KeypadPopUp.TopMost = true;
            KeypadPopUp.Show();
        }

        private bool PasscodeValidated;
        private void PromptPasscode()
        {
            PasscodeValidated = false;
            KeypadPopUp.Text = "Enter Passcode";
            KeypadPopUp.ForeColor = m_KeypadFontColor;
            KeypadPopUp.Value = string.Empty;
            KeypadPopUp.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            KeypadPopUp.TopMost = true;
            KeypadPopUp.Show();
        }
        #endregion
    }

}