using System.Web.Mvc;

namespace HelloWorldMVCApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Error";

            ViewBag.ErrorMessage = Session?["ErrorMessage"];
            Session?.Remove("ErrorMessage");

            return View("Error");
        }
    }
}