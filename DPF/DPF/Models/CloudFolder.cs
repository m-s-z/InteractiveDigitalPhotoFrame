namespace DPF.Models
{
    /// <summary>
    /// CloudFolder class.
    /// </summary>
    public class CloudFolder
    {
        #region fields

        /// <summary>
        /// Backing field of PhotosNumber property.
        /// </summary>
        private int _photosNumber;

        /// <summary>
        /// Backing field of FolderName property.
        /// </summary>
        private string _folderName;

        #endregion

        #region properties

        /// <summary>
        /// Property indicating number of photos in the folder.
        /// </summary>
        public int PhotosNumber
        {
            get => _photosNumber;
            set => _photosNumber = value;
        }

        /// <summary>
        /// Property indicating name of the folder.
        /// </summary>
        public string FolderName
        {
            get => _folderName;
            set => _folderName = value;
        }

        #endregion

        #region methods

        /// <summary>
        /// CloudFolder class constructor.
        /// Updates FolderName and PhotosNumber properties.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="photosNumber">Number of photos in the folder.</param>
        public CloudFolder(string folderName, int photosNumber)
        {
            FolderName = folderName;
            PhotosNumber = photosNumber;
        }

        #endregion
    }
}