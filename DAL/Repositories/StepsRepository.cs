using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.AbstractRepository;
using EntityModels;
using Document = DAL.Models.Document;
using DocumentType = EntityModels.DocumentType;
using Position = EntityModels.Position;
using Role = EntityModels.Role;
using Step = DAL.Models.Step;
using User = DAL.Models.User;

namespace DAL.Repositories
{
    public class StepsRepository : DataRepository<Step, EntityModels.Step>
    {
        public StepsRepository()
        {
            Mapper.CreateMap<Step, EntityModels.Step>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.Document.Id))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Document, opt => opt.Ignore());

            Mapper.CreateMap<EntityModels.Step, Step>();
            Mapper.CreateMap<EntityModels.Document, Document>();
            Mapper.CreateMap<EntityModels.User, User>();
            Mapper.CreateMap<DocumentType, Models.DocumentType>();
            Mapper.CreateMap<Position, Models.Position>();
            Mapper.CreateMap<Role, Models.Role>();
        }

        protected override Step ConvertToModel(EntityModels.Step item)
        {
            return Mapper.Map<EntityModels.Step, Step>(item);
        }

        protected override EntityModels.Step ConvertToEntity(Step item)
        {
            return Mapper.Map<Step, EntityModels.Step>(item);
        }

        public List<Step> GetDocumentSteps(int id)
        {
            var steps = new List<Step>();

            using (var context = new Entities())
            {
                var entitySteps = context.Steps
                    .Where(x => x.DocumentId == id)
                    .ToList();
                steps = entitySteps
                    .Select(x => ConvertToModel(x))
                    .ToList();
            }

            return steps;
        }
    }
}