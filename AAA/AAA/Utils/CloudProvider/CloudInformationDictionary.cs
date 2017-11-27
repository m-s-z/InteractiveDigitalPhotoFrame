using System.Collections.Generic;

namespace AAA.Utils.CloudProvider
{
    /// <summary>
    /// CloudInformationDictionary class.
    /// Contains dictionary with information about each cloud provider.
    /// </summary>
    public class CloudInformationDictionary
    {

        /// <summary>
        /// Dictionary with image source for each cloud provider.
        /// </summary>
        private static readonly Dictionary<CloudTypeEnum, CloudInformation> CLOUD_INFORMATION_DICTIONARY = new Dictionary<CloudTypeEnum, CloudInformation>
        {
            {CloudTypeEnum.Dropbox, new CloudInformation(CloudTypeEnum.Dropbox, "Dropbox", "dropbox_96px.png")},
            {CloudTypeEnum.Flickr, new CloudInformation(CloudTypeEnum.Flickr, "Flickr", "flickr_96px.png")},
            {CloudTypeEnum.GoogleDrive, new CloudInformation(CloudTypeEnum.GoogleDrive, "Google Drive", "google_96px.png")},
            {CloudTypeEnum.OneDrive, new CloudInformation(CloudTypeEnum.OneDrive, "OneDrive", "microsoft_96px.png")},
            {CloudTypeEnum.None, new CloudInformation()}
        };

        /// <summary>
        /// Gets information about given cloud provider.
        /// </summary>
        /// <param name="cloudType">Type of cloud provider.</param>
        /// <returns>Class with information about given cloud provider.</returns>
        public static CloudInformation GetCloudInformation(CloudTypeEnum cloudType)
        {
            return CLOUD_INFORMATION_DICTIONARY.TryGetValue(cloudType, out var cloudInformation) ? cloudInformation : null;
        }
    }
}