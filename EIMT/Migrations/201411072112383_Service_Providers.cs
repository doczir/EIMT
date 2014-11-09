namespace EIMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Service_Providers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceProviders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AccountNumber = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ServiceProviders");
        }
    }
}
