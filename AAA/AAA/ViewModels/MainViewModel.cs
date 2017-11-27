using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AAA.Models;
using AAA.Utils;
using AAA.Utils.CloudProvider;
using AAA.Utils.Controls;
using AAA.Views;
using Xamarin.Forms;

namespace AAA.ViewModels
{
    /// <summary>
    /// MainViewModel class.
    /// Provides properites and methods to handle application logic.
    /// </summary>
    public class MainViewModel : IDPFLibrary.ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field for TestNumberTwo property.
        /// </summary>
        private Account _userAccount;

        private CloudTypeEnum _newCloudType;

        private int _numberOfClouds;
        private int _numberOfDevices;
        private int _numberOfFolders;

        private ObservableCollection<VCCardListItem> _cloudsCollection;
        private ObservableCollection<VCListItem> _cloudChooseCollection;
        private ObservableCollection<VCListItem> _deviceFoldersCollection;
        private ObservableCollection<VCListItem> _devicesCollection;
        private ObservableCollection<VCListItem> _folderDevicesCollection;
        private ObservableCollection<VCListItem> _foldersCollection;

        private ObservableCollection<CardListItem> _mainPageCards;

        private string _cloudEmail;
        private string _deviceName;
        private string _pairCode;

        private VCCardListItem _selectedCloudProvider;
        private VCListItem _selectedDevice;
        private VCListItem _selectedFolder;


        #endregion

        #region properties

        /// <summary>
        /// Property indicating current application user.
        /// </summary>
        public Account UserAccount
        {
            get => _userAccount;

            set => SetProperty(ref _userAccount, value);
        }

        public CloudTypeEnum NewCloudType
        {
            get => _newCloudType;
            set
            {
                SetProperty(ref _newCloudType, value);
                CloudConnectCommand?.ChangeCanExecute();
            }
        }

        public Command ChangePageCommand { get; set; }
        public Command CloudConnectCommand { get; set; }
        public Command CloudDisconnectCommand { get; set; }
        public Command CloudModifyCommand { get; set; }
        public Command DevicePairCommand { get; set; }
        public Command DeviceUnpairCommand { get; set; }
        public Command GoBackPageCommand { get; set; }
        public Command GoToCloudsListPageCommand { get; set; }

        public Command GoToDevicesListPageCommand { get; set; }

        public Command GoToFoldersListPageCommand { get; set; }
        
        public Command DeviceUnassignCommand { get; set; }
        public Command FolderUnassignCommand { get; set; }


        public int NumberOfClouds
        {
            get => _numberOfClouds;
            set => SetProperty(ref _numberOfClouds, value);
        }

        public int NumberOfDevices
        {
            get => _numberOfDevices;
            set => SetProperty(ref _numberOfDevices, value);
        }

        public int NumberOfFolders
        {
            get => _numberOfFolders;
            set => SetProperty(ref _numberOfFolders, value);
        }

        public ObservableCollection<VCCardListItem> CloudsCollection
        {
            get => _cloudsCollection;
            set => SetProperty(ref _cloudsCollection, value);
        }

        public ObservableCollection<VCListItem> CloudChooseCollection
        {
            get => _cloudChooseCollection;
            set => SetProperty(ref _cloudChooseCollection, value);
        }

        public ObservableCollection<VCListItem> DeviceFoldersCollection
        {
            get => _deviceFoldersCollection;
            set => SetProperty(ref _deviceFoldersCollection, value);
        }

        public ObservableCollection<VCListItem> DevicesCollection
        {
            get => _devicesCollection;
            set => SetProperty(ref _devicesCollection, value);
        }

        public ObservableCollection<VCListItem> FolderDevicesCollection
        {
            get => _folderDevicesCollection;
            set => SetProperty(ref _folderDevicesCollection, value);
        }
        public ObservableCollection<VCListItem> FoldersCollection
        {
            get => _foldersCollection;
            set => SetProperty(ref _foldersCollection, value);
        }

        public ObservableCollection<CardListItem> MainPageCards
        {
            get => _mainPageCards;
            set => SetProperty(ref _mainPageCards, value);
        }

