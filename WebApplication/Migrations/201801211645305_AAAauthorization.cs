namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AAAauthorization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorizationTokens",
                c => new
                    {
                        AuthorizationTokenId = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        ExpirationDate = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AuthorizationTokenId)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorizationTokens", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.AuthorizationTokens", new[] { "Account_Id" });
            DropTable("dbo.AuthorizationTokens");
        }
    }
}
