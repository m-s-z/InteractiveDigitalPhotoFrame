namespace DPF.Models
{
    public class CloudFolder
    {
        private int _photosNumber;
        private string _folderName;

        public int PhotosNumber
        {
            get => _photosNumber;
            set => _photosNumber = value;
        }

        public string FolderName
        {
            get => _folderName;
            set => _folderName = value;
        }

        public CloudFolder(string folderName, int photosNumber)
        {
            FolderName = folderName;
            PhotosNumber = photosNumber;
        }
    }
}