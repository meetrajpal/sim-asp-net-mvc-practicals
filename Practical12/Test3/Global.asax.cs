using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Test3.Models.Infrastructure;

namespace Test3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DatabaseInitializer.Initialize();
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            Server.ClearError();

            if (Session != null)
            {
                Session["ErrorMessage"] = exception?.Message;
            }

            Response.Redirect("~/Error");
        }
    }
}
