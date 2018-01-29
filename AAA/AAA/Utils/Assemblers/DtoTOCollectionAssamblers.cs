using System.Collections.ObjectModel;
using AAA.Utils.CloudProvider;
using AAA.Utils.Controls;
using IDPFLibrary;
using IDPFLibrary.DTO.AAA.Cloud.Response;
using IDPFLibrary.DTO.AAA.Device.Response;
using IDPFLibrary.DTO.AAA.Folder.Response;
using Xamarin.Forms;

namespace AAA.Utils.Assemblers
{
    public class DtoTOCollectionAssamblers
    {
        public static ObservableCollection<VCListItem> AssambleFoldersDtoToCollection(AppGetDeviceFoldersResponseDTO dto,
            Command commandToAssign)
        {
            ObservableCollection<VCListItem> newCollection = new ObservableCollection<VCListItem>();

            foreach (var folder in dto.Folders)
            {
                newCollection.Add(new VCListItem(folder, null, commandToAssign));
            }

            return newCollection;
        }

        public static ObservableCollection<VCCardListItem> AssambleCloudsDtoToCollectionCard(
            AppGetCloudsResponseDTO dto, Command commandToAssign)
        {
            ObservableCollection<VCCardListItem> newCollection = new ObservableCollection<VCCardListItem>();

            foreach (var cloud in dto.clouds)
            {
                newCollection.Add(new VCCardListItem(CardTypeEnum.ShortOneAction, cloud, commandToAssign));
            }

            return newCollection;
        }

        public static ObservableCollection<VCListItem> AssambleCloudsDtoToCollectionList(AppGetCloudsResponseDTO dto,
            Command commandToAssign)
        {
            ObservableCollection<VCListItem> newCollection = new ObservableCollection<VCListItem>();

            foreach (var cloud in dto.clouds)
            {
                newCollection.Add(cloud.provider == CloudProviderType.Dropbox
                    ? new VCListItem(cloud, CloudTypeEnum.Dropbox, commandToAssign)
                    : new VCListItem(cloud, CloudTypeEnum.Flickr, commandToAssign));
            }

            return newCollection;
        }

        public static ObservableCollection<VCListItem> AssambleDevicesDtoToCollection(AppGetDevicesResponseDTO dto,
            Command commandToAssign)
        {
            ObservableCollection<VCListItem> newCollection = new ObservableCollection<VCListItem>();

            foreach (var device in dto.Devices)
            {
                newCollection.Add(new VCListItem(device, commandToAssign));
            }

            return newCollection;
        }

        public static ObservableCollection<VCListItem> AssambleCloudFoldersDtoToCollection(
            AppGetCloudFoldersResponseDTO dto, Command commandToAssign)
        {
            ObservableCollection<VCListItem> newCollection = new ObservableCollection<VCListItem>();

            foreach (var folder in dto.folders)
            {
                newCollection.Add(new VCListItem(folder, null, commandToAssign));
            }

            return newCollection;
        }
    }
}