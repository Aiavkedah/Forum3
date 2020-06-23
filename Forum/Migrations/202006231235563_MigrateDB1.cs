namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForumComments", "Name", c => c.String(nullable: false));
            AddColumn("dbo.ForumPosts", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ForumPosts", "Name");
            DropColumn("dbo.ForumComments", "Name");
        }
    }
}
