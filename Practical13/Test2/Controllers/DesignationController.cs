using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Test2.Models.Data;
using Test2.Models.Entities;
using Test2.Models.Repositories;
using Test2.Models.Services;

namespace Test2.Controllers
{
    public class DesignationController : Controller
    {
        private readonly DesignationService _designationService;

        public DesignationController()
        {
            _designationService = new DesignationService(
                new DesignationRepository(new AppDbContext()));
        }

        public ActionResult Index()
        {
            return View(_designationService.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Designation model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _designationService.Create(model);
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
                return View(_designationService.GetById(id));
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Designation model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _designationService.Update(model);
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
                _designationService.Delete(id);
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