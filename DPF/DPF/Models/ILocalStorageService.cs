using System.Collections.Generic;

namespace DPF.Models
{
    public delegate void ErrorOccurredDelegate(object sender, string errorMessage);
    public interface ILocalStorageService
    {
        event ErrorOccurredDelegate ErrorOccured;
        void CreateImagesFolder();

        List<string> GetImagesList();

        string GetConnectedAccounts();

        string GetDeviceToken();

        void SaveConnectedAccounts(string json);

        void SaveDeviceToken(string json);


        void SaveImage();
    }
}