using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// Account view model class
    /// </summary>
    public class AccountViewModel
    {
        /// <summary>
        /// constructor for Account View model class
        /// </summary>
        /// <param name="account">instance of Account model class</param>
        public AccountViewModel(Account account)
        {
            Account = account;
        }
        /// <summary>
        /// instance of Account model class
        /// </summary>
        public Account Account { get; set; }
    }
}