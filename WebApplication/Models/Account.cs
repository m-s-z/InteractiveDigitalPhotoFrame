using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Account
    {
        public Account()
        {

        }
        public Account(string accountName, string password = "")
        {
            Login = accountName;
            Password = password;
        }

        public Account(int id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }

        public Account(string login, string password, ICollection<Device> devices)
        {
            Login = login;
            Password = password;
            Devices = devices;
        }

        public Account(int id, string login, string password, ICollection<Device> devices)
        {
            Id = id;
            Login = login;
            Password = password;
            Devices = devices;
        }

        public int Id { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }   
}