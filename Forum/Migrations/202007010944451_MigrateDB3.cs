namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ForumCategories", "Text", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ForumCategories", "Text", c => c.String(nullable: false));
        }
    }
}
