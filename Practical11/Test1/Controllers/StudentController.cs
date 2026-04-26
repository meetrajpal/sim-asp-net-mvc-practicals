using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Test1.Models.Entities;
using Test1.Models.Repositories;
using Test1.Models.Services;

namespace Test1.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;

        public StudentController()
        {
            _studentService = new StudentService(new Repository<Student>());
        }

        public ActionResult Index()
        {
            return View(_studentService.GetAll());
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

            try
            {
                _studentService.Create(model);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            try
            {
                return View(_studentService.GetById(id));
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _studentService.Update(model);
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _studentService.Delete(id);
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}