using System.Web.Mvc;
using Test2.Models.Data;
using Test2.Models.Repositories;
using Test2.Models.Services;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly DesignationService _designationService;
        public HomeController()
        {
            var context = new AppDbContext();
            _employeeService = new EmployeeService(new EmployeeRepository(context));
            _designationService = new DesignationService(new DesignationRepository(context));
        }

        public ActionResult Index()
        {
            return View(_employeeService.GetAll());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}