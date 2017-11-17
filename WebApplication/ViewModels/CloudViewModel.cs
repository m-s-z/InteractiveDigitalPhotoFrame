using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class CloudViewModel
    {
        public CloudViewModel(List<Cloud> clouds)
        {
            Clouds = clouds;
        }

        public List<Cloud> Clouds { get; set; }
    }
}