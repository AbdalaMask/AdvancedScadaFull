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
using AdvancedScada.Controls.AHMI.Controls;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;

namespace AdvancedScada.Controls.AHMI.Controls
{
    public class HMIIndicator : Indicator
    {

        #region PLC Related Properties
        //********************************************
        //* Property - Address in PLC for click event
        //********************************************
        private string m_PLCAddressClick = string.Empty;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick
        {
            get
            {
                return m_PLCAddressClick;
            }
            set
            {
                m_PLCAddressClick = value;
            }
        }

        private OutputType m_OutputType = OutputType.MomentarySet;
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



        //*************************************************************
        //* Property - Address in PLC to Link to for selecting color 2
        //*************************************************************
        private string m_PLCAddressSelectColor2 = string.Empty;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressSelectColor2
        {
            get
            {
                return m_PLCAddressSelectColor2;
            }
            set
            {
                if (m_PLCAddressSelectColor2 != value)
                {
                    m_PLCAddressSelectColor2 = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*************************************************************
        //* Property - Address in PLC to Link to for selecting color 3
        //*************************************************************
        private string m_PLCAddressSelectColor3 = string.Empty;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressSelectColor3
        {
            get
            {
                return m_PLCAddressSelectColor3;
            }
            set
            {
                if (m_PLCAddressSelectColor3 != value)
                {
                    m_PLCAddressSelectColor3 = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
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


        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }




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
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            if ((m_PLCAddressClick != null && (string.Compare(m_PLCAddressClick, string.Empty) != 0)) && Enabled)
            {
                try
                {
                    switch (m_OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(PLCAddressClick, "0");
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(PLCAddressClick, "1");
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


        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (PLCAddressClick != null && string.Compare(PLCAddressClick, string.Empty) != 0 && Enabled &&
                PLCAddressClick != null)
                try
                {
                    if (OutputType == OutputType.MomentarySet)
                    {
                        Utilities.Write(PLCAddressClick, "1");

                    }
                    else if (OutputType == OutputType.MomentaryReset)
                    {
                        Utilities.Write(PLCAddressClick, "0");

                    }

                    else if (OutputType == OutputType.SetTrue)
                    {
                        Utilities.Write(PLCAddressClick, "1");
                    }

                    else if (OutputType == OutputType.SetFalse)
                    {
                        Utilities.Write(PLCAddressClick, "0");
                    }

                    else if (OutputType == OutputType.Toggle)
                    {
                        bool CurrentValue = Convert.ToBoolean(Text);
                        if (CurrentValue)
                            Utilities.Write(PLCAddressClick, "0");
                        else
                            Utilities.Write(PLCAddressClick, "1");
                    }
                }
                catch (PLCDriverException ex)
                {
                    if (ex.ErrorCode == 1808)
                    {
                        DisplayError("\"" + m_PLCAddressClick + "\" PLC Address not found");
                    }
                    else
                    {
                        DisplayError(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    //* V3.99w - Catch a more general exception
                    DisplayError("GE. " + ex.Message);
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
                //V3.99p - added Try-Catch
                try
                {
                    base.Dispose(disposing);
                }
                catch
                {
                }
            }
        }
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

        //***************************************
        //* Call backs for returned data
        //***************************************
        private string OriginalText;
        //Private Sub PolledDataReturned(ByVal sender As Object, ByVal e As SubscriptionHandlerEventArgs)
        //End Sub

        private void DisplaySubscribeError(object sender, PlcComEventArgs e)
        {
            DisplayError(e.ErrorMessage);
        }
        #endregion

        #region Error Display
        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private System.Windows.Forms.Timer tmrError;
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
    }


}