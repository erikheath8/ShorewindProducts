namespace Shorewind.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastNameToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "LastName");
        }
    }
}
