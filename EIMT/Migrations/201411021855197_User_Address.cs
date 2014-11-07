namespace EIMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class User_Address : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}