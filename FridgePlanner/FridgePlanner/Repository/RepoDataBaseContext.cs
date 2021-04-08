using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Repository
{
    public class RepoDataBaseContext : DbContext
    {
        public RepoDataBaseContext(DbContextOptions<RepoDataBaseContext> options)
            : base(options)
        {
            try
            {
                //If the Database already exists, nothing happens. Otherwise a new Database would be created
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public DbSet<Entities.FridgeItem> FridgeItems { get; set; }
        public DbSet<Entities.ShoppingItem> ShoppingItems { get; set; }
        public DbSet<Entities.Recipe> Recipes { get; set; }
        public DbSet<Entities.RecipeItem> RecipeItems { get; set; }
        public DbSet<Entities.RecipeStep> RecipeSteps { get; set; }

    }
}
