using System;
using System.Web.Mvc;
using System.Web.Security;
using Test2.models.Repositories;
using Test2.models.Services;
using Test2.models.ViewModels;
using Test2.Models.Data;

namespace Test2.Controllers
{

    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController()
        {
            _authService = new AuthService(new UserRepository(new AppDbContext()));
        }

        [AllowAnonymous]

        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AllowAnonymous]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                if (_authService.Login(model))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [AllowAnonymous]

        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _authService.Register(model);
                return RedirectToAction("Login");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}