using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.AbstractRepository;
using DAL.Models;
using Database = EntityModels.Entities;

namespace DAL.Repositories
{
    public class UsersRepository : DataRepository<User, EntityModels.User>
    {
        public UsersRepository()
        {
            Mapper.CreateMap<User, EntityModels.User>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Id))
                .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => src.Position.Id))
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Position, opt => opt.Ignore());

            Mapper.CreateMap<EntityModels.User, User>();
            Mapper.CreateMap<EntityModels.Position, Position>();
            Mapper.CreateMap<EntityModels.Role, Role>();
        }

        protected override User ConvertToModel(EntityModels.User item)
        {
            return Mapper.Map<EntityModels.User, User>(item);
        }

        protected override EntityModels.User ConvertToEntity(User item)
        {
            return Mapper.Map<User, EntityModels.User>(item);
        }

        public List<User> GetUserByPositionId(int positionId)
        {
            var users = new List<User>();

            using (var context = new Database())
            {
                users = context.Users
                    .Where(x => x.PositionId == positionId)
                    .ToList()
                    .Select(x => ConvertToModel(x))
                    .ToList();
            }

            return users;
        }

        public User GetByEmailAndPassword(string login, string password)
        {
            User user = null;

            using (var context = new Database())
            {
                var entityUser = context.Users
                    .FirstOrDefault(x => x.Email == login && x.Password == password);
                user = ConvertToModel(entityUser);
            }

            return user;
        }

        public User GetByEmail(string login)
        {
            User user = null;

            using (var context = new Database())
            {
                var entityUser = context.Users
                    .FirstOrDefault(x => x.Email == login);
                user = ConvertToModel(entityUser);
            }

            return user;
        }
    }
}