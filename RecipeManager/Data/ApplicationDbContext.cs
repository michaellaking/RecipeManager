using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeManager.Models;

namespace RecipeManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Rating> Ratings {get; set;}
        public DbSet<Recipe> Recipes {get; set;}
        public DbSet<Step> Steps {get; set;}

    }
}
