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
            this.Message = msg;
        }

        public IFaultException(int errorCode = 0, string msg = null)
        {
            this.ErrorCode = errorCode;
            this.Message = msg;
        }

        [DataMember]
        public int ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }

        [DataMember]
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
    }
}
