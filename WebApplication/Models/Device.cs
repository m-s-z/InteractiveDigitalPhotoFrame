using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Folder> Folders { get; set; }
        
        public Device(int id, string name, List<Folder> folders)
        {
            Id = id;
            Name = name;
            Folders = folders;
        }
    }
}