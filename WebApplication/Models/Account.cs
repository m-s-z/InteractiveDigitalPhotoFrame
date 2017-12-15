using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WebApplication.Models
{
    public class Account
    {
        public Account()
        {

        }
        public Account(string accountName, string password = "")
        {
            Login = accountName;
            Password = password;
        }

        public Account(int id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }

        public Account(string login, string password, ICollection<Device> devices)
        {
            Login = login;
            Password = password;
            Devices = devices;
        }

        public Account(int id, string login, string password, ICollection<Device> devices)
        {
            Id = id;
            Login = login;
            Password = password;
            Devices = devices;
        }

        public int Id { get; set; }
        [Index(IsUnique = true)]
        [StringLength(200)]
        public String Login { get; set; }
        public String Password { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        //helper function to create hashed password. Store the result of this function in db
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
        //helper function to compare passwords
        public static bool PasswordEquals(string password, string hashedPassword)
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
            return true;
        }
    }   
}