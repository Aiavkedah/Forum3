using System;
using System.Web.Mvc;
using Forum.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Forum.Models;
using Moq;

namespace ForumTest
{
    [TestClass]
    public class ForumTests : Tests
    {
        public HomeController Controller;
        public PostController Post;
        public CommentController Comment;

        [TestInitialize]
        public void TestInitialize()
        {
            InitData();

            Controller = new HomeController
            {
                Db = MockContext.Object,
                ControllerContext = ControllerContextMock.Object
            };
            Post = new PostController
            {
                Db = MockContext.Object,
                ControllerContext = ControllerContextMock.Object
            };
            Comment = new CommentController
            {
                Db = MockContext.Object,
                ControllerContext = ControllerContextMock.Object
            };
        }

        [TestMethod]
        public void IndexGet()
        {
            ViewResult result = Controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);

            MockContext.Verify(i => i.ForumCategories, Times.Once());
            MockContext.Verify(i => i.ForumPosts, Times.Once());
        }

        [TestMethod]
        public void IndexPost()
        {
            ForumCategory category = new ForumCategory { Text = "NewCategory" };
            RedirectToRouteResult result = Controller.Index(category) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            MockCategories.Verify(i => i.Add(It.IsAny<ForumCategory>()), Times.Once());
            MockContext.Verify(i => i.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void PostGet()
        {
            ViewResult result = Post.Posts(1) as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("Category1", result.ViewBag.PostsCategory);
            Assert.AreEqual(1, result.ViewBag.ForumCategoryId);
        }

        [TestMethod]
        public void PostPost()
        {
            ForumPost post = new ForumPost { ForumCategoryId = 1, ForumUserId = "1", Text = "NewPost" };
            RedirectToRouteResult result = Post.Posts(post) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            MockPosts.Verify(i => i.Add(It.IsAny<ForumPost>()), Times.Once());
            MockContext.Verify(i => i.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void CommentGet()
        {
            ViewResult result = Comment.Comments(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Title Forum1 Category1", result.ViewBag.PostTitle);
            Assert.AreEqual(1, result.ViewBag.ForumCategoryId);
            Assert.AreEqual(1, result.ViewBag.ForumPostId);
        }


        [TestMethod]
        public void CommentPost()
        {
            ForumComment comment = new ForumComment { ForumPostId = 1, ForumUserId = "1", Text = "NewComment", Date = DateTime.Now };
            ViewResult result = Comment.Comments(comment) as ViewResult;

            Assert.IsNotNull(result);
            MockComments.Verify(i => i.Add(It.IsAny<ForumComment>()), Times.Once());
            MockContext.Verify(i => i.SaveChanges(), Times.Once());
        }

        /*[TestMethod]
        public void DeleteCategory()
        {
            ForumCategory category = new ForumCategory { Text = "NewCategory" };
            ViewResult result = Controller.Delete(1, category.GetType().ToString()) as ViewResult;
            var confirmed = Controller.DeleteConfirmed(1, category.GetType().ToString(), "");

            Assert.IsNotNull(result);
            Assert.IsNotNull(confirmed);
            MockContext.Verify(i => i.Entry<ForumCategory>(It.IsAny<ForumCategory>()), Times.Once());
            MockContext.Verify(i => i.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void DeletePost()
        {
            ForumPost post = new ForumPost { ForumCategoryId = 1, ForumUserId = "1", Text = "NewPost" };
            ViewResult result = Controller.Delete(1, post.GetType().ToString()) as ViewResult;
            var confirmed = Controller.DeleteConfirmed(1, post.GetType().ToString(), "");

            Assert.IsNotNull(result);
            Assert.IsNotNull(confirmed);
            MockPosts.Verify(i => i.Remove(It.IsAny<ForumPost>()), Times.Once());
            MockContext.Verify(i => i.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void DeleteComment()
        {
            ForumComment comment = new ForumComment { ForumPostId = 1, ForumUserId = "1", Text = "NewComment", Date = DateTime.Now };
            ViewResult result = Controller.Delete(1, comment.GetType().ToString()) as ViewResult;
            var confirmed = Controller.DeleteConfirmed(1, comment.GetType().ToString(), ""
                );

            Assert.IsNotNull(result);
            Assert.IsNotNull(confirmed);
            MockComments.Verify(i => i.Remove(It.IsAny<ForumComment>()), Times.Once());
            MockContext.Verify(i => i.SaveChanges(), Times.Once());
        }*/
    }
}
