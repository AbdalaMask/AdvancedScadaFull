using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvancedScada.IBaseService;
using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using static AdvancedScada.Common.XCollection;
using System.Collections.ObjectModel;
using AdvancedScada.Controls.Drivers;
using System.ServiceModel;
using System.Net;
using System.Net.Sockets;
using AdvancedScada.BaseService.Client;
using AdvancedScada.Management.AlarmManager;

namespace AdvancedScada.Controls_Binding.Alarm
{
    public partial class HMIAlarmBit : UserControl, IServiceCallback
    {
        public AlarmManagers objAlarmManager;
        public List<ClassAlarm> dbCurrent = null;
        public HMIAlarmBit()
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
      
        private IReadService client;

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


        public static void listViewAddItem(ListView varListView, ListViewItem item)
        {
            if (varListView.InvokeRequired)
            {
                varListView.BeginInvoke(new MethodInvoker(() => listViewAddItem(varListView, item)));
            }
            else
            {
                varListView.Items.Add(item);
            }
        }

        //**************************************************
        //* Subscribe to addresses in the Comm(PLC) Driver
        //**************************************************
        private void SafeMethodTrue(string[] row0)
        {
            bool flag = false;
            string[] TagNameSub;
            string[] TagNameEnd;
            if (DGAlarm.Items.Count > 0)
            {
                foreach (ListViewItem listViewItem in DGAlarm.Items)
                {
                    if (listViewItem == null)
                    {
                        break;
                    }

                    TagNameSub = listViewItem.SubItems[2].Text.Split(':', ' ', '.');
                    TagNameEnd = row0[2].Split(':', ' ', '.');
                    if (listViewItem.Text == row0[0] && TagNameEnd[2] == TagNameEnd[2])
                    {
                        listViewItem.ForeColor = Color.Red;
                        if (listViewItem.SubItems[1].Text != row0[1])
                        {
                            listViewItem.SubItems[1].Text = row0[1];
                        }

                        if (listViewItem.SubItems[2].Text != row0[2])
                        {
                            listViewItem.SubItems[2].Text = row0[2];
                        }

                        flag = true;
                    }

                    break;
                }
            }

            if (!flag)
            {
                ListViewItem Listitem = new ListViewItem(row0)
                {
                    ForeColor = Color.Red
                };
                DGAlarm.Items.Insert(0, Listitem);
            }
        }

        private void SafeMethodFalse(string[] row1)
        {
            bool flag = false;
            string[] TagNameSub;
            string[] TagNameEnd;
            if (DGAlarm.Items.Count > 0)
            {
                foreach (ListViewItem listViewItem in DGAlarm.Items)
                {
                    if (listViewItem == null)
                    {
                        break;
                    }

                    TagNameSub = listViewItem.SubItems[2].Text.Split(':', ' ', '.');
                    TagNameEnd = row1[2].Split(':', ' ', '.');

                    if (listViewItem.Text == row1[0] && TagNameEnd[2] == TagNameEnd[2])
                    {
                        listViewItem.ForeColor = Color.Green;
                        if (listViewItem.SubItems[1].Text != row1[1])
                        {
                            listViewItem.SubItems[1].Text = row1[1];
                        }

                        if (listViewItem.SubItems[2].Text != row1[2])
                        {
                            listViewItem.SubItems[2].Text = row1[2];
                        }

                        flag = true;
                    }

                    break;
                }
            }

            if (!flag)
            {
                ListViewItem Listitem = new ListViewItem(row1) { ForeColor = Color.Green };
                DGAlarm.Items.Insert(0, Listitem);
            }
        }
        #region Private Methods     



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

        #endregion
        public void UpdateCollection(Common.ConnectionState status, Dictionary<string, Tag> TagValue)
        {
            eventConnectionChanged?.Invoke(status);
            if (!DesignMode && IsHandleCreated)
            {
                if (TagValue == null)
                {
                    return;
                }

                List<KeyValuePair<string, Tag>> List2 = TagValue.Where(item => dbCurrent.Any(p => p.Channel == item.Key.Split('.')[0]
                && p.Device == item.Key.Split('.')[1] && p.DataBlock == item.Key.Split('.')[2])).ToList();
                
                int i = 1;
                foreach (ClassAlarm author in dbCurrent)
                {
                    string tagName = $"{author.Channel}.{author.Device}.{author.DataBlock}.{author.TriggerTeg}";
                    string LastValue = string.Empty;
                    if (TagValue.ContainsKey(tagName) && TagValue[tagName].TagName == author.TriggerTeg)
                    {
                        string ser = string.Empty;
                        ser = string.Format("{0}", TagValue[tagName].Value.Value);


                        if (ser != author.Value)
                        {
                            //* Save this value so we know if it changed without comparing the invert
                            author.Value = ser;
                            string[] row = { $"{i++}", $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}", DateTime.Now.ToShortTimeString(), tagName, author.AlarmText, string.Format("{0}", author.AlarmCalss), TagValue[tagName].Value.Value };


                            if (ser != LastValue)
                            {
                                LastValue = string.Format("{0}", TagValue[tagName].Value.Value);
                                //* Do something here for the value changed
                                if (LastValue == "True")
                                {
                                    SafeMethodTrue(row);
                                }
                                else if (LastValue == "False")
                                {
                                    string[] row1 = { $"{i++}", $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}", DateTime.Now.ToShortTimeString(), tagName, author.AlarmText, string.Format("{0}", author.AlarmCalss), TagValue[tagName].Value.Value };

                                    SafeMethodFalse(row1);
                                    // break;
                                }
                            }
                        }
                        
                    }

                }
            }
        }

        public void UpdateCollectionDataBlock(Common.ConnectionState status, Dictionary<string, DataBlock> collection)
        {
            throw new NotImplementedException();
        }
    }
}
