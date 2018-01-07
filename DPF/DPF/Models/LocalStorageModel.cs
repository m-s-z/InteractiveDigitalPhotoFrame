using System.Collections.Generic;
using IDPFLibrary.DTO;
using Xamarin.Forms;

namespace DPF.Models
{
    /// <summary>
    /// LocalStorageModel class.
    /// Provides methods to read from and write to internal storage. 
    /// </summary>
    public class LocalStorageModel
    {
        #region fields

        /// <summary>
        /// Instance of the LocalStorageService class.
        /// </summary>
        private ILocalStorageService _service;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked whenever error occurred.
        /// </summary>
        public event ErrorOccurredDelegate ErrorOccured;

        /// <summary>
        /// Event invoked when the synchronization process ends.
        /// </summary>
        public event SynchronizationCompletedDelegate SynchronizationCompleted;

        #endregion

        #region methods

        /// <summary>
        /// LocalStorageModel class constructor.
        /// </summary>
        public LocalStorageModel()
        {
            _service = DependencyService.Get<ILocalStorageService>();
            _service.ErrorOccured += ErrorOccuredEventHandler;
            _service.SynchronizationCompleted += SynchronizationCompletedEventHandler;
        }

        /// <summary>
        /// Calls service to create folders in the storage.
        /// </summary>
        public void CreateImagesFolder()
        {
            _service.CreateImagesFolder();
        }

        /// <summary>
        /// Calls service to read a list of connected accounts from the storage,
        /// </summary>
        /// <returns>Json with list of connected accounts.</returns>
        public string GetConnectedAccounts()
        {
            return _service.GetConnectedAccounts();
        }

        /// <summary>
        /// Calls service to read a device token from the storage,
        /// </summary>
        /// <returns>Json with a device token.</returns>
        public string GetDeviceToken()
        {
            return _service.GetDeviceToken();
        }

        /// <summary>
        /// Calls service to read a list of synchronized photos from the storage,
        /// </summary>
        /// <returns>Json with list of synchronized photos.</returns>
        public string GetPhotoset()
        {
            return _service.GetPhotoset();
        }

        /// <summary>
        /// Calls service to get a path to a photo to display.
        /// </summary>
        /// <param name="imageToShow">Indicates the photo to find.</param>
        /// <returns>Path to the photo to display.</returns>
        public string GetImageToShow(Urls imageToShow)
        {
            return _service.GetImageToShow(imageToShow);
        }

        /// <summary>
        /// Calls service to save a list of connected accounts in the storage.
        /// </summary>
        /// <param name="json">Json with list of connected accounts.</param>
        public void SaveConnectedAccounts(string json)
        {
            _service.SaveConnectedAccounts(json);
        }

        /// <summary>
        /// Calls service to save a device token in the storage.
        /// </summary>
        /// <param name="json">Json with a device token.</param>
        public void SaveDeviceToken(string json)
        {
            _service.SaveDeviceToken(json);
        }

        /// <summary>
        /// Calls service to save a list of synchronized photos in the storage.
        /// </summary>
        /// <param name="json">Json with list of synchronized photos.</param>
        public void SavePhotoset(string json)
        {
            _service.SavePhotoset(json);
        }

        /// <summary>
        /// Calls service to save new photos and removes outdated ones.
        /// </summary>
        /// <param name="newPhotoset">List of new photos.</param>
        /// <param name="oldPhotoset">List of old photos.</param>
        public void SynchronizeImages(GetAllFlickrPhotosURLResponseDTO newPhotoset,
            GetAllFlickrPhotosURLResponseDTO oldPhotoset)
        {
            _service.SynchronizeImages(newPhotoset, oldPhotoset);
        }

        /// <summary>
        /// Handles "ErrorOccured" event of the ILocalStorageService object.
        /// Invokes "ErrorOccured" event to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the LocalStorageService class.</param>
        /// <param name="errorMessage">Message of the error.</param>
        private void ErrorOccuredEventHandler(object sender, string errorMessage)
        {
            ErrorOccured?.Invoke(this, errorMessage);
        }

        /// <summary>
        /// Handles "SynchronizationCompleted" event of the ILocalStorageService object.
        /// Invokes "SynchronizationCompleted" event to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the LocalStorageService class.</param>
        /// <param name="newPhotoset">Collection of new photos.</param>
        private void SynchronizationCompletedEventHandler(object sender, GetAllFlickrPhotosURLResponseDTO newPhotoset)
        {
            SynchronizationCompleted?.Invoke(this, newPhotoset);
        }

        #endregion
    }
}