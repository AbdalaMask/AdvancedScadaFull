
using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.Subscription;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AdvancedScada.Controls.Subscription
{
    public class SubscriptionDetail
    {
        public string PLCAddress { get; set; }
        public string TagAlias { get; set; }
        public int NumberOfElements { get; set; }
        public int NotificationID { get; set; }
        public EventHandler<SubscriptionHandlerEventArgs> CallBack { get; set; }
        public double ScaleFactor { get; set; } = 1;
        public double ScaleOffset { get; set; }
        public string PropertyNameToSet { get; set; }
        public bool Invert { get; set; }
        public bool SuccessfullySubscribed { get; set; }

        public SubscriptionDetail()
        {
        }

        public SubscriptionDetail(string plcAddress, EventHandler<SubscriptionHandlerEventArgs> callback)
        {
            PLCAddress = string.Copy(plcAddress);
            CallBack = callback;
        }


        public SubscriptionDetail(string plcAddress, int notificationID, EventHandler<SubscriptionHandlerEventArgs> callback) : this(plcAddress, callback)
        {
            NotificationID = notificationID;
        }

        public SubscriptionDetail(string plcAddress, int notificationID, EventHandler<SubscriptionHandlerEventArgs> callback, string propertyNameToSet) : this(plcAddress, notificationID, callback)
        {
            PropertyNameToSet = string.Copy(propertyNameToSet);
        }

        public SubscriptionDetail(string plcAddress, int notificationID, EventHandler<SubscriptionHandlerEventArgs> callback, string propertyNameToSet, bool invert) : this(plcAddress, notificationID, callback, propertyNameToSet)
        {
            Invert = invert;
        }

    }

}