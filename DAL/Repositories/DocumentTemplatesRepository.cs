using AutoMapper;
using DAL.AbstractRepository;
using DAL.Models;
using Database = EntityModels.Entities;

namespace DAL.Repositories
{
    public class DocumentTemplatesRepository : DataRepository<DocumentTemplate, EntityModels.DocumentTemplate>
    {
        public DocumentTemplatesRepository()
        {
            Mapper.CreateMap<DocumentTemplate, EntityModels.DocumentTemplate>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type.Id))
                .ForMember(dest => dest.DocumentType, opt => opt.Ignore());


            Mapper.CreateMap<EntityModels.DocumentTemplate, DocumentTemplate>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.DocumentType));
            Mapper.CreateMap<EntityModels.DocumentType, DocumentType>();
        }

        protected override DocumentTemplate ConvertToModel(EntityModels.DocumentTemplate item)
        {
            return Mapper.Map<EntityModels.DocumentTemplate, DocumentTemplate>(item);
        }

        protected override EntityModels.DocumentTemplate ConvertToEntity(DocumentTemplate item)
        {
            return Mapper.Map<DocumentTemplate, EntityModels.DocumentTemplate>(item);
        }
    }
}