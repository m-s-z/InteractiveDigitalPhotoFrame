using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WebApplication.Models
{
    /// <summary>
    /// Account model class
    /// </summary>
    public class Account
    {
        /// <summary>
        /// constructor for Account
        /// </summary>
        public Account()
        {

        }

        /// <summary>
        /// constructor for Account
        /// </summary>
        /// <param name="accountName">account name</param>
        /// <param name="password">password, default value is ""</param>
        public Account(string accountName, string password = "")
        {
            Login = accountName;
            Password = password;
        }

        /// <summary>
        /// constructor for Account
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        public Account(int id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }

        /// <summary>
        /// constructor for Account
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <param name="devices">collection of devices</param>
        public Account(string login, string password, ICollection<Device> devices)
        {
            Login = login;
            Password = password;
            Devices = devices;
        }

        /// <summary>
        /// constructor for Account
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <param name="devices">collection of devices</param>
        public Account(int id, string login, string password, ICollection<Device> devices)
        {
            Id = id;
            Login = login;
            Password = password;
            Devices = devices;
        }

        #region properties
        /// <summary>
        /// id, has to be unique
        /// max lenttgh 200 characters
        /// </summary>
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [StringLength(200)]
        /// <summary>
        /// login
        /// </summary>
        public String Login { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public String Password { get; set; }
        /// <summary>
        /// collection of devices
        /// </summary>
        public virtual ICollection<Device> Devices { get; set; }
        #endregion properties

        #region methods

        /// <summary>
        /// helper function to create hashed password. Store the result of this function in db
        /// </summary>
        /// <param name="password">string to be hashed</param>
        /// <returns>
        /// returns string of with hashed value
        /// </returns>
        public static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }
        /// <summary>
        /// helper function to compare passwords
        /// </summary>
        /// <param name="password">first password (not hashed)</param>
        /// <param name="hashedPassword">hashed password to compare to </param>
        /// <returns>
        /// true if strings have same hash value
        /// false if strings have different hash value
        /// </returns>
        public static bool PasswordEquals(string password, string hashedPassword)
        {
            //the try and catch will be removed later once i delete the initial accounts without hashed passwords (hotfix)
            try
            {
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }
            }catch(Exception e)
            {
                return false;
            }
            return true;
        }
        #endregion methods
    }
}