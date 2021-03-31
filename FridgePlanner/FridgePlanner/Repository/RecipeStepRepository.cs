using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Repository
{
    public class RecipeStepRepository : Repository<Data.RecipeStep, RepoDataBaseContext>
    {
        public RecipeStepRepository(RepoDataBaseContext context) : base(context)
        {

        }
        // here are specific methods for the FridgeItem Repo
    }
}
