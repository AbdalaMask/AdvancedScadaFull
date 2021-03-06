﻿using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AdvancedScada.DriverBase.Devices
{
    [Serializable]
    [DataContract]
    public class Tag : INotifyPropertyChanged
    {
        public delegate void EventValueChanged(dynamic value);


        private int _TagId;

        private string _TagName;

        private dynamic _Value;

        private DateTime _TimeSpan = DateTime.Now;

        private string _Description;

        public EventValueChanged ValueChanged = null;
        private string address;

        [DataMember]
        public int ChannelId { get; set; }

        [DataMember]
        public int DataBlockId { get; set; }


        [DataMember]
        public int DeviceId { get; set; }


        [DataMember]
        public DataTypes DataType { get; set; }

        [DataMember]
        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        [DataMember]
        public int TagId
        {
            get => _TagId;

            set
            {
                _TagId = value;
                OnPropertyChanged("TagId");
            }
        }

        [DataMember]
        public string TagName
        {
            get => _TagName;

            set
            {
                _TagName = value;
                OnPropertyChanged("TagName");
            }
        }

        [DataMember]
        public dynamic Value
        {
            get => _Value;

            set
            {
                _Value = value;
                OnPropertyChanged("Value");
            }
        }

        [DataMember]
        public DateTime TimeSpan
        {
            get => _TimeSpan;

            set
            {
                _TimeSpan = value;
                OnPropertyChanged("TimeSpan");
            }
        }

        [DataMember]
        public string Description
        {
            get => _Description;

            set
            {
                _Description = value;
                OnPropertyChanged("Description");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

    }
}
