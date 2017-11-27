using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAA.Utils.CloudProvider;
using AAA.Views;
using IDPFLibrary;
using Xamarin.Forms;

namespace AAA.Utils
{
    public class ListItem : ViewModelBase
    {
        private string _s1;
        private string _s2;
        private CloudTypeEnum _sImage;

        public string S1
        {
            get => _s1;
            set => SetProperty(ref _s1, value);
        }

        public string S2
        {
            get => _s2;
            set => SetProperty(ref _s2, value);
        }

        public CloudTypeEnum SImage
        {
            get => _sImage;
            set => SetProperty(ref _sImage, value);
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

        public ListItem(string s1, CloudTypeEnum cT = CloudTypeEnum.None, string s2 = "")
        {
            S1 = s1;
            S2 = s2;
            SImage = cT;
            Command1 = new Command(ExecuteCommand1);
            Command2 = new Command(ExecuteCommand2);
        }

        public ListItem(string s1,  Command c1, CloudTypeEnum cT = CloudTypeEnum.None,  string s2 = "",  Command c2 = null)
        {
            S1 = s1;
            S2 = s2;
            SImage = cT;
            Command1 = c1;
            if (c2 == null)
            {
                Command2 = new Command(ExecuteCommand2);
            }
            else
            {
                Command2 = c2;
            }
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
