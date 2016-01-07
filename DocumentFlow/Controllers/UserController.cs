using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BL.Handlers.PositionsHandlers;
using BL.Handlers.RolesHandlers;
using BL.Handlers.UsersHandlers;
using BL.Interfaces;
using BL.Models;
using DocumentFlow.Models.AuthorizeAttribute;

namespace DocumentFlow.Controllers
{
    public class UserController : Controller
    {
        protected IRepositoryHandler<Position> _positionsHandler;
        protected IRepositoryHandler<Role> _rolesHandler;

        protected IRepositoryHandler<User> _usersHandler;


        public UserController()
        {
            _usersHandler = new UsersRepositoryHandler();
            _positionsHandler = new PositionsRepositoryHandler();
            _rolesHandler = new RolesRepositoryHandler();
        }

        public static Models.User ConvertToModel(User user)
        {
            Mapper.CreateMap<User, Models.User>();
            Mapper.CreateMap<Position, Models.Position>();
            Mapper.CreateMap<Role, Models.Role>();

            return Mapper.Map<User, Models.User>(user);
        }

        public static User ConvertToBlModel(Models.User user)
        {
            Mapper.CreateMap<Models.User, User>();
            Mapper.CreateMap<Models.Position, Position>();
            Mapper.CreateMap<Models.Role, Role>();

            return Mapper.Map<Models.User, User>(user);
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult Users()
        {
            return View(_usersHandler.GetAll().Select(ConvertToModel));
        }

        [CustomAuthorize("Admin")]
        public ActionResult EditUser(int id)
        {
            ViewBag.Positions = new PositionsHandler().PositionsSelectList();
            ViewBag.Roles = new RolesHandler().RolesSelectList();

            if (ModelState.IsValid)
            {
                var user = _usersHandler.FindById(id);

                if (user != null)
                {
                    return View(ConvertToModel(user));
                }
            }
            return RedirectToAction("Users");
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult EditUser(Models.User user)
        {
            if (ModelState.IsValid)
            {
                _usersHandler.Update(ConvertToBlModel(user));
            }
            return RedirectToAction("Users");
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteUser(int id)
        {
            var user = _usersHandler.FindById(id);

            if (user != null)
            {
                _usersHandler.Remove(user);
            }
            return RedirectToAction("Users");
        }
    }
}