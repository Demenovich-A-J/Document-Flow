using System.Threading.Tasks;
using DAL.AbstractRepository;
using EntityModels;

namespace DAL.Repositories
{
    public class DocumentsRepository : DataRepository<Document>
    {
        public override async Task<Document> FindById(int id)
        {
            Document document;
            using (var context = new Database())
            {
                document = await context.Documents.FindAsync(id);
            }
            return document;
        }
    }
}