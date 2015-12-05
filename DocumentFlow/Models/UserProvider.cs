using System.Linq;
using System.Threading.Tasks;
using BL.AbstractClasses;
using EntityModels;

namespace DocumentFlow.Models
{
    public class UserProvider
    {
        protected RepositoryHandler<Role> _rolesRepositoryHandler;
        protected User _user;

        protected RepositoryHandler<User> _usersRepositoryHandler;

        public UserProvider(string name, RepositoryHandler<User> usersRepositoryHandler,
            RepositoryHandler<Role> rolesRepositoryHandler)
        {
            _usersRepositoryHandler = usersRepositoryHandler;
            _rolesRepositoryHandler = rolesRepositoryHandler;

            if (name != null)
            {
                _user = _usersRepositoryHandler.GetAll(x => x.UserName == name).First();
            }
        }

        public async Task<User> GetUser()
        {
            if (_user != null)
            {
                return _usersRepositoryHandler == null ? null : await _usersRepositoryHandler.FindById(_user.Id);
            }
            return null;
        }

        public async Task<bool> IsInRole(string role)
        {
            if (_user != null)
            {
                var roleId = _user.RoleId;
                var roleName = await _rolesRepositoryHandler.FindById(roleId);
                return role == roleName.Name;
            }
            return false;
        }
    }
}