using DAL.AbstractRepository;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DocumentsRepository : DataRepository<Document>
    {
        public override async Task<Document> FindById(int id)
        {
            Document document;
            using (var context = new EntityModels.Database())
            {
                document = await context.Documents.FindAsync(id);
            }
            return document;
        }
    }
}
