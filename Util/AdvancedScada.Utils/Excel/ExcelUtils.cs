using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AdvancedScada.Utils.Excel
{
  

    public static class ExcelUtils

    {
        // Reading a simple excel sheet that contains only text and numbers into DataTable...
        private static DataTable WorksheetToDataTable(ExcelWorksheet oSheet)
        {
            int totalRows = oSheet.Dimension.End.Row;
            int totalCols = oSheet.Dimension.End.Column;
            DataTable dt = new DataTable(oSheet.Name);
            DataRow dr = null;
            for (int i = 1; i <= totalRows; i++)
            {
                if (i > 1) dr = dt.Rows.Add();
                for (int j = 1; j <= totalCols; j++)
                {
                    if (i == 1)
                        dt.Columns.Add(oSheet.Cells[i, j].Value.ToString());
                    else
                        dr[j - 1] = oSheet.Cells[i, j].Value.ToString();
                }
            }
            return dt;
        }

        public static GetErorr eventGetErorr;

        public static DataTable ReadExcel(string filename, string sheetName)
        {
            var dtImport = new DataTable();
            using (ExcelPackage excelPkg = new ExcelPackage())
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
                excelPkg.Load(stream);

                ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets[sheetName];
                dtImport = WorksheetToDataTable(oSheet);
            }


            return dtImport;
        }

        /// <summary>
        ///     Exports the datagridview values to Excel.
        /// </summary>
        public static void ExportToExcelPackage(string DataBlockName, ListView listViewS)
        {
            // Creating a Excel object.
            using (var p = new ExcelPackage())
            {
                p.Workbook.Properties.Author = "Miles Dyson";
                p.Workbook.Properties.Title = "SkyNet Monthly Report";
                p.Workbook.Properties.Company = "Cyberdyne Systems";

                // The rest of our code will go here...

                p.Workbook.Worksheets.Add(DataBlockName);
                var ws = p.Workbook.Worksheets[1]; // 1 is the position of the worksheet
                ws.Name = DataBlockName;

                var cellRowIndex = 1;
                var cellColumnIndex = 1;

                //Loop through each row and read value from each column.
                for (var i = 0; i < listViewS.Items.Count + 1; i++)
                {
                    for (var j = 0; j < listViewS.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check.
                        if (cellRowIndex == 1)
                        {
                            var cell_actionName = ws.Cells[cellRowIndex, cellColumnIndex];
                            cell_actionName.Value = listViewS.Columns[j].Text;
                        }
                        else
                        {
                            var processorCell = ws.Cells[cellRowIndex, cellColumnIndex];
                            processorCell.Value = listViewS.Items[i - 1].SubItems[j].Text;
                        }

                        cellColumnIndex++;
                    }

                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                //Getting the location and file name of the excel to save from user.
                var saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 1;
                saveDialog.FileName = DataBlockName;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var bin = p.GetAsByteArray();
                    File.WriteAllBytes(saveDialog.FileName, bin);
                    MessageBox.Show("Export Successful");
                }
            }
        }
    }

}
