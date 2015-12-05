using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.RolesHandlers
{
    public class RolesRepositoryHandler : RepositoryHandler<Role>
    {
        public RolesRepositoryHandler() : base(new RolesRepository()) { }
    }
}
