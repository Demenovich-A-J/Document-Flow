using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocumentFlow.Models
{
    public interface IAuthentication
    {
        User Login(LoginModel loginModel);

        void LogOut();

        User Register(RegisterModel registerModel);

        UserProvider GetUserProvider { get; }
    }
}
