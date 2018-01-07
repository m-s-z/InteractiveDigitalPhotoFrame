using IDPFLibrary.DTO;

namespace DPF.Models
{
    /// <summary>
    /// Delegate for the error occured event.
    /// </summary>
    /// <param name="sender">Instance of the object which invokes event.</param>
    /// <param name="errorMessage">Message of the error.</param>
    public delegate void ErrorOccurredDelegate(object sender, string errorMessage);

    /// <summary>
    /// Delegate for the synchronization completed event.
    /// </summary>
    /// <param name="sender">Instance of the object which invokes event.</param>
    /// <param name="newPhotoset">Collection of new photos.</param>
    public delegate void SynchronizationCompletedDelegate(object sender, GetAllFlickrPhotosURLResponseDTO newPhotoset);

    /// <summary>
    /// Local storage service interface.
    /// </summary>
    public interface ILocalStorageService
    {
        #region properties

        /// <summary>
        /// Event invoked whenever error occured.
        /// </summary>
        event ErrorOccurredDelegate ErrorOccured;

        /// <summary>
        /// Event invoked when the synchronization process ends.
        /// </summary>
        event SynchronizationCompletedDelegate SynchronizationCompleted;

        #endregion

        #region methods

        /// <summary>
        /// Creates folders in the storage.
        /// </summary>
        void CreateImagesFolder();

        /// <summary>
        /// Reads a list of connected accounts from the storage,
        /// </summary>
        /// <returns>Json with list of connected accounts.</returns>
        string GetConnectedAccounts();

        /// <summary>
        /// Reads a device token from the storage,
        /// </summary>
        /// <returns>Json with a device token.</returns>
        string GetDeviceToken();

        /// <summary>
        /// Reads a list of synchronized photos from the storage,
        /// </summary>
        /// <returns>Json with list of synchronized photos.</returns>
        string GetPhotoset();

        /// <summary>
        /// Gets a path to a photo to display.
        /// </summary>
        /// <param name="imageToShow">Indicates the photo to find.</param>
        /// <returns>Path to the photo to display.</returns>
        string GetImageToShow(Urls imageToShow);

        /// <summary>
        /// Saves a list of connected accounts in the storage.
        /// </summary>
        /// <param name="json">Json with list of connected accounts.</param>
        void SaveConnectedAccounts(string json);

        /// <summary>
        /// Saves a device token in the storage.
        /// </summary>
        /// <param name="json">Json with a device token.</param>
        void SaveDeviceToken(string json);

        /// <summary>
        /// Saves a list of synchronized photos in the storage.
        /// </summary>
        /// <param name="json">Json with list of synchronized photos.</param>
        void SavePhotoset(string json);

        /// <summary>
        /// Saves new photos and removes outdated ones.
        /// </summary>
        /// <param name="newPhotoset">List of new photos.</param>
        /// <param name="oldPhotoset">List of old photos.</param>
        void SynchronizeImages(GetAllFlickrPhotosURLResponseDTO newPhotoset,
            GetAllFlickrPhotosURLResponseDTO oldPhotoset);

        #endregion
    }
}