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
        #region fields

        /// <summary>
        /// Backing field of DeviceId property.
        /// </summary>
        private int _id;

        /// <summary>
        /// Backing field of NumberOfFolders property.
        /// </summary>
        private int _numberOfFolders;

        /// <summary>
        /// Backing field of FoldersCollection property.
        /// </summary>
        private ObservableCollection<VCListItem> _foldersCollection;

        /// <summary>
        /// Backing field of LocalName property.
        /// </summary>
        private string _localName;

        #endregion

        #region properties

        /// <summary>
        /// Property indicating the ID of a device.
        /// </summary>
        public int DeviceId
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// Property indicating number of folders assigned to a device.
        /// </summary>
        public int NumberOfFolders
        {
            get => _numberOfFolders;
            set => SetProperty(ref _numberOfFolders, value);
        }

        /// <summary>
        /// Property indicating collection of folders of a device.
        /// </summary>
        public ObservableCollection<VCListItem> FoldersCollection
        {
            get => _foldersCollection;
            set => SetProperty(ref _foldersCollection, value);
        }

        /// <summary>
        /// Property indicating local name of a device.
        /// </summary>
        public string LocalName
        {
            get => _localName;
            set => SetProperty(ref _localName, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Device class constructor.
        /// </summary>
        /// <param name="localName">Local name of a device.</param>
        /// <param name="id">ID of a device.</param>
        public Device(string localName, int id)
        {
            DeviceId = id;
            LocalName = localName;
            FoldersCollection = new ObservableCollection<VCListItem>();
            NumberOfFolders = CountFolders();
        }

        /// <summary>
        /// Updates number of folders.
        /// </summary>
        public void UpdateInformation()
        {
            NumberOfFolders = CountFolders();
        }

        /// <summary>
        /// Counts number of folders.
        /// </summary>
        /// <returns></returns>
        public int CountFolders()
        {
            return FoldersCollection.Count;
        }

        /// <summary>
        /// Assembles collection of folders from a list.
        /// </summary>
        /// <param name="folders">List of folders.</param>
        /// <param name="deleteCommand">Command to execute on tap.</param>
        public void AssambleFoldersDtoToCollection(List<SFolder> folders, Command deleteCommand)
        {
            FoldersCollection = new ObservableCollection<VCListItem>();

            foreach (var folder in folders)
            {
                FoldersCollection.Add(new VCListItem(folder, null, deleteCommand));
            }
        }

        #endregion
    }
}