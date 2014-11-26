namespace EIMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserServiceProvider : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserServiceProviders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceProvider_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceProviders", t => t.ServiceProvider_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ServiceProvider_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserServiceProviders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserServiceProviders", "ServiceProvider_Id", "dbo.ServiceProviders");
            DropIndex("dbo.UserServiceProviders", new[] { "User_Id" });
            DropIndex("dbo.UserServiceProviders", new[] { "ServiceProvider_Id" });
            DropTable("dbo.UserServiceProviders");
        }
    }
}
