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
            this.tbImageGallerySVG = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonSplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.cboxListForderSVG = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.gcSVG = new ComponentFactory.Krypton.Ribbon.KryptonGallery();
            this.ImageGallery = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonSplitContainer2 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.cboxListForder = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.gc = new ComponentFactory.Krypton.Ribbon.KryptonGallery();
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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonContextMenuItem7 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem8 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtbImageGallery)).BeginInit();
            this.xtbImageGallery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbImageGallerySVG)).BeginInit();
            this.tbImageGallerySVG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageGallery)).BeginInit();
            this.ImageGallery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2.Panel1)).BeginInit();
            this.kryptonSplitContainer2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2.Panel2)).BeginInit();
            this.kryptonSplitContainer2.Panel2.SuspendLayout();
            this.kryptonSplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLibraryImages)).BeginInit();
            this.tbLibraryImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel1)).BeginInit();
            this.kryptonSplitContainer3.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel2)).BeginInit();
            this.kryptonSplitContainer3.Panel2.SuspendLayout();
            this.kryptonSplitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtbImageGallery
            // 
            this.xtbImageGallery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtbImageGallery.Location = new System.Drawing.Point(0, 28);
            this.xtbImageGallery.Name = "xtbImageGallery";
            this.xtbImageGallery.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.BarCheckButtonGroupInside;
            this.xtbImageGallery.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.tbImageGallerySVG,
            this.ImageGallery,
            this.tbLibraryImages});
            this.xtbImageGallery.SelectedIndex = 0;
            this.xtbImageGallery.Size = new System.Drawing.Size(625, 404);
            this.xtbImageGallery.TabIndex = 0;
            this.xtbImageGallery.Text = "kryptonNavigator1";
            // 
            // tbImageGallerySVG
            // 
            this.tbImageGallerySVG.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tbImageGallerySVG.Controls.Add(this.kryptonSplitContainer1);
            this.tbImageGallerySVG.Flags = 65534;
            this.tbImageGallerySVG.LastVisibleSet = true;
            this.tbImageGallerySVG.MinimumSize = new System.Drawing.Size(50, 50);
            this.tbImageGallerySVG.Name = "tbImageGallerySVG";
            this.tbImageGallerySVG.Size = new System.Drawing.Size(623, 372);
            this.tbImageGallerySVG.Text = "ImageGallerySVG";
            this.tbImageGallerySVG.ToolTipTitle = "Page ToolTip";
            this.tbImageGallerySVG.UniqueName = "6DCCC9F7D5C34697A9AD81F2B17E0A49";
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.cboxListForderSVG);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.gcSVG);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(623, 372);
            this.kryptonSplitContainer1.SplitterDistance = 205;
            this.kryptonSplitContainer1.TabIndex = 0;
            // 
            // cboxListForderSVG
            // 
            this.cboxListForderSVG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboxListForderSVG.Location = new System.Drawing.Point(0, 0);
            this.cboxListForderSVG.Name = "cboxListForderSVG";
            this.cboxListForderSVG.Size = new System.Drawing.Size(205, 372);
            this.cboxListForderSVG.TabIndex = 0;
            this.cboxListForderSVG.SelectedIndexChanged += new System.EventHandler(this.cboxListForderSVG_SelectedIndexChanged);
            // 
            // gcSVG
            // 
            this.gcSVG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSVG.ImageList = null;
            this.gcSVG.Location = new System.Drawing.Point(0, 0);
            this.gcSVG.Name = "gcSVG";
            this.gcSVG.Size = new System.Drawing.Size(413, 372);
            this.gcSVG.TabIndex = 0;
            this.gcSVG.SelectedIndexChanged += new System.EventHandler(this.gcSVG_SelectedIndexChanged);
            this.gcSVG.MouseLeave += new System.EventHandler(this.gcSVG_MouseLeave);
            // 
            // ImageGallery
            // 
            this.ImageGallery.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.ImageGallery.Controls.Add(this.kryptonSplitContainer2);
            this.ImageGallery.Flags = 65534;
            this.ImageGallery.LastVisibleSet = true;
            this.ImageGallery.MinimumSize = new System.Drawing.Size(50, 50);
            this.ImageGallery.Name = "ImageGallery";
            this.ImageGallery.Size = new System.Drawing.Size(623, 372);
            this.ImageGallery.Text = "ImageGallery";
            this.ImageGallery.ToolTipTitle = "Page ToolTip";
            this.ImageGallery.UniqueName = "31450386A7AE41E7C68E45CB6DDCCC1B";
            // 
            // kryptonSplitContainer2
            // 
            this.kryptonSplitContainer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainer2.Name = "kryptonSplitContainer2";
            // 
            // kryptonSplitContainer2.Panel1
            // 
            this.kryptonSplitContainer2.Panel1.Controls.Add(this.cboxListForder);
            // 
            // kryptonSplitContainer2.Panel2
            // 
            this.kryptonSplitContainer2.Panel2.Controls.Add(this.gc);
            this.kryptonSplitContainer2.Size = new System.Drawing.Size(623, 372);
            this.kryptonSplitContainer2.SplitterDistance = 205;
            this.kryptonSplitContainer2.TabIndex = 0;
            // 
            // cboxListForder
            // 
            this.cboxListForder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboxListForder.Location = new System.Drawing.Point(0, 0);
            this.cboxListForder.Name = "cboxListForder";
            this.cboxListForder.Size = new System.Drawing.Size(205, 372);
            this.cboxListForder.TabIndex = 0;
            this.cboxListForder.SelectedIndexChanged += new System.EventHandler(this.cboxListForder_SelectedIndexChanged);
            // 
            // gc
            // 
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.ImageList = null;
            this.gc.Location = new System.Drawing.Point(0, 0);
            this.gc.Name = "gc";
            this.gc.Size = new System.Drawing.Size(413, 372);
            this.gc.TabIndex = 0;
            this.gc.SelectedIndexChanged += new System.EventHandler(this.gc_SelectedIndexChanged);
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
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(625, 28);
            this.kryptonPanel2.TabIndex = 2;
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
            ((System.ComponentModel.ISupportInitialize)(this.tbImageGallerySVG)).EndInit();
            this.tbImageGallerySVG.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImageGallery)).EndInit();
            this.ImageGallery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2.Panel1)).EndInit();
            this.kryptonSplitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2.Panel2)).EndInit();
            this.kryptonSplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2)).EndInit();
            this.kryptonSplitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbLibraryImages)).EndInit();
            this.tbLibraryImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel1)).EndInit();
            this.kryptonSplitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel2)).EndInit();
            this.kryptonSplitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3)).EndInit();
            this.kryptonSplitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonNavigator xtbImageGallery;
        private ComponentFactory.Krypton.Navigator.KryptonPage tbImageGallerySVG;
        private ComponentFactory.Krypton.Navigator.KryptonPage ImageGallery;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private ComponentFactory.Krypton.Ribbon.KryptonGallery gcSVG;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer2;
        private ComponentFactory.Krypton.Ribbon.KryptonGallery gc;
        private System.Windows.Forms.ImageList imageListLarge;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox cboxListForder;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox cboxListForderSVG;
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
    }
}

