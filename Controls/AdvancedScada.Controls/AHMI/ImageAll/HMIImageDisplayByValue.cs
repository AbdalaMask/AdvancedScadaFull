using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Common;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.ImageAll;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls_Net45;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace AdvancedScada.Controls.AHMI.ImageAll
{
    public class HMIImageDisplayByValue : Label
    {
        #region Basic Properties
        private System.Drawing.Color SavedBackColor;

        //******************************************************************************************
        //* Use the base control's text property and make it visible as a property on the designer
        //******************************************************************************************
        [System.ComponentModel.Browsable(true), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                //* True/False comes from driver, change if BooleanDisplay is different 31-DEC-11
                if ((value == "True" || value == "False") && m_BooleanDisplay != BooleanDisplayOption.TrueFalse)
                {
                    if (value == "True")
                    {
                        if (m_BooleanDisplay == BooleanDisplayOption.OnOff)
                        {
                            value = "On";
                        }
                        if (m_BooleanDisplay == BooleanDisplayOption.YesNo)
                        {
                            value = "Yes";
                        }
                    }
                    else
                    {
                        if (m_BooleanDisplay == BooleanDisplayOption.OnOff)
                        {
                            value = "Off";
                        }
                        if (m_BooleanDisplay == BooleanDisplayOption.YesNo)
                        {
                            value = "No";
                        }
                    }
                }

                //* If suffix has already been added, then removed 17-OCT-11
                if ((m_Suffix != null && (string.Compare(m_Suffix, string.Empty) != 0)) && value.IndexOf(m_Suffix) > 0)
                {
                    value = value.Substring(0, value.IndexOf(m_Suffix));
                }

                if ((m_Format != null && (string.Compare(m_Format, string.Empty) != 0)) && (!DesignMode))
                {
                    try
                    {
                        base.Text = _Prefix + (float.Parse(value) * (float)_ValueScaleFactor).ToString(m_Format) + m_Suffix;
                    }
                    catch (InvalidCastException exC)
                    {
                        base.Text = value;
                    }
                    catch (Exception ex)
                    {
                        base.Text = "Check NumericFormat and variable type";
                    }
                }
                else
                {
                    //* Highlight in red if a Highlightcharacter found mark is in text
                    if (value.IndexOf(_HighlightKeyChar) >= 0)
                    {
                        if (base.BackColor != _Highlightcolor)
                        {
                            SavedBackColor = base.BackColor;
                        }
                        base.BackColor = _Highlightcolor;
                    }
                    else
                    {
                        if (SavedBackColor != new System.Drawing.Color())
                        {
                            base.BackColor = SavedBackColor;
                        }
                    }

                    if (_ValueScaleFactor == 1M)
                    {
                        base.Text = _Prefix + value + m_Suffix;
                    }
                    else
                    {
                        try
                        {
                            base.Text = (Convert.ToSingle(value) * (float)_ValueScaleFactor).ToString();
                        }
                        catch (Exception ex)
                        {
                            DisplayError("Scale Factor Error - " + ex.Message);
                        }
                    }
                }
            }
        }

        //**********************************
        //* Prefix and suffixes to text
        //**********************************
        private string _Prefix;
        public string TextPrefix
        {
            get
            {
                return _Prefix;
            }
            set
            {
                _Prefix = value;
                Invalidate();
            }
        }

        private string m_Suffix;
        public string TextSuffix
        {
            get
            {
                return m_Suffix;
            }
            set
            {
                m_Suffix = value;
                Invalidate();
            }
        }


        //***************************************************************
        //* Property - Highlight Color
        //***************************************************************
        private System.Drawing.Color _Highlightcolor = System.Drawing.Color.Red;
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color HighlightColor
        {
            get
            {
                return _Highlightcolor;
            }
            set
            {
                _Highlightcolor = value;
            }
        }

        private string _HighlightKeyChar = "!";
        [System.ComponentModel.Category("Appearance")]
        public string HighlightKeyCharacter
        {
            get
            {
                return _HighlightKeyChar;
            }
            set
            {
                _HighlightKeyChar = value;
            }
        }


        private string m_Format;
        public string NumericFormat
        {
            get
            {
                return m_Format;
            }
            set
            {
                m_Format = value;
            }
        }

        private decimal _ValueScaleFactor = 1M;
        public decimal ScaleFactor
        {
            get
            {
                return _ValueScaleFactor;
            }
            set
            {
                _ValueScaleFactor = value;
            }
        }

        public enum BooleanDisplayOption
        {
            TrueFalse,
            YesNo,
            OnOff
        }

        private BooleanDisplayOption m_BooleanDisplay;
        public BooleanDisplayOption BooleanDisplay
        {
            get
            {
                return m_BooleanDisplay;
            }
            set
            {
                m_BooleanDisplay = value;
            }
        }
        #endregion

        #region PLC Related Properties

        private int _PollRate;
        public int PollRate
        {
            get
            {
                return _PollRate;
            }
            set
            {
                _PollRate = value;
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


        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressText = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
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
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressImageIndex = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressImageIndex
        {
            get
            {
                return m_PLCAddressImageIndex;
            }
            set
            {
                if (m_PLCAddressImageIndex != value)
                {
                    m_PLCAddressImageIndex = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
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

                    //* When address is changed, re-subscribe to new address
                    SubscribeToComDriver();
                }
            }
        }

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

        public string PLCAddressLeft { get; set; }
        public string PLCAddressTop { get; set; }

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

                //********************************************************
                //* Search for ImageList component in parent form
                //* If one exists, set the client of this component to it
                //********************************************************
                int i = 0;
                int j = this.Parent.Site.Container.Components.Count;
                while (base.ImageList == null && i < j)
                {
                    if (this.Site.Container.Components[i].GetType().ToString() == "System.Windows.Forms.ImageList")
                    {
                        base.ImageList = (ImageList)this.Site.Container.Components[i];
                    }
                    i += 1;
                }

                //************************************************
                //* If no ImageList was found, then add one and
                //* point the ImageList property to it
                //*********************************************
                if (base.ImageList == null)
                {
                    this.Site.Container.Add(new System.Windows.Forms.ImageList());
                    base.ImageList = (ImageList)this.Parent.Site.Container.Components[this.Site.Container.Components.Count - 1];
                    //MyBase.ImageIndex = 0
                    base.ImageList.ColorDepth = ColorDepth.Depth16Bit;
                    base.ImageList.ImageSize = new System.Drawing.Size(this.Width, this.Height);
                    base.ImageList.TransparentColor = System.Drawing.Color.Transparent;
                    base.AutoSize = false;
                    base.ImageList.ImageSize = this.Size;
                }

            }
            else
            {
                SubscribeToComDriver();
            }
        }


        private void ImageDisplayByValue2_SizeChanged(object sender, System.EventArgs e)
        {
            //Try
            //    If ImageList IsNot Nothing Then
            //        ImageList.ImageSize = New System.Drawing.Size(Me.Width, Me.Height)
            //        ImageIndex = 1
            //    End If
            //Catch ex As Exception
            //    System.Windows.Forms.MessageBox.Show(ex.Message)
            //End Try
        }

        #endregion

        #region Constructor/Destructor
        public HMIImageDisplayByValue() : base()
        {

            //If Me.Parent.BackColor = System.Drawing.Color.Black And _
            if (base.ForeColor == System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ControlText) || ForeColor == Color.FromArgb(0, 0, 0, 0))
            {
                ForeColor = System.Drawing.Color.WhiteSmoke;
            }
            this.SizeChanged += ImageDisplayByValue2_SizeChanged;
            this.Click += BasicLabelWithEntry_Click;
        }

        //****************************************************************
        //* UserControl overrides dispose to clean up the component list.
        //****************************************************************
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    //* Unsubscribe from the subscriptions
                    if (SubScriptions != null)
                    {
                        SubScriptions.Dispose();
                    }

                    if (KeypadPopUp != null)
                    {
                        KeypadPopUp.Dispose();
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
        private void SubscribeToComDriver()
        {
            if (!DesignMode && IsHandleCreated)
            {
                //* Create a subscription handler object
                if (SubScriptions == null)
                {
                    SubScriptions = new SubscriptionHandler();
                    SubScriptions.Parent = this;
                    SubScriptions.DisplayError += DisplaySubscribeError;
                }

                SubScriptions.SubscribeAutoProperties();
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
                    ErrorDisplayTime.Interval = 5000;
                }

                //* Save the text to return to
                if (!ErrorDisplayTime.Enabled)
                {
                    OriginalText = this.Text;
                }

                ErrorDisplayTime.Enabled = true;

                base.Text = ErrorMessage;
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
                        if (ScaleFactor == 1M)
                        {
                            Utilities.Write(m_PLCAddressKeypad, KeypadPopUp.Value);
                        }
                        else
                        {
                            Utilities.Write(m_PLCAddressKeypad, Convert.ToSingle(KeypadPopUp.Value) / (float)ScaleFactor);
                        }
                    }
                    else
                    {
                        //DisplayError("ComComponent Property not set")
                    }
                    KeypadPopUp.Visible = false;
                }
            }
        }

        //***********************************************************
        //* If labeled is clicked, pop up a keypad for data entry
        //***********************************************************
        private void BasicLabelWithEntry_Click(object sender, System.EventArgs e)
        {
            if (m_PLCAddressKeypad != null && (string.Compare(m_PLCAddressKeypad, string.Empty) != 0) && Enabled)
            {
                if (KeypadPopUp == null)
                {
                    KeypadPopUp = new Keypad_v3(m_KeypadWidth);
                }

                KeypadPopUp.Text = m_KeypadText;
                KeypadPopUp.Value = string.Empty;
                KeypadPopUp.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                KeypadPopUp.TopMost = true;
                KeypadPopUp.Show();
            }
        }
        #endregion
    }
}
