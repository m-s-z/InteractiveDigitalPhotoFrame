using System;
using System.Net.Http;
using System.Text;
using DPF.Models;
using DPF.Views;
using IDPFLibrary.DTO;
using IDPFLibrary.Utils;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DPF.ViewModels
{
    public class WelcomePageViewModel
    {
        public Command CreateNewDeviceCommand
        {
            get;
            set;
        }

        public WelcomePageViewModel()
        {
            CreateNewDeviceCommand = new Command(ExecuteCreateNewDeviceCommand);
        }

        private async void ExecuteCreateNewDeviceCommand()
        {
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
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Offline mode", "Connect to the Internet to start using DPF", "OK");
            }
        }
    }
}