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
            this.graphicIndicatorBaseSVG1 = new AdvancedScada.Controls_Binding.ImageAll.GraphicIndicatorBaseSVG();
            this.SuspendLayout();
            // 
            // graphicIndicatorBaseSVG1
            // 
            this.graphicIndicatorBaseSVG1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.graphicIndicatorBaseSVG1.Flash1 = false;
            this.graphicIndicatorBaseSVG1.Font2 = new System.Drawing.Font("Arial", 10F);
            this.graphicIndicatorBaseSVG1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.graphicIndicatorBaseSVG1.GraphicAllOff = resources.GetString("graphicIndicatorBaseSVG1.GraphicAllOff");
            this.graphicIndicatorBaseSVG1.GraphicSelect1 = resources.GetString("graphicIndicatorBaseSVG1.GraphicSelect1");
            this.graphicIndicatorBaseSVG1.GraphicSelect2 = resources.GetString("graphicIndicatorBaseSVG1.GraphicSelect2");
            this.graphicIndicatorBaseSVG1.Location = new System.Drawing.Point(50, 48);
            this.graphicIndicatorBaseSVG1.Name = "graphicIndicatorBaseSVG1";
            this.graphicIndicatorBaseSVG1.NumericFormat = null;
            this.graphicIndicatorBaseSVG1.OutputType = AdvancedScada.Controls_Binding.ImageAll.GraphicIndicatorBaseSVG.OutputTypes.MomentarySet;
            this.graphicIndicatorBaseSVG1.RotationAngle = AdvancedScada.Controls_Binding.ImageAll.GraphicIndicatorBaseSVG.RotationAngleEnum.NoRotation;
            this.graphicIndicatorBaseSVG1.Size = new System.Drawing.Size(95, 131);
            this.graphicIndicatorBaseSVG1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.graphicIndicatorBaseSVG1.TabIndex = 0;
            this.graphicIndicatorBaseSVG1.Text = "graphicIndicatorBaseSVG1";
            this.graphicIndicatorBaseSVG1.Text2 = "";
            this.graphicIndicatorBaseSVG1.ValueScaleFactor = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.graphicIndicatorBaseSVG1.ValueSelect1 = false;
            this.graphicIndicatorBaseSVG1.ValueSelect2 = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.graphicIndicatorBaseSVG1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls_Binding.ImageAll.GraphicIndicatorBaseSVG graphicIndicatorBaseSVG1;
    }
}