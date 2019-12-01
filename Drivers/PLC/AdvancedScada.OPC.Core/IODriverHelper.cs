using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.OPC.Core.Drivers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.OPC.Core
{
    public class IODriverHelper : AdvancedScada.Common.IODriver
    {
        #region Flad
        private static Dictionary<string, OpcDaCom> _OpcDaCom = new Dictionary<string, OpcDaCom>();
        private static readonly ManualResetEvent SendDone = new ManualResetEvent(true);
        private static readonly object myLockRead = new object();
        private object LockObject = new object();
        public OpcDaCom opcDaCom;
        public static List<Channel> Channels = new List<Channel>();

        private static int COUNTER;

        private static Task[] taskArray;

        public static bool IsConnected;

        public string Name => "OPC";

        public Image ImageUrl => Properties.Resources.OPC;


        #endregion

        #region SendPackage
        public void SendPackage(OpcDaCom opcDaCom, Channel ch, Device dv, DataBlock db)
        {
            try
            {
                SendDone.WaitOne(-1);
                lock (opcDaCom)
                {
                    var wdArys = opcDaCom.Read(db.DataBlockName, 250, db);
                    if (wdArys == null || wdArys.Length == 0) return;
                    for (var i = 0; i < db.Tags.Count; i++)
                    {
                        db.Tags[i].Value = wdArys[i];
                        db.Tags[i].TimeSpan = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                IsConnected = false;
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

        }
        #endregion

        #region IServiceDriver
        public void InitializeService(Channel ch)
        {
            try
            {
                lock (this)
                {


                    Channels.Add(ch);

                    if (Channels == null) return;
                    // Initialize


                    #region opc.
                    var die = (DIEthernet)ch;
                    opcDaCom = new OpcDaCom(ch.Mode, ch.CPU.Trim());
                    _OpcDaCom.Add(ch.ChannelName, opcDaCom);
                    #endregion Initialize

                    foreach (var dv in ch.Devices)
                    {
                        foreach (var db in dv.DataBlocks)
                        {
                            DataBlockCollection.DataBlocks.Add($"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}", db);
                            foreach (var tg in db.Tags)
                                TagCollection.Tags.Add(
                                    $"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}.{tg.TagName}", tg);
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
                lock (myLockRead)
                {

                    Console.WriteLine("STARTED: {0}", ++COUNTER);
                    taskArray = new Task[Channels.Count];
                    if (taskArray == null) throw new NullReferenceException("No Data");
                    for (var i = 0; i < Channels.Count; i++)
                    {
                        taskArray[i] = new Task(chParam =>
                        {

                            var ch = (Channel)chParam;
                            opcDaCom = _OpcDaCom[ch.ChannelName];
                            if (opcDaCom != null)
                            {
                                opcDaCom.Connection();
                                IsConnected = opcDaCom.IsConnected;
                                while (IsConnected)
                                    foreach (var dv in ch.Devices)
                                    {
                                        foreach (var db in dv.DataBlocks)
                                        {

                                            if (!IsConnected) break;
                                            SendPackage(opcDaCom, ch, dv, db);
                                        }

                                    }

                            }
                        }, Channels[i]);
                        taskArray[i].Start();
                    }
                    foreach (var task in taskArray)
                    {
                        var data = task.AsyncState as Channel;
                        if (data != null)
                            EventscadaException?.Invoke(this.GetType().Name, $"Task #{data.ChannelId} created at {data.ChannelName}, ran on thread #{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}.");


                    }
                }

            }
            catch (Exception ex)
            {
                Disconnect();
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        public void Disconnect()
        {
            IsConnected = false;

        }
        public void WriteTag(string tagName, dynamic value)
        {
            SendDone.Reset();
            var strArrays = tagName.Split('.');
            var dataPacket = new List<byte>();
            try
            {
                var str = $"{strArrays[0]}.{strArrays[1]}";
                foreach (var ch in Channels)
                    foreach (var dv in ch.Devices)
                    {
                        var bEquals = $"{ch.ChannelName}.{dv.DeviceName}".Equals(str);
                        if (bEquals)
                        {
                            opcDaCom = _OpcDaCom[ch.ChannelName];

                            if (opcDaCom == null) return;
                            var obj = opcDaCom;
                            lock (obj)
                            {
                                string[] dataAsArray = { value };
                                opcDaCom.Write($"{TagCollection.Tags[tagName].Address}", 1, dataAsArray);

                            }
                        }
                    }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            finally
            {
                SendDone.Set();
            }
        }


        #endregion
    }
}
