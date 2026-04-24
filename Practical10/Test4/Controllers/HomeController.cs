using System.Web.Mvc;
using Test4.Models.Filters;
using Test4.Models.Models;
using Test4.Models.Services;

namespace Test4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [HandleError(View = "Error")]
        [CustomDivideByZeroFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Calculator model, string reset)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else if (!string.IsNullOrEmpty(reset))
            {
                ModelState.Clear();
                return View(new Calculator());
            }

            CalculatorService calculatorService = new CalculatorService();
            calculatorService.Calculate(model);
            ModelState.Clear();
            return View(model);
        }
    }
}