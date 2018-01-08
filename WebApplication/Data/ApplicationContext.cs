using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Data
{
    /// <summary>
    /// Application context class, represents the database. Based on this class the entity framework generates a database.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        #region properties
        /// <summary>
        /// devices
        /// </summary>
        public DbSet<Device> Devices { get; set; }
        /// <summary>
        /// accounts
        /// </summary>
        public DbSet<Account> Accounts { get; set; }
        /// <summary>
        /// clouds
        /// </summary>
        public DbSet<Cloud> Clouds { get; set; }
        /// <summary>
        /// folders
        /// </summary>
        public DbSet<Folder> Folders { get; set; }
        /// <summary>
        /// DeviceNames
        /// </summary>
        public DbSet<DeviceName> DeviceNames { get; set; }
        #endregion properties
        #region methods

        /// <summary>
        /// overriden onModelCreating method with our Application context
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApplicationContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        #endregion methods
    }
}