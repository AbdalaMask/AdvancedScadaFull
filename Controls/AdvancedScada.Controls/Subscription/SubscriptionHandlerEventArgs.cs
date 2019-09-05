
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
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.Controls.Subscription;
using AdvancedScada;
using AdvancedScada.Controls;

namespace AdvancedScada.Controls.Subscription
{
    public class SubscriptionHandlerEventArgs : EventArgs
    {


        private PlcComEventArgs m_PLCComEventArgs;
        public PlcComEventArgs PLCComEventArgs
        {
            get
            {
                return m_PLCComEventArgs;
            }
            set
            {
                m_PLCComEventArgs = value;
            }
        }

        private SubscriptionDetail m_SubscriptionDetail;
        public SubscriptionDetail SubscriptionDetail
        {
            get
            {
                return m_SubscriptionDetail;
            }
            set
            {
                m_SubscriptionDetail = value;
            }
        }
    }

}