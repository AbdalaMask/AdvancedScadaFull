using AdvancedScada.DriverBase.Devices;
using System.Drawing;
using System.Runtime.Serialization;

namespace AdvancedScada.Common
{
    [DataContract]
    public class IODriverWrite
    {
        [DataMember]
        public string TagName { get; set; }
        [DataMember]
        public dynamic Value { get; set; }
    }
    public interface IODriver
    {
        string Name { get; }
        Image ImageUrl { get; }
        void InitializeService(Channel ch);
        void Connect();
        void Disconnect();
        void WriteTag(string TagName, dynamic Value);
    }
}
