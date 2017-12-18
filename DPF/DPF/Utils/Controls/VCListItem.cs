using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPF.Models;
using IDPFLibrary;
using Xamarin.Forms;

namespace DPF.Utils.Controls
{
    public class VCListItem : ViewModelBase
    {
        private Command _additionalCommand;
        private string _accountName;
        private int _accountId;

        #region properties

        /// <summary>
        /// Additional command to execute on right side of item tap.
        /// </summary>
        public Command AdditionalCommand
        {
            get { return _additionalCommand; }
            set { SetProperty(ref _additionalCommand, value); }
        }

        public string AccountName
        {
            get => _accountName;
            set => SetProperty(ref _accountName, value);
        }

        public int AccountId
        {
            get => _accountId;
            set => SetProperty(ref _accountId, value);
        }

        #endregion

        #region methods

        public VCListItem(string accountName, int accountId, Command additionalCommand)
        {
            AccountName = accountName;
            AccountId = accountId;
            AdditionalCommand = additionalCommand;
        }

        public VCListItem()
        {

        }

        #endregion
    }
}