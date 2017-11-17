using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class AccountViewModel
    {
        public AccountViewModel(Account account)
        {
            Account = account;
        }

        public Account Account { get; set; }
    }
}