using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.Utils.Tools
{
    public delegate void GetSelectedPath(string FullName);
    internal enum Types
    {
        FILE,
        FOLDER
    }

    internal struct ItemType
    {
        public object ItemInfo;
        public Types Type;
    }

    public class SymbolsList : ListView
    {
        public BackgroundWorker bgIconLoader = new BackgroundWorker();
        private readonly ImageList il16 = new ImageList();
        private readonly ImageList il32 = new ImageList();
        private readonly List<ItemType> Paths = new List<ItemType>();
        private readonly Win32 win32 = new Win32();
        private bool use16 = true;

        public static GetSelectedPath eventSelectedPath;
        public void Load()
        {

            il16.ColorDepth = ColorDepth.Depth32Bit;
            il32.ColorDepth = ColorDepth.Depth32Bit;
            il32.ImageSize = new Size(48, 48);

            SmallImageList = il16;
            LargeImageList = il32;
            // this.View = System.Windows.Forms.View.LargeIcon;
            Activation = ItemActivation.TwoClick;
            MultiSelect = false;

            //if (this.View == System.Windows.Forms.View.Details)
            {
                Columns.Add("colName", "Name");
                Columns.Add("colType", "Type");
                Columns.Add("Size", 1, HorizontalAlignment.Right);
                Columns.Add("colDate", "Date");
            }

            bgIconLoader.WorkerReportsProgress = true;
            bgIconLoader.WorkerSupportsCancellation = true;
            bgIconLoader.DoWork += bgIconLoader_DoWork;
            bgIconLoader.ProgressChanged += bgIconLoader_ProgressChanged;
            bgIconLoader.RunWorkerCompleted += bgIconLoader_RunWorkerCompleted;

            ItemActivate += FileFolderList_ItemActivate;
            ItemSelectionChanged += FileFolderList_ItemSelectionChanged;

            Browse(_DefaultPath);


        }

        private void bgIconLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                AutoColResize();
            }));
        }

        private void bgIconLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                string fullname = (string)e.UserState;
                string name = Path.GetFileName((string)e.UserState);
                ListViewItem item = null;

                Invoke(new MethodInvoker(delegate
                {
                    item = FindItemWithText(name, false, 0, true);
                }));

                if (item != null)
                {
                    try
                    {

                        System.Diagnostics.Debug.WriteLine(fullname);

                        item.ImageKey = fullname;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        Application.DoEvents();
                    }
                }
            }
        }

        private void bgIconLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            string fullname = string.Empty;

            foreach (ItemType item in Paths)
            {
                if (bgIconLoader.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                #region // get associated icon
                if (item.Type == Types.FOLDER)
                {
                    Bitmap icon = AdvancedScada.Utils.Properties.Resources.FolderOpen_32x32_72;
                    DirectoryInfo di = (DirectoryInfo)item.ItemInfo;
                    if (use16)
                    {
                        if (!il16.Images.ContainsKey(di.FullName))
                        {
                            il16.Images.Add(di.FullName, icon);
                        }
                    }
                    else
                    {
                        Invoke(new MethodInvoker(delegate
                        {
                            if (!il32.Images.ContainsKey(di.FullName))
                            {
                                il32.Images.Add(di.FullName, icon);
                            }
                        }));
                    }

                    //this.bgIconLoader.ReportProgress(1, di.FullName);
                    fullname = di.FullName;
                }
                else
                {
                    FileInfo fi = (FileInfo)item.ItemInfo;
                    if (use16)
                    {
                        if (!il16.Images.ContainsKey(fi.FullName))
                        {
                            il16.Images.Add(fi.FullName, win32.GetIcon(fi.FullName, true));
                        }
                    }
                    else
                    {
                        Invoke(new MethodInvoker(delegate
                        {
                            if (!il32.Images.ContainsKey(fi.FullName))
                            {
                                il32.Images.Add(fi.FullName, win32.GetIcon(fi.FullName, false));
                            }
                        }));
                    }

                    //this.bgIconLoader.ReportProgress(1, fi.FullName);
                    fullname = fi.FullName;
                }
                #endregion

                #region // add to listview
                string name = Path.GetFileName(fullname);
                ListViewItem lvItem = null;

                Invoke(new MethodInvoker(delegate
                {
                    lvItem = FindItemWithText(name, false, 0, true);
                }));

                if (lvItem != null)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        lvItem.ImageKey = fullname;

                        //if (View == System.Windows.Forms.View.Details)
                        {
                            if (item.Type == Types.FILE)
                            {
                                lvItem.SubItems[1].Text = win32.GetFileType(fullname);
                                lvItem.SubItems[2].Text = win32.GetFileSize(fullname);
                            }
                        }
                    }));
                }
                #endregion
            }
        }

        private void FileFolderList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (SelectedItems.Count <= 0) { return; }

            //try
            {
                ListViewItem item = SelectedItems[0];
                ItemType type = (ItemType)item.Tag;

                if (type.Type == Types.FOLDER)
                {
                    DirectoryInfo di = (DirectoryInfo)type.ItemInfo;
                    _SelectedPath = di.FullName;
                }
                else
                {
                    FileInfo fi = (FileInfo)type.ItemInfo;
                    _SelectedPath = fi.FullName;
                    if (eventSelectedPath != null)
                    {
                        eventSelectedPath(fi.FullName);
                    }
                }
            }
            //catch { }
        }


        public void FileFolderList_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (SelectedItems.Count <= 0)
                {
                    return;
                }

                ListViewItem thisItem = SelectedItems[0];
                ItemType type = (ItemType)thisItem.Tag;

                if (type.Type == Types.FOLDER)
                {
                    DirectoryInfo di = (DirectoryInfo)type.ItemInfo;
                    Browse(di.FullName);
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

        }

        public void Browse(string path)
        {
            try
            {
                bgIconLoader.CancelAsync();
                ListViewItem item = null;
                if (View == System.Windows.Forms.View.LargeIcon || View == System.Windows.Forms.View.Tile)
                {
                    use16 = false;
                }

                while (bgIconLoader.IsBusy)
                {
                    Application.DoEvents();
                }

                Paths.Clear();
                Items.Clear();

                BeginUpdate();

                #region // add "back" item if necessary

                if (_isSoloBrowser)
                {
                    DirectoryInfo currentPath = new DirectoryInfo(path);
                    if (currentPath.FullName.Length > 3)
                    {

                        item = new ListViewItem("...", 0)
                        {
                            Tag = new ItemType
                            {
                                ItemInfo = currentPath.Parent,
                                Type = Types.FOLDER
                            }
                        };

                        Items.Add(item);
                    }
                }

                #endregion

                #region // get folders

                foreach (string folder in Directory.GetDirectories(path))
                {
                    DirectoryInfo di = new DirectoryInfo(folder);

                    if (di.Attributes.ToString().Contains("System"))
                    {
                        continue;
                    }

                    item = new ListViewItem(di.Name)
                    {
                        ImageKey = di.FullName,
                        Tag = new ItemType
                        {
                            ItemInfo = di,
                            Type = Types.FOLDER
                        }
                    };

                    // add temp subitems if View was set to Details
                    //if (this.View == System.Windows.Forms.View.Details)
                    {
                        for (int i = 0; i < Columns.Count; i++)
                        {
                            item.SubItems.Add(string.Empty);
                        }

                        item.SubItems[3].Text = di.CreationTime.ToString();
                        // key should be "colType" but am not sure
                        // why it does not work
                        item.SubItems[1].Text = "File folder";
                    }

                    Items.Add(item);

                    Paths.Add((ItemType)item.Tag);
                }

                #endregion

                #region // get files

                foreach (string file in Directory.GetFiles(path))
                {
                    FileInfo fi = new FileInfo(file);

                    if (fi.Attributes.ToString().Contains("System"))
                    {
                        continue;
                    }
                    if (file.EndsWith(".svg"))
                    {
                        SvgDocument svgDocument = SvgDocument.Open(fi.FullName);
                        item = new ListViewItem(fi.Name);
                        Bitmap bitmap = svgDocument.Draw();
                        il32.Images.Add(fi.FullName, bitmap);
                        svgDocument = null;
                    }
                    else
                    {
                        item = new ListViewItem(fi.Name);
                        Image bitmap = Image.FromFile(fi.FullName);
                        il32.Images.Add(fi.FullName, bitmap);
                    }

                    item.ImageKey = fi.FullName;
                    item.Tag = new ItemType()
                    {
                        ItemInfo = fi,
                        Type = Types.FILE
                    };

                    // add temp subitems if View was set to Details
                    //if (this.View == System.Windows.Forms.View.Details)
                    {
                        for (int i = 0; i < Columns.Count; i++)
                        {
                            item.SubItems.Add(string.Empty);
                        }

                        item.SubItems[3].Text = fi.CreationTime.ToString();
                    }

                    Items.Add(item);

                    Paths.Add((ItemType)item.Tag);

                }

                #endregion

                EndUpdate();
                Refresh();

                Application.DoEvents();

                bgIconLoader.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);

            }




        }

        private void AutoColResize()
        {
            try
            {
                foreach (ColumnHeader col in Columns)
                {
                    //this.Invoke(new MethodInvoker(delegate
                    //{
                    col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                //col.Width = -2;
                //}));
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

        }

        #region Properties

        private string _DefaultPath = "C:\\";

        public string DefaultPath
        {
            get => _DefaultPath;
            set => _DefaultPath = value;
        }

        private string _SelectedPath = string.Empty;

        public string SelectedPath => _SelectedPath;

        private bool _isSoloBrowser = true;

        public bool isSoloBrowser
        {
            get => _isSoloBrowser;
            set => _isSoloBrowser = value;
        }

        #endregion

        ~SymbolsList()
        {
            bgIconLoader.Dispose();
            bgIconLoader = null;
        }

    }

    internal class Win32
    {
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1; // 'Small icon

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbSizeFileInfo,
            uint uFlags);

        [DllImport("user32")]
        public static extern int DestroyIcon(IntPtr hIcon);

        public string GetFileSize(string fullpath)
        {
            FileInfo fi = new FileInfo(fullpath);
            long size = fi.Length;
            string sizeString = string.Format(new FileSizeFormatProvider(), "{0:fs}", size);

            return sizeString;
        }

        public string GetFileType(string fullpath)
        {
            uint dwFileAttributes = FILE_ATTRIBUTE.FILE_ATTRIBUTE_NORMAL;
            uint uFlags = SHGFI.SHGFI_TYPENAME | SHGFI.SHGFI_USEFILEATTRIBUTES;
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr n = SHGetFileInfo(fullpath, dwFileAttributes, ref shinfo, (uint)Marshal.SizeOf(shinfo), uFlags);

            return shinfo.szTypeName;
        }

        public Icon GetIcon(string fullpath, bool use16)
        {
            Icon ico = null;
            IntPtr hImgSmall;
            IntPtr hImgLarge;
            SHFILEINFO shinfo = new SHFILEINFO();

            if (use16)
            {
                //Use this to get the small Icon
                hImgSmall = SHGetFileInfo(fullpath, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                    SHGFI_ICON | SHGFI_SMALLICON);
                ico = Icon.FromHandle(shinfo.hIcon);
            }
            else
            {
                //Use this to get the large Icon
                hImgLarge = SHGetFileInfo(fullpath, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                    SHGFI_ICON | SHGFI_LARGEICON);
                ico = Icon.FromHandle(shinfo.hIcon);
            }

            return ico;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public readonly IntPtr hIcon;
            public readonly IntPtr iIcon;
            public readonly uint dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public readonly string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public readonly string szTypeName;
        }

        private static class FILE_ATTRIBUTE
        {
            public const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        }

        private static class SHGFI
        {
            public const uint SHGFI_TYPENAME = 0x000000400;
            public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        }
    }

    internal class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        private const string fileSizeFormat = "fs";
        private const decimal OneKiloByte = 1024M;
        private const decimal OneMegaByte = OneKiloByte * 1024M;
        private const decimal OneGigaByte = OneMegaByte * 1024M;

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (format == null || !format.StartsWith(fileSizeFormat))
            {
                return defaultFormat(format, arg, formatProvider);
            }

            if (arg is string)
            {
                return defaultFormat(format, arg, formatProvider);
            }

            decimal size;

            try
            {
                size = Convert.ToDecimal(arg);
            }
            catch (InvalidCastException)
            {
                return defaultFormat(format, arg, formatProvider);
            }

            string suffix;
            if (size > OneGigaByte)
            {
                size /= OneGigaByte;
                suffix = " GB";
            }
            else if (size > OneMegaByte)
            {
                size /= OneMegaByte;
                suffix = " MB";
            }
            else if (size > OneKiloByte)
            {
                size /= OneKiloByte;
                suffix = " KB";
            }
            else
            {
                suffix = " Byte(s)";
            }

            string precision = format.Substring(2);
            if (string.IsNullOrEmpty(precision))
            {
                precision = "0";
            }

            return string.Format("{0:N" + precision + "}{1}", size, suffix);
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }

        private static string defaultFormat(string format, object arg, IFormatProvider formatProvider)
        {
            IFormattable formattableArg = arg as IFormattable;
            if (formattableArg != null)
            {
                return formattableArg.ToString(format, formatProvider);
            }

            return arg.ToString();
        }
    }
}