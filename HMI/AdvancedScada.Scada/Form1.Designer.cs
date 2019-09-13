using MfgControl.AdvancedHMI.Controls;

namespace Scada
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.hmiLabel1 = new AdvancedScada.Controls.AHMI.Display.HMILabel();
            this.hmiSquareIlluminatedButton1 = new AdvancedScada.Controls.AHMI.ButtonAll.HMISquareIlluminatedButton();
            this.hmiSevenSegment1 = new AdvancedScada.Controls.AHMI.SevenSegment.HMISevenSegment();
            this.hmiLedDisplay2 = new AdvancedScada.Controls.HslControl.Segment.HMILedDisplay();
            this.hmiSegment7LED1 = new AdvancedScada.Controls.AHMI.Segment.HMISegment7LED();
            this.hmiVacuumPump1 = new AdvancedScada.Controls.HslControl.Pipe.HMIVacuumPump();
            this.hmiPumpOne1 = new AdvancedScada.Controls.HslControl.Motor.HMIPumpOne();
            this.hmiLedDisplay1 = new AdvancedScada.Controls.HslControl.Segment.HMILedDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hmiSevenSegment1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AdvancedScada.HMI.Properties.Resources.DigitalPanelMeterR1;
            this.pictureBox1.Location = new System.Drawing.Point(433, 122);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(323, 145);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // hmiLabel1
            // 
            this.hmiLabel1.BackColor = System.Drawing.Color.Transparent;
            this.hmiLabel1.BooleanDisplay = AdvancedScada.Controls.AHMI.Display.HMILabel.BooleanDisplayOption.TrueFalse;
            this.hmiLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hmiLabel1.DisplayAsTime = false;
            this.hmiLabel1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hmiLabel1.ForeColor = System.Drawing.Color.Black;
            this.hmiLabel1.Highlight = false;
            this.hmiLabel1.HighlightColor = System.Drawing.Color.Red;
            this.hmiLabel1.HighlightForeColor = System.Drawing.Color.White;
            this.hmiLabel1.HighlightKeyCharacter = "!";
            this.hmiLabel1.InterpretValueAsBCD = false;
            this.hmiLabel1.KeypadAlphanumeric = false;
            this.hmiLabel1.KeypadAlphaNumeric = false;
            this.hmiLabel1.KeypadFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hmiLabel1.KeypadFontColor = System.Drawing.Color.WhiteSmoke;
            this.hmiLabel1.KeypadMaxValue = 0D;
            this.hmiLabel1.KeypadMinValue = 0D;
            this.hmiLabel1.KeypadScaleFactor = 1D;
            this.hmiLabel1.KeypadShowCurrentValue = false;
            this.hmiLabel1.KeypadText = null;
            this.hmiLabel1.KeypadWidth = 300;
            this.hmiLabel1.Location = new System.Drawing.Point(441, 360);
            this.hmiLabel1.Name = "hmiLabel1";
            this.hmiLabel1.NumericFormat = null;
            this.hmiLabel1.PLCAddressKeypad = "Ch2.PLC1.DataBlock1.TAG00001";
            this.hmiLabel1.PLCAddressValue = "Ch2.PLC1.DataBlock1.TAG00001";
            this.hmiLabel1.PollRate = 0;
            this.hmiLabel1.Size = new System.Drawing.Size(203, 55);
            this.hmiLabel1.TabIndex = 10;
            this.hmiLabel1.Text = "BasicLabel";
            this.hmiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.hmiLabel1.Value = "BasicLabel";
            this.hmiLabel1.ValueLeftPadCharacter = ' ';
            this.hmiLabel1.ValueLeftPadLength = 0;
            this.hmiLabel1.ValuePrefix = null;
            this.hmiLabel1.ValueScaleFactor = 1D;
            this.hmiLabel1.ValueSuffix = null;
            this.hmiLabel1.ValueToSubtractFrom = 0F;
            // 
            // hmiSquareIlluminatedButton1
            // 
            this.hmiSquareIlluminatedButton1.LightColor = MfgControl.AdvancedHMI.Controls.SquareIlluminatedButton.LightColors.Green;
            this.hmiSquareIlluminatedButton1.Location = new System.Drawing.Point(433, 273);
            this.hmiSquareIlluminatedButton1.Name = "hmiSquareIlluminatedButton1";
            this.hmiSquareIlluminatedButton1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.MomentarySet;
            this.hmiSquareIlluminatedButton1.PLCAddressClick = "";
            this.hmiSquareIlluminatedButton1.PLCAddressText = "";
            this.hmiSquareIlluminatedButton1.PLCAddressValue = "";
            this.hmiSquareIlluminatedButton1.PLCAddressVisible = "";
            this.hmiSquareIlluminatedButton1.Size = new System.Drawing.Size(212, 69);
            this.hmiSquareIlluminatedButton1.TabIndex = 9;
            this.hmiSquareIlluminatedButton1.Text = "hmiSquareIlluminatedButton1";
            this.hmiSquareIlluminatedButton1.Value = false;
            // 
            // hmiSevenSegment1
            // 
            this.hmiSevenSegment1.BackColor = System.Drawing.Color.Transparent;
            this.hmiSevenSegment1.BackColour_1 = System.Drawing.Color.Black;
            this.hmiSevenSegment1.BackColour_2 = System.Drawing.Color.DimGray;
            this.hmiSevenSegment1.BevelRate = 0.5F;
            this.hmiSevenSegment1.FadedColour = System.Drawing.Color.DimGray;
            this.hmiSevenSegment1.ForeColor = System.Drawing.Color.Yellow;
            this.hmiSevenSegment1.HighlightOpaque = ((byte)(50));
            this.hmiSevenSegment1.KeypadFontColor = System.Drawing.Color.WhiteSmoke;
            this.hmiSevenSegment1.KeypadMaxValue = 0D;
            this.hmiSevenSegment1.KeypadMinValue = 0D;
            this.hmiSevenSegment1.KeypadScaleFactor = 1D;
            this.hmiSevenSegment1.KeypadText = null;
            this.hmiSevenSegment1.KeypadWidth = 300;
            this.hmiSevenSegment1.Location = new System.Drawing.Point(465, 156);
            this.hmiSevenSegment1.Name = "hmiSevenSegment1";
            this.hmiSevenSegment1.PLCAddressForecolorHighLimitValue = null;
            this.hmiSevenSegment1.PLCAddressForecolorLowLimitValue = null;
            this.hmiSevenSegment1.PLCAddressKeypad = "Ch2.PLC1.DataBlock1.TAG0000118";
            this.hmiSevenSegment1.PLCAddressText = "Ch2.PLC1.DataBlock1.TAG00001";
            this.hmiSevenSegment1.PLCAddressValue = null;
            this.hmiSevenSegment1.PLCAddressVisible = "";
            this.hmiSevenSegment1.Size = new System.Drawing.Size(258, 84);
            this.hmiSevenSegment1.TabIndex = 8;
            this.hmiSevenSegment1.Text = "0";
            this.hmiSevenSegment1.TextAlignment = AdvancedScada.Controls.AHMI.SevenSegment.HMISevenSegment.Alignment.Right;
            this.hmiSevenSegment1.TotalCharCount = 6;
            // 
            // hmiLedDisplay2
            // 
            this.hmiLedDisplay2.BackColor = System.Drawing.Color.Transparent;
            this.hmiLedDisplay2.DisplayBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.hmiLedDisplay2.DisplayNumber = 8;
            this.hmiLedDisplay2.DisplayText = "66";
            this.hmiLedDisplay2.ForeColor = System.Drawing.Color.Red;
            this.hmiLedDisplay2.KeypadFontColor = System.Drawing.Color.WhiteSmoke;
            this.hmiLedDisplay2.KeypadMaxValue = 0D;
            this.hmiLedDisplay2.KeypadMinValue = 0D;
            this.hmiLedDisplay2.KeypadScaleFactor = 1D;
            this.hmiLedDisplay2.KeypadText = null;
            this.hmiLedDisplay2.KeypadWidth = 300;
            this.hmiLedDisplay2.LedNumberSize = 7;
            this.hmiLedDisplay2.Location = new System.Drawing.Point(403, 8);
            this.hmiLedDisplay2.Margin = new System.Windows.Forms.Padding(3, 11, 3, 11);
            this.hmiLedDisplay2.Name = "hmiLedDisplay2";
            this.hmiLedDisplay2.PLCAddressKeypad = "Ch2.PLC1.DataBlock1.TAG00001";
            this.hmiLedDisplay2.PLCAddressText = "";
            this.hmiLedDisplay2.PLCAddressValue = "Ch2.PLC1.DataBlock1.TAG00001";
            this.hmiLedDisplay2.PLCAddressVisible = "";
            this.hmiLedDisplay2.Size = new System.Drawing.Size(385, 100);
            this.hmiLedDisplay2.TabIndex = 5;
            this.hmiLedDisplay2.Value = "66";
            // 
            // hmiSegment7LED1
            // 
            this.hmiSegment7LED1.AllZeroVisible = false;
            this.hmiSegment7LED1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.hmiSegment7LED1.BkGradient = false;
            this.hmiSegment7LED1.BkGradientAngle = 90;
            this.hmiSegment7LED1.BkGradientColor = System.Drawing.Color.White;
            this.hmiSegment7LED1.BkGradientRate = 0.5F;
            this.hmiSegment7LED1.BkGradientType = AdvancedScada.Controls.Enum.DAS_BkGradientStyle.BKGS_Linear;
            this.hmiSegment7LED1.BkShinePosition = 1F;
            this.hmiSegment7LED1.BkTransparency = 0F;
            this.hmiSegment7LED1.BorderExteriorColor = System.Drawing.Color.Blue;
            this.hmiSegment7LED1.BorderExteriorLength = 0;
            this.hmiSegment7LED1.BorderGradientAngle = 225;
            this.hmiSegment7LED1.BorderGradientLightPos1 = 1F;
            this.hmiSegment7LED1.BorderGradientLightPos2 = -1F;
            this.hmiSegment7LED1.BorderGradientRate = 0.5F;
            this.hmiSegment7LED1.BorderGradientType = AdvancedScada.Controls.Enum.DAS_BorderGradientStyle.BGS_Flat;
            this.hmiSegment7LED1.BorderLightIntermediateBrightness = 0F;
            this.hmiSegment7LED1.BorderShape = AdvancedScada.Controls.Enum.DAS_BorderStyle.BS_Rect;
            this.hmiSegment7LED1.ControlShadow = false;
            this.hmiSegment7LED1.DigitGap = 10;
            this.hmiSegment7LED1.DigitInclined = false;
            this.hmiSegment7LED1.DigitNumber = 8;
            this.hmiSegment7LED1.DigitSize = new System.Drawing.Size(20, 40);
            this.hmiSegment7LED1.DisplayType = AdvancedScada.Controls.Enum.DAS_DisplayStyle.DS_Dec_Style;
            this.hmiSegment7LED1.Flashing = false;
            this.hmiSegment7LED1.FlashingDuration = 1000;
            this.hmiSegment7LED1.ForeColor = System.Drawing.Color.Lime;
            this.hmiSegment7LED1.FrameTransparency = 0.5F;
            this.hmiSegment7LED1.InnerBorderDarkColor = System.Drawing.Color.DarkGray;
            this.hmiSegment7LED1.InnerBorderLength = 5;
            this.hmiSegment7LED1.InnerBorderLightColor = System.Drawing.Color.Lavender;
            this.hmiSegment7LED1.KeypadAlphaNumeric = false;
            this.hmiSegment7LED1.KeypadFont = new System.Drawing.Font("Arial", 10F);
            this.hmiSegment7LED1.KeypadFontColor = System.Drawing.Color.WhiteSmoke;
            this.hmiSegment7LED1.KeypadMaxValue = 0D;
            this.hmiSegment7LED1.KeypadMinValue = 0D;
            this.hmiSegment7LED1.KeypadScaleFactor = 1D;
            this.hmiSegment7LED1.KeypadShowCurrentValue = false;
            this.hmiSegment7LED1.KeypadText = null;
            this.hmiSegment7LED1.KeypadWidth = 400;
            this.hmiSegment7LED1.LEDDateTime = new System.DateTime(2019, 9, 11, 6, 20, 11, 704);
            this.hmiSegment7LED1.Location = new System.Drawing.Point(23, 122);
            this.hmiSegment7LED1.MiddleBorderColor = System.Drawing.Color.Gray;
            this.hmiSegment7LED1.MiddleBorderLength = 0;
            this.hmiSegment7LED1.Name = "hmiSegment7LED1";
            this.hmiSegment7LED1.OuterBorderDarkColor = System.Drawing.Color.DarkGray;
            this.hmiSegment7LED1.OuterBorderLength = 5;
            this.hmiSegment7LED1.OuterBorderLightColor = System.Drawing.Color.Lavender;
            this.hmiSegment7LED1.PLCAddressKeypad = "";
            this.hmiSegment7LED1.PLCAddressText = "";
            this.hmiSegment7LED1.PLCAddressValue = "Ch2.PLC1.DataBlock1.TAG00003";
            this.hmiSegment7LED1.PLCAddressVisible = "";
            this.hmiSegment7LED1.Precision = 0;
            this.hmiSegment7LED1.RoundRadius = 10;
            this.hmiSegment7LED1.SegmentBorderColor = System.Drawing.Color.Black;
            this.hmiSegment7LED1.SegmentFrameColor = System.Drawing.Color.DimGray;
            this.hmiSegment7LED1.SegmentFrameVisible = true;
            this.hmiSegment7LED1.SegmentType = AdvancedScada.Controls.Enum.DAS_SegmentStyle.SS_Ladder;
            this.hmiSegment7LED1.SegmentWidth = 4;
            this.hmiSegment7LED1.ShadowColor = System.Drawing.Color.DimGray;
            this.hmiSegment7LED1.ShadowDepth = 8;
            this.hmiSegment7LED1.ShadowRate = 0.5F;
            this.hmiSegment7LED1.SignVisible = true;
            this.hmiSegment7LED1.Size = new System.Drawing.Size(374, 103);
            this.hmiSegment7LED1.TabIndex = 4;
            this.hmiSegment7LED1.Text = "hmiSegment7LED1";
            this.hmiSegment7LED1.UnitVisible = false;
            this.hmiSegment7LED1.Value = 888D;
            // 
            // hmiVacuumPump1
            // 
            this.hmiVacuumPump1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(151)))), ((int)(((byte)(0)))));
            this.hmiVacuumPump1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.hmiVacuumPump1.Color3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.hmiVacuumPump1.Color4 = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.hmiVacuumPump1.Location = new System.Drawing.Point(170, 257);
            this.hmiVacuumPump1.Name = "hmiVacuumPump1";
            this.hmiVacuumPump1.Size = new System.Drawing.Size(102, 148);
            this.hmiVacuumPump1.TabIndex = 3;
            this.hmiVacuumPump1.Text = "hmiVacuumPump1";
            // 
            // hmiPumpOne1
            // 
            this.hmiPumpOne1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(218)))), ((int)(((byte)(227)))));
            this.hmiPumpOne1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(164)))), ((int)(((byte)(173)))));
            this.hmiPumpOne1.Color3 = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.hmiPumpOne1.Color4 = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(214)))));
            this.hmiPumpOne1.Color5 = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(213)))), ((int)(((byte)(220)))));
            this.hmiPumpOne1.Color6 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(160)))), ((int)(((byte)(169)))));
            this.hmiPumpOne1.Color7 = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(100)))), ((int)(((byte)(111)))));
            this.hmiPumpOne1.Location = new System.Drawing.Point(12, 300);
            this.hmiPumpOne1.MaximumHoldTime = 3000;
            this.hmiPumpOne1.MinimumHoldTime = 500;
            this.hmiPumpOne1.Name = "hmiPumpOne1";
            this.hmiPumpOne1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.Toggle;
            this.hmiPumpOne1.PLCAddressClick = "Ch1.PLC1.DataBlock1.TAG00001";
            this.hmiPumpOne1.PLCAddressText = "";
            this.hmiPumpOne1.PLCAddressValue = "Ch1.PLC1.DataBlock1.TAG00001";
            this.hmiPumpOne1.PLCAddressVisible = "";
            this.hmiPumpOne1.Size = new System.Drawing.Size(152, 134);
            this.hmiPumpOne1.TabIndex = 2;
            this.hmiPumpOne1.Text = "hmiPumpOne1";
            this.hmiPumpOne1.Value = false;
            this.hmiPumpOne1.ValueToWrite = 0;
            // 
            // hmiLedDisplay1
            // 
            this.hmiLedDisplay1.BackColor = System.Drawing.Color.Transparent;
            this.hmiLedDisplay1.DisplayBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.hmiLedDisplay1.DisplayNumber = 8;
            this.hmiLedDisplay1.DisplayText = "66";
            this.hmiLedDisplay1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.hmiLedDisplay1.KeypadFontColor = System.Drawing.Color.WhiteSmoke;
            this.hmiLedDisplay1.KeypadMaxValue = 0D;
            this.hmiLedDisplay1.KeypadMinValue = 0D;
            this.hmiLedDisplay1.KeypadScaleFactor = 1D;
            this.hmiLedDisplay1.KeypadText = null;
            this.hmiLedDisplay1.KeypadWidth = 300;
            this.hmiLedDisplay1.LedNumberSize = 7;
            this.hmiLedDisplay1.Location = new System.Drawing.Point(12, 8);
            this.hmiLedDisplay1.Margin = new System.Windows.Forms.Padding(3, 11, 3, 11);
            this.hmiLedDisplay1.Name = "hmiLedDisplay1";
            this.hmiLedDisplay1.PLCAddressKeypad = "Ch2.PLC1.DataBlock1.TAG00001";
            this.hmiLedDisplay1.PLCAddressText = "";
            this.hmiLedDisplay1.PLCAddressValue = "Ch2.PLC1.DataBlock1.TAG00001";
            this.hmiLedDisplay1.PLCAddressVisible = "";
            this.hmiLedDisplay1.Size = new System.Drawing.Size(385, 100);
            this.hmiLedDisplay1.TabIndex = 1;
            this.hmiLedDisplay1.Value = "66";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 559);
            this.Controls.Add(this.hmiLabel1);
            this.Controls.Add(this.hmiSquareIlluminatedButton1);
            this.Controls.Add(this.hmiSevenSegment1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.hmiLedDisplay2);
            this.Controls.Add(this.hmiSegment7LED1);
            this.Controls.Add(this.hmiVacuumPump1);
            this.Controls.Add(this.hmiPumpOne1);
            this.Controls.Add(this.hmiLedDisplay1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hmiSevenSegment1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private AdvancedScada.Controls.HslControl.Segment.HMILedDisplay hmiLedDisplay1;
        private AdvancedScada.Controls.HslControl.Motor.HMIPumpOne hmiPumpOne1;
        private AdvancedScada.Controls.HslControl.Pipe.HMIVacuumPump hmiVacuumPump1;
        private AdvancedScada.Controls.AHMI.Segment.HMISegment7LED hmiSegment7LED1;
        private AdvancedScada.Controls.HslControl.Segment.HMILedDisplay hmiLedDisplay2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AdvancedScada.Controls.AHMI.SevenSegment.HMISevenSegment hmiSevenSegment1;
        private AdvancedScada.Controls.AHMI.ButtonAll.HMISquareIlluminatedButton hmiSquareIlluminatedButton1;
        private AdvancedScada.Controls.AHMI.Display.HMILabel hmiLabel1;
    }
}

