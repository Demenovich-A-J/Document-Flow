using DAL.AbstractRepository;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DocumentTemplatesRepository : DataRepository<DocumentTemplate>
    {
        public override async Task<DocumentTemplate> FindById(int id)
        {
            DocumentTemplate template;
            using (var context = new EntityModels.Database())
            {
                template = await context.DocumentTemplates.FindAsync(id);
            }
            return template;
        }
    }
}
