using System.Web.Mvc;

namespace HelloWorldMVCApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test1()
        {
            ViewBag.Message = "Hello World";
            return View();
        }

        public ActionResult Test2()
        {
            return View();
        }

        public ActionResult Test3()
        {
            ViewBag.Message = "Hello World";
            return View();
        }
    }
}