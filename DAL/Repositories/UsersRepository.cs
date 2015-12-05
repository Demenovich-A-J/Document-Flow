using System.Threading.Tasks;
using DAL.AbstractRepository;
using EntityModels;

namespace DAL.Repositories
{
    public class UsersRepository : DataRepository<User>
    {
        public override async Task<User> FindById(int id)
        {
            User user;
            using (var context = new Database())
            {
                user = await context.Users.FindAsync(id);
            }
            return user;
        }
    }
}