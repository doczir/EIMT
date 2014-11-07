using System.ComponentModel.DataAnnotations;

namespace EIMT.Models
{
    public class ServiceProviderIdentity
    {
        public string Id { get; set; }
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
        public string Id { get; set; }
        [Key]
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
    }
}