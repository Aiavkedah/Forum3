using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ForumContext : DbContext
    {
        public ForumContext() : base("DefaultConnection")
        { }

        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public DbSet<ForumCategory> ForumCategories { get; set; }
    }
}