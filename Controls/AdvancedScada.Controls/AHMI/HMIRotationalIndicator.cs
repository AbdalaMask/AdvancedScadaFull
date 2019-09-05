using AdvancedScada.Controls;
using AdvancedScada.Controls.Subscription;
using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedScada.Controls;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls_Net45;
using AdvancedScada.DriverBase.Comm;
 
public class HMIRotationalIndicator : RotationalPositionIndicator, IDisposable
{
    #region PLC Related Properties


    //*****************************************
    //* Property - Address in PLC to Link to
    //*****************************************
    private string m_PLCAddressText = string.Empty;
    [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
    public string PLCAddressText
    {
        get
        {
            return m_PLCAddressText;
        }
        set
        {
            if (m_PLCAddressText != value)
            {
                m_PLCAddressText = value;

                //* When address is changed, re-subscribe to new address
                SubscribeToCommDriver();
            }
        }
    }

    //*****************************************
    //* Property - Address in PLC to Link to
    //*****************************************
    private string m_PLCAddressVisible = string.Empty;
    [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
    public string PLCAddressVisible
    {
        get
        {
            return m_PLCAddressVisible;
        }
        set
        {
            if (m_PLCAddressVisible != value)
            {
                m_PLCAddressVisible = value;

                // If Not String.IsNullOrEmpty(m_PLCAddressVisible) Then
                //* When address is changed, re-subscribe to new address
                SubscribeToCommDriver();
                //End If
            }
        }
    }

    //*****************************************
    //* Property - Address in PLC to Link to
    //*****************************************
    private string m_PLCAddressValue;
    [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
    public string PLCAddressValue
    {
        get
        {
            return m_PLCAddressValue;
        }
        set
        {
            if (m_PLCAddressValue != value)
            {
                m_PLCAddressValue = value;

                //* When address is changed, re-subscribe to new address
                SubscribeToCommDriver();
            }
        }
    }

    //*****************************************
    //* Property - Address in PLC to Link to
    //*****************************************
    private string m_PLCAddressTargetValue;
    [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
    public string PLCAddressTargetValue
    {
        get
        {
            return m_PLCAddressTargetValue;
        }
        set
        {
            if (m_PLCAddressTargetValue != value)
            {
                m_PLCAddressTargetValue = value;

                //* When address is changed, re-subscribe to new address
                SubscribeToCommDriver();
            }
        }
    }

    //*****************************************
    //* Property - Address in PLC to Link to
    //*****************************************
    private string m_PLCAddressTargetValueTolerance;
    [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
    public string PLCAddressTargetValueTolerance
    {
        get
        {
            return m_PLCAddressTargetValueTolerance;
        }
        set
        {
            if (m_PLCAddressTargetValueTolerance != value)
            {
                m_PLCAddressTargetValueTolerance = value;

                //* When address is changed, re-subscribe to new address
                SubscribeToCommDriver();
            }
        }
    }
    //*****************************************
    //* Property - Address in PLC to Link to
    //*****************************************
    private string m_PLCAddressTarget2Value;
    [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
    public string PLCAddressTarget2Value
    {
        get
        {
            return m_PLCAddressTarget2Value;
        }
        set
        {
            if (m_PLCAddressTarget2Value != value)
            {
                m_PLCAddressTarget2Value = value;

                //* When address is changed, re-subscribe to new address
                SubscribeToCommDriver();
            }
        }
    }

    //*****************************************
    //* Property - Address in PLC to Link to
    //*****************************************
    private string m_PLCAddressShowTargetBands;
    [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
    public string PLCAddressShowTargetBands
    {
        get
        {
            return m_PLCAddressShowTargetBands;
        }
        set
        {
            if (m_PLCAddressShowTargetBands != value)
            {
                m_PLCAddressShowTargetBands = value;

                //* When address is changed, re-subscribe to new address
                SubscribeToCommDriver();
            }
        }
    }


    private bool m_SuppressErrorDisplay;
    [System.ComponentModel.DefaultValue(false)]
    public bool SuppressErrorDisplay
    {
        get
        {
            return m_SuppressErrorDisplay;
        }
        set
        {
            m_SuppressErrorDisplay = value;
        }
    }
    #endregion

    #region Events
    //********************************************************************
    //* When an instance is added to the form, set the comm component
    //* property. If a comm component does not exist, add one to the form
    //********************************************************************
    protected override void OnCreateControl()
    {
        base.OnCreateControl();

        if (this.DesignMode)
        {

        }
        else
        {
            SubscribeToCommDriver();
            //Console.WriteLine("After STCM in OnCreateControl")
        }
    }
    #endregion

    #region Constructor/Destructor
    public HMIRotationalIndicator() : base()
    {
    }

    //Public Sub New(ByVal Container As System.ComponentModel.IContainer)
    //    Me.New()
    //    'Required for Windows.Forms Class Composition Designer support
    //    Container.Add(Me)
    //End Sub

    //****************************************************************
    //* UserControl overrides dispose to clean up the component list.
    //****************************************************************
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing)
            {
                if (SubScriptions != null)
                {
                    SubScriptions.Dispose();
                }
            }
        }
        finally
        {
            base.Dispose(disposing);
        }
    }
    #endregion

    #region Subscribing and PLC data receiving
    private SubscriptionHandler SubScriptions;
    //**************************************************
    //* Subscribe to addresses in the Comm(PLC) Driver
    //**************************************************
    private void SubscribeToCommDriver()
    {
        if (!DesignMode & IsHandleCreated)
        {
            //* Create a subscription handler object
            if (SubScriptions == null)
            {
                SubScriptions = new SubscriptionHandler();

                SubScriptions.Parent = this;
                SubScriptions.DisplayError += DisplaySubscribeError;
            }

            //Console.WriteLine("Start SubscribeAutoProperties in DPM")

            SubScriptions.SubscribeAutoProperties();

            //Console.WriteLine("Done with SubscribeToCommDriver in DPM")
        }
    }

    //***************************************
    //* Call backs for returned data
    //***************************************
    private string OriginalText;
    private void PolledDataReturned(object sender, SubscriptionHandlerEventArgs e)
    {
    }

    private void DisplaySubscribeError(object sender, PlcComEventArgs e)
    {
        DisplayError(e.ErrorMessage);
    }
    #endregion

    #region Error Display
    //********************************************************
    //* Show an error via the text property for a short time
    //********************************************************
    private System.Windows.Forms.Timer ErrorDisplayTime;
    private void DisplayError(string ErrorMessage)
    {
        if (!m_SuppressErrorDisplay)
        {
            if (ErrorDisplayTime == null)
            {
                ErrorDisplayTime = new System.Windows.Forms.Timer();
                ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                ErrorDisplayTime.Interval = 6000;
            }

            //* Save the text to return to
            if (!ErrorDisplayTime.Enabled)
            {
                OriginalText = this.Text;
            }

            ErrorDisplayTime.Enabled = true;

            Text = ErrorMessage;
        }
    }


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

    #region Keypad popup for data entry
    private Keypad_v3 KeypadPopUp;

    //Public Property KPD As MfgControl.AdvancedScadaHMI.Controls.Keypad
    //*****************************************
    //* Property - Address in PLC to Write Data To
    //*****************************************
    private string m_PLCAddressKeypad = string.Empty;
    [System.ComponentModel.Category("PLC Properties")]
    public string PLCAddressKeypad
    {
        get
        {
            return m_PLCAddressKeypad;
        }
        set
        {
            if (m_PLCAddressKeypad != value)
            {
                m_PLCAddressKeypad = value;
            }
        }
    }

    private string m_KeypadText;
    public string KeypadText
    {
        get
        {
            return m_KeypadText;
        }
        set
        {
            m_KeypadText = value;
        }
    }

    private Color m_KeypadFontColor = Color.WhiteSmoke;
    public Color KeypadFontColor
    {
        get
        {
            return m_KeypadFontColor;
        }
        set
        {
            m_KeypadFontColor = value;
        }
    }


    private int m_KeypadWidth = 300;
    public int KeypadWidth
    {
        get
        {
            return m_KeypadWidth;
        }
        set
        {
            m_KeypadWidth = value;
        }
    }

    private double m_KeypadScaleFactor = 1;
    public double KeypadScaleFactor
    {
        get
        {
            return m_KeypadScaleFactor;
        }
        set
        {
            m_KeypadScaleFactor = value;
        }
    }

    private double m_KeypadMinValue;
    public double KeypadMinValue
    {
        get
        {
            return m_KeypadMinValue;
        }
        set
        {
            m_KeypadMinValue = value;
        }
    }

    private double m_KeypadMaxValue;
    public double KeypadMaxValue
    {
        get
        {
            return m_KeypadMaxValue;
        }
        set
        {
            m_KeypadMaxValue = value;
        }
    }


    private void KeypadPopUp_ButtonClick(object sender, KeypadEventArgs e)
    {
        if (e.Key == "Quit")
        {
            KeypadPopUp.Visible = false;
        }
        else if (e.Key == "Enter")
        {
            if (Utilities.client == null)
            {
                DisplayError("ComComponent Property not set");
            }
            else
            {
                if (KeypadPopUp.Value != null && (string.Compare(KeypadPopUp.Value, string.Empty) != 0))
                {
                    try
                    {
                        if (m_KeypadMaxValue != m_KeypadMinValue)
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to validate value. " + ex.Message);
                        return;
                    }

                    try
                    {
                        if (KeypadScaleFactor == 1 || KeypadScaleFactor == 0)
                        {
                            Utilities.Write(m_PLCAddressKeypad, KeypadPopUp.Value);
                        }
                        else
                        {
                            Utilities.Write(m_PLCAddressKeypad, Convert.ToDouble(KeypadPopUp.Value) / m_KeypadScaleFactor);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to write value. " + ex.Message);
                    }
                }
                KeypadPopUp.Visible = false;
            }
        }
    }

    //***********************************************************
    //* If labeled is clicked, pop up a keypad for data entry
    //***********************************************************
    protected override void OnClick(System.EventArgs e)
    {
        base.OnClick(e);

        if (m_PLCAddressKeypad != null && (string.Compare(m_PLCAddressKeypad, string.Empty) != 0) && Enabled)
        {
            if (KeypadPopUp == null)
            {
                KeypadPopUp = new Keypad_v3(m_KeypadWidth);
            }

            KeypadPopUp.ButtonClick += KeypadPopUp_ButtonClick;
            KeypadPopUp.Text = m_KeypadText;
            KeypadPopUp.ForeColor = m_KeypadFontColor;
            KeypadPopUp.Value = string.Empty;
            KeypadPopUp.StartPosition = FormStartPosition.CenterScreen;
            KeypadPopUp.TopMost = true;
            KeypadPopUp.Show();
        }
    }
    #endregion

}