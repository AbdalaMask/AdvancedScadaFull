using AdvancedScada.Controls_Binding.DialogEditor;
using AdvancedScada.Common;
using AdvancedScada.Common.Client;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.ImageAll
{

    [ToolboxItem(true)]
    public class HMIImageContainer : MfgControl.AdvancedHMI.Controls.GraphicIndicator, IPropertiesControls
    {
        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressClick = string.Empty;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressSelect1 = string.Empty;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressSelect2 = string.Empty;


        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressText2 = string.Empty;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;

        private string OriginalText;

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressValueSelect1
        {
            get { return m_PLCAddressSelect1; }
            set
            {
                if (m_PLCAddressSelect1 != value)
                {
                    m_PLCAddressSelect1 = value;

                    try
                    {
                        if (string.IsNullOrEmpty(m_PLCAddressSelect1) ||
                       string.IsNullOrWhiteSpace(m_PLCAddressSelect1) || Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("ValueSelect1", TagCollectionClient.Tags[m_PLCAddressSelect1], "Value", true);
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
        public string PLCAddressValueSelect2
        {
            get { return m_PLCAddressSelect2; }
            set
            {
                if (m_PLCAddressSelect2 != value)
                {
                    m_PLCAddressSelect2 = value;
                    //* When address is changed, re-subscribe to new address
                    try
                    {
                        // If Not String.IsNullOrEmpty(m_PLCAddressVisible) Then
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressSelect2) ||
                            string.IsNullOrWhiteSpace(m_PLCAddressSelect2) || Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("ValueSelect2", TagCollectionClient.Tags[m_PLCAddressSelect2], "Value", true);
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
                    //* When address is changed, re-subscribe to new address
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

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressText2
        {
            get { return m_PLCAddressText2; }
            set
            {
                if (m_PLCAddressText2 != value) m_PLCAddressText2 = value;
            }
        }

        [Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressClick
        {
            get { return m_PLCAddressClick; }
            set { m_PLCAddressClick = value; }
        }

        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }
        public string PLCAddressValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PLCAddressEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
