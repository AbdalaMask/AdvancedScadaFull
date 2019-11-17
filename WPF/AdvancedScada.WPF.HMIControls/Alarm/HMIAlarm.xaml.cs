using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace AdvancedScada.WPF.HMIControls.Alarm
{
    /// <summary>
    /// Interaction logic for HMIAlarm.xaml
    /// </summary>
    public partial class HMIAlarm : UserControl, IServiceCallback
    {
        public HMIAlarm()
        {
            InitializeComponent();
        }

        public void UpdateCollection(ConnectionState status, Dictionary<string, Tag> collection)
        {
            throw new NotImplementedException();
        }
    }

}
