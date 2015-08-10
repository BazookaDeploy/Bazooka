using DataAccess.Write;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static ISessionFactory Store { get; set; }

        protected void Application_Start()
        {
            var config = new NHibernate.Cfg.Configuration();
            config.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2008Dialect>();
                db.ConnectionStringName = "DataContext";
            });

            var mapper = new ModelMapper();
            mapper.AddMapping<ApplicationMap>();
            mapper.AddMapping<EnviromentMap>();
            mapper.AddMapping<DeployTaskMap>();
            mapper.AddMapping<ParameterMap>();
            mapper.AddMapping<DeploymentMap>();
            mapper.AddMapping<AllowedGroupMap>();
            mapper.AddMapping<AllowedUserMap>();
            mapper.AddMapping<LogEntryMap>();
            mapper.AddMapping<MailTaskMap>();
            mapper.AddMapping<LocalScriptTaskMap>();
            mapper.AddMapping<RemoteScriptTaskMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            Store = config.BuildSessionFactory();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }
    }
}
