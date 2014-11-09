using System;
using System.Web.Mvc;
using System.Web.Routing;
using EIMT.Managers;
using EIMT.Models;
using Microsoft.Ajax.Utilities;
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
            
            return RedirectToAction("ListUsers", "Home");
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

            return RedirectToAction("ListUsers", "Home");
        }

        [HttpGet]
        public ActionResult CreateServiceProvider()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateServiceProvider(CreateServiceProviderViewModel spvm)
        {
            if (ModelState.IsValid)
            {
                using (var spm = new ServiceProviderManager(new ApplicationDbContext()))
                {
                    try
                    {
                        spm.RegisterServiceProvider(spvm.Name, spvm.Password, spvm.AccountNumber);

                        return RedirectToAction("ListServiceProviders", "Home");
                    } catch (Exception e) {
                        ModelState.AddModelError("", e.Message);

                        return View(spvm);
                    }
                }
            }

            return View(spvm);
        }

        [HttpGet]
        public ActionResult DeleteServiceProvider(int id)
        {
            using (var spm = new ServiceProviderManager(new ApplicationDbContext()))
            {
                var res = spm.Delete(id);

                return RedirectToAction("ListServiceProviders", "Home");
            }
        }

        [HttpGet]
        public ActionResult EditServiceProviderData(int id)
        {
            using (var spm = new ServiceProviderManager(new ApplicationDbContext()))
            {
                ServiceProviderIdentity spi = spm.GetById(id);
                if (spi != null)
                {
                    EditServiceProviderDataViewModel espdvm = new EditServiceProviderDataViewModel()
                    {
                        Id = spi.Id,
                        Name = spi.Name,
                        AccountNumber = spi.AccountNumber
                    };

                    return View(espdvm);
                }

                return RedirectToAction("ListServiceProviders", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditServiceProviderData(EditServiceProviderDataViewModel espdvm)
        {
            if (ModelState.IsValid)
            {
                var context = new ApplicationDbContext();
                using (var spm = new ServiceProviderManager(context))
                {
                    ServiceProviderIdentity spi = spm.GetById(espdvm.Id);
                    if (spi != null)
                    {
                        spi.Name = espdvm.Name;
                        spi.AccountNumber = espdvm.AccountNumber;

                        try
                        {
                            spm.Modify(spi);

                            return RedirectToAction("ListServiceProviders", "Home");
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("", e.Message);

                            return View(espdvm);
                        }
                    }
                }
            }
            
            return View(espdvm);
        }

        [HttpGet]
        public ActionResult EditServiceProviderPassword(int id)
        {
            using (var spm = new ServiceProviderManager(new ApplicationDbContext()))
            {
                var spi = spm.GetById(id);
                if (spi != null)
                {
                    return View(new EditServiceProviderPasswordViewModel() {Id = spi.Id});
                }

                return RedirectToAction("ListServiceProviders", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditServiceProviderPassword(EditServiceProviderPasswordViewModel esppvm)
        {
            if (ModelState.IsValid)
            {
                using (var spm = new ServiceProviderManager(new ApplicationDbContext()))
                {
                    if (spm.Authenticate(esppvm.Id, esppvm.OldPassword) != null)
                    {
                        try
                        {
                            spm.ModifyPassword(esppvm.Id, esppvm.Password);
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("", e.Message);

                            return View(esppvm);
                        }

                        return RedirectToAction("ListServiceProviders", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong old password!");

                        return View(esppvm);
                    }
                }
            }

            return View(esppvm);
        }
	}
}