
using System;
using System.Linq;


namespace AdvancedScada.DriverBase.Comm
{
    public class PlcComEventArgs : EventArgs, ICloneable
    {
        #region Constructors
        public PlcComEventArgs(byte[] rawData, string plcAddress, ushort sequenceNumber)
        {
            m_RawData = rawData;

            m_PlcAddress = plcAddress;
            m_TransactionNumber = sequenceNumber;

            //* Create a new list of values that will be extracted from the Raw Data
            m_Values = new System.Collections.ObjectModel.Collection<string>();
        }

        public PlcComEventArgs(byte[] rawData, string plcAddress, ushort sequenceNumber, long ownerObjectID)
        {
            m_RawData = rawData;

            m_PlcAddress = plcAddress;
            m_TransactionNumber = sequenceNumber;

            //* Create a new list of values that will be extracted from the Raw Data
            m_Values = new System.Collections.ObjectModel.Collection<string>();
            m_OwnerObjectID = ownerObjectID;
        }

        public PlcComEventArgs(string[] values, string plcAddress, ushort sequenceNumber)
        {
            m_Values = new System.Collections.ObjectModel.Collection<string>(values);
            m_PlcAddress = plcAddress;
            m_TransactionNumber = sequenceNumber;
        }

        public PlcComEventArgs(string[] values, string plcAddress, ushort sequenceNumber, long ownerObjectID)
        {
            m_Values = new System.Collections.ObjectModel.Collection<string>();
            string[] d = new string[values.Length];
            for (var i = 0; i < d.Length; i++)
            {
                m_Values.Add(d[i]);
            }
            //m_Values.AddRange(Values)

            m_PlcAddress = plcAddress;
            m_TransactionNumber = sequenceNumber;

            m_OwnerObjectID = ownerObjectID;
        }

        //* When used for error message
        public PlcComEventArgs(int errorId, string errorMessage)
        {
            m_Values = new System.Collections.ObjectModel.Collection<string>();
            m_ErrorId = errorId;
            m_ErrorMessage = errorMessage;
        }

        public PlcComEventArgs(int errorId, string errorMessage, ushort transactionNumber)
        {
            m_Values = new System.Collections.ObjectModel.Collection<string>();
            m_ErrorId = errorId;
            m_ErrorMessage = errorMessage;
            m_TransactionNumber = transactionNumber;
        }

        public PlcComEventArgs(int errorId, string errorMessage, ushort transactionNumber, long ownerObjectID)
        {
            m_Values = new System.Collections.ObjectModel.Collection<string>();
            m_ErrorId = errorId;
            m_ErrorMessage = errorMessage;
            m_TransactionNumber = transactionNumber;
            m_OwnerObjectID = ownerObjectID;
        }
        #endregion

        #region Properties
        //****************************************
        //* Extracted values from Raw Byte Stream
        //****************************************
        private System.Collections.ObjectModel.Collection<string> m_Values;
        public System.Collections.ObjectModel.Collection<string> Values
        {
            get
            {
                return m_Values;
            }
            set
            {
                m_Values = value;
            }
        }

        //*************************************
        //* Raw data byte stream from response
        //*************************************
        private byte[] m_RawData;
        public byte[] RawData
        {
            get
            {
                return m_RawData;
            }
        }

        private string m_PlcAddress;
        public string PlcAddress
        {
            get
            {
                return m_PlcAddress;
            }
            set
            {
                m_PlcAddress = value;
            }
        }

        private ushort m_TransactionNumber;
        public ushort TransactionNumber
        {
            get
            {
                return m_TransactionNumber;
            }
        }

        private string m_ErrorMessage;
        public string ErrorMessage
        {
            get
            {
                return m_ErrorMessage;
            }
            set
            {
                m_ErrorMessage = value;
            }
        }

        private int m_ErrorId;
        public int ErrorId
        {
            get
            {
                return m_ErrorId;
            }
            set
            {
                m_ErrorId = value;
            }
        }

        private int m_SubscriptionID;
        public int SubscriptionID
        {
            get
            {
                return m_SubscriptionID;
            }
            set
            {
                m_SubscriptionID = value;
            }
        }

        private long m_OwnerObjectID;
        public long OwnerObjectID
        {
            get
            {
                return m_OwnerObjectID;
            }
            set
            {
                m_OwnerObjectID = value;
            }
        }
        #endregion


        public object Clone()
        {
            PlcComEventArgs pea = new PlcComEventArgs(m_ErrorId, m_ErrorMessage, m_TransactionNumber, m_OwnerObjectID);

            pea.PlcAddress = m_PlcAddress;
            pea.m_SubscriptionID = m_SubscriptionID;
            pea.m_Values = m_Values;
            pea.m_ErrorMessage = m_ErrorMessage;
            pea.m_ErrorId = m_ErrorId;
            pea.m_OwnerObjectID = m_OwnerObjectID;
            pea.m_TransactionNumber = m_TransactionNumber;
            pea.m_RawData = m_RawData;



            //*TODO : finish cloning

            return pea;
        }
    }
}