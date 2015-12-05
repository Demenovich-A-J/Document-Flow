using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;

namespace BL.RolesHandlers
{
    public class RolesRepositoryHandler : RepositoryHandler<Role>
    {
        public RolesRepositoryHandler() : base(new RolesRepository())
        {
        }
    }
}