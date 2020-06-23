namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForumCategories", "Text", c => c.String(nullable: false));
            AddColumn("dbo.ForumComments", "Text", c => c.String(nullable: false));
            AddColumn("dbo.ForumPosts", "Text", c => c.String(nullable: false));
            DropColumn("dbo.ForumCategories", "Name");
            DropColumn("dbo.ForumComments", "Comment");
            DropColumn("dbo.ForumComments", "Name");
            DropColumn("dbo.ForumPosts", "Title");
            DropColumn("dbo.ForumPosts", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ForumPosts", "Name", c => c.String(nullable: false));
            AddColumn("dbo.ForumPosts", "Title", c => c.String(nullable: false));
            AddColumn("dbo.ForumComments", "Name", c => c.String(nullable: false));
            AddColumn("dbo.ForumComments", "Comment", c => c.String());
            AddColumn("dbo.ForumCategories", "Name", c => c.String(nullable: false));
            DropColumn("dbo.ForumPosts", "Text");
            DropColumn("dbo.ForumComments", "Text");
            DropColumn("dbo.ForumCategories", "Text");
        }
    }
}
