using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class RegisterConfirmViewModel
    {
        public RegisterConfirmViewModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}