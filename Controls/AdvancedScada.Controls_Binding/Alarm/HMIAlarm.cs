using AdvancedScada.BaseService.Client;
using AdvancedScada.Common;
using AdvancedScada.Controls.Drivers;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using AdvancedScada.Management.AlarmManager;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            var xmlFile = objAlarmManager.ReadKey(AlarmManagers.XML_NAME_DEFAULT);
            if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile)) return;
            dbCurrent= InitializeData(xmlFile);
        }
        private List<ClassAlarm> InitializeData(string xmlPath)
        {
            objAlarmManager.Alarms.Clear();
            objAlarmManager.XmlPath = xmlPath;
            return  objAlarmManager.GetAlarms(xmlPath);


            //DGAlarmAnalog.Rows.Clear();
            //foreach (var tg in chList)
            //{
            //    string[] row = { tg.Name, string.Format("{0}", tg.AlarmText), string.Format("{0}", tg.AlarmCalss), tg.Value, tg.TriggerTeg, tg.DataBlock, tg.Device, tg.Channel };

            //    DGAlarmAnalog.Rows.Add(row);
            //}

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
            var ic = new InstanceContext(this);
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
            eventConnectionChanged?.Invoke(status);
            if (Tags != null)
            {
                DGAlarm.Rows.Clear();
                   var i = 1;
                foreach (var author in dbCurrent)
                {
                    var tagName = $"{author.Channel}.{author.Device}.{author.DataBlock}.{author.TriggerTeg}";
                    if (Tags.ContainsKey(tagName))
                    {
                        switch (Tags[tagName].DataType)
                        {
                            case DriverBase.DataTypes.BitOnByte:
                                break;
                            case DriverBase.DataTypes.BitOnWord:
                                break;
                            case DriverBase.DataTypes.Bit:
                                if (Tags[tagName].Value == bool.Parse(author.Value))
                                {
                                    string[] row = { $"{i++}", $"{DateTime.Now.ToShortDateString()}", DateTime.Now.ToShortTimeString(), author.AlarmText, string.Format("{0}", author.AlarmCalss), author.Value };
                                    DGAlarm.Rows.Add(row);
                                }
                                break;
                            case DriverBase.DataTypes.Byte:
                                break;
                            case DriverBase.DataTypes.Short:
                                if (Tags[tagName].Value >short.Parse ( author.Value))
                                {
                                    string[] row = { $"{i++}", $"{DateTime.Now.ToShortDateString()}", DateTime.Now.ToShortTimeString(), author.AlarmText, string.Format("{0}", author.AlarmCalss), author.Value };
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







                //var List2 = Tags.Where(item => dbCurrent.Any(p => p.Channel == item.Key.Split('.')[0]
                //&& p.Device == item.Key.Split('.')[1] && p.DataBlock == item.Key.Split('.')[2])).ToList();
                //if (DGAlarm.RowCount < 1) return;
                //for (int i = 0; i < DGAlarm.RowCount; i++)
                //{

                    //    for (var y = 0; y < List2.Count; y++)
                    //    {
                    //        if (DGAlarm[0, i].Value != null)
                    //            if (List2[y].Value.TagName.Equals(int.Parse(DGAlarm[0, i].Value.ToString())))
                    //            {

                    //                if (DGAlarm[1, i].Value != null)
                    //                    for (int j = 0; j < List2.Count; j++)
                    //                        if (List2[j].Value.TagName.Equals(DGAlarm[1, i].Value.ToString()))
                    //                        {

                    //                            DGAlarm[4, i].Value = List2[j].Value.Value;
                    //                            DGAlarm[5, i].Value = $"{List2[j].Value.TimeSpan.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
                    //                            // DGMonitorForm[5, i].Value = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
                    //                        }


                    //            }

                    //    }

                    //}


            }
        }
    }
}
