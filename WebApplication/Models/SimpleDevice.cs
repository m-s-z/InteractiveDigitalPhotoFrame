using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class SimpleDevice
    {
        public SimpleDevice(Device device)
        {
            Accounts = new List<int>();
            DeviceId = device.DeviceId;
            Name = device.Name;
            foreach(var account in device.Accounts)
            {
                Accounts.Add(account.Id);
            }
        }
        public SimpleDevice(int deviceId, string name)
        {
            DeviceId = deviceId;
            Name = name;
        }

        public SimpleDevice(int deviceId, string name, List<int> accounts)
        {
            DeviceId = deviceId;
            Name = name;
            Accounts = accounts;
        }

        public int DeviceId { get; set; }
        public string Name { get; set; }
        public List<int> Accounts { get; set; }
    }
}