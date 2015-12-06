using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;

namespace BL.DocumentTypeHandlers
{
    public class DocumentTypesRepositoryHandler : RepositoryHandler<DocumentType>
    {
        public DocumentTypesRepositoryHandler() : base(new DocumentTypesRepository())
        {
        }
    }
}