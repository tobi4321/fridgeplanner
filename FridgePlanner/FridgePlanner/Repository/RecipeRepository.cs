using FridgePlanner.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Repository
{
    public class RecipeRepository : Repository<Data.Recipe, RepoDataBaseContext>
    {
        private readonly RepoDataBaseContext _context;
        public RecipeRepository(RepoDataBaseContext context) : base(context)
        {
            _context = context;
        }
        // overload GetAll Method of base class to include recipeitems and recipesteps
        public new async Task<List<Recipe>> GetAll()
        {
            List<Recipe> recipes = await _context.DataRecipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps).ToListAsync();
            return recipes;
        }
        public new async Task<Recipe> Get(int id)
        {
            Recipe recipes = await _context.DataRecipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps).Where(x => x.Id == id).FirstAsync();
            return recipes;
        }
        // here are specific methods for the FridgeItem Repo
    }
}
