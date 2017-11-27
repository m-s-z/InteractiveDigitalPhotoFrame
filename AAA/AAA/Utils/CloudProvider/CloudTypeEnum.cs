namespace AAA.Utils.CloudProvider
{
    /// <summary>
    /// Contains all supported types of cloud providers.
    /// </summary>
    public enum CloudTypeEnum
    {
        /// <summary>
        /// Dropbox cloud provider.
        /// </summary>
        Dropbox,

        /// <summary>
        /// Flickr cloud provider.
        /// </summary>
        Flickr,

        /// <summary>
        /// Google cloud provider.
        /// </summary>
        GoogleDrive,

        /// <summary>
        /// Microsoft cloud provider.
        /// </summary>
        OneDrive,

        /// <summary>
        /// Cloud provider is not specified.
        /// </summary>
        None
    }
}