using AdvancedScada.Controls.Subscription;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AdvancedScada;
 
 
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls_Net45;
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.Controls.AHMI.SelectorSwitch;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;

namespace AdvancedScada.Controls.AHMI.SelectorSwitch
{
    public class HMIPilotLight3Color : AdvancedScada.Controls_Net45.PilotLight3Color
    {
        #region PLC Related Properties

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCaddressClick = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCaddressClick
        {
            get
            {
                return m_PLCaddressClick;
            }
            set
            {
                if (m_PLCaddressClick != value)
                {
                    m_PLCaddressClick = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*************************************************************
        //* Property - Address in PLC to Link to for selecting color 2
        //*************************************************************
        private string m_PLCaddressSelectColor2 = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCaddressSelectColor2
        {
            get
            {
                return m_PLCaddressSelectColor2;
            }
            set
            {
                if (m_PLCaddressSelectColor2 != value)
                {
                    m_PLCaddressSelectColor2 = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*************************************************************
        //* Property - Address in PLC to Link to for selecting color 3
        //*************************************************************
        private string m_PLCaddressSelectColor3 = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCaddressSelectColor3
        {
            get
            {
                return m_PLCaddressSelectColor3;
            }
            set
            {
                if (m_PLCaddressSelectColor3 != value)
                {
                    m_PLCaddressSelectColor3 = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCaddressText = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCaddressText
        {
            get
            {
                return m_PLCaddressText;
            }
            set
            {
                if (m_PLCaddressText != value)
                {
                    m_PLCaddressText = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCaddressVisible = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCaddressVisible
        {
            get
            {
                return m_PLCaddressVisible;
            }
            set
            {
                if (m_PLCaddressVisible != value)
                {
                    m_PLCaddressVisible = value;

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

        //****************************
        //* Event - Mouse Down
        //****************************
        private void MomentaryButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if ((m_PLCaddressClick != null && (string.Compare(m_PLCaddressClick, string.Empty) != 0)) && Enabled && Utilities.client != null)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(m_PLCaddressClick, 1);
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(m_PLCaddressClick, 0);
                            break;
                        case OutputType.SetTrue:
                            Utilities.Write(m_PLCaddressClick, 1);
                            break;
                        case OutputType.SetFalse:
                            Utilities.Write(m_PLCaddressClick, 0);
                            break;
                        case OutputType.Toggle:
                            bool CurrentValue = bool.Parse(Text);
                            if (CurrentValue)
                            {
                                Utilities.Write(m_PLCaddressClick, 0);
                            }
                            else
                            {
                                Utilities.Write(m_PLCaddressClick, 1);
                            }
                            break;
                        default:

                            break;
                    }


                }
                catch (Exception ex)
                {
                    DisplayError("WRITE FAILED!" + ex.Message);
                }
            }
            this.Invalidate();
        }



        //****************************
        //* Event - Mouse Up
        //****************************
        private void MomentaryButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((m_PLCaddressClick != null && (string.Compare(m_PLCaddressClick, string.Empty) != 0)) && Enabled && Utilities.client != null)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(m_PLCaddressClick, "0");
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(m_PLCaddressClick, "1");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError("WRITE FAILED!" + ex.Message);
                }
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
    }
}