using AdvancedScada.BaseService.Client;
using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using AdvancedScada.Management.AlarmManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Windows.Forms;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Controls_Binding.Alarm
{
    public partial class HMIAlarm : UserControl, IServiceCallback
    {
        private IReadService client;
        public AlarmManagers objAlarmManager;
        public List<ClassAlarm> dbCurrent = null;
        public HMIAlarm()
        {
            InitializeComponent();
            dbCurrent = new List<ClassAlarm>();
            objAlarmManager = AlarmManagers.GetAlarmManager();
            string xmlFile = objAlarmManager.ReadKey(AlarmManagers.XML_NAME_DEFAULT);
            if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile))
            {
                return;
            }

            dbCurrent = InitializeData(xmlFile);
        }
        private List<ClassAlarm> InitializeData(string xmlPath)
        {
            objAlarmManager.Alarms.Clear();
            objAlarmManager.XmlPath = xmlPath;
            return objAlarmManager.GetAlarms(xmlPath);



        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            try
            {
                if (!DesignMode)
                {
                    GetWCF();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetWCF()
        {
            InstanceContext ic = new InstanceContext(this);
            XCollection.CURRENT_MACHINE = new Machine
            {
                MachineName = Environment.MachineName,
                Description = "Free"
            };
            IPAddress[] hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress iPAddress in hostAddresses)
            {
                if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    XCollection.CURRENT_MACHINE.IPAddress = $"{iPAddress}";
                    break;
                }
            }
            client = ClientDriverHelper.GetInstance().GetReadService(ic);
            client.Connect(XCollection.CURRENT_MACHINE);
        }

        public void UpdateCollection(Common.ConnectionState status, Dictionary<string, Tag> Tags)
        {
            if (!DesignMode && IsHandleCreated)
            {
                //Thread.Sleep(1000);
                eventConnectionChanged?.Invoke(status);
                lock (Tags)
                {
                    if (Tags != null)
                    {
                        List<KeyValuePair<string, Tag>> List2 = Tags.Where(item => dbCurrent.Any(p => p.Channel == item.Key.Split('.')[0]
                        && p.Device == item.Key.Split('.')[1] && p.DataBlock == item.Key.Split('.')[2])).ToList();
                        DGAlarm.Rows.Clear();
                        int i = 1;
                        foreach (ClassAlarm author in dbCurrent)
                        {
                            string tagName = $"{author.Channel}.{author.Device}.{author.DataBlock}.{author.TriggerTeg}";

                            if (Tags.ContainsKey(tagName) && Tags[tagName].TagName == author.TriggerTeg)
                            {
                                switch (Tags[tagName].DataType)
                                {
                                    case DriverBase.DataTypes.BitOnByte:
                                        break;
                                    case DriverBase.DataTypes.BitOnWord:
                                        break;
                                    case DriverBase.DataTypes.Bit:
                                        string LastValue = string.Empty;
                                        if (Tags[tagName].Value == bool.Parse(author.Value))
                                        {
                                            string[] row = { $"{i++}", $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}", DateTime.Now.ToShortTimeString(), tagName, author.AlarmText, string.Format("{0}", author.AlarmCalss), author.Value };
                                            DGAlarm.Rows.Add(row);

                                        }
                                        break;
                                    case DriverBase.DataTypes.Byte:
                                        break;
                                    case DriverBase.DataTypes.Short:
                                        if (Tags[tagName].Value > short.Parse(author.Value))
                                        {
                                            string[] row = { $"{i++}", $"{DateTime.Now.ToShortDateString()}", DateTime.Now.ToShortTimeString(), tagName, author.AlarmText, string.Format("{0}", author.AlarmCalss), author.Value };
                                            DGAlarm.Rows.Add(row);

                                        }
                                        break;
                                    case DriverBase.DataTypes.UShort:
                                        break;
                                    case DriverBase.DataTypes.Int:
                                        break;
                                    case DriverBase.DataTypes.UInt:
                                        break;
                                    case DriverBase.DataTypes.Long:
                                        break;
                                    case DriverBase.DataTypes.ULong:
                                        break;
                                    case DriverBase.DataTypes.Float:
                                        break;
                                    case DriverBase.DataTypes.Double:
                                        break;
                                    case DriverBase.DataTypes.String:
                                        break;
                                    default:
                                        break;
                                }



                            }

                        }









                    }
                }
            }
        }

        public void UpdateCollectionDataBlock(ConnectionState status, Dictionary<string, DataBlock> collection)
        {
            throw new NotImplementedException();
        }
    }
}
