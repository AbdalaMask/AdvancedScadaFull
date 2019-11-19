
using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Common;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AdvancedScada.Controls.AHMI.Components
{
    public class HMIBasicDataLogger : HMIDataSubscriber
    {
        private System.IO.StreamWriter sw;

        #region Constructor/Destructor

        public HMIBasicDataLogger() : base()
        {
        }

        public HMIBasicDataLogger(System.ComponentModel.IContainer container) : this()
        {

            //Required for Windows.Forms Class Composition Designer support
            if (container != null)
            {
                container.Add(this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (LogTimer != null)
            {
                LogTimer.Enabled = false;
            }

            lock (SwLock)
            {
                if (sw != null)
                {
                    sw.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Properties

        private string m_FileFolder = "." + System.IO.Path.DirectorySeparatorChar;
        [BrowsableAttribute(true), EditorAttribute(typeof(FileFolderEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FileFolder
        {
            get
            {
                return m_FileFolder;
            }
            set
            {
                if (value.Length > 0)
                {
                    //* Remove the last back slash if it is there
                    if (Convert.ToChar(value.Substring(value.Length - 1, 1)) == System.IO.Path.DirectorySeparatorChar)
                    {
                        value = value.Substring(0, value.Length - 1);
                    }
                    m_FileFolder = value;
                }
            }
        }

        private string m_FileName = "PLCDataLog";
        public string FileName
        {
            get
            {
                return m_FileName;
            }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                }
            }
        }

        private bool m_FileNameIncludesDate;
        public bool FileNameIncludesDate
        {
            get
            {
                return m_FileNameIncludesDate;
            }
            set
            {
                if (m_FileNameIncludesDate != value)
                {
                    m_FileNameIncludesDate = value;
                    if (m_CreateNewLogFileDaily)
                    {
                        m_FileNameIncludesDate = true;
                    }
                }
            }
        }

        public enum TriggerType
        {
            TimeInterval,
            DataChange,
            WriteOnTrigger,
            EverySample
        }

        private TriggerType m_LogTriggerType;
        public TriggerType LogTriggerType
        {
            get
            {
                return m_LogTriggerType;
            }
            set
            {
                m_LogTriggerType = value;
            }
        }

        private Timer LogTimer;
        private int m_LogInterval = 1000;
        public int LogInterval
        {
            get
            {
                return m_LogInterval;
            }
            set
            {
                m_LogInterval = value;
            }
        }

        private string m_Prefix;
        public string Prefix
        {
            get
            {
                return m_Prefix;
            }
            set
            {
                m_Prefix = value;
            }
        }

        private string m_TimestampFormat = "dd-MMM-yy HH:mm:ss";
        public string TimestampFormat
        {
            get
            {
                return m_TimestampFormat;
            }
            set
            {
                try
                {
                    // Dim TestString As String = Now.ToString("value")
                    m_TimestampFormat = value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid DateTime format of " + value);
                }
            }
        }

        private int m_MaximumPoints;
        public int MaximumPoints
        {
            get
            {
                return m_MaximumPoints;
            }
            set
            {
                m_MaximumPoints = value;
            }
        }

        private bool m_CreateNewLogFileAtMaxPoints;
        public bool CreateNewLogFileAtMaxPoints
        {
            get
            {
                return m_CreateNewLogFileAtMaxPoints;
            }
            set
            {
                m_CreateNewLogFileAtMaxPoints = value;
            }
        }

        private bool m_CreateNewLogFileDaily;
        [System.ComponentModel.Description("Enabling this option will force inclusion of the current Date into the name of the log file")]
        public bool CreateNewLogFileDaily
        {
            get
            {
                return m_CreateNewLogFileDaily;
            }
            set
            {
                m_CreateNewLogFileDaily = value;
                if (m_CreateNewLogFileDaily)
                {
                    m_FileNameIncludesDate = true;
                }
            }
        }

        private int m_LogFileCount;
        public int LogFileCount
        {
            get
            {
                return m_LogFileCount;
            }
        }

        private bool m_EnableLogging = true;
        public bool EnableLogging
        {
            get
            {
                return m_EnableLogging;
            }
            set
            {
                m_EnableLogging = value;
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressEnableLogging = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressEnableeLogging
        {
            get
            {
                return m_PLCAddressEnableLogging;
            }
            set
            {
                if (m_PLCAddressEnableLogging != value)
                {
                    m_PLCAddressEnableLogging = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }



        private bool m_WriteTrigger;
        [System.ComponentModel.Browsable(false)]
        public bool WriteTrigger
        {
            get
            {
                return m_WriteTrigger;
            }
            set
            {
                if (!m_WriteTrigger && value)
                {
                    if (value)
                    {
                        StoreValue();
                    }
                }
                m_WriteTrigger = value;

            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressWriteTrigger = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressWriteTrigger
        {
            get
            {
                return m_PLCAddressWriteTrigger;
            }
            set
            {
                if (m_PLCAddressWriteTrigger != value)
                {
                    m_PLCAddressWriteTrigger = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        #endregion

        #region Events

        private int PointCount;
        protected override void OnDataChanged(PlcComEventArgs e)
        {
            base.OnDataChanged(e);

            if (m_LogTriggerType == TriggerType.DataChange)
            {
                if (m_MaximumPoints == 0 || PointCount < m_MaximumPoints)
                {
                    StoreValue();
                }
                else
                {
                    CreateNewLog();
                }
            }
        }

        protected override void OnDataReturned(PlcComEventArgs e)
        {
            base.OnDataReturned(e);

            if (m_LogTriggerType == TriggerType.EverySample)
            {
                if (m_MaximumPoints == 0 || PointCount < m_MaximumPoints)
                {
                    StoreValue();
                }
                else
                {
                    CreateNewLog();
                }
            }
        }

        private string PreviousDate = string.Empty;
        private string DateTimeNow = string.Empty;
        private string fileNameToUse = string.Empty;
        private void CreateNewLog()
        {
            if (!DesignMode)
            {
                DateTimeNow = DateTime.Now.ToString("dd-MMM-yyyy");
                if (m_CreateNewLogFileDaily && PreviousDate != DateTimeNow)
                {
                    PreviousDate = DateTimeNow;
                    PointCount = 0;
                    m_LogFileCount = 0;
                }
                if (this.m_CreateNewLogFileAtMaxPoints)
                {
                    fileNameToUse = m_FileName + m_LogFileCount.ToString("000");
                    PointCount = 0;
                    m_LogFileCount += 1;
                    if (m_LogFileCount > 999)
                    {
                        m_LogFileCount = 0;
                    }
                }
                else
                {
                    fileNameToUse = m_FileName;
                    m_LogFileCount = 1;
                }
            }
        }

        private object SwLock = new object();
        //* When the subscription with the PLC succeeded, setup for logging
        protected override void OnSuccessfulSubscription(PlcComEventArgs e)
        {
            base.OnSuccessfulSubscription(e);

            //* create the timer to log the data
            if (m_LogTriggerType == TriggerType.TimeInterval)
            {
                if (LogTimer == null)
                {
                    LogTimer = new Timer();
                    if (m_LogInterval > 0)
                    {
                        LogTimer.Interval = m_LogInterval;
                    }
                    else
                    {
                        LogTimer.Interval = 1000;
                    }
                    LogTimer.Tick += LogInterval_Tick;

                    LogTimer.Enabled = true;
                }
            }
        }

        //* Timer tick interval used to store data at a periodic rate
        private void LogInterval_Tick(object sender, System.EventArgs e)
        {
            if (m_MaximumPoints == 0 || PointCount < m_MaximumPoints)
            {
                StoreValue();
            }
            else
            {
                CreateNewLog();
            }
        }


        public void StoreValue()
        {
            try
            {
                if (m_EnableLogging)
                {
                    string StringToWrite = m_Prefix;
                    if (m_TimestampFormat != null && !string.IsNullOrEmpty(m_TimestampFormat))
                    {
                        StringToWrite += DateTime.Now.ToString(m_TimestampFormat);
                    }

                    foreach (var item in SubscribedValues)
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                        {
                            StringToWrite += "," + item.Value;
                        }
                        else
                        {
                            StringToWrite += "," + "Empty Value";
                        }
                    }

                    lock (SwLock)
                    {
                        if (string.IsNullOrEmpty(fileNameToUse))
                        {
                            CreateNewLog();
                        }

                        string FileName = null;
                        if (m_FileNameIncludesDate)
                        {
                            FileName = m_FileFolder + System.IO.Path.DirectorySeparatorChar + fileNameToUse + "-" + DateTimeNow;
                        }
                        else
                        {
                            FileName = m_FileFolder + System.IO.Path.DirectorySeparatorChar + fileNameToUse;
                        }

                        if (FileName.IndexOf(".") < 0)
                        {
                            FileName += ".log";
                        }

                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName, true))
                        {
                            sw.WriteLine(StringToWrite);
                        }
                    }

                    PointCount += 1;
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

    }

    public class FileFolderEditor : System.Drawing.Design.UITypeEditor
    {
        public FileFolderEditor()
        {
        }

        // Indicates whether the UITypeEditor provides a form-based (modal) dialog,  
        // drop down dialog, or no UI outside of the properties window. 
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        // Displays the UI for value selection. 
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            using (FolderBrowserDialog fb = new FolderBrowserDialog())
            {
                fb.ShowDialog();

                return fb.SelectedPath;
            }
        }

        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }

    }


}