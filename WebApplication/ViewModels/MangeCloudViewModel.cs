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

        public String Login { get; set; }
    }
}