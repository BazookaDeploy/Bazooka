using DataAccess.Read;
using DataAccess.Write;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Jobs
{
    public class HealthCheckJob
    {
        /// <summary>
        ///     Nhibernate store to use to save the data    
        /// </summary>
        public static ISessionFactory Store { get; set; }

        public static void Execute()
        {

            List<AgentDto> agents;

            using (var db = new ReadContext())
            {
                agents = db.Enviroments
                           .ToList()
                           .SelectMany(x => x.Agents)
                           .ToList();
            }

            foreach (var agent in agents)
            {

                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(agent.Address);
                        var result2 = client.GetAsync("/api/Health/ping").Result;

                        using (var session = Store.OpenSession())
                        {
                            var a = session.Load<Agent>(agent.Id);
                            a.LastCheck = true;
                            a.LastStatusCheck = DateTime.UtcNow;
                        }
                    }
                    catch (Exception e)
                    {
                        using (var session = Store.OpenSession())
                        {
                            var a = session.Load<Agent>(agent.Id);
                            a.LastCheck = false;
                            a.LastStatusCheck = DateTime.UtcNow;
                        }
                    }
                }
            }
        }
    }
}
