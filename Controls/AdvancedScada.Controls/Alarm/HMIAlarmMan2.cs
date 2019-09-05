using AdvancedScada.Controls.Drivers;
using AdvancedScada.Controls.Subscription;
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
using AdvancedScada;
using AdvancedScada;
 using AdvancedScada.Controls.Drivers;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.Controls.Alarm;
using AdvancedScada;
using AdvancedScada.Controls;

namespace AdvancedScada.Controls.Alarm
{
    [DefaultEvent("DataChanged")]
    public class HMIAlarmMan2 : AdvancedScada.Controls_Net45.AlarmMan
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            drsd = DataReturnedSync;
            dcsd = DataChangedSync;
        }

        public event EventHandler<PlcComEventArgs> DataReturned;
        public event EventHandler<PlcComEventArgs> DataChanged;
        public event EventHandler<PlcComEventArgs> ComError;
        public event EventHandler SuccessfulSubscription;

        #region Constructor/Destructor
        public HMIAlarmMan2()
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }



        }
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
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode && this.Parent != null && this.Parent.Site != null)
            {
                if (base.Site.DesignMode)
                {

                }
            }
            else
            {
                this.Items.Clear();
                SubscribeToComDriver();
            }
        }
        //private void DataSubscriberlistView_DataChanged(string senderPlcAddress, PlcComEventArgs e)
        //{
        //    string LastValue = string.Empty;
        //    if (e.PlcAddress == senderPlcAddress)
        //    {
        //        string[] row0 = { e.PlcAddress, e.Values[0], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") };

        //        if (e.Values[0] != LastValue)
        //        {
        //            LastValue = e.Values[0];
        //            //* Do something here for the value changed
        //            if (LastValue == "True")
        //            {
        //                bool flag = false;
        //                foreach (ListViewItem listViewItem in this.Items)
        //                {
        //                    if (listViewItem.Text == row0[0] && listViewItem.SubItems[2].Text.Substring(14, 2) == row0[2].Substring(14, 2))
        //                    {
        //                        listViewItem.ForeColor = Color.Red;
        //                        if (listViewItem.SubItems[1].Text != row0[1])
        //                        {
        //                            listViewItem.SubItems[1].Text = row0[1];
        //                        }
        //                        if (listViewItem.SubItems[2].Text != row0[2])
        //                        {
        //                            listViewItem.SubItems[2].Text = row0[2];
        //                        }

        //                        flag = true;
        //                        break;
        //                    }
        //                }
        //                if (!flag)
        //                {
        //                    ListViewItem item = new ListViewItem(row0);
        //                    item.ForeColor = Color.Red;
        //                    this.Items.Insert(0, item);
        //                }
        //            }
        //            else if (LastValue == "False")
        //            {
        //                string[] row1 = { e.PlcAddress, e.Values[0], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") };

        //                bool flag = false;
        //                foreach (ListViewItem listViewItem in this.Items)
        //                {
        //                    if (listViewItem.Text == row0[0] && listViewItem.SubItems[2].Text.Substring(14, 2) == row0[2].Substring(14, 2))
        //                    {
        //                        listViewItem.ForeColor = Color.Green;
        //                        if (listViewItem.SubItems[1].Text != row1[1])
        //                        {
        //                            listViewItem.SubItems[1].Text = row1[1];
        //                        }
        //                        if (listViewItem.SubItems[2].Text != row1[2])
        //                        {
        //                            listViewItem.SubItems[2].Text = row1[2];
        //                        }
        //                        flag = true;
        //                        break;
        //                    }
        //                }
        //                if (!flag)
        //                {
        //                    ListViewItem item = new ListViewItem(row1);
        //                    item.ForeColor = Color.Green;
        //                    this.Items.Insert(0, item);
        //                }
        //            }
        //        }
        //    }
        //}

        //**************************************************
        //* Its purpose is to fetch
        //* the main form in order to synchronize the
        //* notification thread/event
        //**************************************************
        protected System.ComponentModel.ISynchronizeInvoke m_SynchronizingObject;
        //* do not let this property show up in the property window
        // <System.ComponentModel.Browsable(False)> _
        public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
        {
            get
            {
                System.ComponentModel.Design.IDesignerHost host1 = null;
                if ((m_SynchronizingObject == null) && base.DesignMode)
                {
                    host1 = (System.ComponentModel.Design.IDesignerHost)this.GetService(typeof(System.ComponentModel.Design.IDesignerHost));
                    if (host1 != null)
                    {
                        m_SynchronizingObject = (System.ComponentModel.ISynchronizeInvoke)host1.RootComponent;
                    }
                }

                return m_SynchronizingObject;
            }

            set
            {
                if (value != null)
                {
                    m_SynchronizingObject = value;
                }
            }
        }

        protected string m_Value;
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


        private int _PollRate;
        public int PollRate
        {
            get
            {
                return _PollRate;
            }
            set
            {
                _PollRate = value;
            }
        }


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
        protected virtual void OnDataReturned(PlcComEventArgs e)
        {
            if (m_SynchronizingObject == null)
            {
                if (DataReturned != null)
                    DataReturned(this, e);
            }
            else
            {
                if (((System.Windows.Forms.Control)m_SynchronizingObject).IsHandleCreated)
                {
                    object[] Parameters = { this, e };
                    SynchronizingObject.BeginInvoke(drsd, Parameters);
                }
            }
        }

        //****************************************************************************
        //* This is required to sync the event back to the parent form's main thread
        //****************************************************************************
        private EventHandler<PlcComEventArgs> drsd;
        private void DataReturnedSync(object sender, PlcComEventArgs e)
        {
            if (DataReturned != null)
                DataReturned(this, e);
        }


        protected virtual void OnDataChanged(PlcComEventArgs e)
        {
            if (m_SynchronizingObject == null)
            {
                if (DataChanged != null)
                    DataChanged(this, e);
            }
            else
            {
                if (((System.Windows.Forms.Control)m_SynchronizingObject).IsHandleCreated)
                {
                    object[] Parameters = { this, e };
                    SynchronizingObject.BeginInvoke(dcsd, Parameters);
                }
            }
        }

        //****************************************************************************
        //* This is required to sync the event back to the parent form's main thread
        //****************************************************************************
        private EventHandler<PlcComEventArgs> dcsd;
        private void DataChangedSync(object sender, PlcComEventArgs e)
        {
            if (DataChanged != null)
                DataChanged(this, e);
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
            if (!this.DesignMode)
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

                m_PLCAddressValueItems.CollectionChanged += SubscribedItemsChanged;

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

                for (var index = 0; index < m_PLCAddressValueItems.Count; index++)
                {
                    if (string.Compare(e.PlcAddress, m_PLCAddressValueItems[index].PLCAddress, true) == 0)
                    {
                        if (e.Values[0] != m_PLCAddressValueItems[index].LastValue)
                        {
                            //* Save this value so we know if it changed without comparing the invert
                            //SubscribedValueList(e.PlcAddress) = e.Values(0)
                            m_PLCAddressValueItems[index].LastValue = e.Values[0];
                            if (!DesignMode)
                            {
                                AlarmMan_DataChanged(m_PLCAddressValueItems[index].PLCAddress, e);

                            }

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