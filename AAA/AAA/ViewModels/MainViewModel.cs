using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AAA.Models;
using AAA.Utils;
using AAA.Utils.Controls;
using AAA.Views;
using IDPFLibrary.DTO.AAA.Login.Request;
using Xamarin.Forms;
using System.Net.Http;
using AAA.Utils.Assemblers;
using Dropbox.Api;
using IDPFLibrary;
using IDPFLibrary.DTO.AAA.Account.Request;
using IDPFLibrary.DTO.AAA.Account.Response;
using IDPFLibrary.DTO.AAA.Cloud.Request;
using IDPFLibrary.DTO.AAA.Cloud.Response;
using IDPFLibrary.DTO.AAA.Device.Request;
using IDPFLibrary.DTO.AAA.Device.Response;
using IDPFLibrary.DTO.AAA.Folder.Request;
using IDPFLibrary.DTO.AAA.Folder.Response;
using IDPFLibrary.DTO.AAA.Login.Response;
using Newtonsoft.Json;

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
        /// Backing field of IsDropboxConnection property.
        /// </summary>
        private bool _isDropboxConnection;

        /// <summary>
        /// Backing field of NewCloudType property.
        /// </summary>
        private CloudProviderType _newCloudType;

        /// <summary>
        /// Backing field of NumberOfClouds property.
        /// </summary>
        private int _numberOfClouds;

        /// <summary>
        /// Backing field of NumberOfDevices property.
        /// </summary>
        private int _numberOfDevices;

        /// <summary>
        /// Backing field of NumberOfFolders property.
        /// </summary>
        private int _numberOfFolders;

        /// <summary>
        /// ID of a current user.
        /// </summary>
        private int _userId;

        /// <summary>
        /// Current time in relation to current Unix Timestamp.
        /// </summary>
        private Int32 _timeStamp;

        /// <summary>
        /// Token of Flickr used in account connecting.
        /// </summary>
        private string _tokenSecret;

        /// <summary>
        /// Backing field of CloudsCollection property.
        /// </summary>
        private ObservableCollection<VCCardListItem> _cloudsCollection;

        /// <summary>
        /// Backing field of CloudFoldersCollection property.
        /// </summary>
        private ObservableCollection<VCListItem> _cloudFoldersCollection;

        /// <summary>
        /// Backing field of CloudChooseCollection property.
        /// </summary>
        private ObservableCollection<VCListItem> _cloudChooseCollection;

        /// <summary>
        /// Backing field of DeviceFoldersCollection property.
        /// </summary>
        private ObservableCollection<VCListItem> _deviceFoldersCollection;

        /// <summary>
        /// Backing field of DevicesCollection property.
        /// </summary>
        private ObservableCollection<VCListItem> _devicesCollection;

        /// <summary>
        /// Backing field of FolderDevicesCollection property.
        /// </summary>
        private ObservableCollection<VCListItem> _folderDevicesCollection;

        /// <summary>
        /// Backing field of FoldersCollection property.
        /// </summary>
        private ObservableCollection<VCListItem> _foldersCollection;

        /// <summary>
        /// Backing field of MainPageCards property.
        /// </summary>
        private ObservableCollection<CardListItem> _mainPageCards;

        /// <summary>
        /// Name of a current user.
        /// </summary>
        private string _username;

        /// <summary>
        /// Authentication token of a current user.
        /// </summary>
        private string _userToken;

        /// <summary>
        /// Backing field of Password property.
        /// </summary>
        private string _password;

        /// <summary>
        /// Backing field of NewCloudName property.
        /// </summary>
        private string _newCloudName;

        /// <summary>
        /// Backing field of DeviceName property.
        /// </summary>
        private string _deviceName;

        /// <summary>
        /// Backing field of PairCode property.
        /// </summary>
        private string _pairCode;

        /// <summary>
        /// Token of Dropbox used in account connecting.
        /// </summary>
        private string _dropboxCode;

        /// <summary>
        /// Backing field of SelectedCloudProvider property.
        /// </summary>
        private VCCardListItem _selectedCloudProvider;

        /// <summary>
        /// Backing field of SelectedDevice property.
        /// </summary>
        private Models.Device _selectedDevice;

        /// <summary>
        /// Backing field of SelectedFolder property.
        /// </summary>
        private SFolder _selectedFolder;

        /// <summary>
        /// Backing field of SelectedCloud property.
        /// </summary>
        private RCloud _selectedCloud;

        /// <summary>
        /// Backing field of DevicesResponseDto property.
        /// </summary>
        private AppGetDevicesResponseDTO _devicesResponseDto;

        /// <summary>
        /// Backing field of CloudsResponseDto property.
        /// </summary>
        private AppGetCloudsResponseDTO _cloudsResponseDto;

        /// <summary>
        /// Backing field of ChangePasswordModel property.
        /// </summary>
        private AppChangePasswordRequestDTO _changePasswordModel;

        /// <summary>
        /// Backing field of RegisterUser property.
        /// </summary>
        private AppRegisterRequestDTO _registerUser;

        /// <summary>
        /// Backing field of DeviceFoldersDto property.
        /// </summary>
        private AppGetDeviceFoldersResponseDTO _deviceFoldersDto;

        /// <summary>
        /// Backing field of CreateCloudRequestDto property.
        /// </summary>
        private AppCreateCloudRequestDTO _createCloudRequestDto;

        /// <summary>
        /// Backing field of CloudFoldersResponseDto property.
        /// </summary>
        private AppGetCloudFoldersResponseDTO _cloudFoldersResponseDto;

        /// <summary>
        /// Backing field of WebViewSourceCloud property.
        /// </summary>
        private WebViewSource _webViewSourceCloud;

        #endregion

        #region properties

        /// <summary>
        /// Flag indicating whether the user tries to connect with Dropbox or not.
        /// </summary>
        public bool IsDropboxConnection
        {
            get => _isDropboxConnection;

            set => SetProperty(ref _isDropboxConnection, value);
        }

        /// <summary>
        /// Property indicating type of a cloud to connect to.
        /// </summary>
        public CloudProviderType NewCloudType
        {
            get => _newCloudType;
            set
            {
                SetProperty(ref _newCloudType, value);
                CloudConnectCommand.ChangeCanExecute();
            }
        }

        /// <summary>
        /// Command which calls ExecuteChangePageCommand method.
        /// </summary>
        public Command ChangePageCommand { get; set; }
        public Command ChangePasswordCommand { get; set; }
        public Command CloudConnectCommand { get; set; }
        public Command CloudDisconnectCommand { get; set; }
        public Command ConfirmDropboxCommand { get; set; }
        public Command DevicePairCommand { get; set; }
        public Command DeviceUnpairCommand { get; set; }
        public Command GoBackPageCommand { get; set; }
        public Command GoToChooseCloudPageCommand { get; set; }
        public Command GoToCloudsListPageCommand { get; set; }

        public Command GoToDevicePageCommand { get; set; }

        public Command GoToDevicesListPageCommand { get; set; }

        public Command GoToFoldersListPageCommand { get; set; }

        public Command GoToMainPageCommand { get; set; }

        public Command GoToProfilePageCommand { get; set; }

        public Command GoToSignUpPageCommand { get; set; }

        public Command GoToCloudFoldersPage { get; set; }
        public Command FolderAssignCommand { get; set; }
        public Command FolderUnassignCommand { get; set; }
        public Command RefreshCommand { get; set; }
        public Command SignUpCommand { get; set; }


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

        public ObservableCollection<VCListItem> CloudFoldersCollection
        {
            get => _cloudFoldersCollection;
            set => SetProperty(ref _cloudFoldersCollection, value);
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

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string NewCloudName
        {
            get => _newCloudName;
            set
            {
                SetProperty(ref _newCloudName, value);
                CloudConnectCommand.ChangeCanExecute();
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
        
        public string DropboxCode
        {
            get => _dropboxCode;
            set => SetProperty(ref _dropboxCode, value);
        }
        public VCCardListItem SelectedCloudProvider
        {
            get => _selectedCloudProvider;
            set => SetProperty(ref _selectedCloudProvider, value);
        }

        public Models.Device SelectedDevice
        {
            get => _selectedDevice;
            set => SetProperty(ref _selectedDevice, value);
        }

        public SFolder SelectedFolder
        {
            get => _selectedFolder;
            set => SetProperty(ref _selectedFolder, value);
        }

        public RCloud SelectedCloud
        {
            get => _selectedCloud;
            set => SetProperty(ref _selectedCloud, value);
        }

        public AppGetDevicesResponseDTO DevicesResponseDto
        {
            get => _devicesResponseDto;
            set
            {
                SetProperty(ref _devicesResponseDto, value);
                AssambleDevicesDtoToCollection();
            }
        }

        public AppGetCloudsResponseDTO CloudsResponseDto
        {
            get => _cloudsResponseDto;
            set
            {
                SetProperty(ref _cloudsResponseDto, value);
                AssambleCloudsDtoToCollection();
            }
        }

        public AppChangePasswordRequestDTO ChangePasswordModel
        {
            get => _changePasswordModel;
            set => SetProperty(ref _changePasswordModel, value);
        }

        public AppRegisterRequestDTO RegisterUser
        {
            get => _registerUser;
            set => SetProperty(ref _registerUser, value);
        }

        public AppGetDeviceFoldersResponseDTO DeviceFoldersDto
        {
            get => _deviceFoldersDto;
            set
            {
                SetProperty(ref _deviceFoldersDto, value);
                AssambleFoldersDtoToCollection();
            }
        }

        public AppCreateCloudRequestDTO CreateCloudRequestDto
        {
            get => _createCloudRequestDto;
            set => SetProperty(ref _createCloudRequestDto, value);
        }

        public AppGetCloudFoldersResponseDTO CloudFoldersResponseDto
        {
            get => _cloudFoldersResponseDto;
            set
            {
                SetProperty(ref _cloudFoldersResponseDto, value);
                AssambleCloudFoldersDtoToCollection();
            }
        }

        public WebViewSource WebViewSourceCloud
        {
            get => _webViewSourceCloud;
            set => SetProperty(ref _webViewSourceCloud, value);
        }

        #endregion

        #region methods

        #region Init

        /// <summary>
        /// MainViewModel class constructor.
        /// </summary>
        public MainViewModel()
        {
            InitProperties();
            InitCommands();
            InitMainPageCards();
            InitDto();
            InitCollections();
        }

        /// <summary>
        /// Initialize commands.
        /// </summary>
        private void InitCommands()
        {
            ChangePageCommand = new Command(ExecuteChangePageCommand);
            ChangePasswordCommand = new Command(ExecuteChangePasswordCommand);
            CloudConnectCommand = new Command(ExecuteCloudConnectCommand, CanExecuteCloudConnectCommand);
            CloudDisconnectCommand = new Command(ExecuteCloudDisconnectCommand);
            ConfirmDropboxCommand = new Command(ExecuteConfirmDropboxCommand);
            DeviceUnpairCommand = new Command(ExecuteDeviceUnpairCommand);
            DevicePairCommand = new Command(ExecuteDevicePairCommand, CanExecuteDevicePairCommand);
            GoBackPageCommand = new Command(ExecuteGoBackPageCommand);
            GoToDevicePageCommand = new Command(ExecuteGoToDevicePageCommand);
            GoToDevicesListPageCommand = new Command(ExecuteGoToDevicesListPageCommand);
            GoToFoldersListPageCommand = new Command(ExecuteGoToFoldersListPageCommand);
            GoToChooseCloudPageCommand = new Command(ExecuteGoToChooseCloudPageCommand);
            GoToCloudsListPageCommand = new Command(ExecuteGoToCloudsListPageCommand);
            GoToMainPageCommand = new Command(ExecuteGoToMainPageCommand);
            GoToProfilePageCommand = new Command(ExecuteGoToProfilePageCommand);
            GoToSignUpPageCommand = new Command(ExecuteGoToSignUpPageCommand);
            GoToCloudFoldersPage = new Command(ExecuteGoToCloudFoldersPage);
            FolderAssignCommand = new Command(ExecuteFolderAssignCommand);
            FolderUnassignCommand = new Command(ExecuteFolderUnassignCommand);
            RefreshCommand = new Command(ExecuteRefreshCommand);
            SignUpCommand = new Command(ExecuteSignUpCommand);
        }

        /// <summary>
        /// Initialize collections.
        /// </summary>
        private void InitCollections()
        {
            CloudsCollection = new ObservableCollection<VCCardListItem>();
            CloudChooseCollection = new ObservableCollection<VCListItem>();
            DevicesCollection = new ObservableCollection<VCListItem>();
            FoldersCollection = new ObservableCollection<VCListItem>();
            CloudFoldersCollection = new ObservableCollection<VCListItem>();
        }

        /// <summary>
        /// Initialize DTOs.
        /// </summary>
        private void InitDto()
        {
            RegisterUser = new AppRegisterRequestDTO();
            ChangePasswordModel = new AppChangePasswordRequestDTO();
            var temp = new AppGetCloudsResponseDTO();
            temp.clouds = new List<RCloud>();
            CloudsResponseDto = temp;
            var temp2 = new AppGetDevicesResponseDTO();
            temp2.Devices = new List<SDeviceName>();
            DevicesResponseDto = temp2;
            var temp3 = new AppGetDeviceFoldersResponseDTO();
            temp3.Folders = new List<SFolder>();
            DeviceFoldersDto = temp3;
            CreateCloudRequestDto = new AppCreateCloudRequestDTO();
        }

        /// <summary>
        /// Initialize MainPage cards.
        /// </summary>
        private void InitMainPageCards()
        {
            MainPageCards = new ObservableCollection<CardListItem>();
            MainPageCards.Add(new CardListItem(CardTypeEnum.HighOneAction, "DEVICES", GoToDevicesListPageCommand,
                "MANAGE", "tablet_card_96px.png", "Paired devices: " + NumberOfDevices));
            MainPageCards.Add(new CardListItem(CardTypeEnum.HighOneAction, "CLOUDS", GoToCloudsListPageCommand,
                "MANAGE", "cloud_card_96px.png", "Connected clouds: " + NumberOfClouds));
            MainPageCards.Add(new CardListItem(CardTypeEnum.HighOneAction, "PROFILE", GoToProfilePageCommand,
                "MANAGE", "user_card_96px.png", Username));
        }

        /// <summary>
        /// Initialize properties.
        /// </summary>
        private void InitProperties()
        {
            PairCode = "";
            DeviceName = "";
            DropboxCode = "";
        }

        #endregion

        #region command execution

        /// <summary>
        /// Checks whether CloudConnectCommand can be executed or not.
        /// </summary>
        /// <returns>True if CloudConnectCommand can be executed, false otherwise.</returns>
        private bool CanExecuteCloudConnectCommand()
        {
            return NewCloudName?.Length > 0 && NewCloudType != CloudProviderType.None;
        }

        /// <summary>
        /// Checks whether DevicePairCommand can be executed or not.
        /// </summary>
        /// <returns>True if DevicePairCommand can be executed, false otherwise.</returns>
        private bool CanExecuteDevicePairCommand()
        {
            return PairCode.Length == 7 && DeviceName.Length > 0;
        }

        /// <summary>
        /// Handles execution of ChangePageCommand.
        /// Changes page to the one indicated by parameter.
        /// </summary>
        /// <param name="pageType">Parameter indicating type of page.</param>
        private void ExecuteChangePageCommand(object pageType)
        {
            Page nextPage = (Page)Activator.CreateInstance((Type)pageType);
            nextPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(nextPage);
        }

        /// <summary>
        /// Handles execution of ChangePasswordCommand.
        /// Calls method ChangePassword in order to change password.
        /// Clears ChangePasswordModel property.
        /// </summary>
        private async void ExecuteChangePasswordCommand()
        {
            if (await ChangePassword())
            {
                ChangePasswordModel.OldPassword = "";
                ChangePasswordModel.Password = "";
                ChangePasswordModel.Password2 = "";
            }
        }

        /// <summary>
        /// Handles execution of CloudConnectCommand.
        /// Calls GetConnectionToCloudProvider.
        /// </summary>
        private async void ExecuteCloudConnectCommand()
        {
            await GetConnectionToCloudProvider();
        }

        /// <summary>
        /// Handles execution of CloudDisconnectCommand.
        /// Calls DisconnectCloud method in order to disconnect selected cloud.
        /// </summary>
        /// <param name="item">Selected cloud.</param>
        private async void ExecuteCloudDisconnectCommand(object item)
        {
            if (item is VCCardListItem selectedCloud)
            {
                SelectedCloudProvider = selectedCloud;
                var result = await DisconnectCloud();
                if (result)
                {
                    SelectedCloudProvider = null;
                    GetClouds();
                }
            }
        }

        /// <summary>
        /// Handles execution of ConfirmDropboxCommand.
        /// Receives access token from Dropbox.
        /// Calls CreateCloud method in order to connect with new cloud account.
        /// </summary>
        private async void ExecuteConfirmDropboxCommand()
        {
            OAuth2Response accessToken = await DropboxOAuth2Helper.ProcessCodeFlowAsync(DropboxCode,
                DropboxApi.DropBoxApiKey, DropboxApi.DropBoxApiKeySecret);

            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PopAsync();

            CreateCloudRequestDto.AccountId = _userId;
            CreateCloudRequestDto.AuthToken = _userToken;
            CreateCloudRequestDto.Provider = NewCloudType;
            CreateCloudRequestDto.CloudName = NewCloudName;
            CreateCloudRequestDto.UserId = accessToken.Uid;
            CreateCloudRequestDto.Token = accessToken.AccessToken;
            CreateCloudRequestDto.TokenSecret = "";

            var result = await CreateCloud();
        }

        /// <summary>
        /// Handles execution of DevicePairCommand.
        /// Calls PairDevice method in order to pair with a new DPF.
        /// </summary>
        private async void ExecuteDevicePairCommand()
        {
            var result = await PairDevice();
            if (result)
            {
                PairCode = "";
                DeviceName = "";
                GetDevices();
                ExecuteGoBackPageCommand();
            }
        }

        /// <summary>
        /// Handles execution of DeviceUnpairCommand.
        /// Calls UnpairDevice method in order to unpair a DPF.
        /// </summary>
        private async void ExecuteDeviceUnpairCommand()
        {
            if (await UnpairDevice())
            {
                ExecuteGoBackPageCommand();
                GetDevices();
                GetClouds();
                SelectedDevice = null;
            }
        }

        /// <summary>
        /// Handles execution of FolderAssignCommand.
        /// Calls AssignFolder method in order to assign selected folder with a DPF.
        /// </summary>
        /// <param name="param">Selected folder.</param>
        private async void ExecuteFolderAssignCommand(object param)
        {
            if (param is VCListItem selectedFolder)
            {
                var result = await AssignFolder(selectedFolder.FolderUniversal);
                if (result)
                {
                    GetSelectedCloudFolders();
                    GetSelectedDevice();
                }
            }
        }

        /// <summary>
        /// Handles execution of GoBackPageCommand.
        /// Changes page to the previous one.
        /// </summary>
        private void ExecuteGoBackPageCommand()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Handles execution of GoToChooseCloudPageCommand.
        /// Changes page to ChooseCloudPage.
        /// </summary>
        private void ExecuteGoToChooseCloudPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new ChooseCloudPage(this));
        }

        /// <summary>
        /// Handles execution of GoToCloudFoldersPage.
        /// Changes page to CloudFoldersPage.
        /// </summary>
        /// <param name="param">Selected page.</param>
        private void ExecuteGoToCloudFoldersPage(object param)
        {
            if (param is VCListItem selectedCloud)
            {
                CloudFoldersCollection.Clear();
                SelectedCloud = selectedCloud.Cloud;
                Application.Current.MainPage.Navigation.PushAsync(new ChooseCloudFolderPage(this));
                GetSelectedCloudFolders();
            }
        }

        /// <summary>
        /// Handles execution of GoToCloudsListCommand.
        /// Changes page to CloudsListPage.
        /// </summary>
        private void ExecuteGoToCloudsListPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new CloudsListPage(this));
        }

        /// <summary>
        /// Handles execution of GoToDevicePageCommand.
        /// Changes page to DevicePage.
        /// </summary>
        /// <param name="item">Selected device.</param>
        private void ExecuteGoToDevicePageCommand(object item)
        {
            if (item is VCListItem selectedDevice)
            {
                SelectedDevice = new Models.Device(selectedDevice.Device.Name, selectedDevice.Device.DeviceId);
                GetSelectedDevice();
                var newPage = new DevicePage
                {
                    BindingContext = this
                };
                Application.Current.MainPage.Navigation.PushAsync(newPage);
            }
        }

        /// <summary>
        /// Handles execution of GoToDevicesListPageCommand.
        /// Changes page to DevicesListPage.
        /// </summary>
        private void ExecuteGoToDevicesListPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DevicesListPage(this));
        }

        /// <summary>
        /// Handles execution of GoToFoldersListPageCommand.
        /// Changes page to FoldersListPage.
        /// </summary>
        private void ExecuteGoToFoldersListPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FoldersListPage(this));
        }

        /// <summary>
        /// Handles execution of GoToMainPageCommand.
        /// Calls LoginTask method in order to log in a user.
        /// If succeeded, changes page to MainAppPage.
        /// </summary>
        private async void ExecuteGoToMainPageCommand()
        {
            if (await LoginTask())
            {
                GetDevices();
                GetClouds();
                Application.Current.MainPage = new NavigationPage(new MainAppPage(this));
                MainPageCards[2].CardSubtext = Username;
            }
        }

        /// <summary>
        /// Handles execution of GoToProfilePageCommand.
        /// Changes page to ProfilePage.
        /// </summary>
        private void ExecuteGoToProfilePageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new ProfilPage(this));
        }

        /// <summary>
        /// Handles execution of GoToSignUpPageCommand.
        /// Changes page to SignUpPage.
        /// </summary>
        private void ExecuteGoToSignUpPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new SignUpPage(this));
        }

        /// <summary>
        /// Handles execution of FolderUnassignCommand.
        /// Calls UnassignFolderFromDevice method in order to unassign 
        /// selected folder from a device.
        /// </summary>
        /// <param name="param">Selected folder.</param>
        private async void ExecuteFolderUnassignCommand(object param)
        {
            if (param is VCListItem selectedFolder)
            {
                SelectedFolder = selectedFolder.Folder;
                var result = await UnassignFolderFromDevice(SelectedFolder);
                if (result)
                {
                    GetSelectedDevice();
                }
            }
        }

        /// <summary>
        /// Handles execution of RefreshCommand.
        /// Calls GetDevices and GetClouds methods 
        /// to refresh basic account's data.
        /// </summary>
        private async void ExecuteRefreshCommand()
        {
            GetDevices();
            GetClouds();
        }

        /// <summary>
        /// Handles execution of SignUpCommand.
        /// Calls AccountRegister method.
        /// </summary>
        private async void ExecuteSignUpCommand()
        {
            await AccountRegister();
        }

        #endregion

        #region requests

        /// <summary>
        /// Sends request to register account.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> AccountRegister()
        {
            try
            {
                if (RegisterUser.Login == null || RegisterUser.Password == null || RegisterUser.Password2 == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Fill in all fields", "OK");
                    return false;
                }
                if (CheckIfNetworkConnection())
                {
                    AppRegisterRequestDTO requestDto = new AppRegisterRequestDTO
                    {
                        Password = RegisterUser.Password,
                        Password2 = RegisterUser.Password2,
                        Login = RegisterUser.Login
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    var result = JsonConvert.DeserializeObject<AppRegisterResponseDTO>(
                        await RestRequester.SendRequest("/Login/AppRegister", HttpMethod.Post, json));
                    if (result.IsSuccess)
                    {
                        await Application.Current.MainPage.DisplayAlert("Account registered", "You can now sign in to use application.", "OK");
                        await Application.Current.MainPage.Navigation.PopAsync();
                        return result.IsSuccess;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", result.Message, "OK");
                        return false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline",
                        "Connect to the Internet to start using application.", "OK");
                    return false;
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Sends request to assign folder to the account.
        /// </summary>
        /// <param name="selectedCloudFolder">Selected folder.</param>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> AssignFolder(SUniversalFolder selectedCloudFolder)
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppAddFolderRequestDTO requestDto = new AppAddFolderRequestDTO
                    {
                        UserId = _userId,
                        Token = _userToken,
                        DeviceId = SelectedDevice.DeviceId,
                        CloudId = SelectedCloud.CloudId,
                        Folders = new List<string> { selectedCloudFolder.Title }
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    var result = JsonConvert.DeserializeObject<AppAddFolderResponseDTO>(
                        await RestRequester.SendRequest("/Folder/AppAddFolder", HttpMethod.Post, json));

                    if (CheckIfAuthenticated(result.Auth))
                    {
                        await Application.Current.MainPage.DisplayAlert("Folder assignment", "Folder has been successfully assigned", "OK");
                        return true;
                    }

                    return false;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to use the application.", "OK");
                    return false;
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Sends request to change password.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> ChangePassword()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppChangePasswordRequestDTO requestDto = new AppChangePasswordRequestDTO()
                    {
                        OldPassword = ChangePasswordModel.OldPassword,
                        Password = ChangePasswordModel.Password,
                        Password2 = ChangePasswordModel.Password2,
                        AccountId = _userId,
                        Token = _userToken
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    var result = JsonConvert.DeserializeObject<AppChangePasswordResponseDTO>(
                        await RestRequester.SendRequest("/Account/AppChangePassword", HttpMethod.Post, json));
                    await Application.Current.MainPage.DisplayAlert("Change password result", result.Message, "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to use the application.", "OK");
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
            }
            return true;
        }

        /// <summary>
        /// Sends request to connect new cloud to the account.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> CreateCloud()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppCreateCloudRequestDTO requestDto = CreateCloudRequestDto;
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    var result = JsonConvert.DeserializeObject<AppCreateCloudResponseDTO>(
                        await RestRequester.SendRequest("/Cloud/AppCreateCloud", HttpMethod.Put, json));

                    if (CheckIfAuthenticated(result.Auth))
                    {
                        GetClouds();
                        return true;
                    }

                    return false;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to use the application.", "OK");
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
            }
            return false;
        }

        /// <summary>
        /// Sends request to disconnect cloud from the account.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> DisconnectCloud()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppDeleteCloudRequestDTO requestDto = new AppDeleteCloudRequestDTO
                    {
                        UserId = _userId,
                        Token = _userToken,
                        CloudId = SelectedCloudProvider.CloudProvider.CloudId
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    var result = JsonConvert.DeserializeObject<AppDeleteCloudResponseDTO>(
                        await RestRequester.SendRequest("/Cloud/AppDeleteCloud", HttpMethod.Post, json));

                    if (CheckIfAuthenticated(result.Auth))
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to use the application.", "OK");
                    return false;
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Sends request to get all connected clouds of the account.
        /// </summary>
        private async void GetClouds()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppGetCloudsRequestDTO requestDto = new AppGetCloudsRequestDTO
                    {
                        UserId = _userId,
                        Token = _userToken
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    CloudsResponseDto = JsonConvert.DeserializeObject<AppGetCloudsResponseDTO>(
                        await RestRequester.SendRequest("/Cloud/AppGetClouds", HttpMethod.Post, json));
                    CheckIfAuthenticated(CloudsResponseDto.Auth);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline",
                        "Connect to the Internet to use the application.", "OK");
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
            }
        }

        /// <summary>
        /// Sends request to get all paired devices of the account.
        /// </summary>
        private async void GetDevices()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppGetDevicesRequestDTO requestDto = new AppGetDevicesRequestDTO
                    {
                        AccountId = _userId,
                        Token = _userToken
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    DevicesResponseDto = JsonConvert.DeserializeObject<AppGetDevicesResponseDTO>(
                        await RestRequester.SendRequest("/Device/AppGetDevices", HttpMethod.Post, json));
                    CheckIfAuthenticated(DevicesResponseDto.Auth);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to use the application.", "OK");
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
            }
        }

        /// <summary>
        /// Sends request to get folders of a selected cloud.
        /// </summary>
        private async void GetSelectedCloudFolders()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppGetCloudFoldersRequestDTO requestDto = new AppGetCloudFoldersRequestDTO
                    {
                        Token = _userToken,
                        DeviceId = SelectedDevice.DeviceId,
                        UserId = _userId,
                        CloudId = SelectedCloud.CloudId
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    CloudFoldersResponseDto = JsonConvert.DeserializeObject<AppGetCloudFoldersResponseDTO>(
                        await RestRequester.SendRequest("/Folder/AppGetCloudFolders", HttpMethod.Post, json));

                    CheckIfAuthenticated(CloudFoldersResponseDto.Auth);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to start using application.", "OK");
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
            }
        }

        /// <summary>
        /// Sends request to get data about selected device.
        /// </summary>
        private async void GetSelectedDevice()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppGetDeviceFoldersRequestDTO requestDto = new AppGetDeviceFoldersRequestDTO
                    {
                        Token = _userToken,
                        DeviceId = SelectedDevice.DeviceId,
                        AccountId = _userId
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    DeviceFoldersDto = JsonConvert.DeserializeObject<AppGetDeviceFoldersResponseDTO>(
                        await RestRequester.SendRequest("/Folder/AppGetDeviceFolders", HttpMethod.Post, json));

                    if (CheckIfAuthenticated(DeviceFoldersDto.Auth))
                    {
                        SelectedDevice.FoldersCollection = FoldersCollection;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to start using application.", "OK");
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
            }
        }

        /// <summary>
        /// Sends request to log user in.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> LoginTask()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                        AppLoginRequestDTO requestDto = new AppLoginRequestDTO
                        {
                            Login = Username,
                            Password = Password
                        };
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                        var result = JsonConvert.DeserializeObject<AppLoginResponseDTO>(
                            await RestRequester.SendRequest("/Login/AppLogin", HttpMethod.Post, json));
                        if (result.IsSuccess)
                        {
                            _userToken = result.Token;
                            _userId = result.UserId;
                            return result.IsSuccess;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", result.Message, "OK");
                            return false;
                        }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to start using application.", "OK");
                    return false;
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Sends request to pair device with the account.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> PairDevice()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppPairDeviceRequestDTO requestDto = new AppPairDeviceRequestDTO
                    {
                        DeviceName = DeviceName,
                        PairCode = PairCode,
                        UserId = _userId,
                        Token = _userToken
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    var result = JsonConvert.DeserializeObject<AppPairDeviceResponseDTO>(
                        await RestRequester.SendRequest("/Device/AppPairDevice", HttpMethod.Post, json));

                    if (CheckIfAuthenticated(result.Auth))
                    {
                        await Application.Current.MainPage.DisplayAlert("Pair device", result.Message, "OK");
                        return true;
                    }

                    return false;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to use the application.", "OK");
                    return false;
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Sends request to unpair device from the account.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> UnpairDevice()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppUnpairDeviceRequestDTO requestDto = new AppUnpairDeviceRequestDTO
                    {
                        DeviceId = SelectedDevice.DeviceId,
                        AccountId = _userId,
                        Token = _userToken
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    var result = JsonConvert.DeserializeObject<AppUnpairDeviceResponseDTO>(
                        await RestRequester.SendRequest("/Device/AppUnpairDevice", HttpMethod.Post, json));

                    if (CheckIfAuthenticated(result.Auth))
                    {
                        await Application.Current.MainPage.DisplayAlert("Unpair device", "The device has been unpaired.", "OK");
                        return true;
                    }

                    return false;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to use the application.", "OK");
                    return false;
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Sends request to unassign folder from the account.
        /// </summary>
        /// <param name="folderToUnassign">Selected folder.</param>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> UnassignFolderFromDevice(SFolder folderToUnassign)
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    AppDeleteFolderRequestDTO requestDto = new AppDeleteFolderRequestDTO
                    {
                        UserId = _userId,
                        FolderId = folderToUnassign.FolderId,
                        Token = _userToken
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);
                    var result = JsonConvert.DeserializeObject<AppDeleteFolderResponseDTO>(
                        await RestRequester.SendRequest("/Folder/AppDeleteFolder", HttpMethod.Delete, json));

                    if (CheckIfAuthenticated(result.Auth))
                    {
                        await Application.Current.MainPage.DisplayAlert("Folder unassignment", "The folder has been successfully  unassiged", "OK");
                        return true;
                    }

                    return false;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Offline", "Connect to the Internet to use the application.", "OK");
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
            }
            return false;
        }

        #endregion

        #region CloudProvidersConnection

        /// <summary>
        /// Checks to which cloud provider user wants to connect.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> GetConnectionToCloudProvider()
        {
            switch (NewCloudType)
            {
                case CloudProviderType.Dropbox:
                    IsDropboxConnection = true;
                    return await GetConnectionWithDropbox();
                case CloudProviderType.Flickr:
                    IsDropboxConnection = false;
                    return await GetConnectionWithFlickr();
                default:
                    return false;
            }
        }

        /// <summary>
        /// Leads to web page where user connects with Dropbox.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> GetConnectionWithDropbox()
        {
            try
            {
                WebViewSourceCloud = DropboxOAuth2Helper.GetAuthorizeUri(DropboxApi.DropBoxApiKey);
                var tempPage = new WebPage(this)
                {
                    Title = "Dropbox",
                };
                await Application.Current.MainPage.Navigation.PushAsync(tempPage);
                return true;
            }
            catch (Exception e)
            {
                OnErrorOccurred(this, e.Message);
            }

            return false;
        }

        /// <summary>
        /// Sends requests and processes responses from FlickrAPI to get access token.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        private async Task<bool> GetConnectionWithFlickr()
        {
            _timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var requestSignature = FlickrAPI.GetRequestToSignature();
            string requestToken;

            string signature = DependencyService.Get<ICloudsConnectionsService>()
                .GetSignature(FlickrAPI.SharedSecret + "&", string.Format(requestSignature, _timeStamp));

            signature = signature.Replace("=", "%3D");
            signature = signature.Replace("+", "%2B");

            var fullRequestUri = string.Format(FlickrAPI.RequestTokenURL, _timeStamp, signature);

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(fullRequestUri),
                    Method = HttpMethod.Get,
                };

                var response = await client.SendAsync(request);
                var contents = await response.Content.ReadAsStringAsync();
                var token = FlickrAPI.GetRequestTokenFromText(contents);
                requestToken = token.Item1;
                _tokenSecret = token.Item2;
            }

            var authorizeUri = string.Format(FlickrAPI.AuthorizeTokenURL, requestToken);
            var webView = new WebView
            {
                Source = authorizeUri
            };
            webView.Navigated += WebViewOnNavigated;
            var tempPage = new WebPage(this)
            {
                Content = webView,
                Title = "Flickr",
            };
            await Application.Current.MainPage.Navigation.PushAsync(tempPage);

            return true;
        }

        /// <summary>
        /// Sends requests and processes responses from FlickrAPI to get access token.
        /// Calls CreateCloud method.
        /// </summary>
        /// <param name="sender">WebView instance.</param>
        /// <param name="eventArgs">Arguments of the event.</param>
        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs eventArgs)
        {
            var accessToken = FlickrAPI.GetAuthorizationTokenFromUrl(eventArgs.Url);

            if (accessToken != null)
            {
                await App.Current.MainPage.Navigation.PopAsync();
                await App.Current.MainPage.Navigation.PopAsync();
                var requestSignature = FlickrAPI.GetAccessToSignature();

                string signature = DependencyService.Get<ICloudsConnectionsService>()
                    .GetSignature(FlickrAPI.SharedSecret + "&" + _tokenSecret, string.Format(requestSignature, _timeStamp, accessToken.Item1, accessToken.Item2));

                signature = signature.Replace("=", "%3D");
                signature = signature.Replace("+", "%2B");

                var fullRequestUri = string.Format(FlickrAPI.AccessTokenURL, signature, _timeStamp, accessToken.Item1, accessToken.Item2);

                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(fullRequestUri),
                        Method = HttpMethod.Get,
                    };

                    var response = await client.SendAsync(request);
                    var contents = await response.Content.ReadAsStringAsync();
                    var token = FlickrAPI.GetAccessTokenFromUrl(contents);

                    CreateCloudRequestDto.AccountId = _userId;
                    CreateCloudRequestDto.CloudName = NewCloudName;
                    CreateCloudRequestDto.AuthToken = _userToken;
                    CreateCloudRequestDto.Provider = NewCloudType;
                    CreateCloudRequestDto.Token = token[1];
                    CreateCloudRequestDto.TokenSecret = token[2];
                    CreateCloudRequestDto.UserId = token[3].Replace("%40", "@");

                    var result = await CreateCloud();
                }
            }
        }

        #endregion

        #region other methods

        /// <summary>
        /// Checks whether the user's token is valid.
        /// </summary>
        /// <returns>True if the user's token is valid, false otherwise.</returns>
        private bool CheckIfAuthenticated(AuthorizationResponse response)
        {
            if (response == AuthorizationResponse.TokenExpired || response == AuthorizationResponse.InvalidToken)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                Application.Current.MainPage.DisplayAlert("Error", "Token has expired or is invalid. Log in again", "OK");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calls DependencyService to check whether the device is connected to the Internet.
        /// </summary>
        /// <returns>True if the device is connected to the Internet, false otherwise.</returns>
        private bool CheckIfNetworkConnection()
        {
            return DependencyService.Get<INetworkConnectionService>().CheckIfNetworkConnected();
        }

        /// <summary>
        /// Handles ErrorOccurred event.
        /// Notifies the user about the error.
        /// </summary>
        /// <param name="sender">Instance of object which invoked the event.</param>
        /// <param name="errorMessage">Message of the error.</param>
        private void OnErrorOccurred(object sender, string errorMessage)
        {
            try
            {
                Application.Current.MainPage.DisplayAlert("Error occurred", errorMessage, "OK");
            }
            catch (Exception exception)
            {
                return;
            }
        }
        
        /// <summary>
        /// Updates FoldersCollection property.
        /// Updates NumberOfFolders property.
        /// </summary>
        private void AssambleFoldersDtoToCollection()
        {
            FoldersCollection =
                DtoTOCollectionAssamblers.AssambleFoldersDtoToCollection(DeviceFoldersDto, FolderUnassignCommand);

            NumberOfFolders = FoldersCollection.Count;
        }

        /// <summary>
        /// Updates CloudsCollection property.
        /// Updates CloudChooseCollection property.
        /// Updates NumberOfClouds property.
        /// Updates MainPageCards property.
        /// </summary>
        private void AssambleCloudsDtoToCollection()
        {
            CloudsCollection =
                DtoTOCollectionAssamblers.AssambleCloudsDtoToCollectionCard(CloudsResponseDto, CloudDisconnectCommand);
            CloudChooseCollection =
                DtoTOCollectionAssamblers.AssambleCloudsDtoToCollectionList(CloudsResponseDto, GoToCloudFoldersPage);
            NumberOfClouds = CloudsCollection.Count;
            MainPageCards[1].CardSubtext = "Connected clouds: " + NumberOfClouds;
        }

        /// <summary>
        /// Updates DevicesCollection property.
        /// Updates NumberOfDevices property.
        /// Updates MainPageCards property.
        /// </summary>
        private void AssambleDevicesDtoToCollection()
        {
            DevicesCollection =
                DtoTOCollectionAssamblers.AssambleDevicesDtoToCollection(DevicesResponseDto, GoToDevicePageCommand);
            NumberOfDevices = DevicesCollection.Count;
            MainPageCards[0].CardSubtext = "Paired devices: " + NumberOfDevices;
        }

        /// <summary>
        /// Updates CloudFoldersCollection property.
        /// </summary>
        private void AssambleCloudFoldersDtoToCollection()
        {
            CloudFoldersCollection =
                DtoTOCollectionAssamblers.AssambleCloudFoldersDtoToCollection(CloudFoldersResponseDto,
                    FolderAssignCommand);
        }

        #endregion

        #endregion
    }
}