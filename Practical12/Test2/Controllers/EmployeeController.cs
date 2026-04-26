using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Test2.Models.Entities;
using Test2.Models.Repositories;
using Test2.Models.Services;

namespace Test2.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService(new EmployeeRepository());
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _employeeService.Create(model);
                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        public ActionResult Edit(int id)
        {
            try
            {
                return View(_employeeService.GetById(id));
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _employeeService.Update(model);
                return RedirectToAction("Index", "Home");
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _employeeService.Delete(id);
                return RedirectToAction("Index", "Home");
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult TotalSalary()
        {
            try
            {
                TempData["Output"] = $"Total Salary: {_employeeService.TotalSalary()}";
                return RedirectToAction("Index", "Home");
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult DOBLT112000()
        {
            try
            {
                var employees = _employeeService.DOBLT112000();
                return View("~/Views/Home/Index.cshtml", employees);
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult MiddleNameNull()
        {
            try
            {
                var employees = _employeeService.NullMiddleName();
                return View("~/Views/Home/Index.cshtml", employees);
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
