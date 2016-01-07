using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.AbstractClasses;
using BL.Models;
using DAL.Repositories;
using DalModels = DAL.Models;

namespace BL.Handlers.UsersHandlers
{
    public class UsersRepositoryHandler
        : RepositoryHandler<User, DalModels.User>
    {
        public UsersRepositoryHandler()
            : base(new UsersRepository())
        {
            Mapper.CreateMap<Position, DalModels.Position>();
            Mapper.CreateMap<DalModels.Position, Position>();

            Mapper.CreateMap<Role, DalModels.Role>();
            Mapper.CreateMap<DalModels.Role, Role>();

            Mapper.CreateMap<User, DalModels.User>();
            Mapper.CreateMap<DalModels.User, User>();
        }

        protected override User ConvertToModel(DalModels.User item)
        {
            return Mapper.Map<DalModels.User, User>(item);
        }

        protected override DalModels.User ConvertToDalModel(User item)
        {
            return Mapper.Map<User, DalModels.User>(item);
        }

        public List<User> GetUserByPositionId(int positionId)
        {
            var users = new List<User>();
            if (!(_repository is UsersRepository)) return users;

            var dalUsers = ((UsersRepository) _repository)
                .GetUserByPositionId(positionId);
            users = dalUsers.Select(ConvertToModel).ToList();
            return users;
        }

        public User GetUserByEmailPassword(string email, string password)
        {
            User user = null;
            if (!(_repository is UsersRepository)) return null;
            var dalUser = ((UsersRepository) _repository).GetByEmailAndPassword(email, password);
            user = ConvertToModel(dalUser);
            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user = null;
            if (!(_repository is UsersRepository)) return null;
            var dalUser = ((UsersRepository) _repository).GetByEmail(email);
            user = ConvertToModel(dalUser);
            return user;
        }
    }
}