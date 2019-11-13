using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.WPF.HMIControls.Alarm
{
    public class dgAlarmH
    {
        public int No { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public string AlarmType { get; set; }
        public string Status { get; set; }
    }
}
