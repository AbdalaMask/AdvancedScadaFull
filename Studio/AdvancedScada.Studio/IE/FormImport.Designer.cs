namespace AdvancedScada.Studio.IE
{
    partial class FormImport
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
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.cboxSheet = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnPathFile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.PathFile = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtDataBlock = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtDevice = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtChannel = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.DGImportForm = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonPanel3 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnExecute = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGImportForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(788, 31);
            this.kryptonHeader1.TabIndex = 0;
            this.kryptonHeader1.Values.Heading = "Excel files";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.cboxSheet);
            this.kryptonPanel1.Controls.Add(this.btnPathFile);
            this.kryptonPanel1.Controls.Add(this.PathFile);
            this.kryptonPanel1.Controls.Add(this.txtDataBlock);
            this.kryptonPanel1.Controls.Add(this.txtDevice);
            this.kryptonPanel1.Controls.Add(this.txtChannel);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 31);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(788, 71);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // cboxSheet
            // 
            this.cboxSheet.DropDownWidth = 269;
            this.cboxSheet.Location = new System.Drawing.Point(496, 44);
            this.cboxSheet.Name = "cboxSheet";
            this.cboxSheet.Size = new System.Drawing.Size(269, 21);
            this.cboxSheet.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxSheet.TabIndex = 10;
            this.cboxSheet.SelectedIndexChanged += new System.EventHandler(this.cboxSheet_SelectedIndexChanged);
            // 
            // btnPathFile
            // 
            this.btnPathFile.Location = new System.Drawing.Point(360, 46);
            this.btnPathFile.Name = "btnPathFile";
            this.btnPathFile.Size = new System.Drawing.Size(36, 22);
            this.btnPathFile.TabIndex = 9;
            this.btnPathFile.Values.Image = global::AdvancedScada.Studio.Properties.Resources.AddDataBlock;
            this.btnPathFile.Values.Text = "....";
            this.btnPathFile.Click += new System.EventHandler(this.btnPathFile_Click);
            // 
            // PathFile
            // 
            this.PathFile.Location = new System.Drawing.Point(106, 48);
            this.PathFile.Name = "PathFile";
            this.PathFile.Size = new System.Drawing.Size(248, 20);
            this.PathFile.TabIndex = 8;
            // 
            // txtDataBlock
            // 
            this.txtDataBlock.Location = new System.Drawing.Point(644, 6);
            this.txtDataBlock.Name = "txtDataBlock";
            this.txtDataBlock.Size = new System.Drawing.Size(121, 20);
            this.txtDataBlock.TabIndex = 7;
            // 
            // txtDevice
            // 
            this.txtDevice.Location = new System.Drawing.Point(402, 6);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(121, 20);
            this.txtDevice.TabIndex = 6;
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(106, 6);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(121, 20);
            this.txtChannel.TabIndex = 5;
            this.txtChannel.Text = " ";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(402, 45);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(38, 19);
            this.kryptonLabel5.TabIndex = 4;
            this.kryptonLabel5.Values.Text = "Sheet";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(550, 6);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(58, 19);
            this.kryptonLabel4.TabIndex = 3;
            this.kryptonLabel4.Values.Text = "DataBlock";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(319, 6);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(42, 19);
            this.kryptonLabel3.TabIndex = 2;
            this.kryptonLabel3.Values.Text = "Device";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(12, 45);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(52, 19);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "PathFile:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(12, 6);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(49, 19);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Channel";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.DGImportForm);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 102);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(788, 313);
            this.kryptonPanel2.TabIndex = 2;
            // 
            // DGImportForm
            // 
            this.DGImportForm.AllowUserToAddRows = false;
            this.DGImportForm.AllowUserToDeleteRows = false;
            this.DGImportForm.AllowUserToResizeRows = false;
            this.DGImportForm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGImportForm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.DGImportForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGImportForm.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Sheet;
            this.DGImportForm.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.DGImportForm.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.DGImportForm.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.DGImportForm.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.DGImportForm.Location = new System.Drawing.Point(0, 0);
            this.DGImportForm.MultiSelect = false;
            this.DGImportForm.Name = "DGImportForm";
            this.DGImportForm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGImportForm.ShowCellErrors = false;
            this.DGImportForm.ShowRowErrors = false;
            this.DGImportForm.Size = new System.Drawing.Size(788, 313);
            this.DGImportForm.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "TagId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "TagName";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Address";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "DataType";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Description";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.btnExecute);
            this.kryptonPanel3.Controls.Add(this.btnOK);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 415);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(788, 37);
            this.kryptonPanel3.TabIndex = 3;
            // 
            // btnExecute
            // 
            this.btnExecute.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExecute.Location = new System.Drawing.Point(548, 0);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(120, 37);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Values.Text = "Execute";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(668, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 37);
            this.btnOK.TabIndex = 0;
            this.btnOK.Values.Text = "Cancel";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormImport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(788, 452);
            this.Controls.Add(this.kryptonPanel3);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonHeader1);
            this.Name = "FormImport";
            this.Text = "FormImport";
            this.Load += new System.EventHandler(this.FormImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGImportForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxSheet;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnPathFile;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox PathFile;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDataBlock;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDevice;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtChannel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView DGImportForm;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExecute;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}