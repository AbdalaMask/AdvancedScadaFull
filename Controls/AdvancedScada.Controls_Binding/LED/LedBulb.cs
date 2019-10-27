using AdvancedScada.Common;
using AdvancedScada.Common.Client;
using AdvancedScada.Controls_Binding;
using AdvancedScada.Controls_Binding.DialogEditor;
using MfgControl.AdvancedHMI.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.Bulb
{

    /// <summary>
    /// The LEDBulb is a .Net control for Windows Forms that emulates an
    /// LED light with two states On and Off.  The purpose of the control is to 
    /// provide a sleek looking representation of an LED light that is sizable, 
    /// has a transparent background and can be set to different colors.  
    /// </summary>
    public partial class LedBulb : Control, IPropertiesControls
    {

        #region Public and Private Members

        private Color _color;
        private bool _on = true;
        private Color _reflectionColor = Color.FromArgb(180, 255, 255, 255);
        private Color[] _surroundColor = new Color[] { Color.FromArgb(0, 255, 255, 255) };
        private Timer _timer = new Timer();

        /// <summary>
        /// Gets or Sets the color of the LED light
        /// </summary>
        [DefaultValue(typeof(Color), "153, 255, 54")]
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                this.DarkColor = ControlPaint.Dark(_color);
                this.DarkDarkColor = ControlPaint.DarkDark(_color);
                this.Invalidate();  // Redraw the control
            }
        }

        /// <summary>
        /// Dark shade of the LED color used for gradient
        /// </summary>
        public Color DarkColor { get; protected set; }

        /// <summary>
        /// Very dark shade of the LED color used for gradient
        /// </summary>
        public Color DarkDarkColor { get; protected set; }

        /// <summary>
        /// Gets or Sets whether the light is turned on
        /// </summary>
        public bool On
        {
            get { return _on; }
            set { _on = value; this.Invalidate(); }
        }

 
        #endregion

        #region Constructor

        public LedBulb()
        {
            SetStyle(ControlStyles.DoubleBuffer
            | ControlStyles.AllPaintingInWmPaint
            | ControlStyles.ResizeRedraw
            | ControlStyles.UserPaint
            | ControlStyles.SupportsTransparentBackColor, true);

            this.Color = Color.FromArgb(255, 153, 255, 54);
            _timer.Tick += new EventHandler(
                (object sender, EventArgs e) => { this.On = !this.On; }
            );
            MaxHoldTimer.Tick += MaxHoldTimer_Tick;
            MinHoldTimer.Tick += HoldTimer_Tick;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Paint event for this UserControl
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Create an offscreen graphics object for double buffering
            Bitmap offScreenBmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            using (System.Drawing.Graphics g = Graphics.FromImage(offScreenBmp))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                // Draw the control
                drawControl(g, this.On);
                // Draw the image to the screen
                e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
            }
        }

        /// <summary>
        /// Renders the control to an image
        /// </summary>
        private void drawControl(Graphics g, bool on)
        {
            // Is the bulb on or off
            Color lightColor = (on) ? this.Color : Color.FromArgb(150, this.DarkColor);
            Color darkColor = (on) ? this.DarkColor : this.DarkDarkColor;

            // Calculate the dimensions of the bulb
            int width = this.Width - (this.Padding.Left + this.Padding.Right);
            int height = this.Height - (this.Padding.Top + this.Padding.Bottom);
            // Diameter is the lesser of width and height
            int diameter = Math.Min(width, height);
            // Subtract 1 pixel so ellipse doesn't get cut off
            diameter = Math.Max(diameter - 1, 1);

            // Draw the background ellipse
            var rectangle = new Rectangle(this.Padding.Left, this.Padding.Top, diameter, diameter);
            g.FillEllipse(new SolidBrush(darkColor), rectangle);

            // Draw the glow gradient
            var path = new GraphicsPath();
            path.AddEllipse(rectangle);
            var pathBrush = new PathGradientBrush(path);
            pathBrush.CenterColor = lightColor;
            pathBrush.SurroundColors = new Color[] { Color.FromArgb(0, lightColor) };
            g.FillEllipse(pathBrush, rectangle);

            // Draw the white reflection gradient
            var offset = Convert.ToInt32(diameter * .15F);
            var diameter1 = Convert.ToInt32(rectangle.Width * .8F);
            var whiteRect = new Rectangle(rectangle.X - offset, rectangle.Y - offset, diameter1, diameter1);
            var path1 = new GraphicsPath();
            path1.AddEllipse(whiteRect);
            var pathBrush1 = new PathGradientBrush(path);
            pathBrush1.CenterColor = _reflectionColor;
            pathBrush1.SurroundColors = _surroundColor;
            g.FillEllipse(pathBrush1, whiteRect);

            // Draw the border
            g.SetClip(this.ClientRectangle);
            if (this.On) g.DrawEllipse(new Pen(Color.FromArgb(85, Color.Black), 1F), rectangle);
        }

        /// <summary>
        /// Causes the Led to start blinking
        /// </summary>
        /// <param name="milliseconds">Number of milliseconds to blink for. 0 stops blinking</param>
        public void Blink(int milliseconds)
        {
            if (milliseconds > 0)
            {
                this.On = true;
                _timer.Interval = milliseconds;
                _timer.Enabled = true;
            }
            else
            {
                _timer.Enabled = false;
                this.On = false;
            }
        }



        #endregion

        
        #region PLC Properties


        public bool HoldTimeMet;
        private int m_MaximumHoldTime = 3000;
        private int m_MinimumHoldTime = 500;
        private OutputType m_OutputType = OutputType.MomentarySet;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressClick = string.Empty;


        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressText = string.Empty;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressValue = string.Empty;

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;

        //**********************************************************************
        //* If output type is set to write value, then write this value to PLC
        //**********************************************************************

        //*****************************************
        //* Property - Hold time before bit reset
        //*****************************************
        private readonly Timer MaxHoldTimer = new Timer();

        //*****************************************
        //* Property - Hold time before bit reset
        //*****************************************
        private readonly Timer MinHoldTimer = new Timer();
        private readonly bool MouseIsDown = false;


        //***************************************
        //* Call backs for returned data
        //***************************************
        private string OriginalText;


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
                            AdvancedScada.Controls_Binding.Licenses.LicenseManager.IsInDesignMode) return;
                        var bd = new Binding("Text", TagCollectionClient.Tags[m_PLCAddressText], "Value", true);
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
                            string.IsNullOrWhiteSpace(m_PLCAddressVisible) || AdvancedScada.Controls_Binding.Licenses.LicenseManager.IsInDesignMode) return;
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
                            AdvancedScada.Controls_Binding.Licenses.LicenseManager.IsInDesignMode) return;
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
        public string PLCAddressClick
        {
            get { return m_PLCAddressClick; }
            set
            {
                if (m_PLCAddressClick != value) m_PLCAddressClick = value;
            }
        }

        public OutputType OutputType
        {
            get { return m_OutputType; }
            set { m_OutputType = value; }
        }

        [DefaultValue(false)]
        public bool SuppressErrorDisplay { get; set; }

        [Category("PLC Properties")]
        public int MinimumHoldTime
        {
            get { return m_MinimumHoldTime; }
            set
            {
                m_MinimumHoldTime = value;
                if (value > 0) MinHoldTimer.Interval = value;
            }
        }

        [Category("PLC Properties")]
        public int MaximumHoldTime
        {
            get { return m_MaximumHoldTime; }
            set
            {
                m_MaximumHoldTime = value;
                if (value > 0) MaxHoldTimer.Interval = value;
            }
        }

        [Category("PLC Properties")]
        public int ValueToWrite { get; set; }
        public string PLCAddressEnabled { get; set; }
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
                DisplayError("WRITE FAILED!" + ex.Message);
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


            if (!string.IsNullOrWhiteSpace(m_PLCAddressClick) & Enabled && PLCAddressClick != null)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(m_PLCAddressClick, true);
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(m_PLCAddressClick, false);
                            break;
                        case OutputType.SetTrue:
                            Utilities.Write(m_PLCAddressClick, true);
                            break;
                        case OutputType.SetFalse:
                            Utilities.Write(m_PLCAddressClick, false);
                            break;
                        case OutputType.Toggle:

                            var CurrentValue = true ;
                            if (CurrentValue)
                                Utilities.Write(m_PLCAddressClick, false);
                            else
                                Utilities.Write(m_PLCAddressClick, true);
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
            if (!string.IsNullOrWhiteSpace(m_PLCAddressClick) & Enabled)
            {
                try
                {
                    switch (OutputType)
                    {
                        case OutputType.MomentarySet:
                            Utilities.Write(m_PLCAddressClick, false);
                            break;
                        case OutputType.MomentaryReset:
                            Utilities.Write(m_PLCAddressClick, true);
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
