using AdvancedScada.Common;
using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Siemens.Core.Common;
using AdvancedScada.Siemens.Core.Siemens;
using HslCommunication.Profinet.Siemens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Siemens.Core
{
    public class IODriverHelper : AdvancedScada.Common.IODriver
    {
        public static readonly ManualResetEvent SendDone = new ManualResetEvent(true);
        public static List<Channel> Channels = new List<Channel>();

        //==================================Siemens===================================================
        private static readonly Dictionary<string, SiemensNet> _PLCS7 = new Dictionary<string, SiemensNet>();
        private static readonly Dictionary<string, SiemensComPPI> _PLCPPI = new Dictionary<string, SiemensComPPI>();
        private static bool IsConnected;
        private static int COUNTER;
        private static Task[] taskArray;

        #region IServiceDriver
        public string Name => "Siemens";
        public Image ImageUrl => Properties.Resources.PLC_SIEMENS;
        public void InitializeService(Channel ch)
        {

            try
            {

                //=================================================================

                if (Channels == null)
                {
                    return;
                }

                Channels.Add(ch);


                IPLCS7Adapter DriverAdapter = null;
                foreach (Device dv in ch.Devices)
                {
                    try
                    {
                        switch (ch.ConnectionType)
                        {
                            case "SerialPort":
                                DISerialPort dis = (DISerialPort)ch;
                                SerialPort sp = new SerialPort(dis.PortName, dis.BaudRate, dis.Parity, dis.DataBits, dis.StopBits)
                                {
                                    Handshake = dis.Handshake
                                };

                                DriverAdapter = new SiemensComPPI(dv.SlaveId, sp);
                                _PLCPPI.Add(ch.ChannelName, (SiemensComPPI)DriverAdapter);
                                break;
                            case "Ethernet":
                                DIEthernet die = (DIEthernet)ch;
                                SiemensPLCS cpu = (SiemensPLCS)Enum.Parse(typeof(SiemensPLCS), die.CPU);
                                DriverAdapter = new SiemensNet(cpu, die.IPAddress, (short)die.Rack, (short)die.Slot);
                                _PLCS7.Add(ch.ChannelName, (SiemensNet)DriverAdapter);

                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        EventscadaException?.Invoke(GetType().Name, ex.Message);
                    }
                    foreach (DataBlock db in dv.DataBlocks)
                    {
                        DataBlockCollection.DataBlocks.Add($"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}", db);
                        foreach (Tag tg in db.Tags)
                        {
                            TagCollection.Tags.Add(
                                $"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}.{tg.TagName}", tg);

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);

            }
        }


        public void Connect()
        {

            try
            {
                IsConnected = true;


                Console.WriteLine(string.Format("STARTED: {0}", ++COUNTER));
                taskArray = new Task[Channels.Count];
                if (taskArray == null)
                {
                    throw new NullReferenceException("No Data");
                }

                for (int i = 0; i < Channels.Count; i++)
                {
                    taskArray[i] = new Task((chParam) =>
                    {
                        IPLCS7Adapter DriverAdapter = null;
                        Channel ch = (Channel)chParam;
                        try
                        {
                            switch (ch.ConnectionType)
                            {
                                case "SerialPort":
                                    DriverAdapter = _PLCPPI[ch.ChannelName];
                                    break;

                                case "Ethernet":
                                    DriverAdapter = _PLCS7[ch.ChannelName];
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Disconnect();
                            EventscadaException?.Invoke(GetType().Name, ex.Message);
                        }



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
                                        if (!IsConnected)
                                        {
                                            break;
                                        }

                                        SendPackageSiemens(DriverAdapter, dv, db);
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

                    }, Channels[i]);
                    taskArray[i].Start();
                    foreach (Task task in taskArray)
                    {
                        Channel data = task.AsyncState as Channel;
                        if (data != null)
                        {
                            EventscadaException?.Invoke(GetType().Name, $"Task #{data.ChannelId} created at {data.ChannelName}, ran on thread #{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        public void Disconnect()
        {

            try
            {
                IsConnected = false;


                Channels = null;
                for (int i = 0; i < taskArray.Length; i++)
                {

                    taskArray[i].Wait(100);
                }

                objConnectionState = ConnectionState.DISCONNECT;
                eventConnectionState?.Invoke(objConnectionState, string.Format("Server disconnect with PLC."));
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);

            }
        }

        #endregion
        #region SendPackage All
        private void SendPackageSiemens(IPLCS7Adapter ISiemens, Device dv, DataBlock db)
        {
            try
            {
                if (!db.IsArray)
                {
                    lock (ISiemens)
                    {
                        ISiemens.ReadStruct(db, db.StartAddress);

                    }
                }
                else
                {
                    switch (db.DataType)
                    {
                        case DataTypes.Bit:

                            lock (ISiemens)
                            {

                                bool[] bitRs = ISiemens.Read<bool>($"{db.MemoryType}{db.StartAddress}", db.Length);

                                int length = bitRs.Length;
                                if (bitRs.Length > db.Tags.Count)
                                {
                                    length = db.Tags.Count;
                                }

                                for (int j = 0; j < length; j++)
                                {
                                    db.Tags[j].Value = bitRs[j];
                                    db.Tags[j].TimeSpan = DateTime.Now;
                                }
                            }
                            break;
                        case DataTypes.Short:

                            lock (ISiemens)
                            {
                                short[] IntRs = ISiemens.Read<short>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (IntRs.Length > db.Tags.Count)
                                {
                                    return;
                                }

                                for (int j = 0; j < IntRs.Length; j++)
                                {

                                    db.Tags[j].Value = IntRs[j];
                                    db.Tags[j].TimeSpan = DateTime.Now;
                                }
                            }
                            break;
                        case DataTypes.Int:

                            lock (ISiemens)
                            {
                                int[] DIntRs = ISiemens.Read<int>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (DIntRs.Length > db.Tags.Count)
                                {
                                    return;
                                }

                                for (int j = 0; j < DIntRs.Length; j++)
                                {
                                    db.Tags[j].Value = DIntRs[j];
                                    db.Tags[j].TimeSpan = DateTime.Now;
                                }
                            }
                            break;
                        case DataTypes.UInt:

                            lock (ISiemens)
                            {
                                uint[] wdRs = ISiemens.Read<uint>($"{db.MemoryType}{db.StartAddress}", db.Length);
                                if (wdRs == null)
                                {
                                    return;
                                }

                                if (wdRs.Length > db.Tags.Count)
                                {
                                    return;
                                }

                                for (int j = 0; j < wdRs.Length; j++)
                                {

                                    db.Tags[j].Value = wdRs[j];

                                    db.Tags[j].TimeSpan = DateTime.Now;
                                }
                            }
                            break;
                        case DataTypes.Long:

                            lock (ISiemens)
                            {
                                long[] dwRs = ISiemens.Read<long>($"{db.MemoryType}{db.StartAddress}", db.Length);

                                for (int j = 0; j < dwRs.Length; j++)
                                {
                                    db.Tags[j].Value = dwRs[j];
                                    db.Tags[j].TimeSpan = DateTime.Now;
                                }
                            }
                            break;
                        case DataTypes.Float:

                            lock (ISiemens)
                            {
                                float[] rl1Rs = ISiemens.Read<float>($"{db.MemoryType}{db.StartAddress}", db.Length);

                                for (int j = 0; j < rl1Rs.Length; j++)
                                {
                                    db.Tags[j].Value = rl1Rs[j];
                                    db.Tags[j].TimeSpan = DateTime.Now;
                                }
                            }
                            break;
                        case DataTypes.Double:

                            lock (ISiemens)
                            {
                                double[] rl2Rs = ISiemens.Read<double>($"{db.MemoryType}{db.StartAddress}", db.Length);

                                for (int j = 0; j < rl2Rs.Length; j++)
                                {
                                    db.Tags[j].Value = rl2Rs[j];
                                    db.Tags[j].TimeSpan = DateTime.Now;
                                }
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Disconnect();
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        #endregion
        #region Write All


        public void WriteTag(string tagName, dynamic value)
        {
            try
            {
                SendDone.Reset();
                string[] ary = tagName.Split('.');
                string tagDevice = string.Format("{0}.{1}", ary[0], ary[1]);
                foreach (Channel ch in Channels)
                {
                    foreach (Device dv in ch.Devices)
                    {

                        if (string.Format("{0}.{1}", ch.ChannelName, dv.DeviceName).Equals(tagDevice))
                        {
                            IPLCS7Adapter DriverAdapter = null;
                            switch (ch.ConnectionType)
                            {
                                case "SerialPort":
                                    DriverAdapter = _PLCPPI[ch.ChannelName];
                                    break;

                                case "Ethernet":
                                    DriverAdapter = _PLCS7[ch.ChannelName];
                                    break;
                            }
                            if (DriverAdapter == null)
                            {
                                return;
                            }

                            lock (DriverAdapter)
                            {
                                switch (TagCollection.Tags[tagName].DataType)
                                {
                                    case DataTypes.Bit:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), value == true ? true : false);
                                        break;
                                    case DataTypes.Byte:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), byte.Parse(value));

                                        break;
                                    case DataTypes.Short:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), short.Parse(value));

                                        break;
                                    case DataTypes.UShort:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), ushort.Parse(value));

                                        break;
                                    case DataTypes.Int:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), int.Parse(value));

                                        break;
                                    case DataTypes.UInt:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), uint.Parse(value));

                                        break;
                                    case DataTypes.Long:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), long.Parse(value));

                                        break;
                                    case DataTypes.ULong:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), ulong.Parse(value));

                                        break;
                                    case DataTypes.Float:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), float.Parse(value));

                                        break;
                                    case DataTypes.Double:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), double.Parse(value));

                                        break;
                                    case DataTypes.String:
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), $"{value}");

                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
            finally
            {
                SendDone.Set();
            }
        }

        #endregion
    }
}
