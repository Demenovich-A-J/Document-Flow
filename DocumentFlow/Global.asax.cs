using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BL.DataConfigurator;
using Database = EntityModels.Entities;

namespace DocumentFlow
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ConfigData.Config();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}