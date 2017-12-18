using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPF.Models;
using DPF.Views;
using Xamarin.Forms;

namespace DPF
{
    public partial class App : Application
    {
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
