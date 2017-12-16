using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Folder
    {
        public int FolderId { get; set; }
        public string Name { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public int CloudId { get; set; }
        public Cloud Cloud { get; set; }
        public Folder()
        {

        }

        public Folder(string name, int deviceId, int cloudId)
        {
            Name = name;
            DeviceId = deviceId;
            CloudId = cloudId;
        }

        public Folder(string name, Device device, Cloud cloud)
        {
            Name = name;
            Device = device;
            Cloud = cloud;
        }
    }
}