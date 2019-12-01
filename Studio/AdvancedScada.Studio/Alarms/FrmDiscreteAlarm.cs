using AdvancedScada.Management.AlarmManager;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Windows.Forms;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Studio.Alarms
{
    public partial class FrmDiscreteAlarm : KryptonForm
    {
        public bool IsDataChanged;
        public AlarmManagers objAlarmManager;
        public FrmDiscreteAlarm()
        {
            InitializeComponent();
        }
        private void InitializeData(string xmlPath)
        {
            objAlarmManager.Alarms.Clear();
            objAlarmManager.XmlPath = xmlPath;
            var chList = objAlarmManager.GetAlarms(xmlPath);
          
          
                DGAlarmAnalog.Rows.Clear();
                foreach (var tg in chList)
                {
                    string[] row = { tg.Name, string.Format("{0}", tg.AlarmText), string.Format("{0}", tg.AlarmCalss), tg.Value, tg.TriggerTeg, tg.DataBlock, tg.Device, tg.Channel };

                    DGAlarmAnalog.Rows.Add(row);
                }
            
        }
        private void FrmDiscreteAlarm_Load(object sender, System.EventArgs e)
        {
            objAlarmManager = AlarmManagers.GetAlarmManager();
            var xmlFile = objAlarmManager.ReadKey(AlarmManagers.XML_NAME_DEFAULT);
            if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile)) return;
            InitializeData(xmlFile);
        }

        private void barButtonNew_Click(object sender, System.EventArgs e)
        {
            try
            {


                var saveFileDialog = new SaveFileDialog { Filter = "Xml Files (*.xml)|*.xml|All files (*.*)|*.*", FileName = "XML_NAME_DEFAULT" };
                var dr = saveFileDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var xmlPath = saveFileDialog.FileName;
                    objAlarmManager.CreatFile(xmlPath);


                    objAlarmManager.Alarms.Clear();
                    IsDataChanged = true;
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void barButtonOpen_Click(object sender, System.EventArgs e)
        {
            try
            {
                //ItemSQLServer.Enabled = true;

                var openFileDialog = new OpenFileDialog { Filter = "Xml Files (*.xml)|*.xml|All files (*.*)|*.*", FileName = "config" };
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    
                    objAlarmManager.Alarms.Clear();
                    objAlarmManager.XmlPath = openFileDialog.FileName;
                    InitializeData(openFileDialog.FileName);
                    IsDataChanged = true;
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void barButtonSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                objAlarmManager.Save(objAlarmManager.XmlPath);
                MessageBox.Show(this, "Data saved successfully!", "INFORMATION", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                IsDataChanged = false;
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void AddAlarm_Click(object sender, EventArgs e)
        {




            var tgFrm = new FrmAddAlarm();
            tgFrm.eventAlarmChanged += (Ar, isNew) =>
            {
                objAlarmManager.AddAlarm(Ar);

                string[] row = { Ar.Name, string.Format("{0}", Ar.AlarmText), string.Format("{0}", Ar.AlarmCalss), Ar.Value, Ar.TriggerTeg, Ar.DataBlock, Ar.Device, Ar.Channel };

                DGAlarmAnalog.Rows.Add(row);
                IsDataChanged = true;
            };
            tgFrm.StartPosition = FormStartPosition.CenterScreen;
            tgFrm.ShowDialog();


        }

        private void EditorAlarm_Click(object sender, EventArgs e)
        {
            if (DGAlarmAnalog.SelectedRows.Count == 1)
            {
                string tgName = (string)DGAlarmAnalog.SelectedRows[0].Cells[0].Value;
                var tgCurrent = objAlarmManager.GetByAlarmName(tgName);

                var tgFrm = new FrmAddAlarm(tgCurrent);
                tgFrm.eventAlarmChanged += (Ar, isNew) =>
                {
                    objAlarmManager.UpdateAlarm(Ar);
                    DGAlarmAnalog.Rows.Clear();
                    foreach (var tg in objAlarmManager.Alarms)
                    {
                        string[] row = { tg.Name, string.Format("{0}", tg.AlarmText), string.Format("{0}", tg.AlarmCalss), tg.Value, tg.TriggerTeg, tg.DataBlock, tg.Device, tg.Channel };

                        DGAlarmAnalog.Rows.Add(row);
                    }
                    IsDataChanged = true;
                };
                tgFrm.StartPosition = FormStartPosition.CenterScreen;
                tgFrm.ShowDialog();

            }
        }
    }
}
