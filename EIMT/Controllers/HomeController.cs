using System.Web.Mvc;

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
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ServiceProviders()
        {
            return View();
        }

    }
}