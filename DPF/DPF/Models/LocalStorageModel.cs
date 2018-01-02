using System.Collections.Generic;
using IDPFLibrary.DTO;
using Xamarin.Forms;

namespace DPF.Models
{
    public class LocalStorageModel
    {
        private ILocalStorageService _service;

        public event ErrorOccurredDelegate ErrorOccured;

        public event SynchronizationCompletedDelegate SynchronizationCompleted;

        public LocalStorageModel()
        {
            _service = DependencyService.Get<ILocalStorageService>();
            _service.ErrorOccured += ErrorOccuredEventHandler;
            _service.SynchronizationCompleted += SynchronizationCompletedEventHandler;
        }

        public void CreateImagesFolder()
        {
            _service.CreateImagesFolder();
        }

        public List<string> GetImagesList()
        {
            return _service.GetImagesList();
        }

        public string GetConnectedAccounts()
        {
            return _service.GetConnectedAccounts();
        }

        public string GetDeviceToken()
        {
            return _service.GetDeviceToken();
        }

        public string GetPhotoset()
        {
            return _service.GetPhotoset();
        }

        public string GetImageToShow(Urls imageToShow)
        {
            return _service.GetImageToShow(imageToShow);
        }

        public void SaveConnectedAccounts(string json)
        {
            _service.SaveConnectedAccounts(json);
        }

        public void SaveDeviceToken(string json)
        {
            _service.SaveDeviceToken(json);
        }

        public void SavePhotoset(string json)
        {
            _service.SavePhotoset(json);
        }

        public void SaveImage()
        {
            _service.SaveImage();
        }

        public void SynchronizeImages(GetAllFlickrPhotosURLResponseDTO newPhotoset,
            GetAllFlickrPhotosURLResponseDTO oldPhotoset)
        {
            _service.SynchronizeImages(newPhotoset, oldPhotoset);
        }

        private void ErrorOccuredEventHandler(object sender, string errorMessage)
        {
            ErrorOccured?.Invoke(this, errorMessage);
        }

        private void SynchronizationCompletedEventHandler(object sender, GetAllFlickrPhotosURLResponseDTO newPhotoset)
        {
            SynchronizationCompleted?.Invoke(this, newPhotoset);
        }
    }
}