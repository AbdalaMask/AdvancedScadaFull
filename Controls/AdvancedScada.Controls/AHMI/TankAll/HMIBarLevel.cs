using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Common;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.TankAll;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AdvancedScada.Controls.AHMI.TankAll
{
    public class HMIBarLevel : MfgControl.AdvancedHMI.Controls.BarLevel
    {

        #region Properties
        private System.Drawing.Color SavedBackColor;

        //******************************************************************************************
        //* Use the base control's text property and make it visible as a property on the designer
        //******************************************************************************************
        [System.ComponentModel.Browsable(true), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                //If (m_Format IsNot Nothing AndAlso (String.Compare(m_Format, "") <> 0)) And (Not DesignMode) Then
                //    Try
                //        MyBase.Text = (CSng(value) * m_ValueScaleFactor).ToString(m_Format)
                //    Catch ex As Exception
                //        MyBase.Text = "Check NumericFormat and variable type"
                //    End Try
                //Else
                //* Highlight in red if an exclamation mark is in text
                if (value.IndexOf("!") >= 0)
                {
                    if (base.BackColor != _Highlightcolor)
                    {
                        SavedBackColor = base.BackColor;
                    }
                    base.BackColor = _Highlightcolor;
                }
                else
                {
                    if (SavedBackColor != new System.Drawing.Color())
                    {
                        base.BackColor = SavedBackColor;
                    }


                    base.Text = value;

                }
            }
        }

        //***************************************************************
        //* Property - Highlight Color
        //***************************************************************
        private System.Drawing.Color _Highlightcolor = System.Drawing.Color.Red;
        public System.Drawing.Color HighlightColor
        {
            get
            {
                return _Highlightcolor;
            }
            set
            {
                _Highlightcolor = value;
            }
        }




        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
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
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
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
        private string m_PLCAddressMinimum = string.Empty;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressMinimum
        {
            get
            {
                return m_PLCAddressMinimum;
            }
            set
            {
                if (m_PLCAddressMinimum != value)
                {
                    m_PLCAddressMinimum = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressMaximum = string.Empty;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressMaximum
        {
            get
            {
                return m_PLCAddressMaximum;
            }
            set
            {
                if (m_PLCAddressMinimum != value)
                {
                    m_PLCAddressMaximum = value;

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
        //Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        //End Sub

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