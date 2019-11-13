using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvancedScada.IBaseService;
using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;

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
