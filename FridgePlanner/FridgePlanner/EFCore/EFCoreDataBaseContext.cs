using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.EFCore
{
    public class EFCoreDataBaseContext : DbContext
    {
        public EFCoreDataBaseContext(DbContextOptions<EFCoreDataBaseContext> options)
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

        public DbSet<Data.FridgeItem> DataFridgeItems { get; set; }
        public DbSet<Data.ShoppingItem> DataShoppingItems { get; set; }
        public DbSet<Data.Recipe> DataRecipes { get; set; }
        public DbSet<Data.RecipeItem> DataRecipeItems { get; set; }
        public DbSet<Data.RecipeStep> DataRecipeSteps { get; set; }

    }
}
