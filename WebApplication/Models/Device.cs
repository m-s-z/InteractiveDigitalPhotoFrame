using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public Device(int id, string name)
        {
            DeviceId = id;
            Name = name;
        }

        public Device(string name)
        {
            Name = name;
        }

        public Device(string name, ICollection<Account> accounts)
        {
            Name = name;
            Accounts = accounts;
        }
    }
}