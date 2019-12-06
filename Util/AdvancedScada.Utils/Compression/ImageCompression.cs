using System.Drawing;
using System.IO;
using System.IO.Compression;

namespace AdvancedScada.Utils.Compression
{
    public class ImageCompression
    {
        public static byte[] Compress(byte[] bytes)
        {
            byte[] result = null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (GZipStream g = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    g.Write(bytes, 0, bytes.Length);
                }
                result = ms.ToArray();
            }
            return result;
        }
        public static byte[] DecompressString(byte[] data)
        {
            using (MemoryStream compressedStream = new MemoryStream(data))
            using (GZipStream zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (MemoryStream resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
        public static byte[] ImageToByte(Image image)
        {
            return (byte[])(new ImageConverter()).ConvertTo(image, typeof(byte[]));
        }
        // using image object not file 
        public byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
