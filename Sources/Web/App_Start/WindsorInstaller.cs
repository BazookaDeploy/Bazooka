using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http;
using NHibernate.Mapping.ByCode;
using DataAccess.Write;
using NHibernate.Cfg;
using NHibernate.Dialect;
using Web.Commands;
using NHibernate;
using DataAccess.Read;
using Web.Infrastructure;

namespace Web.App_Start
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                      .BasedOn<ApiController>()
                      .LifestylePerWebRequest());

            container.Register(Classes.FromThisAssembly()
                      .BasedOn<IBusinessRule>()
                      .WithServiceAllInterfaces()
                      .LifestylePerWebRequest());

            container.Register(Classes.FromThisAssembly()
                      .BasedOn<ICommandHandler>()
                      .WithServiceAllInterfaces()
                      .LifestylePerWebRequest());

            container.Register(Classes.FromThisAssembly()
                      .BasedOn<IPermissionChecker>()
                      .WithServiceAllInterfaces()
                      .LifestylePerWebRequest());

            container.Register(Component.For<IBusinessRuleValidator>()
                                        .ImplementedBy<BusinessRuleValidator>()
                                        .LifestylePerWebRequest());

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
            mapper.AddMapping<ApplicationAdministratorsMap>();
            mapper.AddMapping<AllowedUserMap>();
            mapper.AddMapping<LogEntryMap>();
            mapper.AddMapping<MailTaskMap>();
            mapper.AddMapping<LocalScriptTaskMap>();
            mapper.AddMapping<RemoteScriptTaskMap>();
            mapper.AddMapping<DatabaseTaskMap>();
            mapper.AddMapping<AgentMap>();
            mapper.AddMapping<ApplicationGroupMap>();
            mapper.AddMapping<DeploymentTaskMap>();
            mapper.AddMapping<TaskTemplateMap>();
            mapper.AddMapping<TaskTemplateParameterMap>();


            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            WebApiApplication.Store = config.BuildSessionFactory();

            container.Register(Component.For<ISessionFactory>()
                                        .Instance(WebApiApplication.Store)
                                        .LifestyleSingleton());

            container.Register(Component.For<ISession>()
                                        .UsingFactoryMethod(x => x.Resolve<ISessionFactory>().OpenSession())
                                        .LifestylePerWebRequest());

            container.Register(Component.For<IReadContext>()
                                        .UsingFactoryMethod(x => new ReadContext())
                                        .LifestylePerWebRequest());

            container.Register(Component.For<ICommandExecutor>()
                                         .ImplementedBy<CommandExecutor>()
                                        .LifestyleSingleton());

            container.Register(Component.For<IRepository>()
                                         .ImplementedBy<Repository>()
                                        .LifestylePerWebRequest());
        }
    }
}
