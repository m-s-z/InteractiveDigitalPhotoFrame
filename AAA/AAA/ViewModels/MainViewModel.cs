using System.Collections.Generic;
using System.Collections.ObjectModel;
using AAA.Utils;
using AAA.Views;
using Xamarin.Forms;

namespace AAA.ViewModels
{
    /// <summary>
    /// MainViewModel class.
    /// Provides properites and methods to handle application logic.
    /// </summary>
    public class MainViewModel : IDPFLibrary.ViewModelBase
    {
        #region fields

        /// <summary>
        /// Path to some files
        /// </summary>
        private const string EXAMPLE_OF_CONST = "path/to/some/files";

        /// <summary>
        /// Backing field for TestNumberOne property.
        /// </summary>
        private string _testNumberOne;

        /// <summary>
        /// Backing field for TestNumberTwo property.
        /// </summary>
        private bool _testNumberTwo;

        private ObservableCollection<ListItem> _testList;
        private ObservableCollection<CardItem> _testList2;
        private ObservableCollection<ListItem> _testList3;
        private ObservableCollection<ListItem> _testList4;

        #endregion

        #region properties

        /// <summary>
        /// Property indicating some testing value.
        /// </summary>
        public string TestNumberOne
        {
            get => _testNumberOne;

            set => SetProperty(ref _testNumberOne, value);
        }

        /// <summary>
        /// Flag indicating whether is something or not.
        /// </summary>
        public bool TestNumberTwo
        {
            get => _testNumberTwo;

            set => SetProperty(ref _testNumberTwo, value);
        }

        public ObservableCollection<ListItem> TestList
        {
            get => _testList;

            set => SetProperty(ref _testList, value);
        }
        public ObservableCollection<CardItem> TestList2
        {
            get => _testList2;

            set => SetProperty(ref _testList2, value);
        }

        public ObservableCollection<ListItem> TestList3
        {
            get => _testList3;

            set => SetProperty(ref _testList3, value);
        }

        public ObservableCollection<ListItem> TestList4
        {
            get => _testList4;

            set => SetProperty(ref _testList4, value);
        }

        public Command CommandOne { get; set; }

        public Command ChangePageCommand { get; set; }

        public Command GoToProfilePageCommand { get; set; }

        public Command GoToFolderPageCommand { get; set; }

        public Command GoToChooseProviderPageCommand { get; set; }

        public Command GoToChooseCloudFolderPageCommand { get; set; }

        public Command GoToChooseDevicePageCommand { get; set; }

        public Command ReturnCommand { get; set; }

        public Command GoToChangePasswordPageCommand { get; set; }

        public Command GoToAddCloudProviderCommand { get; set; }

        #endregion

        #region methods

