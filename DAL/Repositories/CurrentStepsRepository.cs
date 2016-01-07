using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.AbstractRepository;
using EntityModels;
using CurrentStep = DAL.Models.CurrentStep;

namespace DAL.Repositories
{
    public class CurrentStepsRepository : DataRepository<CurrentStep, EntityModels.CurrentStep>
    {
        public CurrentStepsRepository()
        {
            Mapper.CreateMap<CurrentStep, EntityModels.CurrentStep>();
            Mapper.CreateMap<EntityModels.CurrentStep, CurrentStep>();
        }

        public IEnumerable<CurrentStep> GetStepsForUser(int id)
        {
            List<CurrentStep> steps;

            using (var context = new Entities())
            {
                var entitySteps = context.CurrentSteps
                    .Where(x => x.UserId == id).ToList()
                    .Select(ConvertToModel).ToList();

                steps = entitySteps;
            }

            return steps;
        }

        public CurrentStep GetDocumentStep(int id)
        {
            CurrentStep step;

            using (var context = new Entities())
            {
                var entityStep = context.CurrentSteps.FirstOrDefault(x => x.DocumentId == id);
                step = ConvertToModel(entityStep);
            }

            return step;
        }

        protected override CurrentStep ConvertToModel(EntityModels.CurrentStep item)
        {
            return Mapper.Map<EntityModels.CurrentStep, CurrentStep>(item);
        }

        protected override EntityModels.CurrentStep ConvertToEntity(CurrentStep item)
        {
            return Mapper.Map<CurrentStep, EntityModels.CurrentStep>(item);
        }
    }
}