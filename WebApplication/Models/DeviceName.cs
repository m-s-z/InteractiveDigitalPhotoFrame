using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DeviceName
    {
        public DeviceName()
        {

        }

        public DeviceName(Account account, Device device, string customDeviceName)
        {
            Account = account;
            Device = device;
            CustomDeviceName = customDeviceName;
        }

        public DeviceName(int deviceNameId, Account account, Device device, string customDeviceName)
        {
            DeviceNameId = deviceNameId;
            Account = account;
            Device = device;
            CustomDeviceName = customDeviceName;
        }
        
        public int DeviceNameId { get; set; }
        [Index("IX_AccountAndDevice", 1, IsUnique = true)]
        public Account Account { get; set; }
        [Index("IX_AccountAndDevice", 2, IsUnique = true)]
        public Device Device { get; set; }
        public String CustomDeviceName { get; set; }
    }
}