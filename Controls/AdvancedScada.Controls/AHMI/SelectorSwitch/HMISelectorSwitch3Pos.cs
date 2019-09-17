using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.SelectorSwitch;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.DriverBase.Comm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AdvancedScada.Controls.AHMI.SelectorSwitch
{
    public class HMISelectorSwitch3Pos : MfgControl.AdvancedHMI.Controls.SelectorSwitch3Pos
    {
        #region PLC Related Properties

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
        private string m_PLCAddressValueLeft = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressValueLeft
        {
            get
            {
                return m_PLCAddressValueLeft;
            }
            set
            {
                if (m_PLCAddressValueLeft != value)
                {
                    m_PLCAddressValueLeft = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressValueRight = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressValueRight
        {
            get
            {
                return m_PLCAddressValueRight;
            }
            set
            {
                if (m_PLCAddressValueRight != value)
                {
                    m_PLCAddressValueRight = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressClickLeft = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressClickLeft
        {
            get
            {
                return m_PLCAddressClickLeft;
            }
            set
            {
                if (m_PLCAddressClickLeft != value)
                {
                    m_PLCAddressClickLeft = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressClickRight = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressClickRight
        {
            get
            {
                return m_PLCAddressClickRight;
            }
            set
            {
                if (m_PLCAddressClickRight != value)
                {
                    m_PLCAddressClickRight = value;

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
        private System.Windows.Forms.Timer tmrError;

        private void ClickedLeft()
        {
            try
            {
                if (m_PLCAddressClickLeft != null && (string.Compare(m_PLCAddressClickLeft, string.Empty) != 0))
                {
                    Utilities.Write(m_PLCAddressClickLeft, 1);
                }

                if (m_PLCAddressClickRight != null && (string.Compare(m_PLCAddressClickRight, string.Empty) != 0))
                {
                    Utilities.Write(m_PLCAddressClickRight, 0);
                }
            }
            catch (Exception ex)
            {
                DisplayError("WRITE FAILED!" + ex.Message);
            }
        }

        private void ClickedCenter()
        {
            try
            {
                if (m_PLCAddressClickLeft != null && (string.Compare(m_PLCAddressClickLeft, string.Empty) != 0))
                {
                    Utilities.Write(m_PLCAddressClickLeft, 0);
                }

                if (m_PLCAddressClickRight != null && (string.Compare(m_PLCAddressClickRight, string.Empty) != 0))
                {
                    Utilities.Write(m_PLCAddressClickRight, 0);
                }
            }
            catch (Exception ex)
            {
                DisplayError("WRITE FAILED!" + ex.Message);
            }
        }

        private void ClickedRight()
        {
            try
            {
                if (m_PLCAddressClickLeft != null && (string.Compare(m_PLCAddressClickLeft, string.Empty) != 0))
                {
                    Utilities.Write(m_PLCAddressClickLeft, 0);
                }

                if (m_PLCAddressClickRight != null && (string.Compare(m_PLCAddressClickRight, string.Empty) != 0))
                {
                    Utilities.Write(m_PLCAddressClickRight, 1);
                }
            }
            catch (Exception ex)
            {
                DisplayError("WRITE FAILED!" + ex.Message);
            }
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
        [System.Runtime.CompilerServices.AccessedThroughProperty(nameof(ErrorDisplayTime))]
        private System.Windows.Forms.Timer _ErrorDisplayTime;
        private System.Windows.Forms.Timer ErrorDisplayTime
        {
            [System.Diagnostics.DebuggerNonUserCode]
            get
            {
                return _ErrorDisplayTime;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
            set
            {
                if (_ErrorDisplayTime != null)
                {
                    _ErrorDisplayTime.Tick -= ErrorDisplay_Tick;
                }

                _ErrorDisplayTime = value;

                if (value != null)
                {
                    _ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                }
            }
        }
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