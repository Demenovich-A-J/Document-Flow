using BL.AbstractClasses;
using BL.RolesHandlers;
using BL.UsersHandlers;
using EntityModels;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace DocumentFlow.Models
{
    public class CustomAuthentication : IAuthentication
    {
        protected const string _cookieName = "__AUTH_COOKIE";

        protected HttpContext _httpContext = HttpContext.Current;

        protected RepositoryHandler<User> _usersRepository = new UsersRepositoryHandler();

        #region IAuthentication Members

        public User Login(LoginModel loginModel)
        {
            var users = _usersRepository.
                GetAll(user => user.UserName == loginModel.Login && user.Password == loginModel.Password);

            User curUser = null;
            if (users.Count() != 0)
            {
                curUser = users.First();
            }

            if (curUser != null)
            {
                CreateCookie(loginModel.Login, true);
            }
            return curUser;
        }

        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  userName,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            var encTicket = FormsAuthentication.Encrypt(ticket);

            var AuthCookie = new HttpCookie(_cookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            _httpContext.Response.Cookies.Set(AuthCookie);
        }

        public void LogOut()
        {
            var httpCookie = _httpContext.Response.Cookies[_cookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }

        public UserProvider GetUserProvider
        {
            get
            {
                HttpCookie authCookie = _httpContext.Response.Cookies.Get(_cookieName);
                if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    return new UserProvider(ticket.Name, _usersRepository, new RolesRepositoryHandler());
                }
                return new UserProvider(null, null, null);
            }
        }
        #endregion


        public User Register(RegisterModel registerModel)
        {
            if(_usersRepository.GetAll(user => user.UserName == registerModel.Login).Count() == 0)
            {
                _usersRepository.Add(new User()
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Patronymic = registerModel.Patronymic,
                    UserName = registerModel.Login,
                    PositionId = registerModel.PositionId,
                    Password = registerModel.Password,
                    Email = registerModel.Email,
                    RoleId = 2 //user
                });

                return _usersRepository.GetAll(user => user.UserName == registerModel.Login).First();
            }
            return null;
        }
    }
}