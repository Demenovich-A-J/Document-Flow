using System.Threading.Tasks;
using DAL.AbstractRepository;
using EntityModels;

namespace DAL.Repositories
{
    public class DocumentTypesRepository : DataRepository<DocumentType>
    {
        public override async Task<DocumentType> FindById(int id)
        {
            DocumentType type;
            using (var context = new Database())
            {
                type = await context.DocumentTypes.FindAsync(id);
            }
            return type;
        }
    }
}