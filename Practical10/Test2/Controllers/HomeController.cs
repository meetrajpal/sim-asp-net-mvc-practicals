using System.Collections.Generic;
using System.Web.Mvc;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult ContentResult()
        {
            return new ContentResult() { Content = "Simple Content Returned.", ContentType = "text/plain" };
        }

        public FileContentResult FileContentResult()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/App_Data/Files/Sample.pdf"));

            var fileResult = new FileContentResult(fileBytes, "application/pdf")
            {
                FileDownloadName = "SampleFile.pdf"
            };

            return fileResult;
        }

        public EmptyResult EmptyResult()
        {
            return new EmptyResult();
        }

        public JavaScriptResult JavaScriptResult()
        {
            return new JavaScriptResult() { Script = "alert('JS code returned from an action.')" };
        }

        public JsonResult JsonResult()
        {
            var products = new List<object>
            {
                new { name = "abc", price = 123 },
                new { name = "pqr", price = 456 }
            };

            return new JsonResult()
            {
                Data = Json(products),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}