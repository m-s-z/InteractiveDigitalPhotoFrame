using System.Collections.Generic;
using Xamarin.Forms;

namespace DPF.Models
{
    public class LocalStorageModel
    {
        private ILocalStorageService _service;

        public event ErrorOccurredDelegate ErrorOccured;

        public LocalStorageModel()
        {
            _service = DependencyService.Get<ILocalStorageService>();
            _service.ErrorOccured += ErrorOccuredEventHandler;
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

        public void SaveConnectedAccounts(string json)
        {
            _service.SaveConnectedAccounts(json);
        }

        public void SaveDeviceToken(string json)
        {
            _service.SaveDeviceToken(json);
        }

        public void SaveImage()
        {
            _service.SaveImage();
        }

        private void ErrorOccuredEventHandler(object sender, string errorMessage)
        {
            ErrorOccured?.Invoke(this, errorMessage);
        }
    }
}