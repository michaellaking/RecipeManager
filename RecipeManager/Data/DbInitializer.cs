using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeManager.Data;
using RecipeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManager.Data
{
    public static class DbInitializer
    {
        public static AppSecrets appSecrets { get; set; }

        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Check if roles already exist and exit if there are
            if (roleManager.Roles.Count() > 0)
                return 1;  // should log an error message here

            // Seed roles
            int result = await SeedRoles(roleManager);
            if (result != 0)
                return 2;  // should log an error message here

            // Check if users already exist and exit if there are
            if (userManager.Users.Count() > 0)
                return 3;  // should log an error message here

            // Seed users
            result = await SeedUsers(userManager);
            if (result != 0)
                return 4;  // should log an error message here

            return 0;
        }

        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Manager Role
            var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Create Player Role
            result = await roleManager.CreateAsync(new IdentityRole("User"));
            if (!result.Succeeded)
                return 2;  // should log an error message here

            return 0;
        }

        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Create Manager User
            var adminUser = new ApplicationUser
            {
                UserName = appSecrets.AdminEmail,
                Email = appSecrets.AdminEmail,
                FirstName = "Curtis",
                LastName = "Admin",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(adminUser, appSecrets.AdminPassword);
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Assign user to Manager role
            result = await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!result.Succeeded)
                return 2;  // should log an error message here

            // Create Player User
            var memberUser = new ApplicationUser
            {
                UserName = appSecrets.UserEmail,
                Email = appSecrets.UserEmail,
                FirstName = "Curtis",
                LastName = "User",
                EmailConfirmed = true
            };
            result = await userManager.CreateAsync(memberUser, appSecrets.UserPassword);
            if (!result.Succeeded)
                return 3;  // should log an error message here

            // Assign user to Player role
            result = await userManager.AddToRoleAsync(memberUser, "User");
            if (!result.Succeeded)
                return 4;  // should log an error message here

            return 0;
        }
    }
}
