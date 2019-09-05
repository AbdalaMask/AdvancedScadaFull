using ComponentFactory.Krypton.Toolkit;
using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagePicker
{
    public partial class FormAddImage : KryptonForm
    {
        public string CategoryName = "Category_Files\\{0}.resx";
        private string[] dirs;
        private ResXResourceWriter rsxw;
        public FormAddImage()
        {
            InitializeComponent();
        }
        private List<string> DirSearch(string sDir)
        {
            var files = new List<string>();
            try
            {
                foreach (var f in Directory.GetFiles(sDir)) files.Add(f);
                foreach (var d in Directory.GetDirectories(sDir)) files.AddRange(DirSearch(d));
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }

            return files;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnSelectedPath_Click(object sender, EventArgs e)
        {
            var SelectedPath = string.Empty;
            imageListBoxControl.Items.Clear();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                SelectedPath = fbd.SelectedPath;
                txtSelectedPath.Text = SelectedPath;
                dirs = Directory.GetFiles(SelectedPath);

                //foreach (string item2 in dirs)
                //{

                //        string newName = System.IO.Path.GetFileNameWithoutExtension(item2);
                //        imageListBoxControl1.Items.Add(item2);

                //}
                imageListBoxControl.Items.AddRange(DirSearch(SelectedPath).ToArray());
                dirs = DirSearch(SelectedPath).ToArray();
            }
        }

        private void ImageListBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var newName = Path.GetExtension(imageListBoxControl.SelectedItem.ToString());
                if (newName.EndsWith(".svg"))
                {
                    var svgDocument = SvgDocument.Open(imageListBoxControl.SelectedItem.ToString());
                    var bitmap = svgDocument.Draw();

                    Pic.Image = bitmap;
                }
                else
                {
                    Pic.Image = Image.FromFile(imageListBoxControl.SelectedItem.ToString());
                }
            }
            catch (Exception)
            {
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            rsxw = new ResXResourceWriter(string.Format(CategoryName, txtCategoryName.Text));

            foreach (var file in dirs)
            {
                if (file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".bmp") || file.EndsWith(".BMP") ||
                    file.EndsWith(".JPG") || file.EndsWith(".gif") || file.EndsWith(".wmf") || file.EndsWith(".svg"))
                {
                    var newName = Path.GetFileNameWithoutExtension(file);

                   
                    if (file.EndsWith(".svg"))
                    {
                        try
                        {
                            var svgDocument = SvgDocument.Open(file);
                            var bitmap = svgDocument.Draw();
                           
                            Pic.Image = bitmap;
                            rsxw.AddResource(file, bitmap);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        var bitmap = Image.FromFile(file);
                        Pic.Image = bitmap;
                        rsxw.AddResource(file, bitmap);
                    }
                }

                Application.DoEvents();
            }

            rsxw.Close();
            MessageBox.Show("تم");
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            imageListBoxControl.Items.RemoveAt(imageListBoxControl.SelectedIndex);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnOK2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
