﻿using AdvancedScada.Common;
using AdvancedScada.Common.Client;
using AdvancedScada.Controls_Binding.DialogEditor;
using AdvancedScada.Controls_Binding.Display;
using MfgControl.AdvancedHMI.Controls;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.ButtonAll
{

    public class HMIbutton : System.Windows.Forms.Button, IPropertiesControls
    {
        public HMIbutton()
        {
            MaxHoldTimer.Tick += MaxHoldTimer_Tick;
            MinHoldTimer.Tick += HoldTimer_Tick;
        }

        #region PLC Properties

        //********************************************
        //* Property - Address in PLC for click event
        //********************************************

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick { get; set; } = string.Empty;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string _PLCAddressHighlight = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressHighlightX
        {
            get => _PLCAddressHighlight;
            set
            {
                if (_PLCAddressHighlight != value)
                {
                    _PLCAddressHighlight = value;
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressText = string.Empty;

        [DefaultValue("")]
        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressText
        {
            get => m_PLCAddressText;
            set
            {
                if (m_PLCAddressText != value)
                {
                    m_PLCAddressText = value;

                    //* When address is changed, re-subscribe to new address
                    try
                    {
                        if (string.IsNullOrEmpty(m_PLCAddressText) || string.IsNullOrWhiteSpace(m_PLCAddressText) ||
                            Licenses.LicenseManager.IsInDesignMode)
                        {
                            return;
                        }

                        Binding bd = new Binding("Text", TagCollectionClient.Tags[m_PLCAddressText], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        Utilities.DisplayError(this, ex.Message);
                    }
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressVisible
        {
            get => m_PLCAddressVisible;
            set
            {
                if (m_PLCAddressVisible != value)
                {
                    m_PLCAddressVisible = value;
                    try
                    {
                        //* When address is changed, re-subscribe to new address

                        if (string.IsNullOrEmpty(m_PLCAddressVisible) || string.IsNullOrWhiteSpace(m_PLCAddressVisible) ||
                            Licenses.LicenseManager.IsInDesignMode)
                        {
                            return;
                        }

                        Binding bd = new Binding("Visible", TagCollectionClient.Tags[m_PLCAddressVisible], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {

                        Utilities.DisplayError(this, ex.Message);
                    }

                }
            }
        }


        private string m_PLCAddressEnabled = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressEnabled
        {
            get => m_PLCAddressEnabled;
            set
            {
                if (m_PLCAddressEnabled != value)
                {
                    m_PLCAddressEnabled = value;
                    try
                    {
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressEnabled) || string.IsNullOrWhiteSpace(m_PLCAddressEnabled) ||
                            Licenses.LicenseManager.IsInDesignMode)
                        {
                            return;
                        }

                        Binding bd = new Binding("Enabled", TagCollectionClient.Tags[m_PLCAddressEnabled], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {

                        Utilities.DisplayError(this, ex.Message);
                    }

                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressSelectTextAlternate = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressSelectTextAlternate
        {
            get => m_PLCAddressSelectTextAlternate;
            set
            {
                if (m_PLCAddressSelectTextAlternate != value)
                {
                    m_PLCAddressSelectTextAlternate = value;
                }
            }
        }

        [DefaultValue("0")]
        public override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private OutputType m_OutputType = OutputType.MomentarySet;

        public OutputType OutputType
        {
            get => m_OutputType;
            set => m_OutputType = value;
        }

        //******************************************************************************************
        //* Use the base control's text property and make it visible as a property on the designer
        //******************************************************************************************
        private dynamic m_Value;

        public dynamic Value
        {
            get => m_Value;
            set
            {
                if (value != m_Value)
                {
                    if (value != null)
                    {
                        m_Value = value;
                        SetLabel.SetLabelText(this, m_Value);
                    }
                    else
                    {
                        m_Value = string.Empty;
                        base.Text = string.Empty;
                    }
                }
            }
        }

        private void ReleaseValue()
        {
            try
            {
                if (m_OutputType == OutputType.MomentarySet)
                {
                    Utilities.Write(PLCAddressClick, "0");
                }
                else if (m_OutputType == OutputType.MomentaryReset)
                {
                    Utilities.Write(PLCAddressClick, "1");
                }
            }
            catch (Exception ex)
            {
                Utilities.DisplayError(this, ex.Message);
            }
        }

        private bool MouseIsDown;

        private bool HoldTimeMet;

        //*****************************************
        //* Property - Hold time before bit reset
        //*****************************************
        private readonly Timer MinHoldTimer = new Timer();
        private int m_MinimumHoldTime = 500;

        [Category("PLC Properties")]
        public int MinimumHoldTime
        {
            get => m_MinimumHoldTime;
            set
            {
                m_MinimumHoldTime = value;
                if (value > 0)
                {
                    MinHoldTimer.Interval = value;
                }
            }
        }

        //*****************************************
        //* Property - Hold time before bit reset
        //*****************************************
        private readonly Timer MaxHoldTimer = new Timer();
        private int m_MaximumHoldTime = 3000;

        [Category("PLC Properties")]
        public int MaximumHoldTime
        {
            get => m_MaximumHoldTime;
            set
            {
                m_MaximumHoldTime = value;
                if (value > 0)
                {
                    MaxHoldTimer.Interval = value;
                }
            }
        }

        //**********************************************************************
        //* If output type is set to write value, then write this value to PLC
        //**********************************************************************

        [Category("PLC Properties")] public int ValueToWrite { get; set; }
        public string PLCAddressValue { get; set; }

        #endregion

        #region Event

        private void HoldTimer_Tick(object sender, EventArgs e)
        {
            MinHoldTimer.Enabled = false;
            HoldTimeMet = true;
            if (!MouseIsDown)
            {
                ReleaseValue();
            }
        }

        private void MaxHoldTimer_Tick(object sender, EventArgs e)
        {
            MaxHoldTimer.Enabled = false;
            ReleaseValue();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            MouseIsDown = false;
            if (PLCAddressClick != null && string.Compare(PLCAddressClick, string.Empty) != 0)
            {
                if (HoldTimeMet || m_MinimumHoldTime <= 0)
                {
                    MaxHoldTimer.Enabled = false;
                    ReleaseValue();
                }
            }
        }


        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (PLCAddressClick != null && string.Compare(PLCAddressClick, string.Empty) != 0 && Enabled &&
                PLCAddressClick != null)
            {
                try
                {
                    if (OutputType == OutputType.MomentarySet)
                    {
                        Utilities.Write(PLCAddressClick, true);
                        if (m_MinimumHoldTime > 0)
                        {
                            MinHoldTimer.Enabled = true;
                        }

                        if (m_MaximumHoldTime > 0)
                        {
                            MaxHoldTimer.Enabled = true;
                        }
                    }
                    else if (OutputType == OutputType.MomentaryReset)
                    {
                        Utilities.Write(PLCAddressClick, false);
                        if (m_MinimumHoldTime > 0)
                        {
                            MinHoldTimer.Enabled = true;
                        }

                        if (m_MaximumHoldTime > 0)
                        {
                            MaxHoldTimer.Enabled = true;
                        }
                    }

                    else if (OutputType == OutputType.SetTrue)
                    {
                        Utilities.Write(PLCAddressClick, true);
                    }

                    else if (OutputType == OutputType.SetFalse)
                    {
                        Utilities.Write(PLCAddressClick, false);
                    }

                    else if (OutputType == OutputType.Toggle)
                    {
                        bool CurrentValue = Convert.ToBoolean(Value);
                        if (CurrentValue)
                        {
                            Utilities.Write(PLCAddressClick, false);
                        }
                        else
                        {
                            Utilities.Write(PLCAddressClick, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DisplayError(ex.Message);
                }
            }
        }

        public void DisplayError(string ErrorMessage)
        {
            Utilities.DisplayError(this, ErrorMessage);
        }

        #endregion
    }


}