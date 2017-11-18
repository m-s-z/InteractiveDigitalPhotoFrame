﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public enum ProviderType
    {
        GoogleDrive = 0, OneDrive = 1, DropBox = 2, Flicker = 3
    }
    public class Cloud
    {
        
        public Cloud(ProviderType provider, string login)
        {
            Provider = provider;
            Login = login;
        }

        public Cloud(string password, ProviderType provider, string login, int id)
        {
            Password = password;
            Provider = provider;
            Login = login;
            Id = id;
        }

        public int CloudId { get; set; }
        public String Password { get; set; }
        public ProviderType Provider { get; set; }
        public string Login { get; set; }

        public int Id { get; set; }
        public Account Account { get; set; }
        
    }
}