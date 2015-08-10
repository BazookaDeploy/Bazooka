using System.Web.Http;

namespace Agent.Controllers
{
    public class HealthController : ApiController
    {
        /// <summary>
        ///     Just to check if someone responds...
        ///     Useful if we need to verify if an agent is alive
        /// </summary>
        /// <returns>Pong obviously</returns>
        [HttpGet]
        public string Ping()
        {
            return "pong";
        }
    }
}
