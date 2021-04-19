using Recruitment.WebMVC.App_Start;
using System.Web.Mvc;
using System.Web.Routing;

namespace Recruitment.WebMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DatabaseSetup.Initialize();
        }
    }
}
