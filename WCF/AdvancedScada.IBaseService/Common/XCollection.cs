using AdvancedScada.DriverBase;

namespace AdvancedScada.IBaseService.Common
{
    public static class Extension
    {
        public static string NameOf(this object o)
        {
            return o.GetType().Name;
        }
    }
    public class XCollection
    {
        public static ScadaException EventscadaException;
        public static ScadaLogger EventscadaLogger;
        public static ChannelCount EventChannelCount;
        public static EventLoggingMessage eventLoggingMessage;
        public static Machine CURRENT_MACHINE = null;
        public static MyException myException ;
        public static EventUOSListenning eventAddMessage ;
        public static EventUOSAccepting eventUOSAccepting ;


        public static PvGridChannelGet EventPvGridChannelGet;
        public static PvGridDeviceGet EventPvGridDeviceGet;
        public static PvGridChannelGet EventPvGridDataBlockGet;


    }
}