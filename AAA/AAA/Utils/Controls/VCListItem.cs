using AAA.Models;
using AAA.Utils.CloudProvider;
using IDPFLibrary;
using IDPFLibrary.DTO.AAA.Cloud.Response;
using IDPFLibrary.DTO.AAA.Device.Response;
using IDPFLibrary.DTO.AAA.Folder.Response;
using Xamarin.Forms;

namespace AAA.Utils.Controls
{
    public class VCListItem : ViewModelBase
    {
        private Command _additionalCommand;
        private Command _mainCommand;
        private RCloud _cloud;
        private SDeviceName _device;
        private SFolder _folder;
        private CloudTypeEnum _cloudType;
        private SUniversalFolder _folderUniversal;
        private int _subtext;

        #region properties

        /// <summary>
        /// Additional command to execute on right side of item tap.
        /// </summary>
        public Command AdditionalCommand
        {
            get { return _additionalCommand; }
            set { SetProperty(ref _additionalCommand, value); }
        }

        /// <summary>
        /// Main command to execute on item tap.
        /// </summary>
        public Command MainCommand
        {
            get { return _mainCommand; }
            set { SetProperty(ref _mainCommand, value); }
        }

        /// <summary>
        /// Type of cloud to conver into subimage displayed on the cell.
        /// </summary>
        public CloudTypeEnum CloudType
        {
            get { return _cloudType; }
            set { SetProperty(ref _cloudType, value); }
        }

        /// <summary>
        /// Value to insert into subtext dispalyed on the cell.
        /// </summary>
        public int Subtext
        {
            get { return _subtext; }
            set { SetProperty(ref _subtext, value); }
        }

        public RCloud Cloud
        {
            get => _cloud;
            set => SetProperty(ref _cloud, value);
        }
        public SDeviceName Device
        {
            get => _device;
            set => SetProperty(ref _device, value);
        }

        public SFolder Folder
        {
            get => _folder;
            set => SetProperty(ref _folder, value);
        }

        public SUniversalFolder FolderUniversal
        {
            get => _folderUniversal;
            set => SetProperty(ref _folderUniversal, value);
        }

        #endregion

        #region methods

        public VCListItem(SDeviceName device, Command mainCommand, Command additionalCommand = null)
        {
            Device = device;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        public VCListItem(SFolder folder, Command mainCommand, Command additionalCommand = null)
        {
            Folder = folder;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        public VCListItem(RCloud cloud, CloudTypeEnum cloudType, Command mainCommand, Command additionalCommand = null)
        {
            Cloud = cloud;
            CloudType = cloudType;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        public VCListItem(Command mainCommand, Command additionalCommand = null)
        {
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        public VCListItem(SUniversalFolder folderUniversal, Command mainCommand, Command additionalCommand = null)
        {
            FolderUniversal = folderUniversal;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        public VCListItem()
        {
            
        }

        #endregion
    }
}