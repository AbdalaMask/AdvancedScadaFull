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
using System.Windows.Controls;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.WPF.HMIControls.Alarm
{
    /// <summary>
    /// Interaction logic for HMIAlarm.xaml
    /// </summary>
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
            dbCurrent = InitializeData(xmlFile);
        }
        private List<ClassAlarm> InitializeData(string xmlPath)
        {
            objAlarmManager.Alarms.Clear();
            objAlarmManager.XmlPath = xmlPath;
            return objAlarmManager.GetAlarms(xmlPath);



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
       
        public void UpdateCollection(ConnectionState status, Dictionary<string, Tag> Tags)
        {
            
                //Thread.Sleep(1000);
                eventConnectionChanged?.Invoke(status);
                lock (Tags)
               {
                try
                {
                    if (Tags != null)
                    {

                        dgAlarm.Items.Clear();
                        int i = 1;
                        foreach (var author in dbCurrent)
                        {
                            var tagName = $"{author.Channel}.{author.Device}.{author.DataBlock}.{author.TriggerTeg}";

                            if (Tags.ContainsKey(tagName) && Tags[tagName].TagName == author.TriggerTeg)
                            {
                                switch (Tags[tagName].DataType)
                                {
                                    case DriverBase.DataTypes.BitOnByte:
                                        break;
                                    case DriverBase.DataTypes.BitOnWord:
                                        break;
                                    case DriverBase.DataTypes.Bit:
                                        var LastValue = string.Empty;
                                        if (Tags[tagName].Value == bool.Parse(author.Value))
                                        {
                                           var AlarmHs = new dgAlarmH() { No = $"{i++}", Date = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}", Time = DateTime.Now.ToShortTimeString(), TriggerTeg = tagName, Message = author.AlarmText, AlarmType = string.Format("{0}", author.AlarmCalss), Status = author.Value };

                                            dgAlarm.Items.Add(AlarmHs);

                                        }
                                        break;
                                    case DriverBase.DataTypes.Byte:
                                        break;
                                    case DriverBase.DataTypes.Short:
                                        if (Tags[tagName].Value > short.Parse(author.Value))
                                        {
                                            var AlarmHs = new dgAlarmH() { No = $"{i++}", Date = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}", Time = DateTime.Now.ToShortTimeString(), TriggerTeg = tagName, Message = author.AlarmText, AlarmType = string.Format("{0}", author.AlarmCalss), Status = author.Value };

                                            dgAlarm.Items.Add(AlarmHs);

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
                catch (Exception ex)
                {

                    EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                }
                   
                }
           
        }

        private void HMIAlarms_Initialized(object sender, EventArgs e)
        {
            try
            {
               if (!Comm.LicenseHMI.IsInDesignMode)
                GetWCF();
 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateCollectionDataBlock(ConnectionState status, Dictionary<string, DataBlock> collection)
        {
            //throw new NotImplementedException();
        }

        ~HMIAlarm()
        {
            try
            {
                client?.Disconnect(XCollection.CURRENT_MACHINE);
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
       
    }

}
