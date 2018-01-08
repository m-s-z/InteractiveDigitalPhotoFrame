using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    /// <summary>
    /// device model class
    /// </summary>
    public class Device
    {
        #region properties
        /// <summary>
        /// device token
        /// </summary>
        public String DeviceToken { get; set; }
        /// <summary>
        /// device id
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// name for device
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// collection of Account model class
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; }
        /// <summary>
        /// connection code, needed to conect with device
        /// </summary>
        public string ConnectionCode { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for Device model class
        /// </summary>
        public Device()
        {

        }
        /// <summary>
        /// constructor for Device model class
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="id">id</param>
        public Device(int id, string name)
        {
            DeviceId = id;
            Name = name;
        }

        public Device(string name)
        {
            Name = name;
        }
        /// <summary>
        /// constructor for Device model class
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="accounts">Collection of account model class</param>
        public Device(string name, ICollection<Account> accounts)
        {
            Name = name;
            Accounts = accounts;
        }
        /// <summary>
        /// constructor for Device model class
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="name">name</param>
        /// <param name="accounts">Collection of account model class</param>
        /// <param name="connectionCode"> connection code</param>
        public Device(int deviceId, string name, ICollection<Account> accounts, string connectionCode)
        {
            DeviceId = deviceId;
            Name = name;
            Accounts = accounts;
            ConnectionCode = connectionCode;
        }

        /// <summary>
        /// constructor for Device model class
        /// </summary>
        /// <param name="deviceToken">device token</param>
        /// <param name="name">name</param>
        /// <param name="accounts">Collection of account model class</param>
        /// <param name="connectionCode"> connection code</param>
        public Device(string deviceToken, string name, ICollection<Account> accounts, string connectionCode)
        {
            DeviceToken = deviceToken;
            Name = name;
            Accounts = accounts;
            ConnectionCode = connectionCode;
        }
        /// <summary>
        /// constructor for Device model class
        /// </summary>
        /// <param name="deviceToken">device token</param>
        /// <param name="name">name</param>
        /// <param name="accounts">Collection of account model class</param>
        public Device(string deviceToken, string name, ICollection<Account> accounts)
        {
            DeviceToken = deviceToken;
            Name = name;
            Accounts = accounts;
        }
    }
}