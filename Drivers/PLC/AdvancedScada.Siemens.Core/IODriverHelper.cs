using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.Siemens.Core
{
    public class IODriverHelper : AdvancedScada.DriverBase.IODriver
    {
        #region Flad

        private static Dictionary<string, Profinet.Plc> _PLCS7 = new Dictionary<string, Profinet.Plc>();
        public static readonly ManualResetEvent SendDone = new ManualResetEvent(true);
        public static List<Channel> Channels = new List<Channel>();
        private static Thread[] threads;
       

        private static int COUNTER;
        public static bool IsConnected;

        public string Name => "Siemens";

      
        #endregion

        #region IServiceDriver

        public void InitializeService(Channel chns)
        {
            try
            {
              


               
                if (Channels == null) return;
                Channels.Add(chns);
                Profinet.Plc DriverAdapter = null;

                var die = (DIEthernet)chns;
                var cpu = (CpuType)Enum.Parse(typeof(CpuType), die.CPU);
                DriverAdapter = new Profinet.Plc(cpu, die.IPAddress, (short)die.Rack, (short)die.Slot);

                foreach (Device dv in chns.Devices)
                {
                    _PLCS7.Add(chns.ChannelName, DriverAdapter);
                    foreach (DataBlock db in dv.DataBlocks)
                    {
                        foreach (Tag tg in db.Tags)
                        {
                            TagCollection.Tags.Add(string.Format("{0}.{1}.{2}.{3}", chns.ChannelName, dv.DeviceName, db.DataBlockName,
                                                                    tg.TagName), tg);

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        public void Connect()
        {
            try
            {
                IsConnected = true;


                Console.WriteLine(string.Format("STARTED: {0}", ++COUNTER));
                threads = new Thread[Channels.Count];

                if (threads == null) throw new NullReferenceException("No Data");
                for (int i = 0; i < Channels.Count; i++)
                {
                    threads[i] = new Thread((chParam) =>
                    {
                        Profinet.Plc DriverAdapter = null;
                        Channel ch = (Channel)chParam;
                        DriverAdapter = _PLCS7[ch.ChannelName];
                        //======Connection to PLC==================================
                        DriverAdapter.Connection();

                        while (IsConnected)
                        {
                            try
                            {
                                foreach (Device dv in ch.Devices)
                                {

                                    foreach (DataBlock db in dv.DataBlocks)
                                    {
                                        if (!IsConnected) break;

                                        SendPackage(DriverAdapter,dv, db);


                                    }

                                }
                            }
                            catch (Exception)
                            {
                                Disconnect();
                                objConnectionState = ConnectionState.DISCONNECT;
                                eventConnectionState?.Invoke(objConnectionState, string.Format("Server disconnect with PLC."));
                            }
                            if (IsConnected && objConnectionState == ConnectionState.DISCONNECT)
                            {
                                objConnectionState = ConnectionState.CONNECT;
                                eventConnectionState?.Invoke(objConnectionState, string.Format("PLC connected to Server."));
                            }
                            else if (!IsConnected && objConnectionState == ConnectionState.CONNECT)
                            {
                                objConnectionState = ConnectionState.DISCONNECT;
                                eventConnectionState?.Invoke(objConnectionState, string.Format("Server disconnect with PLC."));
                            }
                        }

                    })
                    {
                        IsBackground = true
                    };
                    threads[i].Start(Channels[i]);
                }

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
         

        }

        public void Disconnect()
        {
            IsConnected = false;
        }
        public void WriteTag(string tagName, dynamic value)
        {

          
            var dataPacket = new List<byte>();
            try
            {
                SendDone.Reset();
                string[] ary = tagName.Split('.');
                string tagDevice = string.Format("{0}.{1}", ary[0], ary[1]);
                foreach (Channel ch in Channels)
                {
                    foreach (var dv in ch.Devices)
                    {
                        var flag = string.Format("{0}.{1}", ch.ChannelName, dv.DeviceName).Equals(tagDevice);
                        if (flag)
                        {
                            Profinet.Plc PLC_S7 = null;
                            PLC_S7 = _PLCS7[ch.ChannelName];

                            if (PLC_S7 == null) return;
                            var obj = PLC_S7;
                            lock (obj)
                            {
                                var dType = TagCollection.Tags[tagName].DataType;

                                switch (dType)
                                {
                                    case AdvancedScada.DriverBase.Comm.DataTypes.Bit:

                                        PLC_S7.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), value == "1" ? true : false);
                                        break;
                                    case AdvancedScada.DriverBase.Comm.DataTypes.Short:
                                    case AdvancedScada.DriverBase.Comm.DataTypes.Int:

                                        short db1IntVariable = short.Parse(value);
                                        PLC_S7.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), db1IntVariable.ConvertToUshort());

                                        break;
                                    case AdvancedScada.DriverBase.Comm.DataTypes.Double:

                                        double db1RealVariable = double.Parse(value);
                                         PLC_S7.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), db1RealVariable.ConvertToUInt());
 
                                        break;
                                    case AdvancedScada.DriverBase.Comm.DataTypes.String:
                                        string db1stringVariable = string.Format("{0}", value);

                                        PLC_S7.WriteString(string.Format("{0}", TagCollection.Tags[tagName].Address), db1stringVariable);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //* Return an error code
                EventscadaException?.Invoke(this.GetType().Name, "Data Error : " + ex.Message);
            }
            finally
            {
                SendDone.Set();
            }
        }

        #endregion



        #region SendPackage

        private void SendPackage(Profinet.Plc PLCS7, Device dv, DataBlock db)
        {

            try
            {
                SendDone.WaitOne(-1);
                var ibyteArray1 = PLCS7.ReadStruct(db, db.StartAddress);


            }

            catch (Exception ex)
            {
                //IsConnected = false;
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);

            }
        }

        #endregion
    }
}
