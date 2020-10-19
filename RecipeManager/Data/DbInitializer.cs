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
        //public static async Task<int> SeedCategories(IServiceProvider serviceProvider)
        //{
        //    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        //    if (!context.Categories.Any())
        //    {
        //        var categories = new List<Category>();

        //        categories.Add(new Category("breakfast", "meal"));
        //        categories.Add(new Category("lunch", "meal"));
        //        categories.Add(new Category("dinner", "meal"));

        //        categories.Add(new Category("brunch", "meal"));
        //        categories.Add(new Category("snack", "meal"));
        //        categories.Add(new Category("beverage", "drink"));

        //        categories.Add(new Category("low sugar", "diet"));
        //        categories.Add(new Category("keto", "diet"));
        //        categories.Add(new Category("paleo", "diet"));

        //        categories.Add(new Category("vegan", "diet"));
        //        categories.Add(new Category("vegitarian", "diet"));
        //        categories.Add(new Category("dairy free", "diet"));

        //        context.AddRange(categories);
        //        await context.SaveChangesAsync();

        //        return 1;
        //    }
        //    return 0;
        //}

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
                UserName = "admin@recipe.ca",
                Email = "admin@recipe.ca",
                FirstName = "Curtis",
                LastName = "Admin",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(adminUser, "Password!1");
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Assign user to Manager role
            result = await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!result.Succeeded)
                return 2;  // should log an error message here

            // Create Player User
            var memberUser = new ApplicationUser
            {
                UserName = "user@recipe.ca",
                Email = "user@recipe.ca",
                FirstName = "Curtis",
                LastName = "User",
                EmailConfirmed = true
            };
            result = await userManager.CreateAsync(memberUser, "Password!1");
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
