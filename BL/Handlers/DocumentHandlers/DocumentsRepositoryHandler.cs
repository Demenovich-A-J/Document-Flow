using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.AbstractClasses;
using DAL.Repositories;
using DalModels = DAL.Models;
using Document = BL.Models.Document;
using DocumentType = BL.Models.DocumentType;
using Position = BL.Models.Position;
using Role = BL.Models.Role;
using User = BL.Models.User;

namespace BL.Handlers.DocumentHandlers
{
    public class DocumentsRepositoryHandler : RepositoryHandler<Document, DalModels.Document>
    {
        public DocumentsRepositoryHandler()
            : base(new DocumentsRepository())
        {
            Mapper.CreateMap<Document, DalModels.Document>();
            Mapper.CreateMap<DalModels.Document, Document>();

            Mapper.CreateMap<User, DalModels.User>();
            Mapper.CreateMap<DalModels.User, User>();

            Mapper.CreateMap<DocumentType, DalModels.DocumentType>();
            Mapper.CreateMap<DalModels.DocumentType, DocumentType>();

            Mapper.CreateMap<Position, DalModels.Position>();
            Mapper.CreateMap<DalModels.Position, Position>();

            Mapper.CreateMap<Role, DalModels.Role>();
            Mapper.CreateMap<DalModels.Role, Role>();
        }

        protected override Document ConvertToModel(DalModels.Document item)
        {
            return Mapper.Map<DalModels.Document, Document>(item);
        }

        protected override DalModels.Document ConvertToDalModel(Document item)
        {
            return Mapper.Map<Document, DalModels.Document>(item);
        }

        public Document GetUserDocumentByTime(int userId, DateTime time)
        {
            Document document = null;
            if (_repository is DocumentsRepository)
            {
                var dalDocument = (_repository as DocumentsRepository).GetUserDocumentByTime(userId, time);
                document = ConvertToModel(dalDocument);
            }
            return document;
        }

        public List<Document> GetUserDocuments(int userId)
        {
            var documents = new List<Document>();
            if (_repository is DocumentsRepository)
            {
                var dalDocuments = ((DocumentsRepository) _repository).GetUserDocuments(userId);
                documents = dalDocuments.Select(ConvertToModel).ToList();
            }
            return documents;
        }
    }
}