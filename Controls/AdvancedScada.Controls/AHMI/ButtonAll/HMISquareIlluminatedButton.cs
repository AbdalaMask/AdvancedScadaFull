using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Common;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.ButtonAll;
using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using MfgControl.AdvancedHMI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AdvancedScada.Controls.AHMI.ButtonAll
{
    public class HMISquareIlluminatedButton : SquareIlluminatedButton
    {


        #region PLC Related Properties



        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressText = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
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
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
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
        private string m_PLCAddressValue = string.Empty;
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
        private string m_PLCAddressClick = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick
        {
            get
            {
                return m_PLCAddressClick;
            }
            set
            {
                if (m_PLCAddressClick != value)
                {
                    m_PLCAddressClick = value;
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
        private System.Windows.Forms.Timer tmrError;

        //****************************
        //* Event - Mouse Down
        //****************************
        private void MomentaryButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if ((m_PLCAddressClick != null && (string.Compare(m_PLCAddressClick, string.Empty) != 0)) && Enabled && Utilities.client != null)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(m_PLCAddressClick, 1);
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(m_PLCAddressClick, 0);
                            break;
                        case OutputType.SetTrue:
                            Utilities.Write(m_PLCAddressClick, 1);
                            break;
                        case OutputType.SetFalse:
                            Utilities.Write(m_PLCAddressClick, 0);
                            break;
                        case OutputType.Toggle:
                            bool CurrentValue = Value;
                            if (CurrentValue)
                            {
                                Utilities.Write(m_PLCAddressClick, 0);
                            }
                            else
                            {
                                Utilities.Write(m_PLCAddressClick, 1);
                            }
                            break;
                        default:

                            break;
                    }

                    if (tmrError.Enabled)
                    {
                        tmrError.Enabled = false;
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
            if ((m_PLCAddressClick != null && (string.Compare(m_PLCAddressClick, string.Empty) != 0)) && Enabled && Utilities.client != null)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(m_PLCAddressClick, 0);
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(m_PLCAddressClick, 1);
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
    }


}