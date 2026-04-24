using System.Web.Mvc;

namespace Test4.Controllers
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