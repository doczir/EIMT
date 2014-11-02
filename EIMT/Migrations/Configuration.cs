using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Validation;
using System.Text;
using System.Web.Mvc.Html;
using EIMT.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EIMT.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EIMT.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        /// Wrapper for SaveChanges adding the Validation Messages to the generated exception
        /// </summary>
        /// <param name="context">The context.</param>
        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }

        protected override void Seed(EIMT.Models.ApplicationDbContext context)
        {
            var userRoles = new List<IdentityRole> {new IdentityRole {Name = "Admin"}, new IdentityRole {Name = "User"}};

            using (var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext())))
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()))
                )
            {
                foreach (IdentityRole identityRole in userRoles)
                {
                    if (rm.RoleExists(identityRole.Name)) continue;
                    IdentityResult result = rm.Create(identityRole);
                    if (!result.Succeeded)
                        throw new DbEntityValidationException("Creating role " + identityRole.Name +
                                                              "failed with error(s): " + result.Errors.Aggregate((i, j) => i + ';' + j));
                }
                ApplicationUser user = um.FindByName("Admin");
                if (user == null)
                {
                    user = new ApplicationUser {UserName = "Admin"};
                    IdentityResult result = um.Create(user, "4Dm1np4ss");
                    if (!result.Succeeded)
                        throw new DbEntityValidationException("Creating role " + user.UserName +
                                                              "failed with error(s): " + result.Errors.Aggregate((i, j) => i + ';' + j));
                }

                if (!um.IsInRole(user.Id, "Admin"))
                {
                    IdentityResult result = um.AddToRole(user.Id, "Admin");
                    if (!result.Succeeded)
                        throw new DbEntityValidationException("Adding user '" + user.UserName +
                                                              "' to 'Admin' role failed with error(s): " + result.Errors.Aggregate((i, j) => i + ';' + j));
                }
            }

            SaveChanges(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
