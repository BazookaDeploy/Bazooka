namespace Jobs
{
    using DataAccess.Read;
    using DataAccess.Write;
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LogsCompactionJob
    {
        /// <summary>
        ///     Nhibernate store to use to save the data    
        /// </summary>
        public static ISessionFactory Store { get; set; }

        public static void Execute()
        {
            List<int> deploys;

            using (var db = new ReadContext())
            {
                var oldDate = DateTime.UtcNow.AddDays(-30);

                deploys = db.Deployments
                            .Where(x => x.Status == DataAccess.Write.Status.Ended)
                            .Where(x => x.StartDate < oldDate)
                            .Where(x => x.Logs.Count > 1)
                            .Select(x => x.Id)
                            .ToList();
            }

            foreach (var deploy in deploys)
            {
                using (var session = Store.OpenSession())
                {
                    using (var db = new ReadContext())
                    {
                        var current = db.Deployments
                                    .Single(x => x.Id == deploy);

                        foreach (var log in current.Logs)
                        {
                            var logLine = session.Load<LogEntry>(log.Id);
                            session.Delete(logLine);
                        }

                        session.Save(new LogEntry()
                        {
                            DeploymentId = deploy,
                            Error = false,
                            TaskName = "History",
                            Text = "Logs removed due to compaction",
                            TimeStamp = DateTime.UtcNow
                        });

                        session.Flush();
                    }
                }
            }
        }
    }
}
