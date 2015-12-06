using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DocumentPathsHandler
{
    public class DocumentPathRepositoryHandler : RepositoryHandler<DocumentPath>
    {
        public DocumentPathRepositoryHandler() : base(new DocumentPathsRepository()) { }
    }
}
