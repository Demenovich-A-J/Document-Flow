using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.AbstractRepository;
using EntityModels;
using Document = DAL.Models.Document;
using DocumentType = EntityModels.DocumentType;
using Position = EntityModels.Position;
using Role = EntityModels.Role;
using User = DAL.Models.User;

namespace DAL.Repositories
{
    public class DocumentsRepository : DataRepository<Document, EntityModels.Document>
    {
        public DocumentsRepository()
        {
            Mapper.CreateMap<Document, EntityModels.Document>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type.Id))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.DocumentType, opt => opt.Ignore());

            Mapper.CreateMap<EntityModels.Document, Document>()
                 .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.DocumentType)); 
            Mapper.CreateMap<EntityModels.User, User>();
            Mapper.CreateMap<DocumentType, Models.DocumentType>();
            Mapper.CreateMap<Position, Models.Position>();
            Mapper.CreateMap<Role, Models.Role>();
        }

        protected override Document ConvertToModel(EntityModels.Document item)
        {
            return Mapper.Map<EntityModels.Document, Document>(item);
        }

        protected override EntityModels.Document ConvertToEntity(Document item)
        {
            return Mapper.Map<Document, EntityModels.Document>(item);
        }

        public Document GetUserDocumentByTime(int userId, DateTime time)
        {
            Document document = null;
            using (var context = new Entities())
            {
                var entityDocument =
                    context.Set<EntityModels.Document>()
                        .Select(ConvertToModel)
                        .FirstOrDefault(x => x.DateTime.ToString() == time.ToString() && x.User.Id == userId);

                document = entityDocument;
            }
            return document;
        }

        public List<Document> GetUserDocuments(int userId)
        {
            var documents = new List<Document>();
            using (var context = new Entities())
            {
                var entityDocuments = new List<EntityModels.Document>(context.Documents
                    .Where(x => x.UserId == userId));

                if (entityDocuments != null)
                    documents = entityDocuments.Select(x => ConvertToModel(x)).ToList();
            }
            return documents;
        }
    }
}