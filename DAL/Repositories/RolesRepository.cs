using DAL.AbstractRepository;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RolesRepository : DataRepository<Role>
    {
        public override async Task<Role> FindById(int id)
        {
            Role role;
            using (var context = new EntityModels.Database())
            {
                role = await context.Roles.FindAsync(id);
            }
            return role;
        }
    }
}
