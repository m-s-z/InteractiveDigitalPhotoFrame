namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication.Data.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations";
        }

        protected override void Seed(WebApplication.Data.ApplicationContext context)
        {
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Folders]");
            context.SaveChanges();
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [DeviceAccounts]");
            context.SaveChanges();

            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Devices]");

            context.SaveChanges();

            context.Accounts.AddOrUpdate(
                p => p.Id, DummyData.getAccounts().ToArray());
            context.SaveChanges();
            //context.Devices.AddOrUpdate( p => p.DeviceId, DummyData.getDevices().ToArray());
            context.SaveChanges();
            context.Clouds.AddOrUpdate(
                p => p.CloudId, DummyData.getClouds(context).ToArray());
            context.SaveChanges();
            context.Folders.AddOrUpdate(
                p => p.FolderId, DummyData.getFolders(context).ToArray());
        }
    }
}
