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

            ForumDB.ForumCategories.Add(new ForumCategory { ID = 1, Text = "Category1" });
            ForumDB.ForumCategories.Add(new ForumCategory { ID = 2, Text = "Category2" });

            ForumDB.ForumPosts.Add(new ForumPost { ID = 1, ForumCategoryId = 1, ForumUserId = 1, Text = "Title Forum1 Category1" });
            ForumDB.ForumPosts.Add(new ForumPost { ID = 2, ForumCategoryId = 1, ForumUserId = 1, Text = "Title Forum2 Category1" });
            ForumDB.ForumPosts.Add(new ForumPost { ID = 3, ForumCategoryId = 2, ForumUserId = 1, Text = "Title Forum3 Category2" });

            ForumDB.ForumComments.Add(new ForumComment { ID = 1, ForumPostId = 1, ForumUserId = 1, Text = "Comment1 Forum1", Date = DateTime.Now });
            ForumDB.ForumComments.Add(new ForumComment { ID = 2, ForumPostId = 1, ForumUserId = 1, Text = "Comment2 Forum1", Date = DateTime.Now });
            ForumDB.ForumComments.Add(new ForumComment { ID = 3, ForumPostId = 2, ForumUserId = 1, Text = "Comment3 Forum2", Date = DateTime.Now });
            ForumDB.SaveChanges();
            return ForumDB;
        }
    }
}
