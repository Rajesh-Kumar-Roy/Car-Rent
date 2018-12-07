using CarRentApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarRentApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarRentApp.Context.RentDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarRentApp.Context.RentDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            ////Step 1 Create the user.
            //var passwordHasher = new PasswordHasher();
            //var user = new IdentityUser("Administrator");
            //user.PasswordHash = passwordHasher.HashPassword("Admin123456");
            ////user.SecurityStamp = Guid.NewGuid().ToString();

            ////Step 2 Create and add the new Role.
            //var roleToChoose = new IdentityRole("Admin");
            //context.Roles.Add(roleToChoose);

            ////Step 3 Create a role for a user
            //var role = new IdentityUserRole();
            //role.RoleId = roleToChoose.Id;
            //role.UserId = user.Id;

            ////Step 4 Add the role row and add the user to DB)
            //user.Roles.Add(role);
            //context.Users.Add(user);
            
        }
    }
}
