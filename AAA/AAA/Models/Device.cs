using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AAA.Utils.CloudProvider;
using AAA.Utils.Controls;
using IDPFLibrary;
using IDPFLibrary.DTO.AAA.Folder.Response;
using Xamarin.Forms;

namespace AAA.Models
{
    public class Device : ViewModelBase
    {
        private int _id;
        private int _numberOfFolders;
        private ObservableCollection<VCListItem> _foldersCollection;
        private string _localName;

        public int NumberOfFolders
        {
            get => _numberOfFolders;
            set => SetProperty(ref _numberOfFolders, value);
        }

        public ObservableCollection<VCListItem> FoldersCollection
        {
            get => _foldersCollection;
            set => SetProperty(ref _foldersCollection, value);
        }

        public string LocalName
        {
            get => _localName;
            set => SetProperty(ref _localName, value);
        }

        public Device(string localName, int id)
        {;
            _id = id;
            LocalName = localName;
            FoldersCollection = new ObservableCollection<VCListItem>();
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

        public void AssambleFoldersDtoToCollection(List<SFolder> folders, Command deleteCommand)
        {
            FoldersCollection = new ObservableCollection<VCListItem>();

            foreach (var folder in folders)
            {
                FoldersCollection.Add(new VCListItem(folder, null, deleteCommand));
            }
        }
    }
}