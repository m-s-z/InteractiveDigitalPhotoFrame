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
using IDPFLibrary.DTO.AAA.Account.Request;
using IDPFLibrary.DTO.AAA.Account.Response;
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
        /// Backing field for TestNumberTwo property.
        /// </summary>
        private Account _userAccount;

        private CloudTypeEnum _newCloudType;

        private int _numberOfClouds;
        private int _numberOfDevices;
        private int _numberOfFolders;
        private int _userId;

        private ObservableCollection<VCCardListItem> _cloudsCollection;
        private ObservableCollection<VCListItem> _cloudChooseCollection;
        private ObservableCollection<VCListItem> _deviceFoldersCollection;
        private ObservableCollection<VCListItem> _devicesCollection;
        private ObservableCollection<VCListItem> _folderDevicesCollection;
        private ObservableCollection<VCListItem> _foldersCollection;

        private ObservableCollection<CardListItem> _mainPageCards;

        private string _username;
        private string _password;

        private string _cloudEmail;
        private string _deviceName;
        private string _pairCode;

        private VCCardListItem _selectedCloudProvider;
        private Models.Device _selectedDevice;
        private VCListItem _selectedFolder;

        private AppGetDevicesResponseDTO _devicesResponseDto;
        private AppGetCloudsResponseDTO _cloudsResponseDto;
        private AppChangePasswordRequestDTO _changePasswordModel;
        private AppRegisterRequestDTO _registerUser;

        private HttpClient _httpClient;

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

        public CloudTypeEnum NewCloudType
        {
            get => _newCloudType;
            set
            {
                SetProperty(ref _newCloudType, value);
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

        public Command GoToDevicePageCommand { get; set; }

        public Command GoToDevicesListPageCommand { get; set; }

        public Command GoToFoldersListPageCommand { get; set; }

        public Command GoToMainPageCommand { get; set; }

        public Command GoToProfilePageCommand { get; set; }

        public Command GoToSignUpPageCommand { get; set; }

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

        public string CloudEmail
        {
            get => _cloudEmail;
            set
            {
                SetProperty(ref _cloudEmail, value);
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

        public VCListItem SelectedFolder
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
            _httpClient = new HttpClient();
        }

        private void InitCommands()
        {
            ChangePageCommand = new Command(ExecuteChangePageCommand);
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
        }

        private void InitMainPageCards()
        {
            MainPageCards = new ObservableCollection<CardListItem>();
            MainPageCards.Add(new CardListItem(CardTypeEnum.HighOneAction, "DEVICES", GoToDevicesListPageCommand,
                "MANAGE", "tablet_card_96px.png", "Paired devices: " + NumberOfDevices));
            MainPageCards.Add(new CardListItem(CardTypeEnum.HighOneAction, "CLOUDS", GoToCloudsListPageCommand,
                "MANAGE", "cloud_card_96px.png", "Connected clouds: " + NumberOfClouds));
            MainPageCards.Add(new CardListItem(CardTypeEnum.HighOneAction, "PROFILE", GoToProfilePageCommand,
                "MANAGE", "user_card_96px.png", "Your login"));
        }

        private void InitCollections()
        {
            //CloudChooseCollection = new ObservableCollection<VCListItem>();
            CloudsCollection = new ObservableCollection<VCCardListItem>();
            DevicesCollection = new ObservableCollection<VCListItem>();
            //FoldersCollection = new ObservableCollection<VCListItem>();

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
        private void ExecuteCloudDisconnectCommand(object item)
        {
            if (item is VCCardListItem selectedCloud)
            {
                SelectedCloudProvider = selectedCloud;

                UpdateAllInformation();
                SelectedCloudProvider = null;
            } 
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
            //SelectedDevice.


            ExecuteGoBackPageCommand();
            //UserAccount.DevicesCollection.Remove(SelectedDevice.Device);
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

        private void ExecuteGoToDevicePageCommand(object item)
        {
            if (item is VCListItem selectedDevice)
            {
                SelectedDevice = new Models.Device(selectedDevice.Device.Name, selectedDevice.Device.DeviceId);
                var newPage = new DevicePage();
                newPage.BindingContext = this;
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
                Application.Current.MainPage = new NavigationPage(new MainAppPage(this));
            }
        }

        private void ExecuteGoToProfilePageCommand()
        {
            GetClouds();
            //Application.Current.MainPage.Navigation.PushAsync(new ProfilPage(this));
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

        private void ExecuteFolderUnassignCommand(object param)
        {
            //UserAccount.DevicesCollection.FirstOrDefault(d => d == SelectedDevice.Device)?.FoldersCollection.Remove(((VCListItem)param).Folder);
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

        private async Task<bool> AccountRegister()
        {
            try
            {
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

        private async void ChangePassword()
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
                            AccountId = _userId
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
        }

        private async void DisconnectCloud()
        {

        }

        private async void GetClouds()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    using (var client = new HttpClient())
                    {


                        string url = _endpoint + "/Cloud/AppGetClouds";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Get
                        };

                        var response = await _httpClient.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        var result = (JsonConvert.DeserializeObject<AppGetCloudsResponseDTO>(contents));
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
                            AccountId = _userId
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
                            Password = Password,
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = _endpoint + "/Login/AppLogin";
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Post,
                            Content = new StringContent(json,
                                Encoding.UTF8,
                                "application/json")
                        };

                        var response = await _httpClient.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        var result = (JsonConvert.DeserializeObject<AppLoginResponseDTO>(contents));
                        if (result.IsSuccess)
                        {
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

        private async void PairDevice()
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
                            PairCode = PairCode
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
                        await Application.Current.MainPage.DisplayAlert("Pair device", contents, "OK");

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


        private async void UnpairDevice()
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    using (var client = new HttpClient())
                    {
                        AppUnpairDeviceRequestDTO requestDto = new AppUnpairDeviceRequestDTO
                        {
                            AccountId = _userId,
                            DeviceId = SelectedDevice.DeviceId
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

        private async void UnassignFolderFromDevice(SFolder folderToUnassign)
        {
            try
            {
                if (CheckIfNetworkConnection())
                {
                    using (var client = new HttpClient())
                    {
                        AppDeleteFolderRequestDTO requestDto = new AppDeleteFolderRequestDTO
                        {
                            AccountId = _userId,
                            FolderId = folderToUnassign.FolderId
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = "https://idpf.azurewebsites.net/Folder/AppDeleteFolder";
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

        private void AssambleCloudsDtoToCollection()
        {
            CloudsCollection.Clear();

            foreach (var cloud in CloudsResponseDto.clouds)
            {
                CloudsCollection.Add(new VCCardListItem(CardTypeEnum.ShortOneAction, cloud, CloudDisconnectCommand));
            }
        }

        private void AssambleDevicesDtoToCollection()
        {
            DevicesCollection.Clear();

            foreach (var device in DevicesResponseDto.Devices)
            {
                DevicesCollection.Add(new VCListItem(device, GoToDevicePageCommand));
            }
        }

        #endregion
    }
}