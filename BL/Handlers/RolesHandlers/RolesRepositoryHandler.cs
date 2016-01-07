using AutoMapper;
using BL.AbstractClasses;
using BL.Models;
using DAL.Repositories;
using DalModels = DAL.Models;

namespace BL.Handlers.RolesHandlers
{
    public class RolesRepositoryHandler
        : RepositoryHandler<Role, DalModels.Role>
    {
        public RolesRepositoryHandler()
            : base(new RolesRepository())
        {
            Mapper.CreateMap<Role, DalModels.Role>();
            Mapper.CreateMap<DalModels.Role, Role>();
        }

        protected override Role ConvertToModel(DalModels.Role item)
        {
            return Mapper.Map<DalModels.Role, Role>(item);
        }

        protected override DalModels.Role ConvertToDalModel(Role item)
        {
            return Mapper.Map<Role, DalModels.Role>(item);
        }

        public Role FindByName(string name)
        {
            Role role = null;
            if (!(_repository is RolesRepository)) return null;

            var dalRole = ((RolesRepository) _repository).FindByName(name);
            role = ConvertToModel(dalRole);
            return role;
        }
    }
}