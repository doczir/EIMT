namespace EIMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Invoices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Total = c.Int(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        UserServiceProvider_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserServiceProviders", t => t.UserServiceProvider_Id)
                .Index(t => t.UserServiceProvider_Id);
            
            AddColumn("dbo.UserServiceProviders", "LastInvoiceTotal", c => c.Int(nullable: false));
            AddColumn("dbo.UserServiceProviders", "UserNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "UserServiceProvider_Id", "dbo.UserServiceProviders");
            DropIndex("dbo.Invoices", new[] { "UserServiceProvider_Id" });
            DropColumn("dbo.UserServiceProviders", "UserNumber");
            DropColumn("dbo.UserServiceProviders", "LastInvoiceTotal");
            DropTable("dbo.Invoices");
        }
    }
}
