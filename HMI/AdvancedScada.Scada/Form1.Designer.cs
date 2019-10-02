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
            this.hmiDigitalPanelMeter1 = new AdvancedScada.Controls_Binding.DigitalDisplay.HMIDigitalPanelMeter();
            this.SuspendLayout();
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
            this.hmiDigitalPanelMeter1.PLCAddressKeypad = "CH1.PLC1.DataBlock6.TAG00132";
            this.hmiDigitalPanelMeter1.PLCAddressText = "";
            this.hmiDigitalPanelMeter1.PLCAddressValue = "CH1.PLC1.DataBlock6.TAG00132";
            this.hmiDigitalPanelMeter1.PLCAddressVisible = "CH1.PLC1.DataBlock1.TAG00001";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.hmiDigitalPanelMeter1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls_Binding.ImageAll.GraphicIndicatorBaseSVG graphicIndicatorBaseSVG1;
        private Controls_Binding.DigitalDisplay.HMIDigitalPanelMeter hmiDigitalPanelMeter1;
    }
}