namespace Web.Controllers
{
    using DataAccess.Read;
    using DataAccess.Write;
    using Hangfire;
    using Jobs;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    public class MaintenanceTaskController : BaseController
    {

        public IReadContext db { get; set; }


        public MaintenanceTaskDto Get(int id)
        {
            return db.Query<MaintenanceTaskDto>().Include(x => x.Logs).Single(X => X.Id == id);
        }

        [HttpGet]
        public ICollection<MaintenanceTaskDto> Lista(int skip, int take)
        {
            return db.Query<MaintenanceTaskDto>().OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList();
        }


        [HttpPost]
        public void Run(int agentId, int taskId,[System.Web.Http.FromBody]Dictionary<string,string> parameters)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                var deploy = new MaintenanceTask()
                {
                    TemplatedTaskId = taskId,
                    AgentId = agentId,
                    Status = Status.Queud,
                    UserId = User.Identity.GetUserId()
                };

                session.Save(deploy);

                session.Flush();

                BackgroundJob.Enqueue(() => MaintenanceJob.Execute(deploy.Id, parameters));
            };
        }


    }
}