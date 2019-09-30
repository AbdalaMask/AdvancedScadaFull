using AdvancedScada.DriverBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.IBaseService.Common
{
    public delegate void MyException(string msg);
    public delegate void EventUOSListenning(string msg);
    public delegate void EventUOSAccepting(ConnectionState connState, string msg);
}
