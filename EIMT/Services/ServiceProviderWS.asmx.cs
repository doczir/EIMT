using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EIMT.Managers;
using EIMT.Models;

namespace EIMT.Services
{
    /// <summary>
    /// Summary description for Test
    /// </summary>
    [WebService(Namespace = "http://eimt.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceProviderWS : System.Web.Services.WebService
    {

        [WebMethod]
        public string SendInvoice(int spId, string password, string userNumber, string comment, int total, DateTime deadline)
        {
            using (var context = new ApplicationDbContext())
            using (var im = new InvoiceManager(context))
            using (var spm = new ServiceProviderManager(context))
            {
                //context.Configuration.LazyLoadingEnabled = false;

                ServiceProviderIdentity spi = spm.Authenticate(spId, password);
                if (spi == null)
                {
                    return "Wrong password";
                }

                UserServiceProvider usp = im.GetUserServiceProvider(spId, userNumber);
                if (usp == null)
                {
                    return "The user with the provided user number is not available.";
                }

                Invoice invoice = new Invoice()
                {
                    UserServiceProvider = usp,
                    Comment = comment,
                    Total = total,
                    Deadline = deadline,
                    Paid = false
                };

                try
                {
                    im.AddInvoice(invoice);
                }
                catch (Exception e)
                {
                    return "Adding new invoice was unsuccessful.";
                }

                return "Adding new invoice was successful.";
            }
        }
    }
}
