/*
 * Created by SharpDevelop.
 * User: User
 * Date: 27/09/2019
 * Time: 02:33 م
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AdvancedScada.XamlToCode
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnConvert;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.TextBox txtXAML;
		private System.Windows.Forms.TextBox txtCode;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtXAML = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.tbXamlToCode = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPage2 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.btnConvert = new System.Windows.Forms.ToolStripButton();
            this.tsUserControl = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBake = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsClear = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbXamlToCode)).BeginInit();
            this.tbXamlToCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            this.kryptonPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConvert,
            this.toolStripSeparator1,
            this.tsUserControl,
            this.toolStripButton1,
            this.tsBake,
            this.toolStripSeparator2,
            this.tsClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(761, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 410);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(761, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtXAML
            // 
            this.txtXAML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtXAML.Location = new System.Drawing.Point(0, 0);
            this.txtXAML.Multiline = true;
            this.txtXAML.Name = "txtXAML";
            this.txtXAML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtXAML.Size = new System.Drawing.Size(761, 385);
            this.txtXAML.TabIndex = 0;
            // 
            // txtCode
            // 
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Location = new System.Drawing.Point(0, 0);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(761, 385);
            this.txtCode.TabIndex = 0;
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2010Silver;
            // 
            // tbXamlToCode
            // 
            this.tbXamlToCode.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.Context;
            this.tbXamlToCode.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.RemovePageAndDispose;
            this.tbXamlToCode.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Logic;
            this.tbXamlToCode.Button.ContextButtonAction = ComponentFactory.Krypton.Navigator.ContextButtonAction.SelectPage;
            this.tbXamlToCode.Button.ContextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Logic;
            this.tbXamlToCode.Button.ContextMenuMapImage = ComponentFactory.Krypton.Navigator.MapKryptonPageImage.Small;
            this.tbXamlToCode.Button.ContextMenuMapText = ComponentFactory.Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.tbXamlToCode.Button.NextButtonAction = ComponentFactory.Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.tbXamlToCode.Button.NextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Logic;
            this.tbXamlToCode.Button.PreviousButtonAction = ComponentFactory.Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.tbXamlToCode.Button.PreviousButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Logic;
            this.tbXamlToCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbXamlToCode.Group.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.tbXamlToCode.Group.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.tbXamlToCode.Header.HeaderStyleBar = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.tbXamlToCode.Header.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.tbXamlToCode.Header.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.tbXamlToCode.Location = new System.Drawing.Point(0, 25);
            this.tbXamlToCode.Name = "tbXamlToCode";
            this.tbXamlToCode.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.Panel;
            this.tbXamlToCode.PageBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.tbXamlToCode.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2});
            this.tbXamlToCode.Panel.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.tbXamlToCode.SelectedIndex = 0;
            this.tbXamlToCode.Size = new System.Drawing.Size(761, 385);
            this.tbXamlToCode.TabIndex = 3;
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.txtXAML);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(761, 385);
            this.kryptonPage1.Text = "kryptonPage1";
            this.kryptonPage1.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "8b23d0da52ec4a43a480a9d14487848d";
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Controls.Add(this.txtCode);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(761, 385);
            this.kryptonPage2.Text = "kryptonPage2";
            this.kryptonPage2.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "73546f2540fd4da8ba5c8930ad11e1ac";
            // 
            // btnConvert
            // 
            this.btnConvert.Image = ((System.Drawing.Image)(resources.GetObject("btnConvert.Image")));
            this.btnConvert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(69, 22);
            this.btnConvert.Text = "Convert";
            this.btnConvert.Click += new System.EventHandler(this.BtnConvertClick);
            // 
            // tsUserControl
            // 
            this.tsUserControl.Image = global::AdvancedScada.XamlToCode.Properties.Resources._this;
            this.tsUserControl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUserControl.Name = "tsUserControl";
            this.tsUserControl.Size = new System.Drawing.Size(90, 22);
            this.tsUserControl.Text = "UserControl";
            this.tsUserControl.Click += new System.EventHandler(this.TsUserControl_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBake
            // 
            this.tsBake.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBake.Image = global::AdvancedScada.XamlToCode.Properties.Resources.New;
            this.tsBake.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBake.Name = "tsBake";
            this.tsBake.Size = new System.Drawing.Size(23, 22);
            this.tsBake.Text = "Beka";
            this.tsBake.Click += new System.EventHandler(this.TsBake_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsClear
            // 
            this.tsClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsClear.Image = global::AdvancedScada.XamlToCode.Properties.Resources.Exit;
            this.tsClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsClear.Name = "tsClear";
            this.tsClear.Size = new System.Drawing.Size(23, 22);
            this.tsClear.Text = "Clear";
            this.tsClear.Click += new System.EventHandler(this.TsClear_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 432);
            this.Controls.Add(this.tbXamlToCode);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XamlToCode";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbXamlToCode)).EndInit();
            this.tbXamlToCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            this.kryptonPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            this.kryptonPage2.ResumeLayout(false);
            this.kryptonPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsUserControl;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator tbXamlToCode;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage2;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsBake;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsClear;
    }
}
