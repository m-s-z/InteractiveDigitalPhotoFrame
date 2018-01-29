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
using Account = AAA.Models.Account;

namespace AAA.ViewModels
{
    /// <summary>
    /// MainViewModel class.
    /// Provides properites and methods to handle application logic.
    /// </summary>
    public class MainViewModel : IDPFLibrary.ViewModelBase
    {
        #region fields

        private bool _isDropboxConnection;

        /// <summary>
        /// Backing field for TestNumberTwo property.
        /// </summary>
        private Account _userAccount;

        private CloudProviderType _newCloudType;

        private int _numberOfClouds;
        private int _numberOfDevices;
        private int _numberOfFolders;
        private int _userId;

        private Int32 _timeStamp;
        private string _tokenSecret;

        private ObservableCollection<VCCardListItem> _cloudsCollection;
        private ObservableCollection<VCListItem> _cloudFoldersCollection;
        private ObservableCollection<VCListItem> _cloudChooseCollection;
        private ObservableCollection<VCListItem> _deviceFoldersCollection;
        private ObservableCollection<VCListItem> _devicesCollection;
        private ObservableCollection<VCListItem> _folderDevicesCollection;
        private ObservableCollection<VCListItem> _foldersCollection;

        private ObservableCollection<CardListItem> _mainPageCards;

        private string _username;
        private string _userToken;
        private string _password;

        private string _newCloudName;
        private string _deviceName;
        private string _pairCode;
        private string _dropboxCode;

        private VCCardListItem _selectedCloudProvider;
        private Models.Device _selectedDevice;
        private SFolder _selectedFolder;
        private RCloud _selectedCloud;

        private AppGetDevicesResponseDTO _devicesResponseDto;
        private AppGetCloudsResponseDTO _cloudsResponseDto;
        private AppChangePasswordRequestDTO _changePasswordModel;
        private AppRegisterRequestDTO _registerUser;
        private AppGetDeviceFoldersResponseDTO _deviceFoldersDto;
        private AppCreateCloudRequestDTO _createCloudRequestDto;
        private AppGetCloudFoldersResponseDTO _cloudFoldersResponseDto;

        private WebViewSource _webViewSourceCloud;


        private string _endpoint = "https://idpf.azurewebsites.net";

        #endregion

        #region properties

        public bool IsDropboxConnection
        {
            get => _isDropboxConnection;

            set => SetProperty(ref _isDropboxConnection, value);
        }

        public CloudProviderType NewCloudType
        {
            get => _newCloudType;
            set
            {
                SetProperty(ref _newCloudType, value);
                CloudConnectCommand.ChangeCanExecute();
            }
        }

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

        public MainViewModel()
        {
            InitProperties();
            InitCommands();
            InitMainPageCards();
            InitDto();
            InitCollections();
        }

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

        private void InitCollections()
        {
            CloudsCollection = new ObservableCollection<VCCardListItem>();
            CloudChooseCollection = new ObservableCollection<VCListItem>();
            DevicesCollection = new ObservableCollection<VCListItem>();
            FoldersCollection = new ObservableCollection<VCListItem>();
            CloudFoldersCollection = new ObservableCollection<VCListItem>();
        }

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

        private void InitProperties()
        {
            PairCode = "";
            DeviceName = "";
            DropboxCode = "";
        }

        #endregion

        #region command execution

        private bool CanExecuteCloudConnectCommand()
        {
            return NewCloudName?.Length > 0 && NewCloudType != CloudProviderType.None;
        }

        private bool CanExecuteDevicePairCommand()
        {
            return PairCode.Length == 7 && DeviceName.Length > 0;
        }

        private void ExecuteChangePageCommand(object pageType)
        {
            Page nextPage = (Page)Activator.CreateInstance((Type)pageType);
            nextPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(nextPage);
        }

        private async void ExecuteChangePasswordCommand()
        {
            if (await ChangePassword())
            {
                ChangePasswordModel.OldPassword = "";
                ChangePasswordModel.Password = "";
                ChangePasswordModel.Password2 = "";
            }
        }

        private async void ExecuteCloudConnectCommand()
        {
            await GetConnectionToCloudProvider();
        }

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

        private void ExecuteGoBackPageCommand()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ExecuteGoToChooseCloudPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new ChooseCloudPage(this));
        }

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
        private void ExecuteGoToCloudsListPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new CloudsListPage(this));
        }

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

        private void ExecuteGoToDevicesListPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DevicesListPage(this));
        }

        private void ExecuteGoToFoldersListPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FoldersListPage(this));
        }

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

        private void ExecuteGoToProfilePageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new ProfilPage(this));
        }

        private void ExecuteGoToSignUpPageCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new SignUpPage(this));
        }

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

        private async void ExecuteRefreshCommand()
        {
            GetDevices();
            GetClouds();
        }

        private async void ExecuteSignUpCommand()
        {
            await AccountRegister();
        }

        #endregion

        #region requests

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
            }
            catch (Exception e)
            {
                OnErrorOccurred(this, e.Message);
            }

            return false;
        }

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
        /// Calls DependencyService to check whether the device is connected to the Internet.
        /// </summary>
        /// <returns>True if the device is connected to the Internet, false otherwise.</returns>
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

        private void AssambleFoldersDtoToCollection()
        {
            FoldersCollection =
                DtoTOCollectionAssamblers.AssambleFoldersDtoToCollection(DeviceFoldersDto, FolderUnassignCommand);

            NumberOfFolders = FoldersCollection.Count;
        }

        private void AssambleCloudsDtoToCollection()
        {
            CloudsCollection =
                DtoTOCollectionAssamblers.AssambleCloudsDtoToCollectionCard(CloudsResponseDto, CloudDisconnectCommand);
            CloudChooseCollection =
                DtoTOCollectionAssamblers.AssambleCloudsDtoToCollectionList(CloudsResponseDto, GoToCloudFoldersPage);
            NumberOfClouds = CloudsCollection.Count;
            MainPageCards[1].CardSubtext = "Connected clouds: " + NumberOfClouds;
        }

        private void AssambleDevicesDtoToCollection()
        {
            DevicesCollection =
                DtoTOCollectionAssamblers.AssambleDevicesDtoToCollection(DevicesResponseDto, GoToDevicePageCommand);
            NumberOfDevices = DevicesCollection.Count;
            MainPageCards[0].CardSubtext = "Paired devices: " + NumberOfDevices;
        }

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