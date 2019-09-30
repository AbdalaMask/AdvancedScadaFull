namespace AdvancedScada.HMI
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
            this.hmiSquareIlluminatedButton1 = new AdvancedScada.Controls_Binding.ButtonAll.HMISquareIlluminatedButton();
            this.hmiAnnunciator1 = new AdvancedScada.Controls_Binding.ButtonAll.HMIAnnunciator();
            this.hmiCheckBox1 = new AdvancedScada.Controls_Binding.ButtonAll.HMICheckBox();
            this.hmiVacuumPump1 = new AdvancedScada.Controls_Binding.HslControl.Pipe.HMIVacuumPump();
            this.hmiLedSingle1 = new AdvancedScada.Controls_Binding.Leds.HMILedSingle();
            this.hmiProcessIndicator1 = new AdvancedScada.Controls_Binding.ProcessAll.HMIProcessIndicator();
            this.hmiSegment7LED1 = new AdvancedScada.Controls_Binding.Segment.HMISegment7LED();
            this.hmiSevenSegment21 = new AdvancedScada.Controls_Binding.SevenSegment.HMISevenSegment2();
            this.digitalDisplayControl1 = new AdvancedScada.Controls_Net45.DigitalDisplayControl();
            this.lxLedControl1 = new AdvancedScada.Controls_Binding.SevenSegment.LxLedControl();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // hmiSquareIlluminatedButton1
            // 
            this.hmiSquareIlluminatedButton1.LightColor = MfgControl.AdvancedHMI.Controls.SquareIlluminatedButton.LightColors.Green;
            this.hmiSquareIlluminatedButton1.Location = new System.Drawing.Point(211, 12);
            this.hmiSquareIlluminatedButton1.MaximumHoldTime = 3000;
            this.hmiSquareIlluminatedButton1.MinimumHoldTime = 500;
            this.hmiSquareIlluminatedButton1.Name = "hmiSquareIlluminatedButton1";
            this.hmiSquareIlluminatedButton1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.MomentarySet;
            this.hmiSquareIlluminatedButton1.PLCAddressClick = "";
            this.hmiSquareIlluminatedButton1.PLCAddressEnabled = null;
            this.hmiSquareIlluminatedButton1.PLCAddressText = "";
            this.hmiSquareIlluminatedButton1.PLCAddressValue = "";
            this.hmiSquareIlluminatedButton1.PLCAddressVisible = "";
            this.hmiSquareIlluminatedButton1.Size = new System.Drawing.Size(182, 50);
            this.hmiSquareIlluminatedButton1.TabIndex = 1;
            this.hmiSquareIlluminatedButton1.Text = "hmiSquareIlluminatedButton1";
            this.hmiSquareIlluminatedButton1.Value = false;
            this.hmiSquareIlluminatedButton1.ValueToWrite = 0;
            // 
            // hmiAnnunciator1
            // 
            this.hmiAnnunciator1.Location = new System.Drawing.Point(12, 12);
            this.hmiAnnunciator1.MaximumHoldTime = 3000;
            this.hmiAnnunciator1.MinimumHoldTime = 500;
            this.hmiAnnunciator1.Name = "hmiAnnunciator1";
            this.hmiAnnunciator1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.MomentarySet;
            this.hmiAnnunciator1.PLCAddressClick = "";
            this.hmiAnnunciator1.PLCAddressEnabled = "";
            this.hmiAnnunciator1.PLCAddressHighlightX = "";
            this.hmiAnnunciator1.PLCAddressSelectTextAlternate = "";
            this.hmiAnnunciator1.PLCAddressValue = "";
            this.hmiAnnunciator1.PLCAddressVisible = "";
            this.hmiAnnunciator1.Size = new System.Drawing.Size(190, 50);
            this.hmiAnnunciator1.TabIndex = 0;
            this.hmiAnnunciator1.Text = "hmiAnnunciator1";
            this.hmiAnnunciator1.Value = false;
            this.hmiAnnunciator1.ValueToWrite = 0;
            // 
            // hmiCheckBox1
            // 
            this.hmiCheckBox1.AutoSize = true;
            this.hmiCheckBox1.Location = new System.Drawing.Point(36, 160);
            this.hmiCheckBox1.Name = "hmiCheckBox1";
            this.hmiCheckBox1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.MomentarySet;
            this.hmiCheckBox1.PLCAddressCheckChanged = "";
            this.hmiCheckBox1.PLCAddressChecked = "";
            this.hmiCheckBox1.PLCAddressClick = null;
            this.hmiCheckBox1.PLCAddressEnabled = null;
            this.hmiCheckBox1.PLCAddressText = "";
            this.hmiCheckBox1.PLCAddressValue = null;
            this.hmiCheckBox1.PLCAddressVisible = "";
            this.hmiCheckBox1.Size = new System.Drawing.Size(95, 17);
            this.hmiCheckBox1.TabIndex = 2;
            this.hmiCheckBox1.Text = "hmiCheckBox1";
            this.hmiCheckBox1.UseVisualStyleBackColor = true;
            // 
            // hmiVacuumPump1
            // 
            this.hmiVacuumPump1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(151)))), ((int)(((byte)(0)))));
            this.hmiVacuumPump1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.hmiVacuumPump1.Color3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.hmiVacuumPump1.Color4 = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.hmiVacuumPump1.Location = new System.Drawing.Point(12, 183);
            this.hmiVacuumPump1.MaximumHoldTime = 3000;
            this.hmiVacuumPump1.MinimumHoldTime = 500;
            this.hmiVacuumPump1.MoveSpeed = 0F;
            this.hmiVacuumPump1.Name = "hmiVacuumPump1";
            this.hmiVacuumPump1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.MomentarySet;
            this.hmiVacuumPump1.PLCAddressClick = "";
            this.hmiVacuumPump1.PLCAddressEnabled = null;
            this.hmiVacuumPump1.PLCAddressText = "";
            this.hmiVacuumPump1.PLCAddressValue = "";
            this.hmiVacuumPump1.PLCAddressVisible = "";
            this.hmiVacuumPump1.Size = new System.Drawing.Size(119, 167);
            this.hmiVacuumPump1.TabIndex = 3;
            this.hmiVacuumPump1.Text = "hmiVacuumPump1";
            this.hmiVacuumPump1.Value = false;
            this.hmiVacuumPump1.ValueToWrite = 0;
            // 
            // hmiLedSingle1
            // 
            this.hmiLedSingle1.ArrowWidth = 20;
            this.hmiLedSingle1.BorderExteriorColor = System.Drawing.Color.Blue;
            this.hmiLedSingle1.BorderExteriorLength = 0;
            this.hmiLedSingle1.BorderGradientAngle = 225;
            this.hmiLedSingle1.BorderGradientLightPos1 = 1F;
            this.hmiLedSingle1.BorderGradientLightPos2 = -1F;
            this.hmiLedSingle1.BorderGradientRate = 0.5F;
            this.hmiLedSingle1.BorderGradientType = DasNetIndicator.DAS_BorderGradientStyle.BGS_Path;
            this.hmiLedSingle1.BorderLightIntermediateBrightness = 0F;
            this.hmiLedSingle1.GradientAngle = 225;
            this.hmiLedSingle1.GradientRate = 0.6F;
            this.hmiLedSingle1.GradientType = DasNetIndicator.DAS_BkGradientStyle.BKGS_Shine;
            this.hmiLedSingle1.IndicatorAutoSize = true;
            this.hmiLedSingle1.IndicatorHeight = 50;
            this.hmiLedSingle1.IndicatorWidth = 50;
            this.hmiLedSingle1.InnerBorderDarkColor = System.Drawing.Color.DimGray;
            this.hmiLedSingle1.InnerBorderLength = 5;
            this.hmiLedSingle1.InnerBorderLightColor = System.Drawing.Color.White;
            this.hmiLedSingle1.Location = new System.Drawing.Point(513, 172);
            this.hmiLedSingle1.MaximumHoldTime = 3000;
            this.hmiLedSingle1.MiddleBorderColor = System.Drawing.Color.Gray;
            this.hmiLedSingle1.MiddleBorderLength = 0;
            this.hmiLedSingle1.MinimumHoldTime = 500;
            this.hmiLedSingle1.Name = "hmiLedSingle1";
            this.hmiLedSingle1.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.hmiLedSingle1.OffGradientColor = System.Drawing.Color.Gray;
            this.hmiLedSingle1.OnColor = System.Drawing.Color.Lime;
            this.hmiLedSingle1.OnGradientColor = System.Drawing.Color.White;
            this.hmiLedSingle1.OuterBorderDarkColor = System.Drawing.Color.DimGray;
            this.hmiLedSingle1.OuterBorderLength = 5;
            this.hmiLedSingle1.OuterBorderLightColor = System.Drawing.Color.White;
            this.hmiLedSingle1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.MomentarySet;
            this.hmiLedSingle1.PLCAddressClick = "";
            this.hmiLedSingle1.PLCAddressEnabled = null;
            this.hmiLedSingle1.PLCAddressText = "";
            this.hmiLedSingle1.PLCAddressValue = "";
            this.hmiLedSingle1.PLCAddressVisible = "";
            this.hmiLedSingle1.RoundRadius = 30;
            this.hmiLedSingle1.Shape = DasNetIndicator.DAS_ShapeStyle.SS_RoundRect;
            this.hmiLedSingle1.ShinePosition = 0.5F;
            this.hmiLedSingle1.Size = new System.Drawing.Size(59, 52);
            this.hmiLedSingle1.TabIndex = 4;
            this.hmiLedSingle1.Text = "hmiLedSingle1";
            this.hmiLedSingle1.TextPosition = DasNetIndicator.DAS_TextPosStyle.TPS_Left;
            this.hmiLedSingle1.TextVisible = false;
            this.hmiLedSingle1.Value = true;
            this.hmiLedSingle1.ValueToWrite = 0;
            // 
            // hmiProcessIndicator1
            // 
            this.hmiProcessIndicator1.BkGradient = false;
            this.hmiProcessIndicator1.BkGradientAngle = 90;
            this.hmiProcessIndicator1.BkGradientColor = System.Drawing.Color.White;
            this.hmiProcessIndicator1.BkGradientRate = 0.5F;
            this.hmiProcessIndicator1.BkGradientType = DasNetIndicator.DAS_BkGradientStyle.BKGS_Linear;
            this.hmiProcessIndicator1.BkShinePosition = 1F;
            this.hmiProcessIndicator1.BkTransparency = 0F;
            this.hmiProcessIndicator1.BorderExteriorColor = System.Drawing.Color.Blue;
            this.hmiProcessIndicator1.BorderExteriorLength = 0;
            this.hmiProcessIndicator1.BorderGradientAngle = 225;
            this.hmiProcessIndicator1.BorderGradientLightPos1 = 1F;
            this.hmiProcessIndicator1.BorderGradientLightPos2 = -1F;
            this.hmiProcessIndicator1.BorderGradientRate = 0.5F;
            this.hmiProcessIndicator1.BorderGradientType = DasNetIndicator.DAS_BorderGradientStyle.BGS_Ring;
            this.hmiProcessIndicator1.BorderLightIntermediateBrightness = 0F;
            this.hmiProcessIndicator1.BorderShape = DasNetIndicator.DAS_BorderStyle.BS_RoundRect;
            this.hmiProcessIndicator1.ControlShadow = false;
            this.hmiProcessIndicator1.Gradient = false;
            this.hmiProcessIndicator1.IndicatorBorderColor = System.Drawing.Color.DimGray;
            this.hmiProcessIndicator1.IndicatorColor = System.Drawing.Color.YellowGreen;
            this.hmiProcessIndicator1.IndicatorColor2 = System.Drawing.Color.DarkGreen;
            this.hmiProcessIndicator1.IndicatorColor3 = System.Drawing.Color.Red;
            this.hmiProcessIndicator1.IndicatorGap = 30;
            this.hmiProcessIndicator1.IndicatorGradientAngle = 90;
            this.hmiProcessIndicator1.IndicatorGradientColor = System.Drawing.Color.White;
            this.hmiProcessIndicator1.IndicatorGradientRate = 0.5F;
            this.hmiProcessIndicator1.IndicatorGradientType = DasNetIndicator.DAS_BkGradientStyle.BKGS_Linear;
            this.hmiProcessIndicator1.IndicatorImage = null;
            this.hmiProcessIndicator1.IndicatorShinePosition = 1F;
            this.hmiProcessIndicator1.IndicatorSize = 20;
            this.hmiProcessIndicator1.IndicatorString = "...";
            this.hmiProcessIndicator1.IndicatorType = DasNetIndicator.DAS_ProcessIndicatorType.PIT_Arrow;
            this.hmiProcessIndicator1.InnerBorderDarkColor = System.Drawing.Color.DimGray;
            this.hmiProcessIndicator1.InnerBorderLength = 2;
            this.hmiProcessIndicator1.InnerBorderLightColor = System.Drawing.Color.White;
            this.hmiProcessIndicator1.InverseDirection = false;
            this.hmiProcessIndicator1.KeypadFontColor = System.Drawing.Color.WhiteSmoke;
            this.hmiProcessIndicator1.KeypadMaxValue = 0D;
            this.hmiProcessIndicator1.KeypadMinValue = 0D;
            this.hmiProcessIndicator1.KeypadScaleFactor = 1D;
            this.hmiProcessIndicator1.KeypadText = null;
            this.hmiProcessIndicator1.KeypadWidth = 300;
            this.hmiProcessIndicator1.Location = new System.Drawing.Point(428, 12);
            this.hmiProcessIndicator1.MiddleBorderColor = System.Drawing.Color.Gray;
            this.hmiProcessIndicator1.MiddleBorderLength = 0;
            this.hmiProcessIndicator1.Name = "hmiProcessIndicator1";
            this.hmiProcessIndicator1.OuterBorderDarkColor = System.Drawing.Color.DimGray;
            this.hmiProcessIndicator1.OuterBorderLength = 3;
            this.hmiProcessIndicator1.OuterBorderLightColor = System.Drawing.Color.White;
            this.hmiProcessIndicator1.PLCAddressClick = null;
            this.hmiProcessIndicator1.PLCAddressEnabled = null;
            this.hmiProcessIndicator1.PLCAddressKeypad = "";
            this.hmiProcessIndicator1.PLCAddressText = "";
            this.hmiProcessIndicator1.PLCAddressValue = "";
            this.hmiProcessIndicator1.PLCAddressVisible = "";
            this.hmiProcessIndicator1.RoundRadius = 30;
            this.hmiProcessIndicator1.ShadowColor = System.Drawing.Color.DimGray;
            this.hmiProcessIndicator1.ShadowDepth = 8;
            this.hmiProcessIndicator1.ShadowRate = 0.5F;
            this.hmiProcessIndicator1.Size = new System.Drawing.Size(239, 54);
            this.hmiProcessIndicator1.TabIndex = 5;
            this.hmiProcessIndicator1.Text = "hmiProcessIndicator1";
            this.hmiProcessIndicator1.UpdateDuration = 500;
            this.hmiProcessIndicator1.UpdatePercentage = 0.1F;
            this.hmiProcessIndicator1.Value = false;
            this.hmiProcessIndicator1.Vertical = false;
            // 
            // hmiSegment7LED1
            // 
            this.hmiSegment7LED1.AllZeroVisible = false;
            this.hmiSegment7LED1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.hmiSegment7LED1.BkGradient = false;
            this.hmiSegment7LED1.BkGradientAngle = 90;
            this.hmiSegment7LED1.BkGradientColor = System.Drawing.Color.White;
            this.hmiSegment7LED1.BkGradientRate = 0.5F;
            this.hmiSegment7LED1.BkGradientType = DasNetLED.DAS_BkGradientStyle.BKGS_Linear;
            this.hmiSegment7LED1.BkShinePosition = 1F;
            this.hmiSegment7LED1.BkTransparency = 0F;
            this.hmiSegment7LED1.BorderExteriorColor = System.Drawing.Color.Blue;
            this.hmiSegment7LED1.BorderExteriorLength = 0;
            this.hmiSegment7LED1.BorderGradientAngle = 225;
            this.hmiSegment7LED1.BorderGradientLightPos1 = 1F;
            this.hmiSegment7LED1.BorderGradientLightPos2 = -1F;
            this.hmiSegment7LED1.BorderGradientRate = 0.5F;
            this.hmiSegment7LED1.BorderGradientType = DasNetLED.DAS_BorderGradientStyle.BGS_Flat;
            this.hmiSegment7LED1.BorderLightIntermediateBrightness = 0F;
            this.hmiSegment7LED1.BorderShape = DasNetLED.DAS_BorderStyle.BS_Rect;
            this.hmiSegment7LED1.ControlShadow = false;
            this.hmiSegment7LED1.DigitGap = 10;
            this.hmiSegment7LED1.DigitInclined = false;
            this.hmiSegment7LED1.DigitNumber = 8;
            this.hmiSegment7LED1.DigitSize = new System.Drawing.Size(20, 40);
            this.hmiSegment7LED1.DisplayType = DasNetLED.DAS_DisplayStyle.DS_Dec_Style;
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
            this.hmiSegment7LED1.LEDDateTime = new System.DateTime(2019, 9, 30, 21, 16, 24, 180);
            this.hmiSegment7LED1.Location = new System.Drawing.Point(194, 160);
            this.hmiSegment7LED1.MiddleBorderColor = System.Drawing.Color.Gray;
            this.hmiSegment7LED1.MiddleBorderLength = 0;
            this.hmiSegment7LED1.Name = "hmiSegment7LED1";
            this.hmiSegment7LED1.OuterBorderDarkColor = System.Drawing.Color.DarkGray;
            this.hmiSegment7LED1.OuterBorderLength = 5;
            this.hmiSegment7LED1.OuterBorderLightColor = System.Drawing.Color.Lavender;
            this.hmiSegment7LED1.PLCAddressKeypad = "";
            this.hmiSegment7LED1.PLCAddressText = "";
            this.hmiSegment7LED1.PLCAddressValue = "";
            this.hmiSegment7LED1.PLCAddressVisible = "";
            this.hmiSegment7LED1.Precision = 0;
            this.hmiSegment7LED1.RoundRadius = 10;
            this.hmiSegment7LED1.SegmentBorderColor = System.Drawing.Color.Black;
            this.hmiSegment7LED1.SegmentFrameColor = System.Drawing.Color.DimGray;
            this.hmiSegment7LED1.SegmentFrameVisible = true;
            this.hmiSegment7LED1.SegmentType = DasNetLED.DAS_SegmentStyle.SS_Ladder;
            this.hmiSegment7LED1.SegmentWidth = 4;
            this.hmiSegment7LED1.ShadowColor = System.Drawing.Color.DimGray;
            this.hmiSegment7LED1.ShadowDepth = 8;
            this.hmiSegment7LED1.ShadowRate = 0.5F;
            this.hmiSegment7LED1.SignVisible = true;
            this.hmiSegment7LED1.Size = new System.Drawing.Size(313, 90);
            this.hmiSegment7LED1.TabIndex = 6;
            this.hmiSegment7LED1.Text = "hmiSegment7LED1";
            this.hmiSegment7LED1.UnitVisible = false;
            this.hmiSegment7LED1.Value = 888D;
            // 
            // hmiSevenSegment21
            // 
            this.hmiSevenSegment21.DecimalPosition = 0;
            this.hmiSevenSegment21.ForeColor = System.Drawing.Color.LightGray;
            this.hmiSevenSegment21.KeypadFontColor = System.Drawing.Color.WhiteSmoke;
            this.hmiSevenSegment21.KeypadMaxValue = 0D;
            this.hmiSevenSegment21.KeypadMinValue = 0D;
            this.hmiSevenSegment21.KeypadScaleFactor = 1D;
            this.hmiSevenSegment21.KeypadText = null;
            this.hmiSevenSegment21.KeypadWidth = 300;
            this.hmiSevenSegment21.Location = new System.Drawing.Point(450, 72);
            this.hmiSevenSegment21.MaxValueForRed = 200F;
            this.hmiSevenSegment21.MinValueForRed = 100F;
            this.hmiSevenSegment21.Name = "hmiSevenSegment21";
            this.hmiSevenSegment21.NumberOfDigits = 5;
            this.hmiSevenSegment21.PLCAddressClick = null;
            this.hmiSevenSegment21.PLCAddressEnabled = null;
            this.hmiSevenSegment21.PLCAddressKeypad = "";
            this.hmiSevenSegment21.PLCAddressValue = "";
            this.hmiSevenSegment21.PLCAddressVisible = "";
            this.hmiSevenSegment21.Resolution = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hmiSevenSegment21.Size = new System.Drawing.Size(277, 72);
            this.hmiSevenSegment21.TabIndex = 7;
            this.hmiSevenSegment21.Text = "hmiSevenSegment21";
            this.hmiSevenSegment21.Value = 0F;
            this.hmiSevenSegment21.ValueScaleFactor = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // digitalDisplayControl1
            // 
            this.digitalDisplayControl1.BackColor = System.Drawing.Color.Transparent;
            this.digitalDisplayControl1.DigitColor = System.Drawing.Color.Red;
            this.digitalDisplayControl1.Location = new System.Drawing.Point(12, 68);
            this.digitalDisplayControl1.Name = "digitalDisplayControl1";
            this.digitalDisplayControl1.Size = new System.Drawing.Size(285, 86);
            this.digitalDisplayControl1.TabIndex = 8;
            this.digitalDisplayControl1.Text = "digitalDisplayControl1";
            this.digitalDisplayControl1.Value = "23445";
            // 
            // lxLedControl1
            // 
            this.lxLedControl1.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl1.BackColour_1 = System.Drawing.Color.Transparent;
            this.lxLedControl1.BackColour_2 = System.Drawing.Color.DimGray;
            this.lxLedControl1.BevelRate = 0.5F;
            this.lxLedControl1.FadedColour = System.Drawing.Color.DimGray;
            this.lxLedControl1.ForeColor = System.Drawing.Color.ForestGreen;
            this.lxLedControl1.HighlightOpaque = ((byte)(50));
            this.lxLedControl1.Location = new System.Drawing.Point(380, 269);
            this.lxLedControl1.Name = "lxLedControl1";
            this.lxLedControl1.Size = new System.Drawing.Size(386, 150);
            this.lxLedControl1.TabIndex = 9;
            this.lxLedControl1.Text = "00";
            this.lxLedControl1.TextAlignment = AdvancedScada.Controls_Binding.SevenSegment.LxLedControl.Alignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lxLedControl1);
            this.Controls.Add(this.digitalDisplayControl1);
            this.Controls.Add(this.hmiSevenSegment21);
            this.Controls.Add(this.hmiSegment7LED1);
            this.Controls.Add(this.hmiProcessIndicator1);
            this.Controls.Add(this.hmiLedSingle1);
            this.Controls.Add(this.hmiVacuumPump1);
            this.Controls.Add(this.hmiCheckBox1);
            this.Controls.Add(this.hmiSquareIlluminatedButton1);
            this.Controls.Add(this.hmiAnnunciator1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls_Binding.ImageAll.GraphicIndicatorBaseSVG graphicIndicatorBaseSVG1;
        private Controls_Binding.ButtonAll.HMIAnnunciator hmiAnnunciator1;
        private Controls_Binding.ButtonAll.HMISquareIlluminatedButton hmiSquareIlluminatedButton1;
        private Controls_Binding.ButtonAll.HMICheckBox hmiCheckBox1;
        private Controls_Binding.HslControl.Pipe.HMIVacuumPump hmiVacuumPump1;
        private Controls_Binding.Leds.HMILedSingle hmiLedSingle1;
        private Controls_Binding.ProcessAll.HMIProcessIndicator hmiProcessIndicator1;
        private Controls_Binding.Segment.HMISegment7LED hmiSegment7LED1;
        private Controls_Binding.SevenSegment.HMISevenSegment2 hmiSevenSegment21;
        private Controls_Net45.DigitalDisplayControl digitalDisplayControl1;
        private Controls_Binding.SevenSegment.LxLedControl lxLedControl1;
    }
}