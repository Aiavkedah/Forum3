namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ForumComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ForumUserId = c.Int(nullable: false),
                        ForumPostId = c.Int(nullable: false),
                        Comment = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ForumPosts", t => t.ForumPostId, cascadeDelete: true)
                .Index(t => t.ForumPostId);
            
            CreateTable(
                "dbo.ForumPosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ForumUserId = c.Int(nullable: false),
                        ForumCategoryId = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ForumCategories", t => t.ForumCategoryId, cascadeDelete: true)
                .Index(t => t.ForumCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumComments", "ForumPostId", "dbo.ForumPosts");
            DropForeignKey("dbo.ForumPosts", "ForumCategoryId", "dbo.ForumCategories");
            DropIndex("dbo.ForumPosts", new[] { "ForumCategoryId" });
            DropIndex("dbo.ForumComments", new[] { "ForumPostId" });
            DropTable("dbo.ForumPosts");
            DropTable("dbo.ForumComments");
            DropTable("dbo.ForumCategories");
        }
    }
}
