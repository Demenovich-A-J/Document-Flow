using System.Threading.Tasks;
using DAL.AbstractRepository;
using EntityModels;

namespace DAL.Repositories
{
    public class PositionsRepository : DataRepository<Position>
    {
        public override async Task<Position> FindById(int id)
        {
            Position position;
            using (var context = new Database())
            {
                position = await context.Positions.FindAsync(id);
            }
            return position;
        }
    }
}