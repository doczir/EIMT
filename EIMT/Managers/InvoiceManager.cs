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

        public List<Invoice> GetInvoices(ApplicationUser user)
        {
            if (user == null)
            {
                throw new Exception("The user parameter can not be null!");
            }

            var invoices = _context.Invoices.Where(i => i.UserServiceProvider.User.Id == user.Id).ToList();

            return invoices;
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}