namespace AdvancedScada.ImagePicker
{
    partial class MainView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.xtbImageGallery = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.tbLibraryImages = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonSplitContainer3 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.ListBoxCategoryName = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.pnlPictures = new ComponentFactory.Krypton.Ribbon.KryptonGallery();
            this.ImageContextMenu = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.btnAddImageFiles = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.btnEditorImageFiles = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.btnDeleteImageFiles = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuHeading2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.btnExportPNG = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.btnExportICO = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.btnExportGIF = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.btnExportXaml = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.KR_PNG = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.KR_WMF = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.KR_Xaml = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.KR_SVG = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonContextMenuItem7 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem8 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtbImageGallery)).BeginInit();
            this.xtbImageGallery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLibraryImages)).BeginInit();
            this.tbLibraryImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel1)).BeginInit();
            this.kryptonSplitContainer3.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel2)).BeginInit();
            this.kryptonSplitContainer3.Panel2.SuspendLayout();
            this.kryptonSplitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtbImageGallery
            // 
            this.xtbImageGallery.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.xtbImageGallery.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.xtbImageGallery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtbImageGallery.Location = new System.Drawing.Point(0, 28);
            this.xtbImageGallery.Name = "xtbImageGallery";
            this.xtbImageGallery.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.BarCheckButtonGroupInside;
            this.xtbImageGallery.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.tbLibraryImages});
            this.xtbImageGallery.SelectedIndex = 0;
            this.xtbImageGallery.Size = new System.Drawing.Size(625, 404);
            this.xtbImageGallery.TabIndex = 0;
            this.xtbImageGallery.Text = "kryptonNavigator1";
            // 
            // tbLibraryImages
            // 
            this.tbLibraryImages.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tbLibraryImages.Controls.Add(this.kryptonSplitContainer3);
            this.tbLibraryImages.Flags = 65534;
            this.tbLibraryImages.LastVisibleSet = true;
            this.tbLibraryImages.MinimumSize = new System.Drawing.Size(50, 50);
            this.tbLibraryImages.Name = "tbLibraryImages";
            this.tbLibraryImages.Size = new System.Drawing.Size(623, 372);
            this.tbLibraryImages.Text = "LibraryImages";
            this.tbLibraryImages.ToolTipTitle = "Page ToolTip";
            this.tbLibraryImages.UniqueName = "1f1efc5a79114ed39ce31efe6c611f0b";
            // 
            // kryptonSplitContainer3
            // 
            this.kryptonSplitContainer3.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer3.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainer3.Name = "kryptonSplitContainer3";
            // 
            // kryptonSplitContainer3.Panel1
            // 
            this.kryptonSplitContainer3.Panel1.Controls.Add(this.ListBoxCategoryName);
            // 
            // kryptonSplitContainer3.Panel2
            // 
            this.kryptonSplitContainer3.Panel2.Controls.Add(this.pnlPictures);
            this.kryptonSplitContainer3.Size = new System.Drawing.Size(623, 372);
            this.kryptonSplitContainer3.SplitterDistance = 203;
            this.kryptonSplitContainer3.TabIndex = 1;
            // 
            // ListBoxCategoryName
            // 
            this.ListBoxCategoryName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBoxCategoryName.Location = new System.Drawing.Point(0, 0);
            this.ListBoxCategoryName.Name = "ListBoxCategoryName";
            this.ListBoxCategoryName.Size = new System.Drawing.Size(203, 372);
            this.ListBoxCategoryName.TabIndex = 0;
            this.ListBoxCategoryName.SelectedIndexChanged += new System.EventHandler(this.ListBoxCategoryName_SelectedIndexChanged);
            // 
            // pnlPictures
            // 
            this.pnlPictures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPictures.ImageList = null;
            this.pnlPictures.KryptonContextMenu = this.ImageContextMenu;
            this.pnlPictures.Location = new System.Drawing.Point(0, 0);
            this.pnlPictures.Name = "pnlPictures";
            this.pnlPictures.Size = new System.Drawing.Size(415, 372);
            this.pnlPictures.TabIndex = 0;
            this.pnlPictures.SelectedIndexChanged += new System.EventHandler(this.pnlPictures_SelectedIndexChanged);
            this.pnlPictures.MouseLeave += new System.EventHandler(this.pnlPictures_MouseLeave);
            // 
            // ImageContextMenu
            // 
            this.ImageContextMenu.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading1,
            this.kryptonContextMenuItems1,
            this.kryptonContextMenuHeading2,
            this.kryptonContextMenuItems2});
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            this.kryptonContextMenuHeading1.Text = "Add  Image Files";
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.btnAddImageFiles,
            this.btnEditorImageFiles,
            this.btnDeleteImageFiles});
            // 
            // btnAddImageFiles
            // 
            this.btnAddImageFiles.Image = global::AdvancedScada.ImagePicker.Properties.Resources.Add_16x16;
            this.btnAddImageFiles.Text = "Add Image Files";
            this.btnAddImageFiles.Click += new System.EventHandler(this.BtnAddImageFiles_Click);
            // 
            // btnEditorImageFiles
            // 
            this.btnEditorImageFiles.Image = global::AdvancedScada.ImagePicker.Properties.Resources.EditName_16x16;
            this.btnEditorImageFiles.Text = "Editor Image Files";
            // 
            // btnDeleteImageFiles
            // 
            this.btnDeleteImageFiles.Image = global::AdvancedScada.ImagePicker.Properties.Resources.DeleteList_16x16;
            this.btnDeleteImageFiles.Text = "Delete Image Files";
            // 
            // kryptonContextMenuHeading2
            // 
            this.kryptonContextMenuHeading2.ExtraText = "";
            this.kryptonContextMenuHeading2.Text = "Export Image Files";
            // 
            // kryptonContextMenuItems2
            // 
            this.kryptonContextMenuItems2.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.btnExportPNG,
            this.btnExportICO,
            this.btnExportGIF,
            this.btnExportXaml});
            // 
            // btnExportPNG
            // 
            this.btnExportPNG.Image = global::AdvancedScada.ImagePicker.Properties.Resources.Image_16x16;
            this.btnExportPNG.Text = "PNG";
            this.btnExportPNG.Click += new System.EventHandler(this.BtnExportPNG_Click);
            // 
            // btnExportICO
            // 
            this.btnExportICO.Image = global::AdvancedScada.ImagePicker.Properties.Resources.Image_16x16;
            this.btnExportICO.Text = "Ico";
            this.btnExportICO.Click += new System.EventHandler(this.BtnExportICO_Click);
            // 
            // btnExportGIF
            // 
            this.btnExportGIF.Image = global::AdvancedScada.ImagePicker.Properties.Resources.Image_16x16;
            this.btnExportGIF.Text = "GIF";
            this.btnExportGIF.Click += new System.EventHandler(this.BtnExportGIF_Click);
            // 
            // btnExportXaml
            // 
            this.btnExportXaml.Image = global::AdvancedScada.ImagePicker.Properties.Resources.Watermark_16x16;
            this.btnExportXaml.Text = "Xaml";
            this.btnExportXaml.Click += new System.EventHandler(this.BtnExportXaml_Click);
            // 
            // imageListLarge
            // 
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "star_yellow.png");
            this.imageListLarge.Images.SetKeyName(1, "star_blue.png");
            this.imageListLarge.Images.SetKeyName(2, "star_green.png");
            this.imageListLarge.Images.SetKeyName(3, "star_grey.png");
            this.imageListLarge.Images.SetKeyName(4, "star_red.png");
            this.imageListLarge.Images.SetKeyName(5, "arrow_up_green.png");
            this.imageListLarge.Images.SetKeyName(6, "arrow_down_green.png");
            this.imageListLarge.Images.SetKeyName(7, "arrow_left_green.png");
            this.imageListLarge.Images.SetKeyName(8, "arrow_right_green.png");
            this.imageListLarge.Images.SetKeyName(9, "arrow_up_blue.png");
            this.imageListLarge.Images.SetKeyName(10, "arrow_down_blue.png");
            this.imageListLarge.Images.SetKeyName(11, "arrow_left_blue.png");
            this.imageListLarge.Images.SetKeyName(12, "arrow_right_blue.png");
            this.imageListLarge.Images.SetKeyName(13, "user3.png");
            this.imageListLarge.Images.SetKeyName(14, "user1.png");
            this.imageListLarge.Images.SetKeyName(15, "user2.png");
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(376, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(113, 27);
            this.btnOK.TabIndex = 1;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(489, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 27);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.KR_PNG);
            this.kryptonPanel2.Controls.Add(this.KR_WMF);
            this.kryptonPanel2.Controls.Add(this.KR_Xaml);
            this.kryptonPanel2.Controls.Add(this.KR_SVG);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(625, 28);
            this.kryptonPanel2.TabIndex = 2;
            // 
            // KR_PNG
            // 
            this.KR_PNG.Location = new System.Drawing.Point(553, 5);
            this.KR_PNG.Name = "KR_PNG";
            this.KR_PNG.Size = new System.Drawing.Size(47, 20);
            this.KR_PNG.TabIndex = 3;
            this.KR_PNG.Values.Text = "PNG";
            this.KR_PNG.Click += new System.EventHandler(this.KR_PNG_Click);
            // 
            // KR_WMF
            // 
            this.KR_WMF.Location = new System.Drawing.Point(489, 5);
            this.KR_WMF.Name = "KR_WMF";
            this.KR_WMF.Size = new System.Drawing.Size(51, 20);
            this.KR_WMF.TabIndex = 2;
            this.KR_WMF.Values.Text = "WMF";
            this.KR_WMF.Click += new System.EventHandler(this.KR_WMF_Click);
            // 
            // KR_Xaml
            // 
            this.KR_Xaml.Location = new System.Drawing.Point(421, 5);
            this.KR_Xaml.Name = "KR_Xaml";
            this.KR_Xaml.Size = new System.Drawing.Size(50, 20);
            this.KR_Xaml.TabIndex = 1;
            this.KR_Xaml.Values.Text = "Xaml";
            this.KR_Xaml.Click += new System.EventHandler(this.KR_Xaml_Click);
            // 
            // KR_SVG
            // 
            this.KR_SVG.Checked = true;
            this.KR_SVG.Location = new System.Drawing.Point(353, 3);
            this.KR_SVG.Name = "KR_SVG";
            this.KR_SVG.Size = new System.Drawing.Size(45, 20);
            this.KR_SVG.TabIndex = 0;
            this.KR_SVG.Values.Text = "SVG";
            this.KR_SVG.Click += new System.EventHandler(this.KR_SVG_Click);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnOK);
            this.kryptonPanel1.Controls.Add(this.btnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 432);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(625, 29);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // kryptonContextMenuItem7
            // 
            this.kryptonContextMenuItem7.Text = "Menu Item";
            // 
            // kryptonContextMenuItem8
            // 
            this.kryptonContextMenuItem8.Text = "Menu Item";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 461);
            this.Controls.Add(this.xtbImageGallery);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Picker";
            this.Load += new System.EventHandler(this.MainView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtbImageGallery)).EndInit();
            this.xtbImageGallery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbLibraryImages)).EndInit();
            this.tbLibraryImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel1)).EndInit();
            this.kryptonSplitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel2)).EndInit();
            this.kryptonSplitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3)).EndInit();
            this.kryptonSplitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonNavigator xtbImageGallery;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private System.Windows.Forms.ImageList imageListLarge;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Navigator.KryptonPage tbLibraryImages;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer3;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox ListBoxCategoryName;
        private ComponentFactory.Krypton.Ribbon.KryptonGallery pnlPictures;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu ImageContextMenu;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem btnAddImageFiles;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem btnEditorImageFiles;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem btnDeleteImageFiles;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem btnExportPNG;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem btnExportICO;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem btnExportGIF;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem7;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem8;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem btnExportXaml;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton KR_WMF;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton KR_Xaml;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton KR_SVG;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton KR_PNG;
    }
}

