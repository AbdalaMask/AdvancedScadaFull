using System.Runtime.Serialization;

namespace AdvancedScada.DriverBase
{
    [DataContract]
    public class IFaultException
    {
        private int _ErrorCode;

        private string _Message;

        public IFaultException(string msg = null)
        {
            Message = msg;
        }

        public IFaultException(int errorCode = 0, string msg = null)
        {
            ErrorCode = errorCode;
            Message = msg;
        }

        [DataMember]
        public int ErrorCode
        {
            get => _ErrorCode;
            set => _ErrorCode = value;
        }

        [DataMember]
        public string Message
        {
            get => _Message;
            set => _Message = value;
        }
    }
}
