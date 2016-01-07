using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using BL.Handlers.PositionsHandlers;
using BL.Handlers.RolesHandlers;
using BL.Handlers.UsersHandlers;
using DocumentFlow.Models;
using DocumentFlow.Models.AuthorizeAttribute;
using BlModels = BL.Models;

namespace DocumentFlow.Controllers
{
    public class AccountController : Controller
    {
        private readonly PositionsHandler _positionsHandler = new PositionsHandler();
        private readonly PositionsRepositoryHandler _positionsRepositoryHandler = new PositionsRepositoryHandler();
        private readonly RolesRepositoryHandler _rolesRepositoryHandler = new RolesRepositoryHandler();

        private readonly UsersRepositoryHandler _usersRepositoryHandler = new UsersRepositoryHandler();
        private RolesHandler _rolesHandler = new RolesHandler();

        public AccountController()
        {
            Mapper.CreateMap<User, BlModels.User>();
            Mapper.CreateMap<BlModels.User, User>();

            Mapper.CreateMap<Position, BlModels.Position>();
            Mapper.CreateMap<BlModels.Position, Position>();

            Mapper.CreateMap<Role, BlModels.Role>();
            Mapper.CreateMap<BlModels.Role, Role>();
        }

        #region Registration

        public ActionResult Register()
        {
            ViewBag.Positions = _positionsHandler.PositionsSelectList();
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = Mapper.Map<BlModels.User, User>
                (_usersRepositoryHandler.GetUserByEmail(model.Email));


            if (user == null)
            {
                _usersRepositoryHandler.Add(
                    new BlModels.User
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Patronymic = model.Patronymic,
                        Role = _rolesRepositoryHandler.FindByName("User"),
                        Position = _positionsRepositoryHandler.FindById(model.PositionId),
                        Password = model.Password,
                        UserName = model.Email
                    });

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Пользователь с такой почтой уже существует");
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
        public ActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid) return View(loginModel);

            var user = UserController.ConvertToModel(
                _usersRepositoryHandler.GetUserByEmailPassword(loginModel.Email, loginModel.Password));

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(loginModel.Email, false);

                HttpContext.Response.Cookies["Role"].Value = user.Role.Name;
                HttpContext.Response.Cookies["Email"].Value = user.Email;


                return user.Role.Name == "Admin" ? RedirectToAction("Roles", "Role") : RedirectToAction("Index", "Main");
            }

            ModelState.AddModelError("", "Неверный логин или пароль");

            return View(loginModel);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}