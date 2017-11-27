using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using AAA.Utils.CloudProvider;
using IDPFLibrary;

namespace AAA.Models
{
    public class Account : ViewModelBase
    {
        #region fields

        private ObservableCollection<CloudProvider> _cloudsCollection;
        private ObservableCollection<Device> _devicesCollection;
        private string _accountEmail;
        private string _login;
        private string _password;

        #endregion

        #region properties

        public ObservableCollection<CloudProvider> CloudsCollection
        {
            get => _cloudsCollection;
            set => SetProperty(ref _cloudsCollection, value);
        }

        public ObservableCollection<Device> DevicesCollection
        {
            get => _devicesCollection;
            set => SetProperty(ref _devicesCollection, value);
        }

        public string AccountEmail
        {
            get => _accountEmail;
            set => SetProperty(ref _accountEmail, value);
        }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Password
        {
            get => _password;
        }

        #endregion

        #region methods

        public Account()
        {
            CloudsCollection = new ObservableCollection<CloudProvider>();
            DevicesCollection = new ObservableCollection<Device>();
            Login = "MSZ";
            AccountEmail = "mateusz@student.mini.pw.edu.pl";
            SetPassword("Password123");
        }

        public Account(int temp)
        {
            CloudsCollection = new ObservableCollection<CloudProvider>();
            CloudsCollection.Add(new CloudProvider(CloudTypeEnum.Dropbox, "dddddd@email.com"));
            CloudsCollection.Add(new CloudProvider(CloudTypeEnum.GoogleDrive, "xxxxxx@gmail.com"));
            DevicesCollection = new ObservableCollection<Device>();
            DevicesCollection.Add(new Device("Grandparents' DPF", temp));
            DevicesCollection.Add(new Device("Selfies", temp));
            Login = "MSZ";
            AccountEmail = "mateusz@student.mini.pw.edu.pl";
            SetPassword("Password123");
        }

        public void UpdateInformation()
        {
            foreach (Device device in DevicesCollection)
            {
                device.UpdateInformation();
            }
        }

        public void SetPassword(string newPassword)
        {
            _password = newPassword;
        }

        public int CountCloudProviders()
        {
            return CloudsCollection.Count;
        }

        public int CountDevices()
        {
            return DevicesCollection.Count;
        }

        public int CountAllFolders()
        {
            int numberOfFolders = 0;

            foreach (var device in DevicesCollection)
            {
                numberOfFolders += device.NumberOfFolders;
            }

            return numberOfFolders;
        }

        #endregion
    }
}