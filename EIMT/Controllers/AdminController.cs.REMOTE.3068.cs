﻿using System.Web.Mvc;
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
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()))
                )
            {
                var user = um.FindByName(email);
                if (user != null)
                {
                    um.Delete(user);
                }
            }

            return RedirectToAction("Users", "Home");
        }

        [HttpGet]
        public ActionResult EditUser(string email)
        {
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()))
                )
            {
                return View(um.FindByName(email));
            }
        }

        [HttpPost]
        public ActionResult EditUser(ApplicationUser user)
        {
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()))
                )
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

            return RedirectToAction("Users", "Home");
        }

        [HttpPost]
        public ActionResult SetConfirmed(string email)
        {
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()))
                )
            {
                var user = um.FindByName(email);
                if (user != null)
                {
                    user.ConfirmedByAdmin = !user.ConfirmedByAdmin;
                    um.Update(user);
                }
            }

            return RedirectToAction("Users", "Home");
        }
    }
}