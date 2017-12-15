namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceNameMigrationSeedFix1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Login", c => c.String(maxLength: 200));
            CreateIndex("dbo.Accounts", "Login", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Accounts", new[] { "Login" });
            AlterColumn("dbo.Accounts", "Login", c => c.String());
        }
    }
}
