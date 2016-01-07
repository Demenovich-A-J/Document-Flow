using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.AbstractClasses;
using DAL.Repositories;
using CurrentStep = BL.Models.CurrentStep;
using DalModels = DAL.Models;


namespace BL.Handlers.CurrentStepsHandlers
{
    public class CurrentStepsRepositoryHandler : RepositoryHandler<CurrentStep, DalModels.CurrentStep>
    {
        public CurrentStepsRepositoryHandler()
            : base(new CurrentStepsRepository())
        {
            Mapper.CreateMap<CurrentStep, DalModels.CurrentStep>();
            Mapper.CreateMap<DalModels.CurrentStep, CurrentStep>();
        }

        protected override CurrentStep ConvertToModel(DalModels.CurrentStep item)
        {
            return Mapper.Map<DalModels.CurrentStep, CurrentStep>(item);
        }

        protected override DalModels.CurrentStep ConvertToDalModel(CurrentStep item)
        {
            return Mapper.Map<CurrentStep, DalModels.CurrentStep>(item);
        }

        public List<CurrentStep> GetStepsForUser(int userId)
        {
            var steps = new List<CurrentStep>();
            if (_repository is CurrentStepsRepository)
            {
                var dalSteps = (_repository as CurrentStepsRepository)
                    .GetStepsForUser(userId);
                steps = dalSteps.Select(x => ConvertToModel(x)).ToList();
            }
            return steps;
        }

        public CurrentStep GetDocumentStep(int docId)
        {
            CurrentStep step = null;
            if (_repository is CurrentStepsRepository)
            {
                var dalStep = (_repository as CurrentStepsRepository).GetDocumentStep(docId);
                step = ConvertToModel(dalStep);
            }
            return step;
        }
    }
}