namespace EIMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_ConfirmedByAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ConfirmedByAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ConfirmedByAdmin");
        }
    }
}
