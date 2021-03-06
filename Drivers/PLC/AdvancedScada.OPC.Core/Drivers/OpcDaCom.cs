﻿using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using Opc;
using Opc.Da;
using System;
using static AdvancedScada.Common.XCollection;
using Server = Opc.Da.Server;
using Factory = OpcCom.Factory;
using Convert = System.Convert;

namespace AdvancedScada.OPC.Core.Drivers
{
    public class OpcDaCom : IDriverAdapter, IDisposable
    {
        private static readonly object mutex = new object();
        private static OpcDaCom _instance;
        public OpcDaCom(string m_OPCServerPath, string m_OPCServer)
        {
            this.m_OPCServerPath = m_OPCServerPath;
            OPCServer = m_OPCServer;
        }

        public OpcDaCom()
        {
        }

        public static OpcDaCom GetOpcDaCom()
        {
            lock (mutex)
            {
                if (_instance == null)
                {
                    _instance = new OpcDaCom();
                }
            }

            return _instance;
        }

        //***************************************************************
        //* Create the Data Link Layer Instances
        //* if the IP Address is the same, then resuse a common instance
        //***************************************************************
        public bool Connection()
        {

            try
            {
                if (DLL == null)
                {
                    if (DLL != null && DLL.IsConnected)
                    {
                        DLL.Disconnect();
                    }

                    DLL = new Server(fact, null)
                    {
                        Url = new URL(m_OPCServerPath + "/" + OPCServer)
                    };
                    DLL.Connect();
                    if (DLL.IsConnected)
                    {
                        EventscadaException?.Invoke(GetType().Name, "ConnectedSuccess");
                        IsConnected = true;
                    }
                    else
                    {
                        EventscadaException?.Invoke(GetType().Name, "ConnectedFailed");
                        IsConnected = false;
                    }

                }
            }
            catch (Exception)
            {
                EventscadaException?.Invoke(GetType().Name, "ConnectedFailed");
                IsConnected = false;
            }
            return IsConnected;
        }

        public bool Disconnection()
        {

            try
            {
                if (DLL != null)
                {
                    if (DLL.IsConnected)
                    {
                        try
                        {
                            DLL.Disconnect();
                        }
                        catch
                        {
                        }
                    }

                    DLL.Dispose();
                    fact.Dispose();
                }


                IsConnected = false;


            }
            catch (Exception ex)
            {


                EventscadaException?.Invoke(GetType().Name, "ConnectedFailed");

                throw ex;
            }
            return IsConnected;
        }


        #region Field
        private int TransactionID;
        //* Create a common instance to share so multiple driver instances can be used in a project
        private Server DLL;
        private readonly Factory fact = new Factory();
        private ISubscription ReadSubscription;
        private SubscriptionState ReadSubscriptionState;
        private readonly SubscriptionState SubscriptionState = new SubscriptionState();
        private Item[] SubscribedItems;
        public Item OPCItem;
        public bool _IsConnected;
        #endregion

        #region Properties
        public bool IsConnected { get; set; } = false;
        private string m_OPCServerPath = "opcda://localhost";
        public string OPCServerPath
        {
            get => m_OPCServerPath;
            set
            {
                m_OPCServerPath = value.TrimEnd();

                //* Strip off the path separator because it is added in CreateDLLInstance
                if (m_OPCServerPath.LastIndexOf("/") == m_OPCServerPath.Length - 1 || m_OPCServerPath.LastIndexOf("\\") == m_OPCServerPath.Length - 1)
                {
                    m_OPCServerPath = m_OPCServerPath.Substring(0, m_OPCServerPath.Length - 1);
                }
            }
        }

        public string OPCServer
        {
            get;
            set;
        } = "OPC.IwSCP";

        public string OPCGroup
        {
            get => oPCGroup;
            set => oPCGroup = value;
        }
        public string OPCTopic
        {
            get => oPCTopic;
            set => oPCTopic = value;
        }


        //**************************************************************
        //* Stop the polling of subscribed data
        //**************************************************************
        private bool m_DisableSubscriptions;
        public bool DisableSubscriptions
        {
            get => m_DisableSubscriptions;
            set
            {
                if (m_DisableSubscriptions != value)
                {
                    m_DisableSubscriptions = value;
                    if (SubscriptionState != null)
                    {
                        SubscriptionState.Active = !m_DisableSubscriptions;
                    }
                }
            }
        }

        private int m_PollRateOverride = 500;
        private string oPCGroup = string.Empty;
        private string oPCTopic;

