﻿[assembly: Microsoft.Owin.OwinStartup(typeof(Controller.Startup))]
namespace Controller
{
    using Hangfire;
    using Hangfire.SqlServer;
    using Hangfire.SqlServer.Msmq;
    using Jobs;
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

            RecurringJob.AddOrUpdate(() => CleanJob.Execute(), System.Configuration.ConfigurationManager.AppSettings["CleanJobSchedule"]);
            RecurringJob.AddOrUpdate(() => LogsCompactionJob.Execute(), System.Configuration.ConfigurationManager.AppSettings["LogCompactionJobSchedule"]);
            RecurringJob.AddOrUpdate(() => GalleryCleaningJob.Execute(), System.Configuration.ConfigurationManager.AppSettings["GalleryCleanupJob"]);
            RecurringJob.AddOrUpdate(() => HealthCheckJob.Execute(), System.Configuration.ConfigurationManager.AppSettings["HealthCheckJob"]);

        }
    }
}
