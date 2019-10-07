using AdvancedScada.Utils.Net;

namespace AdvancedScada.OPC.Core.Editors
{
    partial class XChannelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XChannelForm));
            this.TabControlOPC = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.tabPageChannel = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.txtDesc = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtChannelName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tbServer = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.HeaderGroupServer = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.remoteServerButton = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.localServerButton = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.button3 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.serverTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.serversComboBox = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonHeaderGroup4 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnBlack = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnNext = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TabControlOPC)).BeginInit();
            this.TabControlOPC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageChannel)).BeginInit();
            this.tabPageChannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbServer)).BeginInit();
            this.tbServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupServer.Panel)).BeginInit();
            this.HeaderGroupServer.Panel.SuspendLayout();
            this.HeaderGroupServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serversComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4.Panel)).BeginInit();
            this.kryptonHeaderGroup4.Panel.SuspendLayout();
            this.kryptonHeaderGroup4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControlOPC
            // 
            this.TabControlOPC.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabControlOPC.Location = new System.Drawing.Point(0, 0);
            this.TabControlOPC.Margin = new System.Windows.Forms.Padding(4);
            this.TabControlOPC.Name = "TabControlOPC";
            this.TabControlOPC.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.HeaderBarCheckButtonGroup;
            this.TabControlOPC.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.tabPageChannel,
            this.tbServer});
            this.TabControlOPC.SelectedIndex = 0;
            this.TabControlOPC.Size = new System.Drawing.Size(500, 273);
            this.TabControlOPC.TabIndex = 2;
            // 
            // tabPageChannel
            // 
            this.tabPageChannel.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tabPageChannel.Controls.Add(this.txtDesc);
            this.tabPageChannel.Controls.Add(this.txtChannelName);
            this.tabPageChannel.Controls.Add(this.kryptonLabel4);
            this.tabPageChannel.Controls.Add(this.kryptonLabel1);
            this.tabPageChannel.Flags = 65534;
            this.tabPageChannel.LastVisibleSet = true;
            this.tabPageChannel.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageChannel.MinimumSize = new System.Drawing.Size(67, 62);
            this.tabPageChannel.Name = "tabPageChannel";
            this.tabPageChannel.Size = new System.Drawing.Size(498, 240);
            this.tabPageChannel.Text = "PageChannel";
            this.tabPageChannel.ToolTipTitle = "Page ToolTip";
            this.tabPageChannel.UniqueName = "5867A4647CE64D017D90BC6F521C74C2";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(121, 56);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(370, 145);
            this.txtDesc.TabIndex = 23;
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(121, 21);
            this.txtChannelName.Margin = new System.Windows.Forms.Padding(4);
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.Size = new System.Drawing.Size(247, 20);
            this.txtChannelName.TabIndex = 20;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(16, 59);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel4.TabIndex = 19;
            this.kryptonLabel4.Values.Text = "Description:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(16, 16);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(94, 20);
            this.kryptonLabel1.TabIndex = 16;
            this.kryptonLabel1.Values.Text = "Channel Name:";
            // 
            // tbServer
            // 
            this.tbServer.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tbServer.Controls.Add(this.HeaderGroupServer);
            this.tbServer.Flags = 65534;
            this.tbServer.LastVisibleSet = true;
            this.tbServer.Margin = new System.Windows.Forms.Padding(4);
            this.tbServer.MinimumSize = new System.Drawing.Size(67, 62);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(498, 240);
            this.tbServer.Text = "Server";
            this.tbServer.ToolTipTitle = "Page ToolTip";
            this.tbServer.UniqueName = "E49C0033D0E049C8FC8960990408E927";
            // 
            // HeaderGroupServer
            // 
            this.HeaderGroupServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderGroupServer.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.HeaderGroupServer.Location = new System.Drawing.Point(0, 0);
            this.HeaderGroupServer.Margin = new System.Windows.Forms.Padding(4);
            this.HeaderGroupServer.Name = "HeaderGroupServer";
            // 
            // HeaderGroupServer.Panel
            // 
            this.HeaderGroupServer.Panel.Controls.Add(this.remoteServerButton);
            this.HeaderGroupServer.Panel.Controls.Add(this.localServerButton);
            this.HeaderGroupServer.Panel.Controls.Add(this.button3);
            this.HeaderGroupServer.Panel.Controls.Add(this.serverTextBox);
            this.HeaderGroupServer.Panel.Controls.Add(this.serversComboBox);
            this.HeaderGroupServer.Size = new System.Drawing.Size(498, 240);
            this.HeaderGroupServer.TabIndex = 1;
            this.HeaderGroupServer.ValuesPrimary.Heading = "Server";
            this.HeaderGroupServer.ValuesPrimary.Image = global::AdvancedScada.OPC.Core.Properties.Resources.AddChannel;
            this.HeaderGroupServer.ValuesSecondary.Heading = "";
            // 
            // remoteServerButton
            // 
            this.remoteServerButton.Location = new System.Drawing.Point(77, 9);
            this.remoteServerButton.Name = "remoteServerButton";
            this.remoteServerButton.Size = new System.Drawing.Size(65, 20);
            this.remoteServerButton.TabIndex = 25;
            this.remoteServerButton.Values.Text = "Remote";
            // 
            // localServerButton
            // 
            this.localServerButton.Location = new System.Drawing.Point(10, 9);
            this.localServerButton.Name = "localServerButton";
            this.localServerButton.Size = new System.Drawing.Size(51, 20);
            this.localServerButton.TabIndex = 24;
            this.localServerButton.Values.Text = "Local";
            // 
            // button3
            // 
            this.button3.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Alternate;
            this.button3.Location = new System.Drawing.Point(396, 33);
            this.button3.Name = "button3";
            this.button3.Orientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.button3.Size = new System.Drawing.Size(27, 30);
            this.button3.TabIndex = 23;
            this.button3.Values.Image = global::AdvancedScada.OPC.Core.Properties.Resources.refresh;
            this.button3.Values.ImageStates.ImageCheckedNormal = null;
            this.button3.Values.ImageStates.ImageCheckedPressed = null;
            this.button3.Values.ImageStates.ImageCheckedTracking = null;
            this.button3.Values.ImageStates.ImageNormal = global::AdvancedScada.OPC.Core.Properties.Resources.refresh;
            this.button3.Values.Text = "";
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // serverTextBox
            // 
            this.serverTextBox.Location = new System.Drawing.Point(165, 9);
            this.serverTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(247, 20);
            this.serverTextBox.TabIndex = 22;
            this.serverTextBox.Text = "opcda://localhost";
            // 
            // serversComboBox
            // 
            this.serversComboBox.DropDownWidth = 163;
            this.serversComboBox.IntegralHeight = false;
            this.serversComboBox.Location = new System.Drawing.Point(10, 37);
            this.serversComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.serversComboBox.Name = "serversComboBox";
            this.serversComboBox.Size = new System.Drawing.Size(379, 21);
            this.serversComboBox.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.serversComboBox.TabIndex = 6;
            this.serversComboBox.Text = " ";
            // 
            // kryptonHeaderGroup4
            // 
            this.kryptonHeaderGroup4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup4.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup4.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup4.Location = new System.Drawing.Point(0, 273);
            this.kryptonHeaderGroup4.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonHeaderGroup4.Name = "kryptonHeaderGroup4";
            // 
            // kryptonHeaderGroup4.Panel
            // 
            this.kryptonHeaderGroup4.Panel.Controls.Add(this.btnCancel);
            this.kryptonHeaderGroup4.Panel.Controls.Add(this.btnBlack);
            this.kryptonHeaderGroup4.Panel.Controls.Add(this.btnNext);
            this.kryptonHeaderGroup4.Size = new System.Drawing.Size(500, 54);
            this.kryptonHeaderGroup4.TabIndex = 6;
            this.kryptonHeaderGroup4.ValuesPrimary.Image = global::AdvancedScada.OPC.Core.Properties.Resources.AddChannel;
            this.kryptonHeaderGroup4.ValuesSecondary.Heading = "";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(138, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBlack
            // 
            this.btnBlack.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBlack.Location = new System.Drawing.Point(258, 0);
            this.btnBlack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(120, 30);
            this.btnBlack.TabIndex = 4;
            this.btnBlack.Values.Text = "< Back";
            this.btnBlack.Click += new System.EventHandler(this.btnBlack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.Location = new System.Drawing.Point(378, 0);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(120, 30);
            this.btnNext.TabIndex = 3;
            this.btnNext.Values.Text = "Next >";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // XChannelForm
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(500, 327);
            this.Controls.Add(this.kryptonHeaderGroup4);
            this.Controls.Add(this.TabControlOPC);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(516, 365);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(516, 365);
            this.Name = "XChannelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XChannelForm - (Administrator)";
            this.Load += new System.EventHandler(this.XChannelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TabControlOPC)).EndInit();
            this.TabControlOPC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabPageChannel)).EndInit();
            this.tabPageChannel.ResumeLayout(false);
            this.tabPageChannel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbServer)).EndInit();
            this.tbServer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupServer.Panel)).EndInit();
            this.HeaderGroupServer.Panel.ResumeLayout(false);
            this.HeaderGroupServer.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroupServer)).EndInit();
            this.HeaderGroupServer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.serversComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4.Panel)).EndInit();
            this.kryptonHeaderGroup4.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup4)).EndInit();
            this.kryptonHeaderGroup4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonNavigator TabControlOPC;
        private ComponentFactory.Krypton.Navigator.KryptonPage tabPageChannel;
        private ComponentFactory.Krypton.Navigator.KryptonPage tbServer;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDesc;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtChannelName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup HeaderGroupServer;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox serversComboBox;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnBlack;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnNext;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton remoteServerButton;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton localServerButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton button3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox serverTextBox;
    }
}