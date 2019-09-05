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
    public class Device : INotifyPropertyChanged
    {
        private string m_Status = "Disconnection";
        private DeviceState _DeviceState = DeviceState.Disconnected;
        private IDriverAdapter _PLC;
        private bool _IsActived = true;

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

        [DataMember]
        public string Status
        {
            get { return m_Status; }

            set
            {
                m_Status = value;
                OnPropertyChanged("Status");
            }
        }
        [Browsable(false)]
        [Display(Name = "PLC")]
        [Category("Device")]
        public IDriverAdapter PLC
        {
            get
            {
                return _PLC;
            }
            set
            {
                _PLC = value;
            }
        }
        [Browsable(true)]
        [DataMember]
        [Category("Device")]
        [DisplayName("DeviceState")]
        public DeviceState DeviceState
        {
            get
            {
                return _DeviceState;
            }
            set
            {
                _DeviceState = value;
                OnPropertyChanged("DeviceState");
            }
        }
        [Browsable(true)]
        [Category("Device")]
        [Display(Name = "IsActived", Order = 7)]
        [DataMember]
        public bool IsActived
        {
            get
            {
                return _IsActived;
            }
            set
            {
                _IsActived = value;
                OnPropertyChanged("IsActived");
            }
        }
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
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(newName));

        }
    }
}
