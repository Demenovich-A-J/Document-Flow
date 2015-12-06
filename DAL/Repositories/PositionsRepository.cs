using DAL.AbstractRepository;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PositionsRepository : DataRepository<Position>
    {
        public override async Task<Position> FindById(int id)
        {
            Position position;
            using (var context = new EntityModels.Database())
            {
                position = await context.Positions.FindAsync(id);
            }
            return position;
        }
    }
}
