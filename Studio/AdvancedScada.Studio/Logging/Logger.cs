using System.ComponentModel;

namespace AdvancedScada.Studio.Logging
{
    public class Logger

    {
        public static BindingList<Logger> Loggers = new BindingList<Logger>();

        public int ID { get; set; }

        public string LogType { get; set; }

        public string TIME { get; set; }

        public string MESSAGE { get; set; }

    }
}
