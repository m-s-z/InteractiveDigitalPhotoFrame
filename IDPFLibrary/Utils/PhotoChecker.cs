using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.Utils
{
    public class PhotoChecker
    {
        private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public static bool IsImage(string name)
        {
            foreach (var extension in ImageExtensions)
            {
                if (name.EndsWith(extension))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
