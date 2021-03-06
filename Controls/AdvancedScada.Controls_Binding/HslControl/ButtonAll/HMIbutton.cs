﻿using AdvancedScada.Common;
using AdvancedScada.Common.Client;
using AdvancedScada.Controls_Binding.DialogEditor;
using HslControls;
using MfgControl.AdvancedHMI.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.HslControl.ButtonAll
{

    public class HMIbutton : HslButton, IPropertiesControls
    {
        public HMIbutton()
        {
            MaxHoldTimer.Tick += MaxHoldTimer_Tick;
            MinHoldTimer.Tick += HoldTimer_Tick;
        }
        #region Basic Properties
        private Color m_BackColor = Color.LightGray;
        public new Color BackColor
        {
            get => m_BackColor;
            set
            {
                if (m_BackColor != value)
                {
                    m_BackColor = value;

                    if (m_Highlight)
                    {
                        base.BackColor = m_Highlightcolor;
                    }
                    else
                    {
                        base.BackColor = m_BackColor;
                    }

                }
            }
        }

        //***************************************************************
        //* Property - Highlight Color
        //***************************************************************
        private System.Drawing.Color m_Highlightcolor = System.Drawing.Color.Green;
        public System.Drawing.Color HighlightColor
        {
            get => m_Highlightcolor;
            set => m_Highlightcolor = value;
        }

        //***************************************************************
        //* Property - If value from PLC is true, then highlight button
        //***************************************************************
        //Private OriginalBackcolor As Drawing.Color = Nothing
        private bool m_Highlight;
        public bool Highlight
        {
            get => m_Highlight;
            set
            {
                if (value)
                {
                    base.BackColor = m_Highlightcolor;
                }
                else
                {
                    base.BackColor = m_BackColor;
                }

                m_Highlight = value;
                //* V3.99y - seems to not help
                //*Invalidate()
            }
        }

        //******************************
        //* Property - Text
        //******************************
        private string m_Text;
        [System.ComponentModel.Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public new string Text
        {
            get => m_Text;
            set
            {
                m_Text = value;

                if (m_SelectTextAlternate)
                {
                    base.Text = m_TextAlternate;
                }
                else
                {
                    base.Text = m_Text;
                }
            }
        }

        private Color m_ForeColor = Color.Black;
        public new Color ForeColor
        {
            get => m_ForeColor;
            set
            {
                m_ForeColor = value;

                if (m_SelectTextAlternate)
                {
                    base.ForeColor = m_ForeColorAlternate;
                }
                else
                {
                    base.ForeColor = m_ForeColor;
                }
            }
        }


        //******************************
        //* Property - Alternate Text
        //******************************
        private string m_TextAlternate;
        [System.ComponentModel.Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string TextAlternate
        {
            get => m_TextAlternate;
            set
            {
                m_TextAlternate = value;

                if (m_SelectTextAlternate)
                {
                    base.Text = m_TextAlternate;
                }
                else
                {
                    base.Text = m_Text;
                }
            }
        }

        private Color m_ForeColorAlternate = Color.Black;
        public Color ForeColorAlternate
        {
            get => m_ForeColorAlternate;
            set
            {
                m_ForeColorAlternate = value;

                if (m_SelectTextAlternate)
                {
                    base.ForeColor = m_ForeColorAlternate;
                }
                else
                {
                    base.ForeColor = m_ForeColor;
                }
            }
        }

        //***********************************
        //* Property - Select Alternate Text
        //***********************************
        private bool m_SelectTextAlternate;
        public bool SelectTextAlternate
        {
            get => m_SelectTextAlternate;
            set
            {
                if (value != m_SelectTextAlternate)
                {
                    m_SelectTextAlternate = value;
                    if (value)
                    {
                        base.Text = m_TextAlternate;
                        base.ForeColor = m_ForeColorAlternate;
                    }
                    else
                    {
                        base.Text = m_Text;
                        base.ForeColor = m_ForeColor;
                    }
                    //Me.Invalidate()
                }
            }
        }

        #endregion
        #region PLC Related Properties

        //********************************************
        //* Property - Address in PLC for click event
        //********************************************
        private string m_PLCAddressClick = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick
        {
            get => m_PLCAddressClick;
            set => m_PLCAddressClick = value;
        }

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

                    try
                    {
                        //* When address is changed, re-subscribe to new address
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
                        DisplayError(ex.Message);
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
                        if (string.IsNullOrEmpty(m_PLCAddressText) || string.IsNullOrWhiteSpace(m_PLCAddressText) ||
                                 Licenses.LicenseManager.IsInDesignMode)
                        {
                            return;
                        }

                        Binding bd = new Binding("Visible", TagCollectionClient.Tags[m_PLCAddressText], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
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
                        if (string.IsNullOrEmpty(m_PLCAddressText) || string.IsNullOrWhiteSpace(m_PLCAddressText) ||
                                 Licenses.LicenseManager.IsInDesignMode)
                        {
                            return;
                        }

                        Binding bd = new Binding("Enabled", TagCollectionClient.Tags[m_PLCAddressText], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
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
                        Utilities.SetLabelText(this, m_Value);
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
            catch (PLCDriverException ex)
            {
                if (ex.ErrorCode == 1808)
                {
                    DisplayError("\"" + m_PLCAddressClick + "\" PLC Address not found");
                }
                else
                {
                    DisplayError(ex.Message);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to release button bit. " + ex.Message);
            }
        }

        private bool MouseIsDown;

        public bool HoldTimeMet;

        //*****************************************
        //* Property - Hold time before bit reset
        //*****************************************
        public readonly Timer MinHoldTimer = new Timer();
        public int m_MinimumHoldTime = 500;

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
        public readonly Timer MaxHoldTimer = new Timer();
        public int m_MaximumHoldTime = 3000;

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
        private int m_ValueToWrite;
        [System.ComponentModel.Category("PLC Properties")]
        public int ValueToWrite
        {
            get => m_ValueToWrite;
            set => m_ValueToWrite = value;
        }
        private bool m_SuppressErrorDisplay;
        [System.ComponentModel.DefaultValue(false)]
        public bool SuppressErrorDisplay
        {
            get => m_SuppressErrorDisplay;
            set => m_SuppressErrorDisplay = value;
        }

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
                        Utilities.Write(PLCAddressClick, "1");
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
                        Utilities.Write(PLCAddressClick, "0");
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
                        Utilities.Write(PLCAddressClick, "1");
                    }

                    else if (OutputType == OutputType.SetFalse)
                    {
                        Utilities.Write(PLCAddressClick, "0");
                    }

                    else if (OutputType == OutputType.Toggle)
                    {
                        bool CurrentValue = Convert.ToBoolean(Value);
                        if (CurrentValue)
                        {
                            Utilities.Write(PLCAddressClick, "0");
                        }
                        else
                        {
                            Utilities.Write(PLCAddressClick, "1");
                        }
                    }
                }
                catch (PLCDriverException ex)
                {
                    if (ex.ErrorCode == 1808)
                    {
                        DisplayError("\"" + m_PLCAddressClick + "\" PLC Address not found");
                    }
                    else
                    {
                        DisplayError(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    //* V3.99w - Catch a more general exception
                    DisplayError("GE. " + ex.Message);
                }
            }
        }

        #endregion

        #region Error Display
        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private System.Windows.Forms.Timer ErrorDisplayTime;

        public void DisplayError(string ErrorMessage)
        {
            if (!m_SuppressErrorDisplay)
            {
                if (ErrorDisplayTime == null)
                {
                    ErrorDisplayTime = new System.Windows.Forms.Timer();
                    ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                    ErrorDisplayTime.Interval = 5000;
                }

                //* Save the text to return to
                if (!ErrorDisplayTime.Enabled)
                {
                    OriginalText = Text;
                }

                ErrorDisplayTime.Enabled = true;
                Utilities.DisplayError(this, ErrorMessage);
                Text = ErrorMessage;
            }
        }

        private string OriginalText;
        //**************************************************************************************
        //* Return the text back to its original after displaying the error for a few seconds.
        //**************************************************************************************
        private void ErrorDisplay_Tick(object sender, System.EventArgs e)
        {
            Text = OriginalText;

            if (ErrorDisplayTime != null)
            {
                ErrorDisplayTime.Enabled = false;
                ErrorDisplayTime.Dispose();
                ErrorDisplayTime = null;
            }
        }
        #endregion
    }


}