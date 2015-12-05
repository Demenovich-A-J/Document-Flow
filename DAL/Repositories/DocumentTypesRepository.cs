using DAL.AbstractRepository;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DocumentTypesRepository : DataRepository<DocumentType>
    {
        public override async Task<DocumentType> FindById(int id)
        {
            DocumentType type;
            using (var context = new EntityModels.Database())
            {
                type = await context.DocumentTypes.FindAsync(id);
            }
            return type;
        }
    }
}
