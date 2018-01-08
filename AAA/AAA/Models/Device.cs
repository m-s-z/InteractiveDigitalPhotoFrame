using System.Collections.ObjectModel;
using AAA.Utils.CloudProvider;
using IDPFLibrary;

namespace AAA.Models
{
    public class Device : ViewModelBase
    {
        private int _numberOfFolders;
        private ObservableCollection<Folder> _foldersCollection;
        private string _localName;

        public int NumberOfFolders
        {
            get => _numberOfFolders;
            set => SetProperty(ref _numberOfFolders, value);
        }

        public ObservableCollection<Folder> FoldersCollection
        {
            get => _foldersCollection;
            set => SetProperty(ref _foldersCollection, value);
        }

        public string LocalName
        {
            get => _localName;
            set => SetProperty(ref _localName, value);
        }

        public Device(string localName)
        {
            LocalName = localName;
            FoldersCollection = new ObservableCollection<Folder>();
            NumberOfFolders = CountFolders();
        }

        public Device(string localName, int temp)
        {
            LocalName = localName;
            FoldersCollection = new ObservableCollection<Folder>();
            FoldersCollection.Add(new Folder(CloudTypeEnum.Flickr, "Holidays 2017"));
            FoldersCollection.Add(new Folder(CloudTypeEnum.Dropbox, "Trip to France"));
            NumberOfFolders = CountFolders();
        }

        public void UpdateInformation()
        {
            NumberOfFolders = CountFolders();
        }

        public int CountFolders()
        {
            return FoldersCollection.Count;
        }
    }
}