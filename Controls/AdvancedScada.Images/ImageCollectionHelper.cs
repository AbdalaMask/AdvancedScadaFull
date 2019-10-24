using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace AdvancedScada.Images
{
    public class ImageCollectionHelper
    {
        internal const string PackPrefix = "pack://application:,,,";
        static Dictionary<ImageType, string> folders = new Dictionary<ImageType, string>() {
            {ImageType.Colored, "Images"},
            {ImageType.GrayScaled, "GrayScaleImages"},
            {ImageType.Office2013, "Office2013"},
            {ImageType.DevAV, "DevAV"},
            {ImageType.Svg, "SvgImages"}
        };
        internal static ImageType[] IncompleteFolderKeys = new ImageType[] { ImageType.DevAV };
        public ImageCollectionHelper()
        {

        }
        public ImageCollectionHelper(string group, ImageType imageType, string name)
        {
            Group = group;
            ImageType = imageType;
            Name = name;
            
        }
        public string Group { get; private set; }
        public ImageType ImageType { get; private set; }
        public string Name { get; private set; }
        public ImageSize Size { get; private set; }
        public string[] Tags { get; private set; }
        public static bool IsIncompleteFolder(string item)
        {
            return false;
        }
        static IList<string> folderList = null;
        static IList<string> IncompleteFolderList
        {
            get
            {
                if (folderList == null)
                {
                    folderList = folders.Where(element => IncompleteFolderKeys.Contains(element.Key)).Select(element => element.Value).ToList();
                }
                return folderList;
            }
        }
        public static string GetImageFolderName(ImageType imageType)
        {
            return folders[imageType];
        }
        public static ImageType? GetImageTypeByFolderName(string folderName)
        {
            var pair = folders.FirstOrDefault(x => x.Value == folderName);
            return pair.Value != folderName ? (ImageType?)null : pair.Key;
        }
        public static ImageType GetImageType(string key)
        {
            var pair = folders.FirstOrDefault(x => x.Key != ImageType.Colored && key.IndexOf(x.Value, StringComparison.OrdinalIgnoreCase) >= 0);
            return pair.Value == null || key.IndexOf(pair.Value, StringComparison.OrdinalIgnoreCase) < 0 ? ImageType.Colored : pair.Key;
        }
        internal static int ImagesCountForName { get { return (folders.Keys.Except(IncompleteFolderKeys).Count() - 1) * 2; } }
        public static readonly string ResourceName = "AdvancedScada.Images.g.resources";


        public string MakeUri()
        {
            return string.Format(PackPrefix + "/{0};component/{1}", "AdvancedScada.Images", MakeUriShort());
        }
        internal string MakeUriShort()
        {
            return string.Format("{1}/{2}/{3}", "AdvancedScada.Images", ImageCollectionHelper.GetImageFolderName(ImageType), Group, Name);
        }
    }
}
