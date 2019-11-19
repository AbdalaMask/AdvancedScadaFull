using AdvancedScada;
using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Common;
using AdvancedScada.Controls;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls_Net45;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedScada.Controls.AHMI.TankAll
{
    public class HMIStackLight : StackLight5Lights_V4
    {
        #region Basic Properties

        private System.Drawing.Color SavedBackColor;

        //******************************************************************************************
        //* Use the base control's text property and make it visible as a property on the designer
        //******************************************************************************************
        [System.ComponentModel.Browsable(true), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (m_Format != null && (string.Compare(m_Format, string.Empty) != 0))
                {
                    try
                    {
                        base.Text = _Prefix + (float.Parse(value) * (float)_ValueScaleFactor).ToString(m_Format) + _Suffix;
                    }
                    catch (Exception ex)
                    {
                        base.Text = "Check NumericFormat and variable type";
                    }
                }
                else
                {
                    //* Highlight in red if an exclamation mark is in text
                    if (value.IndexOf(_HighlightKeyChar) >= 0)
                    {
                        if (base.BackColor != _Highlightcolor)
                        {
                            SavedBackColor = base.BackColor;
                        }
                        base.BackColor = _Highlightcolor;
                    }
                    else
                    {
                        if (SavedBackColor != new System.Drawing.Color())
                        {
                            base.BackColor = SavedBackColor;
                        }
                    }

                    if (_ValueScaleFactor == 1M)
                    {
                        base.Text = _Prefix + value + _Suffix;
                    }
                    else
                    {
                        try
                        {
                            base.Text = (Convert.ToSingle(value) * (float)_ValueScaleFactor).ToString();
                        }
                        catch (Exception ex)
                        {
                            DisplayError("Scale Factor Error - " + ex.Message);
                        }
                    }
                }
            }
        }

        //**********************************
        //* Prefix and suffixes to text
        //**********************************
        private string _Prefix;
        public string TextPrefix
        {
            get
            {
                return _Prefix;
            }
            set
            {
                _Prefix = value;
                Invalidate();
            }
        }

        private string _Suffix;
        public string TextSuffix
        {
            get
            {
                return _Suffix;
            }
            set
            {
                _Suffix = value;
                Invalidate();
            }
        }

        //***************************************************************
        //* Property - Highlight Color
        //***************************************************************
        private System.Drawing.Color _Highlightcolor = System.Drawing.Color.Red;
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color HighlightColor
        {
            get
            {
                return _Highlightcolor;
            }
            set
            {
                _Highlightcolor = value;
            }
        }

        private string _HighlightKeyChar = "!";
        [System.ComponentModel.Category("Appearance")]
        public string HighlightKeyCharacter
        {
            get
            {
                return _HighlightKeyChar;
            }
            set
            {
                _HighlightKeyChar = value;
            }
        }

        private string m_Format;
        public string NumericFormat
        {
            get
            {
                return m_Format;
            }
            set
            {
                m_Format = value;
            }
        }

        private decimal _ValueScaleFactor = 1M;
        public decimal ScaleFactor
        {
            get
            {
                return _ValueScaleFactor;
            }
            set
            {
                _ValueScaleFactor = value;
            }
        }

        #endregion

        #region PLC Related Properties


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

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressText = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressText
        {
            get
            {
                return m_PLCAddressText;
            }
            set
            {
                if (m_PLCAddressText != value)
                {
                    m_PLCAddressText = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressVisible
        {
            get
            {
                return m_PLCAddressVisible;
            }
            set
            {
                if (m_PLCAddressVisible != value)
                {
                    m_PLCAddressVisible = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressLightPos_5Value = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressLightPos_5Value
        {
            get
            {
                return m_PLCAddressLightPos_5Value;
            }
            set
            {
                if (m_PLCAddressLightPos_5Value != value)
                {
                    m_PLCAddressLightPos_5Value = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressLightPos_1Value = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressLightPos_1Value
        {
            get
            {
                return m_PLCAddressLightPos_1Value;
            }
            set
            {
                if (m_PLCAddressLightPos_1Value != value)
                {
                    m_PLCAddressLightPos_1Value = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressLightPos_2Value = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressLightPos_2Value
        {
            get
            {
                return m_PLCAddressLightPos_2Value;
            }
            set
            {
                if (m_PLCAddressLightPos_2Value != value)
                {
                    m_PLCAddressLightPos_2Value = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressLightPos_4Value = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressLightPos_4Value
        {
            get
            {
                return m_PLCAddressLightPos_4Value;
            }
            set
            {
                if (m_PLCAddressLightPos_4Value != value)
                {
                    m_PLCAddressLightPos_4Value = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressLightPos_3Value = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressLightPos_3Value
        {
            get
            {
                return m_PLCAddressLightPos_3Value;
            }
            set
            {
                if (m_PLCAddressLightPos_3Value != value)
                {
                    m_PLCAddressLightPos_3Value = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

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


                if (DesignMode)
                {
                    if (this.Parent.BackColor == System.Drawing.Color.Black && base.ForeColor == System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ControlText))
                    {
                        ForeColor = System.Drawing.Color.White;
                    }
                }
            }
            else
            {
                SubscribeToComDriver();
            }
        }

        #endregion

        #region Constructor/Destructor

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

                    if (KeypadPopUp != null)
                    {
                        KeypadPopUp.Dispose();
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion

        #region Subscribing and PLC data receiving

        private SubscriptionHandler SubScriptions;
        //**************************************************
        //* Subscribe to addresses in the Comm(PLC) Driver
        //**************************************************
        private void SubscribeToComDriver()
        {
            if (!DesignMode & IsHandleCreated)
            {
                //* Create a subscription handler object
                if (SubScriptions == null)
                {
                    SubScriptions = new SubscriptionHandler();
                    SubScriptions.Parent = this;
                    SubScriptions.DisplayError += DisplaySubscribeError;
                }

                SubScriptions.SubscribeAutoProperties();
            }
        }

        //***************************************
        //* Call backs for returned data
        //***************************************
        private string OriginalText;
        private void PolledDataReturned(object sender, SubscriptionHandlerEventArgs e)
        {
        }

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
                    ErrorDisplayTime.Interval = 5000;
                }

                //* Save the text to return to
                if (!ErrorDisplayTime.Enabled)
                {
                    OriginalText = this.Text;
                }

                ErrorDisplayTime.Enabled = true;

                base.Text = ErrorMessage;
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


        private void KeypadPopUp_ButtonClick(object sender, KeypadEventArgs e)
        {
            if (e.Key == "Quit")
            {
                KeypadPopUp.Visible = false;
            }
            else if (e.Key == "Enter")
            {
                if (Utilities.client != null && (KeypadPopUp.Value != null && (string.Compare(KeypadPopUp.Value, string.Empty) != 0)))
                {
                    if (ScaleFactor == 1M)
                    {
                        Utilities.Write(m_PLCAddressKeypad, KeypadPopUp.Value);
                    }
                    else
                    {
                        Utilities.Write(m_PLCAddressKeypad, decimal.Parse(KeypadPopUp.Value) / ScaleFactor);
                    }
                }
                else
                {
                    DisplayError("ComComponent Property not set");
                }
                KeypadPopUp.Visible = false;
            }
        }

        //***********************************************************
        //* If the control is clicked, pop up a keypad for data entry
        //***********************************************************
        private void BasicLabelWithEntry_Click(object sender, System.EventArgs e)
        {
            if (m_PLCAddressKeypad != null && (string.Compare(m_PLCAddressKeypad, string.Empty) != 0) && Enabled)
            {
                if (KeypadPopUp == null)
                {
                    KeypadPopUp = new Keypad_v3();
                }

                KeypadPopUp.Text = m_KeypadText;
                KeypadPopUp.Value = string.Empty;
                KeypadPopUp.StartPosition = FormStartPosition.CenterScreen;
                KeypadPopUp.TopMost = true;
                KeypadPopUp.Show();
            }
        }

        #endregion


        public HMIStackLight()
        {
            SubscribeToEvents();
        }

        //INSTANT C# NOTE: Converted event handler wireups:
        private bool EventsSubscribed = false;
        private void SubscribeToEvents()
        {
            if (EventsSubscribed)
                return;
            else
                EventsSubscribed = true;

            this.Click += BasicLabelWithEntry_Click;
        }

    }

}
