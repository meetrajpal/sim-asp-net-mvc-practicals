using System.Web.Mvc;

namespace Test3.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Error";
            return View("Error");
        }
    }
}