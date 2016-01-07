using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;

namespace BL.Handlers.RolesHandlers
{
    public class RolesHandler
    {
        protected IDataRepository<Role> _rolesRepository;

        public RolesHandler()
        {
            _rolesRepository = new RolesRepository();
        }

        public List<SelectListItem> RolesSelectList()
        {
            var roles = _rolesRepository.GetAll();

            return roles != null
                ? new List<SelectListItem>
                    (roles.Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()}))
                : new List<SelectListItem>();
        }
    }
}