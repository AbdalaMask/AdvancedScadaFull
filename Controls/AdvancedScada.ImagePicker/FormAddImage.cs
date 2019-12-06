using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using System.Xml;
using static AdvancedScada.Common.XCollection;
namespace ImagePicker
{
    public partial class FormAddImage : KryptonForm
    {
        public string CategoryName = "Category_Files\\{0}";
        private string[] dirs;
        private ResXResourceWriter rsxw;
        public FormAddImage()
        {
            InitializeComponent();
        }
        private List<string> DirSearch(string sDir)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
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
            string SelectedPath = string.Empty;
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
                string newName = Path.GetExtension(imageListBoxControl.SelectedItem.ToString());
                if (newName.EndsWith(".svg"))
                {

                    SVGSample.svg.SVGParser.MaximumSize = new Size(1000, 700);
                    Svg.SvgDocument svgDocument = SVGSample.svg.SVGParser.GetSvgDocument(imageListBoxControl.SelectedItem.ToString());
                    Bitmap bitmap = SVGSample.svg.SVGParser.GetBitmapFromSVG(imageListBoxControl.SelectedItem.ToString());
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
        public static void UpdateResourceFile(Hashtable data, string path)
        {
            Hashtable resourceEntries = new Hashtable();

            //Get existing resources
            ResXResourceReader reader = new ResXResourceReader(path);
            if (reader != null)
            {
                IDictionaryEnumerator id = reader.GetEnumerator();
                foreach (DictionaryEntry d in reader)
                {
                    if (d.Value == null)
                    {
                        resourceEntries.Add(d.Key.ToString(), string.Empty);
                    }
                    else
                    {
                        resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
                    }
                }
                reader.Close();
            }

            //Modify resources here...
            foreach (string key in data.Keys)
            {
                if (!resourceEntries.ContainsKey(key))
                {

                    string value = data[key].ToString();
                    if (value == null)
                    {
                        value = string.Empty;
                    }

                    resourceEntries.Add(key, value);
                }
            }

            //Write the combined resource file
            ResXResourceWriter resourceWriter = new ResXResourceWriter(path);

            foreach (string key in resourceEntries.Keys)
            {
                resourceWriter.AddResource(key, resourceEntries[key]);
            }
            resourceWriter.Generate();
            resourceWriter.Close();

        }
        private void BtnOK_Click(object sender, EventArgs e)
        {
            int i = 0;
            rsxw = new ResXResourceWriter(string.Format(CategoryName, txtCategoryName.Text));

            foreach (string file in dirs)
            {
                if (file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".bmp") || file.EndsWith(".BMP") ||
                    file.EndsWith(".JPG") || file.EndsWith(".gif") || file.EndsWith(".wmf") || file.EndsWith(".svg") || file.EndsWith(".Xaml"))
                {

                    string newName = $"{ txtCategoryName.Text}_" + i++;
                    if (file.EndsWith(".svg") || file.EndsWith(".Xaml"))
                    {
                        try
                        {


                            XmlDocument xmlDoc = new XmlDocument
                            {
                                XmlResolver = null
                            };
                            xmlDoc.Load(file);
                            string GETXML = xmlDoc.InnerXml;


                            //  Pic.Image = bitmap;
                            rsxw.AddResource(newName, GETXML);
                        }
                        catch (Exception ex)
                        {
                            EventscadaException?.Invoke(GetType().Name, ex.Message);
                            continue;
                        }

                    }
                    else if (file.EndsWith(".wmf"))
                    {

                        rsxw.AddResource(newName, Convert.ToBase64String(System.IO.File.ReadAllBytes(file)));
                    }
                    else
                    {
                        rsxw.AddResource(newName, Convert.ToBase64String(System.IO.File.ReadAllBytes(file)));
                    }
                }

                Application.DoEvents();
            }
            //  UpdateResourceFile(openWith, string.Format(CategoryName, txtCategoryName.Text));
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
