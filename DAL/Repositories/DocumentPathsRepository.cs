using System.Threading.Tasks;
using DAL.AbstractRepository;
using EntityModels;

namespace DAL.Repositories
{
    public class DocumentPathsRepository : DataRepository<DocumentPath>
    {
        public override async Task<DocumentPath> FindById(int id)
        {
            DocumentPath documentPath;
            using (var context = new Database())
            {
                documentPath = await context.DocumentPaths.FindAsync(id);
            }
            return documentPath;
        }
    }
}