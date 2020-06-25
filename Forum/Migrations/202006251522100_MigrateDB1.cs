namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ForumPosts", "ForumUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ForumPosts", "ForumUserId");
            AddForeignKey("dbo.ForumPosts", "ForumUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumPosts", "ForumUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ForumPosts", new[] { "ForumUserId" });
            AlterColumn("dbo.ForumPosts", "ForumUserId", c => c.String());
        }
    }
}
