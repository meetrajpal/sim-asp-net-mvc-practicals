using HelloWorldMVCApp.Controllers;
using System.Web.Mvc;
using Xunit;

namespace HelloWorldMVCApp.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Test3_WhenCalled_ReturnsViewResult()
        {

            var controller = new HomeController();


            var result = controller.Test3();


            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void Test3_WhenCalled_SetsHelloWorldMessage()
        {

            var controller = new HomeController();


            var result = controller.Test3() as ViewResult;


            Assert.Equal("Hello World", result.ViewBag.Message);
        }
    }
}
