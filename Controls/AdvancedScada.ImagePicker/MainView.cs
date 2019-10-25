using ComponentFactory.Krypton.Toolkit;
using AdvancedScada.Images;
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
        //// Create a ResXResourceReader for the file items.resx.
        //private ResXResourceReader rsxr;

        Dictionary<int, string> ImageListCurrentTip = new Dictionary<int, string>();
        // local variable declarations
        public string CategoryName = "Category_Files\\{0}.resx";
        public ResXResourceWriter rsxw = null;
        ImageList il32 = null;
        Dictionary<int, Image> ImageListCurrentImage = new Dictionary<int, Image>();

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
        readonly static char[] splitCharacters = new char[] { '\\', '/' };
        [System.Runtime.CompilerServices.MethodImpl(256)]
        public static string[] Split(string key)
        {
            return key.Split(splitCharacters);
        }
        public void GetImageFolderName(string imageType)
        {

            var category = ImageResourceCache.DoLoad(imageType).GetAllResourceKeys();
            string[] parts;
            ListBoxCategoryName.Items.Clear();
            if (category == null) return;
            foreach (var item2 in category)
            {
                parts = Split(item2);
                ListBoxCategoryName.Items.Add(parts[1]);
            }

        }
        private void MainView_Load(object sender, EventArgs e)
        {
           
            string imageType = "";
            try
            {

                if (KR_SVG.Checked)
                {
                    imageType = "svgimages";
                }
                else if (KR_Xaml.Checked)
                {
                    imageType = "XamlImages";
                }
                else if (KR_WMF.Checked)
                {
                    imageType = "WmfImages";
                }
                else if (KR_PNG.Checked)
                {
                    imageType = "PngImages";
                }
                else
                {
                    imageType = "svgimages";
                }
                GetImageFolderName(imageType);


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }


        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        #region list
        private void ListBoxCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            il32 = new ImageList();
            il32.ColorDepth = ColorDepth.Depth32Bit;
            il32.ImageSize = new Size(50, 50);
            ImageListCurrentImage.Clear();
            ImageListCurrentTip.Clear();
            string imageType;
            if (KR_SVG.Checked)
            {
                imageType = "svgimages";
            }
            else if (KR_Xaml.Checked)
            {
                imageType = "XamlImages";
            }
            else if (KR_WMF.Checked)
            {
                imageType = "WmfImages";
            }
            else if (KR_PNG.Checked)
            {
                imageType = "PngImages";
            }
            else
            {
                imageType = "svgimages";
            }
            var rsxr = ImageResourceCache.Default(imageType).GetImages($"{imageType}/{ListBoxCategoryName.SelectedItem}");
            if (rsxr == null) return;

            string newName = string.Empty;
            var i = 0;
            foreach (DictionaryEntry file in rsxr)
            {
                Image bitmap;
                if (KR_SVG.Checked)
                {
                    newName = Path.GetFileNameWithoutExtension($"{file.Key}");
                    svgDocument = SvgDocument.FromSvg<SvgDocument>($"{file.Value}");
                    SVGSample.svg.SVGParser.MaximumSize = new Size(Width, Height);
                    try
                    {
                        bitmap = svgDocument.Draw();

                    }
                    catch
                    {

                        continue;
                    }
                }
                else if (KR_Xaml.Checked)
                {
                    try
                    {
                        IMAGE_DPI = 96;
                        XmlTextReader xmlReader = new XmlTextReader(new StringReader($"{file.Value}"));
                        newName = $"{file.Key}";
                        object control = XamlReader.Load(xmlReader);
                        bitmap = SaveImage((System.Windows.Controls.Viewbox)control, "C://Test.png");
                    }
                    catch
                    {

                        continue;
                    }

                }
                else if (KR_WMF.Checked)
                {
                    try
                    {
                        var returnImage = Convert.FromBase64String($"{file.Value}");
                        newName = $"{file.Key}";
                        MemoryStream ms = new MemoryStream(returnImage);
                        bitmap = new Metafile(ms);
                        Bitmap pic = new Bitmap(100, 100);
                        using (Graphics g = Graphics.FromImage(pic))
                        {
                            g.DrawImage(bitmap, new System.Drawing.Rectangle(0, 0, pic.Width, pic.Height)); //redraw smaller image
                        }
                        bitmap = pic;
                    }
                    catch
                    {

                        continue;
                    }
                }
                else if (KR_PNG.Checked)
                {
                    try
                    {
                        var returnImage = Convert.FromBase64String($"{file.Value}");
                        newName = $"{file.Key}";
                        MemoryStream ms = new MemoryStream(returnImage);
                        bitmap = Image.FromStream(ms);

                    }
                    catch
                    {

                        continue;
                    }
                }
                else
                {
                    bitmap = Image.FromFile($"{file.Value}");
                }



                if (bitmap != null)
                {
                    ImageListCurrentTip.Add(i, string.Format("{0}.{1}.{2}", newName, bitmap.Height, bitmap.Width));

                    ImageListCurrentImage.Add(i++, bitmap);

                    il32.Images.Add($"{file.Key}", bitmap);
                }
                else
                {
                    continue;
                }
                Application.DoEvents();


            }

            //Close the reader.
            rsxr.Close();


            pnlPictures.ImageList = il32;

        }


        private Svg.SvgDocument svgDocument;


        #region Xaml
        private static double IMAGE_DPI;
        public static System.Drawing.Image SaveImage(System.Windows.Controls.Viewbox control, string path)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                GenerateImage(control, stream);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                return img;
            }
        }

        public static void GenerateImage(System.Windows.Controls.Viewbox control, Stream result)
        {
            //Set background to white
            //control.Background = System.Windows.Media.Brushes.White;

            System.Windows.Size controlSize = RetrieveDesiredSize(control);
            System.Windows.Rect rect = new System.Windows.Rect(0, 0, controlSize.Width, controlSize.Height);

            System.Windows.Media.Imaging.RenderTargetBitmap rtb = new System.Windows.Media.Imaging.RenderTargetBitmap((int)controlSize.Width, (int)controlSize.Height, IMAGE_DPI, IMAGE_DPI, PixelFormats.Pbgra32);

            control.Arrange(rect);
            rtb.Render(control);

            System.Windows.Media.Imaging.PngBitmapEncoder png = new System.Windows.Media.Imaging.PngBitmapEncoder();
            png.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(rtb));
            png.Save(result);
        }
        private static System.Windows.Size RetrieveDesiredSize(System.Windows.Controls.Viewbox control)
        {
            control.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
            return control.DesiredSize;
        }
        #endregion

        #endregion


        #region WPFUri
        public string Group { get; private set; }
        public string NameUri { get; private set; }
        internal const string PackPrefix = "pack://application:,,,";
        public string MakeUri(string ImageType)
        {
            return string.Format(PackPrefix + "/{0};component/{1}", "AdvancedScada.Images", MakeUriShort(ImageType));
        }
        public string MakeUriShort(string ImageType)
        {
            return string.Format("{1}/{2}/{3}", "AdvancedScada.Images", ImageType, Group.ToLower(), NameUri);
        }
        public string FromSvgRes(string ImageType, string Group, string NameUri)
        {

            string SvgNew2 = null;
            var tmg = ImageResourceCache.DoLoad(ImageType).GetResource($"{ImageType}/{Group}");
            using (ResXResourceReader rsxr = new ResXResourceReader(tmg))
            {

                foreach (DictionaryEntry file in rsxr)
                {

                    if (NameUri == $"{file.Key}")
                    {
                        SvgNew2 = $"{file.Value}";
                        break;
                    }

                }
            }
            return SvgNew2;
        }
        #endregion
        #region pnlPictures

      

        private void pnlPictures_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var bitmap = ImageListCurrentImage[pnlPictures.SelectedIndex];
                var stringBitmap = ImageListCurrentTip[pnlPictures.SelectedIndex].Split('.');
                var bitmapMessage = string.Format("Name:{0} Height:{1} Width:{2} ", stringBitmap[0] + Environment.NewLine, stringBitmap[1] + Environment.NewLine, stringBitmap[2]);

                toolTip1.Active = true;
                toolTip1.Show(bitmapMessage, this);
                toolTip1.SetToolTip(pnlPictures, bitmapMessage);
                //==================================================================
                if (OnImagSVGSelected_Clicked != null)
                {
                    Group = $"{ListBoxCategoryName.SelectedItem}";
                    NameUri = stringBitmap[0];

                    var Setimages = FromSvgRes("svgimages", Group, NameUri);
                    OnImagSVGSelected_Clicked?.Invoke(Setimages);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                //====================================================================================
                if (OnStringImageSelected_Clicked != null)
                {
                    Group = $"{ListBoxCategoryName.SelectedItem}";
                    NameUri = stringBitmap[0];
                    var Setimages = MakeUri("svgimages");

                    OnStringImageSelected_Clicked?.Invoke(Setimages);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            //try
            //{
            //    var bitmap = ImageListCurrentImage[pnlPictures.SelectedIndex];
            //    var stringBitmap = ImageListCurrentTip[pnlPictures.SelectedIndex].Split('.');
            //    var bitmapMessage = string.Format("Name:{0} Height:{1} Width:{2} ", stringBitmap[0] + Environment.NewLine, stringBitmap[1] + Environment.NewLine, stringBitmap[2]);
            //    byte[] data = ImageCompression.ImageToByte(bitmap);
            //    byte[] dataCompress = ImageCompression.Compress(data);
            //    string FullNameBase = Convert.ToBase64String(dataCompress);
            //    toolTip1.Active = true;
            //    toolTip1.Show(bitmapMessage, this);
            //    toolTip1.SetToolTip(pnlPictures, bitmapMessage);
            //    //==================================================================
            //    if (OnStringImageSelected_Clicked != null)
            //    {
            //        OnStringImageSelected_Clicked(FullNameBase);
            //        this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //        this.Close();
            //    }
            //    //====================================================================================
            //    if (OnImagSelected_Clicked != null)
            //    {
            //        OnImagSelected_Clicked(bitmap);
            //        this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //        this.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            //}

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
        #region Event

       
        private void BtnAddImageFiles_Click(object sender, EventArgs e)
        {
            var frm = new FormAddImage();
            frm.ShowDialog();
        }

        private void KR_Xaml_Click(object sender, EventArgs e)
        {
            GetImageFolderName("XamlImages");
        }

        private void KR_SVG_Click(object sender, EventArgs e)
        {
            GetImageFolderName("svgimages");
        }

        private void KR_WMF_Click(object sender, EventArgs e)
        {
            GetImageFolderName("WmfImages");
        }

        private void KR_PNG_Click(object sender, EventArgs e)
        {
            GetImageFolderName("PngImages");
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
    }
}
