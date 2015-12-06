using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;

namespace BL.UsersHandlers
{
    public class UsersRepositoryHandler : RepositoryHandler<User>
    {
        public UsersRepositoryHandler() : base(new UsersRepository())
        {
        }
    }
}