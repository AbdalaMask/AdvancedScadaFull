namespace AdvancedScada.Studio.Service
{
    partial class FormServerUtils
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txtStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsChannelCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtChannelCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.DGServerUtils = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.ColDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBinding = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGServerUtils)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtStatus,
            this.toolStripSeparator1,
            this.tsChannelCount,
            this.toolStripSeparator2,
            this.txtChannelCount,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 411);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(752, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // txtStatus
            // 
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(52, 22);
            this.txtStatus.Text = "txtStatus";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsChannelCount
            // 
            this.tsChannelCount.Name = "tsChannelCount";
            this.tsChannelCount.Size = new System.Drawing.Size(99, 22);
            this.tsChannelCount.Text = "   ChannelCount :";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // txtChannelCount
            // 
            this.txtChannelCount.Name = "txtChannelCount";
            this.txtChannelCount.Size = new System.Drawing.Size(13, 22);
            this.txtChannelCount.Text = "0";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // DGServerUtils
            // 
            this.DGServerUtils.AllowUserToAddRows = false;
            this.DGServerUtils.AllowUserToDeleteRows = false;
            this.DGServerUtils.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGServerUtils.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGServerUtils.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGServerUtils.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColDateTime,
            this.ColBinding,
            this.ColAddress});
            this.DGServerUtils.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGServerUtils.Location = new System.Drawing.Point(0, 0);
            this.DGServerUtils.Name = "DGServerUtils";
            this.DGServerUtils.Size = new System.Drawing.Size(752, 411);
            this.DGServerUtils.TabIndex = 1;
            // 
            // ColDateTime
            // 
            this.ColDateTime.HeaderText = "DateTime";
            this.ColDateTime.Name = "ColDateTime";
            this.ColDateTime.ReadOnly = true;
            // 
            // ColBinding
            // 
            this.ColBinding.HeaderText = "Binding";
            this.ColBinding.Name = "ColBinding";
            this.ColBinding.ReadOnly = true;
            // 
            // ColAddress
            // 
            this.ColAddress.HeaderText = "Address";
            this.ColAddress.Name = "ColAddress";
            this.ColAddress.ReadOnly = true;
            // 
            // FormServerUtils
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 436);
            this.ControlBox = false;
            this.Controls.Add(this.DGServerUtils);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormServerUtils";
            this.ShowIcon = false;
            this.Text = "FormServerUtils";
            this.Load += new System.EventHandler(this.FormServerUtils_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGServerUtils)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView DGServerUtils;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBinding;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAddress;
        private System.Windows.Forms.ToolStripLabel txtStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tsChannelCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel txtChannelCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}