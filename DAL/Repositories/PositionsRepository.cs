using AutoMapper;
using DAL.AbstractRepository;
using DAL.Models;
using Database = EntityModels.Entities;
using System.Linq;

namespace DAL.Repositories
{
    public class PositionsRepository : DataRepository<Position, EntityModels.Position>
    {
        public PositionsRepository()
        {
            Mapper.CreateMap<EntityModels.Position, Position>();
            Mapper.CreateMap<Position, EntityModels.Position>();
        }

        protected override Position ConvertToModel(EntityModels.Position item)
        {
            return Mapper.Map<EntityModels.Position, Position>(item);
        }

        protected override EntityModels.Position ConvertToEntity(Position item)
        {
            return Mapper.Map<Position, EntityModels.Position>(item);
        }
    }
}