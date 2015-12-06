using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.PositionsHandler
{
    public class PositionsRepositoryHandler : RepositoryHandler<Position>
    {
        public PositionsRepositoryHandler() : base(new PositionsRepository()) { }
    }
}
