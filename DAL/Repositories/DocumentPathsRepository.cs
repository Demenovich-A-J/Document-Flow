using DAL.AbstractRepository;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DocumentPathsRepository : DataRepository<DocumentPath>
    {
        public override async Task<DocumentPath> FindById(int id)
        {
            DocumentPath documentPath;
            using (var context = new EntityModels.Database())
            {
                documentPath = await context.DocumentPaths.FindAsync(id);
            }
            return documentPath;
        }
    }
}
