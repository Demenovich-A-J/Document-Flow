using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using BL.Handlers.PositionsHandlers;
using BL.Handlers.UsersHandlers;
using DocumentFlow.Models;
using DocumentFlow.Models.AuthorizeAttribute;
using BlModels = BL.Models;

namespace DocumentFlow.Controllers
{
    public class ManageController : Controller
    {
        private readonly PositionsHandler _positionsHandler = new PositionsHandler();
        private readonly PositionsRepositoryHandler _positionsRepositoryHandler = new PositionsRepositoryHandler();
        private readonly UsersRepositoryHandler _usersRepositoryHandler = new UsersRepositoryHandler();

        public ManageController()
        {
            Mapper.CreateMap<User, BlModels.User>();
            Mapper.CreateMap<BlModels.User, User>();

            Mapper.CreateMap<Position, BlModels.Position>();
            Mapper.CreateMap<BlModels.Position, Position>();

            Mapper.CreateMap<Role, BlModels.Role>();
            Mapper.CreateMap<BlModels.Role, Role>();
        }

        public static User ConvertToModel(BlModels.User user)
        {
            Mapper.CreateMap<BlModels.User, User>();
            Mapper.CreateMap<BlModels.Position, Position>();
            Mapper.CreateMap<BlModels.Role, Role>();

            return Mapper.Map<BlModels.User, User>(user);
        }

        public static BlModels.User ConvertToBlModel(User user)
        {
            Mapper.CreateMap<User, BlModels.User>();
            Mapper.CreateMap<Position, BlModels.Position>();
            Mapper.CreateMap<Role, BlModels.Role>();

            return Mapper.Map<User, BlModels.User>(user);
        }

        #region DeleteUser

        [HttpGet]
        [CustomAuthorize("User")]
        public ActionResult Delete()
        {
            ViewBag.FullName = GetUserName();
            return View();
        }

        [HttpGet]
        [CustomAuthorize("User")]
        public ActionResult DeleteConfirmed()
        {
            var user = _usersRepositoryHandler.GetUserByEmail(FormsAuthentication.Decrypt
                (Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);

            if (user == null) return RedirectToAction("Index", "Main");

            _usersRepositoryHandler.Remove(user);
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region EditUser

        [CustomAuthorize("User")]
        public string GetUserName()
        {
            var user = GetUser();

            var fullName = "";
            if (user != null)
            {
                fullName = user.LastName + " " + user.FirstName + " " + user.Patronymic;
            }
            return fullName;
        }

        [CustomAuthorize("User")]
        private BlModels.User GetUser()
        {
            var userName = FormsAuthentication.Decrypt
                (Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var user = _usersRepositoryHandler.GetUserByEmail(userName);
            return user;
        }

        [HttpGet]
        [CustomAuthorize("User")]
        public ActionResult Edit()
        {
            var user = GetUser();

            if (user != null)
            {
                var model = new EditUserModel
                {
                    Login = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    Email = user.Email,
                    PositionId = user.Position.Id
                };
                ViewBag.Positions = _positionsHandler.PositionsSelectList();
                ViewBag.FullName = GetUserName();
                return View(model);
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [CustomAuthorize("User")]
        public ActionResult Edit(EditUserModel model)
        {
            var user = GetUser();

            if (user != null)
            {
                user.Email = model.Email;
                user.UserName = model.Login;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Patronymic = model.Patronymic;
                user.Position = _positionsRepositoryHandler.FindById(model.PositionId);

                _usersRepositoryHandler.Update(user);
                return RedirectToAction("Index", "Main");
            }
            ModelState.AddModelError("", "Не удалось найти пользователя. Попробуйте еще раз.");
            return RedirectToAction("Index", "Main");
        }

        #endregion

        #region EditPassword

        [HttpGet]
        [CustomAuthorize("User")]
        public ActionResult EditPassword()
        {
            ViewBag.FullName = GetUserName();
            return View();
        }

        [HttpPost]
        [CustomAuthorize("User")]
        public ActionResult EditPassword(EditPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _usersRepositoryHandler.GetUserByEmail(FormsAuthentication.Decrypt
                    (Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                if (user == null) return View(model);

                user.Password = model.NewPassword;
                _usersRepositoryHandler.Update(user);

                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
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