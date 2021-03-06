﻿using DataAccess.Read;
using Hangfire.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Jobs
{
    public class CleanJob
    {
        public static void Execute()
        {

            List<string> agents;

            using (var db = new ReadContext())
            {
                agents = db.Enviroments
                           .ToList()
                           .SelectMany(x => x.Agents)
                           .Select(x => x.Address)
                           .Distinct()
                           .ToList();
            }

            using (var client = new HttpClient())
            {
                foreach (var agent in agents)
                {
                    try
                    {
                        client.BaseAddress = new Uri(agent);
                        var result2 = client.GetAsync("/api/deploy/clean").Result;
                    }
                    catch (Exception e) {

                        LogProvider.GetCurrentClassLogger().WarnException(e.Message, e);

                        // do nothing, an agent may be unavailable at this time 
                        // or the network down. Just pass to the next
                    }
                }
            }
        }
    }
}
