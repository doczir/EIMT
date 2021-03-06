﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace EIMT.Models
{

    public class AttachedServiceProviderViewModel : ServiceProviderViewModel
    {
        [Display(Name = "User number")]
        public string UserNumber { get; set; }
    }

    public class InvoiceViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Service Provider")]
        public String ServiceProviderName { get; set; }

        [Display(Name = "Comment")]
        public String Comment { get; set; }

        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }

        [Display(Name = "Total")]
        public int Total { get; set; }

        [Display(Name = "Paid")]
        public Boolean Paid { get; set; }

        public InvoiceViewModel()
        {
        }

        public InvoiceViewModel(Invoice invoice)
        {
            Id = invoice.Id;
            Comment = invoice.Comment;
            Deadline = invoice.Deadline;
            Paid = invoice.Paid;
            ServiceProviderName = invoice.UserServiceProvider.ServiceProvider.Name;
            Total = invoice.Total;
        }
    }

    public class InvoiceDetailsViewModel : InvoiceViewModel
    {
        [Display(Name = "Service provider account number")]
        public string SPAccountNumber { get; set; }
    }

    public class AddServiceProviderViewModel
    {
        [Range(1, int.MaxValue)]
        [Required]
        [DisplayName("Last Invoice Total")]
        public int LastInvoiceTotal { get; set; }

        [Required]
        [DisplayName("User Number")]
        public string UserNumber { get; set; }

        [Required]
        [DisplayName("Service Provider")]
        public int SelectedServiceProvider { get; set; }
        

        public IEnumerable<ServiceProvider> ServiceProviders { get; set; }
    }
}