using AdvancedScada.Common;
using AdvancedScada.Common.Client;
using AdvancedScada.Controls_Binding.DialogEditor;
using MfgControl.AdvancedHMI.Controls;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.ProcessAll
{
    public class HMIProcessIndicator : DasNetIndicator.DAS_Net_ProcessIndicator, IPropertiesControls
    {

        public HMIProcessIndicator()
        {
            MaxHoldTimer.Tick += MaxHoldTimer_Tick;
            MinHoldTimer.Tick += HoldTimer_Tick;
        }
        #region PLC Properties
        public bool HoldTimeMet;

        //*****************************************
        //* Property - Hold time before bit reset
        //*****************************************
        private readonly Timer MaxHoldTimer = new Timer();

        //*****************************************
        //* Property - Hold time before bit reset
        //*****************************************
        private readonly Timer MinHoldTimer = new Timer();
        private readonly bool MouseIsDown = false;
        public OutputType OutputType { get; set; }
        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressText = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressText
        {
            get { return m_PLCAddressText; }
            set
            {
                if (m_PLCAddressText != value)
                {
                    m_PLCAddressText = value;

                    try
                    {
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressText) || string.IsNullOrWhiteSpace(m_PLCAddressText) ||
                            Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("Text", TagCollectionClient.Tags[m_PLCAddressValue], "Value", true);
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
            get { return m_PLCAddressVisible; }
            set
            {
                if (m_PLCAddressVisible != value)
                {
                    m_PLCAddressVisible = value;

                    try
                    {
                        // If Not String.IsNullOrEmpty(m_PLCAddressVisible) Then
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressVisible) ||
                            string.IsNullOrWhiteSpace(m_PLCAddressVisible) || Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("Visible", TagCollectionClient.Tags[m_PLCAddressVisible], "Value", true);
                        DataBindings.Add(bd);
                        //End If
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
        private string m_PLCAddressValue = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressValue
        {
            get { return m_PLCAddressValue; }
            set
            {
                if (m_PLCAddressValue != value)
                {
                    m_PLCAddressValue = value;

                    try
                    {
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressValue) || string.IsNullOrWhiteSpace(m_PLCAddressValue) ||
                            Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("Value", TagCollectionClient.Tags[m_PLCAddressValue], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick { get; set; }
        public string PLCAddressEnabled { get; set; }
        [DefaultValue(true)]
        public bool SuppressErrorDisplay { get; set; }

        #endregion
        private void ReleaseValue()
        {
            try
            {
                switch (OutputType)
                {
                    case OutputType.MomentarySet:
                        Utilities.Write(PLCAddressClick, false);
                        break;
                    case OutputType.MomentaryReset:
                        Utilities.Write(PLCAddressClick, true);
                        break;
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
            }
        }

        private void HoldTimer_Tick(object sender, EventArgs e)
        {
            MinHoldTimer.Enabled = false;
            HoldTimeMet = true;
            if (!MouseIsDown) ReleaseValue();
        }

        private void MaxHoldTimer_Tick(object sender, EventArgs e)
        {
            MaxHoldTimer.Enabled = false;
            ReleaseValue();
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);


            if (!string.IsNullOrWhiteSpace(PLCAddressClick) & Enabled && PLCAddressClick != null)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(PLCAddressClick, true);
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(PLCAddressClick, false);
                            break;
                        case OutputType.SetTrue:
                            Utilities.Write(PLCAddressClick, true);
                            break;
                        case OutputType.SetFalse:
                            Utilities.Write(PLCAddressClick, false);
                            break;
                        case OutputType.Toggle:
                            var CurrentValue = Value;
                            if (CurrentValue)
                                Utilities.Write(PLCAddressClick, false);
                            else
                                Utilities.Write(PLCAddressClick, true);
                            break;
                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError("WRITE FAILED!" + ex.Message);
                }

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!string.IsNullOrWhiteSpace(PLCAddressClick) & Enabled)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(PLCAddressClick, false);
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(PLCAddressClick, true);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError("WRITE FAILED!" + ex.Message);
                }

                Invalidate();
            }
        }



        #region "Error Display"

        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private Timer ErrorDisplayTime;

        public void DisplayError(string ErrorMessage)
        {
            if (!SuppressErrorDisplay)
            {
                if (ErrorDisplayTime == null)
                {
                    ErrorDisplayTime = new Timer();
                    ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                    ErrorDisplayTime.Interval = 5000;
                }

                //* Save the text to return to
                if (!ErrorDisplayTime.Enabled) OriginalText = Text;

                ErrorDisplayTime.Enabled = true;

                Text = ErrorMessage;
                Utilities.DisplayError(this, ErrorMessage);
            }
        }
        private string OriginalText;
        //**************************************************************************************
        //* Return the text back to its original after displaying the error for a few seconds.
        //**************************************************************************************
        private void ErrorDisplay_Tick(object sender, EventArgs e)
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
