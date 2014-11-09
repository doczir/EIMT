using System.ComponentModel;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
=======
>>>>>>> daad53afcd8b48e96e18f44c3f6d41276ba8c9d2
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EIMT.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
<<<<<<< HEAD
        [Display(Name = "Name")]
=======
        [DefaultValue(false)]
        public bool ConfirmedByAdmin { get; set; }

>>>>>>> daad53afcd8b48e96e18f44c3f6d41276ba8c9d2
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Confirmed by admin")]
        public bool ConfirmedByAdmin { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}