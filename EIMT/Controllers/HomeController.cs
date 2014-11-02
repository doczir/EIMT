using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using EIMT.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace EIMT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult AddServiceProvider()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult ListInvoices()
        {
            return View();
        }

        [Authorize(Roles="Admin")]
        public ActionResult Users()
        {
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                return View(um.Users.ToList().Where(u => um.IsInRole(u.Id, "User")).ToList());
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ServiceProviders()
        {
            return View();
        }

    }
}