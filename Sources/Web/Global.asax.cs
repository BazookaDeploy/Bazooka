using Castle.Windsor;
using Castle.Windsor.Installer;
using DataAccess.Write;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.App_Start;

namespace Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static ISessionFactory Store { get; set; }

        protected void Application_Start()
        {


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new WindsorContainer().Install(FromAssembly.This());

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(container));
        }
    }
}
