using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeviceId { get; set; }

        public Folder(string Name, int Id, int Device)
        {
            this.Id = Id;
            this.Name = Name;
            this.DeviceId = Device;
        }
    }
}