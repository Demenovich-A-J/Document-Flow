using System.Threading.Tasks;
using DAL.AbstractRepository;
using EntityModels;

namespace DAL.Repositories
{
    public class RolesRepository : DataRepository<Role>
    {
        public override async Task<Role> FindById(int id)
        {
            Role role;
            using (var context = new Database())
            {
                role = await context.Roles.FindAsync(id);
            }
            return role;
        }
    }
}