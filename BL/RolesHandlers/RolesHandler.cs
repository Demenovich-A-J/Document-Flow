using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.AbstractRepository;
using DAL.Repositories;
using EntityModels;


namespace BL.RolesHandlers
{
    public class RolesHandler
    {
         protected DataRepository<Role> _rolesRepository;

        public RolesHandler()
        {
            _rolesRepository = new RolesRepository();
        }

        public List<SelectListItem> RolesSelectList()
        {
            var roles = _rolesRepository.GetAll(x => true);

            return roles != null
                ? new List<SelectListItem>
                    (roles.Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()}))
                : new List<SelectListItem>();
        }
    }
}
