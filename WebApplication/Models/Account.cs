using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Account
    {
        public Account(string accountName, string password = "")
        {
            AccountName = accountName;
            Password = password;
        }

        public String AccountName { get; set; }
        public String Password { get; set; }
    }
}