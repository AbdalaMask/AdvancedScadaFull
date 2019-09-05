namespace AdvancedScada.Studio.DB
{
    partial class FormSQLServerUtils
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
            this.kryptonSplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.TableNamesListBox = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.ColumnLookUpEdit = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.DBListLookUpEditButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DBListLookUpEdit = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
          
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBListLookUpEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 54);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kryptonHeaderGroup1);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.ColumnLookUpEdit);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(712, 464);
            this.kryptonSplitContainer1.SplitterDistance = 232;
            this.kryptonSplitContainer1.TabIndex = 3;
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.TableNamesListBox);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(232, 464);
            this.kryptonHeaderGroup1.TabIndex = 0;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Tables";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.Studio.Properties.Resources.Database_16x16;
            // 
            // TableNamesListBox
            // 
            this.TableNamesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableNamesListBox.Location = new System.Drawing.Point(0, 0);
            this.TableNamesListBox.Name = "TableNamesListBox";
            this.TableNamesListBox.Size = new System.Drawing.Size(230, 419);
            this.TableNamesListBox.TabIndex = 0;
            this.TableNamesListBox.SelectedIndexChanged += new System.EventHandler(this.TableNamesListBox_SelectedIndexChanged);
            this.TableNamesListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TableNamesListBox_MouseClick);
            // 
            // ColumnLookUpEdit
            // 
            this.ColumnLookUpEdit.AllowUserToAddRows = false;
            this.ColumnLookUpEdit.AllowUserToDeleteRows = false;
            this.ColumnLookUpEdit.AllowUserToResizeRows = false;
            this.ColumnLookUpEdit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ColumnLookUpEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColumnLookUpEdit.Location = new System.Drawing.Point(0, 0);
            this.ColumnLookUpEdit.Name = "ColumnLookUpEdit";
            this.ColumnLookUpEdit.Size = new System.Drawing.Size(475, 464);
            this.ColumnLookUpEdit.TabIndex = 1;
            this.ColumnLookUpEdit.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ColumnLookUpEdit_CellContentClick);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.DBListLookUpEditButton);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.DBListLookUpEdit);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(712, 54);
            this.kryptonPanel1.TabIndex = 4;
            // 
            // DBListLookUpEditButton
            // 
            this.DBListLookUpEditButton.Location = new System.Drawing.Point(681, 12);
            this.DBListLookUpEditButton.Name = "DBListLookUpEditButton";
            this.DBListLookUpEditButton.Size = new System.Drawing.Size(28, 25);
            this.DBListLookUpEditButton.TabIndex = 2;
            this.DBListLookUpEditButton.Values.Text = "....";
            this.DBListLookUpEditButton.Click += new System.EventHandler(this.DBListLookUpEditButton_Click);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(19, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(78, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Image = global::AdvancedScada.Studio.Properties.Resources.Database_16x16;
            this.kryptonLabel1.Values.Text = "Databaes";
            // 
            // DBListLookUpEdit
            // 
            this.DBListLookUpEdit.DropDownWidth = 586;
            this.DBListLookUpEdit.Location = new System.Drawing.Point(121, 13);
            this.DBListLookUpEdit.Name = "DBListLookUpEdit";
            this.DBListLookUpEdit.Size = new System.Drawing.Size(554, 21);
            this.DBListLookUpEdit.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.DBListLookUpEdit.TabIndex = 1;
            this.DBListLookUpEdit.SelectedIndexChanged += new System.EventHandler(this.ColumnLookUpEdit_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
          
            // FormSQLServerUtils
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 518);
            this.ControlBox = false;
            this.Controls.Add(this.kryptonSplitContainer1);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormSQLServerUtils";
            this.Text = "SQLServerUtils";
            this.Load += new System.EventHandler(this.FormSQLServerUtils_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ColumnLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBListLookUpEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox TableNamesListBox;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView ColumnLookUpEdit;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton DBListLookUpEditButton;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DBListLookUpEdit;
   
    }
}