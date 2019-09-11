namespace AdvancedScada.Studio.Logging
{
    partial class XtraFormLogging
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
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.DGFormLogging = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGFormLogging)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.DGFormLogging);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(711, 391);
            this.kryptonHeaderGroup1.TabIndex = 1;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Logging";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.Studio.Properties.Resources.Copy_16x16;
            // 
            // DGFormLogging
            // 
            this.DGFormLogging.AllowUserToAddRows = false;
            this.DGFormLogging.AllowUserToDeleteRows = false;
            this.DGFormLogging.AllowUserToResizeRows = false;
            this.DGFormLogging.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGFormLogging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGFormLogging.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Sheet;
            this.DGFormLogging.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.DGFormLogging.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.DGFormLogging.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.DGFormLogging.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.DGFormLogging.HideOuterBorders = true;
            this.DGFormLogging.Location = new System.Drawing.Point(0, 0);
            this.DGFormLogging.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGFormLogging.MultiSelect = false;
            this.DGFormLogging.Name = "DGFormLogging";
            this.DGFormLogging.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGFormLogging.Size = new System.Drawing.Size(709, 359);
            this.DGFormLogging.TabIndex = 0;
            // 
            // XtraFormLogging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 391);
            this.ControlBox = false;
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockActive;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "XtraFormLogging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logging";
            this.Load += new System.EventHandler(this.XtraFormLogging_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGFormLogging)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView DGFormLogging;
    }
}