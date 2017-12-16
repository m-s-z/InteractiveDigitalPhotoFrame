namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DevicePairCodeSeed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "MyProperty", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "ConnectionCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "ConnectionCode");
            DropColumn("dbo.Devices", "MyProperty");
        }
    }
}
