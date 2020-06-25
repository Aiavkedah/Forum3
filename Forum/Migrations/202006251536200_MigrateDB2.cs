namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ForumComments", "ForumUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ForumComments", "ForumUserId");
            AddForeignKey("dbo.ForumComments", "ForumUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumComments", "ForumUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ForumComments", new[] { "ForumUserId" });
            AlterColumn("dbo.ForumComments", "ForumUserId", c => c.String());
        }
    }
}
