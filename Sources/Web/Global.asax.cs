using DataAccess.Write;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NServiceBus;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static ISessionFactory Store { get; set; }

        public static IBus Bus { get; set; }

        protected void Application_Start()
        {
            var config = new NHibernate.Cfg.Configuration();
            config.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2008Dialect>();
                db.ConnectionStringName = "DataContext";
            });

            var busConfig = new BusConfiguration();
            busConfig.EndpointName("Bazooka.web");
            busConfig.UseSerialization<JsonSerializer>();
            busConfig.EnableInstallers();
            busConfig.Conventions().DefiningCommandsAs(x => x.GetInterfaces().Contains(typeof(Bazooka.Core.Commands.ICommand)));
            busConfig.UsePersistence<InMemoryPersistence>();

            Bus = NServiceBus.Bus.Create(busConfig).Start();


            var mapper = new ModelMapper();
            mapper.AddMapping<ApplicationMap>();
            mapper.AddMapping<EnviromentMap>();
            mapper.AddMapping<DeployUnitMap>();
            mapper.AddMapping<ParameterMap>();
            mapper.AddMapping<DeploymentMap>();

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
