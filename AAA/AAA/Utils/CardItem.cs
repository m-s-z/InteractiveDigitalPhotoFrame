using IDPFLibrary;
using Xamarin.Forms;

namespace AAA.Utils
{
    public class CardItem : ViewModelBase
    {
        private string _s1;
        private string _s2;
        private string _s3;

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

        public string S3
        {
            get => _s3;
            set => SetProperty(ref _s3, value);
        }

        public Command Command1
        {
            get;
            set;
        }

        public CardItem(string s1, Command c1, string s2, string s3)
        {
            S1 = s1;
            S2 = s2;
            S3 = s3;
            Command1 = c1;
        }
    }
}
