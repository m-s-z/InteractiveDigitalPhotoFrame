using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class ChangePasswordViewModel
    {
        public String Message { get; set; }
        public ChangePasswordViewModel()
        {
                
        }

        public ChangePasswordViewModel(string message)
        {
            Message = message;
        }
    }
}