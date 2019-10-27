using AdvancedScada.BaseService.Client;
using AdvancedScada.Common;
using AdvancedScada.Controls.Drivers;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using AdvancedScada.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.Alarm
{
    [CallbackBehavior]
    [DefaultEvent("DataChanged")]

    public class HMIAlarmMan : AdvancedScada.Controls_Net45.AlarmMan, IServiceCallback
    {
        

        #region Public Methods

        public string GetValueByName(string name)
        {
            var index = 0;
            while (index < m_PLCAddressValueItems.Count)
            {
                if (string.Compare(m_PLCAddressValueItems[index].Name, name, true) == 0)
                    return m_PLCAddressValueItems[index].LastValue;
                index += 1;
            }

            return string.Empty;
        }

        #endregion

       

        #region PLC Properties

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private readonly ObservableCollection<PLCAddressItem> m_PLCAddressValueItems =
            new ObservableCollection<PLCAddressItem>();

        [Category("PLC Properties")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<PLCAddressItem> PLCAddressValueItems
        {
            get { return m_PLCAddressValueItems; }
        }

        #endregion

       

        #region Public Methods WCF

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

        public static string path = string.Empty;
       

        public void DataChanged(List<Tag> registers)
        {
            AlarmMan_DataChanged(registers);
        }

        #endregion

        #region Public Methods

        private IReadService client;

        public static void listViewAddItem(ListView varListView, ListViewItem item)
        {
            if (varListView.InvokeRequired)
                varListView.BeginInvoke(new MethodInvoker(() => listViewAddItem(varListView, item)));
            else
                varListView.Items.Add(item);
        }

        //**************************************************
        //* Subscribe to addresses in the Comm(PLC) Driver
        //**************************************************
        private void SafeMethodTrue(string[] row0)
        {
            var flag = false;
            string[] TagNameSub;
            string[] TagNameEnd;
            if (Items.Count > 0)
                foreach (ListViewItem listViewItem in Items)
                {
                    if (listViewItem == null) break;
                    TagNameSub = listViewItem.SubItems[2].Text.Split(':', ' ', '.');
                    TagNameEnd = row0[2].Split(':', ' ', '.');
                    if (listViewItem.Text == row0[0] && TagNameEnd[2] == TagNameEnd[2])
                    {
                        listViewItem.ForeColor = Color.Red;
                        if (listViewItem.SubItems[1].Text != row0[1])
                            listViewItem.SubItems[1].Text = row0[1];
                        if (listViewItem.SubItems[2].Text != row0[2])
                            listViewItem.SubItems[2].Text = row0[2];

                        flag = true;
                    }

                    break;
                }

            if (!flag)
            {
                var Listitem = new ListViewItem(row0);
                Listitem.ForeColor = Color.Red;
                Items.Insert(0, Listitem);
            }
        }

        private void SafeMethodFalse(string[] row1)
        {
            var flag = false;
            string[] TagNameSub;
            string[] TagNameEnd;
            if (Items.Count > 0)
                foreach (ListViewItem listViewItem in Items)
                {
                    if (listViewItem == null) break;
                    TagNameSub = listViewItem.SubItems[2].Text.Split(':', ' ', '.');
                    TagNameEnd = row1[2].Split(':', ' ', '.');

                    if (listViewItem.Text == row1[0] && TagNameEnd[2] == TagNameEnd[2])
                    {
                        listViewItem.ForeColor = Color.Green;
                        if (listViewItem.SubItems[1].Text != row1[1])
                            listViewItem.SubItems[1].Text = row1[1];
                        if (listViewItem.SubItems[2].Text != row1[2])
                            listViewItem.SubItems[2].Text = row1[2];
                        flag = true;
                    }

                    break;
                }

            if (!flag)
            {
                var Listitem = new ListViewItem(row1) { ForeColor = Color.Green };
                Items.Insert(0, Listitem);
            }
        }

        public void AlarmMan_DataChanged(List<Tag> TagValue)
        {
            if (!DesignMode && IsHandleCreated)
            {
                if (TagValue == null) return;
                var List2 = TagValue
                    .Where(item => m_PLCAddressValueItems.Any(item2 => item2.PLCAddress == item.TagName)).ToList();
                string[] TagName;
                for (var index1 = 0; index1 < List2.Count; index1++)
                    for (var index = 0; index < m_PLCAddressValueItems.Count; index++)
                    {
                        var LastValue = string.Empty;
                        TagName = m_PLCAddressValueItems[index].PLCAddress.Split('.');

                        if (string.Compare(List2[index1].TagName, TagName[0], true) == 0)
                        {
                            if (List2[index1].Value == null) return;
                            var ser = string.Empty;
                            ser = string.Format("{0}", List2[index1].Value);
                            if (ser != m_PLCAddressValueItems[index].LastValue)
                            {
                                //* Save this value so we know if it changed without comparing the invert
                                m_PLCAddressValueItems[index].LastValue = ser;
                                string[] row0 =
                                {
                                List2[index1].TagName, string.Format("{0}", List2[index1].Value),
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                            };

                                if (ser != LastValue)
                                {
                                    LastValue = string.Format("{0}", List2[index1].Value);
                                    //* Do something here for the value changed
                                    if (LastValue == "True")
                                    {
                                        SafeMethodTrue(row0);
                                    }
                                    else if (LastValue == "False")
                                    {
                                        string[] row1 =
                                        {
                                        List2[index1].TagName, string.Format("{0}", List2[index1].Value),
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                                    };
                                        SafeMethodFalse(row1);
                                        // break;
                                    }
                                }
                            }
                        }
                    }
            }
        }

        #endregion

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

        #region "Error Display"

        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }

        #endregion




        public void DataChanged(List<Tag>[] registers)
        {
            for (int i = 0; i < registers.Length; i++)
            {
                AlarmMan_DataChanged(registers[i]);
            }
        }


       

        public void UpdateCollection(ConnectionState status, Dictionary<string, Tag> collection)
        {

        }
    }
}