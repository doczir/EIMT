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
        }

        public List<Invoice> GetInvoices(ApplicationUser user)
        {
            if (user == null)
            {
                throw new Exception("The user parameter can not be null!");
            }

            var invoices = _context.Invoices.Where(i => i.UserServiceProvider.User.Id == user.Id).ToList();

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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}