        public string CloudEmail
        {
            get => _cloudEmail;
            set
            {
                SetProperty(ref _cloudEmail, value);
                CloudConnectCommand?.ChangeCanExecute();
            }
        }

        public string DeviceName
        {
            get => _deviceName;
            set
            {
                SetProperty(ref _deviceName, value);
                DevicePairCommand?.ChangeCanExecute();
            }
        }

        public string PairCode
        {
            get => _pairCode;
            set
            {
                SetProperty(ref _pairCode, value);
                DevicePairCommand?.ChangeCanExecute();
            }
        }
        public VCCardListItem SelectedCloudProvider
        {
            get => _selectedCloudProvider;
            set => SetProperty(ref _selectedCloudProvider, value);
        }

        public VCListItem SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                SetProperty(ref _selectedDevice, value);
                InitDeviceFoldersCollection();
            }
        }

        public VCListItem SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                SetProperty(ref _selectedFolder, value);
                InitFolderDevicesCollection();
            }
        }

        #endregion

        #region methods

        public MainViewModel()
        {
            PairCode = "";
            DeviceName = "";
            InitCommands();
            InitUser();
            InitCollections();
            InitMainPageCards();
        }

        private void InitCommands()
        {
            ChangePageCommand = new Command(ExecuteChangePageCommand);
            CloudConnectCommand = new Command(ExecuteCloudConnectCommand, CanExecuteCloudConnectCommand);
            CloudDisconnectCommand = new Command(ExecuteCloudDisconnectCommand);
            CloudModifyCommand = new Command(ExecuteCloudModifyCommand);
            DeviceUnpairCommand = new Command(ExecuteDeviceUnpairCommand);
            DevicePairCommand = new Command(ExecuteDevicePairCommand, CanExecuteDevicePairCommand);
            GoBackPageCommand = new Command(ExecuteGoBackPageCommand);
            GoToDevicesListPageCommand = new Command(ExecuteGoToDevicesListPageCommand);
            GoToFoldersListPageCommand = new Command(ExecuteGoToFoldersListPageCommand);
            GoToCloudsListPageCommand = new Command(ExecuteGoToCloudsListPageCommand);
            DeviceUnassignCommand = new Command(ExecuteDeviceUnassignCommand);
            FolderUnassignCommand = new Command(ExecuteFolderUnassignCommand);
        }

        private void InitMainPageCards()
        {
            MainPageCards = new ObservableCollection<CardListItem>();
            MainPageCards.Add(new CardListItem(CardTypeEnum.HighOneAction, "DEVICES", GoToDevicesListPageCommand,
                "MANAGE", "tablet_card_96px.png", "Paired devices: " + NumberOfDevices));
            MainPageCards.Add(new CardListItem(CardTypeEnum.HighOneAction, "FOLDERS", GoToFoldersListPageCommand,
                "MANAGE", "folder_card_96px.png", "Assigned folders: " + NumberOfFolders));
            MainPageCards.Add(new CardListItem(CardTypeEnum.HighOneAction, "CLOUDS", GoToCloudsListPageCommand,
                "MANAGE", "cloud_card_96px.png", "Connected clouds: " + NumberOfClouds));
        }

        private void InitCollections()
        {
            CloudChooseCollection = new ObservableCollection<VCListItem>();
            CloudsCollection = new ObservableCollection<VCCardListItem>();
            DevicesCollection = new ObservableCollection<VCListItem>();
            FoldersCollection = new ObservableCollection<VCListItem>();

            foreach (var device in UserAccount.DevicesCollection)
            {
                DevicesCollection.Add(new VCListItem(device, ChangePageCommand));

                foreach (var folder in device.FoldersCollection)
                {
                    if (FoldersCollection.FirstOrDefault(f => f.Folder == folder) == null)
                    {
                        FoldersCollection.Add(new VCListItem(folder, ChangePageCommand));
                    }
                }

            }

            foreach (var cloud in UserAccount.CloudsCollection)
            {
                CloudsCollection.Add(new VCCardListItem(CardTypeEnum.ShortTwoActions, cloud, CloudModifyCommand, CloudDisconnectCommand));
                CloudChooseCollection.Add(new VCListItem(cloud, null));
            }

            NumberOfClouds = UserAccount.CountCloudProviders();
            NumberOfDevices = UserAccount.CountDevices();
            NumberOfFolders = UserAccount.CountAllFolders();
        }

        private void InitDeviceFoldersCollection()
        {
            if (SelectedDevice == null)
            {
                return;;
            }

            DeviceFoldersCollection = new ObservableCollection<VCListItem>();

            foreach (var folder in SelectedDevice.Device.FoldersCollection)
            {
                DeviceFoldersCollection.Add(new VCListItem(folder, null, FolderUnassignCommand));
            }
        }

        private void InitFolderDevicesCollection()
        {
            if (SelectedFolder == null)
            {
                return; ;
            }

            FolderDevicesCollection = new ObservableCollection<VCListItem>();

            foreach (var device in UserAccount.DevicesCollection)
            {
                foreach (var folder in device.FoldersCollection)
                {
                    if (folder == SelectedFolder.Folder)
                    {
                        FolderDevicesCollection.Add(new VCListItem(device, null, DeviceUnassignCommand));
                    }
                }

            }
        }

        private void InitUser()
        {
            UserAccount = new Account(1);
        }

        private bool CanExecuteCloudConnectCommand()
        {
            return CloudEmail?.Length > 0 && NewCloudType != CloudTypeEnum.None;
        }

        private bool CanExecuteDevicePairCommand()
        {
            return PairCode.Length == 5 && DeviceName.Length > 0;
        }

        private void ExecuteChangePageCommand(object pageType)
        {
            Page nextPage = (Page) Activator.CreateInstance((Type) pageType);
            nextPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(nextPage);
        }

        private void ExecuteCloudConnectCommand()
        {
            UserAccount.CloudsCollection.Add(new CloudProvider(NewCloudType, CloudEmail));
            UpdateAllInformation();
            ExecuteGoBackPageCommand();
        }
        private void ExecuteCloudDisconnectCommand()
        {
            if (SelectedCloudProvider == null)
            {
                return;
            }

            UserAccount.CloudsCollection.Remove(SelectedCloudProvider.CloudProvider);
            UpdateAllInformation();
            SelectedCloudProvider = null;
        }

        private void ExecuteCloudModifyCommand()
        {
            
        }

        private void ExecuteDevicePairCommand()
        {
            UserAccount.DevicesCollection.Add(new Models.Device(DeviceName, 1));
            UpdateAllInformation();
            ExecuteGoBackPageCommand();
        }

        private void ExecuteDeviceUnpairCommand()
        {
            ExecuteGoBackPageCommand();
            UserAccount.DevicesCollection.Remove(SelectedDevice.Device);
            SelectedDevice = null;
            DeviceFoldersCollection = new ObservableCollection<VCListItem>();
            UpdateAllInformation();
        }

        private void ExecuteGoBackPageCommand()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ExecuteGoToCloudsListPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new CloudsListPage(this));
        }

        private void ExecuteGoToDevicesListPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DevicesListPage(this));
        }

        private void ExecuteGoToFoldersListPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FoldersListPage(this));
        }

        private void ExecuteDeviceUnassignCommand(object param)
        {
            UserAccount.DevicesCollection.FirstOrDefault(d => d == ((VCListItem)param).Device)?.FoldersCollection.Remove(SelectedFolder.Folder);
            UpdateAllInformation();
            Application.Current.MainPage.DisplayAlert("Unassignment", "The device has been successfully  unassiged", "OK");
        }

        private void ExecuteFolderUnassignCommand(object param)
        {
            UserAccount.DevicesCollection.FirstOrDefault(d => d == SelectedDevice.Device)?.FoldersCollection.Remove(((VCListItem)param).Folder);
            UpdateAllInformation();
            Application.Current.MainPage.DisplayAlert("Unassignment", "The folder has been successfully  unassiged", "OK");
        }

        private void UpdateAllInformation()
        {
            UserAccount.UpdateInformation();
            InitCollections();
            InitMainPageCards();
            InitDeviceFoldersCollection();
            InitFolderDevicesCollection();
        }

        #endregion
    }
}