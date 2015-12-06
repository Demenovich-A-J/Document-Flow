using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;

namespace BL.DocumentHandler
{
    public class DocumentsRepositoryHandler : RepositoryHandler<Document>
    {
        public DocumentsRepositoryHandler() : base(new DocumentsRepository())
        {
        }
    }
}