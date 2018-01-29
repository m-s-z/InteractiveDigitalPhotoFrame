using AAA.ViewModels;
using AAA.Views;
using Xamarin.Forms;

namespace AAA
{
    /// <summary>
    /// Main application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Main application class constructor.
        /// </summary>
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
