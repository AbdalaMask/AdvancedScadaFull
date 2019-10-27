using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Common;
using AdvancedScada.Controls;
using AdvancedScada.Controls.Drivers;
using AdvancedScada.Controls.Drivers;
using AdvancedScada.Controls.Subscription;
using System;
using System.Collections.Generic;

namespace AdvancedScada.Controls.Subscription
{
    public class SubscriptionHandler : IDisposable
    {


        public event EventHandler<PlcComEventArgs> DisplayError;

        #region Properties
        //*****************************************************
        //* Property - Component to communicate to PLC through
        //*****************************************************
        private IComComponent m_ComComponent;

        public object Parent { get; set; }
        public List<SubscriptionDetail> SubscriptionList { get; }
        #endregion


        #region Constructor/Destructor
        public SubscriptionHandler()
        {
            SubscriptionList = new List<SubscriptionDetail>();
            m_ComComponent = new IComComponent();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //****************************************************************
        //* Control overrides dispose to clean up the component list.
        //****************************************************************
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (m_ComComponent != null)
                    {
                        //* Unsubscribe from all
                        for (int i = 0; i < SubscriptionList.Count; i++)
                        {
                            m_ComComponent.Unsubscribe(SubscriptionList[i].NotificationID);
                        }
                        SubscriptionList.Clear();
                    }
                    if (SubscribeTryTimer != null)
                    {
                        SubscribeTryTimer.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


        //******************************************************
        //* Attempt to create a subscription to the PLC driver
        //******************************************************
        public void SubscribeTo(string PLCAddress, int numberOfElements, EventHandler<SubscriptionHandlerEventArgs> callBack)
        {
            SubscribeTo(PLCAddress, numberOfElements, callBack, string.Empty, 1, 0);
        }

        public void SubscribeTo(string plcAddress, int numberOfElements, EventHandler<SubscriptionHandlerEventArgs> callBack, string propertyName, double ScaleFactor, double ScaleOffset)
        {
            //* Check to see if the subscription has already been created
            int index = 0;
            while (index < SubscriptionList.Count && (SubscriptionList[index].CallBack != callBack || SubscriptionList[index].PropertyNameToSet != propertyName))
            {
                index += 1;
            }

            //* Already subscribed and PLCAddress was changed, so unsubscribe
            if ((index < SubscriptionList.Count) && SubscriptionList[index].PLCAddress != plcAddress)
            {
                m_ComComponent.Unsubscribe(SubscriptionList[index].NotificationID);
                SubscriptionList.RemoveAt(index);
                //* V3.99y - the address changed and old subscription removed, so force the next condition check to re-subscribe
                index = SubscriptionList.Count;
            }

            //* Make sure subscription doesn't already exist - V3.99b
            if (index >= SubscriptionList.Count)
            {
                //* Is there an address to subscribe to?
                if (!string.IsNullOrEmpty(plcAddress))
                {
                    try
                    {
                        if (m_ComComponent != null)
                        {


                            //* If subscription succeedded, save the subscription details
                            SubscriptionDetail temp = new SubscriptionDetail(plcAddress, callBack);
                            temp.PropertyNameToSet = propertyName;

                            temp.NumberOfElements = numberOfElements;
                            if (temp.NumberOfElements <= 0)
                            {
                                temp.NumberOfElements = 1;
                            }

                            if (plcAddress.ToUpper().IndexOf("NOT ") == 0)
                            {
                                temp.Invert = true;
                            }

                            temp.ScaleFactor = ScaleFactor;
                            temp.ScaleOffset = ScaleOffset;

                            SubscriptionList.Add(temp);
                            //* V3.99y - reduced from 500 to 10
                            InitializeTryTimer(10);
                        }
                        else
                        {
                            OnDisplayError("ComComponent Property not set", plcAddress);
                        }
                    }
                    catch (PLCDriverException ex)
                    {
                        //* If subscribe fails, set up for retry
                        InitializeSubscribeTry(ex, plcAddress);
                    }
                }
            }
        }



        public void UnsubscribeAll()
        {
            if (m_ComComponent != null)
            {
                foreach (var Subscript in SubscriptionList)
                {
                    m_ComComponent.Unsubscribe(Subscript.NotificationID);
                }
            }
        }

        private void SubscribeToComDriver()
        {
            if (!m_ComComponent.DisableSubscriptions)
            {
                foreach (var Subscript in SubscriptionList)
                {
                    if (!Subscript.SuccessfullySubscribed)
                    {
                        string address = Subscript.PLCAddress;
                        if (Subscript.Invert)
                        {
                            address = Subscript.PLCAddress.Substring(4);
                        }

                        if (!string.IsNullOrWhiteSpace(address.Trim()))
                        {
                            try
                            {
                                int NotificationID = m_ComComponent.Subscribe(address, (short)Subscript.NumberOfElements, 250, SubscribedDataReturned);
                                Subscript.NotificationID = NotificationID;
                                Subscript.SuccessfullySubscribed = true;
                            }
                            catch (Exception ex)
                            {
                                OnDisplayError(ex.Message, address);
                                PLCDriverException e = new PLCDriverException(ex.Message);
                                //e.Message = ex.Message
                                InitializeSubscribeTry(e, Subscript.PLCAddress);
                            }
                        }
                        else
                        {
                            //* Empty or null address
                            var dbg = 0;
                        }
                    }
                }
            }
            else
            {
                InitializeTryTimer(500);
            }
        }

        public void SubscribeAutoProperties()
        {
            //* Check through the properties looking for PLCAddress***, then see if the suffix matches an existing property
            System.Reflection.PropertyInfo[] p = Parent.GetType().GetProperties();


            for (var i = 0; i <= p.Length - 1; i++)
            {
                //If p(i).Name.IndexOf("YAxis") >= 0 Then
                //    Dim dbg = 0
                //End If
                if ((p[i] != null) && (!string.IsNullOrEmpty(p[i].Name)) && ((p[i].PropertyType) == typeof(string)) || (p[i].PropertyType == typeof(PLCAddressItem))) // (String.Compare(p(i).Name, "Container") <> 0) Then
                {

                    //* Does this property start with "PLCAddress"?
                    if (p[i].Name.IndexOf("PLCAddress", StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        //Console.WriteLine("SAP2")
                        //* Get the property value
                        PLCAddressItem PA = null;
                        string PLCAddress = string.Empty;
                        //* This is to phase in from strings to PLCAddressItem
                        if ((p[i].PropertyType) == typeof(string))
                        {
                            PLCAddress = (string)(p[i].GetValue(Parent, null));
                        }
                        else
                        {
                            PA = (PLCAddressItem)(p[i].GetValue(Parent, null));
                            if (PA != null)
                            {
                                PLCAddress = PA.PLCAddress;
                            }
                        }
                        //* V3.99y beta 32 added true, so clearing an address will unsubscribe
                        if (true || !(string.IsNullOrEmpty(PLCAddress)))
                        {
                            //* Get the text in the name after PLCAddress
                            string PropertyToWrite;

                            PropertyToWrite = p[i].Name.Substring(10);

                            int j = 0;
                            //* See if there is a corresponding property with the extracted name
                            while (j < p.Length && (string.Compare(p[j].Name, PropertyToWrite, true) != 0))
                            {
                                j += 1;
                            }

                            //* If the proprty was found, then subscribe to the PLC Address
                            if (j < p.Length)
                            {
                                if (PA == null)
                                {
                                    SubscribeTo(PLCAddress, 1, null, PropertyToWrite, 1, 0);
                                }
                                else
                                {
                                    SubscribeTo(PLCAddress, 1, null, PropertyToWrite, PA.ScaleFactor, PA.ScaleOffset);
                                }
                            }
                        }
                        else
                        {
                        }
                    }
                }
            }

            //Console.WriteLine("Done with SubscribeAutoProperties")
        }

        private void SubscribedDataReturned(object sender, PlcComEventArgs e)
        {
            double Value = 0;
            foreach (var Subscript in SubscriptionList)
            {
                string address = Subscript.PLCAddress;
                if (Subscript.Invert)
                {
                    address = Subscript.PLCAddress.Substring(4);
                }

                if (e.ErrorId == 0)
                {

                    //If (e.PlcAddress Is Nothing) OrElse (String.Compare(address, e.PlcAddress, True) = 0) Then
                    if ((e.PlcAddress == null) || (e.SubscriptionID == Subscript.NotificationID))
                    {
                        SubscriptionHandlerEventArgs a = new SubscriptionHandlerEventArgs();
                        a.PLCComEventArgs = e;
                        a.SubscriptionDetail = Subscript;

                        if (e.Values != null && e.Values.Count > 0)
                        {
                            //* Check if the value should be inverted
                            if (Subscript.Invert)
                            {
                                //* Try to invert a boolean value
                                try
                                {
                                    PlcComEventArgs ea;
                                    //* Clone the EventArgs for the inversion because there may be another subscription that doesn't need inverted
                                    ea = (PlcComEventArgs)e.Clone();
                                    System.Collections.ObjectModel.Collection<string> x = new System.Collections.ObjectModel.Collection<string>();

                                    string s = (Convert.ToString(!Convert.ToBoolean(e.Values[0])));
                                    x.Add(s);
                                    ea.Values = x;
                                    a.PLCComEventArgs = ea;
                                }
                                catch (Exception ex)
                                {
                                    var dbg = 0;
                                }
                            }
                        }
                        else
                        {
                            //* No data returned from driver
                            e.ErrorId = -999;
                            e.ErrorMessage = "No Values Returned from Driver";

                            try
                            {
                                System.Collections.ObjectModel.Collection<string> x = new System.Collections.ObjectModel.Collection<string>() { e.ErrorMessage };
                                e.Values = x;

                                SubscriptionHandlerEventArgs a1 = new SubscriptionHandlerEventArgs();
                                a1.PLCComEventArgs = e;
                                a1.SubscriptionDetail = Subscript;

                                //Subscript.CallBack.Invoke(sender, a)
                                OnDisplayError(e.ErrorMessage, e.PlcAddress);
                            }
                            catch (Exception ex)
                            {
                                var dbg = 0;
                            }
                        }

                        if (Subscript.CallBack != null)
                        {
                            Subscript.CallBack.Invoke(sender, a);
                        }

                        //********************************************
                        //* Process the AutoProperty subscribed items
                        //********************************************
                        if (this.Parent != null && a.SubscriptionDetail.PropertyNameToSet != null && (string.Compare(a.SubscriptionDetail.PropertyNameToSet, string.Empty) != 0))
                        {
                            if (e.ErrorId == 0)
                            {
                                try
                                {
                                    //* Does this property exist?
                                    if (Parent.GetType().GetProperty(a.SubscriptionDetail.PropertyNameToSet) != null)
                                    {
                                        string v = a.PLCComEventArgs.Values[0];
                                        if (a.SubscriptionDetail.ScaleFactor != 1 || a.SubscriptionDetail.ScaleOffset != 0)
                                        {
                                            //										Dim Value As Double
                                            //* V3.99y - fix for using "," instead of "."
                                            if (double.TryParse(v, System.Globalization.NumberStyles.Number, System.Globalization.NumberFormatInfo.CurrentInfo, out Value))
                                            {
                                                //*If Double.TryParse(v, Value) Then
                                                v = (a.SubscriptionDetail.ScaleFactor * Value + a.SubscriptionDetail.ScaleOffset).ToString();
                                            }
                                        }

                                        //* Write the value to the property that came from the end of the PLCAddress... property name
                                        Parent.GetType().GetProperty(a.SubscriptionDetail.PropertyNameToSet).SetValue(Parent, Utilities.DynamicConverter(v, Parent.GetType().GetProperty(a.SubscriptionDetail.PropertyNameToSet).PropertyType), null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    OnDisplayError("INVALID VALUE RETURNED!" + a.PLCComEventArgs.Values[0], e.PlcAddress);
                                }
                            }
                            else
                            {
                                OnDisplayError("Com Error " + a.PLCComEventArgs.ErrorId + "." + a.PLCComEventArgs.ErrorMessage, e.PlcAddress);
                            }
                        }
                    }
                }
                else
                {
                    //* Error returned from driver
                    try
                    {
                        System.Collections.ObjectModel.Collection<string> x = new System.Collections.ObjectModel.Collection<string>();
                        x.Add(e.ErrorMessage);
                        e.Values = x;

                        SubscriptionHandlerEventArgs a = new SubscriptionHandlerEventArgs();
                        a.PLCComEventArgs = e;
                        a.SubscriptionDetail = Subscript;

                        //Subscript.CallBack.Invoke(sender, a)
                        OnDisplayError(e.ErrorMessage, e.PlcAddress);
                    }
                    catch (Exception ex)
                    {
                        var dbg = 0;
                    }
                }
            }
        }

        //********************************************
        //* Show the error and start the retry time
        //********************************************
        private void InitializeSubscribeTry(PLCDriverException ex, string PLCAddress)
        {

            OnDisplayError(ex.Message, PLCAddress);


            InitializeTryTimer(10000);
        }

        private void InitializeTryTimer(int interval)
        {
            if (SubscribeTryTimer == null)
            {
                SubscribeTryTimer = new System.Windows.Forms.Timer();
                SubscribeTryTimer.Interval = Math.Max(interval, 10);
                SubscribeTryTimer.Tick += SubscribeTry_Tick;
            }

            SubscribeTryTimer.Enabled = true;
        }



        //********************************************
        //* Keep retrying to subscribe if it failed
        //********************************************
        private System.Windows.Forms.Timer SubscribeTryTimer;
        private void SubscribeTry_Tick(object sender, System.EventArgs e)
        {
            SubscribeTryTimer.Enabled = false;
            SubscribeTryTimer.Dispose();
            SubscribeTryTimer = null;

            SubscribeToComDriver();
        }


        protected virtual void OnDisplayError(string msg, string plcAddress)
        {
            PlcComEventArgs e = new PlcComEventArgs(0, msg);
            e.PlcAddress = plcAddress;

            if (DisplayError != null)
                DisplayError(this, e);
        }


    }

}