using AdvancedScada.Controls_Binding.DialogEditor;
using AdvancedScada.DriverBase;
using MfgControl.AdvancedHMI.Controls;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.DigitalDisplay
{
    public class HMITempController : MfgControl.AdvancedHMI.Controls.TempController
    {


        //*****************************************
        //* Property - What to do to bit in PLC
        //*****************************************
        private OutputType m_OutputType = OutputType.MomentarySet;

        //****************************
        //* Event - Button Click
        //****************************
        //********************************************
        //* Property - Address in PLC for click event
        //********************************************
        private string m_PLCAddressClick1 = string.Empty;

        private string m_PLCAddressClick2 = string.Empty;

        private string m_PLCAddressClick3 = string.Empty;

        private string m_PLCAddressClick4 = string.Empty;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressValue = string.Empty;

        private string OriginalText;



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
                        var bd = new Binding("Value", TagCollection.Tags[m_PLCAddressValue], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }

        [Category("Numeric Display")]
        public decimal ScaleFactor
        {
            get { return _ValueScaleFactor; }
            set { _ValueScaleFactor = value; }
        }

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick1
        {
            get { return m_PLCAddressClick1; }
            set { m_PLCAddressClick1 = value; }
        }

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick2
        {
            get { return m_PLCAddressClick2; }
            set { m_PLCAddressClick2 = value; }
        }

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick3
        {
            get { return m_PLCAddressClick3; }
            set { m_PLCAddressClick3 = value; }
        }

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick4
        {
            get { return m_PLCAddressClick4; }
            set { m_PLCAddressClick4 = value; }
        }

        [Category("PLC Properties")]
        public OutputType OutputType
        {
            get { return m_OutputType; }
            set { m_OutputType = value; }
        }

        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }

        private void _Click1(object sender, EventArgs e)
        {
            MouseDownAction(m_PLCAddressClick1);
        }

        private void _MouseUp1(object sender, EventArgs e)
        {
            MouseUpAction(m_PLCAddressClick1);
        }

        private void _click2(object sender, EventArgs e)
        {
            MouseDownAction(m_PLCAddressClick2);
        }

        private void _MouseUp2(object sender, EventArgs e)
        {
            MouseUpAction(m_PLCAddressClick2);
        }

        private void _click3(object sender, EventArgs e)
        {
            MouseDownAction(m_PLCAddressClick3);
        }

        private void _MouseUp3(object sender, EventArgs e)
        {
            MouseUpAction(m_PLCAddressClick3);
        }

        private void _click4(object sender, EventArgs e)
        {
            MouseDownAction(m_PLCAddressClick4);
        }

        private void _MouseUp4(object sender, EventArgs e)
        {
            MouseUpAction(m_PLCAddressClick4);
        }


        private void MouseDownAction(string PLCAddress)
        {
            if (PLCAddress != null && string.Compare(PLCAddress, string.Empty) != 0)
                try
                {
                    switch (m_OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(PLCAddress, "1");
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(PLCAddress, "0");
                            break;
                        case OutputType.SetTrue:
                            Utilities.Write(PLCAddress, "1");
                            break;
                        case OutputType.SetFalse:
                            Utilities.Write(PLCAddress, "0");
                            break;
                        case OutputType.Toggle:
                            var CurrentValue = false;
                            CurrentValue = Convert.ToBoolean(PLCAddress);
                            if (CurrentValue)
                                Utilities.Write(PLCAddress, "0");
                            else
                                Utilities.Write(PLCAddress, "1");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError(ex.Message);
                }
        }

        private void MouseUpAction(string PLCAddress)
        {
            if (PLCAddress != null && (string.Compare(PLCAddress, string.Empty) != 0) & Enabled)
                try
                {
                    switch (OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(PLCAddress, "0");
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(PLCAddress, "1");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError(ex.Message);
                }
        }

        #region "Error Display"

        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private Timer ErrorDisplayTime;
        private decimal _ValueScaleFactor;

        private void DisplayError(string ErrorMessage)
        {
            if (!SuppressErrorDisplay)
            {
                if (ErrorDisplayTime == null)
                {
                    ErrorDisplayTime = new Timer();
                    ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                    ErrorDisplayTime.Interval = 6000;
                }

                //* Save the text to return to
                if (!ErrorDisplayTime.Enabled) OriginalText = Text;

                ErrorDisplayTime.Enabled = true;

                Text = ErrorMessage;
            }
        }

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