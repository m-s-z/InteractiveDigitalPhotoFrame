using System.Collections.Generic;

namespace IDPFLibrary.Utils
{
    /// <summary>
    /// PhotoChecker class.
    /// Provides method to check whether a file is an image or not.
    /// </summary>
    public class PhotoChecker
    {
        #region fields

        /// <summary>
        /// List of supported extensions.
        /// </summary>
        private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".BMP", ".GIF", ".PNG" };

        #endregion

        #region methods

        /// <summary>
        /// Checks whether a file is an image or not.
        /// </summary>
        /// <param name="name">Name of the image.</param>
        /// <returns>True if is an image, false otherwise.</returns>
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

        #endregion
    }
}
