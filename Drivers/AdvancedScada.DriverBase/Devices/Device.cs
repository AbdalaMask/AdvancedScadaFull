using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace AdvancedScada.DriverBase.Devices
{
    [Serializable]
    [DataContract]
    public class Device 
    {
      

        public Device()
        {
            DataBlocks = new List<DataBlock>();
        }

        [Browsable(true)]
        [DataMember]
        [Category("Device")]
        public int DeviceId { get; set; }
        [Browsable(true)]
        [DataMember]
        [Category("Device")]
        public short SlaveId { get; set; }
        [DataMember]
        [Browsable(true)]
        [Category("Device")]
        public string DeviceName { get; set; }

       
        [Category("Device")]
        [DataMember]
        [Browsable(true)]
        public string Description { get; set; }

        [Browsable(false)]
        public object ChannelId { get; set; }

        [DisplayName("DataBlocks")]
        [DataMember]
        [Browsable(false)]
        [Category("Device")]
        public List<DataBlock> DataBlocks { get; set; }
       
    }
}