        public MainViewModel()
        {
            TestNumberOne = "Hello!!!";
            ChangePageCommand = new Command(ExecuteChangePageCommand);
            GoToFolderPageCommand = new Command(ExecuteGoToFolderPageCommand);
            GoToChangePasswordPageCommand = new Command(ExecuteGoToChangePasswordPageCommand);
            GoToChooseProviderPageCommand = new Command(ExecuteGoToChooseProviderPageCommand);
            GoToChooseCloudFolderPageCommand = new Command(ExecuteGoToChooseCloudFolderPageCommand);
            GoToChooseDevicePageCommand = new Command(ExecuteGoToChooseDevicePageCommand);
            ReturnCommand = new Command(ExecuteReturnCommand);
            CommandOne = new Command(ExecuteCommandGoToDevice);
            GoToAddCloudProviderCommand = new Command(ExecuteGoToAddCloudProviderCommand);
            GoToProfilePageCommand = new Command(ExecuteCommandGoToProfilePage);
            TestList = new ObservableCollection<ListItem>();
            TestList.Add(new ListItem("Grandpa's tablet", CommandOne, CloudType.None, "Folders assigned: 4"));
            TestList.Add(new ListItem("Anna's tablet", CommandOne, CloudType.None, "Folders assigned: 2"));
            TestList.Add(new ListItem("DPF in my school", CommandOne, CloudType.None, "Folders assigned: 3"));
            TestList3 = new ObservableCollection<ListItem>();
            TestList3.Add(new ListItem("Holiday 2017", GoToFolderPageCommand, CloudType.Flickr, "Used by devices: 3"));
            TestList3.Add(new ListItem("Dog's photos", GoToFolderPageCommand, CloudType.Dropbox, "Used by devices: 2"));
            TestList3.Add(new ListItem("Selfies", GoToFolderPageCommand, CloudType.Google, "Used by devices: 1"));
            TestList3.Add(new ListItem("Holiday 2017", GoToFolderPageCommand, CloudType.Flickr, "Used by devices: 3"));
            TestList3.Add(new ListItem("Dog's photos", GoToFolderPageCommand, CloudType.Dropbox, "Used by devices: 2"));
            TestList3.Add(new ListItem("Selfies", GoToFolderPageCommand, CloudType.Google, "Used by devices: 1"));
            TestList3.Add(new ListItem("Holiday 2017", GoToFolderPageCommand, CloudType.Flickr, "Used by devices: 3"));
            TestList4 = new ObservableCollection<ListItem>();
            TestList4.Add(new ListItem("Dropbox", CloudType.Dropbox, "ddddd@email.com"));
            TestList4.Add(new ListItem("Flickr", CloudType.Flickr, "fffff@email.com"));
            TestList4.Add(new ListItem("Google", CloudType.Google, "ggggg@gmail.com"));
            TestList4.Add(new ListItem("Microsoft", CloudType.Microsoft, "mmmmm@email.com"));
            TestList2 = new ObservableCollection<CardItem>();
            TestList2.Add(new CardItem("DEVICE", new Command(ExecuteCommandOne), "Paired devices: 8", "tablet_card_96px.png"));
            TestList2.Add(new CardItem("FOLDERS", new Command(ExecuteCommandTwo), "Assigned folders: 12", "folder_card_96px.png"));
            TestList2.Add(new CardItem("CLOUDS", new Command(ExecuteCommandThree), "Cloud providers: 2", "cloud_card_96px.png"));
        }

        /// <summary>
        /// TestMethod method.
        /// Updates value of TestNumberOne property if secondParameter is true.
        /// </summary>
        /// <param name="firstParameter">Value to assing to TestNumberOne property.</param>
        /// <param name="secondParameter">Flag indicating whether to change value of TestNumberOne property or not.</param>
        /// <returns>Returns flag indicating whether the value of TestNumberOne property was changed or not.</returns>
        private bool TestMethod(string firstParameter, bool secondParameter)
        {
            if (secondParameter)
            {
                TestNumberOne = firstParameter;
                return true;
            }

            return false;
        }
        private void ExecuteCommandOne()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DevicesListPage(this));
        }

        private void ExecuteCommandTwo()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FoldersListPage(this));
        }

        private void ExecuteCommandThree()
        {
            Application.Current.MainPage.Navigation.PushAsync(new CloudsListPage(this));
        }

        private void ExecuteCommandGoToDevice()
        {
            var newPage = new DevicePage();
            newPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(newPage);
        }

        private void ExecuteChangePageCommand(object param)
        {
            var page = new AddDevicePage();
            page.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private void ExecuteCommandGoToProfilePage()
        {
            var newPage = new ProfilPage();
            newPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(newPage);
        }

        private void ExecuteGoToChangePasswordPageCommand()
        {
            var newPage = new ChangePasswordPage();
            newPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(newPage);
        }

        private void ExecuteGoToFolderPageCommand()
        {
            var newPage = new FolderPage();
            newPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(newPage);
        }

        private void ExecuteGoToChooseProviderPageCommand()
        {
            var newPage = new ChooseCloudPage();
            newPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(newPage);
        }

        private void ExecuteGoToChooseCloudFolderPageCommand()
        {
            var newPage = new ChooseCloudFolderPage();
            newPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(newPage);
        }

        private void ExecuteGoToChooseDevicePageCommand()
        {
            var newPage = new ChooseDevicePage();
            newPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(newPage);
        }

        private void ExecuteGoToAddCloudProviderCommand()
        {
            var newPage = new AddCloudPage();
            newPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(newPage);
        }

        private void ExecuteReturnCommand()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}