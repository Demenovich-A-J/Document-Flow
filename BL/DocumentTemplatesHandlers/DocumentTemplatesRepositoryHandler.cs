using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;

namespace BL.DocumentTemplatesHandlers
{
    public class DocumentTemplatesRepositoryHandler : RepositoryHandler<DocumentTemplate>
    {
        public DocumentTemplatesRepositoryHandler() : base(new DocumentTemplatesRepository())
        {
        }
    }
}