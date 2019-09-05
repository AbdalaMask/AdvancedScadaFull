namespace AdvancedScada.Studio.DB.SQLite
{
    partial class SQLiteFormCreate
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
            this.btnCreate_database = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Databasename_Button = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtDatabasename = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.btnCreateTable = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCreate_Filed = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.ComFiledType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtFiledName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnCreate_Table = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtTableName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.chkKey = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ComFiledType)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.btnCreate_database);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.Databasename_Button);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtDatabasename);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(323, 419);
            this.kryptonHeaderGroup1.TabIndex = 0;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "CREATE_DATABASE";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::AdvancedScada.Studio.Properties.Resources.Database_16x16;
            // 
            // btnCreate_database
            // 
            this.btnCreate_database.Location = new System.Drawing.Point(66, 52);
            this.btnCreate_database.Name = "btnCreate_database";
            this.btnCreate_database.Size = new System.Drawing.Size(151, 25);
            this.btnCreate_database.TabIndex = 3;
            this.btnCreate_database.Values.Text = "OK";
            this.btnCreate_database.Click += new System.EventHandler(this.btnCreate_database_Click);
            // 
            // Databasename_Button
            // 
            this.Databasename_Button.Location = new System.Drawing.Point(203, 26);
            this.Databasename_Button.Name = "Databasename_Button";
            this.Databasename_Button.Size = new System.Drawing.Size(41, 20);
            this.Databasename_Button.TabIndex = 2;
            this.Databasename_Button.Values.Text = "...";
            this.Databasename_Button.Click += new System.EventHandler(this.Databasename_Button_Click);
            // 
            // txtDatabasename
            // 
            this.txtDatabasename.Location = new System.Drawing.Point(97, 26);
            this.txtDatabasename.Name = "txtDatabasename";
            this.txtDatabasename.Size = new System.Drawing.Size(100, 20);
            this.txtDatabasename.TabIndex = 1;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 26);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(94, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Databasename:";
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(323, 0);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.btnCreateTable);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.btnCreate_Filed);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.ComFiledType);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.txtFiledName);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.btnCreate_Table);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.txtTableName);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.chkKey);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(379, 419);
            this.kryptonHeaderGroup2.TabIndex = 1;
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "CREATE_TABLE";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = global::AdvancedScada.Studio.Properties.Resources.Database_16x16;
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.Location = new System.Drawing.Point(46, 188);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(277, 25);
            this.btnCreateTable.TabIndex = 9;
            this.btnCreateTable.Values.Text = "CreateTable";
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // btnCreate_Filed
            // 
            this.btnCreate_Filed.Location = new System.Drawing.Point(46, 157);
            this.btnCreate_Filed.Name = "btnCreate_Filed";
            this.btnCreate_Filed.Size = new System.Drawing.Size(277, 25);
            this.btnCreate_Filed.TabIndex = 8;
            this.btnCreate_Filed.Values.Text = "OK";
            this.btnCreate_Filed.Click += new System.EventHandler(this.btnCreate_Filed_Click);
            // 
            // ComFiledType
            // 
            this.ComFiledType.DropDownWidth = 188;
            this.ComFiledType.Items.AddRange(new object[] {
            "INTEGER",
            "TEXT",
            "BLOB",
            "REAL",
            "NUMERIC",
            "NVARCHAR(2048)"});
            this.ComFiledType.Location = new System.Drawing.Point(97, 130);
            this.ComFiledType.Name = "ComFiledType";
            this.ComFiledType.Size = new System.Drawing.Size(188, 21);
            this.ComFiledType.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ComFiledType.TabIndex = 7;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(3, 130);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(69, 20);
            this.kryptonLabel4.TabIndex = 6;
            this.kryptonLabel4.Values.Text = "Filed Type:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(3, 104);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel3.TabIndex = 5;
            this.kryptonLabel3.Values.Text = "Filed Name:";
            // 
            // txtFiledName
            // 
            this.txtFiledName.Location = new System.Drawing.Point(97, 104);
            this.txtFiledName.Name = "txtFiledName";
            this.txtFiledName.Size = new System.Drawing.Size(172, 20);
            this.txtFiledName.TabIndex = 4;
            // 
            // btnCreate_Table
            // 
            this.btnCreate_Table.Location = new System.Drawing.Point(97, 52);
            this.btnCreate_Table.Name = "btnCreate_Table";
            this.btnCreate_Table.Size = new System.Drawing.Size(195, 25);
            this.btnCreate_Table.TabIndex = 3;
            this.btnCreate_Table.Values.Text = "OK";
            this.btnCreate_Table.Click += new System.EventHandler(this.btnCreate_Table_Click);
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(109, 26);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(176, 20);
            this.txtTableName.TabIndex = 2;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(15, 26);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(85, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "TABLE NAME:";
            // 
            // chkKey
            // 
            this.chkKey.Location = new System.Drawing.Point(275, 104);
            this.chkKey.Name = "chkKey";
            this.chkKey.Size = new System.Drawing.Size(98, 20);
            this.chkKey.TabIndex = 0;
            this.chkKey.Values.Text = "PRIMARY KEY";
            // 
            // SQLiteFormCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 419);
            this.ControlBox = false;
            this.Controls.Add(this.kryptonHeaderGroup2);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SQLiteFormCreate";
            this.Text = "SQLiteFormCreate";
            this.Load += new System.EventHandler(this.SQLiteFormCreate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ComFiledType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Databasename_Button;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDatabasename;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCreateTable;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCreate_Filed;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ComFiledType;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtFiledName;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCreate_Table;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtTableName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkKey;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCreate_database;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}