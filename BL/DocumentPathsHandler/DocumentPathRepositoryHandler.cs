using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;

namespace BL.DocumentPathsHandler
{
    public class DocumentPathRepositoryHandler : RepositoryHandler<DocumentPath>
    {
        public DocumentPathRepositoryHandler() : base(new DocumentPathsRepository())
        {
        }
    }
}