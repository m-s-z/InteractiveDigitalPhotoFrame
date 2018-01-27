using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AAA.Models;
using AAA.Utils;
using AAA.Utils.CloudProvider;
using AAA.Utils.Controls;
using AAA.Views;
using IDPFLibrary.DTO.AAA.Login.Request;
using Xamarin.Forms;
using System.Net.Http;
using System.Text;
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
using Xamarin.Auth;
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

        private VCCardListItem _selectedCloudProvider;
        private Models.Device _selectedDevice;
        private SFolder _selectedFolder;

        private AppGetDevicesResponseDTO _devicesResponseDto;
        private AppGetCloudsResponseDTO _cloudsResponseDto;
        private AppChangePasswordRequestDTO _changePasswordModel;
        private AppRegisterRequestDTO _registerUser;
        private AppGetDeviceFoldersResponseDTO _deviceFoldersDto;
        private AppCreateCloudRequestDTO _createCloudRequestDto;


        private string _endpoint = "https://idpf.azurewebsites.net";

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

        public CloudProviderType NewCloudType
        {
            get => _newCloudType;
            set
            {
                SetProperty(ref _newCloudType, value);
            }
        }

        public Command ChangePageCommand { get; set; }
        public Command ChangePasswordCommand { get; set; }
        public Command CloudConnectCommand { get; set; }
        public Command CloudDisconnectCommand { get; set; }
        public Command CloudModifyCommand { get; set; }
        public Command DevicePairCommand { get; set; }
        public Command DeviceUnpairCommand { get; set; }
        public Command GoBackPageCommand { get; set; }
        public Command GoToCloudsListPageCommand { get; set; }

        public Command GoToDevicePageCommand { get; set; }

        public Command GoToDevicesListPageCommand { get; set; }

        public Command GoToFoldersListPageCommand { get; set; }

        public Command GoToMainPageCommand { get; set; }

        public Command GoToProfilePageCommand { get; set; }

        public Command GoToSignUpPageCommand { get; set; }

        public Command DeviceUnassignCommand { get; set; }
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
            set
            {
                SetProperty(ref _username, value);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }

        public string NewCloudName
        {
            get => _newCloudName;
            set
            {
                SetProperty(ref _newCloudName, value);
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

        public Models.Device SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                SetProperty(ref _selectedDevice, value);
                InitDeviceFoldersCollection();
            }
        }

        public SFolder SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                SetProperty(ref _selectedFolder, value);
                InitFolderDevicesCollection();
            }
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
            set
            {
                SetProperty(ref _changePasswordModel, value);
            }
        }

        public AppRegisterRequestDTO RegisterUser
        {
            get => _registerUser;
            set
            {
                SetProperty(ref _registerUser, value);
            }
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
            set
            {
                SetProperty(ref _createCloudRequestDto, value);
            }
        }

        #endregion

        #region methods

        public MainViewModel()
        {
            PairCode = "";
            DeviceName = "";
            RegisterUser = new AppRegisterRequestDTO();
            ChangePasswordModel = new AppChangePasswordRequestDTO();
            InitCommands();
            InitUser();
            InitCollections();
            InitMainPageCards();
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

        private void InitCommands()
        {
            ChangePageCommand = new Command(ExecuteChangePageCommand);
            ChangePasswordCommand = new Command(ExecuteChangePasswordCommand);
            CloudConnectCommand = new Command(ExecuteCloudConnectCommand);
            CloudDisconnectCommand = new Command(ExecuteCloudDisconnectCommand);
            CloudModifyCommand = new Command(ExecuteCloudModifyCommand);
            DeviceUnpairCommand = new Command(ExecuteDeviceUnpairCommand);
            DevicePairCommand = new Command(ExecuteDevicePairCommand, CanExecuteDevicePairCommand);
            GoBackPageCommand = new Command(ExecuteGoBackPageCommand);
            GoToDevicePageCommand = new Command(ExecuteGoToDevicePageCommand);
            GoToDevicesListPageCommand = new Command(ExecuteGoToDevicesListPageCommand);
            GoToFoldersListPageCommand = new Command(ExecuteGoToFoldersListPageCommand);
            GoToCloudsListPageCommand = new Command(ExecuteGoToCloudsListPageCommand);
            GoToMainPageCommand = new Command(ExecuteGoToMainPageCommand);
            GoToProfilePageCommand = new Command(ExecuteGoToProfilePageCommand);
            GoToSignUpPageCommand = new Command(ExecuteGoToSignUpPageCommand);
            DeviceUnassignCommand = new Command(ExecuteDeviceUnassignCommand);
            FolderUnassignCommand = new Command(ExecuteFolderUnassignCommand);
            RefreshCommand = new Command(ExecuteRefreshCommand);
            SignUpCommand = new Command(ExecuteSignUpCommand);
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

        private void InitCollections()
        {
            //CloudChooseCollection = new ObservableCollection<VCListItem>();
            CloudsCollection = new ObservableCollection<VCCardListItem>();
            DevicesCollection = new ObservableCollection<VCListItem>();
            FoldersCollection = new ObservableCollection<VCListItem>();

            //foreach (var device in UserAccount.DevicesCollection)
            //{
            //    DevicesCollection.Add(new VCListItem(device, GoToDevicePageCommand));

            //    foreach (var folder in device.FoldersCollection)
            //    {
            //        if (FoldersCollection.FirstOrDefault(f => f.Folder == folder) == null)
            //        {
            //            FoldersCollection.Add(new VCListItem(folder, ChangePageCommand));
            //        }
            //    }

            //}

            //foreach (var cloud in UserAccount.CloudsCollection)
            //{
            //    CloudsCollection.Add(new VCCardListItem(CardTypeEnum.ShortOneAction, cloud, CloudDisconnectCommand));
            //    CloudChooseCollection.Add(new VCListItem(cloud, null));
            //}

            //NumberOfClouds = UserAccount.CountCloudProviders();
            //NumberOfDevices = UserAccount.CountDevices();
            //NumberOfFolders = UserAccount.CountAllFolders();
        }

        private void InitDeviceFoldersCollection()
        {
            //if (SelectedDevice == null)
            //{
            //    return;;
            //}

            //DeviceFoldersCollection = new ObservableCollection<VCListItem>();

            //foreach (var folder in SelectedDevice.Device.FoldersCollection)
            //{
            //    DeviceFoldersCollection.Add(new VCListItem(folder, null, FolderUnassignCommand));
            //}
        }

        private void InitFolderDevicesCollection()
        {
            //if (SelectedFolder == null)
            //{
            //    return; ;
            //}

            //FolderDevicesCollection = new ObservableCollection<VCListItem>();

            //foreach (var device in UserAccount.DevicesCollection)
            //{
            //    foreach (var folder in device.FoldersCollection)
            //    {
            //        if (folder == SelectedFolder.Folder)
            //        {
            //            FolderDevicesCollection.Add(new VCListItem(device, null, DeviceUnassignCommand));
            //        }
            //    }

            //}
        }

        private void InitUser()
        {
            UserAccount = new Account(1);
        }

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
            Page nextPage = (Page) Activator.CreateInstance((Type) pageType);
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

        private void ExecuteCloudConnectCommand()
        {
            GetConnectionToCloudProvider();
            //UserAccount.CloudsCollection.Add(new CloudProvider(NewCloudType, CloudEmail));
            //UpdateAllInformation();
            //ExecuteGoBackPageCommand();
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

        private void ExecuteCloudModifyCommand()
        {
            
        }

        private async void ExecuteDevicePairCommand()
        {
            //UserAccount.DevicesCollection.Add(new Models.Device(DeviceName, 1));
            //UpdateAllInformation();
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
            //SelectedDevice.
            if (await UnpairDevice())
            {
                ExecuteGoBackPageCommand();
                GetDevices();
                GetClouds();
                SelectedDevice = null;
            }

            //ExecuteGoBackPageCommand();
            ////UserAccount.DevicesCollection.Remove(SelectedDevice.Device);
            //SelectedDevice = null;
            //DeviceFoldersCollection = new ObservableCollection<VCListItem>();
            //UpdateAllInformation();
        }

        private void ExecuteGoBackPageCommand()
        {
            Application.Current.MainPage.Navigation.PopAsync();
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
            //GetDevices();
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

        private void ExecuteDeviceUnassignCommand(object param)
        {
            
            UpdateAllInformation();
            Application.Current.MainPage.DisplayAlert("Unassignment", "The device has been successfully  unassiged", "OK");
        }

        private async void ExecuteFolderUnassignCommand(object param)
        {
            if (param is VCListItem selectedFolder)
            {
                SelectedFolder = selectedFolder.Folder;
                var result = await UnassignFolderFromDevice(SelectedFolder);
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

        private void UpdateAllInformation()
        {
            UserAccount.UpdateInformation();
            InitCollections();
            InitMainPageCards();
            InitDeviceFoldersCollection();
            InitFolderDevicesCollection();
        }

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
                    using (var client = new HttpClient())
                    {
                        AppRegisterRequestDTO requestDto = new AppRegisterRequestDTO
                        {
                            Password = RegisterUser.Password,
                            Password2 = RegisterUser.Password2,
                            Login = RegisterUser.Login
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = "https://idpf.azurewebsites.net/Login/AppRegister";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Post,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        var result = (JsonConvert.DeserializeObject<AppRegisterResponseDTO>(contents));
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

        private async Task<bool> ChangePassword()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    using (var client = new HttpClient())
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

                        string url = "https://idpf.azurewebsites.net/Account/AppChangePassword";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Post,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        var result = (JsonConvert.DeserializeObject<AppChangePasswordResponseDTO>(contents));
                        await Application.Current.MainPage.DisplayAlert("Change password result", result.Message, "OK");
                        
                    }
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
                    using (var client = new HttpClient())
                    {
                        AppCreateCloudRequestDTO requestDto = CreateCloudRequestDto;

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = _endpoint + "/Cloud/AppCreateCloud";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Put,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();

                        var result = (JsonConvert.DeserializeObject<AppCreateCloudResponseDTO>(contents));

                        await Application.Current.MainPage.DisplayAlert("Connection status", result.Message, "OK");

                        if (result.Auth == AuthorizationResponse.TokenExpired || result.Auth == AuthorizationResponse.InvalidToken)
                        {
                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            return false;
                        }
                        GetClouds();
                        return true;
                    }
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
                    using (var client = new HttpClient())
                    {
                        AppDeleteCloudRequestDTO requestDto = new AppDeleteCloudRequestDTO
                        {
                            UserId = _userId,
                            Token = _userToken,
                            CloudId = SelectedCloudProvider.CloudProvider.CloudId
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = _endpoint + "/Cloud/AppDeleteCloud";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Post,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();

                        var result = (JsonConvert.DeserializeObject<AppDeleteCloudResponseDTO>(contents));

                        if (result.Auth == AuthorizationResponse.TokenExpired || result.Auth == AuthorizationResponse.InvalidToken)
                        {
                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            return false;
                        }
                        
                        return true;
                    }
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
                    using (var client = new HttpClient())
                    {
                        AppGetCloudsRequestDTO requestDto = new AppGetCloudsRequestDTO
                        {
                            UserId = _userId,
                            Token = _userToken
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = _endpoint + "/Cloud/AppGetClouds";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Post,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        
                        CloudsResponseDto = (JsonConvert.DeserializeObject<AppGetCloudsResponseDTO>(contents));

                        if (CloudsResponseDto.Auth == AuthorizationResponse.TokenExpired || CloudsResponseDto.Auth == AuthorizationResponse.InvalidToken)
                        {
                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            return;
                        }
                    }
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

        private async Task<bool> GetConnectionToCloudProvider()
        {
            switch (NewCloudType)
            {
                case CloudProviderType.Dropbox:
                    return await GetConnectionWithDropbox();
                case CloudProviderType.Flickr:
                    return await GetConnectionWithFlickr();
                default:
                    return false;
            }
        }

        private async Task<bool> GetConnectionWithDropbox()
        {
            return false;
        }

        private async Task<bool> GetConnectionWithFlickr()
        {
            _timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var requestSignature = FlickrAPI.GetRequestToSignature();
            string requestToken;

            string signature = DependencyService.Get<ICloudsConnectionsService>()
                .GetSignature(FlickrAPI.SharedSecret +"&", string.Format(requestSignature, _timeStamp));

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
                var token = GetRequestTokenFromText(contents);
                requestToken = token.Item1;
                _tokenSecret = token.Item2;

            }

            var authorizeUri = string.Format(FlickrAPI.AuthorizeTokenURL, requestToken);

            var webView = new WebView
            {
                Source = authorizeUri
            };

            webView.Navigated += WebViewOnNavigated;
            var tempPage = new WebPage()
            {
                Content = webView,
                Title = "Flickr",
            };
            await Application.Current.MainPage.Navigation.PushAsync(tempPage);

            return true;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs eventArgs)
        {
            var accessToken = GetAuthorizationTokenFromUrl(eventArgs.Url);

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
                    var token = GetAccessTokenFromUrl(contents);

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

        private Tuple<string, string> GetRequestTokenFromText(string text)
        {
            if (text.Contains("oauth_token") && text.Contains("oauth_callback_confirmed") && text.Contains("&oauth_token_secret"))
            {
                var at = text.Replace("oauth_callback_confirmed=true&oauth_token=", "");
                var oauth_token = at.Substring(0, at.IndexOf('&'));
                var oauth_token_secret = at.Substring(at.IndexOf('&') + 20, at.Length - at.IndexOf('&') - 20);
                return Tuple.Create(oauth_token, oauth_token_secret);
            }
            return null;
        }

        private Tuple<string, string> GetAuthorizationTokenFromUrl(string url)
        {
            if (url.Contains("oauth_token") && url.Contains("&oauth_verifier"))
            {
                var at = url.Replace("https://www.idpf.azurewebsites.net/?oauth_token=", "");
                var access = at.Substring(0, at.IndexOf('&'));
                var verifier = at.Substring(at.IndexOf('&') + 16, at.Length - at.IndexOf('&') - 16);
                return Tuple.Create(access, verifier);
            }
            return null;
        }

        private string[] GetAccessTokenFromUrl(string url)
        {
            if (url.Contains("oauth_token") && url.Contains("&oauth_token_secret"))
            {
                var s = url;
                var i = s.Split('&');
                i[1] = i[1].Replace("oauth_token=", "");
                i[2] = i[2].Replace("oauth_token_secret=", "");
                i[3] = i[3].Replace("user_nsid=", "");
                return i;
            }
            return null;
        }

        private async void GetDevices()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    using (var client = new HttpClient())
                    {
                        AppGetDevicesRequestDTO requestDto = new AppGetDevicesRequestDTO
                        {
                            AccountId = _userId,
                            Token = _userToken
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = "https://idpf.azurewebsites.net/Device/AppGetDevices";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Post,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        DevicesResponseDto = (JsonConvert.DeserializeObject<AppGetDevicesResponseDTO>(contents));
                        //var temp = new AppGetDevicesResponseDTO();
                        //temp.Devices = result;
                        //DevicesResponseDto = temp;
                        
                        if (DevicesResponseDto.Auth == AuthorizationResponse.TokenExpired || DevicesResponseDto.Auth == AuthorizationResponse.InvalidToken)
                        {
                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            return;
                        }
                       
                    }
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

        private async void GetSelectedDevice()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    using (var client = new HttpClient())
                    {
                        AppGetDeviceFoldersRequestDTO requestDto = new AppGetDeviceFoldersRequestDTO
                        {
                            Token = _userToken,
                            DeviceId = SelectedDevice.DeviceId,
                            AccountId = _userId
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = "https://idpf.azurewebsites.net/Folder/AppGetDeviceFolders";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Post,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        DeviceFoldersDto = (JsonConvert.DeserializeObject<AppGetDeviceFoldersResponseDTO>(contents));
                        if (DeviceFoldersDto.Auth == AuthorizationResponse.TokenExpired || DeviceFoldersDto.Auth == AuthorizationResponse.InvalidToken)
                        {
                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            return;
                        }
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
                    using (var client = new HttpClient())
                    {
                        AppLoginRequestDTO requestDto = new AppLoginRequestDTO
                        {
                            Login = Username,
                            Password = Password
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = "https://idpf.azurewebsites.net/Login/AppLogin";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Post,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        var result = (JsonConvert.DeserializeObject<AppLoginResponseDTO>(contents));
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
                    using (var client = new HttpClient())
                    {
                        AppPairDeviceRequestDTO requestDto = new AppPairDeviceRequestDTO
                        {
                            DeviceName = DeviceName,
                            PairCode = PairCode,
                            UserId = _userId,
                            Token = _userToken
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = "https://idpf.azurewebsites.net/Device/AppPairDevice";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Post,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        var result = (JsonConvert.DeserializeObject<AppPairDeviceResponseDTO>(contents));

                        if (result.Auth == AuthorizationResponse.TokenExpired || result.Auth == AuthorizationResponse.InvalidToken)
                        {
                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            return false;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Pair device", result.Message, "OK");
                            return true;
                        }
                    }
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
                    using (var client = new HttpClient())
                    {
                        AppUnpairDeviceRequestDTO requestDto = new AppUnpairDeviceRequestDTO
                        {
                            DeviceId = SelectedDevice.DeviceId,
                            AccountId = _userId,
                            Token = _userToken
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = "https://idpf.azurewebsites.net/Device/AppUnpairDevice";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Post,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        var result = (JsonConvert.DeserializeObject<AppUnpairDeviceResponseDTO>(contents));

                        if (result.Auth == AuthorizationResponse.TokenExpired || result.Auth == AuthorizationResponse.InvalidToken)
                        {
                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            return false;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Unpair device", "The device has been unpaired.", "OK");
                            return true;
                        }
                    }
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
                    using (var client = new HttpClient())
                    {
                        AppDeleteFolderRequestDTO requestDto = new AppDeleteFolderRequestDTO
                        {
                            UserId = _userId,
                            FolderId = folderToUnassign.FolderId,
                            Token = _userToken
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = "https://idpf.azurewebsites.net/Folder/AppDeleteFolder";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Delete,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        var result = (JsonConvert.DeserializeObject<AppDeleteFolderResponseDTO>(contents));

                        if (result.Auth == AuthorizationResponse.TokenExpired || result.Auth == AuthorizationResponse.InvalidToken)
                        {
                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            return false;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Folder unassignment", "The folder has been successfully  unassiged", "OK");
                            return true;
                        }
                    }
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
        /// Calls model to check whether the device is connected to the Internet.
        /// Upadtes IsNetworkConnected property.
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
            FoldersCollection.Clear();

            foreach (var folder in DeviceFoldersDto.Folders)
            {
                FoldersCollection.Add(new VCListItem(folder, null, FolderUnassignCommand));
            }

            NumberOfFolders = FoldersCollection.Count;
        }

        private void AssambleCloudsDtoToCollection()
        {
            CloudsCollection.Clear();

            foreach (var cloud in CloudsResponseDto.clouds)
            {
                CloudsCollection.Add(new VCCardListItem(CardTypeEnum.ShortOneAction, cloud, CloudDisconnectCommand));
            }

            NumberOfClouds = CloudsCollection.Count;
            MainPageCards[1].CardSubtext = "Connected clouds: " + NumberOfClouds;
        }

        private void AssambleDevicesDtoToCollection()
        {
            DevicesCollection.Clear();

            foreach (var device in DevicesResponseDto.Devices)
            {
                DevicesCollection.Add(new VCListItem(device, GoToDevicePageCommand));
            }

            NumberOfDevices = DevicesCollection.Count;
            MainPageCards[0].CardSubtext = "Paired devices: " + NumberOfDevices;
        }

        #endregion
    }
}