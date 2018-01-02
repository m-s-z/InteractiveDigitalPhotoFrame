using System;
using System.Collections.Generic;
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
            foreach (var newPhotosetUrl in newPhotoset.Urls)
            {
                var temp = oldPhotoset.Urls.Find(p =>
                    p.PhotoId == newPhotosetUrl.PhotoId && p.MyProperty == newPhotosetUrl.MyProperty);
                if (temp == null)
                {
                    await SaveImage(newPhotosetUrl);
                }
            }

            //SynchronizationCompleted?.Invoke(this, newPhotoset);

            foreach (var oldPhotosetUrl in oldPhotoset.Urls)
            {
                var temp = newPhotoset.Urls.Find(p =>
                    p.PhotoId == oldPhotosetUrl.PhotoId && p.MyProperty == oldPhotosetUrl.MyProperty);
                if (temp == null)
                {
                    DeleteImage(oldPhotosetUrl);
                }
            }

            SynchronizationCompleted?.Invoke(this, newPhotoset);
        }

        private void DeleteImage(Urls imageToDelete)
        {
            try
            {
                File.Delete(string.Format(PATH_TO_PICTURES_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    CloudProviderTypeToDirectoryNameConverter(imageToDelete.MyProperty), imageToDelete.PhotoId));
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
                var st = Directory.GetFiles(string.Format(PATH_TO_PICTURES_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    CloudProviderTypeToDirectoryNameConverter(imageToShow.MyProperty), ""));
                foreach (string s in st)
                {
                    System.Diagnostics.Debug.WriteLine(s);
                }

                string path = string.Format(PATH_TO_PICTURES_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    CloudProviderTypeToDirectoryNameConverter(imageToShow.MyProperty), imageToShow.PhotoId);
                if (File.Exists(path))
                {
                    return path;
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
                    CloudProviderTypeToDirectoryNameConverter(imageToSave.MyProperty), ""));

                var webClient = new WebClient();
                webClient.DownloadDataCompleted += (s, e) =>
                {
                    var bytes = e.Result;
                    string imagePath = string.Format(PATH_TO_PICTURES_TEMPLATE,
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        CloudProviderTypeToDirectoryNameConverter(imageToSave.MyProperty), imageToSave.PhotoId);
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

        public void SaveImage()
        {
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------------");
            System.Diagnostics.Debug.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            System.Diagnostics.Debug.WriteLine(Directory.GetCurrentDirectory());
            
            var st = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            foreach (string s in st)
            {
                System.Diagnostics.Debug.WriteLine(s);
                System.Diagnostics.Debug.WriteLine("Directory: " + Directory.GetParent(s).Name + "  File: " +
                                                   Path.GetFileName(s));
                System.Diagnostics.Debug.WriteLineIf(
                    (Directory.GetParent(s).Name.Equals("files")) && Path.GetFileName(s).Equals("downloaded.png"),
                    "Yes, it is!");
            }
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------------");
            st = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            foreach (string s in st)
            {
                System.Diagnostics.Debug.WriteLine(s);
            }
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------------");
            st = Directory.GetDirectories(string.Format(PATH_TO_PICTURES_TEMPLATE, Environment.GetFolderPath(Environment.SpecialFolder.Personal), "", ""));
            foreach (string s in st)
            {
                System.Diagnostics.Debug.WriteLine(s);
            }
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------------");


            //var webClient = new WebClient();
            //webClient.DownloadDataCompleted += (s, e) =>
            //{
            //    var bytes = e.Result; // get the downloaded data
            //    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            //    string localFilename = "downloaded.png";
            //    string localPath = Path.Combine(documentsPath, localFilename);
            //    File.WriteAllBytes(localPath, bytes); // writes to local storage
            //};

            //var url = new Uri("https://3.bp.blogspot.com/-vLUGceSS8r4/V8knCEU569I/AAAAAAAAAcg/pK2uc4kBiPUe2t9KwsozXYhYVnmFjq4xQCLcB/s1600/Screenshot_2016-09-02-13-01-53.png");
            //webClient.DownloadDataAsync(url);

            //webClient = new WebClient();
            //webClient.DownloadDataCompleted += (s, e) =>
            //{
            //    var bytes = e.Result; // get the downloaded data
            //    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            //    string localFilename = "downloaded2.png";
            //    string localPath = Path.Combine(documentsPath, localFilename);
            //    File.WriteAllBytes(localPath, bytes); // writes to local storage
            //};

            //url = new Uri("https://qph.ec.quoracdn.net/main-qimg-aa72bafd48ebfcb5b6e14006ab9c48be");
            //webClient.DownloadDataAsync(url);
        }

        public string GetConnectedAccounts()
        {
            try
            {
                return File.ReadAllText(string.Format(PATH_TO_DATA_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ConnectedAccounts.txt"));
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
                return File.ReadAllText(string.Format(PATH_TO_DATA_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DeviceToken.txt"));
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
                return File.ReadAllText(string.Format(PATH_TO_DATA_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Photoset.txt"));
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