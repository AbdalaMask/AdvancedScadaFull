using System;
using System.Linq;

namespace AdvancedScada.WPF.HMIControls.Alarm
{
    public class dgAlarmH
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string TriggerTeg { get; set; }
        public string Message { get; set; }
        public string AlarmType { get; set; }
        public string Status { get; set; }
    }
}
