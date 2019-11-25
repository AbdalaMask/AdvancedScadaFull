using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Management.AlarmManager
{
  public  class AlarmManagers
    {
        public const string ROOT = "Root";
        public const string AllAlarm = "Alarm";
        public const string Alarm_NAME = "AlarmName";
        public const string Alarm_Text = "AlarmText";
        public const string Alarm_Calss = "AlarmCalss";
        public const string Alarm_Value = "Value";
        public const string TriggerTeg = "TriggerTeg";
        public const string Channel = "Channel";
        public const string Device = "Device";
        public const string DataBlock = "DataBlock";
        public const string XML_NAME_DEFAULT = "AlarmCollection";
        private static readonly object mutex = new object();
        private static AlarmManagers _instance;

        public string XmlPath { set; get; }
        public List<ClassAlarm> Alarms { get; set; } = new List<ClassAlarm>();

        public static AlarmManagers GetAlarmManager()
        {
            lock (mutex)
            {
                if (_instance == null) _instance = new AlarmManagers();
            }

            return _instance;
        }


        public void AddAlarm(ClassAlarm SQ)
        {
            try
            {
                if (SQ == null) throw new NullReferenceException("The Alarm is null reference exception");
                var fCh = IsExisted(SQ);
                if (fCh != null) throw new Exception($"Alarm name: '{SQ.Name}' is existed");
                Alarms.Add(SQ);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ClassAlarm IsExisted(ClassAlarm ch)
        {
            ClassAlarm result = null;
            try
            {
                foreach (var item in Alarms)
                    if (item.Name.Equals(ch.Name))
                    {
                        result = item;
                        break;
                    }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

            return result;
        }

        public void UpdateAlarm(ClassAlarm ch)
        {
            try
            {
                if (ch == null) throw new NullReferenceException("The Alarms is null reference exception");
                var fCh = IsExisted(ch);
                if (fCh != null) throw new Exception($"Alarms name: '{ch.Name}' is existed");
                foreach (var item in Alarms)
                    if (item.Name == ch.Name)
                    {
                        item.Name = ch.Name;
                        item.AlarmCalss = ch.AlarmCalss;
                        item.AlarmText = ch.AlarmText;
                        item.Value = ch.Value;
                        item.TriggerTeg = ch.TriggerTeg;
                        item.Channel = ch.Channel;
                        item.Device = ch.Device;
                        item.DataBlock = ch.DataBlock;

                    }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        

        /// <summary>
        ///     Xóa kênh.
        /// </summary>
        /// <param name="chName">Tên kênh</param>
        public void Delete(string chName)
        {
            try
            {
                var result = GetByAlarmName(chName);
                if (result == null) throw new KeyNotFoundException("Alarms name is not found exception");
                Alarms.Remove(result);
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Xóa kênh.
        /// </summary>
        /// <param name="ch">Kênh</param>
        public void DeleteAlarm(ClassAlarm ch)
        {
            try
            {
                if (ch == null) throw new NullReferenceException("The Alarms is null reference exception");
                foreach (var item in Alarms)
                    if (item.Name == ch.Name)
                    {
                        Alarms.Remove(item);
                        break;
                    }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
      

        /// <summary>
        ///     Tìm kiếm kênh theo tên kênh.
        /// </summary>
        /// <param name="chName">Tên kênh</param>
        /// <returns>Kênh</returns>
        public ClassAlarm GetByAlarmName(string chName)
        {
            ClassAlarm result = null;
            try
            {
                foreach (var item in Alarms)
                    if (item.Name.Equals(chName))
                    {
                        result = item;
                        break;
                    }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

            return result;
        }
        public List<ClassAlarm> GetAlarms(string XmlPath)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                if (string.IsNullOrEmpty(XmlPath) || string.IsNullOrWhiteSpace(XmlPath))
                    XmlPath = ReadKey(XML_NAME_DEFAULT);
                xmlDoc.Load(XmlPath);
                var nodes = xmlDoc.SelectNodes(ROOT);
                foreach (XmlNode rootNode in nodes)
                {
                    var channelNodeList = rootNode.SelectNodes(AllAlarm);
                    foreach (XmlNode chNode in channelNodeList)
                    {
                        var newClassAlarm = new ClassAlarm();


                        if (newClassAlarm != null)
                        {
                           
                            newClassAlarm.Name = chNode.Attributes[Alarm_NAME].Value;
                            newClassAlarm.AlarmCalss = chNode.Attributes[Alarm_Calss].Value;
                            newClassAlarm.AlarmText = chNode.Attributes[Alarm_Text].Value;
                            newClassAlarm.Channel = chNode.Attributes[Channel].Value;
                            newClassAlarm.DataBlock = chNode.Attributes[DataBlock].Value;
                            newClassAlarm.Device = chNode.Attributes[Device].Value;
                            newClassAlarm.TriggerTeg = chNode.Attributes[TriggerTeg].Value;
                            newClassAlarm.Value = chNode.Attributes[Alarm_Value].Value;

                            Alarms.Add(newClassAlarm);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

            return Alarms;
        }
        public void CreatFile(string pathXml)
        {
            try
            {
                if (File.Exists(pathXml)) File.Delete(pathXml);
                var element = new XElement(ROOT);
                var doc = new XDocument(element);
                doc.Save(pathXml);
                XmlPath = pathXml;
                WriteKey(XML_NAME_DEFAULT, pathXml);
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Phương thức đọc key.
        /// </summary>
        /// <param name="keyName">Tên key</param>
        /// <returns>Giá trị của key</returns>
        public string ReadKey(string keyName)
        {
            var result = string.Empty;
            try
            {
                RegistryKey regKey;
                regKey = Registry.CurrentUser.OpenSubKey(@"Software\IndustrialHMI"); //HKEY_CURRENR_USER\Software\VSSCD
                if (regKey != null) result = (string)regKey.GetValue(keyName);
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

            return result;
        }

        /// <summary>
        ///     Phương thức ghi tên và giá trị của key
        /// </summary>
        /// <param name="keyName">Tên key</param>
        /// <param name="keyValue">Giá trị của key</param>
        public void WriteKey(string keyName, string keyValue)
        {
            try
            {
                RegistryKey regKey;
                regKey = Registry.CurrentUser.CreateSubKey(@"Software\IndustrialHMI");
                regKey.SetValue(keyName, keyValue);
                regKey.Close();
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        public void Save(string pathXml)
        {
            try
            {
                WriteKey(XML_NAME_DEFAULT, pathXml);
                CreatFile(pathXml);
                XmlPath = pathXml;
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(pathXml);
                var root = xmlDoc.SelectSingleNode(ROOT);

                // List SQLServers.
                foreach (var ch in Alarms)
                {
                    var chElement = xmlDoc.CreateElement(AllAlarm);
                    
                    chElement.SetAttribute(Alarm_NAME, ch.Name);
                    chElement.SetAttribute(Alarm_Calss, ch.AlarmCalss);
                    chElement.SetAttribute(Alarm_Text, ch.AlarmText);
                    chElement.SetAttribute(Channel, ch.Channel);
                    chElement.SetAttribute(DataBlock, ch.DataBlock);
                    chElement.SetAttribute(Device, ch.Device);
                    chElement.SetAttribute(TriggerTeg, ch.TriggerTeg);
                    chElement.SetAttribute(Alarm_Value, ch.Value);

                    root.AppendChild(chElement);
                    
                }

                xmlDoc.Save(pathXml);
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
    }
}
