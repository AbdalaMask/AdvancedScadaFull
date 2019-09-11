using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.Subscription;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using AdvancedScada;
 
using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls_Net45;
 
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.Controls.AHMI.ButtonAll;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using MfgControl.AdvancedHMI.Controls;

namespace AdvancedScada.Controls.AHMI.ButtonAll
{
    public class HMICheckBox : System.Windows.Forms.CheckBox
    {
        #region PLC Related Properties

        private OutputType m_OutputType = OutputType.MomentarySet;

        public OutputType OutputType
        {
            get { return m_OutputType; }
            set { m_OutputType = value; }
        }

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
                        //* When address is changed, re-subscribe to new address
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
        private string m_PLCAddressChecked = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressChecked
        {
            get { return m_PLCAddressChecked; }
            set
            {
                if (m_PLCAddressCheckChanged != value)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        m_PLCAddressCheckChanged = value;
                    }
                    else
                    {
                        m_PLCAddressCheckChanged = string.Empty;
                    }
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressCheckChanged = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressCheckChanged
        {
            get { return m_PLCAddressCheckChanged; }
            set
            {
                if (m_PLCAddressCheckChanged != value) m_PLCAddressCheckChanged = value;
            }
        }

        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }

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
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!string.IsNullOrEmpty(m_PLCAddressCheckChanged))
                try
                {
                    var WriteValue = "0";
                    if (Checked) WriteValue = "1";
                    Utilities.Write(m_PLCAddressCheckChanged, WriteValue);
                }
                catch (Exception ex)
                {
                    DisplayError(ex.Message);
                }
        }
        #endregion

    }
}