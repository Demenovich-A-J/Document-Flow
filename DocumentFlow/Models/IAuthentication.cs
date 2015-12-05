using EntityModels;

namespace DocumentFlow.Models
{
    public interface IAuthentication
    {
        UserProvider GetUserProvider { get; }
        User Login(LoginModel loginModel);

        void LogOut();

        User Register(RegisterModel registerModel);
    }
}