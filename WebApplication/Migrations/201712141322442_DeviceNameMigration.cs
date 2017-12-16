namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceNameMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceNames",
                c => new
                    {
                        DeviceNameId = c.Int(nullable: false, identity: true),
                        CustomDeviceName = c.String(),
                        Account_Id = c.Int(),
                        Device_DeviceId = c.Int(),
                    })
                .PrimaryKey(t => t.DeviceNameId)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Devices", t => t.Device_DeviceId)
                .Index(t => t.Account_Id)
                .Index(t => t.Device_DeviceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceNames", "Device_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.DeviceNames", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.DeviceNames", new[] { "Device_DeviceId" });
            DropIndex("dbo.DeviceNames", new[] { "Account_Id" });
            DropTable("dbo.DeviceNames");
        }
    }
}