        public int PollRateOverride
        {
            get => m_PollRateOverride;
            set
            {
                if (value >= 0)
                {
                    m_PollRateOverride = value;
                }
            }
        }
        #endregion
        #region Read/Write Interface to Driver
        public int BeginRead(string startAddress, int numberOfElements)
        {
            if (DLL == null)
            {
                Connection();
            }

            //*********************************************************************
            //* If Async Mode, then return immediately and return value on event
            //*********************************************************************
            if (TransactionID < 32767)
            {
                TransactionID += 1;
            }
            else
            {
                TransactionID = 0;
            }

            if (ReadSubscriptionState == null)
            {
                ReadSubscriptionState = new SubscriptionState
                {
                    Name = "AsyncReadGroup"
                };

                ReadSubscription = DLL.CreateSubscription(ReadSubscriptionState);
            }


            Item[] Items = new Item[1];
            Items[0] = new Item();

            if (OPCTopic != null && string.Compare(OPCTopic, string.Empty) != 0)
            {
                Items[0].ItemName = "[" + OPCTopic + "]";
            }
            else
            {
                Items[0].ItemName = string.Empty;
            }

            Items[0].ItemName += startAddress;

            if (numberOfElements > 1)
            {
                Items[0].ItemName += ",L" + numberOfElements;
            }

            Items[0].SamplingRate = 250;
            Items[0].ClientHandle = TransactionID;

            ItemResult[] ItemRes = ReadSubscription.AddItems(Items);

            for (int i = 0; i < ItemRes.Length; i++)
            {
                Items[i].ServerHandle = ItemRes[i].ServerHandle;
            }

            DLL.Read(Items);

            return TransactionID;
        }


        public string[] Read(string SubscribedGroup, int UpdateRate, DataBlock db)
        {

            lock (this)
            {




                if (DLL == null)
                {
                    Connection();
                }

                SubscribedItems = new Item[db.Tags.Count];


                for (int i = 0; i < db.Tags.Count; i++)
                {
                    SubscribedItems[i] = new Item
                    {
                        ItemName = db.Tags[i].Address,
                        SamplingRate = 50,
                        Active = true,
                        ClientHandle = i
                    };
                }


                ItemValue[] values = DLL.Read(SubscribedItems);

                int ArraySize = 0;
                string[] ReturnValues = new string[values.Length];
                string ArrayValues = string.Empty;
                if (values.Length > 0)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        ArrayValues = string.Empty;
                        if (values[i].Value is Array)
                        {
                            ArraySize = ((Array)values[i].Value).Length;

                            if (ArraySize > 0)
                            {
                                Array ar = (Array)values[i].Value;
                                Array ari = (Array)values[i].Value;

                                int tempVar = ((Array)values[i].Value).Length;

                                foreach (object item in ari)
                                {
                                    ArrayValues += $"/{item}";
                                }

                                ReturnValues[i] = ArrayValues;
                            }
                        }

                        else
                        {
                            ReturnValues[i] = Convert.ToString(values[i].Value);

                        }


                    }
                }
                else
                {
                    ReturnValues[0] = Convert.ToString(values[0].Value);
                }

                return ReturnValues;
            }
        }



        //*************************************************************
        //* Overloaded method of ReadAny - that reads only one element
        //*************************************************************
        public int BeginRead(string startAddress)
        {
            return BeginRead(startAddress, 1);
        }



        //*****************************************************************
        //* Write Section

        public int BeginWrite(string startAddress, int numberOfElements, string[] dataToWrite)
        {
            return Write(startAddress, numberOfElements, dataToWrite);
        }

        public int Write(string startAddress, int numberOfElements, string[] dataToWrite)
        {
            if (DLL == null)
            {
                Connection();
            }

            Item[] items = new Item[1];
            items[0] = new Item();

            //* If there is a topic then add it to the Item Name
            if (!string.IsNullOrEmpty(OPCTopic))
            {
                items[0].ItemName = "[" + OPCTopic + "]" + startAddress;
            }
            else
            {
                items[0].ItemName = startAddress;
            }
            //items(0).ItemName &= startAddress

            //* If Writing multiple elements, add the length to the Item Nam
            if (numberOfElements > 1)
            {
                items[0].ItemName += ",L" + numberOfElements;
            }

            //* Create an array OPC ItemValue
            ItemValue[] Values = new ItemValue[numberOfElements];
            for (int i = 0; i < numberOfElements; i++)
            {
                Values[i] = new ItemValue(items[i])
                {
                    Value = Convert.ToString(dataToWrite[i])
                };
            }

            IdentifiedResult[] x = DLL.Write(Values);


            if (x[0].ResultID == ResultID.S_OK)
            {
                return 0;
            }

            EventscadaException?.Invoke(GetType().Name, "Failed to Write to " + items[0].ItemName);
            return 0;
        }

        public int Write(string startAddress, string dataToWrite)
        {
            string[] temp = new string[1];
            temp[0] = dataToWrite;
            return Write(startAddress, 1, temp);
        }

        public void Dispose()
        {
            try
            {
                if (DLL != null)
                {
                    if (DLL.IsConnected)
                    {
                        try
                        {
                            DLL.Disconnect();
                        }
                        catch
                        {
                        }
                    }

                    DLL.Dispose();
                    fact.Dispose();
                }


                IsConnected = false;


            }
            catch (Exception)
            {


                EventscadaException?.Invoke(GetType().Name, "ConnectedFailed");

            }
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public bool ReadSingle(string address, ushort length)
        {
            throw new NotImplementedException();
        }
        public TValue Read<TValue>(string address)
        {
            throw new NotImplementedException();
        }
        public bool Write(string address, dynamic value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
