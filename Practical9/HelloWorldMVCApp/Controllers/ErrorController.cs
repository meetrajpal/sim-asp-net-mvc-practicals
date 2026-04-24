using System.Web.Mvc;

namespace HelloWorldMVCApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("Error");
        }
    }
}