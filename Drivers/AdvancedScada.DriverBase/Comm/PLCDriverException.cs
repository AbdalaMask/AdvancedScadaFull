
using System;
using System.Linq;


namespace AdvancedScada.DriverBase.Comm
{
    [SerializableAttribute()]
    public class PLCDriverException : Exception
    {
        private int _ErrorCode;
        public int ErrorCode
        {
            get
            {
                return _ErrorCode;
            }
            set
            {
                _ErrorCode = value;
            }
        }

        public PLCDriverException()
        {
        }

        public PLCDriverException(string message) : this(message, null)
        {
        }

        public PLCDriverException(int errorCode, string message) : this(message, null)
        {
            _ErrorCode = errorCode;
        }

        public PLCDriverException(Exception innerException) : this((new System.Resources.ResourceManager("ModbusDrivers.Common.en-US", System.Reflection.Assembly.GetExecutingAssembly())).GetString("DF1 Exception"), innerException)
        {
        }

        public PLCDriverException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PLCDriverException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
