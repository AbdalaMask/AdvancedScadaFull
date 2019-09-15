using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace AdvancedScada.DriverBase.Devices
{
    [Serializable]
    [DataContract]
    public class Channel
    {

        public Channel()
        {
            Devices = new List<DriverBase.Devices.Device>();
        }
        [DataMember]
        public string ChannelTypes { get; set; }
      
        [DataMember]
        public int ChannelId { get; set; }

        [DataMember]
        public string ChannelName { get; set; }
      
        [DataMember]
        public string CPU { get; set; }

        [DataMember]
        public int Rack { get; set; }

        [DataMember]
        public int Slot { get; set; }

        [DataMember]
        public string Mode { get; set; }

        [DataMember]
        public string ConnectionType { get; set; }

        [DataMember]
        public string Description { get; set; }
        //=====================list=============================
        [DataMember]
        public List<Device> Devices { get; set; }

       
    }
}
