using AdvancedScada.DriverBase.Client;
using AdvancedScada.Utils.Compression;
using AdvancedScada.WPF.HMIControls.Comm;
using Svg;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AdvancedScada.WPF.HMIControls.ImageAll
{
    public class HMIImageContainerSvg : Control
    {
        public HMIImageContainerSvg()
        {
            this.ClipToBounds = true;
            this.SnapsToDevicePixels = true;
        }
        #region Property PLC
        public static readonly DependencyProperty PLCAddressValueSelect1Property = DependencyProperty.Register(
           "PLCAddressValueSelect1", typeof(string), typeof(HMIImageContainerSvg), new FrameworkPropertyMetadata("0"));

        [Category("HMI")]
        public string PLCAddressValueSelect1
        {
            get
            {
                return (string)base.GetValue(PLCAddressValueSelect1Property);
            }
            set
            {
                base.SetValue(PLCAddressValueSelect1Property, value);



            }
        }
        [Category("HMI")]
        public string GraphicAllOff
        {
            get { return (string)GetValue(GraphicAllOffProperty); }
            set { SetValue(GraphicAllOffProperty, value); }
        }
        SvgDocument svgGraphicAllOff = null;
        public static readonly DependencyProperty GraphicAllOffProperty =
            DependencyProperty.Register("GraphicAllOff", typeof(string), typeof(HMIImageContainerSvg),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnGraphicAllOffChanged)));

        [Category("HMI")]
        public string GraphicSelect1
        {
            get { return (string)GetValue(GraphicSelect1Property); }
            set { SetValue(GraphicSelect1Property, value); }
        }
        SvgDocument svgGraphicSelect1 = null;
        public static readonly DependencyProperty GraphicSelect1Property =
            DependencyProperty.Register("GraphicSelect1", typeof(string), typeof(HMIImageContainerSvg),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnGraphicSelect1Changed)));

        private static void OnGraphicSelect1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HMIImageContainerSvg)d).svgGraphicSelect1 = SvgDocument.FromSvg<SvgDocument>(StringCompression.Decompress($"{e.NewValue}"));
            ((HMIImageContainerSvg)d).m_GraphicSelect1 = ((HMIImageContainerSvg)d).svgGraphicSelect1.Draw();
            ((HMIImageContainerSvg)d).Image3 = ((HMIImageContainerSvg)d).LoadImage(((HMIImageContainerSvg)d).ImageToByteArray(((HMIImageContainerSvg)d).m_GraphicSelect1));

        }

        [Category("HMI")]
        public string GraphicSelect2
        {
            get { return (string)GetValue(GraphicSelect2Property); }
            set { SetValue(GraphicSelect2Property, value); }
        }
        SvgDocument svgGraphicSelect2 = null;
        public static readonly DependencyProperty GraphicSelect2Property =
            DependencyProperty.Register("GraphicSelect2", typeof(string), typeof(HMIImageContainerSvg),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnGraphicSelect2Changed)));

        private static void OnGraphicSelect2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HMIImageContainerSvg)d).svgGraphicSelect2 = SvgDocument.FromSvg<SvgDocument>(StringCompression.Decompress($"{e.NewValue}"));
            ((HMIImageContainerSvg)d).m_GraphicSelect2 = ((HMIImageContainerSvg)d).svgGraphicSelect2.Draw();
            ((HMIImageContainerSvg)d).Image2 = ((HMIImageContainerSvg)d).LoadImage(((HMIImageContainerSvg)d).ImageToByteArray(((HMIImageContainerSvg)d).m_GraphicSelect2));

        }

        static void OnGraphicAllOffChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HMIImageContainerSvg)d).svgGraphicAllOff = SvgDocument.FromSvg<SvgDocument>(StringCompression.Decompress($"{e.NewValue}"));
            ((HMIImageContainerSvg)d).m_GraphicAllOff = ((HMIImageContainerSvg)d).svgGraphicAllOff.Draw();
            ((HMIImageContainerSvg)d).Image1 = ((HMIImageContainerSvg)d).LoadImage(((HMIImageContainerSvg)d).ImageToByteArray(((HMIImageContainerSvg)d).m_GraphicAllOff));
        }
        private Bitmap m_GraphicAllOff;
        private Bitmap m_GraphicSelect2;
        private Bitmap m_GraphicSelect1;


        private ImageSource Image1;
        private ImageSource Image2;
        private ImageSource Image3;
        #endregion
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
            return image;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            double bevel = height * 0.1;
            if (this.Background != null)
                drawingContext.DrawRectangle(this.Background, null, new Rect(0, 0, this.ActualWidth, this.ActualHeight));


            if (ValueSelect1)
            {
                if (m_GraphicSelect1 != null)
                {

                    svgGraphicSelect1.Width = (int)width;
                    svgGraphicSelect1.Height = (int)height;

                    m_GraphicSelect1 = svgGraphicSelect1.Draw();
                    Image2 = LoadImage(ImageToByteArray(m_GraphicSelect1));
                    drawingContext.DrawImage(Image2, new Rect(0, 0, width, height));

                }
            }
            else if (ValueSelect2)
            {
                if (m_GraphicSelect2 != null)
                {

                    svgGraphicSelect2.Width = (int)width;
                    svgGraphicSelect2.Height = (int)height;

                    m_GraphicSelect2 = svgGraphicSelect2.Draw();
                    Image3 = LoadImage(ImageToByteArray(m_GraphicSelect2));
                    drawingContext.DrawImage(Image3, new Rect(0, 0, width, height));

                }
            }
            else
            {

                if (m_GraphicAllOff != null)
                {

                    svgGraphicAllOff.Width = (int)width;
                    svgGraphicAllOff.Height = (int)height;

                    m_GraphicAllOff = svgGraphicAllOff.Draw();
                    Image1 = LoadImage(ImageToByteArray(m_GraphicAllOff));
                    drawingContext.DrawImage(Image1, new Rect(0, 0, width, height));

                }

            }


        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext g = drawingVisual.RenderOpen();

            double width = this.ActualWidth;
            double height = this.ActualHeight;

            if (ValueSelect1)
            {
                if (Image2 != null) g.DrawImage(Image2, new Rect(0, 0, width, height));
            }
            else if (ValueSelect2)
            {
                if (Image3 != null) g.DrawImage(Image3, new Rect(0, 0, width, height));
            }
            else
            {
                if (Image1 != null) g.DrawImage(Image1, new Rect(0, 0, width, height));
            }
            g.Close();
            try
            {
                //* When address is changed, re-subscribe to new address
                if (string.IsNullOrEmpty(PLCAddressValueSelect1) || string.IsNullOrWhiteSpace(PLCAddressValueSelect1) ||
                           LicenseHMI.IsInDesignMode) return;
                Binding binding = new Binding("Value");
                binding.Source = TagCollectionClient.Tags[PLCAddressValueSelect1];
                this.SetBinding(ValueSelect1Property, binding);


            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
            }

        }
        private void DisplayError(string message)
        {

        }

        #region Property


        public bool ValueSelect1
        {
            get { return (bool)GetValue(ValueSelect1Property); }
            set { SetValue(ValueSelect1Property, value); }
        }

        // Using a DependencyProperty as the backing store for ValueSelect1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueSelect1Property =
            DependencyProperty.Register("ValueSelect1", typeof(bool), typeof(HMIImageContainerSvg), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public bool ValueSelect2
        {
            get { return (bool)GetValue(ValueSelect2Property); }
            set { SetValue(ValueSelect2Property, value); }
        }

        // Using a DependencyProperty as the backing store for ValueSelect1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueSelect2Property =
            DependencyProperty.Register("ValueSelect2", typeof(bool), typeof(HMIImageContainerSvg), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion
    }
}
