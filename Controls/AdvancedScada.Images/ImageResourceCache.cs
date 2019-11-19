using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Security;

namespace AdvancedScada.Images
{

    public class ImageResourceCache
    {
        private static readonly object mutex = new object();
        readonly Dictionary<string, Stream> resources;
        readonly IDictionary<string, Stream> resourcesByFileName;
        private static ImageResourceCache _instance;
        public ImageResourceCache()
        {
            resourcesByFileName = new Dictionary<string, Stream>(64, StringComparer.OrdinalIgnoreCase);
            resources = new Dictionary<string, Stream>(64, StringComparer.OrdinalIgnoreCase);
        }
        public static ImageResourceCache GetChannelManager()
        {
            lock (mutex)
            {
                if (_instance == null) _instance = new ImageResourceCache();
            }

            return _instance;
        }
        public ResXResourceReader GetImages(string resourceName)
        {
            if (!this.resources.ContainsKey(resourceName))
            {
                return null;
            }
            ResXResourceReader rsxr = new ResXResourceReader(this.resources[resourceName]);
            return rsxr;
        }
        public Stream GetResource(string resourceName)
        {
            Stream result = null;
            return (resourceName != null) && resources.TryGetValue(resourceName, out result) ? result : null;
        }
        internal ICollection GetKeys() { return resources.Keys; }
        public string[] GetAllResourceKeys()
        {
            return resources.Keys.Count == 0 ? null : resources.Keys.ToArray();
        }

        static ImageResourceCache defaultCore = null;
        public static ImageResourceCache Default(string ImageType)
        {

            if (defaultCore == null) defaultCore = DoLoad(ImageType);
            return defaultCore;

        }

        readonly static char[] splitCharacters = new char[] { '\\', '/' };
        [System.Runtime.CompilerServices.MethodImpl(256)]
        public static string[] Split(string key)
        {
            return key.Split(splitCharacters);
        }
        [SecuritySafeCritical]
        public static ImageResourceCache DoLoad(string ImageType)
        {
            ImageResourceCache cache = GetChannelManager();
            cache.resources.Clear(); cache.resourcesByFileName.Clear();
            using (ResourceReader reader = DoLoadResourceReader())
            {
                IDictionaryEnumerator e = reader.GetEnumerator();
                string[] parts; string key, category;
                while (e.MoveNext())
                {
                    key = e.Key as string;
                    parts = Split(key);
                    if (parts[0] == ImageType.ToLower())
                    {
                        cache.resources.Add(key, (Stream)e.Value);
                        category = parts[1];
                        key = parts[0] + @"\" + parts[parts.Length - 1];
                        if (!cache.resourcesByFileName.ContainsKey(key))
                            cache.resourcesByFileName.Add(key, (Stream)e.Value);
                        continue;
                    }



                }
            }
            return cache;
        }
        public static readonly string ResourceName = "AdvancedScada.Images.g.resources";

        public static readonly Assembly ImagesAssembly = Assembly.GetExecutingAssembly();
        [SecuritySafeCritical]
        static ResourceReader DoLoadResourceReader()
        {
            List<string> resources = new List<string>(AssemblyBuilder.GetExecutingAssembly().GetManifestResourceNames());
            return new ResourceReader(ImagesAssembly.GetManifestResourceStream(ResourceName));
        }
    }
}
