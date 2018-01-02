using IDPFLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DPF.Models;
using DPF.Utils.Controls;
using DPF.Views;
using IDPFLibrary.DTO;
using IDPFLibrary.Utils;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DPF.ViewModels
{
    public class MainDPFViewModel : ViewModelBase
    {
        private const string EMPTY_PHOTOSET_DEFAULT_PATH = "photos_96px.png";

        private bool _isNetworkConnected;

        private int _deviceId;
        private string _deviceToken;
        private GetDeviceAccountsDTO _getDeviceAccounts;

        private GetAllFlickrPhotosURLResponseDTO _currentPhotoset;

        private LocalStorageModel _localStorageModel;
        private NetworkConnectionModel _networkConnectionModel;

        private List<String> _listOfImageNames;
        private int _photoCounter;
        private string _code = "Generating...";
        private string _photoPath;
        private bool _isActive;
        private bool _isCodeVisible;
        private bool _isSlideshow;
        private int _slideshowCounter;
        private int _keepActiveCounter;
        private ObservableCollection<VCListItem> _connectedAccountsCollection;
        private Color _refreshingColor;

        private ObservableCollection<String> _listOfImageUrl;

        public bool IsNetworkConnected
        {
            get => _isNetworkConnected;
            set => SetProperty(ref _isNetworkConnected, value);
        }

        public Color RefreshingColor
        {
            get => _refreshingColor;
            set => SetProperty(ref _refreshingColor, value);
        }

        public GetAllFlickrPhotosURLResponseDTO CurrentPhotoset
        {
            get => _currentPhotoset;
            set => SetProperty(ref _currentPhotoset, value);
        }

        public GetDeviceAccountsDTO GetDeviceAccounts
        {
            get => _getDeviceAccounts;
            set
            {
                SetProperty(ref _getDeviceAccounts, value);
                AssamblerAccountsList();
            }
        }

        public string Code
        {
            get => _code;
            set => SetProperty(ref _code, value);
        }

        public string PhotoPath
        {
            get => _photoPath;
            set => SetProperty(ref _photoPath, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        public bool IsCodeVisible
        {
            get => _isCodeVisible;
            set => SetProperty(ref _isCodeVisible, value);
        }

        public bool IsSlideshow
        {
            get => _isSlideshow;
            set => SetProperty(ref _isSlideshow, value);
        }

        public ObservableCollection<VCListItem> ConnectedAccountsCollection
        {
            get => _connectedAccountsCollection;
            set => SetProperty(ref _connectedAccountsCollection, value);
        }

        public List<String> ListOfImageNames
        {
            get;
            set;
        }

        public ObservableCollection<String> ListOfImageUrl
        {
            get => _listOfImageUrl;
            set => SetProperty(ref _listOfImageUrl, value);
        }

        public Command GoToAccountsListPageCommand
        {
            get;
            set;
        }

        public Command NextPhotoCommand
        {
            get;
            set;
        }

        public Command PreviousPhotoCommand
        {
            get;
            set;
        }

        public Command ShowCodeCommand
        {
            get;
            set;
        }

        public Command TapToActiveCommand
        {
            get;
            set;
        }
        public Command ControlSlideshowCommand
        {
            get;
            set;
        }

        public Command RefreshCommand
        {
            get;
            set;
        }

        public Command DisconnectAccountCommand
        {
            get;
            set;
        }



        public MainDPFViewModel()
        {
            InitCommands();
            InitNetworkConnectionModel();
            InitLocalStorageModel();

            GetUpdates();



            //ListOfImageNames = new List<string>()
            //{
            //    "dpf_mock_background.png",
            //    "mock_2.jpg",
            //    "mock_3.jpg"
            //};
            //PhotoPath = ListOfImageNames[0];
            _photoCounter = 0;
            RefreshingColor = new Color(0, 255, 0);
        }

        private void InitCommands()
        {
            GoToAccountsListPageCommand = new Command(ExecuteGoToAccountsListPageCommand);
            ShowCodeCommand = new Command(ExecuteShowCodeCommand);
            TapToActiveCommand = new Command(ExecuteTapToActiveCommand);
            PreviousPhotoCommand = new Command(ExecutePreviousPhotoCommand);
            NextPhotoCommand = new Command(ExecuteNextPhotoCommand);
            ControlSlideshowCommand = new Command(ExecuteControlSlideshowCommand);
            RefreshCommand = new Command(ExecuteRefreshCommand);
            DisconnectAccountCommand = new Command(ExecuteDisconnectAccountCommand);
        }

        private void InitNetworkConnectionModel()
        {
            _networkConnectionModel = new NetworkConnectionModel();
            _networkConnectionModel.ErrorOccured += OnErrorOccurred;
        }

        private void InitLocalStorageModel()
        {
            _localStorageModel = new LocalStorageModel();
            _localStorageModel.ErrorOccured += OnErrorOccurred;
            _localStorageModel.SynchronizationCompleted += OnSynchronizationCompleted;
            _localStorageModel.CreateImagesFolder();

            string photosetJson = _localStorageModel.GetPhotoset();
            if (photosetJson != null)
            {
                CurrentPhotoset = JsonConvert.DeserializeObject<GetAllFlickrPhotosURLResponseDTO>(photosetJson);
                if (CurrentPhotoset.Urls.Count != 0)
                {
                    PhotoPath = _localStorageModel.GetImageToShow(CurrentPhotoset.Urls[0]);
                }
                else
                {
                    PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
                }
            }
            else
            {
                CurrentPhotoset = new GetAllFlickrPhotosURLResponseDTO(new List<Urls>());
                PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
            }

            string json = _localStorageModel.GetDeviceToken();
            if (json != null)
            {
                CreateNewDeviceDTO newDeviceDto = (JsonConvert.DeserializeObject<CreateNewDeviceDTO>(json));
                _deviceId = newDeviceDto.DeviceId;
                _deviceToken = newDeviceDto.DeviceToken;
            }

            json = _localStorageModel.GetConnectedAccounts();
            if (json != null)
            {
                GetDeviceAccounts = (JsonConvert.DeserializeObject<GetDeviceAccountsDTO>(json));
            }
        }

        private async Task GetUpdates()
        {
            await GetConnectedAccountsRequest();
        }

        private async void ExecuteRefreshCommand()
        {
            RefreshingColor = new Color(255, 0, 0);
            await GetUpdates();
            await GetAllPhotosUrl();
            RefreshingColor = new Color(0, 255, 0);
        }

        private void OnErrorOccurred(object sedner, string errorMessage)
        {
            
        }

        private void OnSynchronizationCompleted(object sender, GetAllFlickrPhotosURLResponseDTO newPhotoset)
        {
            CurrentPhotoset = newPhotoset;
            var json = JsonConvert.SerializeObject(CurrentPhotoset);
            _localStorageModel.SavePhotoset(json);
            _localStorageModel.SaveImage();
        }

        private async Task GetAllPhotosUrl()
        {
            if (GetDeviceAccounts.Accounts.Count == 0)
            {
                return;
            }

            if (CheckIfNetworkConnection())
            {
                using (var client = new HttpClient())
                {
                    GetAllFlickrPhotosURLRequestDTO requestDto = new GetAllFlickrPhotosURLRequestDTO
                    {
                        DeviceId = _deviceId,
                        DeviceToken = _deviceToken,
                        AccountIds = GetDeviceAccounts.Accounts.Select(p => p.AccountId).ToList<int>()
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                    string url = "https://idpf.azurewebsites.net/Device/GetAllFlickrPhotosUrl";
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
                    GetAllFlickrPhotosURLResponseDTO getAllFlickrPhotosUrl = (JsonConvert.DeserializeObject<GetAllFlickrPhotosURLResponseDTO>(contents));
                    if (getAllFlickrPhotosUrl != null)
                    {
                        _localStorageModel.SynchronizingImages(getAllFlickrPhotosUrl, CurrentPhotoset);


                        //ListOfImageNames.Clear();
                        //foreach (var photoUrl in getAllFlickrPhotosUrl.Urls)
                        //{
                        //    ListOfImageNames.Add(photoUrl.Link);
                        //}
                        //PhotoPath = ListOfImageNames[0];
                    }

                    
                }
            }
        }

        private async Task GetConnectedAccountsRequest()
        {
            if (CheckIfNetworkConnection())
            {
                using (var client = new HttpClient())
                {
                    CreateNewDeviceDTO requestDto = new CreateNewDeviceDTO
                    {
                        DeviceId = _deviceId,
                        DeviceToken = _deviceToken
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                    string url = "https://idpf.azurewebsites.net/Device/GetDeviceAccounts";
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
                    GetDeviceAccounts = (JsonConvert.DeserializeObject<GetDeviceAccountsDTO>(contents));
                    _localStorageModel.SaveConnectedAccounts(contents);
                }
            }
        }

        private void ExecuteGoToAccountsListPageCommand()
        {
            CheckIfNetworkConnection();
            var page = new ConnectedAccountsPage();
            page.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(page);
            IsActive = false;
        }

        private void ExecuteNextPhotoCommand()
        {
            _keepActiveCounter = 0;
            ChangeNextPhoto();
        }

        private void ChangeNextPhoto()
        {

            if (CurrentPhotoset.Urls.Count == 0)
            {
                PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
                return;
            }

            _slideshowCounter = 0;
            _photoCounter++;

            if (_photoCounter > CurrentPhotoset.Urls.Count - 1)
            {
                _photoCounter = 0;
            }

            PhotoPath = _localStorageModel.GetImageToShow(CurrentPhotoset.Urls[_photoCounter]);
        }

        private void ExecutePreviousPhotoCommand()
        {
            _keepActiveCounter = 0;

            if (CurrentPhotoset.Urls.Count == 0)
            {
                PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
                return;
            }

            _slideshowCounter = 0;
            _photoCounter--;
            if (_photoCounter < 0)
            {
                _photoCounter = ListOfImageNames.Count - 1;
            }

            PhotoPath = _localStorageModel.GetImageToShow(CurrentPhotoset.Urls[_photoCounter]);
        }

        private void ExecuteTapToActiveCommand()
        {
            IsActive = !IsActive;
            _keepActiveCounter = 0;
            if (IsActive)
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    if (IsActive)
                    {
                        if (_keepActiveCounter >= 100)
                        {
                            if (!IsCodeVisible)
                            {
                                IsActive = !IsActive;
                            }
                        }
                        _keepActiveCounter++;
                    }

                    return IsActive;
                });
            }

        }

        private async void ExecuteShowCodeCommand()
        {
            _keepActiveCounter = 0;
            IsCodeVisible = !IsCodeVisible;

            if (IsCodeVisible)
            {
                if (CheckIfNetworkConnection())
                {
                    using (var client = new HttpClient())
                    {
                        CreateNewDeviceDTO requestDto = new CreateNewDeviceDTO
                        {
                            DeviceId = _deviceId,
                            DeviceToken = _deviceToken
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                        string url = "https://idpf.azurewebsites.net/Device/GeneratePairCode";
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
                        Code = contents;
                    }
                }
                else
                {
                    Code = "Offline mode";
                }
            }
            else
            {
                Code = "Generating...";
            }
        }

        private void ExecuteControlSlideshowCommand()
        {
            _keepActiveCounter = 0;

            IsSlideshow = !IsSlideshow;
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                if (IsSlideshow)
                {
                    _slideshowCounter++;

                    if (_slideshowCounter >= 40)
                    {
                        ChangeNextPhoto();
                    }
                }
                else
                {
                    _slideshowCounter = 0;
                }

                return IsSlideshow;
            });
        }

        private async void ExecuteDisconnectAccountCommand(object param)
        {
            if (param is VCListItem item)
            {
                DisconnectAccount(item.AccountId);
            }
        }

        private async void DisconnectAccount(int accountId)
        {
            if (CheckIfNetworkConnection())
            {
                using (var client = new HttpClient())
                {
                    UnpairDeviceRequestDTO requestDto = new UnpairDeviceRequestDTO
                    {
                        DeviceId = _deviceId,
                        DeviceToken = _deviceToken,
                        AccountId = accountId

                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                    string url = "https://idpf.azurewebsites.net/Device/UnpairDevice";
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

                await GetUpdates();
            }
        }

        private void AssamblerAccountsList()
        {
            ConnectedAccountsCollection = new ObservableCollection<VCListItem>();
            foreach (var account in GetDeviceAccounts.Accounts)
            {
                ConnectedAccountsCollection.Add(new VCListItem(account.Name, account.AccountId, DisconnectAccountCommand));
            }
        }

        private bool CheckIfNetworkConnection()
        {
            IsNetworkConnected = _networkConnectionModel.CheckIfNetworkConnected();
            return IsNetworkConnected;
        }

    }
}
