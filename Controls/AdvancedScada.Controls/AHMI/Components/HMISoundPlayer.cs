
using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.Components;
using AdvancedScada.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AdvancedScada.Controls.AHMI.Components
{
    public class HMISoundPlayer : HMIDataSubscriber
    {

        #region Properties
        private string m_FileFolder = "C:\\Windows\\Media\\";
        [System.ComponentModel.BrowsableAttribute(true), System.ComponentModel.EditorAttribute(typeof(FileFolderEditor), typeof(System.Drawing.Design.UITypeEditor))]
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
                    if (value.Substring(value.Length - 1, 1) == "\\")
                    {
                        value = value.Substring(0, value.Length - 1);
                    }
                    m_FileFolder = value;
                }
            }
        }

        private string m_SoundFileName = "tada.wav";
        public string SoundFileName
        {
            get
            {
                return m_SoundFileName;
            }
            set
            {
                if (m_SoundFileName != value)
                {
                    m_SoundFileName = value;
                }
            }
        }

        public enum TriggerTypeOptions
        {
            PositiveChange,
            NegativeChange,
            AnyChange
        }
        private TriggerTypeOptions m_TriggerType = TriggerTypeOptions.AnyChange;
        public TriggerTypeOptions TriggerType
        {
            get
            {
                return m_TriggerType;
            }
            set
            {
                m_TriggerType = value;
            }
        }

        private bool m_Enabled = true;
        public bool Enabled
        {
            get
            {
                return m_Enabled;
            }
            set
            {
                m_Enabled = value;
            }
        }
        #endregion

        #region Constructor/Destructor
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion

        #region Events
        private bool LastValue;
        protected override void OnDataChanged(PlcComEventArgs e)
        {
            base.OnDataChanged(e);

            if (e.Values != null && e.Values.Count > 0)
            {
                if (m_TriggerType == TriggerTypeOptions.AnyChange)
                {
                    if (m_Enabled)
                    {
                        PlaySound();
                    }
                }
                else
                {
                    bool NewValue = false;
                    try
                    {
                        //* convert the value to Boolean so we can look for rising/falling edges
                        NewValue = Convert.ToBoolean(Utilities.DynamicConverter(e.Values[0], typeof(bool)));
                        if ((m_TriggerType == TriggerTypeOptions.PositiveChange && NewValue && !LastValue) || (m_TriggerType == TriggerTypeOptions.NegativeChange && !NewValue && LastValue))
                        {
                            if (m_Enabled)
                            {
                                PlaySound();
                            }
                        }
                        LastValue = NewValue;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to convert " + e.Values[0] + " to Boolean");
                    }
                }

                m_Value = e.Values[0];
            }
        }

        //* When the subscription with the PLC succeeded
        protected override void OnSuccessfulSubscription(PlcComEventArgs e)
        {
            base.OnSuccessfulSubscription(e);
        }

        private System.Media.SoundPlayer player;
        private void PlaySound()
        {
            try
            {
                if (player == null)
                {
                    player = new System.Media.SoundPlayer(m_FileFolder + "\\" + m_SoundFileName);
                }
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }


}