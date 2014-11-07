namespace EIMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Service_Providers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListServiceProviders",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.Int(nullable: false),
                    AccountNumber = c.String(),
                    Password = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.ListServiceProviders");
        }
    }
}
