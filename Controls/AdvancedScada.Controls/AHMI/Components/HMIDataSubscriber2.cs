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
    public class HMIDataSubscriber2 : System.ComponentModel.Component, ISupportInitialize
    {
        public event EventHandler<PlcComEventArgs> DataReturned;
        public event EventHandler<PlcComEventArgs> DataChanged;
        public event EventHandler<PlcComEventArgs> ComError;
        public event EventHandler SuccessfulSubscription;

        private System.Threading.SynchronizationContext m_synchronizationContext;

        #region Constructor/Destructor
        public HMIDataSubscriber2(System.ComponentModel.IContainer container) : this()
        {

            //Required for Windows.Forms Class Composition Designer support
            if (container != null)
            {
                container.Add(this);
            }
        }

        public HMIDataSubscriber2() : base()
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
                        SubScriptions.UnsubscribeAll();
                        SubScriptions.Dispose();
                    }
                    m_PLCAddressValueItems.CollectionChanged -= SubscribedItemsChanged;
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

                if ((value != null) && Utilities.client == null)
                {
                    if (base.Site.DesignMode)
                    {

                    }
                }
            }
        }


        #endregion

        #region PLC Related Properties


        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private System.Collections.ObjectModel.ObservableCollection<PLCAddressItem> m_PLCAddressValueItems = new System.Collections.ObjectModel.ObservableCollection<PLCAddressItem>();
        [System.ComponentModel.Category("PLC Properties"), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public System.Collections.ObjectModel.ObservableCollection<PLCAddressItem> PLCAddressValueItems
        {
            get
            {
                return m_PLCAddressValueItems;
            }
        }
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

            m_PLCAddressValueItems.CollectionChanged += SubscribedItemsChanged;
        }


        protected virtual void OnDataReturned(PlcComEventArgs e)
        {
            if (m_synchronizationContext != null)
            {
                m_synchronizationContext.Post(DataReturnedSync, e);
            }
            else
            {
                if (DataReturned != null)
                    DataReturned(this, e);
            }
        }

        //****************************************************************************
        //* This is required to sync the event back to the parent form's main thread
        //****************************************************************************
        //Dim drsd As EventHandler(Of PlcComEventArgs) = AddressOf DataReturnedSync
        private void DataReturnedSync(object e)
        {
            if (DataReturned != null)
                DataReturned(this, (PlcComEventArgs)e);
        }


        protected virtual void OnDataChanged(PlcComEventArgs e)
        {
            if (m_synchronizationContext != null)
            {
                m_synchronizationContext.Post(DataChangedSync, e);
            }
            else
            {
                if (DataChanged != null)
                    DataChanged(this, e);
            }
        }

        //****************************************************************************
        //* This is required to sync the event back to the parent form's main thread
        //****************************************************************************
        private void DataChangedSync(object e)
        {
            if (DataChanged != null)
                DataChanged(this, (PlcComEventArgs)e);
        }


        protected virtual void OnSuccessfulSubscription(PlcComEventArgs e)
        {
            if (SuccessfulSubscription != null)
                SuccessfulSubscription(this, e);
        }

        protected virtual void OnComError(PlcComEventArgs e)
        {
            if (ComError != null)
                ComError(this, e);
        }

        protected virtual void SubscribedItemsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //* Ver 3.99b - Added Not Initializing
            if (!this.DesignMode && !Initializing)
            {
                if (SubScriptions != null)
                {
                    SubScriptions.UnsubscribeAll();
                }
                SubscribeToComDriver();
            }
        }
        #endregion

        #region Subscribing and PLC data receiving

        private SubscriptionHandler SubScriptions;
        //**************************************************
        //* Subscribe to addresses in the Comm(PLC) Driver
        //**************************************************
        protected void SubscribeToComDriver()
        {
            if (!DesignMode)
            {
                //* Create a subscription handler object
                if (SubScriptions == null)
                {
                    SubScriptions = new SubscriptionHandler();
                    SubScriptions.Parent = this;
                    SubScriptions.DisplayError += DisplaySubscribeError;
                }

                for (var index = 0; index < m_PLCAddressValueItems.Count; index++)
                {
                    if (!string.IsNullOrEmpty(m_PLCAddressValueItems[index].PLCAddress))
                    {
                        //* We must pass the address as a property name so the subscriptionHandler doesn't confuse the next address as a change for the same property
                        SubScriptions.SubscribeTo(m_PLCAddressValueItems[index].PLCAddress, m_PLCAddressValueItems[index].NumberOfElements, PolledDataReturned, m_PLCAddressValueItems[index].PLCAddress, 1, 0);

                        PlcComEventArgs x = new PlcComEventArgs(0, string.Empty);
                        x.PlcAddress = m_PLCAddressValueItems[index].PLCAddress;
                        OnSuccessfulSubscription(x);
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
        private void PolledDataReturnedValue(object sender, PlcComEventArgs e)
        {
            try
            {
                //* Fire this event every time data is returned
                OnDataReturned(e);

                //INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
                int i = 0;
                for (var index = 0; index < m_PLCAddressValueItems.Count; index++)
                {
                    if (string.Compare(e.PlcAddress, m_PLCAddressValueItems[index].PLCAddress, true) == 0)
                    {
                        //					Dim i As Integer
                        string tempString = string.Empty;
                        string tempValue = string.Empty;
                        while (i < e.Values.Count)
                        {
                            try
                            {
                                tempValue = m_PLCAddressValueItems[index].GetScaledValue(e.Values[i]);
                            }
                            catch (Exception ex)
                            {
                                tempValue = "," + "INVALID - Check scale factor/offset - " + e.Values[i];
                            }

                            if (i > 0)
                            {
                                tempString += "," + tempValue;
                            }
                            else
                            {
                                tempString = tempValue;
                            }
                            i += 1;
                        }

                        if (m_PLCAddressValueItems[index].LastValue != tempString)
                        {
                            m_PLCAddressValueItems[index].LastValue = tempString;

                            //* This event is only fired when the returned data has changed
                            OnDataChanged(e);
                        }
                    }
                }

            }
            catch
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

        #region Public Methods
        public string GetValueByName(string name)
        {
            int index = 0;
            while (index < m_PLCAddressValueItems.Count)
            {
                if (string.Compare(m_PLCAddressValueItems[index].Name, name, true) == 0)
                {
                    return m_PLCAddressValueItems[index].LastValue;
                }
                index += 1;
            }

            return string.Empty;
        }
        #endregion

    }

}