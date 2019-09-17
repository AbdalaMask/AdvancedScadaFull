using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.Components;
using AdvancedScada.Controls.Drivers;
using AdvancedScada.Controls.Drivers;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.DriverBase.Comm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AdvancedScada.Controls.AHMI.Components
{
    [DefaultEvent("DataChanged")]
    public class HMIDataSubscriber : System.ComponentModel.Component, ISupportInitialize
    {
        public event EventHandler<PlcComEventArgs> DataReturned;
        public event EventHandler<PlcComEventArgs> DataChanged;
        public event EventHandler<PlcComEventArgs> ComError;
        public event EventHandler SuccessfulSubscription;

        protected System.Threading.SynchronizationContext m_synchronizationContext;


        #region Constructor/Destructor
        public HMIDataSubscriber(System.ComponentModel.IContainer container) : this()
        {

            //Required for Windows.Forms Class Composition Designer support
            if (container != null)
            {
                container.Add(this);
            }
        }

        public HMIDataSubscriber() : base()
        {

            m_synchronizationContext = System.Windows.Forms.WindowsFormsSynchronizationContext.Current;
        }

        //****************************************************************
        //* Component overrides dispose to clean up the component list.
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

        #region Basic Properties
        public override ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                base.Site = value;


            }
        }

        protected string m_Value;
        [System.ComponentModel.Browsable(false)]
        public string Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }
        #endregion

        #region PLC Related Properties

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private PLCAddressItem m_PLCAddressValue;
        [System.ComponentModel.Category("PLC Properties")]
        public PLCAddressItem PLCAddressValue
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
                    if (!Initializing)
                    {
                        SubscribeToComDriver();
                    }
                }
            }
        }

        //* Do this to hide it from the Property Window
        [System.ComponentModel.Browsable(false)]
        public System.Collections.Concurrent.ConcurrentDictionary<string, string> SubscribedValues { get; } = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
        #endregion

        #region Events
        private bool Initializing;
        public void BeginInit()
        {
            Initializing = true;
        }

        public void EndInit()
        {
            Initializing = false;

            if (Utilities.client != null)
            {
                SubscribeToComDriver();
            }
        }


        protected virtual void OnDataReturned(PlcComEventArgs e)
        {
            if (m_synchronizationContext != null)
            {
                m_synchronizationContext.Post(DataReceivedSync, e);
            }
            else
            {
                OnDataReceived(e);
            }
        }

        private void DataReceivedSync(object e)
        {
            try
            {
                PlcComEventArgs e1 = (PlcComEventArgs)e;
                OnDataReceived(e1);
            }
            catch (Exception ex)
            {
                //Dim dbg = 0
            }
        }

        protected virtual void OnDataReceived(PlcComEventArgs e)
        {
            DataReturned?.Invoke(this, e);
        }


        protected virtual void OnDataChanged(PlcComEventArgs e)
        {
            DataChanged?.Invoke(this, e);
        }

        //****************************************************************************
        //* This is required to sync the event back to the parent form's main thread
        //****************************************************************************
        private void DataChangedSync(PlcComEventArgs e)
        {
            try
            {
                PlcComEventArgs e1 = e;
                OnDataChanged(e1);
            }
            catch (Exception ex)
            {
                //Dim dbg = 0
            }
        }


        protected virtual void OnSuccessfulSubscription(PlcComEventArgs e)
        {
            SuccessfulSubscription?.Invoke(this, e);
        }

        protected virtual void OnComError(PlcComEventArgs e)
        {
            ComError?.Invoke(this, e);
        }
        #endregion

        #region Subscribing and PLC data receiving
        private bool InvertValue;
        private SubscriptionHandler SubScriptions;
        //**************************************************
        //* Subscribe to addresses in the Comm(PLC) Driver
        //**************************************************
        protected void SubscribeToComDriver()
        {
            if (!DesignMode)
            {
                //If (m_SynchronizingObject Is Nothing OrElse DirectCast(m_SynchronizingObject, Control).IsHandleCreated) Then
                //* Create a subscription handler object
                if (SubScriptions == null)
                {
                    SubScriptions = new SubscriptionHandler();
                    SubScriptions.Parent = this;
                    SubScriptions.DisplayError += DisplaySubscribeError;
                }

                if (m_PLCAddressValue != null && !string.IsNullOrEmpty(m_PLCAddressValue.PLCAddress))
                {
                    string[] AddressList = m_PLCAddressValue.PLCAddress.Split(',');

                    for (var i = 0; i < AddressList.Count(); i++)
                    {
                        if (!SubscribedValues.ContainsKey(AddressList[i]))
                        {
                            //* We must pass the address as a property name so the subscriptionHandler doesn't confuse the next address as a change for the same property
                            SubScriptions.SubscribeTo(AddressList[i], 1, PolledDataReturned, AddressList[i], 1, 0);

                            SubscribedValues.TryAdd(AddressList[i], string.Empty);

                            PlcComEventArgs x = new PlcComEventArgs(0, string.Empty);
                            x.PlcAddress = AddressList[i];
                            OnSuccessfulSubscription(x);
                        }
                    }
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
            if (e.PLCComEventArgs.ErrorId == 0)
            {
                try
                {
                    if (string.IsNullOrEmpty(e.SubscriptionDetail.PropertyNameToSet) || string.Compare(e.SubscriptionDetail.PropertyNameToSet, e.PLCComEventArgs.PlcAddress, true) == 0)
                    {
                        PolledDataReturnedValue(sender, e.PLCComEventArgs);
                    }
                    else if (e.SubscriptionDetail.PropertyNameToSet == "Value")
                    {
                        PolledDataReturnedValue(sender, e.PLCComEventArgs);
                    }
                    else
                    {
                        //* Write the value to the property that came from the end of the PLCAddress... property name
                        try
                        {
                            //* Write the value to the property that came from the end of the PLCAddress... property name
                            this.GetType().GetProperty(e.SubscriptionDetail.PropertyNameToSet).SetValue(this, Utilities.DynamicConverter(e.PLCComEventArgs.Values[0], this.GetType().GetProperty(e.SubscriptionDetail.PropertyNameToSet).PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            //OnDisplayError("INVALID VALUE RETURNED!" & a.PLCComEventArgs.Values(0))
                        }
                    }
                }
                catch (Exception ex)
                {
                    DisplayError("INVALID VALUE!" + ex.Message);
                }
            }
            else
            {
                DisplayError("Com Error " + e.PLCComEventArgs.ErrorId + "." + e.PLCComEventArgs.ErrorMessage);
            }
        }


        //***************************************
        //* Call backs for returned data
        //***************************************
        private string LastValue;
        private void PolledDataReturnedValue(object sender, PlcComEventArgs e)
        {
            try
            {
                //* Fire this event every time data is returned
                OnDataReturned(e);

                //* Case may be switched so find key based on that
                string TargetKey = string.Empty;
                foreach (var key in SubscribedValues.Keys)
                {
                    if (string.Compare(key, e.PlcAddress, true) == 0)
                    {
                        TargetKey = key;
                    }
                }

                int i = 0;
                string TempString = string.Empty;
                string tempValue = string.Empty;
                while (i < e.Values.Count)
                {
                    try
                    {
                        tempValue = this.m_PLCAddressValue.GetScaledValue(e.Values[i]);
                    }
                    catch (Exception ex)
                    {
                        tempValue = "," + "INVALID - Check scale factor/offset - " + e.Values[i];
                    }

                    if (i > 0)
                    {
                        TempString += "," + tempValue;
                    }
                    else
                    {
                        TempString = tempValue;
                    }
                    i += 1;
                }



                if (TempString != SubscribedValues[TargetKey])
                {
                    //* Save this value so we know if it changed without comparing the invert
                    SubscribedValues[e.PlcAddress] = TempString;

                    //* This event is only fired when the returned data has changed
                    OnDataChanged(e);
                }

            }
            catch (Exception ex)
            {
                DisplayError("INVALID VALUE RETURNED!");
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
            if (ErrorDisplayTime == null)
            {
                ErrorDisplayTime = new System.Windows.Forms.Timer();
                ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                ErrorDisplayTime.Interval = 5000;
            }

            //* Save the text to return to
            if (!ErrorDisplayTime.Enabled)
            {
                // OriginalText = Me.Text
            }

            OnComError(new PlcComEventArgs(1, ErrorMessage));

            ErrorDisplayTime.Enabled = true;
        }


        //**************************************************************************************
        //* Return the text back to its original after displaying the error for a few seconds.
        //**************************************************************************************
        private void ErrorDisplay_Tick(object sender, System.EventArgs e)
        {
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