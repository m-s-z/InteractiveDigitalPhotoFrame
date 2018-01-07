using System.Collections.ObjectModel;
using IDPFLibrary;

namespace DPF.Models
{
    /// <summary>
    /// AccountCloud class.
    /// </summary>
    public class AccountCloud
    {
        #region properties

        /// <summary>
        /// Property indicating flickr user ID.
        /// </summary>
        public string FlickrUserId { get; set; }

        /// <summary>
        /// Property indicating type of cloud provider.
        /// </summary>
        public CloudProviderType CloudProvider { get; set; }

        /// <summary>
        /// Property indicating login of the account.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Property indicating ID of the account.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Property indicating cloud token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Property indicating cloud secret token.
        /// </summary>
        public string TokenSecret { get; set; }

        /// <summary>
        /// Property with collection of folders to sync.
        /// </summary>
        public ObservableCollection<CloudFolder> FoldersCollection;

        #endregion

        #region methods

        /// <summary>
        /// AccountCloud class constructor.
        /// </summary>
        public AccountCloud()
        {

        }

        #endregion
    }
}