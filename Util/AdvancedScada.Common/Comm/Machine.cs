using System.Runtime.Serialization;

namespace AdvancedScada.Common
{


    [DataContract]
    public class Machine
    {

        private int _MachineId;

        private string _MachineName;

        private string _IPAddress;

        private string _Description;

        [DataMember]
        public int MachineId
        {
            get => _MachineId;

            set => _MachineId = value;
        }

        [DataMember]
        public string MachineName
        {
            get => _MachineName;

            set => _MachineName = value;
        }

        [DataMember]
        public string IPAddress
        {
            get => _IPAddress;

            set => _IPAddress = value;
        }

        [DataMember]
        public string Description
        {
            get => _Description;

            set => _Description = value;
        }
    }
}
