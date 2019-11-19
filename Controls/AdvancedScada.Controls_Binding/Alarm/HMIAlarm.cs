using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.Alarm
{
    public partial class HMIAlarm : UserControl, IServiceCallback
    {
        public HMIAlarm()
        {
            InitializeComponent();
        }

        public void UpdateCollection(Common.ConnectionState status, Dictionary<string, Tag> collection)
        {
            throw new NotImplementedException();
        }
    }
}
