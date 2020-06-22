using Forum.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ForumTests
{
    public class HomeControllerTest
    {
        private HomeController controller;
        private Task<ActionResult> result;

        [OneTimeSetUp]
        public void SetupContext()
        {
            controller = new HomeController();
            result = controller.Index();
        }

        [Test]
        public void IndexViewResultNotNull()
        {
            Assert.IsNotNull(result);
        }

        [Test]
        public void Test1()
        {
            //Assert.Pass();
        }
    }
}