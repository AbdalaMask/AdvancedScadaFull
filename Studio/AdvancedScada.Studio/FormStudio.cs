﻿using AdvancedScada.BaseService;
using AdvancedScada.Common;
using AdvancedScada.ImagePicker;
using AdvancedScada.Management.BLManager;
using AdvancedScada.Studio.Alarms;
using AdvancedScada.Studio.Config;
using AdvancedScada.Studio.DB;
using AdvancedScada.Studio.Editors;
using AdvancedScada.Studio.LinkToSQL;
using AdvancedScada.Studio.Monitor;
using AdvancedScada.Studio.Properties;
using AdvancedScada.Studio.Tools;
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Windows.Forms;
using static AdvancedScada.Common.XCollection;
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
            {
                Label.Invoke(new SetLabelTextInvoker(SetLabelText), Label, Text);
            }
            else
            {
                Label.Text += Text;
            }

            Application.DoEvents();
        }
        #endregion

        public FormStudio()
        {
            InitializeComponent();
            if (Settings.Default.ApplicationSkinName != string.Empty && Settings.Default.ApplicationSkinName != null)
            {
                PaletteModeManager cpu = (PaletteModeManager)Enum.Parse(typeof(PaletteModeManager), Settings.Default["ApplicationSkinName"].ToString());

                kryptonManager1.GlobalPaletteMode = cpu;
            }
            SkinNameList.AddRange(System.Enum.GetNames(typeof(PaletteModeManager)));
        }
        #region bar
        private string SkinName;
        List<string > SkinNameList = new List<string >();
        private void kryptonRibbonGroupGallery1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                kryptonManager1.GlobalPaletteMode = (PaletteModeManager)System.Enum.Parse(typeof(PaletteModeManager), $"{SkinNameList[kryptonRibbonGroupGallery1.SelectedIndex]}");

                SkinName = kryptonManager1.GlobalPaletteMode.ToString();
                GrSkin.TextLine1 = SkinName;
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

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
                    foreach (KryptonPage item in TabForm.Pages)
                    {
                        if (item.Text == "SQLServerUtils")
                        {
                            TabForm.SelectedPage = item;
                        }
                    }
                    return;
                }
            }
            KryptonPage page = NewPage("SQLServerUtils ", 0, new FormSQLServerUtils());
            TabForm.Pages.Add(page);
        }

        private void mConfiguration_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FormConfiguration))
                {
                    form.Activate();
                    return;
                }
            }

            FormConfiguration child = new FormConfiguration();
            child.Show();
        }

        private void mPCControllercs_Click(object sender, EventArgs e)
        {

        }

        private void barButtonLibraryImages_Click(object sender, EventArgs e)
        {
            try
            {
                MainView frm = new MainView();
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
            {
                if (form.GetType() == typeof(PLC_MonitorForm))
                {
                    foreach (KryptonPage item in TabForm.Pages)
                    {
                        if (item.Text == "MonitorForm")
                        {
                            TabForm.SelectedPage = item;
                        }
                    }
                    return;
                }
            }

            KryptonPage page = NewPage("MonitorForm", 0, new PLC_MonitorForm());
            TabForm.Pages.Add(page);
            TabForm.SelectedPage = page;
        }
        private void mExit_Click(object sender, EventArgs e)
        {
            if (SkinName != string.Empty && SkinName != null)
            {
                Settings.Default["ApplicationSkinName"] = SkinName;
            }

            Settings.Default.Save();
            Application.ExitThread();
        }

        #endregion

        #region Navigator




        private void TagManagerItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(XTagManager))
                {
                    foreach (KryptonPage item in TabForm.Pages)
                    {
                        if (item.Text == "TagManager")
                        {
                            TabForm.SelectedPage = item;
                        }
                    }

                    return;
                }
            }
            KryptonPage page = NewPage("TagManager", 0, new XTagManager());
            TabForm.Pages.Add(page);
            TabForm.SelectedPage = page;
        }

        private void SQLManagerItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(XSQLMaster))
                {
                    foreach (KryptonPage item in TabForm.Pages)
                    {
                        if (item.Text == "SQLMaster")
                        {
                            TabForm.SelectedPage = item;
                        }
                    }

                    return;
                }
            }

            KryptonPage page = NewPage("SQLMaster", 0, new XSQLMaster());
            TabForm.Pages.Add(page);
            TabForm.SelectedPage = page;

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
            KryptonPage p = new KryptonPage
            {
                Text = name + _count.ToString(),
                TextTitle = name + _count.ToString(),
                TextDescription = name + _count.ToString(),
                ImageSmall = imageListSmall.Images[image]
            };

            // Add the control for display inside the page
            content.Dock = DockStyle.Fill;
            p.Controls.Add(content);

            _count++;
            return p;
        }

        private KryptonPage NewPage(string name, int image, System.Windows.Forms.Control content)
        {

            // Create new page with title and image
            KryptonPage p = new KryptonPage
            {
                Text = name,
                TextTitle = name,
                TextDescription = name,
                ImageSmall = imageListSmall.Images[image]
            };

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
        public ServiceHost InitializeTags(bool Start = false)
        {
            ServiceHost host = null;
            WebServiceHost objWebServiceHost = null;
            try
            {

                eventConnectionState += new EventConnectionState(SetConnectionState);
                new ServiceDriverHelper().InitializePLC();
                host = new ServiceDriverHelper().InitializeReadServiceHttp();
                objWebServiceHost = new ServiceDriverHelper().InitializeReadServiceWeb();
                host.Opened += host_Opened;
                host.Open();




                if (host.State == CommunicationState.Opened)
                {
                    txtStatus.Text = "The Server is running";
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }


            Text = "ServerUtils : AdvancedScada";
            return host;
        }
        private ConnectionState _ConnState = ConnectionState.DISCONNECT;

        private void SetConnectionState(ConnectionState connState, string msg)
        {

            try
            {

                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate ()
                    {
                        if (connState != _ConnState)
                        {
                            switch (connState)
                            {
                                case ConnectionState.CONNECT:
                                    lblConnectState.Image = Properties.Resources.Connect16px;
                                    lblConnectState.Text = "Connected";
                                    break;
                                case ConnectionState.DISCONNECT:
                                    lblConnectState.Image = Properties.Resources.Disconnect16px;
                                    lblConnectState.Text = "Disonnect";
                                    break;
                            }

                            _ConnState = connState;
                        }
                    });
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

        }
        private void ServiceBase_eventChannelCount(int ChannelCount, bool IsNew)
        {
            if (IsNew)
            {
                int ChannelCount2 = int.Parse(txtChannelCount.Text);

                txtChannelCount.Text = $"{ChannelCount2 + ChannelCount}";
            }
            else
            {
                int ChannelCount2 = int.Parse(txtChannelCount.Text);

                txtChannelCount.Text = $"{ChannelCount2 - ChannelCount}";
            }
        }

        public void host_Opened(object sender, EventArgs e)
        {

            txtStatus.Text = "The Server is running";
        }
        private void FormStudio_Load(object sender, EventArgs e)
        {
            Left = SystemInformation.WorkingArea.Size.Width - Size.Width;
            Top = SystemInformation.WorkingArea.Size.Height - Size.Height;
            CheckEnabele.Checked = Settings.Default.CheckEnabele;

            Registry.SetValue("HKEY_CURRENT_USER\\Software\\FormConfiguration", "IPAddress", "localhost");
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\FormConfiguration", "Port", "8080");


            try
            {
                EventChannelCount += ServiceBase_eventChannelCount;
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);

            }
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
            ChannelService objChannelManager = ChannelService.GetChannelManager();
            string xmlFile = objChannelManager.ReadKey(objChannelManager.XML_NAME_DEFAULT);
            if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile))
            {
                return;
            }

            System.Collections.Generic.List<DriverBase.Devices.Channel> chList = objChannelManager.GetChannels(xmlFile);
            if (chList.Count < 1)
            {
                return;
            }

            try
            {

                InitializeTags(true);

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }
        private void FormStudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                WindowState = FormWindowState.Minimized;
            }

            notifyIcon1.Visible = true;
            e.Cancel = true;
            if (SkinName != string.Empty && SkinName != null)
            {
                Settings.Default["ApplicationSkinName"] = SkinName;
            }
        }
        #endregion



        #region popupMenuNotifyIcon
        private void ItemExit_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show(this, "You sure you want to save?", "MSG_QUESTION", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);
            if (SkinName != string.Empty)
            {
                Settings.Default["ApplicationSkinName"] = SkinName;
            }

            Settings.Default.Save();
            if (rs == DialogResult.Yes)
            {
                Application.ExitThread();
            }
        }

        private void ItemView_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }





        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && IsHandleCreated)
            {
                popupMenuNotifyIcon.Show(MousePosition);
            }
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
                    foreach (KryptonPage item in TabForm.Pages)
                    {
                        if (item.Text == "DiscreteAlarm")
                        {
                            TabForm.SelectedPage = item;
                        }
                    }
                    return;
                }
            }
            KryptonPage page = NewPage("DiscreteAlarm", 0, new FrmDiscreteAlarm());
            TabForm.Pages.Add(page);
            TabForm.SelectedPage = page;
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
            KryptonPage page = NewPage("FormMain", 0, new FormMain());
            TabForm.Pages.Add(page);
            TabForm.SelectedPage = page;
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
            KryptonPage page = NewPage("AlarmAnalog", 0, new FrmAddAlarm());
            TabForm.Pages.Add(page);
            TabForm.SelectedPage = page;
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
            KryptonPage page = NewPage("AlarmClasses", 0, new FrmAlarmClasses());
            TabForm.Pages.Add(page);
            TabForm.SelectedPage = page;
        }
    }
}
