using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDPFLibrary;
using Xamarin.Forms;

namespace AAA.Utils
{
    public class ListItem : ViewModelBase
    {
        private string _s1;
        private CloudType _s2;

        public string S1
        {
            get => _s1;
            set => SetProperty(ref _s1, value);
        }

        public CloudType S2
        {
            get => _s2;
            set => SetProperty(ref _s2, value);
        }

        public Command Command1
        {
            get;
            set;
        }

        public Command Command2
        {
            get;
            set;
        }

        public ListItem(string s1, string s2 = null)
        {
            S1 = s1;
            S2 = CloudType.Microsoft;
            Command1 = new Command(ExecuteCommand1);
            Command2 = new Command(ExecuteCommand2);
        }

        private void ExecuteCommand1()
        {
            Application.Current.MainPage.DisplayAlert("Command1", "Command1 was executed", "OK");
        }

        private void ExecuteCommand2()
        {
            Application.Current.MainPage.DisplayAlert("Command2", "Command2 was executed", "OK");
        }
    }
}
