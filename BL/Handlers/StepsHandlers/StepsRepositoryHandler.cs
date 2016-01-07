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
using Step = BL.Models.Step;
using User = BL.Models.User;

namespace BL.Handlers.StepsHandlers
{
    public class StepsRepositoryHandler
        : RepositoryHandler<Step, DalModels.Step>
    {
        public StepsRepositoryHandler()
            : base(new StepsRepository())
        {
            Mapper.CreateMap<Step, DalModels.Step>();
            Mapper.CreateMap<DalModels.Step, Step>();

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

        protected override Step ConvertToModel(DalModels.Step item)
        {
            return Mapper.Map<DalModels.Step, Step>(item);
        }

        protected override DalModels.Step ConvertToDalModel(Step item)
        {
            return Mapper.Map<Step, DalModels.Step>(item);
        }

        public List<Step> GetDocumentSteps(int docId)
        {
            var steps = new List<Step>();
            if (_repository is StepsRepository)
            {
                var dalStep = (_repository as StepsRepository).GetDocumentSteps(docId);
                steps = dalStep.Select(x => ConvertToModel(x)).ToList();
            }
            return steps;
        }
    }
}