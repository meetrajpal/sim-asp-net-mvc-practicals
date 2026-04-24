using System.Web.Mvc;
using Test1.Models.Entities;
using Test1.Models.Repositories;
using Test1.Models.Services;

namespace Test1.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            StudentService studentService = new StudentService(new Repository<Student>());
            return View(studentService.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StudentService studentService = new StudentService(new Repository<Student>());
            studentService.Create(model);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            StudentService studentService = new StudentService(new Repository<Student>());
            return View(studentService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StudentService studentService = new StudentService(new Repository<Student>());
            studentService.Update(model);


            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            StudentService studentService = new StudentService(new Repository<Student>());
            studentService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}