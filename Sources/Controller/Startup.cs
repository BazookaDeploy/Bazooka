[assembly: Microsoft.Owin.OwinStartup(typeof(Controller.Startup))]
namespace Controller
{
    using Hangfire;
    using Hangfire.SqlServer;
    using Hangfire.SqlServer.Msmq;
    using Jobs;
    using Microsoft.Owin;
    using Owin;
    using System;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.UseWelcomePage("/");
            app.UseHangfire(config =>
            {
                var options = new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) };
                config.UseSqlServerStorage("DataContext", options).UseMsmqQueues(@".\private$\hangfire-0");
                config.UseServer();               
            });

            RecurringJob.AddOrUpdate(() => CleanJob.Execute(), Cron.Daily(0, 0));
        }
    }
}
