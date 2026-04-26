using System.Web.Mvc;
using Test2.Models.Data;
using Test2.Models.Repositories;
using Test2.Models.Services;

namespace Test2.Controllers
{
    [Authorize]
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