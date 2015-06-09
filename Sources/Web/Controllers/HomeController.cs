using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            bool admin;

            using (var ApplicationDbContext = new ApplicationDbContext())
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext));
                admin = manager.FindById(User.Identity.GetUserId()).Administrator;
            }

            ViewBag.Admin = admin;
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
