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
            this.sevenSegment21 = new AdvancedScada.Controls_Net45.SevenSegment2();
            this.sevenSegment31 = new AdvancedScada.Controls_Net45.SevenSegment3();
            this.sevenSegmentGDI1 = new AdvancedScada.Controls_Net45.SevenSegmentGDI();
            this.SuspendLayout();
            // 
            // sevenSegment21
            // 
            this.sevenSegment21.BackColor = System.Drawing.Color.Transparent;
            this.sevenSegment21.DecimalPosition = 0;
            this.sevenSegment21.ForecolorHighLimitValue = 999999D;
            this.sevenSegment21.ForeColorInLimits = System.Drawing.Color.White;
            this.sevenSegment21.ForecolorLowLimitValue = -999999D;
            this.sevenSegment21.ForeColorOverHighLimit = System.Drawing.Color.Red;
            this.sevenSegment21.ForeColorUnderLowLimit = System.Drawing.Color.Yellow;
            this.sevenSegment21.Location = new System.Drawing.Point(29, 143);
            this.sevenSegment21.Name = "sevenSegment21";
            this.sevenSegment21.NumberOfDigits = 5;
            this.sevenSegment21.ResolutionOfLastDigit = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sevenSegment21.ShowOffSegments = true;
            this.sevenSegment21.Size = new System.Drawing.Size(366, 95);
            this.sevenSegment21.TabIndex = 0;
            this.sevenSegment21.Text = "sevenSegment21";
            this.sevenSegment21.Value = 0D;
            // 
            // sevenSegment31
            // 
            this.sevenSegment31._ValueScaleFactor = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sevenSegment31.BackColor = System.Drawing.Color.Transparent;
            this.sevenSegment31.DecimalPosition = 0;
            this.sevenSegment31.ForeColor = System.Drawing.Color.LightGray;
            this.sevenSegment31.Location = new System.Drawing.Point(41, 35);
            this.sevenSegment31.MaxValueForRed = 200F;
            this.sevenSegment31.MinValueForRed = 100F;
            this.sevenSegment31.Name = "sevenSegment31";
            this.sevenSegment31.NumberOfDigits = 5;
            this.sevenSegment31.Resolution = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sevenSegment31.Size = new System.Drawing.Size(354, 92);
            this.sevenSegment31.TabIndex = 1;
            this.sevenSegment31.Text = "sevenSegment31";
            this.sevenSegment31.Value = 0D;
            // 
            // sevenSegmentGDI1
            // 
            this.sevenSegmentGDI1._ValueScaleFactor = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sevenSegmentGDI1.BackColor = System.Drawing.Color.Transparent;
            this.sevenSegmentGDI1.DecimalPosition = 0;
            this.sevenSegmentGDI1.ForeColor = System.Drawing.Color.LightGray;
            this.sevenSegmentGDI1.Location = new System.Drawing.Point(29, 244);
            this.sevenSegmentGDI1.MaxValueForRed = 200F;
            this.sevenSegmentGDI1.MinValueForRed = 100F;
            this.sevenSegmentGDI1.Name = "sevenSegmentGDI1";
            this.sevenSegmentGDI1.NumberOfDigits = 5;
            this.sevenSegmentGDI1.Resolution = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sevenSegmentGDI1.ShowOffSegments = true;
            this.sevenSegmentGDI1.Size = new System.Drawing.Size(354, 92);
            this.sevenSegmentGDI1.TabIndex = 2;
            this.sevenSegmentGDI1.Text = "sevenSegmentGDI1";
            this.sevenSegmentGDI1.Value = 0D;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sevenSegmentGDI1);
            this.Controls.Add(this.sevenSegment31);
            this.Controls.Add(this.sevenSegment21);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls_Net45.SevenSegment2 sevenSegment21;
        private Controls_Net45.SevenSegment3 sevenSegment31;
        private Controls_Net45.SevenSegmentGDI sevenSegmentGDI1;
    }
}