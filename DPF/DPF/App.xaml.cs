using DPF.Models;
using DPF.Views;
using Xamarin.Forms;

namespace DPF
{
    /// <summary>
    /// Main application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Application class constructor.
        /// Checks whether the device token has already been saved in the storage or not.
        /// If yes - shows main page, otherwise - presents welcome page.
        /// </summary>
        public App()
        {
            InitializeComponent();
            var deviceToken = DependencyService.Get<ILocalStorageService>().GetDeviceToken();

            if (deviceToken == null)
            {
                MainPage = new WelcomePage();
            }
            else
            {
                MainPage = new NavigationPage(new MainAppPage());
            }
        }
    }
}
