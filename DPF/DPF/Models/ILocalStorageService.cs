using System.Collections.Generic;
using IDPFLibrary.DTO;

namespace DPF.Models
{
    public delegate void ErrorOccurredDelegate(object sender, string errorMessage);

    public delegate void SynchronizationCompletedDelegate(object sender, GetAllFlickrPhotosURLResponseDTO newPhotoset);
    public interface ILocalStorageService
    {
        event ErrorOccurredDelegate ErrorOccured;

        event SynchronizationCompletedDelegate SynchronizationCompleted;
        void CreateImagesFolder();

        List<string> GetImagesList();

        string GetConnectedAccounts();

        string GetDeviceToken();

        string GetPhotoset();

        string GetImageToShow(Urls imageToShow);

        void SaveConnectedAccounts(string json);

        void SaveDeviceToken(string json);

        void SavePhotoset(string json);

        void SynchronizeImages(GetAllFlickrPhotosURLResponseDTO newPhotoset,
            GetAllFlickrPhotosURLResponseDTO oldPhotoset);
    }
}