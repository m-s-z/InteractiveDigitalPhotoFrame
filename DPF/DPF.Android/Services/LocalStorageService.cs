using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DPF.Droid.Services;
using DPF.Models;
using Xamarin.Forms;
using IDPFLibrary;
using IDPFLibrary.DTO;

[assembly: Dependency(typeof(LocalStorageService))]

namespace DPF.Droid.Services
{
    /// <summary>
    /// LocalStorageService class.
    /// Implements ILocalStorageService interface.
    /// </summary>
    public class LocalStorageService : ILocalStorageService
    {
        #region fields

        /// <summary>
        /// Template to the path where pictures are stored.
        /// </summary>
        private const string PATH_TO_PICTURES_TEMPLATE = "{0}/pictures{1}/{2}";

        /// <summary>
        /// Template to the path where application data are stored.
        /// </summary>
        private const string PATH_TO_DATA_TEMPLATE = "{0}/data/{1}";

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
        /// Creates main directories where application data is stored.
        /// </summary>
        public void CreateImagesFolder()
        {
            try
            {
                Directory.CreateDirectory(string.Format(PATH_TO_PICTURES_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "", ""));
                Directory.CreateDirectory(string.Format(PATH_TO_DATA_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), ""));
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

        /// <summary>
        /// If photo is outdated, calls DeleteImage method.
        /// If there is a new photo, calls SaveImage method.
        /// Invokes "SynchronizationCompleted" event to other application's modules.
        /// </summary>
        /// <param name="newPhotoset">List of new photos.</param>
        /// <param name="oldPhotoset">List of old photos.</param>
        public async void SynchronizeImages(GetAllFlickrPhotosURLResponseDTO newPhotoset, GetAllFlickrPhotosURLResponseDTO oldPhotoset)
        {
            try
            {
                foreach (var oldPhotosetUrl in oldPhotoset.Urls)
                {
                    var temp = newPhotoset.Urls.Find(p =>
                        p.PhotoId == oldPhotosetUrl.PhotoId && p.CloudProvider == oldPhotosetUrl.CloudProvider);

                    if (temp == null)
                    {
                        DeleteImage(oldPhotosetUrl);
                    }
                }

                foreach (var newPhotosetUrl in newPhotoset.Urls)
                {
                    var temp = oldPhotoset.Urls.Find(p =>
                        p.PhotoId == newPhotosetUrl.PhotoId && p.CloudProvider == newPhotosetUrl.CloudProvider);

                    if (temp == null)
                    {
                        await SaveImage(newPhotosetUrl);
                    }
                }

                SynchronizationCompleted?.Invoke(this, newPhotoset);
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the indicated photo from a device memory.
        /// </summary>
        /// <param name="imageToDelete">Photo to delete.</param>
        private void DeleteImage(Urls imageToDelete)
        {
            try
            {
                File.Delete(string.Format(PATH_TO_PICTURES_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    CloudProviderTypeToDirectoryNameConverter(imageToDelete.CloudProvider), imageToDelete.PhotoId));
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

        /// <summary>
        /// Gets path to the indicated photo.
        /// </summary>
        /// <param name="imageToShow">Photo to get path to.</param>
        /// <returns>Path to the photo.</returns>
        public string GetImageToShow(Urls imageToShow)
        {
            try
            {
                string path = string.Format(PATH_TO_PICTURES_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    CloudProviderTypeToDirectoryNameConverter(imageToShow.CloudProvider), imageToShow.PhotoId);

                if (File.Exists(path))
                {
                    return path;
                }

                return "";
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }

            return null;
        }

        /// <summary>
        /// Creates new directory to store photos in if does not exist.
        /// Downloads the indicated photo and saves in the device memory.
        /// </summary>
        /// <param name="imageToSave">Photo to save.</param>
        /// <returns>Returns void task.</returns>
        public async Task SaveImage(Urls imageToSave)
        {
            try
            {
                Directory.CreateDirectory(string.Format(PATH_TO_PICTURES_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    CloudProviderTypeToDirectoryNameConverter(imageToSave.CloudProvider), ""));

                var webClient = new WebClient();
                webClient.DownloadDataCompleted += (s, e) =>
                {
                    var bytes = e.Result;
                    string imagePath = string.Format(PATH_TO_PICTURES_TEMPLATE,
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        CloudProviderTypeToDirectoryNameConverter(imageToSave.CloudProvider), imageToSave.PhotoId);
                    File.WriteAllBytes(imagePath, bytes);
                };

                var url = new Uri(imageToSave.Link);
                webClient.DownloadDataAsync(url);
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

        /// <summary>
        /// Reads a list of connected accounts from the storage,
        /// </summary>
        /// <returns>Json with list of connected accounts.</returns>
        public string GetConnectedAccounts()
        {
            try
            {
                if (File.Exists(string.Format(PATH_TO_DATA_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ConnectedAccounts.txt")))
                {
                    return File.ReadAllText(string.Format(PATH_TO_DATA_TEMPLATE,
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ConnectedAccounts.txt"));
                }

                return null;
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return null;
            }

        }

        /// <summary>
        /// Reads a device token from the storage,
        /// </summary>
        /// <returns>Json with a device token.</returns>
        public string GetDeviceToken()
        {
            try
            {
                if (File.Exists(string.Format(PATH_TO_DATA_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DeviceToken.txt")))
                {
                    return File.ReadAllText(string.Format(PATH_TO_DATA_TEMPLATE,
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DeviceToken.txt"));
                }

                return null;
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Saves a list of connected accounts in the storage.
        /// </summary>
        /// <param name="json">Json with list of connected accounts.</param>
        public void SaveConnectedAccounts(string json)
        {
            try
            {
                File.WriteAllText(
                    string.Format(PATH_TO_DATA_TEMPLATE, Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        "ConnectedAccounts.txt"), json);
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

        /// <summary>
        /// Reads a list of synchronized photos from the storage,
        /// </summary>
        /// <returns>Json with list of synchronized photos.</returns>
        public string GetPhotoset()
        {
            try
            {
                if (File.Exists(string.Format(PATH_TO_DATA_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Photoset.txt")))
                {
                    return File.ReadAllText(string.Format(PATH_TO_DATA_TEMPLATE,
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Photoset.txt"));
                }

                return null;
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Saves a list of synchronized photos in the storage.
        /// </summary>
        /// <param name="json">Json with list of synchronized photos.</param>
        public void SavePhotoset(string json)
        {
            try
            {
                File.WriteAllText(
                    string.Format(PATH_TO_DATA_TEMPLATE, Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        "Photoset.txt"), json);
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

        /// <summary>
        /// Saves a device token in the storage.
        /// </summary>
        /// <param name="json">Json with a device token.</param>
        public void SaveDeviceToken(string json)
        {
            try
            {
                Directory.CreateDirectory(string.Format(PATH_TO_DATA_TEMPLATE, Environment.GetFolderPath(Environment.SpecialFolder.Personal), ""));
                File.WriteAllText(string.Format(PATH_TO_DATA_TEMPLATE, Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DeviceToken.txt"), json);
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

        /// <summary>
        /// Creates string depending on the type of cloud provider.
        /// </summary>
        /// <param name="cloudProviderType">Type of cloud provider.</param>
        /// <returns>String with cloud provider name.</returns>
        private string CloudProviderTypeToDirectoryNameConverter(CloudProviderType cloudProviderType)
        {
            switch (cloudProviderType)
            {
                case CloudProviderType.Dropbox:
                    return "/dropbox";
                case CloudProviderType.Flickr:
                    return "/flickr";
                case CloudProviderType.GoogleDrive:
                    return "/googledrive";
                case CloudProviderType.OneDrive:
                    return "/onedrive";
                default:
                    return "/unknown";
            }
        }

        /// <summary>
        /// Invokes "ErrorOccurred" event to other application's modules.
        /// </summary>
        /// <param name="errorMessage">Message of the error.</param>
        private void ErrorHandler(string errorMessage)
        {
            ErrorOccured?.Invoke(this, errorMessage);
        }

        #endregion
    }
}