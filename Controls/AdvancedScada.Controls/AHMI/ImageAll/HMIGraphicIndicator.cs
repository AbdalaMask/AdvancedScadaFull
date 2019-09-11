using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls.Subscription;
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
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.Controls.AHMI.ImageAll;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;



//****************************************************************************
//* 12-DEC-08 Added line in OnPaint to exit is LegendText is nothing
//* 05-OCT-09 Exit OnPaint if GrayPen is Nothing
//****************************************************************************

namespace AdvancedScada.Controls.AHMI.ImageAll
{
    public class HMIGraphicIndicator : GraphicIndicator
    {

        #region Constructor/Destructor
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
        #region PLC Related Properties
        public bool HoldTimeMet;
        private int m_MaximumHoldTime = 3000;
        private int m_MinimumHoldTime = 500;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressClick = string.Empty;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressSelect1 = string.Empty;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressSelect2 = string.Empty;


        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressText2 = string.Empty;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;

        //**********************************************************************
        //* If output type is set to write value, then write this value to PLC
        //**********************************************************************

        //*****************************************
        //* Property - Hold time before bit reset
        //*****************************************
        private readonly Timer MaxHoldTimer = new Timer();

        //*****************************************
        //* Property - Hold time before bit reset
        //*****************************************
        private readonly Timer MinHoldTimer = new Timer();
        private readonly bool MouseIsDown = false;

        //***************************************
        //* Call backs for returned data
        //***************************************
        private string OriginalText;


        public string PLCAddressValueSelect1
        {
            get { return m_PLCAddressSelect1; }
            set
            {
                if (m_PLCAddressSelect1 != value)
                {
                    m_PLCAddressSelect1 = value;
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

        public string PLCAddressValueSelect2
        {
            get { return m_PLCAddressSelect2; }
            set
            {
                if (m_PLCAddressSelect2 != value)
                    m_PLCAddressSelect2 = value;
                {
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

        public string PLCAddressVisible
        {
            get { return m_PLCAddressVisible; }
            set
            {
                if (m_PLCAddressVisible != value)
                {
                    m_PLCAddressVisible = value;
                    //* When address is changed, re-subscribe to new address
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

        public string PLCAddressText2
        {
            get
            {
                return m_PLCAddressText2;
            }
            set
            {
                if (m_PLCAddressText2 != value)
                {
                    m_PLCAddressText2 = value;
                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }
        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick
        {
            get { return m_PLCAddressClick; }
            set
            {
                if (m_PLCAddressClick != value) m_PLCAddressClick = value;
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
        [Category("PLC Properties")]
        public int MinimumHoldTime
        {
            get { return m_MinimumHoldTime; }
            set
            {
                m_MinimumHoldTime = value;
                if (value > 0) MinHoldTimer.Interval = value;
            }
        }

        [Category("PLC Properties")]
        public int MaximumHoldTime
        {
            get { return m_MaximumHoldTime; }
            set
            {
                m_MaximumHoldTime = value;
                if (value > 0) MaxHoldTimer.Interval = value;
            }
        }

        [Category("PLC Properties")]
        public int ValueToWrite { get; set; }


        private void ReleaseValue()
        {
            try
            {
                switch (OutputType)
                {
                    case OutputTypes.MomentarySet:
                        Utilities.Write(m_PLCAddressClick, Convert.ToString(false));
                        break;
                    case OutputTypes.MomentaryReset:
                        Utilities.Write(m_PLCAddressClick, Convert.ToString(true));
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HoldTimer_Tick(object sender, EventArgs e)
        {
            MinHoldTimer.Enabled = false;
            HoldTimeMet = true;
            if (!MouseIsDown) ReleaseValue();
        }

        private void MaxHoldTimer_Tick(object sender, EventArgs e)
        {
            MaxHoldTimer.Enabled = false;
            ReleaseValue();
        }
        #endregion
        #region Events
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);


            if (!string.IsNullOrWhiteSpace(m_PLCAddressClick) & Enabled && m_PLCAddressClick != null)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputTypes.MomentarySet:
                            Utilities.Write(m_PLCAddressClick, "1");
                            break;
                        case OutputTypes.MomentaryReset:
                            Utilities.Write(m_PLCAddressClick, "0");
                            break;
                        case OutputTypes.SetTrue:
                            Utilities.Write(m_PLCAddressClick, "1");
                            break;
                        case OutputTypes.SetFalse:
                            Utilities.Write(m_PLCAddressClick, "0");
                            break;
                        case OutputTypes.Toggle:

                            var CurrentValue = false;
                            if (CurrentValue)
                                Utilities.Write(m_PLCAddressClick, "0");
                            else
                                Utilities.Write(m_PLCAddressClick, "1");
                            break;
                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError("WRITE FAILED!" + ex.Message);
                }

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!string.IsNullOrWhiteSpace(m_PLCAddressClick) & Enabled)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputTypes.MomentarySet:
                            Utilities.Write(m_PLCAddressClick, "0");
                            break;
                        case OutputTypes.MomentaryReset:
                            Utilities.Write(m_PLCAddressClick, "1");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError("WRITE FAILED!" + ex.Message);
                }

                Invalidate();
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