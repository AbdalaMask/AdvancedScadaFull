namespace AdvancedScada.Studio.Alarms
{
    partial class FrmAlarmAnalog
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
            ComponentFactory.Krypton.Toolkit.IconSpec iconSpec2 = new ComponentFactory.Krypton.Toolkit.IconSpec();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlarmAnalog));
            this.DGAlarmAnalog = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny2 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.ColName = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColAlarmText = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColAlarmCalss = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn();
            this.ColLimitMode = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn();
            this.ColLimitValue = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn();
            this.ColTriggerTeg = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColDataBlock = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColDevice = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColChannel = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGAlarmAnalog)).BeginInit();
            this.SuspendLayout();
            // 
            // DGAlarmAnalog
            // 
            this.DGAlarmAnalog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGAlarmAnalog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColAlarmText,
            this.ColAlarmCalss,
            this.ColLimitMode,
            this.ColLimitValue,
            this.ColTriggerTeg,
            this.ColDataBlock,
            this.ColDevice,
            this.ColChannel});
            this.DGAlarmAnalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGAlarmAnalog.Location = new System.Drawing.Point(0, 0);
            this.DGAlarmAnalog.Name = "DGAlarmAnalog";
            this.DGAlarmAnalog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGAlarmAnalog.ShowCellErrors = false;
            this.DGAlarmAnalog.ShowRowErrors = false;
            this.DGAlarmAnalog.Size = new System.Drawing.Size(1116, 487);
            this.DGAlarmAnalog.TabIndex = 0;
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.FixedRight;
            this.buttonSpecAny1.UniqueName = "548d9b927c914d939ad6730cc75af63a";
            // 
            // buttonSpecAny2
            // 
            this.buttonSpecAny2.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.Next;
            this.buttonSpecAny2.UniqueName = "9b9a47497ce143a09f63854fbad9bd85";
            // 
            // ColName
            // 
            this.ColName.FillWeight = 120F;
            this.ColName.HeaderText = "Name";
            this.ColName.Name = "ColName";
            this.ColName.Width = 100;
            // 
            // ColAlarmText
            // 
            this.ColAlarmText.FillWeight = 300F;
            this.ColAlarmText.HeaderText = "Alarm Text";
            this.ColAlarmText.Name = "ColAlarmText";
            this.ColAlarmText.Width = 100;
            // 
            // ColAlarmCalss
            // 
            this.ColAlarmCalss.DataSource = null;
            this.ColAlarmCalss.DropDownWidth = 121;
            this.ColAlarmCalss.HeaderText = "Alarm Calss";
            this.ColAlarmCalss.Name = "ColAlarmCalss";
            this.ColAlarmCalss.Width = 100;
            // 
            // ColLimitMode
            // 
            this.ColLimitMode.DataSource = null;
            this.ColLimitMode.DropDownWidth = 121;
            this.ColLimitMode.HeaderText = "Limit Mode";
            this.ColLimitMode.Name = "ColLimitMode";
            this.ColLimitMode.Width = 100;
            // 
            // ColLimitValue
            // 
            this.ColLimitValue.DataSource = null;
            this.ColLimitValue.DropDownWidth = 121;
            this.ColLimitValue.HeaderText = "Limit Value";
            this.ColLimitValue.Name = "ColLimitValue";
            this.ColLimitValue.Width = 100;
            // 
            // ColTriggerTeg
            // 
            this.ColTriggerTeg.HeaderText = "Trigger Teg";
            iconSpec2.Alignment = ComponentFactory.Krypton.Toolkit.IconSpec.IconAlignment.Right;
            iconSpec2.Icon = ((System.Drawing.Image)(resources.GetObject("iconSpec2.Icon")));
            this.ColTriggerTeg.IconSpecs.Add(iconSpec2);
            this.ColTriggerTeg.Name = "ColTriggerTeg";
            this.ColTriggerTeg.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColTriggerTeg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColTriggerTeg.Width = 96;
            // 
            // ColDataBlock
            // 
            this.ColDataBlock.HeaderText = "DataBlock";
            this.ColDataBlock.Name = "ColDataBlock";
            this.ColDataBlock.Width = 100;
            // 
            // ColDevice
            // 
            this.ColDevice.HeaderText = "Device";
            this.ColDevice.Name = "ColDevice";
            this.ColDevice.Width = 100;
            // 
            // ColChannel
            // 
            this.ColChannel.HeaderText = "Channel";
            this.ColChannel.Name = "ColChannel";
            this.ColChannel.Width = 100;
            // 
            // FrmAlarmAnalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 487);
            this.Controls.Add(this.DGAlarmAnalog);
            this.Name = "FrmAlarmAnalog";
            this.Text = "FrmAlarmAnalog";
            ((System.ComponentModel.ISupportInitialize)(this.DGAlarmAnalog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView DGAlarmAnalog;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny2;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColName;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColAlarmText;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn ColAlarmCalss;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn ColLimitMode;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn ColLimitValue;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColTriggerTeg;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColDataBlock;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColDevice;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColChannel;
    }
}