
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using AdvancedScada;
using AdvancedScada.Controls.Subscription;
using AdvancedScada;
using AdvancedScada.Controls;

namespace AdvancedScada.Controls.Subscription
{
    public class SubscriptionDetail
    {
        private string m_PLCAddress;
        public string PLCAddress
        {
            get
            {
                return m_PLCAddress;
            }
            set
            {
                m_PLCAddress = value;
            }
        }

        private string m_TagAlias;
        public string TagAlias
        {
            get
            {
                return m_TagAlias;
            }
            set
            {
                m_TagAlias = value;
            }
        }
        private int m_NumberOfElements;
        public int NumberOfElements
        {
            get
            {
                return m_NumberOfElements;
            }
            set
            {
                m_NumberOfElements = value;
            }
        }

        private int m_NotificationID;
        public int NotificationID
        {
            get
            {
                return m_NotificationID;
            }
            set
            {
                m_NotificationID = value;
            }
        }

        private EventHandler<SubscriptionHandlerEventArgs> m_Callback;
        public EventHandler<SubscriptionHandlerEventArgs> CallBack
        {
            get
            {
                return m_Callback;
            }
            set
            {
                m_Callback = value;
            }
        }

        private double m_ScaleFactor = 1;
        public double ScaleFactor
        {
            get
            {
                return m_ScaleFactor;
            }
            set
            {
                m_ScaleFactor = value;
            }
        }

        private double m_ScaleOffset;
        public double ScaleOffset
        {
            get
            {
                return m_ScaleOffset;
            }
            set
            {
                m_ScaleOffset = value;
            }
        }


        private string m_PropertyNameToSet;
        public string PropertyNameToSet
        {
            get
            {
                return m_PropertyNameToSet;
            }
            set
            {
                m_PropertyNameToSet = value;
            }
        }

        private bool m_Invert;
        public bool Invert
        {
            get
            {
                return m_Invert;
            }
            set
            {
                m_Invert = value;
            }
        }

        private bool m_SuccessfullySubscribed;
        public bool SuccessfullySubscribed
        {
            get
            {
                return m_SuccessfullySubscribed;
            }
            set
            {
                m_SuccessfullySubscribed = value;
            }
        }

        public SubscriptionDetail()
        {
        }

        public SubscriptionDetail(string plcAddress, EventHandler<SubscriptionHandlerEventArgs> callback)
        {
            m_PLCAddress = string.Copy(plcAddress);
            m_Callback = callback;
        }


        public SubscriptionDetail(string plcAddress, int notificationID, EventHandler<SubscriptionHandlerEventArgs> callback) : this(plcAddress, callback)
        {
            m_NotificationID = notificationID;
        }

        public SubscriptionDetail(string plcAddress, int notificationID, EventHandler<SubscriptionHandlerEventArgs> callback, string propertyNameToSet) : this(plcAddress, notificationID, callback)
        {
            m_PropertyNameToSet = string.Copy(propertyNameToSet);
        }

        public SubscriptionDetail(string plcAddress, int notificationID, EventHandler<SubscriptionHandlerEventArgs> callback, string propertyNameToSet, bool invert) : this(plcAddress, notificationID, callback, propertyNameToSet)
        {
            m_Invert = invert;
        }

    }

}