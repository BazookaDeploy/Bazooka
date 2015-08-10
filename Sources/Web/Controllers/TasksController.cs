using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Web.Controllers
{
    public class TasksController : ApiController
    {
        private ReadContext db = new ReadContext();

        public ICollection<TaskDto> Get(int enviromentId)
        {
            return db.Tasks.Where(x => x.EnviromentId == enviromentId).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
