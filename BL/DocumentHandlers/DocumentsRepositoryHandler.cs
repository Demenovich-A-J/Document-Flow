using BL.AbstractClasses;
using DAL.AbstractRepository;
using DAL.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DocumentHandler
{
    public class DocumentsRepositoryHandler : RepositoryHandler<Document>
    {
        public DocumentsRepositoryHandler() : base(new DocumentsRepository()) { }
    }
}
