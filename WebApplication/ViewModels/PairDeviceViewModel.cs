using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class PairDeviceViewModel
    {
        public PairDeviceViewModel(string result)
        {
            Result = result;
        }

        public String Result { get; set; }
    }
}