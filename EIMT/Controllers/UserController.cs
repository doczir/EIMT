using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EIMT.Managers;
using EIMT.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

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
            using (var dbc = new ApplicationDbContext())
            using (var um = new ApplicationUserManager(new UserStore<ApplicationUser>(dbc)))
            using (var spm = new ServiceProviderManager(dbc))
            {
                if (!ModelState.IsValid)
                {
                    var userId = User.Identity.GetUserId();
                    var sps = dbc.ServiceProviders.Where(
                        sp => !dbc.UserServiceProvider.Any(uspo => uspo.User.Id == userId && uspo.ServiceProvider.Id == sp.Id)).ToList();

                    spvm.ServiceProviders = sps;

                    return View(spvm);
                }
                try
                {
                    dbc.UserServiceProvider.Add(new UserServiceProvider()
                    {
                        LastInvoiceTotal = spvm.LastInvoiceTotal,
                        UserNumber = spvm.UserNumber,
                        User = um.FindById(User.Identity.GetUserId()),
                        ServiceProvider = spm.GetSpById(spvm.SelectedServiceProvider)
                    });

                    dbc.SaveChanges();

                    return RedirectToAction("ServiceProviders", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);

                    var userId = User.Identity.GetUserId();
                    var sps = dbc.ServiceProviders.Where(
                        sp => !dbc.UserServiceProvider.Any(uspo => uspo.User.Id == userId && uspo.ServiceProvider.Id == sp.Id)).ToList();

                    spvm.ServiceProviders = sps;

                    return View(spvm);
                }
            }
        }

        public ActionResult DeleteServiceProvider(int spId)
        {
            using (var dbc = new ApplicationDbContext())
            {
                var uid = User.Identity.GetUserId();
                var toRemove = dbc.UserServiceProvider.Where(usp => usp.ServiceProvider.Id == spId && usp.User.Id == uid).ToList();
                toRemove.ForEach(usp => dbc.UserServiceProvider.Remove(usp));

                dbc.SaveChanges();

                return RedirectToAction("ServiceProviders", "Home", dbc.UserServiceProvider.Where(usp => usp.User.Id == uid));
            }

            
        }
    }
}