using DataAccess.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jobs
{
    public class CleanJob
    {
        public static void Execute() {

            List<string> agents;

            using (var db = new ReadContext())
            {
                agents = db.DeployUnits.Select(x => x.Machine).Distinct().ToList();
            }

            foreach (var agent in agents)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(agent);
                    var result2 = client.GetAsync("/api/deploy/clean").Result;
                }
            }
        }
    }
}
