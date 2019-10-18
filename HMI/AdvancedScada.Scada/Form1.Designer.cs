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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.graphicIndicatorBaseSVG2 = new AdvancedScada.Controls_Binding.ImageAll.GraphicIndicatorBaseSVG();
            this.hmiBottle1 = new AdvancedScada.Controls_Binding.HslControl.TankAll.HMIBottle();
            this.hmiLedDisplay1 = new AdvancedScada.Controls_Binding.HslControl.Segment.HMILedDisplay();
            this.hmiVacuumPump1 = new AdvancedScada.Controls_Binding.HslControl.Pipe.HMIVacuumPump();
            this.hmiPipeLine1 = new AdvancedScada.Controls_Binding.HslControl.Pipe.HMIPipeLine();
            this.hmiMotor1 = new AdvancedScada.Controls_Binding.HslControl.Motor.HMIMotor();
            this.hmiLabel1 = new AdvancedScada.Controls_Binding.Display.HMILabel();
            this.hmiSegment7LED1 = new AdvancedScada.Controls_Binding.Segment.HMISegment7LED();
            this.hmiDigitalPanelMeter1 = new AdvancedScada.Controls_Binding.DigitalDisplay.HMIDigitalPanelMeter();
            this.graphicIndicatorBaseSVG3 = new AdvancedScada.Controls_Binding.ImageAll.GraphicIndicatorBaseSVG();
            this.SuspendLayout();
            // 
            // graphicIndicatorBaseSVG2
            // 
            this.graphicIndicatorBaseSVG2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.graphicIndicatorBaseSVG2.Flash1 = false;
            this.graphicIndicatorBaseSVG2.Font2 = new System.Drawing.Font("Arial", 10F);
            this.graphicIndicatorBaseSVG2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.graphicIndicatorBaseSVG2.GraphicAllOff = resources.GetString("graphicIndicatorBaseSVG2.GraphicAllOff");
            this.graphicIndicatorBaseSVG2.GraphicSelect1 = resources.GetString("graphicIndicatorBaseSVG2.GraphicSelect1");
            this.graphicIndicatorBaseSVG2.GraphicSelect2 = null;
            this.graphicIndicatorBaseSVG2.Location = new System.Drawing.Point(490, 251);
            this.graphicIndicatorBaseSVG2.Name = "graphicIndicatorBaseSVG2";
            this.graphicIndicatorBaseSVG2.NumericFormat = null;
            this.graphicIndicatorBaseSVG2.OutputType = AdvancedScada.Controls_Binding.ImageAll.GraphicIndicatorBaseSVG.OutputTypes.MomentarySet;
            this.graphicIndicatorBaseSVG2.PLCAddressClick = null;
            this.graphicIndicatorBaseSVG2.PLCAddressEnabled = null;
            this.graphicIndicatorBaseSVG2.PLCAddressValue = null;
            this.graphicIndicatorBaseSVG2.PLCAddressValueSelect1 = "CH3.PLC1.DataBlock1.TAG00011";
            this.graphicIndicatorBaseSVG2.PLCAddressVisible = null;
            this.graphicIndicatorBaseSVG2.RotationAngle = AdvancedScada.Controls_Binding.ImageAll.GraphicIndicatorBaseSVG.RotationAngleEnum.NoRotation;
            this.graphicIndicatorBaseSVG2.Size = new System.Drawing.Size(158, 121);
            this.graphicIndicatorBaseSVG2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.graphicIndicatorBaseSVG2.TabIndex = 9;
            this.graphicIndicatorBaseSVG2.Text2 = "";
            this.graphicIndicatorBaseSVG2.ValueScaleFactor = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.graphicIndicatorBaseSVG2.ValueSelect1 = false;
            this.graphicIndicatorBaseSVG2.ValueSelect2 = false;
            // 
            // hmiBottle1
            // 
            this.hmiBottle1.BackColorCenter = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.hmiBottle1.BackColorEdge = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(196)))), ((int)(((byte)(216)))));
            this.hmiBottle1.BackColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(232)))), ((int)(((byte)(244)))));
            this.hmiBottle1.ForeColorCenter = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(221)))), ((int)(((byte)(98)))));
            this.hmiBottle1.ForeColorEdge = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(190)))), ((int)(((byte)(77)))));
            this.hmiBottle1.ForeColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(139)))));
            this.hmiBottle1.Location = new System.Drawing.Point(682, 44);
            this.hmiBottle1.Name = "hmiBottle1";
            this.hmiBottle1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.MomentarySet;
            this.hmiBottle1.PLCAddressClick = null;
            this.hmiBottle1.PLCAddressEnabled = null;
            this.hmiBottle1.PLCAddressText = "";
            this.hmiBottle1.PLCAddressValue = "CH3.PLC1.DataBlock2.TAG00018";
            this.hmiBottle1.PLCAddressVisible = "";
            this.hmiBottle1.Size = new System.Drawing.Size(98, 240);
            this.hmiBottle1.TabIndex = 8;
            this.hmiBottle1.Value = 50D;
            // 
            // hmiLedDisplay1
            // 
            this.hmiLedDisplay1.BackColor = System.Drawing.Color.Transparent;
            this.hmiLedDisplay1.DisplayBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.hmiLedDisplay1.DisplayNumber = 8;
            this.hmiLedDisplay1.DisplayText = "123.45";
            this.hmiLedDisplay1.ForeColor = System.Drawing.Color.Black;
            this.hmiLedDisplay1.KeypadFontColor = System.Drawing.Color.WhiteSmoke;
            this.hmiLedDisplay1.KeypadMaxValue = 0D;
            this.hmiLedDisplay1.KeypadMinValue = 0D;
            this.hmiLedDisplay1.KeypadScaleFactor = 1D;
            this.hmiLedDisplay1.KeypadText = null;
            this.hmiLedDisplay1.KeypadWidth = 300;
            this.hmiLedDisplay1.LedNumberSize = 5;
            this.hmiLedDisplay1.Location = new System.Drawing.Point(42, 262);
            this.hmiLedDisplay1.Margin = new System.Windows.Forms.Padding(3, 11, 3, 11);
            this.hmiLedDisplay1.Name = "hmiLedDisplay1";
            this.hmiLedDisplay1.PLCAddressClick = null;
            this.hmiLedDisplay1.PLCAddressEnabled = null;
            this.hmiLedDisplay1.PLCAddressKeypad = "CH3.PLC1.DataBlock2.TAG00019";
            this.hmiLedDisplay1.PLCAddressText = "";
            this.hmiLedDisplay1.PLCAddressValue = "CH3.PLC1.DataBlock2.TAG00019";
            this.hmiLedDisplay1.PLCAddressVisible = "";
            this.hmiLedDisplay1.Size = new System.Drawing.Size(286, 71);
            this.hmiLedDisplay1.TabIndex = 6;
            this.hmiLedDisplay1.Value = null;
            // 
            // hmiVacuumPump1
            // 
            this.hmiVacuumPump1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(151)))), ((int)(((byte)(0)))));
            this.hmiVacuumPump1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.hmiVacuumPump1.Color3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.hmiVacuumPump1.Color4 = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.hmiVacuumPump1.Location = new System.Drawing.Point(364, 238);
            this.hmiVacuumPump1.MaximumHoldTime = 3000;
            this.hmiVacuumPump1.MinimumHoldTime = 500;
            this.hmiVacuumPump1.Name = "hmiVacuumPump1";
            this.hmiVacuumPump1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.Toggle;
            this.hmiVacuumPump1.PLCAddressClick = "CH3.PLC1.DataBlock1.TAG00011";
            this.hmiVacuumPump1.PLCAddressEnabled = null;
            this.hmiVacuumPump1.PLCAddressText = "";
            this.hmiVacuumPump1.PLCAddressValue = "CH3.PLC1.DataBlock1.TAG00011";
            this.hmiVacuumPump1.PLCAddressVisible = "";
            this.hmiVacuumPump1.Size = new System.Drawing.Size(120, 147);
            this.hmiVacuumPump1.TabIndex = 5;
            this.hmiVacuumPump1.Text = "hmiVacuumPump1";
            this.hmiVacuumPump1.Value = false;
            this.hmiVacuumPump1.ValueToWrite = 0;
            // 
            // hmiPipeLine1
            // 
            this.hmiPipeLine1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("hmiPipeLine1.BackgroundImage")));
            this.hmiPipeLine1.Location = new System.Drawing.Point(364, 196);
            this.hmiPipeLine1.MaximumHoldTime = 3000;
            this.hmiPipeLine1.MinimumHoldTime = 500;
            this.hmiPipeLine1.Name = "hmiPipeLine1";
            this.hmiPipeLine1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.Toggle;
            this.hmiPipeLine1.PipeLineActive = true;
            this.hmiPipeLine1.PLCAddressClick = "CH3.PLC1.DataBlock1.TAG00002";
            this.hmiPipeLine1.PLCAddressEnabled = null;
            this.hmiPipeLine1.PLCAddressText = "";
            this.hmiPipeLine1.PLCAddressValue = "CH3.PLC1.DataBlock1.TAG00002";
            this.hmiPipeLine1.PLCAddressVisible = "";
            this.hmiPipeLine1.Size = new System.Drawing.Size(297, 36);
            this.hmiPipeLine1.TabIndex = 4;
            this.hmiPipeLine1.Value = false;
            this.hmiPipeLine1.ValueToWrite = 0;
            // 
            // hmiMotor1
            // 
            this.hmiMotor1.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.hmiMotor1.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(96)))));
            this.hmiMotor1.Location = new System.Drawing.Point(205, 162);
            this.hmiMotor1.MaximumHoldTime = 3000;
            this.hmiMotor1.MinimumHoldTime = 500;
            this.hmiMotor1.Name = "hmiMotor1";
            this.hmiMotor1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.Toggle;
            this.hmiMotor1.PLCAddressClick = "CH3.PLC1.DataBlock1.TAG00002";
            this.hmiMotor1.PLCAddressEnabled = null;
            this.hmiMotor1.PLCAddressText = "";
            this.hmiMotor1.PLCAddressValue = "CH3.PLC1.DataBlock1.TAG00002";
            this.hmiMotor1.PLCAddressVisible = "";
            this.hmiMotor1.SelactadMotorColor = AdvancedScada.Controls_Binding.MotorColor.Green;
            this.hmiMotor1.Size = new System.Drawing.Size(123, 86);
            this.hmiMotor1.TabIndex = 3;
            this.hmiMotor1.Text = "hmiMotor1";
            this.hmiMotor1.Value = false;
            this.hmiMotor1.ValueToWrite = 0;
            // 
            // hmiLabel1
            // 
            this.hmiLabel1.BackColor = System.Drawing.Color.Transparent;
            this.hmiLabel1.BooleanDisplay = AdvancedScada.Controls_Binding.Display.HMILabel.BooleanDisplayOption.TrueFalse;
            this.hmiLabel1.DisplayAsTime = false;
            this.hmiLabel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.hmiLabel1.ForeColor = System.Drawing.Color.Black;
            this.hmiLabel1.Highlight = false;
            this.hmiLabel1.HighlightColor = System.Drawing.Color.Red;
            this.hmiLabel1.HighlightForeColor = System.Drawing.Color.White;
            this.hmiLabel1.HighlightKeyCharacter = "!";
            this.hmiLabel1.InterpretValueAsBCD = false;
            this.hmiLabel1.KeypadAlphanumeric = false;
            this.hmiLabel1.KeypadAlphaNumeric = false;
            this.hmiLabel1.KeypadFont = new System.Drawing.Font("Arial", 10F);
            this.hmiLabel1.KeypadFontColor = System.Drawing.Color.WhiteSmoke;
            this.hmiLabel1.KeypadMaxValue = 0D;
            this.hmiLabel1.KeypadMinValue = 0D;
            this.hmiLabel1.KeypadScaleFactor = 1D;
            this.hmiLabel1.KeypadShowCurrentValue = false;
            this.hmiLabel1.KeypadText = null;
            this.hmiLabel1.KeypadWidth = 300;
            this.hmiLabel1.Location = new System.Drawing.Point(39, 162);
            this.hmiLabel1.Name = "hmiLabel1";
            this.hmiLabel1.NumericFormat = null;
            this.hmiLabel1.PLCAddressClick = null;
            this.hmiLabel1.PLCAddressEnabled = "Ch1.PLC1.DataBlock2.TAG00023";
            this.hmiLabel1.PLCAddressKeypad = "Ch1.PLC1.DataBlock1.TAG00003";
            this.hmiLabel1.PLCAddressValue = "Ch1.PLC1.DataBlock1.TAG00003";
            this.hmiLabel1.PLCAddressVisible = "Ch1.PLC1.DataBlock2.TAG00023";
            this.hmiLabel1.PollRate = 0;
            this.hmiLabel1.Size = new System.Drawing.Size(160, 43);
            this.hmiLabel1.TabIndex = 2;
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
            this.hmiSegment7LED1.LEDDateTime = new System.DateTime(2019, 10, 2, 21, 1, 16, 305);
            this.hmiSegment7LED1.Location = new System.Drawing.Point(344, 37);
            this.hmiSegment7LED1.MiddleBorderColor = System.Drawing.Color.Gray;
            this.hmiSegment7LED1.MiddleBorderLength = 0;
            this.hmiSegment7LED1.Name = "hmiSegment7LED1";
            this.hmiSegment7LED1.OuterBorderDarkColor = System.Drawing.Color.DarkGray;
            this.hmiSegment7LED1.OuterBorderLength = 5;
            this.hmiSegment7LED1.OuterBorderLightColor = System.Drawing.Color.Lavender;
            this.hmiSegment7LED1.PLCAddressKeypad = "CH3.PLC1.DataBlock2.TAG00018";
            this.hmiSegment7LED1.PLCAddressText = "";
            this.hmiSegment7LED1.PLCAddressValue = "CH3.PLC1.DataBlock2.TAG00018";
            this.hmiSegment7LED1.PLCAddressVisible = "CH3.PLC1.DataBlock1.TAG00001";
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
            this.hmiSegment7LED1.Size = new System.Drawing.Size(290, 92);
            this.hmiSegment7LED1.TabIndex = 1;
            this.hmiSegment7LED1.Text = "hmiSegment7LED1";
            this.hmiSegment7LED1.UnitVisible = false;
            this.hmiSegment7LED1.Value = 888D;
            // 
            // hmiDigitalPanelMeter1
            // 
            this.hmiDigitalPanelMeter1.DecimalPosition = 0;
            this.hmiDigitalPanelMeter1.ForeColor = System.Drawing.Color.LightGray;
            this.hmiDigitalPanelMeter1.KeypadFontColor = System.Drawing.Color.WhiteSmoke;
            this.hmiDigitalPanelMeter1.KeypadMaxValue = 0D;
            this.hmiDigitalPanelMeter1.KeypadMinValue = 0D;
            this.hmiDigitalPanelMeter1.KeypadScaleFactor = 1D;
            this.hmiDigitalPanelMeter1.KeypadText = null;
            this.hmiDigitalPanelMeter1.KeypadWidth = 300;
            this.hmiDigitalPanelMeter1.Location = new System.Drawing.Point(17, 13);
            this.hmiDigitalPanelMeter1.Name = "hmiDigitalPanelMeter1";
            this.hmiDigitalPanelMeter1.NumberOfDigits = 5;
            this.hmiDigitalPanelMeter1.PLCAddressClick = null;
            this.hmiDigitalPanelMeter1.PLCAddressEnabled = null;
            this.hmiDigitalPanelMeter1.PLCAddressKeypad = "CH3.PLC1.DataBlock2.TAG00017";
            this.hmiDigitalPanelMeter1.PLCAddressText = "";
            this.hmiDigitalPanelMeter1.PLCAddressValue = "CH3.PLC1.DataBlock2.TAG00017";
            this.hmiDigitalPanelMeter1.PLCAddressVisible = "CH3.PLC1.DataBlock1.TAG00001";
            this.hmiDigitalPanelMeter1.Resolution = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hmiDigitalPanelMeter1.Size = new System.Drawing.Size(311, 135);
            this.hmiDigitalPanelMeter1.TabIndex = 0;
            this.hmiDigitalPanelMeter1.Text = "hmiDigitalPanelMeter1";
            this.hmiDigitalPanelMeter1.Value = 0F;
            this.hmiDigitalPanelMeter1.ValueScaleFactor = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // graphicIndicatorBaseSVG3
            // 
            this.graphicIndicatorBaseSVG3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.graphicIndicatorBaseSVG3.Flash1 = false;
            this.graphicIndicatorBaseSVG3.Font2 = new System.Drawing.Font("Arial", 10F);
            this.graphicIndicatorBaseSVG3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.graphicIndicatorBaseSVG3.GraphicAllOff = resources.GetString("graphicIndicatorBaseSVG3.GraphicAllOff");
            this.graphicIndicatorBaseSVG3.GraphicSelect1 = resources.GetString("graphicIndicatorBaseSVG3.GraphicSelect1");
            this.graphicIndicatorBaseSVG3.GraphicSelect2 = null;
            this.graphicIndicatorBaseSVG3.Location = new System.Drawing.Point(630, 330);
            this.graphicIndicatorBaseSVG3.Name = "graphicIndicatorBaseSVG3";
            this.graphicIndicatorBaseSVG3.NumericFormat = null;
            this.graphicIndicatorBaseSVG3.OutputType = AdvancedScada.Controls_Binding.ImageAll.GraphicIndicatorBaseSVG.OutputTypes.MomentarySet;
            this.graphicIndicatorBaseSVG3.PLCAddressClick = null;
            this.graphicIndicatorBaseSVG3.PLCAddressEnabled = null;
            this.graphicIndicatorBaseSVG3.PLCAddressValue = null;
            this.graphicIndicatorBaseSVG3.PLCAddressValueSelect1 = "CH3.PLC1.DataBlock1.TAG00011";
            this.graphicIndicatorBaseSVG3.PLCAddressVisible = null;
            this.graphicIndicatorBaseSVG3.RotationAngle = AdvancedScada.Controls_Binding.ImageAll.GraphicIndicatorBaseSVG.RotationAngleEnum.NoRotation;
            this.graphicIndicatorBaseSVG3.Size = new System.Drawing.Size(158, 121);
            this.graphicIndicatorBaseSVG3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.graphicIndicatorBaseSVG3.TabIndex = 10;
            this.graphicIndicatorBaseSVG3.Text2 = "";
            this.graphicIndicatorBaseSVG3.ValueScaleFactor = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.graphicIndicatorBaseSVG3.ValueSelect1 = false;
            this.graphicIndicatorBaseSVG3.ValueSelect2 = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.graphicIndicatorBaseSVG2);
            this.Controls.Add(this.hmiBottle1);
            this.Controls.Add(this.hmiLedDisplay1);
            this.Controls.Add(this.hmiVacuumPump1);
            this.Controls.Add(this.hmiPipeLine1);
            this.Controls.Add(this.hmiMotor1);
            this.Controls.Add(this.hmiLabel1);
            this.Controls.Add(this.hmiSegment7LED1);
            this.Controls.Add(this.hmiDigitalPanelMeter1);
            this.Controls.Add(this.graphicIndicatorBaseSVG3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls_Binding.ImageAll.GraphicIndicatorBaseSVG graphicIndicatorBaseSVG1;
        private Controls_Binding.DigitalDisplay.HMIDigitalPanelMeter hmiDigitalPanelMeter1;
        private Controls_Binding.Segment.HMISegment7LED hmiSegment7LED1;
        private Controls_Binding.Display.HMILabel hmiLabel1;
        private Controls_Binding.HslControl.Motor.HMIMotor hmiMotor1;
        private Controls_Binding.HslControl.Pipe.HMIPipeLine hmiPipeLine1;
        private Controls_Binding.HslControl.Pipe.HMIVacuumPump hmiVacuumPump1;
        private Controls_Binding.HslControl.Segment.HMILedDisplay hmiLedDisplay1;
        private Controls_Binding.HslControl.TankAll.HMIBottle hmiBottle1;
        private Controls_Binding.ImageAll.GraphicIndicatorBaseSVG graphicIndicatorBaseSVG2;
        private Controls_Binding.ImageAll.GraphicIndicatorBaseSVG graphicIndicatorBaseSVG3;
    }
}