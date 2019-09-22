using AdvancedScada.Utils.Compression;
using ComponentFactory.Krypton.Toolkit;
using ImagePicker;
using Microsoft.Win32;
using Svg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using static AdvancedScada.IBaseService.Common.XCollection;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using Image = System.Drawing.Image;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace AdvancedScada.ImagePicker
{
    public delegate void EventImageSelected(Image ImageName);
    public delegate void EventImageSVGSelected(string ImageName);
    public delegate void EventStringImageSelected(string ImageName);
    public partial class MainView : KryptonForm
    {
        public EventStringImageSelected OnStringImageSelected_Clicked = null;
        public EventImageSelected OnImagSelected_Clicked = null;
        public EventImageSVGSelected OnImagSVGSelected_Clicked = null;
        #region Fild

        public static System.Windows.Controls.Canvas previewTarget;
        // Create a ResXResourceReader for the file items.resx.
        private ResXResourceReader rsxr;
        Dictionary<int, string> ImageListCurrentSVG = new Dictionary<int, string>();
        Dictionary<int, string> ImageListCurrentTip = new Dictionary<int, string>();
        // local variable declarations
        public string CategoryName = "Category_Files\\{0}.resx";
        public ResXResourceWriter rsxw = null;
        ImageList il32 = null;
        Dictionary<int, Image> ImageListCurrentImage = new Dictionary<int, Image>();
        string[] dirs2 = null;
        string[] dirs3 = null;
        #endregion

        public string ReadKey(string keyName)
        {
            var result = string.Empty;
            try
            {
                RegistryKey regKey;
                regKey = Registry.CurrentUser.OpenSubKey(@"Software\HMI"); //HKEY_CURRENR_USER\Software\VSSCD
                if (regKey != null) result = (string)regKey.GetValue(keyName);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

            return result;
        }

        public MainView(Image imageName = null)
        {
            InitializeComponent();

        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            try
            {
                var SelectedPath = ReadKey("Symbols");
                var SelectedPath2 = ReadKey("LibraryImage");
                var SelectedPath3 = ReadKey("LibraryImages");
                if (SelectedPath != string.Empty && SelectedPath != null)
                {
                    var dirs = Directory.GetDirectories(SelectedPath);
                    foreach (var item2 in dirs)
                    {
                        var f = new FileInfo(item2);
                        var v = f.Name.Split('.');
                        cboxListForderSVG.Items.Add(v[0]);
                    }
                }
                if (SelectedPath2 != string.Empty && SelectedPath2 != null)
                {

                    dirs2 = Directory.GetDirectories(SelectedPath2);

                    foreach (var item2 in dirs2)
                    {
                        var f = new FileInfo(item2);
                        var v = f.Name.Split('.');
                        cboxListForder.Items.Add(v[0]);
                    }
                }
                if (SelectedPath3 != string.Empty && SelectedPath3 != null)
                {

                    dirs3 = Directory.GetFiles(SelectedPath3);

                    foreach (var item2 in dirs3)
                    {
                        var f = new FileInfo(item2);
                        var v = f.Name.Split('.');
                        ListBoxCategoryName.Items.Add(v[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }


        }
        private List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
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
            catch (System.Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }

            return files;
        }

        #region list
        private void ListBoxCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            il32 = new ImageList();
            il32.ColorDepth = ColorDepth.Depth32Bit;
            il32.ImageSize = new Size(50, 50);
            ImageListCurrentImage.Clear();
            ImageListCurrentTip.Clear();
            var CategoryName = string.Format(ReadKey("LibraryImages") + "\\{0}.resx", ListBoxCategoryName.SelectedItem);
            rsxr = new ResXResourceReader(CategoryName);

            var i = 0;
            foreach (DictionaryEntry file in rsxr)
            {
                string newName = Path.GetFileNameWithoutExtension($"{file.Key}");
                var bitmap = (Image)file.Value;
                ImageListCurrentImage.Add(i++, bitmap);
                ImageListCurrentTip.Add(i, string.Format("{0}.{1}.{2}", newName, bitmap.Height, bitmap.Width));

                il32.Images.Add($"{file.Key}", bitmap);
                Application.DoEvents();


            }

            //Close the reader.
            rsxr.Close();
            pnlPictures.ImageList = il32;

        }
        /// <summary>
        /// The file path of the SVG image selected.
        /// </summary>
        private string selectedPath;

        /// <summary>
        /// Instance reference for the svgDocument used and updated throughout the manipulation of the image.
        /// </summary>
        private Svg.SvgDocument svgDocument;

        private void cboxListForder_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            
            il32 = new ImageList();
            il32.ColorDepth = ColorDepth.Depth32Bit;
            il32.ImageSize = new Size(50, 50);
            int Level = 0;
            int i = 0;
            string SelectedPath = ReadKey("LibraryImage") + $@"\\{cboxListForder.SelectedItem.ToString()}";
            ImageListCurrentImage.Clear();
            ImageListCurrentTip.Clear();
            try
            {
                
                    var dirs = DirSearch(SelectedPath).ToArray();
                Task t = Task.Factory.StartNew(() => {
                    foreach (var item in dirs)
                    {
                        Image bitmap = null;
                        string newName = Path.GetFileNameWithoutExtension(item);
                        if (item.EndsWith(".wmf"))
                        {
                            bitmap = new Metafile(item);
                            Bitmap pic = new Bitmap(100, 100);
                            using (Graphics g = Graphics.FromImage(pic))
                            {
                                g.DrawImage(bitmap, new System.Drawing.Rectangle(0, 0, pic.Width, pic.Height)); //redraw smaller image
                            }
                            bitmap = pic;
                        }
                        else
                        {
                            bitmap = Image.FromFile(item);
                        }

                        ImageListCurrentImage.Add(i++, bitmap);
                        ImageListCurrentTip.Add(i, string.Format("{0}.{1}.{2}", newName, bitmap.Height, bitmap.Width));
                        il32.Images.Add(newName, bitmap);
                      
                    }
                
            } );
            t.Wait();
            gc.ImageList = il32;
                   
                       
               
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                this.Text = $"{Level++}";
            }
         
        }

        private void cboxListForderSVG_SelectedIndexChanged(object sender, EventArgs e)
        {
            il32 = new ImageList();
            il32.ColorDepth = ColorDepth.Depth32Bit;
            il32.ImageSize = new Size(50, 50);

            string SelectedPath = ReadKey("Symbols") + $@"\\{ cboxListForderSVG.SelectedItem.ToString()}";
            int i = 0;
            var dirs = DirSearch(SelectedPath).ToArray();
            ImageListCurrentSVG.Clear();
            ImageListCurrentTip.Clear();
          
            int Level = 0;
            foreach (var item in dirs)
            {
                string newName = Path.GetFileNameWithoutExtension(item);

                try
                {
                    SVGSample.svg.SVGParser.MaximumSize = new Size(1000, 700);

                     
                    svgDocument = SVGSample.svg.SVGParser.GetSvgDocument(item);

                    var bitmap  = SVGSample.svg.SVGParser.GetBitmapFromSVG(item);
                  
                    ImageListCurrentSVG.Add(i++, item);
                    ImageListCurrentTip.Add(i, string.Format("{0}.{1}.{2}", newName, bitmap.Height, bitmap.Width));
                    il32.Images.Add(newName, bitmap);
                    Application.DoEvents();

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    this.Text = $"{Level++}";
                    //return;
                }

            }

            gcSVG.ImageList = il32;
        }
        #endregion
        #region gc

        private void gcSVG_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void gc_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }
        private void gc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var bitmap = ImageListCurrentImage[gc.SelectedIndex];
                var stringBitmap = ImageListCurrentTip[gc.SelectedIndex].Split('.');
                var bitmapMessage = string.Format("Name:{0} Height:{1} Width:{2} ", stringBitmap[0] + Environment.NewLine, stringBitmap[1] + Environment.NewLine, stringBitmap[2]);
                byte[] data = ImageCompression.ImageToByte(bitmap);
                byte[] dataCompress = ImageCompression.Compress(data);
                string FullNameBase = Convert.ToBase64String(dataCompress);
                toolTip1.Active = true;
                toolTip1.Show(bitmapMessage, this);
                toolTip1.SetToolTip(gc, bitmapMessage);
                //==================================================================
                if (OnStringImageSelected_Clicked != null)
                {
                    OnStringImageSelected_Clicked(FullNameBase);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                //====================================================================================
                if (OnImagSelected_Clicked != null)
                {
                    OnImagSelected_Clicked(bitmap);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

        }

        private void gcSVG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var bitmap = ImageListCurrentSVG[gcSVG.SelectedIndex];

                var stringBitmap = ImageListCurrentTip[gcSVG.SelectedIndex].Split('.');

              
                var bitmapMessage = string.Format("Name:{0} Height:{1} Width:{2} ", stringBitmap[0] + Environment.NewLine, stringBitmap[1] + Environment.NewLine, stringBitmap[2]);

                toolTip1.Active = true;
                toolTip1.Show(bitmapMessage, this);
                toolTip1.SetToolTip(gcSVG, bitmapMessage);
                //==================================================================
                if (OnImagSVGSelected_Clicked != null)
                {

                    SVGSample.svg.SVGParser.MaximumSize = new Size(1000, 700);
                    svgDocument = SVGSample.svg.SVGParser.GetSvgDocument(bitmap);

                    var xmlDoc = new XmlDocument
                    {
                        XmlResolver = null
                    };
                    xmlDoc.Load(bitmap);
                    var GETXML = xmlDoc.InnerXml;

                

                    OnImagSVGSelected_Clicked?.Invoke(GETXML);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                //====================================================================================
                if (OnStringImageSelected_Clicked != null)
                {
                    SVGSample.svg.SVGParser.MaximumSize = new Size(1000, 700);
                    svgDocument = SVGSample.svg.SVGParser.GetSvgDocument(bitmap);

                    var xmlDoc = new XmlDocument
                    {
                        XmlResolver = null
                    };
                    xmlDoc.Load(bitmap);
                    var GETXML = xmlDoc.InnerXml;

 
                    OnStringImageSelected_Clicked?.Invoke(StringCompression.Compress(GETXML));
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }
        private void pnlPictures_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var bitmap = ImageListCurrentImage[pnlPictures.SelectedIndex];
                var stringBitmap = ImageListCurrentTip[pnlPictures.SelectedIndex].Split('.');
                var bitmapMessage = string.Format("Name:{0} Height:{1} Width:{2} ", stringBitmap[0] + Environment.NewLine, stringBitmap[1] + Environment.NewLine, stringBitmap[2]);
                byte[] data = ImageCompression.ImageToByte(bitmap);
                byte[] dataCompress = ImageCompression.Compress(data);
                string FullNameBase = Convert.ToBase64String(dataCompress);
                toolTip1.Active = true;
                toolTip1.Show(bitmapMessage, this);
                toolTip1.SetToolTip(pnlPictures, bitmapMessage);
                //==================================================================
                if (OnStringImageSelected_Clicked != null)
                {
                    OnStringImageSelected_Clicked(FullNameBase);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                //====================================================================================
                if (OnImagSelected_Clicked != null)
                {
                    OnImagSelected_Clicked(bitmap);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        private void pnlPictures_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }
        #endregion
        #region Export


        private void BtnExportPNG_Click(object sender, EventArgs e)
        {
            var bitmap = il32.Images[pnlPictures.SelectedIndex];

            var newName = $"Image";

            newName = newName + ".PNG";

            try
            {
                saveFileDialog1.Filter = "Image Files(*.Png)|*.Png|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = newName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    bitmap.Save(saveFileDialog1.FileName, ImageFormat.Png);
            }
            catch
            {
                MessageBox.Show("Failed to save image to PNG format.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Image file saved to " + newName,
                "Image Saved", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnExportICO_Click(object sender, EventArgs e)
        {

            var bitmap = il32.Images[pnlPictures.SelectedIndex];

            var newName = $"Image";

            newName = newName + ".Ico";

            try
            {
                saveFileDialog1.Filter = "Image Files(*.Ico)|*.Ico|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = newName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    bitmap.Save(saveFileDialog1.FileName, ImageFormat.Icon);
            }
            catch
            {
                MessageBox.Show("Failed to save image to Icon format.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Image file saved to " + newName,
                "Image Saved", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnExportGIF_Click(object sender, EventArgs e)
        {
            var bitmap = il32.Images[pnlPictures.SelectedIndex];

            var newName = $"Image";

            newName = newName + ".gif";

            try
            {
                saveFileDialog1.Filter = "Image Files(*.GIF)|*.GIF|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = newName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    bitmap.Save(saveFileDialog1.FileName, ImageFormat.Gif);
            }
            catch
            {
                MessageBox.Show("Failed to save image to Gif format.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Image file saved to " + newName,
                "Image Saved", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnExportXaml_Click(object sender, EventArgs e)
        {
            var pic = il32.Images[pnlPictures.SelectedIndex];
            var newName = $"Image";
            newName = newName + ".Xaml";
            try
            {
                var xamlOutputText = string.Empty;
                var xamlOutput = string.Empty;

                previewTarget = new System.Windows.Controls.Canvas();
                previewTarget.Background = Brushes.Transparent;
                Rectangle rectangle = null;
                var bitmap = new Bitmap(pic);
                previewTarget.Height = bitmap.Height;
                previewTarget.Width = bitmap.Width;


                for (var y = 0; y < bitmap.Height; ++y)
                    for (var x = 0; x < bitmap.Width; ++x)
                    {
                        var pixel = bitmap.GetPixel(x, y);
                        rectangle = new Rectangle();
                        rectangle.Fill = new SolidColorBrush(Color.FromArgb(pixel.A, pixel.R, pixel.G, pixel.B));
                        rectangle.Height = rectangle.Width = 1.0;
                        rectangle.SetValue(System.Windows.Controls.Canvas.LeftProperty, (double)x);
                        rectangle.SetValue(System.Windows.Controls.Canvas.TopProperty, (double)y);
                        rectangle.SnapsToDevicePixels = true;
                        previewTarget.Children.Add(rectangle);

                        Application.DoEvents();
                    }

                Application.DoEvents();

                var settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "\t";
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                var output = new StringBuilder();
                XamlWriter.Save(previewTarget, XmlWriter.Create(output, settings));
                xamlOutputText = output.ToString();
                saveFileDialog1.Filter = "Xaml Files(*.Xaml)|*.Xaml|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = newName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    TextWriter writeFile = new StreamWriter(saveFileDialog1.FileName);
                    writeFile.Write(xamlOutputText);
                    writeFile.Flush();
                    writeFile.Close();
                    writeFile = null;
                }
            }
            catch
            {
                MessageBox.Show("Failed to save Xaml to Xaml format.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Xaml file saved to " + newName,
                "Xaml Saved", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        #endregion
        private void BtnAddImageFiles_Click(object sender, EventArgs e)
        {
            var frm = new FormAddImage();
            frm.ShowDialog();
        }


    }
}
