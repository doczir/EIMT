using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EIMT.Models
{
    public class ServiceProvider
    {

        [Key]
        public string Id { get; set; }
        public int Name { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
    }
}