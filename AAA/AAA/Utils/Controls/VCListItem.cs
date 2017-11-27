using AAA.Models;
using AAA.Utils.CloudProvider;
using IDPFLibrary;
using Xamarin.Forms;

namespace AAA.Utils.Controls
{
    public class VCListItem : ViewModelBase
    {
        private Command _additionalCommand;
        private Command _mainCommand;
        private Models.CloudProvider _cloud;
        private Models.Device _device;
        private Folder _folder;
        private CloudTypeEnum _cloudType;
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

        public Models.CloudProvider Cloud
        {
            get => _cloud;
            set => SetProperty(ref _cloud, value);
        }
        public Models.Device Device
        {
            get => _device;
            set => SetProperty(ref _device, value);
        }

        public Folder Folder
        {
            get => _folder;
            set => SetProperty(ref _folder, value);
        }

        #endregion

        #region methods

        public VCListItem(Models.Device device, Command mainCommand, Command additionalCommand = null)
        {
            Device = device;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        public VCListItem(Folder folder, Command mainCommand, Command additionalCommand = null)
        {
            Folder = folder;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        public VCListItem(Models.CloudProvider cloud, Command mainCommand, Command additionalCommand = null)
        {
            Cloud = cloud;
            MainCommand = mainCommand;
            AdditionalCommand = additionalCommand;
        }

        public VCListItem()
        {
            
        }

        #endregion
    }
}