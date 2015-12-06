using DAL.AbstractRepository;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UsersRepository : DataRepository<User>
    {
        public async override Task<User> FindById(int id)
        {
            User user;
            using (var context = new EntityModels.Database())
            {
                user = await context.Users.FindAsync(id);
            }
            return user;
        }
    }
}
