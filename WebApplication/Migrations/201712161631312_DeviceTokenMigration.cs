namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceTokenMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "DeviceToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "DeviceToken");
        }
    }
}
