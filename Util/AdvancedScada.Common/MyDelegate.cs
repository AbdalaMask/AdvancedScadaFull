using AdvancedScada.DriverBase.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.Common
{
    public delegate void ChannelCount(int channelCount, bool isNew);
    public delegate void EventLoggingMessage(string message);
    public delegate void ScadaLogger(int _Id, string _logType, string _time, string _message);
    public delegate void ScadaException(string classname, string erorr);
    public delegate void EventConnectionChanged(ConnectionState status);
    public delegate void PvGridChannelGet(object Value, bool Visible);
    public delegate void PvGridDeviceGet(object Value, bool Visible);
    public delegate void PvGridDataBlockGet(object Value, bool Visible);
    public delegate void EventListenning(string msg);
    public delegate void EventConnectionState(ConnectionState connState, string msg);
    //=====================================================================================
    public delegate void EventSelectedDriversChanged(bool isNew);
    public delegate void EventChannelChanged(Channel ch, bool isNew);
    public delegate void EventDeviceChanged(Device dv, bool isNew);
    public delegate void EventDataBlockChanged(DataBlock db, bool IsNew);
    public delegate void EventTagChanged(Tag tg, bool isNew);
}
