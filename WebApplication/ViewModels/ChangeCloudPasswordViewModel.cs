using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class ChangeCloudPasswordViewModel
    {
        public ChangeCloudPasswordViewModel(string result)
        {
            Result = result;
        }

        public string Result { get; set; }
    }
}