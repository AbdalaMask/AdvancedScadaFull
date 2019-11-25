using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.Management.AlarmManager
{
  public  class ClassAlarm
    {
        public string Name { get; set; }
        public string AlarmText { get; set; }
        public string AlarmCalss { get; set; }
        public string Value { get; set; }
        public string TriggerTeg { get; set; }
        public string DataBlock { get; set; }
        public string Device { get; set; }
        public string Channel { get; set; }
       
    }
}
