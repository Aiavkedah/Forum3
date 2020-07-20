using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Forum.Models;
using Moq;

namespace ForumTest
{
    public class Tests
    {
        public Mock<ApplicationDbContext> MockContext    = new Mock<ApplicationDbContext>();
        public Mock<DbSet<ForumCategory>> MockCategories = new Mock<DbSet<ForumCategory>>();
        public Mock<DbSet<ForumPost>>     MockPosts      = new Mock<DbSet<ForumPost>>();
        public Mock<DbSet<ForumComment>>  MockComments   = new Mock<DbSet<ForumComment>>();
        public Mock<DbSet<ApplicationUser>> MockUsers    = new Mock<DbSet<ApplicationUser>>();
        public Mock<ControllerContext>  ControllerContextMock = new Mock<ControllerContext>();

        public void InitData()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", UserName = "Admin" },
                new ApplicationUser { Id = "2", UserName = "User" }
            }.AsQueryable();

            var categories = new List<ForumCategory>
            {
                new ForumCategory { ID = 1, Text = "Category1" },
                new ForumCategory { ID = 2, Text = "Category2" }
            }.AsQueryable();

            var posts = new List<ForumPost>
            {
                new ForumPost { ID = 1, ForumCategoryId = 1, ForumUserId = "1", Text = "Title Forum1 Category1", ForumCategory = new ForumCategory { ID = 1, Text = "Category1" }, ApplicationUser = new ApplicationUser { Id = "1", UserName = "Admin" } },
                new ForumPost { ID = 2, ForumCategoryId = 1, ForumUserId = "1", Text = "Title Forum2 Category1", ForumCategory = new ForumCategory { ID = 1, Text = "Category1" }, ApplicationUser = new ApplicationUser { Id = "1", UserName = "Admin" } },
                new ForumPost { ID = 3, ForumCategoryId = 2, ForumUserId = "1", Text = "Title Forum3 Category2", ForumCategory = new ForumCategory { ID = 2, Text = "Category2" }, ApplicationUser = new ApplicationUser { Id = "1", UserName = "Admin" } }
            }.AsQueryable();

            var comments = new List<ForumComment>
            {
                new ForumComment { ID = 1, ForumPostId = 1, ForumUserId = "1", Text = "Comment1 Forum1", Date = DateTime.Now, ForumPost = new ForumPost { ID = 1, ForumCategoryId = 1, Text = "Title Forum1 Category1" }, ApplicationUser = new ApplicationUser { Id = "1", UserName = "Admin" } },
                new ForumComment { ID = 2, ForumPostId = 2, ForumUserId = "1", Text = "Comment2 Forum1", Date = DateTime.Now, ForumPost = new ForumPost { ID = 1, ForumCategoryId = 1, Text = "Title Forum2 Category1" }, ApplicationUser = new ApplicationUser { Id = "1", UserName = "Admin" } },
                new ForumComment { ID = 3, ForumPostId = 2, ForumUserId = "1", Text = "Comment3 Forum2", Date = DateTime.Now, ForumPost = new ForumPost { ID = 1, ForumCategoryId = 1, Text = "Title Forum2 Category1" }, ApplicationUser = new ApplicationUser { Id = "1", UserName = "Admin" } }
            }.AsQueryable();

            MockCategories.As<IQueryable<ForumCategory>>().Setup(i => i.Provider).Returns(categories.Provider);
            MockCategories.As<IQueryable<ForumCategory>>().Setup(i => i.Expression).Returns(categories.Expression);
            MockCategories.As<IQueryable<ForumCategory>>().Setup(i => i.ElementType).Returns(categories.ElementType);
            MockCategories.As<IQueryable<ForumCategory>>().Setup(i => i.GetEnumerator()).Returns(categories.GetEnumerator());

            MockPosts.As<IQueryable<ForumPost>>().Setup(i => i.Provider).Returns(posts.Provider);
            MockPosts.As<IQueryable<ForumPost>>().Setup(i => i.Expression).Returns(posts.Expression);
            MockPosts.As<IQueryable<ForumPost>>().Setup(i => i.ElementType).Returns(posts.ElementType);
            MockPosts.As<IQueryable<ForumPost>>().Setup(i => i.GetEnumerator()).Returns(posts.GetEnumerator());

            MockComments.As<IQueryable<ForumComment>>().Setup(i => i.Provider).Returns(comments.Provider);
            MockComments.As<IQueryable<ForumComment>>().Setup(i => i.Expression).Returns(comments.Expression);
            MockComments.As<IQueryable<ForumComment>>().Setup(i => i.ElementType).Returns(comments.ElementType);
            MockComments.As<IQueryable<ForumComment>>().Setup(i => i.GetEnumerator()).Returns(comments.GetEnumerator());

            MockUsers.As<IQueryable<ApplicationUser>>().Setup(i => i.Provider).Returns(users.Provider);
            MockUsers.As<IQueryable<ApplicationUser>>().Setup(i => i.Expression).Returns(users.Expression);
            MockUsers.As<IQueryable<ApplicationUser>>().Setup(i => i.ElementType).Returns(users.ElementType);
            MockUsers.As<IQueryable<ApplicationUser>>().Setup(i => i.GetEnumerator()).Returns(users.GetEnumerator());

            MockContext.Setup(i => i.ForumCategories).Returns(MockCategories.Object);
            MockContext.Setup(i => i.ForumCategories.Find(It.IsAny<int>())).Returns(categories.First());
            MockContext.Setup(i => i.ForumPosts).Returns(MockPosts.Object);
            MockContext.Setup(i => i.ForumPosts.Find(It.IsAny<int>())).Returns(posts.First());
            MockContext.Setup(i => i.ForumComments).Returns(MockComments.Object);
            MockContext.Setup(i => i.ForumComments.Find(It.IsAny<int>())).Returns(comments.First());
            MockContext.Setup(i => i.Users).Returns(MockUsers.Object);

            var mockIdentity = new Mock<ClaimsIdentity>();
            mockIdentity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(new Claim("", "1"));

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(i => i.Identity).Returns(mockIdentity.Object);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(i => i.User).Returns(userMock.Object);

            ControllerContextMock.SetupGet(i => i.HttpContext).Returns(contextMock.Object);
        }
    }
}
