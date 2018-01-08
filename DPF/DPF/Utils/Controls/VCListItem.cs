using IDPFLibrary;
using Xamarin.Forms;

namespace DPF.Utils.Controls
{
    /// <summary>
    /// VCListItem class.
    /// </summary>
    public class VCListItem : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of AdditionalCommand property.
        /// </summary>
        private Command _additionalCommand;

        /// <summary>
        /// Backing field of AccountName property.
        /// </summary>
        private string _accountName;

        /// <summary>
        /// Backing field of AccountId property.
        /// </summary>
        private int _accountId;

        #endregion

        #region properties

        /// <summary>
        /// Additional command to execute on right side of item tap.
        /// </summary>
        public Command AdditionalCommand
        {
            get { return _additionalCommand; }
            set { SetProperty(ref _additionalCommand, value); }
        }

        /// <summary>
        /// Name of the account.
        /// </summary>
        public string AccountName
        {
            get => _accountName;
            set => SetProperty(ref _accountName, value);
        }

        /// <summary>
        /// ID of the account.
        /// </summary>
        public int AccountId
        {
            get => _accountId;
            set => SetProperty(ref _accountId, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// VCListItem class constructor.
        /// </summary>
        /// <param name="accountName">Name of the account.</param>
        /// <param name="accountId">ID of the account.</param>
        /// <param name="additionalCommand">Command to execute on tap.</param>
        public VCListItem(string accountName, int accountId, Command additionalCommand)
        {
            AccountName = accountName;
            AccountId = accountId;
            AdditionalCommand = additionalCommand;
        }

        /// <summary>
        /// VCListItem class constructor.
        /// </summary>
        public VCListItem()
        {

        }

        #endregion
    }
}