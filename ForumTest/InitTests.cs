using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using Forum.Controllers;
using Forum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForumTest
{
    public class InitTests
    {
        public static ForumContext ForumContext()
        {
            ForumContext ForumDB = new Forum.Models.ForumContext();

            ForumDB.ForumCategories.Add(new ForumCategory { ID = 1, Name = "Category1" });
            ForumDB.ForumCategories.Add(new ForumCategory { ID = 2, Name = "Category2" });

            ForumDB.ForumPosts.Add(new ForumPost { ID = 1, ForumCategoryId = 1, ForumUserId = 1, Title = "Title Forum1 Category1" });
            ForumDB.ForumPosts.Add(new ForumPost { ID = 2, ForumCategoryId = 1, ForumUserId = 1, Title = "Title Forum2 Category1" });
            ForumDB.ForumPosts.Add(new ForumPost { ID = 3, ForumCategoryId = 2, ForumUserId = 1, Title = "Title Forum3 Category2" });

            ForumDB.ForumComments.Add(new ForumComment { ID = 1, ForumPostId = 1, ForumUserId = 1, Comment = "Comment1 Forum1", Date = DateTime.Now });
            ForumDB.ForumComments.Add(new ForumComment { ID = 2, ForumPostId = 1, ForumUserId = 1, Comment = "Comment2 Forum1", Date = DateTime.Now });
            ForumDB.ForumComments.Add(new ForumComment { ID = 3, ForumPostId = 2, ForumUserId = 1, Comment = "Comment3 Forum2", Date = DateTime.Now });
            ForumDB.SaveChanges();
            return ForumDB;
        }
    }
}
