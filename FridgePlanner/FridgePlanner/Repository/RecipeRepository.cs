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
        // here are specific methods for the Recipe Repo

        public async Task<Recipe> AddItem(Recipe recipe,RecipeItem item)
        {
            var rec = _context.DataRecipes.Include(r => r.RecipeItems).Include(r => r.RecipeSteps).Where(x=>x.Id == recipe.Id).First();
            rec.RecipeItems.Add(item);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> DeleteItem(Recipe recipe,RecipeItem item)
        {
            var rec = _context.DataRecipes.Include(r => r.RecipeItems).Include(r => r.RecipeSteps).Where(x => x.Id == recipe.Id).First();
            rec.RecipeItems.Remove(item);

            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> UpdateItem(Recipe recipe,RecipeItem item)
        {
            var rec = _context.DataRecipes.Include(r => r.RecipeItems).Include(r => r.RecipeSteps).Where(x => x.Id == recipe.Id).First();
            var it = rec.RecipeItems.Where(i => i.Id == item.Id).First();
            it.Name = item.Name;
            it.Amount = item.Amount;
            it.Unit = item.Unit;

            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> AddStep(Recipe recipe, RecipeStep step)
        {
            var rec = _context.DataRecipes.Include(r => r.RecipeItems).Include(r => r.RecipeSteps).Where(x => x.Id == recipe.Id).First();
            rec.RecipeSteps.Add(step);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> DeleteStep(Recipe recipe, RecipeStep step)
        {
            var rec = _context.DataRecipes.Include(r => r.RecipeItems).Include(r => r.RecipeSteps).Where(x => x.Id == recipe.Id).First();
            rec.RecipeSteps.Remove(step);

            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> UpdateStep(Recipe recipe, RecipeStep step)
        {
            var rec = _context.DataRecipes.Include(r => r.RecipeItems).Include(r => r.RecipeSteps).Where(x => x.Id == recipe.Id).First();
            var st = rec.RecipeSteps.Where(i => i.Id == step.Id).First();
            st.Titel = step.Titel;
            st.Text = step.Text;
            st.StepNumber = step.StepNumber;

            await _context.SaveChangesAsync();
            return recipe;
        }


    }
}
