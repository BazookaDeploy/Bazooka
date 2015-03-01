
namespace Controller
{
    using DataAccess.Write;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Dialect;
    using NHibernate.Mapping.ByCode;
    using NServiceBus;
    using NServiceBus.Persistence;
    using NServiceBus.Persistence.Legacy;
    using System.Linq;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public static ISessionFactory Store { get; set; }


        public void Customize(BusConfiguration configuration)
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
            mapper.AddMapping<DeployUnitMap>();
            mapper.AddMapping<ParameterMap>();
            mapper.AddMapping<DeploymentMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            Store = config.BuildSessionFactory();


            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UsePersistence<MsmqPersistence>().For(Storage.Subscriptions);
            configuration.Transactions()
                         .DisableDistributedTransactions()
                         .DoNotWrapHandlersExecutionInATransactionScope()
                         .Disable();
            configuration.EndpointName("bazooka.controller");
            configuration.UseSerialization<JsonSerializer>();
            configuration.EnableInstallers();
            configuration.Conventions().DefiningCommandsAs(x => x.GetInterfaces().Contains(typeof(Bazooka.Core.Commands.ICommand)));
            

        }
    }
}
