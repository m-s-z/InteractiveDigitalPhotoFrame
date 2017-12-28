using System.Collections.ObjectModel;
using IDPFLibrary;

namespace DPF.Models
{
    public class AccountCloud
    {
        public string FlickrUserId { get; set; }
        public CloudProviderType CloudProvider { get; set; }
        public string Login { get; set; }
        public int Id { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
        public ObservableCollection<CloudFolder> FoldersCollection;

        public AccountCloud()
        {

        }
    }
}