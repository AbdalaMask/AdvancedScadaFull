using AdvancedScada.Utils.Compression;
using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace HslScada.Controls
{
    public class HMIImageContainer : Control
    {
        public HMIImageContainer()
        {
            this.ClipToBounds = true;
            this.SnapsToDevicePixels = true;
        }
        [Category("HMI")]
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        SvgDocument svgGraphicAllOff = null;
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(HMIImageContainer),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnSourceChanged)));
        static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HMIImageContainer)d).svgGraphicAllOff = SvgDocument.FromSvg<SvgDocument>(StringCompression.Decompress($"{e.NewValue}"));
            ((HMIImageContainer)d).m_GraphicAllOff = ((HMIImageContainer)d).svgGraphicAllOff.Draw();
            ((HMIImageContainer)d).LoadImage(((HMIImageContainer)d).ImageToByteArray(((HMIImageContainer)d).m_GraphicAllOff));
        }
        private Bitmap m_GraphicAllOff;


        
        public byte[] ImageToByteArray(System.Drawing.Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }
        private BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();

            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }

            image.Freeze();
            imageSource = image;
            return image;
        }
        private ImageSource imageSource;
        protected override void OnRender(DrawingContext drawingContext)
        {
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            double bevel = height * 0.1;
            if (this.Background != null)
                drawingContext.DrawRectangle(this.Background, null, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            if (imageSource != null)
            {
                if (m_GraphicAllOff != null)
                {

                    svgGraphicAllOff.Width = (int)width;
                    svgGraphicAllOff.Height = (int)height;

                    m_GraphicAllOff = svgGraphicAllOff.Draw();
                    imageSource = LoadImage(ImageToByteArray(m_GraphicAllOff));
                    drawingContext.DrawImage(imageSource, new Rect(0, 0, width, height));

                }

            }




        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            //var b = ImageCompression.DecompressString(Convert.FromBase64String(Source));
            // LoadImage(b);

        }



    }
}
