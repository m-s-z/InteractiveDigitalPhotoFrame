using System;
using System.Net.Http;
using System.Text;
using DPF.Models;
using DPF.Views;
using IDPFLibrary;
using IDPFLibrary.DTO;
using IDPFLibrary.Utils;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DPF.ViewModels
{
    /// <summary>
    /// WelcomePageViewModel class.
    /// Provides methods and properties which control the welcome page.
    /// </summary>
    public class WelcomePageViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of IsReadyToConnect property.
        /// </summary>
        private bool _isReadyToConnect;

        #endregion

        #region properties

        /// <summary>
        /// Flag indicating whether the device is ready to start connecting to the server or not.
        /// </summary>
        public bool IsReadyToConnect
        {
            get => _isReadyToConnect;
            set => SetProperty(ref _isReadyToConnect, value);
        }

        /// <summary>
        /// Command which sends request to the server to create new DPF device.
        /// </summary>
        public Command CreateNewDeviceCommand
        {
            get;
            set;
        }

        #endregion

        #region methods

        /// <summary>
        /// WelcomePageViewModel class constructor.
        /// </summary>
        public WelcomePageViewModel()
        {
            CreateNewDeviceCommand = new Command(ExecuteCreateNewDeviceCommand);
            IsReadyToConnect = true;
        }

        /// <summary>
        /// Handles execution of "CreateNewDeviceCommand".
        /// Updates IsReadyToConnect property.
        /// Sends request to the server to create new DPF device.
        /// Receives response containing device token.
        /// Calls dependency service to save device token in storage.
        /// Navigates to the main page.
        /// </summary>
        private async void ExecuteCreateNewDeviceCommand()
        {
            IsReadyToConnect = false;

            if (DependencyService.Get<INetworkConnectionService>().CheckIfNetworkConnected())
            {
                using (var client = new HttpClient())
                {
                    CreateNewDeviceRequestDTO requestDto = new CreateNewDeviceRequestDTO { key = RegistrationCode.CODE };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestDto);

                    string url = "https://idpf.azurewebsites.net/Device/CreateNewDevice";
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
                    CreateNewDeviceDTO newDeviceDto = (JsonConvert.DeserializeObject<CreateNewDeviceDTO>(contents));
                    DependencyService.Get<ILocalStorageService>().SaveDeviceToken(contents);
                }

                var page = new MainAppPage();
                Application.Current.MainPage = new NavigationPage(page);
                IsReadyToConnect = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Offline mode", "Connect to the Internet to start using DPF", "OK");
                IsReadyToConnect = true;
            }
        }

        #endregion
    }
}