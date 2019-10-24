using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.Images
{
    public enum ImageType
    {
        Colored,
        GrayScaled,
        Office2013,
        DevAV, 
        Svg
    }
    public enum ImageSize
    {
        
    }
    public class ImageResourceCache
    {
        readonly Dictionary<string, Stream> resources;
        readonly IDictionary<string, Stream> resourcesByFileName;
        public ImageResourceCache()
        {
            resourcesByFileName = new Dictionary<string, Stream>(64, StringComparer.OrdinalIgnoreCase);
            resources = new Dictionary<string, Stream>(64, StringComparer.OrdinalIgnoreCase);
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
        public  ImageResourceCache Default(string ImageType)
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
        public static ImageResourceCache DoLoad( string ImageType)
        {
            ImageResourceCache cache = new ImageResourceCache();
            using (ResourceReader reader = DoLoadResourceReader())
            {
                IDictionaryEnumerator e = reader.GetEnumerator();
                string[] parts; string key, fileName, category;
                while (e.MoveNext())
                {
                      key = e.Key as string;
                        parts = Split(key);
                    if(parts[0]== ImageType)
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
        public static readonly Assembly ImagesAssembly = Assembly.GetExecutingAssembly();
        [SecuritySafeCritical]
        static ResourceReader DoLoadResourceReader()
        {
            List<string> resources = new List<string>(AssemblyBuilder.GetExecutingAssembly().GetManifestResourceNames());
            return new ResourceReader(ImagesAssembly.GetManifestResourceStream(ImageCollectionHelper.ResourceName));
        }
    }
}
