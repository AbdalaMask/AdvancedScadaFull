using AdvancedScada.Controls.DialogEditor;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using AdvancedScada;
 
 
 
using AdvancedScada.Controls.DialogEditor;
using AdvancedScada.Controls_Net45;
using AdvancedScada.Controls.AHMI.Display;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;

namespace AdvancedScada.Controls.AHMI.Display
{
    public class HMIKeyboardInput : AdvancedScada.Controls_Net45.KeyboardInput
    {


        #region PLC Related Properties

        private OutputType m_OutputType = OutputType.MomentarySet;

        public OutputType OutputType
        {
            get { return m_OutputType; }
            set { m_OutputType = value; }
        }

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
        private string m_PLCAddressGetFocusValue = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressGetFocusValue
        {
            get { return m_PLCAddressGetFocusValue; }
            set
            {
                if (m_PLCAddressGetFocusValue != value)
                {
                    m_PLCAddressGetFocusValue = value;

                    try
                    {

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
        private string m_PLCAddressWriteValue = string.Empty;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressWriteValue
        {
            get { return m_PLCAddressWriteValue; }
            set
            {
                if (m_PLCAddressWriteValue != value) m_PLCAddressWriteValue = value;
            }
        }

        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }


        //***************************************
        //* Call backs for returned data
        //***************************************
        private string OriginalText;

        #region "Error Display"

        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private Timer ErrorDisplayTime;

        private void DisplayError(string ErrorMessage)
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

        protected override void OnEnterKeyPressed(EventArgs e)
        {
            base.OnEnterKeyPressed(e);

            try
            {
                if (m_PLCAddressWriteValue != null)
                    Utilities.Write(m_PLCAddressWriteValue, double.Parse(Text));
                else
                    MessageBox.Show("PLCAddressWriteValue not set to anything");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to write value - " + ex.Message);
            }
        }

        #endregion
    }
}