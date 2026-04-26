using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Test2.Models.Data;
using Test2.Models.Entities;
using Test2.Models.Repositories;
using Test2.Models.Services;

namespace Test2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly DesignationService _designationService;

        public EmployeeController()
        {
            var context = new AppDbContext();
            _employeeService = new EmployeeService(new EmployeeRepository(context));
            _designationService = new DesignationService(new DesignationRepository(context));
        }

        public ActionResult Index()
        {
            return View(_employeeService.GetAll());
        }

        public ActionResult Create()
        {
            ViewBag.DesignationId = new SelectList(
                _designationService.GetAll(), "Id", "DesignationName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Designations = new SelectList(
                    _designationService.GetAll(), "Id", "DesignationName");
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
                ViewBag.Designations = new SelectList(
                    _designationService.GetAll(), "Id", "DesignationName");
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var employee = _employeeService.GetById(id);
                ViewBag.Designations = new SelectList(
                    _designationService.GetAll(), "Id", "DesignationName", employee.DesignationId);
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
                ViewBag.Designations = new SelectList(
                    _designationService.GetAll(), "Id", "DesignationName", model.DesignationId);
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
                ViewBag.Designations = new SelectList(
                    _designationService.GetAll(), "Id", "DesignationName", model.DesignationId);
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

        public ActionResult CountByDesignation()
        {
            return PartialView(_employeeService.GetCountByDesignation());
        }
    }
}