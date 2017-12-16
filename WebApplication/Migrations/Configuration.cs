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
            
            /*i dont want to fully delete it but for now im going away from populating the db using the seed method as it generates to many errors
             * context.Accounts.AddOrUpdate(
                p => p.Id, DummyData.getAccounts().ToArray());
            context.SaveChanges();
            //context.Devices.AddOrUpdate( p => p.DeviceId, DummyData.getDevices().ToArray());
            context.SaveChanges();
            context.Clouds.AddOrUpdate(
                p => p.CloudId, DummyData.getClouds(context).ToArray());
            context.SaveChanges();
            context.DeviceNames.AddOrUpdate(
                p => p.DeviceNameId, DummyData.getDeviceNames(context).ToArray());
                */
        }
    }
}
