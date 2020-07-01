namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ForumPosts", "Text", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ForumPosts", "Text", c => c.String(nullable: false));
        }
    }
}
