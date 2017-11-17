using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class DeviceViewModel
    {
        public DeviceViewModel(List<Device> devices)
        {
            Devices = devices;
        }

        public List<Device> Devices { get; set; }
    }
}