﻿using AdvancedScada.Common;
using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Modbus.Common;
using AdvancedScada.Modbus.Core.Modbus.ASCII;
using AdvancedScada.Modbus.Core.Modbus.RTU;
using AdvancedScada.Modbus.Core.Modbus.TCP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Modbus.Core
{
    public partial class IODriverHelper : AdvancedScada.Common.IODriver
    {
        public static readonly ManualResetEvent SendDone = new ManualResetEvent(true);
        public static List<Channel> Channels = new List<Channel>();

        //==================================Modbus===================================================
        private static readonly Dictionary<string, ModbusTCPMaster> mbe = new Dictionary<string, ModbusTCPMaster>();
        private static readonly Dictionary<string, ModbusRTUMaster> rtu = new Dictionary<string, ModbusRTUMaster>();
        private static readonly Dictionary<string, ModbusASCIIMaster> ascii = new Dictionary<string, ModbusASCIIMaster>();

        private static Task[] taskArray;
        private static bool IsConnected;
        private static int COUNTER;
        #region IServiceDriver
        public string Name => "Modbus";
        public Image ImageUrl => Properties.Resources.Modbus;
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
                IModbusAdapter DriverAdapter = null;
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


                                switch (dis.Mode)
                                {
                                    case "RTU":
                                        DriverAdapter = new ModbusRTUMaster(dv.SlaveId, sp);
                                        rtu.Add(ch.ChannelName, (ModbusRTUMaster)DriverAdapter);
                                        break;
                                    case "ASCII":
                                        DriverAdapter = new ModbusASCIIMaster(dv.SlaveId, sp);
                                        ascii.Add(ch.ChannelName, (ModbusASCIIMaster)DriverAdapter);
                                        break;
                                }

                                break;
                            case "Ethernet":
                                DIEthernet die = (DIEthernet)ch;
                                DriverAdapter = new ModbusTCPMaster(dv.SlaveId, die.IPAddress, die.Port);
                                mbe.Add(ch.ChannelName, (ModbusTCPMaster)DriverAdapter);
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
                        IModbusAdapter DriverAdapter = null;
                        Channel ch = (Channel)chParam;

                        switch (ch.Mode)
                        {
                            case "RTU":
                                DriverAdapter = rtu[ch.ChannelName];
                                break;
                            case "ASCII":
                                DriverAdapter = ascii[ch.ChannelName];
                                break;
                            case "TCP":
                                DriverAdapter = mbe[ch.ChannelName];
                                break;
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

                                        SendPackageModbus(DriverAdapter, db);


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
                }
                //Task.WaitAll(taskArray);
                foreach (Task task in taskArray)
                {
                    Channel data = task.AsyncState as Channel;
                    if (data != null)
                    {
                        EventscadaException?.Invoke(GetType().Name, $"Task #{data.ChannelId} created at {data.ChannelName}, ran on thread #{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}.");
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

                TagCollection.Tags.Clear();
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

        private void SendPackageModbus(IModbusAdapter DriverAdapter, DataBlock db)
        {
            try
            {
                SendDone.WaitOne(-1);
                switch (db.DataType)
                {
                    case DataTypes.Bit:
                        lock (DriverAdapter)
                        {
                            bool[] bitRs = null;
                            if (db.TypeOfRead == "ReadCoilStatus")
                            {
                                bitRs = DriverAdapter.Read<bool>($"{db.StartAddress}", db.Length);
                            }
                            else if (db.TypeOfRead == "ReadInputStatus")
                            {
                                bitRs = DriverAdapter.ReadDiscrete($"{db.StartAddress}", db.Length);
                            }

                            if (bitRs == null)
                            {
                                return;
                            }

                            if (bitRs.Length > db.Tags.Count)
                            {
                                return;
                            }

                            for (int j = 0; j < db.Tags.Count; j++)
                            {
                                db.Tags[j].Value = bitRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Byte:
                        lock (DriverAdapter)
                        {
                            byte[] byteRs = DriverAdapter.Read<byte>($"{db.StartAddress}", db.Length);
                            if (byteRs == null)
                            {
                                return;
                            }

                            if (byteRs.Length > db.Tags.Count)
                            {
                                return;
                            }

                            for (int j = 0; j < db.Tags.Count; j++)
                            {
                                db.Tags[j].Value = byteRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Short:
                        lock (DriverAdapter)
                        {
                            short[] IntRs = DriverAdapter.Read<short>($"{db.StartAddress}", db.Length);
                            if (IntRs == null)
                            {
                                return;
                            }

                            if (IntRs.Length > db.Tags.Count)
                            {
                                return;
                            }

                            for (int j = 0; j < db.Tags.Count; j++)
                            {
                                db.Tags[j].Value = IntRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.UShort:
                        lock (DriverAdapter)
                        {
                            ushort[] IntRs = DriverAdapter.Read<ushort>($"{db.StartAddress}", db.Length);
                            if (IntRs == null)
                            {
                                return;
                            }

                            if (IntRs.Length > db.Tags.Count)
                            {
                                return;
                            }

                            for (int j = 0; j < db.Tags.Count; j++)
                            {
                                db.Tags[j].Value = IntRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Int:
                        lock (DriverAdapter)
                        {
                            int[] DIntRs = DriverAdapter.Read<int>(string.Format("{0}", db.StartAddress), db.Length);
                            if (DIntRs == null)
                            {
                                return;
                            }

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
                        lock (DriverAdapter)
                        {
                            uint[] wdRs = DriverAdapter.Read<uint>($"{db.StartAddress}", db.Length);

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
                        lock (DriverAdapter)
                        {
                            long[] dwRs = DriverAdapter.Read<long>(string.Format("{0}", db.StartAddress), db.Length);
                            if (dwRs == null)
                            {
                                return;
                            }

                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.ULong:
                        lock (DriverAdapter)
                        {
                            ulong[] dwRs = DriverAdapter.Read<ulong>(string.Format("{0}", db.StartAddress), db.Length);
                            if (dwRs == null)
                            {
                                return;
                            }

                            for (int j = 0; j < dwRs.Length; j++)
                            {
                                db.Tags[j].Value = dwRs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Float:
                        lock (DriverAdapter)
                        {
                            float[] rl1Rs = DriverAdapter.Read<float>(string.Format("{0}", db.StartAddress), db.Length);
                            if (rl1Rs == null)
                            {
                                return;
                            }

                            for (int j = 0; j < rl1Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl1Rs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.Double:
                        lock (DriverAdapter)
                        {
                            double[] rl2Rs = DriverAdapter.Read<double>(string.Format("{0}", db.StartAddress), db.Length);
                            if (rl2Rs == null)
                            {
                                return;
                            }

                            for (int j = 0; j < rl2Rs.Length; j++)
                            {
                                db.Tags[j].Value = rl2Rs[j];
                                db.Tags[j].TimeSpan = DateTime.Now;
                            }
                        }
                        break;
                    case DataTypes.String:
                        break;
                    default:
                        break;
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
                            IModbusAdapter DriverAdapter = null;


                            switch (ch.Mode)
                            {
                                case "RTU":
                                    DriverAdapter = rtu[ch.ChannelName];
                                    break;
                                case "ASCII":
                                    DriverAdapter = ascii[ch.ChannelName];
                                    break;
                                case "TCP":
                                    DriverAdapter = mbe[ch.ChannelName];
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
                                        DriverAdapter.Write(string.Format("{0}", TagCollection.Tags[tagName].Address), value);
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
