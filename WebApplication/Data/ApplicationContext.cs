using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Cloud> Clouds { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<DeviceName> DeviceNames { get; set; }
    }
}