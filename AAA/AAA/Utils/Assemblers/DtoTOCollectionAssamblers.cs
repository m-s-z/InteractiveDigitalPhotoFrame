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
    /// <summary>
    /// DtoTOCollectionAssamblers class.
    /// Provides methods which assemble collections from given DTO.
    /// </summary>
    public class DtoTOCollectionAssamblers
    {
        /// <summary>
        /// Assemble collection of folders from a DTO.
        /// </summary>
        /// <param name="dto">DTO source of information.</param>
        /// <param name="commandToAssign">Command to execute on tap.</param>
        /// <returns>Collection of folders.</returns>
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

        /// <summary>
        /// Assemble collection of clouds' cards from a DTO.
        /// </summary>
        /// <param name="dto">DTO source of information.</param>
        /// <param name="commandToAssign">Command to execute on tap.</param>
        /// <returns>Collection of clouds' cards.</returns>
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

        /// <summary>
        /// Assemble collection of clouds from a DTO.
        /// </summary>
        /// <param name="dto">DTO source of information.</param>
        /// <param name="commandToAssign">Command to execute on tap.</param>
        /// <returns>Collection of clouds.</returns>
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

        /// <summary>
        /// Assemble collection of devices from a DTO.
        /// </summary>
        /// <param name="dto">DTO source of information.</param>
        /// <param name="commandToAssign">Command to execute on tap.</param>
        /// <returns>Collection of devices.</returns>
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

        /// <summary>
        /// Assemble collection of folders of a cloud from a DTO.
        /// </summary>
        /// <param name="dto">DTO source of information.</param>
        /// <param name="commandToAssign">Command to execute on tap.</param>
        /// <returns>Collection of folders of a cloud.</returns>
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