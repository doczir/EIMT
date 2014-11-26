using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Web.ModelBinding;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EIMT.Models
{
    public class ServiceProviderIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }

        public ServiceProviderIdentity(ServiceProvider sp)
        {
            Id = sp.Id;
            Name = sp.Name;
            AccountNumber = sp.AccountNumber;
        }
    }

    public class ServiceProvider
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
    }

    public class UserServiceProvider
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
        public int LastInvoiceTotal { get; set; }
        public string UserNumber { get; set; }
    }

    public class Invoice
    {
        public int Id { get; set; }

        public UserServiceProvider UserServiceProvider { get; set; }

        public String Comment { get; set; }

        public int Total { get; set; }

        public DateTime Deadline { get; set; }
    }
}