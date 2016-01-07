using AutoMapper;
using BL.AbstractClasses;
using DAL.Repositories;
using DalModels = DAL.Models;
using DocumentTemplate = BL.Models.DocumentTemplate;
using DocumentType = BL.Models.DocumentType;

namespace BL.Handlers.DocumentTemplatesHandlers
{
    public class DocumentTemplatesRepositoryHandler
        : RepositoryHandler<DocumentTemplate, DalModels.DocumentTemplate>
    {
        public DocumentTemplatesRepositoryHandler()
            : base(new DocumentTemplatesRepository())
        {
            Mapper.CreateMap<DocumentTemplate, DalModels.DocumentTemplate>();
            Mapper.CreateMap<DalModels.DocumentTemplate, DocumentTemplate>();

            Mapper.CreateMap<DocumentType, DalModels.DocumentType>();
            Mapper.CreateMap<DalModels.DocumentType, DocumentType>();
        }

        protected override DocumentTemplate ConvertToModel(DalModels.DocumentTemplate item)
        {
            return Mapper.Map<DalModels.DocumentTemplate, DocumentTemplate>(item);
        }

        protected override DalModels.DocumentTemplate ConvertToDalModel(DocumentTemplate item)
        {
            return Mapper.Map<DocumentTemplate, DalModels.DocumentTemplate>(item);
        }
    }
}