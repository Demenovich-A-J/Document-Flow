using System.Threading.Tasks;
using DAL.AbstractRepository;
using EntityModels;

namespace DAL.Repositories
{
    public class DocumentTemplatesRepository : DataRepository<DocumentTemplate>
    {
        public override async Task<DocumentTemplate> FindById(int id)
        {
            DocumentTemplate template;
            using (var context = new Database())
            {
                template = await context.DocumentTemplates.FindAsync(id);
            }
            return template;
        }
    }
}