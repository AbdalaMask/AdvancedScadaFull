using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.SevenSegment;
using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls_Net45;
using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Client;
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.Monitor;
using MfgControl.AdvancedHMI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Linq;

namespace AdvancedScada.Controls.AHMI.SevenSegment
{

    public class HMISevenSegment2 : MfgControl.AdvancedHMI.Controls.SevenSegment
    {
        #region Properties
        #endregion

        #region PLC Related Properties



        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        private string m_PLCAddressText = string.Empty;
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
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        private string m_PLCAddressVisible = string.Empty;
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
        private string m_PLCAddressValue;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressValue
        {
            get
            {
                return m_PLCAddressValue;
            }
            set
            {
                if (m_PLCAddressValue != value)
                {
                    m_PLCAddressValue = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressForecolorHighLimitValue;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressForecolorHighLimitValue
        {
            get
            {
                return m_PLCAddressForecolorHighLimitValue;
            }
            set
            {
                if (m_PLCAddressForecolorHighLimitValue != value)
                {
                    m_PLCAddressForecolorHighLimitValue = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        private string m_PLCAddressForecolorLowLimitValue;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressForecolorLowLimitValue
        {
            get
            {
                return m_PLCAddressForecolorLowLimitValue;
            }
            set
            {
                if (m_PLCAddressForecolorLowLimitValue != value)
                {
                    m_PLCAddressForecolorLowLimitValue = value;

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
            if (!DesignMode && IsHandleCreated)
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

                this.Text = ErrorMessage;
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

        //Public Property KPD As MfgControl.AdvancedScada.Controls.Keypad
        //*****************************************
        //* Property - Address in PLC to Write Data To
        //*****************************************
        private string m_PLCAddressKeypad = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
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


        private void KeypadPopUp_ButtonClick(object sender, KeypadEventArgs e)
        {
            if (e.Key == "Quit")
            {
                KeypadPopUp.Visible = false;
            }
            else if (e.Key == "Enter")
            {


                if (KeypadPopUp.Value != null && (string.Compare(KeypadPopUp.Value, string.Empty) != 0))
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
                    KeypadPopUp = new Keypad_v3(m_KeypadWidth);
                    KeypadPopUp.ButtonClick += KeypadPopUp_ButtonClick;
                }

                KeypadPopUp.Text = m_KeypadText;
                KeypadPopUp.ForeColor = m_KeypadFontColor;
                KeypadPopUp.MinValue = Convert.ToDecimal(m_KeypadMinValue);
                KeypadPopUp.MaxValue = Convert.ToDecimal(m_KeypadMaxValue);
                KeypadPopUp.Value = string.Empty;
                KeypadPopUp.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                KeypadPopUp.TopMost = true;
                KeypadPopUp.Show();
            }
        }
        #endregion
    }


}