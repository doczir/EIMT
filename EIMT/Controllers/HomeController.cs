using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
        public ActionResult ListUsers()
        {
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                return View(um.Users.ToList().Where(u => um.IsInRole(u.Id, "User")).ToList());
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ListServiceProviders()
        {
            using (var context = new ApplicationDbContext())
            {
                IList<ServiceProviderViewModel> spvmList = new List<ServiceProviderViewModel>();

                foreach (var serviceProvider in context.ServiceProviders)
                {
                    spvmList.Add(new ServiceProviderViewModel() {Id = serviceProvider.Id, Name = serviceProvider.Name, AccountNumber = serviceProvider.AccountNumber});
                }

                return View(spvmList);
            }
        }

    }
}