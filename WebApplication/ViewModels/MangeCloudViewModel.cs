using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class MangeCloudViewModel
    {
        public MangeCloudViewModel(string name)
        {
            Login = name;
        }

        public MangeCloudViewModel(int id, string login)
        {
            Id = id;
            Login = login;
        }

        public int Id { get; set; }
        public String Login { get; set; }
    }
}