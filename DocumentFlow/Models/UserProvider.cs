using BL.AbstractClasses;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace DocumentFlow.Models
{
    public class UserProvider
    {
        protected User _user;

        public async Task<User> GetUser()
        {
            if (_user != null)
            {
                return _usersRepositoryHandler == null ? null : await _usersRepositoryHandler.FindById(_user.Id);
            }
            return null;
        }

        protected RepositoryHandler<User> _usersRepositoryHandler;
        protected RepositoryHandler<Role> _rolesRepositoryHandler;

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

        public UserProvider(string name, RepositoryHandler<User> usersRepositoryHandler, RepositoryHandler<Role> rolesRepositoryHandler)
        {
            this._usersRepositoryHandler = usersRepositoryHandler;
            this._rolesRepositoryHandler = rolesRepositoryHandler;

            if (name != null)
            {
                _user = _usersRepositoryHandler.GetAll(x => x.UserName == name).First();
            }
        }
    }
}