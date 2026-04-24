using System;
using System.Web.Mvc;
using System.Web.UI;

namespace Test3.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(CacheProfile = "CacheFor5Mins", Location = OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            ViewBag.Time = DateTime.Now;
            return View();
        }
    }
}