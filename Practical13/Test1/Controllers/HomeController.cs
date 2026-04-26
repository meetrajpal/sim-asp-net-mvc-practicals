using System.Web.Mvc;
using Test1.Models.Data;
using Test1.Models.Repositories;
using Test1.Models.Services;

namespace Test1.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public HomeController()
        {
            _employeeService = new EmployeeService(new EmployeeRepository(new AppDbContext()));
        }

        public ActionResult Index()
        {
            return View(_employeeService.GetAll());
        }
    }
}