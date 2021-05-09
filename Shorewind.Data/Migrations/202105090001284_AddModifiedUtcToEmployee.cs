namespace Shorewind.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModifiedUtcToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "ModifiedUtc", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "ModifiedUtc");
        }
    }
}
