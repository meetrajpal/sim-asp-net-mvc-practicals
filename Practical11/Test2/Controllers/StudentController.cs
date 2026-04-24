using System.Web.Mvc;
using Test2.Models.Entities;
using Test2.Models.Repositories;
using Test2.Models.Services;

namespace Test2.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            StudentService studentService = new StudentService(new Repository<Student>());
            return View(studentService.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student model)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var service = new StudentService(new Repository<Student>());
            service.Create(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var service = new StudentService(new Repository<Student>());
            var existing = service.GetById(model.Id);
            if (existing == null)
                return RedirectToAction("Index");

            service.Update(model);
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