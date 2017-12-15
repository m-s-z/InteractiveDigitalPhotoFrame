namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceMyPropertyFix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Devices", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "MyProperty", c => c.Int(nullable: false));
        }
    }
}
