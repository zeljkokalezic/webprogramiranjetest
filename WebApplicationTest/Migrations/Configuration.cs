namespace WebApplicationTest.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplicationTest.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WebApplicationTest.Models.ApplicationDbContext";
        }

        protected override void Seed(WebApplicationTest.Models.ApplicationDbContext context)
        {
            //create roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(Role.ADMIN))
                roleManager.Create(new IdentityRole(Role.ADMIN));

            if (!roleManager.RoleExists(Role.STUDENT))
                roleManager.Create(new IdentityRole(Role.STUDENT));

            //create administrator
            var adminEmail = WebConfigurationManager.AppSettings["AdministratorEmail"];
            if (!context.Administrators.Any(x => x.UserName == adminEmail))
            {
                var password = new ApplicationUserManager(new UserStore<ApplicationUser>(context)).PasswordHasher.HashPassword("password");
                var administrator = new Administrator()
                {
                    AdministratorProperty = "Called from seed",
                    Email = adminEmail,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    UserName = adminEmail,
                    PasswordHash = password,
                    Birthdate = DateTime.Now
                };

                context.Administrators.AddOrUpdate(administrator);
                context.SaveChanges();

                var adminManager = new UserManager<Administrator>(new UserStore<Administrator>(context));
                adminManager.AddToRole(administrator.Id, Role.ADMIN);
            }
        }
    }
}
