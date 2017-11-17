using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public enum ProviderType
    {
        GoogleDrive, OneDrive, DropBox, Flicker
    }
    public class Cloud
    {
        public Cloud(ProviderType provider, string login)
        {
            Provider = provider;
            Login = login;
        }

        public ProviderType Provider { get; set; }
        public string Login { get; set; }
    }
}