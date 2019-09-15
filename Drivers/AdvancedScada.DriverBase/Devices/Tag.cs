using AdvancedScada.DriverBase.Comm;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace AdvancedScada.DriverBase.Devices
{
    [Serializable]
    [DataContract]
    public class Tag : INotifyPropertyChanged
    {
        public delegate void EventValueChanged(dynamic value);
   
     
        private dynamic m_Value;
     
        public EventValueChanged ValueChanged = null;

 
   
        
        [DataMember]
        public int ChannelId { get; set; }


      
        [DataMember]
        public int DataBlockId { get; set; }

        
        [DataMember]
        public int DeviceId { get; set; }

        /// <summary>
        ///     Kiểu dữ liệu.
        /// </summary>
        [DataMember]
        public DataTypes DataType { get; set; }


        [DataMember]
        public dynamic Value
        {
            get { return m_Value; }
            set
            {

                m_Value = value;
                OnPropertyChanged("Value");

            }
        }

        [DataMember]
        public string Address { get; set; }


        [DataMember]
        public string TagName { get; set; }


        [DataMember]
        public int TagId { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public string Description { get; set; }

      
      
       

       
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(newName));
            if (ValueChanged != null) ValueChanged(Value);
        }


       
     
     
    }
}
