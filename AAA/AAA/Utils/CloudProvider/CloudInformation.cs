namespace AAA.Utils.CloudProvider
{
    /// <summary>
    /// CloudInformation class.
    /// Contains information about cloud provider.
    /// </summary>
    public class CloudInformation
    {
        #region properties

        /// <summary>
        /// Property with path to cloud's logo image.
        /// </summary>
        public string CloudLogoPath { get; }

        /// <summary>
        /// Property with name of the cloud.
        /// </summary>
        public string CloudName { get; }

        /// <summary>
        /// Property containing type of cloud provider.
        /// </summary>
        public CloudTypeEnum CloudType { get; }

        #endregion

        #region methods

        /// <summary>
        /// Default CloudInformation class constructor.
        /// Used if information about cloud is undefined.
        /// </summary>
        public CloudInformation()
        {
            CloudType = CloudTypeEnum.None;
            CloudName = "Cloud undefined";
            CloudLogoPath = "";
        }

        /// <summary>
        /// CloudInformation class constructor.
        /// </summary>
        /// <param name="cloudType">Type of cloud provider.</param>
        /// <param name="cloudName">Name of cloud.</param>
        /// <param name="cloudLogoPath">Path to cloud's logo image.</param>
        public CloudInformation(CloudTypeEnum cloudType, string cloudName, string cloudLogoPath)
        {
            CloudType = cloudType;
            CloudName = cloudName;
            CloudLogoPath = cloudLogoPath;
        }

        #endregion
    }
}