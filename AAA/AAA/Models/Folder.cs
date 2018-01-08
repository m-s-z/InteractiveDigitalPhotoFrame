using System;
using AAA.Utils.CloudProvider;
using IDPFLibrary;

namespace AAA.Models
{
    public class Folder : ViewModelBase
    {
        private CloudTypeEnum _cloudType;
        private int _photosNumber;
        private string _folderName;

        public CloudTypeEnum CloudType
        {
            get => _cloudType;
            set => SetProperty(ref _cloudType, value);
        }

        public int PhotosNumber
        {
            get => _photosNumber;
            set => SetProperty(ref _photosNumber, value);
        }

        public string FolderName
        {
            get => _folderName;
            set => SetProperty(ref _folderName, value);
        }

        public Folder(CloudTypeEnum cloudType)
        {
            Random random = new Random();
            FolderName = random.Next(1000, 7000).ToString();
            PhotosNumber = random.Next(1, 20);
            CloudType = cloudType;
        }

        public Folder(CloudTypeEnum cloudType, string folderName)
        {
            Random random = new Random();
            FolderName = folderName;
            PhotosNumber = random.Next(1, 20);
            CloudType = cloudType;
        }
    }
}