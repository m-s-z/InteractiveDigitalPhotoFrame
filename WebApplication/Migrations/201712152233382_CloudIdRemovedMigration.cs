namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CloudIdRemovedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clouds", "FlickrUserId", c => c.String());
            DropColumn("dbo.Clouds", "CloudId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clouds", "CloudId", c => c.Int(nullable: false));
            DropColumn("dbo.Clouds", "FlickrUserId");
        }
    }
}
