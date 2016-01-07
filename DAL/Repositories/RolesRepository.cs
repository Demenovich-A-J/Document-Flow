using System.Linq;
using AutoMapper;
using DAL.AbstractRepository;
using DAL.Models;
using Database = EntityModels.Entities;

namespace DAL.Repositories
{
    public class RolesRepository : DataRepository<Role, EntityModels.Role>
    {
        public RolesRepository()
        {
            Mapper.CreateMap<EntityModels.Role, Role>();
            Mapper.CreateMap<Role, EntityModels.Role>();
        }

        protected override Role ConvertToModel(EntityModels.Role item)
        {
            return Mapper.Map<EntityModels.Role, Role>(item);
        }

        protected override EntityModels.Role ConvertToEntity(Role item)
        {
            return Mapper.Map<Role, EntityModels.Role>(item);
        }

        public Role FindByName(string name)
        {
            Role role = null;

            using (var context = new Database())
            {
                var entityRole = context.Roles.FirstOrDefault(x => x.Name == name);
                role = ConvertToModel(entityRole);
            }
            return role;
        }
    }
}