using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    /// <summary>
    /// device name model class, class connecting devices and accounts
    /// </summary>
    public class DeviceName
    {
        /// <summary>
        /// constructor for DeviceName model class
        /// </summary>   
        public DeviceName()
        {

        }
        /// <summary>
        /// constructor for DeviceName model class
        /// </summary>
        /// <param name="account">instance of Account model class</param>
        /// <param name="device">instance of Device model class</param>
        /// <param name="customDeviceName">custom name for device</param>
        public DeviceName(Account account, Device device, string customDeviceName)
        {
            Account = account;
            Device = device;
            CustomDeviceName = customDeviceName;
        }
        /// <summary>
        /// constructor for DeviceName model class
        /// </summary>
        /// <param name="deviceNameId">id</param>
        /// <param name="account">instance of Account model class</param>
        /// <param name="device">instance of Device model class</param>
        /// <param name="customDeviceName">custom name for device</param>
        public DeviceName(int deviceNameId, Account account, Device device, string customDeviceName)
        {
            DeviceNameId = deviceNameId;
            Account = account;
            Device = device;
            CustomDeviceName = customDeviceName;
        }
        /// <summary>
        /// id for entry
        /// </summary>
        public int DeviceNameId { get; set; }
        [Index("IX_AccountAndDevice", 1, IsUnique = true)]
        public Account Account { get; set; }
        [Index("IX_AccountAndDevice", 2, IsUnique = true)]
        public Device Device { get; set; }
        /// <summary>
        /// custom name for device, specific to the account
        /// </summary>
        public String CustomDeviceName { get; set; }
    }
}