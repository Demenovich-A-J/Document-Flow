using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BL.RolesHandlers;
using BL.UsersHandlers;
using EntityModels;

namespace DocumentFlow
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var context = new Database();

            if (!context.Database.Exists())
            {
                context.Database.Create();

                //add roles user admin
                var rolesHandler = new RolesRepositoryHandler();
                rolesHandler.Add(new Role {Name = "Admin"});
                rolesHandler.Add(new Role {Name = "User"});

                //add admin
                var usersHandler = new UsersRepositoryHandler();
                usersHandler.Add(new User
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin",
                    Patronymic = "admin",
                    Password = "123456",
                    UserName = "admin",
                    RoleId = 1,
                    PositionId = -1
                });
            }

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}