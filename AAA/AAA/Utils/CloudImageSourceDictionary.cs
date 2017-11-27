using System.Collections.Generic;

namespace AAA.Utils
{
    /// <summary>
    /// Contains dictionary with image source for each cloud provider.
    /// </summary>
    public class CloudImageSourceDictionary
    {
        /// <summary>
        /// Dictionary with image source for each cloud provider.
        /// </summary>
        private static readonly Dictionary<CloudType, string> CLOUD_IMAGE_SOURCE_DICTIONARY = new Dictionary<CloudType, string>
        {
            {CloudType.Dropbox, "dropbox_96px.png"},
            {CloudType.Flickr, "flickr_96px.png"},
            {CloudType.Google, "google_96px.png"},
            {CloudType.Microsoft, "microsoft_96px.png"},
            {CloudType.None, ""}
        };

        /// <summary>
        /// Gets an image source for a given cloud provider.
        /// </summary>
        /// <param name="cloudType">Type of cloud provider.</param>
        /// <returns>Image source.</returns>
        public static string GetImageSource(CloudType cloudType)
        {
            return CLOUD_IMAGE_SOURCE_DICTIONARY.TryGetValue(cloudType, out var imageSource) ? imageSource : null;
        }
    }
}