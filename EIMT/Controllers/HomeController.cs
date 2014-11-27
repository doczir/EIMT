using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EIMT.Managers;
using EIMT.Models;
using Microsoft.Ajax.Utilities;
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
        public ActionResult ServiceProviders()
        {
            using (var context = new ApplicationDbContext())
            {
                var uid = User.Identity.GetUserId();
                var usps = context.UserServiceProvider.Include("ServiceProvider").Where(usp => usp.User.Id == uid).ToList();

                List<AttachedServiceProviderViewModel> aspvms = new List<AttachedServiceProviderViewModel>();
                foreach (var usp in usps)
                {
                    AttachedServiceProviderViewModel aspvm = new AttachedServiceProviderViewModel()
                    {
                        Id = usp.ServiceProvider.Id,
                        Name = usp.ServiceProvider.Name,
                        UserNumber = usp.UserNumber,
                        AccountNumber = usp.ServiceProvider.AccountNumber
                    };

                    aspvms.Add(aspvm);
                }

                return View(aspvms);
            }
        }

        [Authorize(Roles = "User")]
        public ActionResult Invoices()
        {
            using (var im = new InvoiceManager(new ApplicationDbContext()))
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                string userId = User.Identity.GetUserId();

                var user = um.FindById(userId);

                var invoices = im.GetInvoices(user);
                
                List<InvoiceViewModel> ivms = new List<InvoiceViewModel>();
                foreach (var invoice in invoices)
                {
                    ivms.Add(new InvoiceViewModel(invoice));
                }

                return View(ivms);
            }
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