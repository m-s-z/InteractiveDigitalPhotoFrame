using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class FolderViewModel
    {
        //we should group folders to appropriate devices
        public FolderViewModel(List<Device> devices, List<Folder> folders)
        {
            Devices = devices;
            Folders = folders;
        }

        public FolderViewModel(List<Device> devices, List<Folder> folders, int indexOfOpenDevice)
        {
            Devices = devices;
            Folders = folders;
            IndexOfOpenDevice = indexOfOpenDevice;
        }

        public List<Device> Devices { get; set; }
        public List<Folder> Folders { get; set; }
        public int IndexOfOpenDevice { get; set; }
    }
}