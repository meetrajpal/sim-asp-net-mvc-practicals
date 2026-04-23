using System.Web.Mvc;

namespace Test1.Controllers
{
    [RoutePrefix("employee-details")]
    public class EmployeeController : Controller
    {
        [Route("~/")]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("{name}")]
        public ActionResult Details(string name)
        {
            ViewBag.Name = name;
            return View();
        }
    }
}