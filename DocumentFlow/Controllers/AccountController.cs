using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using DocumentFlow.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Net.Mail;
using BL.PositionsHandler;
using EntityModels;
using BL.UsersHandlers;
using BL.AbstractClasses;
using System.Web.Helpers;
using System.Net;
using System.Web.Security;

namespace DocumentFlow.Controllers
{
    public class AccountController : Controller
    {
        protected static UserProvider _currentUserProvider;

        public static string FullName;

        protected static PositionsHandler _positionsHandler = new PositionsHandler();
        protected static RepositoryHandler<User> _usersHandler = new UsersRepositoryHandler();

        protected IAuthentication _customAuthentification = new CustomAuthentication();


        #region Registration
        public ActionResult Register()
        {
            ViewBag.Positions = _positionsHandler.PositionsSelectList();
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                var user = _customAuthentification.Register(model);
                if (user != null)
                {
                    return RedirectToAction("Login");
                }
            }

            ViewBag.Positions = _positionsHandler.PositionsSelectList();
            return View(model);
        }
        #endregion

        #region Login

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                _customAuthentification.LogOut();
                var user = _customAuthentification.Login(loginModel);

                if (user != null)
                {
                    _currentUserProvider = _customAuthentification.GetUserProvider;
                    FullName = user.FirstName + " " + user.LastName;

                    if (await _currentUserProvider.IsInRole("Admin"))
                    {
                        return RedirectToAction("Roles", "Admin");
                    }
                    return RedirectToAction("Index", "Main");
                }
            }
            ModelState.AddModelError("", "Неверный логин или пароль");
            return View();
        }

        public ActionResult Logoff()
        {
            IAuthentication custom = new CustomAuthentication();
            custom.LogOut();

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region DeleteUser
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DeleteConfirmed()
        {
            var user = await _currentUserProvider.GetUser();
            if (user != null)
            {
                _usersHandler.Remove(user);
                _customAuthentification.LogOut();

                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Index", "Main");
        }
        #endregion

        #region EditUser
        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            var user = await _currentUserProvider.GetUser();

            if (user != null)
            {
                EditUserModel model = new EditUserModel
                {
                    Login = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    Email = user.Email,
                    PositionId = user.PositionId
                };
                ViewBag.Positions = _positionsHandler.PositionsSelectList();
                return View(model);
            }
            _customAuthentification.LogOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditUserModel model)
        {
            var user = await _currentUserProvider.GetUser();

            if (user != null)
            {
                user.Email = model.Email;
                user.UserName = model.Login;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Patronymic = model.Patronymic;
                user.PositionId = model.PositionId;

                FullName = model.FirstName + " " + model.LastName;

                _usersHandler.Update(user);
                return RedirectToAction("Index", "Main");
            }
            else
            {
                ModelState.AddModelError("", "Не удалось найти пользователя. Попробуйте еще раз.");
                return RedirectToAction("Index", "Main");
            }
        }
        #endregion

        #region EditPassword
        [HttpGet]
        public ActionResult EditPassword()
        {
            return View("EditPassword");
        }

        [HttpPost]
        public async Task<ActionResult> EditPassword(EditPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _currentUserProvider.GetUser();
                if (user != null)
                {
                    user.Password = model.NewPassword;
                    _usersHandler.Update(user);

                    _customAuthentification.LogOut();
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неверный пароль");
            }
            return View(model);
        }
        #endregion
    }
}
