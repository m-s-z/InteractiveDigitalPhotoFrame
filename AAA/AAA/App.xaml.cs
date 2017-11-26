using AAA.ViewModels;
using AAA.Views;
using Xamarin.Forms;

namespace AAA
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainAppPage();
            MainPage = new NavigationPage(new MainAppPage());

            //MainPage = new NavigationPage(new FolderPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
