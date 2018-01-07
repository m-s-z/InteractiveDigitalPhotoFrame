using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    public class LocalStorageService : ILocalStorageService
    {
        private const string PATH_TO_PICTURES_TEMPLATE = "{0}/pictures{1}/{2}";
        private const string PATH_TO_DATA_TEMPLATE = "{0}/data/{1}";

        public event ErrorOccurredDelegate ErrorOccured;

        public event SynchronizationCompletedDelegate SynchronizationCompleted;

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

        public List<string> GetImagesList()
        {
            var st = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).ToList();
            return st;
        }

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
                else
                {
                    return "";
                }
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }

            return null;
        }

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
                    File.WriteAllBytes(imagePath, bytes); // writes to local storage
                };

                var url = new Uri(imageToSave.Link);
                webClient.DownloadDataAsync(url);
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

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

        private bool CheckIfPhotoIsStillSynchronizing()
        {
            return false;
        }

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

        private void ErrorHandler(string errorMessage)
        {
            ErrorOccured?.Invoke(this, errorMessage);
        }
    }
}