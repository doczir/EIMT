using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EIMT.Managers;
using EIMT.Models;
using Microsoft.AspNet.Identity;

namespace EIMT.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {

        [HttpGet]
        public ActionResult AddServiceProvider()
        {
            using (var dbc = new ApplicationDbContext())
            {
                var userId = User.Identity.GetUserId();
                var sps = dbc.ServiceProviders.Where(
                    sp => !dbc.UserServiceProvider.Any(usp => usp.User.Id == userId && usp.ServiceProvider.Id == sp.Id)).ToList();

                var spvm = new AddServiceProviderViewModel()
                {
                    ServiceProviders = sps
                };

                return View(spvm);
            }
        }

        [HttpPost]
        public ActionResult AddServiceProvider(AddServiceProviderViewModel spvm)
        {
            if (!ModelState.IsValid) return View(spvm);
            using (var dbc = new ApplicationDbContext())
            {
                try
                {
                    dbc.UserServiceProvider.Add(spvm.UserServiceProvider);
                        
                    return RedirectToAction("AddServiceProvider", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);

                    var userId = User.Identity.GetUserId();
                    var sps = dbc.ServiceProviders.Where(
                        sp => dbc.UserServiceProvider.Any(uspo => uspo.User.Id == userId && uspo.ServiceProvider.Id == sp.Id)).ToList();

                    spvm.ServiceProviders = sps;

                    return View(spvm);
                }
            }
        }
    }
}