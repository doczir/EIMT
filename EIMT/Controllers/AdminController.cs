using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EIMT.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EIMT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        [HttpGet]
        public ActionResult DeleteUser(string email)
        {
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                var user = um.FindByName(email);
                if (user != null)
                {
                    um.Delete(user);
                }
            }
            
            return RedirectToAction("ListUsers", "Home");
        }

        [HttpGet]
        public ActionResult EditUser(string email)
        {
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                return View(um.FindByName(email));
            }
        }

        [HttpPost]
        public ActionResult EditUser(ApplicationUser user)
        {
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                var dbUser = um.FindByName(user.Email);
                if (dbUser != null)
                {
                    dbUser.Name = user.Name;
                    dbUser.Address = user.Address;
                    dbUser.ConfirmedByAdmin = user.ConfirmedByAdmin;

                    um.Update(dbUser);
                }
            }

            return RedirectToAction("ListUsers", "Home");
        }

        [HttpGet]
        public ActionResult CreateServiceProvider()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateServiceProvider(ServiceProviderViewModel spvm)
        {
            if (ModelState.IsValid)
            {
                // TODO Create service provider
            }

            return View();
        }

        [HttpGet]
        public ActionResult DeleteServiceProvider(string name)
        {
            return RedirectToAction("ListServiceProviders", "Admin");
        }

        [HttpGet]
        public ActionResult EditServiceProvider()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditServiceProvider(ServiceProviderViewModel spvm)
        {
            if (ModelState.IsValid)
            {
                // TODO Edit service provider
            }

            return View();
        }

	}
}