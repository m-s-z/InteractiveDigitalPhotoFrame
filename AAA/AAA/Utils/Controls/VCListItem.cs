using AAA.Models;
using AAA.Utils.CloudProvider;
using IDPFLibrary;
using IDPFLibrary.DTO.AAA.Cloud.Response;
using IDPFLibrary.DTO.AAA.Device.Response;
using IDPFLibrary.DTO.AAA.Folder.Response;
using Xamarin.Forms;

namespace AAA.Utils.Controls
{
    /// <summary>
    /// VCListItem class.
    /// </summary>
    public class VCListItem : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of AdditionalCommand property.
        /// </summary>
        private Command _additionalCommand;

        /// <summary>
        /// Backing field of MainCommand property.
        /// </summary>
        private Command _mainCommand;

        /// <summary>
        /// Backing field of Cloud property.
        /// </summary>
        private RCloud _cloud;

        /// <summary>
        /// Backing field of Device property.
        /// </summary>
        private SDeviceName _device;

        /// <summary>
        /// Backing field of Folder property.
        /// </summary>
        private SFolder _folder;

        /// <summary>
        /// Backing field of CloudType property.
        /// </summary>
        private CloudTypeEnum _cloudType;

        /// <summary>
        /// Backing field of FolderUniversal property.
        /// </summary>
        private SUniversalFolder _folderUniversal;

        /// <summary>
        /// Backing field of Subtext property.
        /// </summary>
        private int _subtext;

        #endregion

        #region properties

        /// <summary>
        /// Additional command to execute on right side of item tap.
        /// </summary>
        public Command AdditionalCommand
        {
            get => _additionalCommand;
            set => SetProperty(ref _additionalCommand, value);
        }

        /// <summary>
        /// Main command to execute on item tap.
        /// </summary>
        public Command MainCommand
        {
            get => _mainCommand;
            set => SetProperty(ref _mainCommand, value);
        }

        /// <summary>
        /// Type of cloud to convert into subimage displayed on the cell.
        /// </summary>
        public CloudTypeEnum CloudType
        {
            get => _cloudType;
            set => SetProperty(ref _cloudType, value);
        }

        /// <summary>
        /// Value to insert into subtext displayed on the cell.
        /// </summary>
        public int Subtext
        {
            get => _subtext;
            set => SetProperty(ref _subtext, value);
        }

        /// <summary>
        /// Property indicating connected cloud.
        /// </summary>
        public RCloud Cloud
        {
            get => _cloud;
            set => SetProperty(ref _cloud, value);
        }

        /// <summary>
        /// Property indicating paired device.
        /// </summary>
        public SDeviceName Device
        {
            get => _device;
            set => SetProperty(ref _device, value);
        }

        /// <summary>
        /// Property indicating assigned folder.
        /// </summary>
        public SFolder Folder
        {
            get => _folder;
            set => SetProperty(ref _folder, value);
        }

        /// <summary>
        /// Property indicating assigned universal type of folder.
        /// </summary>
        public SUniversalFolder FolderUniversal
        {
            get => _folderUniversal;
            set => SetProperty(ref _folderUniversal, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// VCListItem class constructor.
        /// </summary>
        /// <param name="device">Paired device.</param>
        /// <param name="mainCommand">Main command.</param>
        /// <param name="additionalCommand">Additional command.</param>
        public VCListItem(SDeviceName device, Command mainCommand, Command additionalCommand = null)
        {
            Device = device;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        /// <summary>
        /// VCListItem class constructor.
        /// </summary>
        /// <param name="folder">Assigned folder.</param>
        /// <param name="mainCommand">Main command.</param>
        /// <param name="additionalCommand">Additional command.</param>
        public VCListItem(SFolder folder, Command mainCommand, Command additionalCommand = null)
        {
            Folder = folder;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        /// <summary>
        /// VCListItem class constructor.
        /// </summary>
        /// <param name="cloud">Connected cloud.</param>
        /// <param name="cloudType">Type of cloud</param>
        /// <param name="mainCommand">Main command.</param>
        /// <param name="additionalCommand">Additional command.</param>
        public VCListItem(RCloud cloud, CloudTypeEnum cloudType, Command mainCommand, Command additionalCommand = null)
        {
            Cloud = cloud;
            CloudType = cloudType;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        /// <summary>
        /// VCListItem class constructor.
        /// </summary>
        /// <param name="mainCommand">Main command.</param>
        /// <param name="additionalCommand">Additional command.</param>
        public VCListItem(Command mainCommand, Command additionalCommand = null)
        {
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        /// <summary>
        /// VCListItem class constructor.
        /// </summary>
        /// <param name="folderUniversal">Assigned folder.</param>
        /// <param name="mainCommand">Main command.</param>
        /// <param name="additionalCommand">Additional command.</param>
        public VCListItem(SUniversalFolder folderUniversal, Command mainCommand, Command additionalCommand = null)
        {
            FolderUniversal = folderUniversal;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        /// <summary>
        /// VCListItem class constructor.
        /// </summary>
        public VCListItem()
        {
            
        }

        #endregion
    }
}