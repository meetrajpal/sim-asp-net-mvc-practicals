using System.Web.Mvc;
using Test2.Models.Repositories;
using Test2.Models.Services;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public HomeController()
        {
            _employeeService = new EmployeeService(new EmployeeRepository());
        }

        public ActionResult Index()
        {
            return View(_employeeService.GetAll());
        }
    }
}