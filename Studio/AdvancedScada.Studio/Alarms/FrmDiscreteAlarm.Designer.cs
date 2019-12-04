namespace AdvancedScada.Studio.Alarms
{
    partial class FrmDiscreteAlarm
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
            this.DGAlarmAnalog = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.ColName = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColAlarmText = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColAlarmCalss = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColValue = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColTriggerTeg = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColDataBlock = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColDevice = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColChannel = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.AlarmAnalogContext = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.AddAlarm = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.EditorAlarm = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.barButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.barButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.barButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.kryptonHeader2 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.DGAlarmAnalog)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGAlarmAnalog
            // 
            this.DGAlarmAnalog.AllowUserToAddRows = false;
            this.DGAlarmAnalog.AllowUserToDeleteRows = false;
            this.DGAlarmAnalog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColAlarmText,
            this.ColAlarmCalss,
            this.ColValue,
            this.ColTriggerTeg,
            this.ColDataBlock,
            this.ColDevice,
            this.ColChannel});
            this.DGAlarmAnalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGAlarmAnalog.KryptonContextMenu = this.AlarmAnalogContext;
            this.DGAlarmAnalog.Location = new System.Drawing.Point(0, 48);
            this.DGAlarmAnalog.Name = "DGAlarmAnalog";
            this.DGAlarmAnalog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGAlarmAnalog.ShowCellErrors = false;
            this.DGAlarmAnalog.ShowRowErrors = false;
            this.DGAlarmAnalog.Size = new System.Drawing.Size(955, 377);
            this.DGAlarmAnalog.TabIndex = 1;
            // 
            // ColName
            // 
            this.ColName.HeaderText = "Name";
            this.ColName.Name = "ColName";
            this.ColName.Width = 95;
            // 
            // ColAlarmText
            // 
            this.ColAlarmText.HeaderText = "AlarmText";
            this.ColAlarmText.Name = "ColAlarmText";
            this.ColAlarmText.Width = 95;
            // 
            // ColAlarmCalss
            // 
            this.ColAlarmCalss.HeaderText = "AlarmCalss";
            this.ColAlarmCalss.Name = "ColAlarmCalss";
            this.ColAlarmCalss.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColAlarmCalss.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColAlarmCalss.Width = 95;
            // 
            // ColValue
            // 
            this.ColValue.HeaderText = "Value";
            this.ColValue.Name = "ColValue";
            this.ColValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColValue.Width = 95;
            // 
            // ColTriggerTeg
            // 
            this.ColTriggerTeg.HeaderText = "TriggerTeg";
            this.ColTriggerTeg.Name = "ColTriggerTeg";
            this.ColTriggerTeg.Width = 94;
            // 
            // ColDataBlock
            // 
            this.ColDataBlock.HeaderText = "DataBlock";
            this.ColDataBlock.Name = "ColDataBlock";
            this.ColDataBlock.ReadOnly = true;
            this.ColDataBlock.Width = 95;
            // 
            // ColDevice
            // 
            this.ColDevice.HeaderText = "Device";
            this.ColDevice.Name = "ColDevice";
            this.ColDevice.ReadOnly = true;
            this.ColDevice.Width = 95;
            // 
            // ColChannel
            // 
            this.ColChannel.HeaderText = "Channel";
            this.ColChannel.Name = "ColChannel";
            this.ColChannel.ReadOnly = true;
            this.ColChannel.Width = 95;
            // 
            // AlarmAnalogContext
            // 
            this.AlarmAnalogContext.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading1,
            this.kryptonContextMenuItems1});
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            this.kryptonContextMenuHeading1.Image = global::AdvancedScada.Studio.Properties.Resources.AddTag;
            this.kryptonContextMenuHeading1.Text = "Alarms";
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.AddAlarm,
            this.EditorAlarm});
            // 
            // AddAlarm
            // 
            this.AddAlarm.Image = global::AdvancedScada.Studio.Properties.Resources.AddDataBlock;
            this.AddAlarm.Text = "AddAlarm";
            this.AddAlarm.Click += new System.EventHandler(this.AddAlarm_Click);
            // 
            // EditorAlarm
            // 
            this.EditorAlarm.Image = global::AdvancedScada.Studio.Properties.Resources.Edit;
            this.EditorAlarm.Text = "EditorAlarm";
            this.EditorAlarm.Click += new System.EventHandler(this.EditorAlarm_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.barButtonNew,
            this.toolStripSeparator2,
            this.barButtonOpen,
            this.toolStripSeparator3,
            this.barButtonSave,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(955, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // barButtonNew
            // 
            this.barButtonNew.Image = global::AdvancedScada.Studio.Properties.Resources.New;
            this.barButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.barButtonNew.Name = "barButtonNew";
            this.barButtonNew.Size = new System.Drawing.Size(51, 22);
            this.barButtonNew.Text = "New";
            this.barButtonNew.Click += new System.EventHandler(this.barButtonNew_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // barButtonOpen
            // 
            this.barButtonOpen.Image = global::AdvancedScada.Studio.Properties.Resources.Open;
            this.barButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.barButtonOpen.Name = "barButtonOpen";
            this.barButtonOpen.Size = new System.Drawing.Size(56, 22);
            this.barButtonOpen.Text = "Open";
            this.barButtonOpen.Click += new System.EventHandler(this.barButtonOpen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // barButtonSave
            // 
            this.barButtonSave.Image = global::AdvancedScada.Studio.Properties.Resources.Save;
            this.barButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.barButtonSave.Name = "barButtonSave";
            this.barButtonSave.Size = new System.Drawing.Size(51, 22);
            this.barButtonSave.Text = "Save";
            this.barButtonSave.Click += new System.EventHandler(this.barButtonSave_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // kryptonHeader2
            // 
            this.kryptonHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader2.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeader2.Location = new System.Drawing.Point(0, 25);
            this.kryptonHeader2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonHeader2.Name = "kryptonHeader2";
            this.kryptonHeader2.Size = new System.Drawing.Size(955, 23);
            this.kryptonHeader2.TabIndex = 1;
            this.kryptonHeader2.Values.Description = "";
            this.kryptonHeader2.Values.Heading = "AlarmList";
            this.kryptonHeader2.Values.Image = global::AdvancedScada.Studio.Properties.Resources.AddTag;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip2.Location = new System.Drawing.Point(0, 425);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(955, 25);
            this.toolStrip2.TabIndex = 6;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // FrmDiscreteAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 450);
            this.ControlBox = false;
            this.Controls.Add(this.DGAlarmAnalog);
            this.Controls.Add(this.kryptonHeader2);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockActive;
            this.Name = "FrmDiscreteAlarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alarms";
            this.Load += new System.EventHandler(this.FrmDiscreteAlarm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGAlarmAnalog)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView DGAlarmAnalog;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton barButtonNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton barButtonOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton barButtonSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu AlarmAnalogContext;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem AddAlarm;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem EditorAlarm;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColName;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColAlarmText;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColAlarmCalss;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColValue;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColTriggerTeg;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColDataBlock;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColDevice;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColChannel;
    }
}