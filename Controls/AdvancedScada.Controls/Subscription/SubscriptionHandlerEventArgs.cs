﻿
using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.DriverBase.Comm;
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
    public class SubscriptionHandlerEventArgs : EventArgs
    {
        public PlcComEventArgs PLCComEventArgs { get; set; }
        public SubscriptionDetail SubscriptionDetail { get; set; }
    }

}