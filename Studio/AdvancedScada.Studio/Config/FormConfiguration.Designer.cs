namespace AdvancedScada.Studio.Config
{
    partial class FormConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfiguration));
            this.kryptonNavigator1 = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.txtPort = new ComponentFactory.Krypton.Toolkit.KryptonDomainUpDown();
            this.txtIPAddress = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPage2 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.cboxDatabaseTypes = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtServerName = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPage3 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.btnLibraryImages = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtLibraryImages = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnTypeForms = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnLibraryImage = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cboxTypeForms = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.cboxLibraryImage = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.FBD = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            this.kryptonPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxDatabaseTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).BeginInit();
            this.kryptonPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.HidePage;
            this.kryptonNavigator1.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonNavigator1.Header.HeaderVisiblePrimary = false;
            this.kryptonNavigator1.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.HeaderBarCheckButtonHeaderGroup;
            this.kryptonNavigator1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2,
            this.kryptonPage3});
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(466, 280);
            this.kryptonNavigator1.TabIndex = 0;
            this.kryptonNavigator1.Text = "Configuration";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.txtPort);
            this.kryptonPage1.Controls.Add(this.txtIPAddress);
            this.kryptonPage1.Controls.Add(this.kryptonLabel2);
            this.kryptonPage1.Controls.Add(this.kryptonLabel1);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.ImageSmall = global::AdvancedScada.Studio.Properties.Resources.AddDataBlock;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(67, 62);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(464, 226);
            this.kryptonPage1.Text = "PC Server";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "01BFE04B86C94B6E5DBAE7CFBD60404F";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(120, 32);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(141, 22);
            this.txtPort.StateCommon.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "8080";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(129, 4);
            this.txtIPAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(132, 20);
            this.txtIPAddress.TabIndex = 2;
            this.txtIPAddress.Text = "localhost";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(10, 32);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(36, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Port:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(4, 4);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "IP Address:";
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Controls.Add(this.kryptonHeaderGroup1);
            this.kryptonPage2.Controls.Add(this.txtServerName);
            this.kryptonPage2.Controls.Add(this.kryptonLabel3);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.ImageSmall = global::AdvancedScada.Studio.Properties.Resources.Database_16x16;
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(67, 62);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(464, 226);
            this.kryptonPage2.Text = "SQL Server";
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "793F330EF9674ABF09AC888FBC86BEBD";
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 40);
            this.kryptonHeaderGroup1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cboxDatabaseTypes);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(464, 186);
            this.kryptonHeaderGroup1.TabIndex = 2;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "ScreenDatabaseTypes";
            // 
            // cboxDatabaseTypes
            // 
            this.cboxDatabaseTypes.DropDownWidth = 199;
            this.cboxDatabaseTypes.IntegralHeight = false;
            this.cboxDatabaseTypes.Items.AddRange(new object[] {
            "SQLite",
            "SQL"});
            this.cboxDatabaseTypes.Location = new System.Drawing.Point(149, 4);
            this.cboxDatabaseTypes.Margin = new System.Windows.Forms.Padding(4);
            this.cboxDatabaseTypes.Name = "cboxDatabaseTypes";
            this.cboxDatabaseTypes.Size = new System.Drawing.Size(265, 21);
            this.cboxDatabaseTypes.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cboxDatabaseTypes.TabIndex = 1;
            this.cboxDatabaseTypes.Text = "kryptonComboBox2";
            this.cboxDatabaseTypes.SelectedIndexChanged += new System.EventHandler(this.cboxDatabaseTypes_SelectedIndexChanged);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(9, 4);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(99, 20);
            this.kryptonLabel4.TabIndex = 0;
            this.kryptonLabel4.Values.Text = "DatabaseTypes :";
            // 
            // txtServerName
            // 
            this.txtServerName.DropDownWidth = 363;
            this.txtServerName.IntegralHeight = false;
            this.txtServerName.Location = new System.Drawing.Point(147, 5);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(268, 21);
            this.txtServerName.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.txtServerName.TabIndex = 1;
            this.txtServerName.Text = "kryptonComboBox1";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(21, 4);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(87, 20);
            this.kryptonLabel3.TabIndex = 0;
            this.kryptonLabel3.Values.Text = "Server Name :";
            // 
            // kryptonPage3
            // 
            this.kryptonPage3.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage3.Controls.Add(this.btnLibraryImages);
            this.kryptonPage3.Controls.Add(this.txtLibraryImages);
            this.kryptonPage3.Controls.Add(this.kryptonLabel8);
            this.kryptonPage3.Controls.Add(this.btnTypeForms);
            this.kryptonPage3.Controls.Add(this.btnLibraryImage);
            this.kryptonPage3.Controls.Add(this.cboxTypeForms);
            this.kryptonPage3.Controls.Add(this.cboxLibraryImage);
            this.kryptonPage3.Controls.Add(this.kryptonLabel6);
            this.kryptonPage3.Controls.Add(this.kryptonLabel5);
            this.kryptonPage3.Flags = 65534;
            this.kryptonPage3.ImageSmall = global::AdvancedScada.Studio.Properties.Resources.Image_16x16;
            this.kryptonPage3.LastVisibleSet = true;
            this.kryptonPage3.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(67, 62);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Size = new System.Drawing.Size(464, 226);
            this.kryptonPage3.Text = "LibraryImage";
            this.kryptonPage3.ToolTipTitle = "Page ToolTip";
            this.kryptonPage3.UniqueName = "354B65BF2F8B410C0FA2A6BFB73894BC";
            // 
            // btnLibraryImages
            // 
            this.btnLibraryImages.Location = new System.Drawing.Point(406, 72);
            this.btnLibraryImages.Margin = new System.Windows.Forms.Padding(4);
            this.btnLibraryImages.Name = "btnLibraryImages";
            this.btnLibraryImages.Size = new System.Drawing.Size(51, 25);
            this.btnLibraryImages.TabIndex = 11;
            this.btnLibraryImages.Values.Text = "...";
            this.btnLibraryImages.Click += new System.EventHandler(this.BtnLibraryImages_Click);
            // 
            // txtLibraryImages
            // 
            this.txtLibraryImages.Location = new System.Drawing.Point(143, 70);
            this.txtLibraryImages.Margin = new System.Windows.Forms.Padding(4);
            this.txtLibraryImages.Name = "txtLibraryImages";
            this.txtLibraryImages.Size = new System.Drawing.Size(253, 20);
            this.txtLibraryImages.TabIndex = 10;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(11, 68);
            this.kryptonLabel8.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(89, 20);
            this.kryptonLabel8.TabIndex = 9;
            this.kryptonLabel8.Values.Text = "LibraryImages:";
            // 
            // btnTypeForms
            // 
            this.btnTypeForms.Location = new System.Drawing.Point(406, 37);
            this.btnTypeForms.Margin = new System.Windows.Forms.Padding(4);
            this.btnTypeForms.Name = "btnTypeForms";
            this.btnTypeForms.Size = new System.Drawing.Size(51, 25);
            this.btnTypeForms.TabIndex = 5;
            this.btnTypeForms.Values.Text = "...";
            this.btnTypeForms.Click += new System.EventHandler(this.btnTypeForms_Click);
            // 
            // btnLibraryImage
            // 
            this.btnLibraryImage.Location = new System.Drawing.Point(406, 5);
            this.btnLibraryImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnLibraryImage.Name = "btnLibraryImage";
            this.btnLibraryImage.Size = new System.Drawing.Size(51, 25);
            this.btnLibraryImage.TabIndex = 4;
            this.btnLibraryImage.Values.Text = "...";
            this.btnLibraryImage.Click += new System.EventHandler(this.btnLibraryImage_Click);
            // 
            // cboxTypeForms
            // 
            this.cboxTypeForms.Location = new System.Drawing.Point(143, 39);
            this.cboxTypeForms.Margin = new System.Windows.Forms.Padding(4);
            this.cboxTypeForms.Name = "cboxTypeForms";
            this.cboxTypeForms.Size = new System.Drawing.Size(253, 20);
            this.cboxTypeForms.TabIndex = 3;
            // 
            // cboxLibraryImage
            // 
            this.cboxLibraryImage.Location = new System.Drawing.Point(143, 2);
            this.cboxLibraryImage.Margin = new System.Windows.Forms.Padding(4);
            this.cboxLibraryImage.Name = "cboxLibraryImage";
            this.cboxLibraryImage.Size = new System.Drawing.Size(255, 20);
            this.cboxLibraryImage.TabIndex = 2;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(11, 38);
            this.kryptonLabel6.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(59, 20);
            this.kryptonLabel6.TabIndex = 1;
            this.kryptonLabel6.Values.Text = "Symbols:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(11, 6);
            this.kryptonLabel5.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(84, 20);
            this.kryptonLabel5.TabIndex = 0;
            this.kryptonLabel5.Values.Text = "LibraryImage:";
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(357, 280);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 29);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(233, 280);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(124, 29);
            this.btnOK.TabIndex = 2;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormConfiguration
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(466, 309);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.kryptonNavigator1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Configuration";
            this.Load += new System.EventHandler(this.FormConfiguration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            this.kryptonPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            this.kryptonPage2.ResumeLayout(false);
            this.kryptonPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboxDatabaseTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).EndInit();
            this.kryptonPage3.ResumeLayout(false);
            this.kryptonPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage2;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage3;
        private ComponentFactory.Krypton.Toolkit.KryptonDomainUpDown txtPort;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtIPAddress;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboxDatabaseTypes;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox txtServerName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnTypeForms;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnLibraryImage;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox cboxTypeForms;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox cboxLibraryImage;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private System.Windows.Forms.FolderBrowserDialog FBD;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnLibraryImages;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtLibraryImages;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
    }
}