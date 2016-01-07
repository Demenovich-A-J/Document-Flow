using AutoMapper;
using BL.AbstractClasses;
using DAL.Repositories;
using DalModels = DAL.Models;
using DocumentType = BL.Models.DocumentType;

namespace BL.Handlers.DocumentTypeHandlers
{
    public class DocumentTypesRepositoryHandler
        : RepositoryHandler<DocumentType, DalModels.DocumentType>
    {
        public DocumentTypesRepositoryHandler()
            : base(new DocumentTypesRepository())
        {
            Mapper.CreateMap<DocumentType, DalModels.DocumentType>();
            Mapper.CreateMap<DalModels.DocumentType, DocumentType>();
        }

        protected override DocumentType ConvertToModel(DalModels.DocumentType item)
        {
            return Mapper.Map<DalModels.DocumentType, DocumentType>(item);
        }

        protected override DalModels.DocumentType ConvertToDalModel(DocumentType item)
        {
            return Mapper.Map<DocumentType, DalModels.DocumentType>(item);
        }
    }
}