using AdvancedScada.Common;
using AdvancedScada.Common.Client;
using AdvancedScada.Controls_Binding.DialogEditor;
using AdvancedScada.Controls_Net45;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.Display
{
    public class HMILabel : System.Windows.Forms.Label, IPropertiesControls
    {
        public event EventHandler ValueChanged;

        #region Constructor
        public HMILabel() : base()
        {

            Value = "BasicLabel";

            if (base.ForeColor == System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ControlText) || ForeColor == Color.FromArgb(0, 0, 0))
            {
                ForeColor = System.Drawing.Color.WhiteSmoke;
            }

            //* V3.99y beta 26
            BackColor = Color.Transparent;
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

                    if (m_KeypadFont != null)
                    {
                        m_KeypadFont.Dispose();
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        #endregion

        #region Basic Properties
        //Private SavedBackColor As System.Drawing.Color

        //* Remove Text from the property window so users do not attempt to use it
        [System.ComponentModel.Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        //******************************************************************************************
        //* Use the base control's text property and make it visible as a property on the designer
        //******************************************************************************************
        private string m_Value;
        [System.ComponentModel.EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design", "System.Drawing.Design.UITypeEditor")]
        public string Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                if (value != m_Value)
                {
                    if (value != null)
                    {
                        m_Value = value;
                        UpdateText();
                        OnvalueChanged(EventArgs.Empty);
                    }
                    else
                    {
                        //* version 3.99f
                        if (!string.IsNullOrEmpty(m_Value))
                        {
                            OnvalueChanged(EventArgs.Empty);
                        }
                        m_Value = string.Empty;
                        base.Text = string.Empty;
                    }
                    //* Be sure error handler doesn't revert back to an incorrect text
                    OriginalText = base.Text;
                }
            }
        }

        private char m_ValueLeftPadCharacter = ' ';
        public char ValueLeftPadCharacter
        {
            get
            {
                return m_ValueLeftPadCharacter;
            }
            set
            {
                m_ValueLeftPadCharacter = value;
                UpdateText();
            }
        }

        private int m_ValueLeftPadLength;
        public int ValueLeftPadLength
        {
            get
            {
                return m_ValueLeftPadLength;
            }
            set
            {
                m_ValueLeftPadLength = value;
                UpdateText();
            }
        }


        //**********************************
        //* Prefix and suffixes to text
        //**********************************
        private string m_Prefix;
        public string ValuePrefix
        {
            get
            {
                return m_Prefix;
            }
            set
            {
                if (m_Prefix != value)
                {
                    m_Prefix = value;
                    UpdateText();
                }
            }
        }

        private string m_Suffix;
        public string ValueSuffix
        {
            get
            {
                return m_Suffix;
            }
            set
            {
                if (m_Suffix != value)
                {
                    m_Suffix = value;
                    UpdateText();
                }
            }
        }

        private float m_ValueToSubtractFrom;
        public float ValueToSubtractFrom
        {
            get
            {
                return m_ValueToSubtractFrom;
            }
            set
            {
                m_ValueToSubtractFrom = value;
            }
        }

        private bool m_InterpretValueAsBCD;
        public bool InterpretValueAsBCD
        {
            get
            {
                return m_InterpretValueAsBCD;
            }
            set
            {
                m_InterpretValueAsBCD = value;
            }
        }

        private Color m_BackColor = Color.Black;
        public new Color BackColor
        {
            get
            {
                return m_BackColor;
            }
            set
            {
                if (m_BackColor != value)
                {
                    m_BackColor = value;
                    UpdateText();
                }
            }
        }

        //***************************************************************
        //* Property - Highlight Color
        //***************************************************************
        private System.Drawing.Color m_Highlightcolor = System.Drawing.Color.Red;
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color HighlightColor
        {
            get
            {
                return m_Highlightcolor;
            }
            set
            {
                if (m_Highlightcolor != value)
                {
                    m_Highlightcolor = value;
                    UpdateText();
                }
            }
        }

        private System.Drawing.Color m_HighlightForecolor = System.Drawing.Color.White;
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color HighlightForeColor
        {
            get
            {
                return m_HighlightForecolor;
            }
            set
            {
                if (m_HighlightForecolor != value)
                {
                    m_HighlightForecolor = value;
                    UpdateText();
                }
            }
        }

        private Color m_ForeColor = Color.White;
        public new Color ForeColor
        {
            get
            {
                return m_ForeColor;
            }
            set
            {
                if (m_ForeColor != value)
                {
                    m_ForeColor = value;
                    UpdateText();
                }
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
                if (_HighlightKeyChar != value)
                {
                    _HighlightKeyChar = value;
                    UpdateText();
                }
            }
        }

        private bool m_Highlight;
        [System.ComponentModel.Category("Appearance"), System.ComponentModel.Description("Switches to Highlight colors")]
        public bool Highlight
        {
            get
            {
                return m_Highlight;
            }
            set
            {
                if (m_Highlight != value)
                {
                    m_Highlight = value;
                    UpdateText();
                }
            }
        }


        private string m_NumericFormat;
        public string NumericFormat
        {
            get
            {
                return m_NumericFormat;
            }
            set
            {
                if (m_NumericFormat != value)
                {
                    m_NumericFormat = value;
                    UpdateText();
                }
            }
        }

        private double m_ValueScaleFactor = 1;
        public double ValueScaleFactor
        {
            get
            {
                return m_ValueScaleFactor;
            }
            set
            {
                if (m_ValueScaleFactor != value)
                {
                    m_ValueScaleFactor = value;
                    UpdateText();
                }
                //TODO: Does not refresh in designmode
                //Text = MyBase.Text
            }
        }

        public enum BooleanDisplayOption
        {
            TrueFalse,
            YesNo,
            OnOff,
            OneZero
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
                if (m_BooleanDisplay != value)
                {
                    m_BooleanDisplay = value;
                    UpdateText();
                }
            }
        }

        private bool m_DisplayAsTime;
        public bool DisplayAsTime
        {
            get
            {
                return m_DisplayAsTime;
            }
            set
            {
                if (m_DisplayAsTime != value)
                {
                    m_DisplayAsTime = value;
                    UpdateText();
                }
            }
        }
        #endregion

        #region PLC Related Properties


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

        private Font m_KeypadFont = new Font("Arial", 10);
        public Font KeypadFont
        {
            get
            {
                return m_KeypadFont;
            }
            set
            {
                m_KeypadFont = value;
            }
        }

        private Color m_KeypadForeColor = Color.WhiteSmoke;
        public Color KeypadFontColor
        {
            get
            {
                return m_KeypadForeColor;
            }
            set
            {
                m_KeypadForeColor = value;
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

        //* 29-JAN-13
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

        private double m_KeypadScaleFactor = 1;
        [System.ComponentModel.DefaultValue(1)]
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

        private bool m_KeypadAlphanumeric;
        public bool KeypadAlphanumeric
        {
            get
            {
                return m_KeypadAlphanumeric;
            }
            set
            {
                m_KeypadAlphanumeric = value;
            }
        }

        private bool m_KeypadShowCurrentValue;
        public bool KeypadShowCurrentValue
        {
            get
            {
                return m_KeypadShowCurrentValue;
            }
            set
            {
                m_KeypadShowCurrentValue = value;
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



        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressValue = string.Empty;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
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



        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
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

                    try
                    {
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressVisible) || string.IsNullOrWhiteSpace(m_PLCAddressVisible) ||
                            Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("Visible", TagCollectionClient.Tags[m_PLCAddressVisible], "Value", true);
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
        private string m_PLCAddressHighlight = string.Empty;
        [System.ComponentModel.DefaultValue(""), System.ComponentModel.Category("PLC Properties")]
        [Editor(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string PLCAddressHighlight
        {
            get
            {
                return m_PLCAddressHighlight;
            }
            set
            {
                if (m_PLCAddressHighlight != value)
                {
                    m_PLCAddressHighlight = value;

                    try
                    {
                        //* When address is changed, re-subscribe to new address
                        if (string.IsNullOrEmpty(m_PLCAddressHighlight) || string.IsNullOrWhiteSpace(m_PLCAddressHighlight) ||
                            Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("Highlight", TagCollectionClient.Tags[m_PLCAddressHighlight], "Value", true);
                        DataBindings.Add(bd);
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }

        #endregion

        #region Private Methods
        private void UpdateText()
        {
            //* Build the string with a temporary variable because Mybase.Text will keep firing Me.Invalidate
            string ResultText = m_Value;

            if (!string.IsNullOrEmpty(ResultText))
            {
                //* True/False comes from driver, change if BooleanDisplay is different 31-DEC-11
                if (string.Compare(m_Value, "True", true) == 0)
                {
                    if (m_BooleanDisplay == BooleanDisplayOption.OnOff)
                    {
                        ResultText = "On";
                    }
                    if (m_BooleanDisplay == BooleanDisplayOption.YesNo)
                    {
                        ResultText = "Yes";
                    }
                    if (m_BooleanDisplay == BooleanDisplayOption.TrueFalse)
                    {
                        ResultText = "True";
                    }
                    if (m_BooleanDisplay == BooleanDisplayOption.OneZero)
                    {
                        ResultText = "1";
                    }
                }
                else if (string.Compare(m_Value, "False", true) == 0)
                {
                    if (m_BooleanDisplay == BooleanDisplayOption.OnOff)
                    {
                        ResultText = "Off";
                    }
                    if (m_BooleanDisplay == BooleanDisplayOption.YesNo)
                    {
                        ResultText = "No";
                    }
                    if (m_BooleanDisplay == BooleanDisplayOption.TrueFalse)
                    {
                        ResultText = "False";
                    }
                    if (m_BooleanDisplay == BooleanDisplayOption.OneZero)
                    {
                        ResultText = "0";
                    }
                }
                else
                {
                    //* V3.99v
                    if (m_InterpretValueAsBCD)
                    {
                        try
                        {
                            byte[] b = BitConverter.GetBytes(int.Parse(ResultText));
                            ResultText = string.Empty;

                            for (var index = 3; index >= 0; index--)
                            {
                                if ((b[index] & 240) > 0 || ResultText.Length > 0)
                                {
                                    ResultText += ((b[index] & 240) >> 4).ToString();
                                }
                                if ((b[index] & 15) > 0 || ResultText.Length > 0)
                                {
                                    ResultText += (b[index] & 15).ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            DisplayError(ex.Message);
                            ResultText = "BCD Error";
                        }
                    }

                    //******************************************************
                    //* Scale Factor and Format only applied to non-Boolean
                    //******************************************************
                    //* Apply the scale factor
                    try
                    {
                        if (m_ValueScaleFactor != 1)
                        {
                            ResultText = Convert.ToString(Convert.ToSingle(ResultText) * m_ValueScaleFactor);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!DesignMode)
                        {
                            DisplayError("Scale Factor Error - " + ex.Message);
                        }
                    }

                    //* Apply the format
                    if ((!string.IsNullOrEmpty(m_NumericFormat)) && !m_DisplayAsTime)
                    {
                        try
                        {
                            //* 31-MAY-13, 17-JUN-15 Changed from Single to Double to prevent rounding problems
                            double v = 0;
                            if (double.TryParse(ResultText, out v))
                            {
                                ResultText = v.ToString(m_NumericFormat);
                            }
                        }
                        catch (InvalidCastException ex)
                        {
                            DisplayError(ex.Message);
                            if (!DesignMode)
                            {
                                ResultText = "----";
                            }
                            else
                            {
                                ResultText = Value;
                            }
                        }
                        catch (Exception ex)
                        {
                            DisplayError(ex.Message);
                            if (!DesignMode)
                            {
                                ResultText = "Check NumericFormat and variable type";
                            }
                        }
                    }


                    if (m_DisplayAsTime)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(Value))
                            {
                                double ScaledValue = double.Parse(Value) * m_ValueScaleFactor;
                                int remainder = 0;
                                ResultText = Math.DivRem(Convert.ToInt32(ScaledValue), 3600, out remainder) + ":" + Math.DivRem(remainder, 60, out remainder).ToString("00") + ":" + remainder.ToString("00");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!this.DesignMode)
                            {
                                base.Text = ex.Message;
                            }
                            return;
                        }
                    }
                }
                //End If

                if (m_ValueToSubtractFrom != 0F)
                {
                    try
                    {
                        ResultText = (m_ValueToSubtractFrom - float.Parse(ResultText)).ToString();
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }


                //* Apply the left padding
                if (m_ValueLeftPadLength > 0)
                {
                    ResultText = ResultText.PadLeft(m_ValueLeftPadLength, m_ValueLeftPadCharacter);
                }

            }
            else
            {
                ResultText = string.Empty;
            }

            //* Highlight in red if a Highlightcharacter found mark is in text
            //If Not DesignMode Then
            if (Value.IndexOf(_HighlightKeyChar) >= 0 || m_Highlight)
            {
                //If MyBase.BackColor <> _Highlightcolor Then SavedBackColor = MyBase.BackColor
                base.BackColor = m_Highlightcolor;
                base.ForeColor = m_HighlightForecolor;
            }
            else
            {
                //If SavedBackColor <> Nothing Then MyBase.BackColor = SavedBackColor
                base.BackColor = m_BackColor;
                base.ForeColor = m_ForeColor;
            }
            //End If


            //* Apply the Prefix and Suffix
            if (!string.IsNullOrEmpty(m_Prefix))
            {
                ResultText = m_Prefix + ResultText;
            }
            if (!string.IsNullOrEmpty(m_Suffix))
            {
                ResultText += m_Suffix;
            }


            Utilities.SetLabelText(this, ResultText);
        }
        #endregion

        #region Events

        protected virtual void OnvalueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }
        #endregion


        //***************************************
        //* Call backs for returned data
        //***************************************
        private string OriginalText;
        #region Error Display
        private void DisplaySubscribeError(object sender, string e)
        {
            DisplayError(e);
        }

        //********************************************************
        //* Show an error via the text property for a short time
        //********************************************************
        private System.Windows.Forms.Timer ErrorDisplayTime;
        private object ErrorLock = new object();
        public void DisplayError(string ErrorMessage)
        {
            if (!m_SuppressErrorDisplay)
            {
                //* Create the error display timer
                if (ErrorDisplayTime == null)
                {
                    ErrorDisplayTime = new System.Windows.Forms.Timer();
                    ErrorDisplayTime.Tick += ErrorDisplay_Tick;
                    ErrorDisplayTime.Interval = 5000;
                }

                //* Save the text to return to
                lock (ErrorLock)
                {
                    if (!ErrorDisplayTime.Enabled)
                    {
                        ErrorDisplayTime.Enabled = true;
                        OriginalText = base.Text;
                        base.Text = ErrorMessage;
                        Utilities.DisplayError(this, ErrorMessage);
                    }
                }
            }
        }


        //**************************************************************************************
        //* Return the text back to its original after displaying the error for a few seconds.
        //**************************************************************************************
        private void ErrorDisplay_Tick(object sender, System.EventArgs e)
        {
            //UpdateText()
            lock (ErrorLock)
            {
                base.Text = OriginalText;
                //If ErrorDisplayTime IsNot Nothing Then
                ErrorDisplayTime.Enabled = false;
                // ErrorIsDisplayed = False
            }

        }
        #endregion

        #region "Keypad popup for data entry"

        private Keypad_v3 KeypadPopUp;

        //*****************************************
        //* Property - Address in PLC to Write Data To
        //*****************************************
        private string m_PLCAddressKeypad = string.Empty;

        [Category("PLC Properties")]
        public string PLCAddressKeypad
        {
            get { return m_PLCAddressKeypad; }
            set
            {
                if (m_PLCAddressKeypad != value) m_PLCAddressKeypad = value;
            }
        }

        public bool KeypadAlphaNumeric { get; set; }
        public int PollRate { get; set; }
        public string PLCAddressClick { get; set; }
        public string PLCAddressEnabled { get; set; }

        private Color m_KeypadFontColor = Color.WhiteSmoke;



        private void KeypadPopUp_ButtonClick(object sender, KeypadEventArgs e)
        {
            if (e.Key == "Quit")
            {
                KeypadPopUp.Visible = false;
            }
            else if (e.Key == "Enter")
            {
                if (KeypadPopUp.Value != null && string.Compare(KeypadPopUp.Value, string.Empty) != 0)
                {
                    try
                    {
                        if (KeypadMaxValue != KeypadMinValue)
                            if ((Convert.ToDouble(KeypadPopUp.Value) < KeypadMinValue) |
                                (Convert.ToDouble(KeypadPopUp.Value) > KeypadMaxValue))
                            {
                                MessageBox.Show("Value must be >" + KeypadMinValue + " and <" + KeypadMaxValue);
                                return;
                            }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to validate value. " + ex.Message);
                        return;
                    }

                    try
                    {
                        if ((KeypadScaleFactor == 1) | (KeypadScaleFactor == 0))
                        {
                            Utilities.Write(m_PLCAddressKeypad, KeypadPopUp.Value);
                        }
                        else
                        {
                            var v = Convert.ToDouble(KeypadPopUp.Value);
                            var z = v / m_KeypadScaleFactor;
                            Utilities.Write(m_PLCAddressKeypad,
                                (Convert.ToDouble(KeypadPopUp.Value) / m_KeypadScaleFactor).ToString());
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

        //***********************************************************
        //* If labeled is clicked, pop up a keypad for data entry
        //***********************************************************
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (m_PLCAddressKeypad != null && (string.Compare(m_PLCAddressKeypad, string.Empty) != 0) & Enabled)
            {
                if (KeypadPopUp == null)
                {
                    KeypadPopUp = new Keypad_v3(m_KeypadWidth);
                    KeypadPopUp.ButtonClick += KeypadPopUp_ButtonClick;
                }

                KeypadPopUp.Text = KeypadText;
                KeypadPopUp.ForeColor = m_KeypadFontColor;
                KeypadPopUp.Value = string.Empty;
                KeypadPopUp.StartPosition = FormStartPosition.CenterScreen;
                KeypadPopUp.TopMost = true;
                KeypadPopUp.Show();
            }
        }

        #endregion
    }
}