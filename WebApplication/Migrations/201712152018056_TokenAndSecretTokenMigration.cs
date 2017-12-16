namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TokenAndSecretTokenMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clouds", "Token", c => c.String());
            AddColumn("dbo.Clouds", "TokenSecret", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clouds", "TokenSecret");
            DropColumn("dbo.Clouds", "Token");
        }
    }
}
