using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class DeviceViewModel
    {
        public DeviceViewModel(List<DeviceName> devices)
        {
            Devices = devices;
        }

        public List<DeviceName> Devices { get; set; }
    }
}