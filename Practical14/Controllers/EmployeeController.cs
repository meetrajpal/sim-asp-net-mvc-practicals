using Practical14.Models.Data;
using Practical14.Models.Repositories;
using Practical14.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Practical14.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService(
                new EmployeeRepository(new Practical14DBEntities()));
        }


        public ActionResult Index(string keyword = "", int page = 1, int pageSize = 10)
        {
            var allRecords = string.IsNullOrWhiteSpace(keyword)
                ? _employeeService.GetAll().ToList()
                : _employeeService.Search(keyword).ToList();

            var totalRecords = allRecords.Count();

            var records = allRecords
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Keyword = keyword;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Total = totalRecords;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return View(records);
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
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
    }
}