using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Utils.Excel;
using ComponentFactory.Krypton.Toolkit;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Studio.IE
{
    public delegate void EventDataBlockImport(DataBlock db);
    public partial class FormImport : KryptonForm
    {
        public FormImport()
        {
            InitializeComponent();
        }
        public static readonly ManualResetEvent SendDone = new ManualResetEvent(true);
        private readonly Channel ch;
        private readonly DataBlock db;
        private readonly Device dv;
        public EventDataBlockImport eventDataBlockChanged = null;

        public FormImport(Channel chParam = null, Device dvParam = null, DataBlock dbParam = null, Tag tgParam = null)
        {
            InitializeComponent();
            ch = chParam;
            dv = dvParam;
            db = dbParam;
        }
        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (eventDataBlockChanged != null)
            {
                eventDataBlockChanged(db);
            }

            DialogResult = DialogResult.OK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPathFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                FileName = "*.xls",
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                PathFile.Text = openFileDialog.FileName;
                ExcelPackage excel = new ExcelPackage(fileInfo);

                foreach (ExcelWorksheet worksheet in excel.Workbook.Worksheets)
                {
                    cboxSheet.Items.Add(worksheet.Name);
                }

            }
        }

        private void cboxSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = ExcelUtils.ReadExcel(PathFile.Text, cboxSheet.Text);

                short counter = 0;
                DGImportForm.Rows.Clear();
                db.Tags.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    counter++;
                    Tag newTag = new Tag
                    {
                        TagId = counter,
                        TagName = $"{item["TagName"]}",
                        Address =
                            $"{item["Address"]}",
                        DataType = db.DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), $"{item["DataType"]}"),
                        Description = $"{item["Description"]}"
                    };

                    db.Tags.Add(newTag);
                }

                foreach (Tag tg in db.Tags)
                {
                    string[] row = { string.Format("{0}", tg.TagId), tg.TagName, string.Format("{0}", tg.Address), string.Format("{0}", tg.DataType), tg.Description };


                    DGImportForm.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        private void FormImport_Load(object sender, EventArgs e)
        {
            txtDevice.Text = dv.DeviceName;
            txtChannel.Text = ch.ChannelName;
            txtDataBlock.Text = db.DataBlockName;
        }
    }
}
