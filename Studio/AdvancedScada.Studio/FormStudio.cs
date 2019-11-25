using AdvancedScada.Common;
using AdvancedScada.ImagePicker;
using AdvancedScada.Management.BLManager;
using AdvancedScada.Studio.Alarms;
using AdvancedScada.Studio.Config;
using AdvancedScada.Studio.DB;
using AdvancedScada.Studio.DB.SQL;
using AdvancedScada.Studio.DB.SQLite;
using AdvancedScada.Studio.Editors;
using AdvancedScada.Studio.LinkToSQL;
using AdvancedScada.Studio.Logging;
using AdvancedScada.Studio.Monitor;
using AdvancedScada.Studio.Properties;
using AdvancedScada.Studio.Service;
using AdvancedScada.Studio.Tools;
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace AdvancedScada.Studio
{
    public partial class FormStudio : KryptonForm
    {
        #region MyRegion



        public bool IsDataChanged;
        private delegate void SetLabelTextInvoker(TextBox label, string Text);
        public static void SetLabelText(TextBox Label, string Text)
        {
            if (Label.InvokeRequired)
                Label.Invoke(new SetLabelTextInvoker(SetLabelText), Label, Text);
            else
                Label.Text += Text;
            Application.DoEvents();
        }
        #endregion

        public FormStudio()
        {
            InitializeComponent();
            if (Settings.Default.ApplicationSkinName != string.Empty && Settings.Default.ApplicationSkinName != null)
            {
                var cpu = (PaletteModeManager)Enum.Parse(typeof(PaletteModeManager), Settings.Default["ApplicationSkinName"].ToString());
                navigatorOutlook.DismissPopups();
                kryptonManager1.GlobalPaletteMode = cpu;
            }

        }
        #region bar
        string SkinName;
        private void kryptonRibbonGroupGallery1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (kryptonRibbonGroupGallery1.SelectedIndex)
            {
                case 0:
                    navigatorOutlook.DismissPopups();
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
                    break;
                case 1:
                    navigatorOutlook.DismissPopups();
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Black;
                    break;
                case 2:
                    navigatorOutlook.DismissPopups();
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
                    break;
                case 3:
                    navigatorOutlook.DismissPopups();
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Black;
                    break;
                case 4:
                    navigatorOutlook.DismissPopups();
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                    break;
                case 5:
                    navigatorOutlook.DismissPopups();
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
                    break;
                default:
                    break;
            }
            SkinName = kryptonManager1.GlobalPaletteMode.ToString();
        }

        private void barCheckEnabele_Click(object sender, EventArgs e)
        {

        }

        private void mSQLServerUtils_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FormSQLServerUtils))
                {
                    form.Activate();
                    return;
                }
            }
            KryptonPage page = NewPage("SQLServerUtils ", 0, new FormSQLServerUtils());
            TabForm.Pages.Add(page);
        }

        private void mConfiguration_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
                if (form.GetType() == typeof(FormConfiguration))
                {
                    form.Activate();
                    return;
                }
            var child = new FormConfiguration();
            child.Show();
        }

        private void mPCControllercs_Click(object sender, EventArgs e)
        {

        }

        private void barButtonLibraryImages_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new MainView();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("File not found", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnMonioring_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
                if (form.GetType() == typeof(PLC_MonitorForm))
                {
                    form.Activate();
                    return;
                }
            KryptonPage page = NewPage("MonitorForm", 0, new PLC_MonitorForm());
            TabForm.Pages.Add(page);
        }
        private void mExit_Click(object sender, EventArgs e)
        {
            if (SkinName != string.Empty && SkinName != null)
                Settings.Default["ApplicationSkinName"] = SkinName;
            Settings.Default.Save();
            Application.ExitThread();
        }

        #endregion

        #region Navigator


        private void buttonSpecExpandCollapse_Click(object sender, EventArgs e)
        {
            // Are we currently showing fully expanded?
            if (navigatorOutlook.NavigatorMode == NavigatorMode.OutlookFull)
            {
                // Switch to mini mode and reverse direction of arrow
                navigatorOutlook.NavigatorMode = NavigatorMode.OutlookMini;
                buttonSpecExpandCollapse.TypeRestricted = PaletteNavButtonSpecStyle.ArrowRight;
            }
            else
            {
                // Switch to full mode and reverse direction of arrow
                navigatorOutlook.NavigatorMode = NavigatorMode.OutlookFull;
                buttonSpecExpandCollapse.TypeRestricted = PaletteNavButtonSpecStyle.ArrowLeft;
            }
        }

        private void ServiceItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FormServerUtils))
                {
                    form.Activate();

                    return;
                }
            }
            KryptonPage page = NewPage("ServerUtils", 0, new FormServerUtils());
            TabForm.Pages.Add(page);

        }

        private void LoggingItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(XtraFormLogging))
                {
                    form.Activate();
                    return;
                }
            }
            KryptonPage page = NewPage("FormLogging ", 0, new XtraFormLogging());
            TabForm.Pages.Add(page);
        }

        private void TagManagerItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(XTagManager))
                {
                    form.Activate();
                    return;
                }
            }
            KryptonPage page = NewPage("TagManager", 0, new XTagManager());
            TabForm.Pages.Add(page);
        }

        private void SQLManagerItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
                if (form.GetType() == typeof(XSQLMaster))
                {
                    form.Activate();
                    return;
                }
            KryptonPage page = NewPage("SQLMaster ", 0, new XSQLMaster());
            TabForm.Pages.Add(page);

        }

        private void SQLItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(SQLFormCreate))
                {
                    form.Activate();
                    return;
                }
            }
            KryptonPage page = NewPage("SQL ", 0, new SQLFormCreate());
            TabForm.Pages.Add(page);
        }

        private void SQLiteItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(SQLiteFormCreate))
                {
                    form.Activate();
                    return;
                }
            }
            KryptonPage page = NewPage("SQLite ", 0, new SQLiteFormCreate());
            TabForm.Pages.Add(page);
        }
        #endregion

        #region KryptonPage
        private int _count = 1;
        private KryptonPage NewInput()
        {
            return NewPages("Input ", 0, new ContentHistory());
        }
        private KryptonPage NewPages(string name, int image, System.Windows.Forms.Control content)
        {
            // Create new page with title and image
            KryptonPage p = new KryptonPage();
            p.Text = name + _count.ToString();
            p.TextTitle = name + _count.ToString();
            p.TextDescription = name + _count.ToString();
            p.ImageSmall = imageListSmall.Images[image];

            // Add the control for display inside the page
            content.Dock = DockStyle.Fill;
            p.Controls.Add(content);

            _count++;
            return p;
        }

        private KryptonPage NewPage(string name, int image, System.Windows.Forms.Control content)
        {

            // Create new page with title and image
            KryptonPage p = new KryptonPage();
            p.Text = name;
            p.TextTitle = name;
            p.TextDescription = name;
            p.ImageSmall = imageListSmall.Images[image];

            // Add the control for display inside the page
            KryptonForm contentDoc = (KryptonForm)content;
            contentDoc.Dock = DockStyle.Fill;
            contentDoc.TopLevel = false;
            contentDoc.Show();
            p.Controls.Add(contentDoc);



            return p;
        }
        #endregion


        #region Form
        private void FormStudio_Load(object sender, EventArgs e)
        {
            Left = SystemInformation.WorkingArea.Size.Width - Size.Width;
            Top = SystemInformation.WorkingArea.Size.Height - Size.Height;
            CheckEnabele.Checked = Settings.Default.CheckEnabele;

            Registry.SetValue("HKEY_CURRENT_USER\\Software\\FormConfiguration", "IPAddress", "localhost");
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\FormConfiguration", "Port", "8080");

            XCollection.EventscadaLogger += (_Id, _logType, _time, _message) =>
            {
                Logger logger = new Logger { ID = Logger.Loggers.Count + 1, LogType = _logType, TIME = _time, MESSAGE = _message };
                Logger.Loggers.Add(logger);
            };

            // Setup docking functionality
            KryptonDockingWorkspace w = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
            kryptonDockingManager.ManageControl(panelFill, w);
            kryptonDockingManager.ManageFloating(this);

            // Add initial docking pages
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Bottom, new KryptonPage[] { NewInput() });



            ThisNotificationPopup.TitleText = "سكادا";
            ThisNotificationPopup.ContentText = "برمجة عبداللة الصاوى";
            ThisNotificationPopup.IsRightToLeft = false;

            ThisNotificationPopup.Popup();

            btnFormMain_Click();
            var objChannelManager = ChannelService.GetChannelManager();
            var xmlFile = objChannelManager.ReadKey(objChannelManager.XML_NAME_DEFAULT);
            if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile)) return;
            var chList = objChannelManager.GetChannels(xmlFile);
            if (chList.Count < 1) return;
            ServiceItem.PerformClick();
        }
        private void FormStudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) WindowState = FormWindowState.Minimized;
            notifyIcon1.Visible = true;
            e.Cancel = true;
            if (SkinName != string.Empty && SkinName != null)
                Settings.Default["ApplicationSkinName"] = SkinName;

        }
        #endregion



        #region popupMenuNotifyIcon
        private void ItemExit_Click(object sender, EventArgs e)
        {
            var rs = MessageBox.Show(this, "You sure you want to save?", "MSG_QUESTION", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);
            if (SkinName != string.Empty)
                Settings.Default["ApplicationSkinName"] = SkinName;
            Settings.Default.Save();
            if (rs == DialogResult.Yes) Application.ExitThread();
        }

        private void ItemView_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }





        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && IsHandleCreated)
                popupMenuNotifyIcon.Show(MousePosition);
        }

        private void FormStudio_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Maximized;
        }

        #endregion
        private void TabForm_CloseAction(object sender, CloseActionEventArgs e)
        {
            if (e.Item.Text == "MonitorForm")
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(PLC_MonitorForm))
                    {
                        form.Close();
                        return;
                    }
                }
            }
            else if (e.Item.Text == "ServerUtils")
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(FormServerUtils))
                    {

                        Application.ExitThread();
                    }
                }
            }


        }

        private void CheckEnabele_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckEnabele_Click(object sender, EventArgs e)
        {
            if (Settings.Default.CheckEnabele == false)
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "AdvancedScada.Studio",
                    Application.ExecutablePath);
                Settings.Default.CheckEnabele = true;
                Settings.Default.Save();
            }
            else
            {
                Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true)
                    .DeleteValue("AdvancedScada.Studio", false);
                Settings.Default.CheckEnabele = false;
                Settings.Default.Save();
            }
        }

        private void btnDiscreteAlarms_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FrmDiscreteAlarm))
                {
                    form.Activate();
                    return;
                }
            }
            KryptonPage page = NewPage("DiscreteAlarm ", 0, new FrmDiscreteAlarm());
            TabForm.Pages.Add(page);
        }
        private void btnFormMain_Click()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FormMain))
                {
                    form.Activate();
                    return;
                }
            }
            KryptonPage page = NewPage("FormMain ", 0, new FormMain());
            TabForm.Pages.Add(page);
        }
        private void btnAlarmAnalog_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FrmAddAlarm))
                {
                    form.Activate();
                    return;
                }
            }
            KryptonPage page = NewPage("AlarmAnalog ", 0, new FrmAddAlarm());
            TabForm.Pages.Add(page);
        }

        private void btnAlarmclasses_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FrmAlarmClasses))
                {
                    form.Activate();
                    return;
                }
            }
            KryptonPage page = NewPage("AlarmClasses ", 0, new FrmAlarmClasses());
            TabForm.Pages.Add(page);
        }
    }
}
