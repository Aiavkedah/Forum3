using System;
using System.Web.Mvc;
using Forum.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Forum.Models;
using System.Threading.Tasks;

namespace ForumTest
{
    [TestClass]
    public class UnitTest1
    {
        public HomeController Controller;

        [TestInitialize]
        public void TestInitialize()
        {
            Controller = new HomeController
            {
                Db = InitTests.ForumContext()
            };
        }

        [TestMethod]
        public async Task IndexGet()
        {
            ViewResult result = await Controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void IndexPost()
        {
            ForumCategory category = new ForumCategory { Text = "NewCategory" };
            RedirectToRouteResult result = Controller.Index(category) as RedirectToRouteResult;
            ForumCategory newCategory = Controller.Db.ForumCategories.Find(category.ID);

            Assert.IsNotNull(result);
            Assert.IsNotNull(newCategory);
            Assert.AreEqual(category.Text, newCategory.Text);
        }

        [TestMethod]
        public void PostsGet()
        {
            ViewResult result = Controller.Posts(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Category1", result.ViewBag.PostsCategory);
            Assert.AreEqual(1, result.ViewBag.ForumCategoryId);
        }


        [TestMethod]
        public void PostsPost()
        {
            ForumPost post = new ForumPost { ForumCategoryId = 1, ForumUserId = 1, Text = "NewPost" };
            RedirectToRouteResult result = Controller.Posts(post) as RedirectToRouteResult;
            ForumPost newPost = Controller.Db.ForumPosts.Find(post.ID);

            Assert.IsNotNull(result);
            Assert.IsNotNull(newPost);
            Assert.AreEqual(post.Text, newPost.Text);
        }

        [TestMethod]
        public void PostGet()
        {
            ViewResult result = Controller.Post(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Title Forum1 Category1", result.ViewBag.PostTitle);
            Assert.AreEqual(1, result.ViewBag.ForumCategoryId);
            Assert.AreEqual(1, result.ViewBag.ForumPostId);
        }


        [TestMethod]
        public void PostPost()
        {
            ForumComment comment = new ForumComment { ForumPostId = 1, ForumUserId = 1, Text = "NewComment", Date = DateTime.Now };
            ViewResult result = Controller.Post(comment) as ViewResult;
            ForumComment newComment = Controller.Db.ForumComments.Find(comment.ID);

            Assert.IsNotNull(result);
            Assert.IsNotNull(newComment);
            Assert.AreEqual(comment.Text, newComment.Text);
            Assert.AreEqual(comment.ForumPostId, result.ViewBag.PostId);
        }

        [TestMethod]
        public void Delete()
        {
            ForumCategory category = new ForumCategory { Text = "NewCategory" };
            //ForumPost post = new ForumPost { ForumCategoryId = 1, ForumUserId = 1, Title = "NewPost" };
            //ForumComment comment = new ForumComment { ForumPostId = 1, ForumUserId = 1, Comment = "NewComment", Date = DateTime.Now };
            ViewResult result = Controller.Delete(1, category.GetType().ToString()) as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.IsNull(Controller.Db.ForumComments.Find(category.ID));
        }
    }
}
