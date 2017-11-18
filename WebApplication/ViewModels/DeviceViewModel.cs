using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class DeviceViewModel
    {
        //we should group folders to appropriate devices
        public DeviceViewModel(List<Device> devices, List<Folder> folders)
        {
            Devices = devices;
            Folders = folders;
        }

        public List<Device> Devices { get; set; }
        public List<Folder> Folders { get; set; }
    }
}