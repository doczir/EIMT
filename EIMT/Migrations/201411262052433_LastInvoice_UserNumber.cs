namespace EIMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastInvoice_UserNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserServiceProviders", "LastInvoiceTotal", c => c.Int(nullable: false));
            AddColumn("dbo.UserServiceProviders", "UserNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserServiceProviders", "UserNumber");
            DropColumn("dbo.UserServiceProviders", "LastInvoiceTotal");
        }
    }
}
