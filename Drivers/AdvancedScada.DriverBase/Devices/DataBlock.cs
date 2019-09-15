using AdvancedScada.DriverBase.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AdvancedScada.DriverBase.Devices
{
    [Serializable]
    [DataContract]
    public class DataBlock
    {

        public DataBlock()
        {
            Tags = new List<Tag>();
        }

       
        [DataMember]
        public string Description { get; set; }
        
        [DataMember]
        public DataTypes DataType { get; set; }

        
        [DataMember]
        public ushort Length { get; set; }

      
        [DataMember]
       
        public ushort StartAddress { get; set; }

        
        [DataMember]
      
        public string DataBlockName { get; set; }

       
        [DataMember]
        
        public int DataBlockId { get; set; }

        [DataMember]
         
        public string MemoryType { get; set; }

       
        [DataMember]
       
        public int ChannelId { get; set; }

        
        [DataMember]
       
        public int DeviceId { get; set; }

        [Browsable(false)]
        [DataMember]
        public List<Tag> Tags { get; set; }
        
        [DataMember]
        public string TypeOfRead { get; set; }
      

    }
}
