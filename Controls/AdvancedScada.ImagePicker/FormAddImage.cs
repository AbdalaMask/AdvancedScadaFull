using ComponentFactory.Krypton.Toolkit;
using Svg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

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
                    
                    SVGSample.svg.SVGParser.MaximumSize = new Size(1000, 700);
                  var  svgDocument = SVGSample.svg.SVGParser.GetSvgDocument(imageListBoxControl.SelectedItem.ToString());
                   var bitmap = SVGSample.svg.SVGParser.GetBitmapFromSVG(imageListBoxControl.SelectedItem.ToString());
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
        public static void UpdateResourceFile(Hashtable data, String path)
        {
            Hashtable resourceEntries = new Hashtable();

            ////Get existing resources
            //ResXResourceReader reader = new ResXResourceReader(path);
            //if (reader != null)
            //{
            //    IDictionaryEnumerator id = reader.GetEnumerator();
            //    foreach (DictionaryEntry d in reader)
            //    {
            //        if (d.Value == null)
            //            resourceEntries.Add(d.Key.ToString(), "");
            //        else
            //            resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
            //    }
            //    reader.Close();
            //}

            //Modify resources here...
            foreach (String key in data.Keys)
            {
                if (!resourceEntries.ContainsKey(key))
                {

                    String value = data[key].ToString();
                    if (value == null) value = "";

                    resourceEntries.Add(key, value);
                }
            }

            //Write the combined resource file
            ResXResourceWriter resourceWriter = new ResXResourceWriter(path);

            foreach (String key in resourceEntries.Keys)
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
           
            foreach (var file in dirs)
            {
                if (file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".bmp") || file.EndsWith(".BMP") ||
                    file.EndsWith(".JPG") || file.EndsWith(".gif") || file.EndsWith(".wmf") || file.EndsWith(".svg"))
                {
                    //var newName = Path.GetFileNameWithoutExtension(file);
                    //newName= Regex.Replace(newName, "[ ]","_");

                    var newName =$"{ txtCategoryName.Text}_"+ i++;
                    if (file.EndsWith(".svg"))
                    {
                        try
                        {
                           
                            //SVGSample.svg.SVGParser.MaximumSize = new Size(1000, 700);
                            //var svgDocument = SVGSample.svg.SVGParser.GetSvgDocument(file);
                            //var bitmap = SVGSample.svg.SVGParser.GetBitmapFromSVG(file);


                            var xmlDoc = new XmlDocument
                            {
                                XmlResolver = null
                            };
                            xmlDoc.Load(file);
                            var GETXML = xmlDoc.InnerXml;

                           
                          //  openWith.Add(newName, GETXML);


                          //  Pic.Image = bitmap;
                            rsxw.AddResource(newName, GETXML);
                        }
                        catch (Exception ex)
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
