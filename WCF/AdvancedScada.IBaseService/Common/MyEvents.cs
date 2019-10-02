using AdvancedScada.DriverBase;

namespace AdvancedScada.IBaseService.Common
{
    
    public delegate void EventListenning(string msg);
    public delegate void EventConnectionState(ConnectionState connState, string msg);
}
