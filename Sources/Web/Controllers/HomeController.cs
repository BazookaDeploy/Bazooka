using DataAccess.Read;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using Web.Models;
using System.Linq;
using System.Configuration;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            bool admin, appsAdmin;

            using (var ApplicationDbContext = new ApplicationDbContext())
            {
                var id = User.Identity.GetUserId();
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext));
                admin = manager.FindById(id).Administrator;

                using(var context = new ReadContext())
                {
                    appsAdmin = context.Query<ApplicationAdministratorDto>().Any(x => x.UserId ==id);
                }
                
            }

            ViewBag.Admin = admin;
            ViewBag.AppsAdmin = appsAdmin;
            ViewBag.Title = "Home Page";
            ViewBag.ActiveDirectory = ConfigurationManager.AppSettings["activeDirectory"];

            return View();
        }
    }
}
