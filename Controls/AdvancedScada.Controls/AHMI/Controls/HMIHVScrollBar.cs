using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Common;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.Controls;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls_Net45;
using MfgControl.AdvancedHMI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.Controls.AHMI.Controls
{
    public class HMIHVScrollBar : HVScrollBar
    {

        private OutputType _OutputType = OutputType.WriteValue;

        #region PLC Properties



        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private bool InvertVisible;
        private string m_PLCAddressVisible = string.Empty;
        [Browsable(true), Category("PLC Properties"), Description("PLCAddress to control the ScrollBar visibility.")]
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
                    SubscribeToCommDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressValue = string.Empty;
        [Browsable(true), Category("PLC Properties"), Description("PLCAddress to control the ScrollBar value.")]
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
                    SubscribeToCommDriver();
                }
            }
        }

        //*******************************************************
        //* Property - Supress error from showing on the control
        //*******************************************************
        private bool m_SuppressErrorDisplay;
        [Browsable(true), Category("PLC Properties"), Description("Supress error from showing on the control."), DefaultValue(false)]
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
                SubscribeToCommDriver();
            }
        }

        //****************************************************************
        //* Control overrides dispose to clean up the component list.
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

        //*******************************************************************************
        //* Subscribe to addresses in the Comm(PLC) Driver
        //* This code will look at properties to find the "PLCAddress" + property name
        //*
        //*******************************************************************************
        private SubscriptionHandler SubScriptions;
        private void SubscribeToCommDriver()
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
            }
        }

        #endregion



    }
}
