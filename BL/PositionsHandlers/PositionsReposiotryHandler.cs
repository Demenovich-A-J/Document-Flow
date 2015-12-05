using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;

namespace BL.PositionsHandler
{
    public class PositionsRepositoryHandler : RepositoryHandler<Position>
    {
        public PositionsRepositoryHandler() : base(new PositionsRepository())
        {
        }
    }
}