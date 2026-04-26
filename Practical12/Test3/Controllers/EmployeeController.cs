using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Test3.Models.Entities;
using Test3.Models.Repositories;
using Test3.Models.Services;

namespace Test3.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly DesignationService _designationService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService(new EmployeeRepository());
            _designationService = new DesignationService(new DesignationRepository());
        }

        public ActionResult Index()
        {
            return View(_employeeService.GetAll());
        }


        public ActionResult Create()
        {
            ViewBag.Designations = new SelectList(_designationService.GetAll(), "Id", "DesignationName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Designations = new SelectList(_designationService.GetAll(), "Id", "DesignationName");
                return View(model);
            }

            try
            {
                _employeeService.Create(model);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Designations = new SelectList(_designationService.GetAll(), "Id", "DesignationName");
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var employee = _employeeService.GetById(id);
                ViewBag.Designations = new SelectList(_designationService.GetAll(), "Id", "DesignationName", employee.DesignationId);
                return View(employee);
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Designations = new SelectList(_designationService.GetAll(), "Id", "DesignationName", model.DesignationId);
                return View(model);
            }

            try
            {
                _employeeService.Update(model);
                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Designations = new SelectList(_designationService.GetAll(), "Id", "DesignationName", model.DesignationId);
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _employeeService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Summary()
        {
            return PartialView(_employeeService.GetEmployeeSummary());
        }

        public ActionResult ByDOB()
        {
            return PartialView("Index", _employeeService.GetAllOrderedByDOB());
        }

        public ActionResult ByDesignation()
        {
            ViewBag.Designations = new SelectList(_designationService.GetAll(), "Id", "DesignationName");
            return PartialView();
        }

        [HttpPost]
        public ActionResult ByDesignation(int designationId)
        {
            try
            {
                var employees = _employeeService.GetByDesignationId(designationId);
                ViewBag.Designations = new SelectList(_designationService.GetAll(), "Id", "DesignationName", designationId);
                return PartialView(employees);
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult MaxSalary()
        {
            try
            {
                return PartialView(_employeeService.GetMaxSalaryEmployee());
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}