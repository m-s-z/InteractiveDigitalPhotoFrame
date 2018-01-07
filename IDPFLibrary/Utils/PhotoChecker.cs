using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.Utils
{
    public class PhotoChecker
    {
        private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".BMP", ".GIF", ".PNG" };
        public static bool IsImage(string name)
        {
            foreach (var extension in ImageExtensions)
            {
                string tempName = name.ToLower(); 
                if (name.EndsWith(extension.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
