namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        DeviceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeviceId);
            
            CreateTable(
                "dbo.Clouds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CloudId = c.Int(nullable: false),
                        password = c.String(),
                        Provider = c.Int(nullable: false),
                        Login = c.String(),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Folders",
                c => new
                    {
                        FolderId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeviceId = c.Int(nullable: false),
                        CloudId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FolderId)
                .ForeignKey("dbo.Clouds", t => t.CloudId, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId)
                .Index(t => t.CloudId);
            
            CreateTable(
                "dbo.DeviceAccounts",
                c => new
                    {
                        Device_DeviceId = c.Int(nullable: false),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Device_DeviceId, t.Account_Id })
                .ForeignKey("dbo.Devices", t => t.Device_DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Device_DeviceId)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Folders", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Folders", "CloudId", "dbo.Clouds");
            DropForeignKey("dbo.Clouds", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.DeviceAccounts", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.DeviceAccounts", "Device_DeviceId", "dbo.Devices");
            DropIndex("dbo.DeviceAccounts", new[] { "Account_Id" });
            DropIndex("dbo.DeviceAccounts", new[] { "Device_DeviceId" });
            DropIndex("dbo.Folders", new[] { "CloudId" });
            DropIndex("dbo.Folders", new[] { "DeviceId" });
            DropIndex("dbo.Clouds", new[] { "Account_Id" });
            DropTable("dbo.DeviceAccounts");
            DropTable("dbo.Folders");
            DropTable("dbo.Clouds");
            DropTable("dbo.Devices");
            DropTable("dbo.Accounts");
        }
    }
}
