using IDPFLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DPF.Models;
using DPF.Utils.Controls;
using DPF.Views;
using Dropbox.Api;
using Dropbox.Api.FileProperties;
using IDPFLibrary.DTO;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DPF.ViewModels
{
    /// <summary>
    /// MainDPFViewModel class.
    /// </summary>
    public class MainDPFViewModel : ViewModelBase
    {


        /// <summary>
        /// Indicates the length of the interval between refreshing (in minutes).
        /// </summary>
        private const int REFRESH_TIMER = 5;

        /// <summary>
        /// Indicates the length of the interval between photo changing during slideshow (in tenths of a second).
        /// </summary>
        private const int SLIDESHOW_TIMER = 40;

        /// <summary>
        /// Contains path to default photo to display.
        /// </summary>
        private const string EMPTY_PHOTOSET_DEFAULT_PATH = "photos_96px.png";
        
        /// <summary>
        /// Flag indicating whether the device is in Active state or not.
        /// </summary>
        private bool _isActive;

        /// <summary>
        /// Flag indicating whether the paring code is visible or not.
        /// </summary>
        private bool _isCodeVisible;

        /// <summary>
        /// Flag indicating whether the device is connected to the network or not.
        /// </summary>
        private bool _isNetworkConnected;

        /// <summary>
        /// Flag indicating whether the slideshow is on or not.
        /// </summary>
        private bool _isSlideshow;

        /// <summary>
        /// This DPF device ID.
        /// </summary>
        private int _deviceId;

        /// <summary>
        /// Indicates how many photos where searched in storage while offline.
        /// </summary>
        private int _displayUnstoredPhotoCounter;

        /// <summary>
        /// Indicates how much time passed since the user's last activity (in tenths of a second).
        /// </summary>
        private int _keepActiveCounter;

        /// <summary>
        /// Number (in photoset) of currently displayed photo.
        /// </summary>
        private int _photoCounter;

        /// <summary>
        /// Indicates how much time passed since the last slide changing (in tenths of a second).
        /// </summary>
        private int _slideshowCounter;

        /// <summary>
        /// Indicates current pairing code.
        /// </summary>
        private string _code = "Generating...";

        /// <summary>
        /// This DPF device token.
        /// </summary>
        private string _deviceToken;

        /// <summary>
        /// Indicates path to the currently displayed photo.
        /// </summary>
        private string _photoPath;

        /// <summary>
        /// Contains list of photos synchronized with the DPF.
        /// </summary>
        private GetAllFlickrPhotosURLResponseDTO _currentPhotoset;

        /// <summary>
        /// Contains list of connected accounts.
        /// </summary>
        private GetDeviceAccountsDTO _getDeviceAccounts;

        /// <summary>
        /// Instance of LocalStorageModel class.
        /// </summary>
        private LocalStorageModel _localStorageModel;

        /// <summary>
        /// Instance of NetworkConnectionModel class.
        /// </summary>
        private NetworkConnectionModel _networkConnectionModel;

        private List<String> _listOfImageNames;
        
       
        
        
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

            Device.StartTimer(TimeSpan.FromMinutes(REFRESH_TIMER), () =>
            {
                RefreshingColor = new Color(255, 0, 0);
                ExecuteRefreshCommand();
                RefreshingColor = new Color(0, 255, 0);

                return true;
            });
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

            try
            {
                string photosetJson = _localStorageModel.GetPhotoset();
                if (photosetJson != null)
                {
                    CurrentPhotoset = JsonConvert.DeserializeObject<GetAllFlickrPhotosURLResponseDTO>(photosetJson);
                    if (CurrentPhotoset.Urls.Count != 0)
                    {
                        var path = _localStorageModel.GetImageToShow(CurrentPhotoset.Urls[0]);

                        if (path.Equals(""))
                        {
                            PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
                        }
                        else
                        {
                            PhotoPath = path;
                        }
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
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
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

        private void OnErrorOccurred(object sender, string errorMessage)
        {
            try
            {
                Application.Current.MainPage.DisplayAlert("Error occurred", errorMessage, "OK");
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

        }

        private void OnSynchronizationCompleted(object sender, GetAllFlickrPhotosURLResponseDTO newPhotoset)
        {
            //if (CurrentPhotoset.Urls.Count != 0)
            //{
            //    if (_photoCounter > CurrentPhotoset.Urls.Count - 1)
            //    {
            //        _photoCounter = 0;
            //        PhotoPath = CurrentPhotoset.Urls[_photoCounter].Link;
            //    }
            //    else
            //    {
            //        PhotoPath = CurrentPhotoset.Urls[_photoCounter].Link;
            //    }
            //}
            //else
            //{
            //    PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
            //}
        }

        private async Task GetAllPhotosUrl()
        {
            try
            {
                if (GetDeviceAccounts.Accounts.Count == 0)
                {
                    PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
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


                        var oldPhotoset = CurrentPhotoset;
                        CurrentPhotoset = getAllFlickrPhotosUrl;
                        var jsonPhotoset = JsonConvert.SerializeObject(CurrentPhotoset);
                        _localStorageModel.SavePhotoset(jsonPhotoset);

                        if (CurrentPhotoset.Urls.Count != 0)
                        {
                            if (_photoCounter > CurrentPhotoset.Urls.Count - 1)
                            {
                                _photoCounter = 0;
                                PhotoPath = CurrentPhotoset.Urls[_photoCounter].Link;
                            }
                            else
                            {
                                PhotoPath = CurrentPhotoset.Urls[_photoCounter].Link;
                            }
                        }
                        else
                        {
                            PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
                        }

                        if (getAllFlickrPhotosUrl != null)
                        {
                            _localStorageModel.SynchronizeImages(getAllFlickrPhotosUrl, oldPhotoset);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
            }
        }

        private async Task GetConnectedAccountsRequest()
        {
            try
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
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
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

        private async void ExecuteNextPhotoCommand()
        {
            _keepActiveCounter = 0;
            ChangeNextPhoto();
        }

        private async void ChangeNextPhoto()
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

            string path = _localStorageModel.GetImageToShow(CurrentPhotoset.Urls[_photoCounter]);
            if (path.Equals(""))
            {
                if (CheckIfNetworkConnection())
                {
                    _displayUnstoredPhotoCounter = 0;
                    PhotoPath = CurrentPhotoset.Urls[_photoCounter].Link;
                }
                else
                {
                    if (_displayUnstoredPhotoCounter <= CurrentPhotoset.Urls.Count)
                    {
                        _displayUnstoredPhotoCounter++;
                        ChangeNextPhoto();
                    }
                    else
                    {
                        _displayUnstoredPhotoCounter = 0;
                        PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
                    }
                }
            }
            else
            {
                _displayUnstoredPhotoCounter = 0;
                PhotoPath = path;
            }
        }



        private void ExecutePreviousPhotoCommand()
        {
            _keepActiveCounter = 0;
            ChangePreviousPhoto();
        }

        private void ChangePreviousPhoto()
        {
            if (CurrentPhotoset.Urls.Count == 0)
            {
                PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
                return;
            }

            _slideshowCounter = 0;
            _photoCounter--;

            if (_photoCounter < 0)
            {
                _photoCounter = CurrentPhotoset.Urls.Count - 1;
            }

            string path = _localStorageModel.GetImageToShow(CurrentPhotoset.Urls[_photoCounter]);

            if (path.Equals(""))
            {
                if (CheckIfNetworkConnection())
                {
                    _displayUnstoredPhotoCounter = 0;
                    PhotoPath = CurrentPhotoset.Urls[_photoCounter].Link;
                }
                else
                {
                    if (_displayUnstoredPhotoCounter <= CurrentPhotoset.Urls.Count)
                    {
                        _displayUnstoredPhotoCounter++;
                        ChangePreviousPhoto();
                    }
                    else
                    {
                        _displayUnstoredPhotoCounter = 0;
                        PhotoPath = EMPTY_PHOTOSET_DEFAULT_PATH;
                    }
                }
            }
            else
            {
                _displayUnstoredPhotoCounter = 0;
                PhotoPath = path;
            }
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

            try
            {
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
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
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

                    if (_slideshowCounter >= SLIDESHOW_TIMER)
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
                var result = await Application.Current.MainPage.DisplayAlert("Disconnect?", "Do you want to disconnect acconut: \"" + item.AccountName + "\" from this DPF?", "Disconnect", "Cancel");
                if (result)
                {
                    item.AccountName = "Disconnecting...";
                    DisconnectAccount(item.AccountId);
                }
            }    
        }

        private async void DisconnectAccount(int accountId)
        {
            try
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
                    await GetAllPhotosUrl();
                }
            }
            catch (Exception exception)
            {
                OnErrorOccurred(this, exception.Message);
            }
        }


        private async void GetDropboxPhotos()
        {

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
