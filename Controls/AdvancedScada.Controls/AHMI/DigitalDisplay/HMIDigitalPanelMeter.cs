using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.DigitalDisplay;
using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls_Net45;
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.Monitor;
using MfgControl.AdvancedHMI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Linq;

namespace AdvancedScada.Controls.AHMI.DigitalDisplay
{
    public class HMIDigitalPanelMeter : DigitalPanelMeter
    {
        public event EventHandler ValueChanged;
        #region Constructor/Destructor
        public HMIDigitalPanelMeter() : base()
        {
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
        #region PLC Related Properties

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
                        SubscribeToComDriver();
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
                        SubscribeToComDriver();
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
                        SubscribeToComDriver();
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }

        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }

        #endregion
        #region Subscribing and PLC data receiving
        private SubscriptionHandler SubScriptions;
        //*******************************************************************************
        //* Subscribe to addresses in the Comm(PLC) Driver
        //* This code will look at properties to find the "PLCAddress" + property name
        //*
        //*******************************************************************************
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


        private void DisplaySubscribeError(object sender, PlcComEventArgs e)
        {
            DisplayError(e.ErrorMessage);
        }
        #endregion
        #region "Error Display"

        private string OriginalText;

        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private Timer ErrorDisplayTime;

        private void DisplayError(string ErrorMessage)
        {
            if (!SuppressErrorDisplay)
            {
                if (ErrorDisplayTime == null)
                {
                    ErrorDisplayTime = new Timer();
                    ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                    ErrorDisplayTime.Interval = 6000;
                }

                //* Save the text to return to
                if (!ErrorDisplayTime.Enabled) OriginalText = Text;

                ErrorDisplayTime.Enabled = true;

                Text = ErrorMessage;
            }
        }


        //**************************************************************************************
        //* Return the text back to its original after displaying the error for a few seconds.
        //**************************************************************************************
        private void ErrorDisplay_Tick(object sender, EventArgs e)
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
            get { return m_PLCAddressKeypad; }
            set
            {
                if (m_PLCAddressKeypad != value) m_PLCAddressKeypad = value;
            }
        }

        public string KeypadText { get; set; }

        private Color m_KeypadFontColor = Color.WhiteSmoke;

        public Color KeypadFontColor
        {
            get { return m_KeypadFontColor; }
            set { m_KeypadFontColor = value; }
        }

        private int m_KeypadWidth = 300;

        public int KeypadWidth
        {
            get { return m_KeypadWidth; }
            set { m_KeypadWidth = value; }
        }

        private double m_KeypadScaleFactor = 1;

        public double KeypadScaleFactor
        {
            get { return m_KeypadScaleFactor; }
            set { m_KeypadScaleFactor = value; }
        }

        public double KeypadMinValue { get; set; }

        public double KeypadMaxValue { get; set; }


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
                        if ((KeypadScaleFactor == 1) | (KeypadScaleFactor == 0))
                        {
                            Utilities.Write(m_PLCAddressKeypad, KeypadPopUp.Value);
                        }
                        else
                        {
                            var v = Convert.ToDouble(KeypadPopUp.Value);
                            var z = v / m_KeypadScaleFactor;
                            Utilities.Write(m_PLCAddressKeypad,
                                (Convert.ToDouble(KeypadPopUp.Value) / m_KeypadScaleFactor).ToString());
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