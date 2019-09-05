using AdvancedScada.Controls.Subscription;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Linq;
using AdvancedScada;
 
 
 using AdvancedScada.Controls.Subscription;
using AdvancedScada.DriverBase.Comm;
 
using AdvancedScada.Monitor;
using AdvancedScada.Controls.AHMI.Linear;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;

namespace AdvancedScada.Controls.AHMI.Linear
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(HMILinearMeterH), "HMI7Segment.ico")]
    public class HMILinearMeterH : AdvancedScada.Controls_Net45.LinearMeterH
    {




        #region propartas

        private string _TagName;

        [Category("Link TagName")]
        [Browsable(true)]
        public string TagName
        {
            get { return _TagName; }

            set
            {
                _TagName = value;
                try
                {
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        #endregion
        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }
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
        private string OriginalText;

        private System.Windows.Forms.Timer ErrorDisplayTime
        {
            get
            {
                return _ErrorDisplayTime;
            }
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
            if (SuppressErrorDisplay)
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