using BL.AbstractClasses;
using DAL.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DocumentTemplatesHandlers
{
    public class DocumentTemplatesRepositoryHandler : RepositoryHandler<DocumentTemplate>
    {
        public DocumentTemplatesRepositoryHandler() : base(new DocumentTemplatesRepository()) { }
    }
}
