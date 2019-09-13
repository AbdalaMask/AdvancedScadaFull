
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AdvancedScada;
using AdvancedScada;
using AdvancedScada.BaseService.Client;
using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Comm;

using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using AdvancedScada.IBaseService.Common;

using static AdvancedScada.IBaseService.Common.XCollection;
using AdvancedScada.Controls.Subscription;
using AdvancedScada;
using AdvancedScada.Controls;

namespace AdvancedScada.Controls.Subscription
{
    [CallbackBehavior(UseSynchronizationContext = true)]
    public class IComComponent : IServiceCallback
    {

        private static List<SubscriptionInfo> SubscriptionList = new List<SubscriptionInfo>();

        public event EventHandler<PlcComEventArgs> DataReceived;
        public event EventHandler<PlcComEventArgs> ComError;
        private struct SubscriptionInfo
        {
            public string Address;
            public EventHandler<PlcComEventArgs> dlgCallBack;
            public int PollRate;
            public int PollRateDivisor;
            public int ID;
            public int ElementsToRead;
        }
        public IComComponent()
        {

        }
        //***************************************************************
        //* Create the Data Link Layer Instances
        //* if the IP Address is the same, then resuse a common instance
        //***************************************************************
        private void CreateDLLInstance()
        {
            if (AdvancedScada.Controls.Utilities.client == null)
            {
                try
                {



                    var ic = new InstanceContext(this);
                    XCollection.CURRENT_MACHINE = new Machine
                    {
                        MachineName = Environment.MachineName,
                        Description = "Free"
                    };
                    IPAddress[] hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
                    foreach (IPAddress iPAddress in hostAddresses)
                    {
                        if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                        {
                            XCollection.CURRENT_MACHINE.IPAddress = $"{iPAddress}";
                            break;
                        }
                    }
                    AdvancedScada.Controls.Utilities.client = DriverHelper.GetInstance().GetReadService(ic);
                    AdvancedScada.Controls.Utilities.client.Connect(XCollection.CURRENT_MACHINE);


                }
                catch (Exception ex)
                {

                    EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                }



            }
        }

        //**************************************************************
        //* Stop the polling of subscribed data
        //**************************************************************
        private bool m_DisableSubscriptions;
        public bool DisableSubscriptions
        {
            get
            {
                return m_DisableSubscriptions;
            }
            set
            {
                if (m_DisableSubscriptions != value)
                {
                    m_DisableSubscriptions = value;

                }
            }
        }

        private int m_PollRateOverride = 500;
        [System.ComponentModel.Category("Communication Settings")]
        public int PollRateOverride
        {
            get
            {
                return m_PollRateOverride;
            }
            set
            {
                if (value >= 0)
                {
                    m_PollRateOverride = value;
                }
            }
        }

        private object obj_lock = new object();

        private int CurrentID;

        public int Subscribe(string plcAddress, short numberOfElements, int pollRate, EventHandler<PlcComEventArgs> callback)
        {
            SubscriptionInfo tmpPA = new SubscriptionInfo(); 
            try
            {
               if (AdvancedScada.Controls.Utilities.client == null)
            {
                CreateDLLInstance();
            }

            if (m_PollRateOverride != 0)
            {
                pollRate = m_PollRateOverride;
            }
            else
            {
                //* Poll rate is in 50ms increments
                pollRate = Convert.ToInt32(Math.Ceiling(pollRate / 50.0) * 50);
                //* Avoid a 0 poll rate
                if (pollRate <= 0)
                {
                    pollRate = 500;
                }
            }
            //***********************************************************
            //* Check if there was already a subscription made for this
            //***********************************************************
            int index = 0;

            while (index < SubscriptionList.Count && (SubscriptionList[index].Address != plcAddress || SubscriptionList[index].dlgCallBack != callback))
            {
                index += 1;
            }


            //* If a subscription was already found, then returns it's ID
            if (index < SubscriptionList.Count)
            {
                //* Return the subscription that already exists
                return SubscriptionList[index].ID;
            }
            else
            {
                //* The ID is used as a reference for removing polled addresses
                CurrentID += 1;

                tmpPA = new SubscriptionInfo();

                tmpPA.PollRate = pollRate;

                //* Poll rate is only in increments of 50ms
                tmpPA.PollRateDivisor = Convert.ToInt32(pollRate / 50.0);
                if (tmpPA.PollRateDivisor <= 0)
                {
                    tmpPA.PollRateDivisor = 1;
                }

                tmpPA.dlgCallBack = callback;
                tmpPA.ID = CurrentID;
                tmpPA.Address = plcAddress;
                tmpPA.ElementsToRead = numberOfElements;

                SubscriptionList.Add(tmpPA);
                SubscriptionList.Sort(SortPolledAddresses);


               
            }
            }
            catch (Exception ex)
            {

                OnComError(new PlcComEventArgs(1, "INVALID VALUE!" + ex.Message));
            }
          
          return tmpPA.ID;

        }
        //***************************************************************
        //* Used to sort polled addresses by File Type and element
        //* This helps in optimizing reading
        //**************************************************************
        private int SortPolledAddresses(SubscriptionInfo A1, SubscriptionInfo A2)
        {
            if ((A1.Address != A2.Address))
            {
                return 1;
            }
            else if (A1.Address == A2.Address)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public int Unsubscribe(int id)
        {
            try
            {
                int i = 0;
                while (i < SubscriptionList.Count && SubscriptionList[i].ID != id)
                {
                    i += 1;
                }

                if (i < SubscriptionList.Count)
                {
                    int PollRate = SubscriptionList[i].PollRate;
                    SubscriptionList.RemoveAt(i);
                    if (SubscriptionList.Count == 0)
                    {
                    }
                    else
                    {
                        //* Check if no more subscriptions to this poll rate
                        int j = 0;
                        bool StillUsed = false;
                        while (j < SubscriptionList.Count)
                        {
                            if (SubscriptionList[j].PollRate == PollRate)
                            {
                                StillUsed = true;
                            }
                            j += 1;
                        }
                    }
                }
                AdvancedScada.Controls.Utilities.client.Disconnect(XCollection.CURRENT_MACHINE);
            }
            catch (Exception ex)
            {

                OnComError(new PlcComEventArgs(1, "INVALID VALUE!" + ex.Message));
            }

            return 0;
        }


        protected virtual void OnComError(PlcComEventArgs e)
        {
            ComError?.Invoke(this, e);
        }
        public void DataTags(Dictionary<string, Tag> Tags)
        {
            int i = 0;
            int SavedCount = SubscriptionList.Count;

            lock (obj_lock)
            {

                foreach (var Subscript in SubscriptionList.ToList())
                {
                    try
                    {
                        string address = Subscript.Address;
                        string v = $"{ Tags[address].Value}";
                        string[] ReturnedValues = { v };

                        PlcComEventArgs f = new PlcComEventArgs(ReturnedValues, Subscript.Address, 0);
                        f.PlcAddress = Subscript.Address;
                        f.SubscriptionID = Subscript.ID;
                        object[] z = { this, f };
                        Subscript.dlgCallBack(this, f);
                    }
                    catch (Exception ex)
                    {

                        OnComError(new PlcComEventArgs(1, "INVALID VALUE!" + ex.Message));
                    }



                }
            }
        }

    }

}