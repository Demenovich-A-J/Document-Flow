using AutoMapper;
using DAL.AbstractRepository;
using DAL.Models;
using Database = EntityModels.Entities;
using System.Linq;

namespace DAL.Repositories
{
    public class DocumentTypesRepository : DataRepository<DocumentType, EntityModels.DocumentType>
    {
        public DocumentTypesRepository()
        {
            Mapper.CreateMap<EntityModels.DocumentType, DocumentType>();
            Mapper.CreateMap<DocumentType, EntityModels.DocumentType>();
        }

        protected override DocumentType ConvertToModel(EntityModels.DocumentType item)
        {
            return Mapper.Map<EntityModels.DocumentType, DocumentType>(item);
        }

        protected override EntityModels.DocumentType ConvertToEntity(DocumentType item)
        {
            return Mapper.Map<DocumentType, EntityModels.DocumentType>(item);
        }
    }
}