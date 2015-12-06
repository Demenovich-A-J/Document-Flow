using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.UsersHandlers
{
    public class UsersRepositoryHandler: RepositoryHandler<User>
    {
        public UsersRepositoryHandler() : base(new UsersRepository()) { }
    }
}
