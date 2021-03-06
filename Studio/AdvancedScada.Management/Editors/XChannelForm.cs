﻿using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Management.BLManager;
using ComponentFactory.Krypton.Toolkit;

namespace AdvancedScada.Management.Editors
{
    public partial class XChannelForm : KryptonForm
    {
        public Channel ch = null;
        public EventChannelChanged eventChannelChanged = null;
        public ChannelService objChannelManager = null;
        public XChannelForm()
        {
            InitializeComponent();
        }
    }
}