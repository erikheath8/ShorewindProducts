namespace Shorewind.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailAddressToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "Email");
        }
    }
}
