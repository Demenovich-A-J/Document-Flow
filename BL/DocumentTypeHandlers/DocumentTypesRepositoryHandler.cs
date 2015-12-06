using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DocumentTypeHandlers
{
    public class DocumentTypesRepositoryHandler: RepositoryHandler<DocumentType>
    {
        public DocumentTypesRepositoryHandler() : base(new DocumentTypesRepository()) { }
    }
}
