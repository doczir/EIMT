using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EIMT.Models;

namespace EIMT.Managers
{
    public class InvoiceManager : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public InvoiceManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Boolean AddInvoice(Invoice invoice)
        {
            if (invoice == null)
            {
                throw new Exception("Invalid argument!");
            }

            _context.Invoices.Add(invoice);

            _context.SaveChanges();

            return true;
        }

        public InvoiceDetailsViewModel GetInvoiceDetails(int id)
        {
            Invoice invoice = _context.Invoices.Include("UserServiceProvider").Include("UserServiceProvider.ServiceProvider").First(i => i.Id == id);

            if (invoice == null)
            {
                throw new Exception("Not an existing invoice!");
            }

            InvoiceDetailsViewModel idvm = new InvoiceDetailsViewModel()
            {
                Id = invoice.Id,
                Comment = invoice.Comment,
                Deadline = invoice.Deadline,
                Paid = invoice.Paid,
                Total = invoice.Total,
                ServiceProviderName = invoice.UserServiceProvider.ServiceProvider.Name,
                SPAccountNumber = invoice.UserServiceProvider.ServiceProvider.AccountNumber
            };

            return idvm;
        }

        public List<Invoice> GetInvoices(ApplicationUser user)
        {
            if (user == null)
            {
                throw new Exception("The user parameter can not be null!");
            }

            var invoices = _context.Invoices.Include("UserServiceProvider.User").Include("UserServiceProvider.ServiceProvider").Where(i => i.UserServiceProvider.User.Id == user.Id).ToList();

            return invoices;
        }

        public Boolean ChangeInvoicePaid(int id)
        {
            var invoice = _context.Invoices.Where(i => i.Id == id).ToList().First();

            if (invoice == null)
            {
                throw new Exception("Not an existing invoice!");
            }

            invoice.Paid = !invoice.Paid;

            _context.SaveChanges();

            return true;
        }

        public Boolean DeleteInvoice(int id)
        {
            var invoice = _context.Invoices.Where(i => i.Id == id).ToList().First();

            if (invoice == null)
            {
                throw new Exception("Not an existing invoice!");
            }

            _context.Invoices.Remove(invoice);

            _context.SaveChanges();

            return true;
        }

        public UserServiceProvider GetUserServiceProvider(int spId, string userNumber)
        {
            UserServiceProvider userServiceProvider =
                _context.UserServiceProvider.Include("User").First(usp => usp.ServiceProvider.Id == spId && usp.UserNumber == userNumber);

            return userServiceProvider;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}