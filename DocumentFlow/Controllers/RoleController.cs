using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BL.Handlers.RolesHandlers;
using BL.Interfaces;
using BL.Models;
using DocumentFlow.Models.AuthorizeAttribute;

namespace DocumentFlow.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRepositoryHandler<Role> _rolesHandler;

        public RoleController()
        {
            _rolesHandler = new RolesRepositoryHandler();
        }

        private static Models.Role ConvertToModel(Role role)
        {
            Mapper.CreateMap<Role, Models.Role>();

            return Mapper.Map<Role, Models.Role>(role);
        }

        private static Role ConvertToBlModel(Models.Role role)
        {
            Mapper.CreateMap<Models.Role, Role>();

            return Mapper.Map<Models.Role, Role>(role);
        }


        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult Roles()
        {
            var a = HttpContext.Request.Cookies["Role"].Value;

            return View(_rolesHandler.GetAll().Select(ConvertToModel));
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult CreateRole(Models.Role role)
        {
            if (ModelState.IsValid)
            {
                _rolesHandler.Add(ConvertToBlModel(role));
            }
            return RedirectToAction("Roles");
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult EditRole(int id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Roles");

            var role = _rolesHandler.FindById(id);

            if (role != null)
            {
                return View(ConvertToModel(role));
            }
            return RedirectToAction("Roles");
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult EditRole(Models.Role role)
        {
            if (ModelState.IsValid)
            {
                _rolesHandler.Update(ConvertToBlModel(role));
            }
            return RedirectToAction("Roles");
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteRole(int id)
        {
            var role = _rolesHandler.FindById(id);

            if (role != null)
            {
                _rolesHandler.Remove(role);
            }
            return RedirectToAction("Roles");
        }
    }
}