using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BL.Handlers.UsersHandlers;

namespace DocumentFlow.Models.AuthorizeAttribute
{
    public class CustomAuthorize : System.Web.Mvc.AuthorizeAttribute
    {
         private readonly string[] _allowedroles;
        private readonly UsersRepositoryHandler _usersRepositoryHandler =
            new UsersRepositoryHandler();
        public CustomAuthorize(params string[] roles)
        {
            this._allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var currentUserRole = httpContext.Request.Cookies["Role"].Value;

            return _allowedroles.Any(role => role == currentUserRole);
        }
    }
}