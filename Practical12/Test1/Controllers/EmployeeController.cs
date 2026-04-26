using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Test1.Models.Entities;
using Test1.Models.Repositories;
using Test1.Models.Services;

namespace Test1.Controllers
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

        public ActionResult Query3()
        {
            try
            {
                _employeeService.ExecuteQuery3Task();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Query4()
        {
            try
            {
                _employeeService.ExecuteQuery4Task();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Query5()
        {
            try
            {
                _employeeService.ExecuteQuery5Task();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Query6()
        {
            try
            {
                _employeeService.ExecuteQuery6Task();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
