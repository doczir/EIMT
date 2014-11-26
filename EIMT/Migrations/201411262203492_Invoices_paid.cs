namespace EIMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Invoices_paid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Paid");
        }
    }
}
