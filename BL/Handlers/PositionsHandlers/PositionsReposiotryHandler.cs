using AutoMapper;
using BL.AbstractClasses;
using DAL.Repositories;
using DalModels = DAL.Models;
using Position = BL.Models.Position;

namespace BL.Handlers.PositionsHandlers
{
    public class PositionsRepositoryHandler
        : RepositoryHandler<Position, DalModels.Position>
    {
        public PositionsRepositoryHandler()
            : base(new PositionsRepository())
        {
            Mapper.CreateMap<Position, DalModels.Position>();
            Mapper.CreateMap<DalModels.Position, Position>();
        }

        protected override Position ConvertToModel(DalModels.Position item)
        {
            return Mapper.Map<DalModels.Position, Position>(item);
        }

        protected override DalModels.Position ConvertToDalModel(Position item)
        {
            return Mapper.Map<Position, DalModels.Position>(item);
        }
    }
}