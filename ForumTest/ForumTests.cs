using System;
using System.Web.Mvc;
using Forum.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Forum.Models;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Security.Claims;

namespace ForumTest
{
    [TestClass]
    public class ForumTests : Tests
    {
        public HomeController Controller;
        
        [TestInitialize]
        public void TestInitialize()
        {
            InitData();

            Controller = new HomeController
            {
                Db = MockContext.Object
            };
        }

        [TestMethod]
        public void IndexGet()
        {
            ViewResult result = Controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);

            MockContext.Verify(i => i.ForumCategories, Times.Once());
            MockContext.Verify(i => i.ForumPosts, Times.Once());

            MockCategories.Verify();
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
        public void PostsGet()
        {
            var identityMock = new Mock<ClaimsIdentity>();
            identityMock.Setup(p => p.FindFirst(It.IsAny<string>())).Returns(new Claim("foo", "1"));

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity).Returns(identityMock.Object);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                       .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            Controller.ControllerContext = controllerContextMock.Object;
            ViewResult result = Controller.Posts(1) as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("Category1", result.ViewBag.PostsCategory);
            Assert.AreEqual(1, result.ViewBag.ForumCategoryId);
        }


        [TestMethod]
        public void PostsPost()
        {
            ForumPost post = new ForumPost { ForumCategoryId = 1, ForumUserId = "1", Text = "NewPost" };
            RedirectToRouteResult result = Controller.Posts(post) as RedirectToRouteResult;
            ForumPost newPost = Controller.Db.ForumPosts.Find(post.ID);

            Assert.IsNotNull(result);
            Assert.IsNotNull(newPost);
            Assert.AreEqual(post.Text, newPost.Text);
        }

        [TestMethod]
        public void PostGet()
        {
            ViewResult result = Controller.Comments(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Title Forum1 Category1", result.ViewBag.PostTitle);
            Assert.AreEqual(1, result.ViewBag.ForumCategoryId);
            Assert.AreEqual(1, result.ViewBag.ForumPostId);
        }


        [TestMethod]
        public void PostPost()
        {
            ForumComment comment = new ForumComment { ForumPostId = 1, ForumUserId = "1", Text = "NewComment", Date = DateTime.Now };
            ViewResult result = Controller.Comments(comment) as ViewResult;
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
