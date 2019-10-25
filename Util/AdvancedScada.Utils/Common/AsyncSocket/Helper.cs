using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common.AsyncSocket
{
    static class Helper
    {
        public static void SubBytes(this MemoryStream ms, int startposition)
        {
            ms.Position = 0;
            byte[] temp = new byte[ms.Length - startposition];
            ms.Position = startposition;
            ms.Read(temp, 0, temp.Length);
            ms.SetLength(0);
            ms.Write(temp, 0, temp.Length);
        }
    }
}
