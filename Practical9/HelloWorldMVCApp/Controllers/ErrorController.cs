using System.Web.Mvc;

namespace HelloWorldMVCApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Error";
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}