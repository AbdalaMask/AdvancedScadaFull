using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        [DataMember]

        public int DeviceId { get; set; }

        [DataMember]

        public short SlaveId { get; set; }
        [DataMember]

        public string DeviceName { get; set; }


        [Category("Device")]
        public string Description { get; set; }

        [Browsable(false)]
        public object ChannelId { get; set; }


        [DataMember]
        [Browsable(false)]
        public List<DataBlock> DataBlocks { get; set; }

    }
}
