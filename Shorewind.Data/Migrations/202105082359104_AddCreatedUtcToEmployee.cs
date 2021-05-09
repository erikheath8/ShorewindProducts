namespace Shorewind.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedUtcToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "CreatedUtc", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "CreatedUtc");
        }
    }
}
