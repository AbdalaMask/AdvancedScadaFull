using AdvancedScada.DriverBase;

namespace AdvancedScada.IBaseService.Common
{
    public class XCollection
    {
        public static ScadaException EventscadaException;
        public static ScadaLogger EventscadaLogger;
        public static ChannelCount EventChannelCount;
        public static EventLoggingMessage eventLoggingMessage;
        public static Machine CURRENT_MACHINE = null;
       
        public static EventListenning eventAddMessage;
        public static EventConnectionState eventConnectionState;
        public static EventConnectionChanged eventConnectionChanged ;

        public static PvGridChannelGet EventPvGridChannelGet;
        public static PvGridDeviceGet EventPvGridDeviceGet;
        public static PvGridChannelGet EventPvGridDataBlockGet;


    }
}