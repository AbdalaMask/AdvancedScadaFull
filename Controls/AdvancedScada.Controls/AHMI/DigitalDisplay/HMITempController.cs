using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.Subscription;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AdvancedScada;
 
 
using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.Subscription;
using MfgControl.AdvancedHMI.Controls;

using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Client;
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.Controls.AHMI.DigitalDisplay;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;

namespace AdvancedScada.Controls.AHMI.DigitalDisplay
{
    public class HMITempController : TempController
    {



        #region Properties
        private System.Drawing.Color SavedBackColor;

        private decimal _ValueScaleFactor = 1M;
        [System.ComponentModel.Category("Numeric Display")]
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
        private string m_PLCAddressValuePV = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressValuePV
        {
            get
            {
                return m_PLCAddressValuePV;
            }
            set
            {
                if (m_PLCAddressValuePV != value)
                {
                    m_PLCAddressValuePV = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressValueSP = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressValueSP
        {
            get
            {
                return m_PLCAddressValueSP;
            }
            set
            {
                if (m_PLCAddressValueSP != value)
                {
                    m_PLCAddressValueSP = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //********************************************
        //* Property - Address in PLC for click event
        //********************************************
        private string m_PLCAddressClick1 = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick1
        {
            get
            {
                return m_PLCAddressClick1;
            }
            set
            {
                m_PLCAddressClick1 = value;
            }
        }

        private string m_PLCAddressClick2 = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick2
        {
            get
            {
                return m_PLCAddressClick2;
            }
            set
            {
                m_PLCAddressClick2 = value;
            }
        }

        private string m_PLCAddressClick3 = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick3
        {
            get
            {
                return m_PLCAddressClick3;
            }
            set
            {
                m_PLCAddressClick3 = value;
            }
        }

        private string m_PLCAddressClick4 = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick4
        {
            get
            {
                return m_PLCAddressClick4;
            }
            set
            {
                m_PLCAddressClick4 = value;
            }
        }

        //*****************************************
        //* Property - What to do to bit in PLC
        //*****************************************
        private OutputType m_OutputType = OutputType.MomentarySet;
        [System.ComponentModel.Category("PLC Properties")]
        public OutputType OutputType
        {
            get
            {
                return m_OutputType;
            }
            set
            {
                m_OutputType = value;
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
        //* Event - Button Click
        //****************************
        private void _Click1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseDownActon(m_PLCAddressClick1);
        }

        private void _MouseUp1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseUpAction(m_PLCAddressClick1);
        }

        private void _click2(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseDownActon(m_PLCAddressClick2);
        }

        private void _MouseUp2(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseUpAction(m_PLCAddressClick2);
        }

        private void _click3(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseDownActon(m_PLCAddressClick3);
        }

        private void _MouseUp3(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseUpAction(m_PLCAddressClick3);
        }

        private void _click4(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseDownActon(m_PLCAddressClick4);
        }

        private void _MouseUp4(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseUpAction(m_PLCAddressClick4);
        }

        private void MouseDownActon(string PLCAddress)
        {
            if (PLCAddress != null && (string.Compare(PLCAddress, string.Empty) != 0))
            {
                try
                {
                    switch (m_OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(PLCAddress, "1");
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(PLCAddress, "0");
                            break;
                        case OutputType.SetTrue:
                            Utilities.Write(PLCAddress, "1");
                            break;
                        case OutputType.SetFalse:
                            Utilities.Write(PLCAddress, "0");
                            break;
                        case OutputType.Toggle:
                            var CurrentValue = false;
                            CurrentValue = Convert.ToBoolean(PLCAddress);
                            if (CurrentValue)
                                Utilities.Write(PLCAddress, "0");
                            else
                                Utilities.Write(PLCAddress, "1");
                            break;
                    }
                }
                catch (PLCDriverException ex)
                {
                    if (ex.ErrorCode == 1808)
                    {
                        DisplayError("\"" + PLCAddress + "\" PLC Address not found");
                    }
                    else
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }


        private void MouseUpAction(string PLCAddress)
        {
            if (PLCAddress != null && (string.Compare(PLCAddress, string.Empty) != 0) && Enabled)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(PLCAddress, "0");
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(PLCAddress, "1");
                            break;
                    }

                }
                catch (PLCDriverException ex)
                {
                    if (ex.ErrorCode == 1808)
                    {
                        DisplayError("\"" + PLCAddress + "\" PLC Address not found");
                    }
                    else
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }
        #endregion

        #region Constructor/Destructor
        public HMITempController()
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



    }


}