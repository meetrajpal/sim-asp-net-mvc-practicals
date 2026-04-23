using HelloWorldMVCApp.Controllers;
using System.Web.Mvc;
using Xunit;

namespace HelloWorldMVCApp.Tests
{
    public class HomeControllerTests
    {
        /// <summary>
        /// This method verifies the Test3 action of HomeController when called returns a ViewResult.
        /// </summary>
        [Fact]
        public void Test3_WhenCalled_ReturnsViewResult()
        {

            var controller = new HomeController();


            var result = controller.Test3();


            Assert.IsType<ViewResult>(result);
        }

        /// <summary>
        /// This method verifies that the Test3 action sets the ViewBag.Message property to string "Hello World".
        /// </summary>
        [Fact]
        public void Test3_WhenCalled_SetsHelloWorldMessage()
        {

            var controller = new HomeController();


            var result = controller.Test3() as ViewResult;


            Assert.Equal("Hello World", result.ViewBag.Message);
        }
    }
}
