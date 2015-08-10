namespace Controller
{
    using DataAccess.Write;
    using Hangfire.Logging;
    using Hangfire.Logging.LogProviders;
    using Jobs;
    using Microsoft.Owin.Hosting;
    using NHibernate.Cfg;
    using NHibernate.Dialect;
    using NHibernate.Mapping.ByCode;
    using System;
    using System.IO;
    using Topshelf;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Configure AppDomain parameter to simplify the config – http://stackoverflow.com/a/3501950/1317575
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));

            LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());

            HostFactory.Run(x =>
            {
                x.Service<Application>(s =>
                {
                    s.ConstructUsing(name => new Application());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.ApplyCommandLine();
                x.SetDescription("Bazooka deployment controller");
                x.SetDisplayName("Bazooka deployment controller");
                x.SetServiceName("BazookaController");
                x.StartAutomatically();
            });
        }
    }

    public class Application
    {

        private const string Endpoint = "http://localhost:12345";

        private IDisposable _host;

        public void Start()
        {
            _host = WebApp.Start<Startup>(Endpoint);

            Console.WriteLine();
            Console.WriteLine("Hangfire Server started.");
            Console.WriteLine("Dashboard is available at {0}/hangfire", Endpoint);
            Console.WriteLine();

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
            mapper.AddMapping<LogEntryMap>();
            mapper.AddMapping<MailTaskMap>();
            mapper.AddMapping<LocalScriptTaskMap>();
            mapper.AddMapping<RemoteScriptTaskMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            DeployJob.Store = config.BuildSessionFactory();
            LogsCompactonJob.Store = DeployJob.Store;
        }

        public void Stop()
        {
            _host.Dispose();
        }
    }
}
